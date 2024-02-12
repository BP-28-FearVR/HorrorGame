using UnityEngine;
using UnityEngine.Events;

// Class that triggers an Event when the specified collision occurs
public class CollisionTriggeredEvent : CollisionTrigger
{
    [Tooltip("The event to call if the specified collision occurs")]
    [SerializeField] private UnityEvent eventToCall;
    // Start is called before the first frame update, which calls CollisionTrigger.CheckInput
    void Start()
    {
        base.CheckInput();
    }

    // OnTriggerEnter is called every time this GameObject's collider detects a collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Detect if the other GameObject has the correct Tag or Layer (depending on the choosen detection type)
        if (detectUsing == DetectUsing.Tag)
        {
            if (!other.gameObject.CompareTag(collidingTag)) return;
        }
        else
        {
            if (other.gameObject.layer != _collidingLayerInt) return;
        }
        eventToCall.Invoke();
    }
}
