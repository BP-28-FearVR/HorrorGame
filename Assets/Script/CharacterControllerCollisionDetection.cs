using UnityEngine;

public class CharacterControllerColissionDetection : MonoBehaviour
{
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        //Gets CharacterController
        _characterController = GetComponent<CharacterController>();

        
        if ( _characterController != null)
        {
            //Set layerOverridePriority
            if (_characterController.layerOverridePriority < 1)
            {
                _characterController.layerOverridePriority = 1;
            }

            LayerMask mustIncludeLayers = LayerMask.GetMask(new string[] { "Floor", "Trigger" });

            //Bitwise logic to combine existing exclude/include
            _characterController.includeLayers = _characterController.includeLayers | mustIncludeLayers;
            _characterController.excludeLayers = _characterController.excludeLayers & (~mustIncludeLayers);
        }
        
        
    }
}
