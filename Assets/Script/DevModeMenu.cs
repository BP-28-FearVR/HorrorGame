using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DevModeMenu : MonoBehaviour
{
    [Tooltip("The DevModeMenu GameObject")]
    [SerializeField] private GameObject devMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(devMenu != null)
        {
            //Check for each Button if it has any listener subscribed to the attached UnityEvent. If not, deactivate the button
            Button[] buttons = devMenu.GetComponentsInChildren<Button>();
            foreach(Button button in buttons)
            {
                if (!hasListenerAttached(button.onClick)) 
                {
                    button.interactable = false;
                }
            }
        }
    }

    // Check if the UnityEvent has a Listener attached to him
    private bool hasListenerAttached(UnityEvent eventToCheck) {
        bool result = false;
        for (int i = 0; i < eventToCheck.GetPersistentEventCount(); i++)
        {
            if (eventToCheck.GetPersistentTarget(i) != null)
            {
                result = true;
            }
        }
        return result;
    }

    // Toggle the state of the DevMenu
    public void ToggleDevMenu()
    {
        devMenu.SetActive(!devMenu.activeSelf);
    }
}
