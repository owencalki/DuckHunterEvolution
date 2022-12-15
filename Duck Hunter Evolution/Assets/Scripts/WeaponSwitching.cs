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

    public ParticleSystem tree;
    public ParticleSystem ground;
    public ParticleSystem rock;
    public ParticleSystem water;



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
                if(objecthit.collider.CompareTag("Duck"))
                objecthit.collider.gameObject.GetComponent<Duck>().Death();

                if(objecthit.collider.CompareTag("Ground"))
                {
                    Vector3 norm = objecthit.transform.forward;
                    Instantiate(ground, objecthit.point, Quaternion.LookRotation(Vector3.forward,norm));
                }

                if (objecthit.collider.CompareTag("Tree"))
                {
                    Vector3 norm = objecthit.transform.forward;
                    Instantiate(tree, objecthit.point, Quaternion.LookRotation(Vector3.forward, norm));

                }

                if (objecthit.collider.CompareTag("Rock"))
                {
                    Vector3 norm = objecthit.transform.forward;
                    Instantiate(rock, objecthit.point, Quaternion.LookRotation(Vector3.forward, norm));

                }

                if (objecthit.collider.CompareTag("Water"))
                {
                    Vector3 norm = objecthit.transform.forward;
                    Instantiate(water, objecthit.point, Quaternion.LookRotation(Vector3.forward, norm));
                    Debug.Log("water");

                }
            }
        }
    }
}
