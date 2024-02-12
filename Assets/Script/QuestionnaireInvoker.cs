using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireInvoker : MonoBehaviour
{
    // Time to wait after scene has started
    [SerializeField] private float secondsToWait = 20.0f;

    // Add the questionnaire object to the according field of the questionnaire trigger object in the unity editor
    [SerializeField] private GameObject questionnaire;
    // Add the relaxation UI object to the according field of the questionnaire trigger object in the unity editor
    [SerializeField] private GameObject relaxUI;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TriggerQuestionnaire", secondsToWait);
    }

    // this method deactivates the relaxation UI and enables the questionnaire
    public void TriggerQuestionnaire()
    {
        relaxUI.SetActive(false);
        questionnaire.SetActive(true);
    }
}
