using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public GameObject character;
    public ParticleSystem flash;
    public int Ammo;
    Transform weapon;
    public List<GunCreator> guns;
    public GameObject cam;
    public LayerMask mask;
    public Image crosshair;


    void Start()
    {
        SelectedWeapon();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedWeapon++;
            SelectedWeapon();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedWeapon--;
            SelectedWeapon();

        }
    }

    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform weapon in character.transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                gameObject.GetComponentInParent<PlayerController>().animator.Play("Idle_" + weapon.name);
                flash = weapon.GetComponentInChildren<ParticleSystem>();
                Ammo = guns[selectedWeapon].ammoCount;
            }
            else if(weapon.gameObject.CompareTag("weapon"))
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

        crosshair.sprite = guns[selectedWeapon].crosshairSprite;
        crosshair.transform.localScale = guns[selectedWeapon].crosshairScale;

    }

    public void MuzzleFlash()
    {
        flash.Play();

    }

    public void TakeAmmo(int amount)
    {
        Ammo = Ammo - amount;
    }

    public void Reload ()
    {
        Ammo = guns[selectedWeapon].ammoCount;
        gameObject.GetComponent<PlayerController>().animator.SetBool("hasAmmo",true);
    }

    public void Shoot()
    {
        RaycastHit[] hit = Physics.SphereCastAll(cam.transform.position, guns[selectedWeapon].bulletSpread, cam.transform.forward, guns[selectedWeapon].range,mask);
        if(hit.Length>0)
        {
            foreach (RaycastHit objecthit in hit)
            {
                Debug.Log(objecthit.collider.name);
                objecthit.collider.gameObject.GetComponent<Hit>().Shot(guns[selectedWeapon].damage);
            }
        }
    }
}
