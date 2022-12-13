using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Gun",menuName ="Gun")]
public class GunCreator : ScriptableObject
{
    public new string name;
    public int ammoCount;
    public int selectedWeaponNum;
    public float bulletSpread;
    public float range;
    public float damage;
    public Sprite crosshairSprite;
    public Vector3 crosshairScale;

}
