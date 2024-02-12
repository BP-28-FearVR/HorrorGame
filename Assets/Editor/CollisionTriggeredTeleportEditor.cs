using UnityEditor;
using UnityEngine;

// Changes the Inspector Editor of the CollisionTriggeredTeleport Class
[CustomEditor(typeof(CollisionTriggeredTeleport))]
public class CollisionTriggeredTeleportEditor : Editor
{
    SerializedProperty detectUsing;

    SerializedProperty collidingTag;

    SerializedProperty collidingLayer;

    SerializedProperty transformParent;

    SerializedProperty teleportType;

    SerializedProperty teleportVector;

    SerializedProperty teleportTransformDestination;

    // OnEnable is called when the GameObject is loaded
    private void OnEnable()
    {
        // Find serialized Properties and store them
        detectUsing = serializedObject.FindProperty("detectUsing");
        collidingTag = serializedObject.FindProperty("collidingTag");
        collidingLayer = serializedObject.FindProperty("collidingLayer");
        transformParent = serializedObject.FindProperty("transformParent");
        teleportType = serializedObject.FindProperty("teleportType");
        teleportVector = serializedObject.FindProperty("teleportVector");
        teleportTransformDestination = serializedObject.FindProperty("teleportTransformDestination");
    }

    // OnInspectorGUI specifies the way the Inspector Editor should be drawn
    public override void OnInspectorGUI()
    {
        // Get the current instance of CollisionTriggeredTeleport (target only exists in this context)
        CollisionTriggeredTeleport collisionTriggeredTeleport = (CollisionTriggeredTeleport)target;

        serializedObject.Update();

        // Generate the non-editable script reference a normal script component has
        using (new EditorGUI.DisabledScope(true)) EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

        // Generate the following fields in the Inspector Editor
        EditorGUILayout.PropertyField(detectUsing);

        // Show either Tag or Layer depending on the choosen Detection type
        if (collisionTriggeredTeleport.detectUsing == CollisionTriggeredTeleport.DetectUsing.Tag)
        {
            EditorGUILayout.PropertyField(collidingTag);
        }
        else
        {
            EditorGUILayout.PropertyField(collidingLayer);
        }
        EditorGUILayout.PropertyField(transformParent);
        EditorGUILayout.PropertyField(teleportType);
        // If the Teleport Mode needs the Teleport Vector, display it
        if (collisionTriggeredTeleport.teleportType == CollisionTriggeredTeleport.TeleportMode.Relative ||
            collisionTriggeredTeleport.teleportType == CollisionTriggeredTeleport.TeleportMode.Absolute)
        {
            EditorGUILayout.PropertyField(teleportVector);
        }
        // If the Teleport Mode is ToGameObject, display a field to insert the GameObject into
        else if(collisionTriggeredTeleport.teleportType == CollisionTriggeredTeleport.TeleportMode.ToGameObject)
        {
            EditorGUILayout.PropertyField(teleportTransformDestination);
        }
        // Teleport Mode ResetToParent need no further field 

        serializedObject.ApplyModifiedProperties();
    }
}