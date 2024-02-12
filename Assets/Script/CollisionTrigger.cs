using System.Linq;
using UnityEngine;

// Base Class for Collision based Reactions
public abstract class CollisionTrigger : MonoBehaviour
{
    public enum DetectUsing
    {
        Tag, Layer
    }

    protected static string defaultTagName = "Untagged";
    protected static string defaultLayerName = "Default";

    // Variable used in Assets/Editor/CollisionTriggeredTeleportEditor
    [Tooltip("How to detect the colliding Object")]
    public DetectUsing detectUsing = DetectUsing.Tag;

    [Tooltip("The tag collision has to be detected for")]
    [SerializeField] protected string collidingTag = defaultTagName;

    [Tooltip("The layer collision has to be detected for")]
    [SerializeField] protected string collidingLayer = defaultLayerName;

    // Layer comparison is done in int, calculation result of the layer conversion to int is saved
    protected int _collidingLayerInt = -1;

    // Checks if the tag or layer (depending on the choosen detection type) exists
    protected void CheckInput()
    {
        if (detectUsing == DetectUsing.Tag)
        {
#if UNITY_EDITOR
            // Does the Unity Editor have a tag with that name registered?
            if (!UnityEditorInternal.InternalEditorUtility.tags.Contains<string>(collidingTag))
            {
                throw new System.Exception("Unregistered Tag used in Trigger Area GameObject");
            }
#endif
        }
        else
        {
            _collidingLayerInt = LayerMask.NameToLayer(collidingLayer);
            // Does a layer with that name exist (-1 means an non-existant layer)
            if (_collidingLayerInt == -1)
            {
                throw new System.Exception("Unregistered Layer used in Trigger Area GameObject");
            }
        }
    }
}
