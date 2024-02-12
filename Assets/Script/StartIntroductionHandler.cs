using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartIntroductionHandler : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button startButton;
    private int _currentPage;

    // creates an accessible component in the editor, which allows to add as many Text pages to the Introduction as one would like
    // press "+" in the according component in the editor to add a new field for text input for each Page
    [TextArea(15, 20)]
    [SerializeField] private string[] textPageList;     

    // text field of the UI element, which is used to show the currently selected page
    [SerializeField] private TextMeshProUGUI textDisplay;

    // Event when a page changes
    [System.Serializable]
    public class PageChangeEvent : UnityEvent<int>
    {}

    [Tooltip("Event that is Called when the page updates. (might not be different than before)")]
    [SerializeField] private PageChangeEvent onPageChange;

    // check if there has been entered a page via the input field in the editor
    // if so, show the first page on the UI, else initialize the page array with an empty string
    // UI always starts displaying page 0
    void Start()
    {
        // set up first page
        _currentPage = 0;
        if (textPageList.Length <= 0)
        {
            textPageList = new string[1];
            textPageList[0] = "";
        }
        textDisplay.text = textPageList[0];

        // set up buttons
        CheckIfShouldEnableStartButton();
        previousButton.interactable = false;
        if (textPageList.Length <= 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }

        //Call Event with Initial State
        onPageChange.Invoke(_currentPage);
    }

    // if on the last Page enable StartButton
    public void CheckIfShouldEnableStartButton()
    {
        if (_currentPage+1 == textPageList.Length)
        {
            startButton.interactable = true;
        } else
        {
            startButton.interactable = false;
        }
    }

    // if the "next" button is clicked, the UI will show the next page in line
    public void ShowNextPage()
    {
        // increase page number
        _currentPage++;

        // if page number is out of bounds, reset to max page number
        if (_currentPage >= textPageList.Length)
        {
            _currentPage = textPageList.Length - 1;
        }

        // display question according to page number
        textDisplay.text = textPageList[_currentPage];
        
        previousButton.interactable = true;

        // disable "next" button if UI currently shows the last page
        if (_currentPage == textPageList.Length - 1)
        {
            nextButton.interactable = false;
        }

        CheckIfShouldEnableStartButton();

        //Notify Listners
        onPageChange.Invoke(_currentPage);
    }

    // if the "previous" button is clicked, the UI will show the previous page
    public void ShowPrevPage()
    {
        // decrease page number
        _currentPage--;

        // if page number is out ouf bounds, reset to min page number
        if (_currentPage < 0)
        {
            _currentPage = 0;
        }

        // display question according to page number
        textDisplay.text = textPageList[_currentPage];

        // disable "previous" button if there is no previous page
        if (_currentPage == 0)
        {
            previousButton.interactable = false;
        }

        // disable "confirm" button if UI currently does not show the last page
        if (_currentPage < textPageList.Length - 1)
        {
            nextButton.interactable = true;
            startButton.interactable = false;
        }

        CheckIfShouldEnableStartButton();

        //Notify Listners
        onPageChange.Invoke(_currentPage);
    }
}