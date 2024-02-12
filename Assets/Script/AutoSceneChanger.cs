using UnityEngine;

// Used to call the SceneChanger after a specified time in order to switch to a specified Scene
public class AutoSceneChanger : MonoBehaviour
{
    [Tooltip("The Scene Changer that is used for the Scene transition.")]
    [SerializeField] private SceneChanger sceneChanger;

    [Tooltip("The time (in seconds) to wait for the AutoSceneTransition to start.")] 
    [SerializeField] private float time = 20.0f;

    // Stores the TargetScene Name
    [Tooltip("The target scene to switch to.")]
    [SerializeField] private SceneObject targetScene;

    [Tooltip("The questionnaire that shall be shown to the user before changing te scene.")]
    [SerializeField] private GameObject questionnaire;

    [Tooltip("The instruction UI that has to be deactivated when the questionnaire starts.")]
    [SerializeField] private GameObject instructionsToBeDeactivated;

    // Setter for TargetScene Name
    public void SetTargetScene(SceneObject sceneObject)
    {
        targetScene = sceneObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnTimerDone", time);
    }

    // Is called once the timer finishes, starts the questionnaire if there is one
    // scene change will then be handled by questionnaire, otherwise scene changes directly
    void OnTimerDone()
    {
        if (questionnaire != null)
        {
            if (instructionsToBeDeactivated != null)
            {
                instructionsToBeDeactivated.SetActive(false);
            }
            questionnaire.SetActive(true);
        } 
        else
        {
            sceneChanger.FadeToScene(targetScene);
        }
    }
}
