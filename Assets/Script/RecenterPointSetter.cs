using UnityEngine;

public class RecenterPointSetter : MonoBehaviour
{
    [Tooltip("The GameObject to pass to a RecenterScript instance")]
    [SerializeField] private GameObject recenterPoint;

    [Tooltip("The RecenterScript instance to pass the new RecenterPoint to")]
    [SerializeField] private RecenterScript recenterScript;

    // Start is called before the first frame update
    private void Start()
    {
        if (recenterScript == null) throw new System.Exception("No RecenterScript was passed to RecenterPointSetter");
    }

    // Calls the RecenterScript to set a (new) RecenterPoint
    public void SetRecenterPoint()
    {
        recenterScript.SetOwnRecenterPoint(recenterPoint);
    }
}
