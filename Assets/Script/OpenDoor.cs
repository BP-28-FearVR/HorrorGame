using System.Collections;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Tooltip("The door to open")]
    [SerializeField] private GameObject door;

    [Tooltip("Opening Door Speed")]
    [SerializeField] private float openSpeed = 0.01f;

    [Tooltip("Final Door Rotation")]
    [SerializeField] private float openRotation = 84.0f;

    [Tooltip("The time in seconds after which the door can be opened")]
    [SerializeField] private float timeUntilDoorOpens = 0;

    [Tooltip("The time in seconds that Outline stays on/off. (<= 0 to disable blinking)")]
    [SerializeField] private float blinkToggleInterval = 2;

    [Tooltip("The trigger area for opening this door")]
    [SerializeField] private GameObject doorTrigger;

    [Tooltip("The outline object for the outline animation.")]
    [SerializeField] private Outline outline;

    [Tooltip("The UI the remaining time should be printed to")]
    [SerializeField] private TextMeshProUGUI timerTextUI;

    private bool _isOutlineOn = false;
    private bool _isDoorMoving = false;
    private bool _isDoorOpened = false;

    private float _remainingTime;

    void Start()
    {
        // check if timeUntilDoorOpens is a valid time for this use case
        if (timeUntilDoorOpens < 0) 
        { 
            timeUntilDoorOpens = 0;
        } else if(timeUntilDoorOpens > 5999)
        {
            //99 minutes and 59 seconds convert to 5999 seconds
            timeUntilDoorOpens = 5999;
        }
        if(timerTextUI != null)
        {
            _remainingTime = timeUntilDoorOpens;
            printRemainingTime();
        }
    }

    private void countDownTime()
    {
        // Decrease time left by 1 second as this function is called every second
        _remainingTime -= 1f;
        if (_remainingTime >= 0)
        {
            printRemainingTime();
        }
        if (_remainingTime <= 0)
        {
            timerTextUI.color = Color.green;
            CancelInvoke("countDownTime");
        }
    }

    private void printRemainingTime()
    {
        int minutes = Mathf.FloorToInt(_remainingTime / 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);
        timerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Toggles the Outline on and off.
    private void ToggleOutline()
    {
        if (outline != null)
        {
            if (_isOutlineOn)
            {
                outline.TurnOutlineOff();
            }
            else
            {
                outline.TurnOutlineOn();
            }
        }

        _isOutlineOn = !_isOutlineOn;
    }

    // StopBlinkingAndDeactivateOutline
    public void StopBlinkingAndDeactivateOutline()
    {
        CancelInvoke("ToggleOutline");

        if (outline != null)
        {   
            outline.TurnOutlineOff();
        }

        _isOutlineOn = false;
    }

    //Is invoked when the timer started by StartTriggerTimer is finished
    //Activates the Door Trigger and the Blinking Outline
    public void OnTimerDone()
    {
        if (doorTrigger != null)
        {
            doorTrigger.SetActive(true);
        }

        if (outline == null) return;

        if (blinkToggleInterval > 0) {
            InvokeRepeating("ToggleOutline", 0, blinkToggleInterval);
        } else
        {
            outline.TurnOutlineOn();
            _isOutlineOn = true;
        }
    }

    //Starts the timer that calls OnTimerDone
    public void StartTriggerTimer()
    {
        Invoke("OnTimerDone", timeUntilDoorOpens);
        if(timerTextUI != null)
        {
            // calls a function that counts down the time by 1 second every second after 1 second
            InvokeRepeating("countDownTime", 1, 1);
        }
    }

    // For use by the dev menu
    public void setTimeByDevMenu(float newRemainingTime)
    {
        _remainingTime = newRemainingTime;
        CancelInvoke("OnTimerDone");
        Invoke("OnTimerDone", _remainingTime);
    }

    // Opens door if closed and closes door if opened
    public void Interact()
    {
        if (!_isDoorOpened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    // Opens the door if it's not moving or open
    public void Open()
    {
        if (!_isDoorMoving && !_isDoorOpened)
        {
            StartCoroutine(RotateDoor(openRotation));
            _isDoorOpened = true;

            StopBlinkingAndDeactivateOutline();
        }
    }

    // Closes the door if it's not moving or closed
    public void Close()
    {
        if (!_isDoorMoving && _isDoorOpened)
        {
            StartCoroutine(RotateDoor(0.0f));
            _isDoorOpened = false;
        }
    }

    // Rotates the door to a given target angle
    IEnumerator RotateDoor(float target)
    {
        // Start Rotation
        _isDoorMoving = true;
        Quaternion initialRotation = door.transform.rotation;
        float elapsedTime = 0.0f;

        // Calculates the new Rotation for each tick until the animation time has elapsed and reached the target angle
        while (elapsedTime < 1.0f)
        {
            door.transform.rotation = Quaternion.Lerp(initialRotation, Quaternion.Euler(-90, 0, target), elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        // Apply final rotation
        door.transform.rotation = Quaternion.Euler(-90, 0, target);
        _isDoorMoving = false;
    }
}
