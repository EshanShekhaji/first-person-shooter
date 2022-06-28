using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform cam;
    public PlayerMovement playerMovement;
    public GameObject bulletImpact;
    float timeSinceLastShot;
    bool CanShoot()
    {
        return !weaponData.isReloading && timeSinceLastShot > 1f / (weaponData.rateOfFire / 60f);
    }
    
    
    
    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }
    
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }
    
    
    
    
    public void Shoot()
    {
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                playerMovement.isShooting = true;
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, weaponData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                }
                
                GameObject.Instantiate(bulletImpact, hitInfo.point, Quaternion.identity);
                
                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
                onGunShot();
            }
        }
    }
    
    public void StartReload()
    {
        if(!weaponData.isReloading && weaponData.currentAmmo != weaponData.magSize)
        {
            StartCoroutine(Reload());
        }
    }
    
    public IEnumerator Reload()
    {
        weaponData.isReloading = true;
        
        yield return new WaitForSeconds(weaponData.reloadTime);
        weaponData.currentAmmo = weaponData.magSize;
        weaponData.isReloading = false;
    }
    
    
    public void onGunShot()
    {
        
    }
}
