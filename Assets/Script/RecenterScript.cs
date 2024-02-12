using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class RecenterScript : MonoBehaviour
{

    [Tooltip("The InputActionProperty to listen for Input. It needs an interaction in its Action Properties.")]
    [SerializeField] private InputActionProperty actionButton;

    [Tooltip("The GameObject this XR Origin is going to recenter to using its position and rotation. If it contains a child with a Renderer, the Renderer will be shown as long as the Recenter Button is pressed")]
    [SerializeField] private GameObject recenterPoint;

    // This XR Origin to recenter
    private XROrigin _xrOrigin;

    // The Character Controller of this XR Origin (the height is needed to offset the y-position during repositioning)
    private CharacterController _characterController;

    // Optional: A Renderer of the RecenterPoint (or of a child of the RecenterPoint) to show as long as the Recenter Button is pressed
    private Renderer _recenterPointRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if(actionButton == null) throw new System.Exception("No InputActionProperty was passed to RecenterScript");
        if(recenterPoint == null) throw new System.Exception("No RecenterPoint GameObject was passed to RecenterScript");

        _xrOrigin = GetComponent<XROrigin>();
        _characterController = GetComponent<CharacterController>();
        GetRecenterPointRenderer();
    }


    // Registers a callback that calls the Recenter function.
    private void OnEnable()
    {
        actionButton.action.performed += Recenter;
    }

    // Unsubscribes the callback
    private void OnDisable()
    {
        actionButton.action.performed -= Recenter;
    }

    // Update is called every frame
    private void Update()
    {
        // If a Renderer was found, enable or disable it depending on the Recenter Button having been pressed or released
        if (_recenterPointRenderer != null)
        {
            if (actionButton.action.WasPressedThisFrame())
            {
                _recenterPointRenderer.enabled = true;
            }
            else if (actionButton.action.WasReleasedThisFrame())
            {
                _recenterPointRenderer.enabled = false;
            }
        }
    }

    // public method to set (a new) RecenterPoint
    public void SetOwnRecenterPoint(GameObject newRecenterPoint)
    {
        if (recenterPoint == null) throw new System.Exception("No new RecenterPoint GameObject was passed to RecenterScript");
        recenterPoint = newRecenterPoint;
        GetRecenterPointRenderer();
    }

    // Get the Renderer of the current RecenterPoint
    private void GetRecenterPointRenderer()
    {
        _recenterPointRenderer = recenterPoint.GetComponentInChildren<Renderer>(true);
    }

    // Recenter the XR Origin. Needs the not explicitly used variable ctx in order to successfully unregister the callback later
    private void Recenter(InputAction.CallbackContext ctx)
    {
        // Move Camera to RecenterPoint + the height of the CharacterController so the bottom of the XR Origin is moved to the RecenterPoint
        _xrOrigin.MoveCameraToWorldLocation(recenterPoint.transform.position + new Vector3(0, _characterController.height, 0));
        // Look the direction the RecenterPoint is looking at
        _xrOrigin.MatchOriginUpCameraForward(recenterPoint.transform.up, recenterPoint.transform.forward);
    }
}
