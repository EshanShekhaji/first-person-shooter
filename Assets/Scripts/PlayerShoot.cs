using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    public PlayerMovement playerMovement;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
            playerMovement.isShooting = true;
        } else { playerMovement.isShooting = false; }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadInput?.Invoke();
        }
    }
}
