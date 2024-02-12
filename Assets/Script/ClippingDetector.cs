using UnityEngine;

// This class handles collision of it's GameObject with other Objects, fading the screen to Black if the other GameObject has the specified Layer
// Both this GameObject need a Collider, while this GameObject needs the Collider to be a Trigger and the other to not be a Trigger.
// This GameObject also need a RigidBody
public class ClippingDetector : MonoBehaviour
{
    private static string defaultLayerName = "Default";

    [Tooltip("The Animator to use for animating the Black Screen")]
    [SerializeField] private Animator animator;

    [Tooltip("The Name of the Layer that when near it the Screen should fade to black")]
    [SerializeField] private string forbiddenLayerName = defaultLayerName;

    // The amount of Objects with the specified Layer this GameObject is clipping through
    private int _amountClippedThrough = 0;

    // Layer comparison is done in int, calculation result of the layer conversion to int is saved
    private int _forbiddenLayerIndex;

    // Is called when this GameObject is activated for the first time
    private void Start()
    {
        _forbiddenLayerIndex = LayerMask.NameToLayer(forbiddenLayerName);
        // Does a layer with that name exist (-1 means an non-existant layer)
        if (_forbiddenLayerIndex == -1)
        {
            throw new System.Exception("Unregistered Layer used in ClippingDetector");
        }
    }

    //Is called when a non-Trigger Collider enters this GameObjects Trigger-Collider
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _forbiddenLayerIndex)
        {
            //If the screen isn't black because of this ClippingDetector, fade it to black
            if(_amountClippedThrough == 0)
            {
                animator.SetBool("ClippingObject", true);
            }
            _amountClippedThrough++;
        }
    }

    //Is called when a non-Trigger Collider leaves this GameObjects Trigger-Collider
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _forbiddenLayerIndex)
        {
            _amountClippedThrough--;
            // If there are no more Objects with the specified Layer colliding with our GameObjects Collider, fade back in
            if (_amountClippedThrough == 0)
            {
                animator.SetBool("ClippingObject", false);
            }
        }
    }
}
