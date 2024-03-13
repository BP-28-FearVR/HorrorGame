using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Interactions;

public class GameDebugHelper : MonoBehaviour
{
    //Konami-Code: UP-UP-DOWN-DOWN-LEFT-RIGHT-LEFT-RIGHT-B-A

    [Tooltip("The event to call when debugMode is enabled")]
    [SerializeField] private UnityEvent onDebugModeEnabled;

    [Tooltip("The event to call when debugMode is disabled")]
    [SerializeField] private UnityEvent onDebugModeDisabled;

    [Tooltip("The event to call when the DevMenu should be activated")]
    [SerializeField] private UnityEvent onToggleDevMenu;

    [Tooltip("The event to call when the Console should be activated")]
    [SerializeField] private UnityEvent onToggleConsole;

    [Tooltip("Time in seconds until the next 'state'/'button'/'event'(up,down,left,right,b,a) **has** to occur.")]
    [SerializeField] private double timeBetweenPresses = 1; // 1 Sec

    [Tooltip("Input Action for the A button.")]
    [SerializeField] private InputActionReference aButton;

    [Tooltip("Input Action for the B button.")]
    [SerializeField] private InputActionReference bButton;

    [Tooltip("Input Action for the Up.")]
    [SerializeField] private InputActionReference up;

    [Tooltip("Input Action for the Down.")]
    [SerializeField] private InputActionReference down;

    [Tooltip("Input Action for the Left.")]
    [SerializeField] private InputActionReference left;

    [Tooltip("Input Action for the Right.")]
    [SerializeField] private InputActionReference right;

    [Tooltip("Input Action for the Y button.")]
    [SerializeField] private InputActionReference yButton;

    [Tooltip("Input Action for the X button.")]
    [SerializeField] private InputActionReference xButton;

    // The time at which the last "event" occured
    private static double _lastActionTime = 0;
    // The current state of the Input
    private static GameDebugHelperStates _state = GameDebugHelperStates.Nothing;


    // Sets initial state and added event Handlers.
    void Start()
    {
        
        aButton.action.performed += OnA;
        bButton.action.performed += OnB;
        yButton.action.performed += OnY;
        xButton.action.performed += OnX;

        up.action.performed += OnUp;
        down.action.performed += OnDown;
        left.action.performed += OnLeft;
        right.action.performed += OnRight;

        if (_state != GameDebugHelperStates.DebugMode)
        {
            _state = GameDebugHelperStates.Nothing;
        } else
        {
            onDebugModeEnabled.Invoke();
        }
        _lastActionTime = 0;
    }

    // Get that is True if DebugMode is Enbaled
    public static bool IsDebugModeEnabled()
    {
        return _state == GameDebugHelperStates.DebugMode;
    }

    // Disables debug mode
    public void DisableDebugMode() {
        _state = GameDebugHelperStates.Nothing;
        onDebugModeDisabled.Invoke();
    }

    // Handles Moving Joystick to Up
    private void OnUp(InputAction.CallbackContext context)
    {
        if (GameDebugHelper._state == GameDebugHelperStates.DebugMode || context.interaction == null || !(context.interaction is SectorInteraction)) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.Nothing,_):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput1;
                break;
            case (GameDebugHelperStates.CorrectInput1,true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput2;
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }

        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles Moving Joystick to Down
    private void OnDown(InputAction.CallbackContext context)
    {
        if (GameDebugHelper._state == GameDebugHelperStates.DebugMode || context.interaction == null || !(context.interaction is SectorInteraction)) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.CorrectInput2, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput3;
                break;
            case (GameDebugHelperStates.CorrectInput3, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput4;
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }

        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles Moving Joystick to the Left
    private void OnLeft(InputAction.CallbackContext context)
    {
        if (GameDebugHelper._state == GameDebugHelperStates.DebugMode || context.interaction == null || !(context.interaction is SectorInteraction)) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.CorrectInput4, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput5;
                break;
            case (GameDebugHelperStates.CorrectInput6, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput7;
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }

        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles Moving Joystick to the Right
    private void OnRight(InputAction.CallbackContext context)
    {
        if (GameDebugHelper._state == GameDebugHelperStates.DebugMode || context.interaction == null || !(context.interaction is SectorInteraction)) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.CorrectInput5, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput6;
                break;
            case (GameDebugHelperStates.CorrectInput7, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput8;
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }

        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles A Button
    private void OnA(InputAction.CallbackContext context)
    {
        if (_state == GameDebugHelperStates.DebugMode) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.CorrectInput9, true):
                GameDebugHelper._state = GameDebugHelperStates.DebugMode;
                onDebugModeEnabled.Invoke();
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }

        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles B Button
    private void OnB(InputAction.CallbackContext context)
    {
        if (_state == GameDebugHelperStates.DebugMode) return;

        switch ((GameDebugHelper._state, _lastActionTime - context.time <= timeBetweenPresses))
        {
            case (GameDebugHelperStates.CorrectInput8, true):
                GameDebugHelper._state = GameDebugHelperStates.CorrectInput9;
                break;
            default:
                GameDebugHelper._state = GameDebugHelperStates.Nothing;
                break;
        }
        GameDebugHelper._lastActionTime = context.time;
    }

    // Handles Y Button
    private void OnY(InputAction.CallbackContext context)
    {
        if (_state == GameDebugHelperStates.DebugMode)
        {
            onToggleDevMenu.Invoke();
        }
    }

    // Handles X Button
    private void OnX(InputAction.CallbackContext context)
    {
        if (_state == GameDebugHelperStates.DebugMode)
        {
            onToggleConsole.Invoke();
        }
    }
}

enum GameDebugHelperStates
{
    Nothing = -1, // Initial State 

    // States when part of the input-seqence was correct.
    CorrectInput1 = 0, //UP 
    CorrectInput2 = 1, //UP-UP
    CorrectInput3 = 2, //UP-UP-DOWN
    CorrectInput4 = 3, //UP-UP-DOWN-DOWN
    CorrectInput5 = 4, //UP-UP-DOWN-DOWN-LEFT
    CorrectInput6 = 5, //UP-UP-DOWN-DOWN-LEFT-RIGHT
    CorrectInput7 = 6, //UP-UP-DOWN-DOWN-LEFT-RIGHT-LEFT
    CorrectInput8 = 7, //UP-UP-DOWN-DOWN-LEFT-RIGHT-LEFT-RIGHT
    CorrectInput9 = 8, //UP-UP-DOWN-DOWN-LEFT-RIGHT-LEFT-RIGHT-B

    // State when full input was correct and DebugMode is Enabled.
    DebugMode = 9      //UP-UP-DOWN-DOWN-LEFT-RIGHT-LEFT-RIGHT-B-A
}