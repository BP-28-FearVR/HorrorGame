using UnityEngine;

public class TorchHandler : MonoBehaviour
{
    [Tooltip("The Torch Rigidbody that should fall down.")]
    [SerializeField] private Rigidbody torchBody;

    [Tooltip("The Torque-velocity vector, that is applied to the torch. (Max total angular velocity is 7)")]
    [SerializeField] private Vector3 pushVector = new Vector3(7,0,0);

    //Handles throwing of the torch when player is too close.
    public void OnPlayerTooClose()
    {
        if (torchBody != null)
        {
            torchBody.AddRelativeTorque(pushVector, ForceMode.VelocityChange);
        }
        

        this.enabled = false;
        this.gameObject.SetActive(false);
    }
}
