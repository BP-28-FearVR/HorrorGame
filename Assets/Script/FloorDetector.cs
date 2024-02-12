using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [Tooltip("The layer which is detected by the Spherecast.")]
    [SerializeField] private LayerMask layerMask;

    [Tooltip("This object will disappear if the player leaves the layer which is set in layer mask")]
    [SerializeField] private GameObject invisibleFloor;

    [Tooltip("The radius of the Spherecast")]
    [SerializeField] private float radius = 1.5f;

    [Tooltip("The maximal Distance of the Spherecast")]
    [SerializeField] private float maxDistance = 3.0f;

    [Tooltip("The direction for the sphereCast (towards the floor).")]
    [SerializeField] private Vector3 sphereCastDirection = new Vector3(0, -1, 0);


    // If the invisibleFloor is set and active, test each fixedUpdate if Player above layerMask-Layer if not disable invisibleFloor.
    void FixedUpdate()
    {
        if (invisibleFloor != null && invisibleFloor.activeSelf)
        {
            bool isSpherecastColliding = Physics.SphereCast(transform.position, radius, sphereCastDirection, out _, maxDistance, layerMask);

            if (!isSpherecastColliding)
            {
                invisibleFloor.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
