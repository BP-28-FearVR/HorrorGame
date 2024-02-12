using System;
using UnityEngine;

public class StartIntroductionHideChildren : MonoBehaviour
{

    [Tooltip("Page on which the children will be shown.")]
    [SerializeField] private int actionPage;


    //Hide Children on start
    public void Start()
    {
        SetActiveChildren(false);
    }

    //Show children based on Page change
    public void OnPageChangeEvent(int newPage)
    {
        SetActiveChildren(newPage >= actionPage);
    }

    //Sets the active state of the Children to the value of 'shouldBeActive'
    private void SetActiveChildren(Boolean shouldBeActive)
    {
        //Assuming parent is the parent game object
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            child.SetActive(shouldBeActive);
        }
    }
}
