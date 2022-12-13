using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject character;
    public float speed = 10;
    public float sensitivity = 30;
    public float xRot;
    public float yRot;
    public Animator animator;
    public string activeGun;
    AudioManager audioManager;


    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        //WASD Movement ---------------------------------------------------------------------------------------------------------------
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position = transform.position + transform.forward * v * speed * Time.deltaTime + transform.right * h * speed * Time.deltaTime;
        //--------------------------------------------------------------------------------------------------------------------------------


        //Looking Around ---------------------------------------------------------------------------------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot += mouseY;
        yRot += mouseX;
        xRot = Mathf.Clamp(xRot, -60, 70);


        character.transform.localRotation = Quaternion.Euler(-xRot, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        //--------------------------------------------------------------------------------------------------------------------------------


        //Animations ---------------------------------------------------------------------------------------------------------------
        
        if(h!=0 || v!=0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);

        }
        if (gameObject.GetComponent<WeaponSwitching>().Ammo != 0)
        {
            if (Input.GetMouseButton(0) && gameObject.GetComponent<WeaponSwitching>().Ammo > 0)
            {
                animator.SetTrigger("shoot");
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.ResetTrigger("shoot");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("hasAmmo",false);
        }

        if(gameObject.GetComponent<WeaponSwitching>().Ammo < 0)
        {
            animator.SetBool("hasAmmo", false);
        }

        
        //--------------------------------------------------------------------------------------------------------------------------------

    }
    public void PlaySound(string soundName)
    {
        audioManager.Play(soundName, 0);
    }
    
}
