using UnityEngine;
using UnityEngine.XR;

public class PositionUI : MonoBehaviour
{
    [Tooltip("Camera Offset")]
    [SerializeField]
    private GameObject cameraOffset; // Camera Offset for Camera Rotation

    [Tooltip("Headset XRNode")] 
    [SerializeField]
    private XRNode xrNode = XRNode.Head; // XRNode for Headset position and rotation

    [Tooltip("UI Distance from User")] 
    [SerializeField]
    private float distanceFromUser = 3.5f; // Distance of the UI from the user

    [Tooltip("UI Height from Ground")] 
    [SerializeField]
    private float heightFromGround = 2.0f; // Height above the ground where the UI should appear

    private Vector3 _headsetPosition;       // XR Headset Position
    private Quaternion _headsetRotation;    // XR Headset Rotation

    private void Start()
    {
        if (XRSettings.isDeviceActive)
        {
            if (!cameraOffset)
            {
                throw new System.Exception("No Camera Offset set to position UI!");
            }

            // Check if Headset Device and its Position/Rotation are retrievable
            if (!IsValidDevice())
            {
                throw new System.Exception("Failed to retrieve Device and its properties!");
            }

            SetUIPositionAndRotation();
        }
    }

    private bool IsValidDevice()
    {
        // Retrieve Headset Device
        InputDevice device = InputDevices.GetDeviceAtXRNode(xrNode);
        if (device == null)
        {
            return false;
        }

        // Retrieve Headset Position
        if (!device.TryGetFeatureValue(CommonUsages.devicePosition, out _headsetPosition))
        {
            return false;
        }

        // Retrieve Headset Rotation
        if (!device.TryGetFeatureValue(CommonUsages.deviceRotation, out _headsetRotation))
        {
            return false;
        }

        return true;
    }

    private void SetUIPositionAndRotation()
    {
        // Calculate total UI Rotation based on Camera Offset + XR Rotation
        Quaternion totalRotation = Quaternion.Euler(cameraOffset.transform.eulerAngles + _headsetRotation.eulerAngles);

        // Calculate final UI Position based on total Rotation and Distance from User and Ground
        Vector3 targetPosition = _headsetPosition + (totalRotation * Vector3.forward * distanceFromUser);
        targetPosition.y = heightFromGround;

        // Set the calculated UI Position
        transform.position = targetPosition;

        // Set the calculated UI Rotation (horizontal only)
        transform.rotation = Quaternion.Euler(0.0f, totalRotation.eulerAngles.y, 0.0f);
    }
}
