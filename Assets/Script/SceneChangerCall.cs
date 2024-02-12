using UnityEngine;

// Used to attach to an empty GameObject in order to call the Scene Change Manager indirectly (using this script)
public class SceneChangerCall : MonoBehaviour
{
    [Tooltip("The Scene Changer that is used for the Scene transition.")]
    [SerializeField] private SceneChanger sceneChanger;

    // Stores the TargetScene Name
    [Tooltip("The target scene to switch to.")]
    [SerializeField] private SceneObject targetScene;

    private void Start()
    {
        if(sceneChanger == null) throw new System.Exception("No SceneChanger was passed to SceneChangerCall");
    }

    // Calls the stored Scene Changer in order to switch the scene
    public void CallSceneChange()
    {
        sceneChanger.FadeToScene(targetScene);
    }
}
