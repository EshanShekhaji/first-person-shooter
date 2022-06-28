using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float horizontalSensitivity;
    [SerializeField] float verticalSensitivity;
    
    [SerializeField]Transform cam;
    [SerializeField]Transform orientation;
    
    float mouseX;
    float mouseY;
    
    float multiplier = 0.01f;
    
    float xRotation;
    float yRotation;
    
    void Start()
    {
        // cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        GetMouseInput();
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
    void GetMouseInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        
        yRotation += mouseX * horizontalSensitivity * multiplier;
        xRotation -= mouseY * horizontalSensitivity * multiplier;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
