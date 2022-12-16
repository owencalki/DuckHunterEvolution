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

    public UIManager UImanager;



    void Start()
    {
        SelectedWeapon();
    }

    void Update()
    {

        //Pressing F checks for enough money and calls to switch to that gun using SelectedWeapon method
        if(Input.GetKeyDown(KeyCode.F) && UImanager.hoveredGun!=null)
        {
            if (UImanager.UseMoney(UImanager.hoveredGun.cost) == true)
            {
                selectedWeapon = UImanager.hoveredGun.selectedWeaponNum;
                SelectedWeapon();
            }

        }
    }


    //Finds all transforms that are a child to character and sets them to active if they're order number matches the int selectedWeapon
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
            else if(weapon.gameObject.CompareTag("weapon"))//sets all other transforms tagged with weapon to false
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

        //changes crosshair to match the guns given crosshair
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

    public void Shoot()//casts and ray and determines the objects hit. Then calls many things depending on the object.
    {
        RaycastHit[] hit = Physics.SphereCastAll(cam.transform.position, guns[selectedWeapon].bulletSpread, cam.transform.forward, guns[selectedWeapon].range,mask);
        if(hit.Length>0)
        {
            foreach (RaycastHit objecthit in hit)
            {
                if (objecthit.collider.CompareTag("Duck"))
                {
                    objecthit.collider.gameObject.GetComponent<Duck>().Death();
                }

                //PARTICLE EFFECTS ONLY-----------------------------------------------------------------------------
                if (objecthit.collider.CompareTag("Ground"))
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
                }
                //--------------------------------------------------------------------------------------------
            }
        }
    }
}
