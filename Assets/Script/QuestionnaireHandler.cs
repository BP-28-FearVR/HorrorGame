using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireHandler : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button confirmButton;
    private int _currentPage;

    // creates an accessible component in the editor, which allows to add as many questions to the questionaire as one would like
    // press "+" in the according component in the editor to add a new field for text input for each question
    [TextArea(15, 20)]
    [SerializeField] private string[] questionList;     

    // text field of the UI element, which is used to show the currently selected question
    [SerializeField] private TextMeshProUGUI shownQuestion;

    // radio buttons
    [SerializeField] private Toggle[] options;

    // _choices[i] saves the index of which radio button is selected on page i, if none is selected _choices[i] contains -1
    private QuestionnaireData _choiceOnPage = null;
    private bool _isEveryQuestionAnswered = false;

    // check if there has been entered a question via the input field in the editor
    // if so, show the first question on the questionnaire UI, else initialize the question array with an empty string
    // UI always starts displaying page 0
    void Start()
    {
        // set up first page
        _currentPage = 0;
        if (questionList.Length <= 0)
        {
            questionList = new string[1];
            questionList[0] = "";
        }
        shownQuestion.text = questionList[0];

        // set up buttons
        previousButton.interactable = false;
        confirmButton.interactable = false;
        if (questionList.Length <= 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }

        // initialize radio button choices: at first for each question all radio buttons are unselected (= -1)
        _choiceOnPage = new QuestionnaireData(questionList);
    }

    // if last page is reached and a choice has been made on every page, the confirm button will be enabled
    public void EnableConfirm()
    {
        if (_currentPage == questionList.Length - 1)
        {
            SaveChoices();
            if (_isEveryQuestionAnswered)
            {
                confirmButton.interactable = true;
            }
            else
            {
                confirmButton.interactable = false;
            }
        }
    }

    // if the "next" button is clicked, the UI will show the next question in line
    public void ShowNextPage()
    {
        // increase page number
        _currentPage++;

        // if page number is out of bounds, reset to max page number
        if (_currentPage >= questionList.Length)
        {
            _currentPage = questionList.Length - 1;
        }

        // display question according to page number
        shownQuestion.text = questionList[_currentPage];
        
        previousButton.interactable = true;

        // disable "next" button if UI currently shows the last page
        if (_currentPage == questionList.Length - 1)
        {
            nextButton.interactable = false;
        }
    }

    // if the "previous" button is clicked, the UI will show the previous question
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
        shownQuestion.text = questionList[_currentPage];

        // disable "previous" button if there is no previous page
        if (_currentPage == 0)
        {
            previousButton.interactable = false;
        }

        // disable "confirm" button if UI currently does not show the last page
        if (_currentPage < questionList.Length - 1)
        {
            nextButton.interactable = true;
            confirmButton.interactable = false;
        }
    }

    // saves the index of the selected radio button of the current page (has to be called right before switching a page)
    public void SaveChoices ()
    {
        // set base value - will be overwritten if a choice was made:
        _choiceOnPage[_currentPage] = -1;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].isOn)
            {
                _choiceOnPage[_currentPage] = i;
                break;
            }
        }

        _isEveryQuestionAnswered = true;
        for (int i = 0; i < questionList.Length; i++)
        {
            if (_choiceOnPage[i] <= -1)
            {
                _isEveryQuestionAnswered = false;
                break;
            }
        }

        _choiceOnPage.SaveToFile();
    }

    // turns on the radio button previously selected on the next page (has to be called right after switching the page)
    public void LoadChoicesOfNextPage()
    {
        // turn off selected radio button of old page (only if there has been made a choice on the old page)
        if (_choiceOnPage[_currentPage] <= -1)
        {
            if (_choiceOnPage[_currentPage - 1] >= 0)
            {
                options[_choiceOnPage[_currentPage - 1]].isOn = false;
            }
        }
        // turn on selected radio button of new page (only if there has been made a choice on this page before)
        else
        {
                options[_choiceOnPage[_currentPage]].isOn = true;
        }
    }

    // turns on the radio button previously selected on the previous page (has to be called right after switching the page)
    public void LoadChoicesOfPreviousPage()
    {
        // turn off selected radio button of old page (only if there has been made a choice on the old page)
        if (_choiceOnPage[_currentPage] <= -1)
        {
            if (_choiceOnPage[_currentPage + 1] >= 0)
            {
                options[_choiceOnPage[_currentPage + 1]].isOn = false;
            }
        }
        // turn on selected radio button of new page (only if there has been made a choice on this page before)
        else
        {
            options[_choiceOnPage[_currentPage]].isOn = true;
        }
    }
}