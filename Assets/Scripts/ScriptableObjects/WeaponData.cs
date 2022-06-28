using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
public class WeaponData : ScriptableObject
{
    public new string name;
    
    public float damage;
    public float maxDistance;
    
    public int currentAmmo;
    public int magSize;
    public float rateOfFire;
    public float reloadTime;
    public bool isReloading;
}
