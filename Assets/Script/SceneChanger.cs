using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

// This class handles switching between Scenes with a fade-out and fade-in effect
public class SceneChanger : MonoBehaviour
{
    [Tooltip("The Animator that is used for the scene transition.")]
    [SerializeField] private Animator animator;

    // Name of the scene to Load.
    private SceneObject _sceneToLoad;

    // The function to be called in order to initiate a Scene Change (The parameter's datatype makes sure no invalid input except null is passed)
    public void FadeToScene(SceneObject sceneObject)
    {
        if (sceneObject == null) throw new System.Exception("No SceneAsset was passed to SceneChanger");
        _sceneToLoad = sceneObject;
        animator.SetTrigger("FadeOut");
    }

    // Handles the actual scene Transion
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
