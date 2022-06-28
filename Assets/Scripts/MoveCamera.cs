using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]Transform cameraPosition;
    [SerializeField]float cameraHeight;
    
    void Update()
    {
        transform.position = cameraPosition.position + new Vector3(0, cameraHeight, 0);
    }
}
