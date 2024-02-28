using System;
using UnityEngine;

public class StartIntroductionHideChildren : MonoBehaviour
{

    [Tooltip("Page on which the children will be shown.")]
    [SerializeField] private int actionPage;

    //Have Itemes been once active
    private bool _hasBeenActivated;


    //Hide Children on start
    public void Start()
    {
        _hasBeenActivated = false;
        SetActiveChildren(false);
    }

    //Show children based on Page change
    public void OnPageChangeEvent(int newPage)
    {
        bool shouldBeActive = _hasBeenActivated || newPage >= actionPage;
        SetActiveChildren(shouldBeActive);
        _hasBeenActivated = shouldBeActive;
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
