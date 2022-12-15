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
    public Rigidbody rb;


    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void FixedUpdate()
    {
        //WASD Movement ---------------------------------------------------------------------------------------------------------------
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
        //--------------------------------------------------------------------------------------------------------------------------------


        //Looking Around ---------------------------------------------------------------------------------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot += mouseY;
        yRot += mouseX;
        xRot = Mathf.Clamp(xRot, -85, 85);

        character.transform.localRotation = Quaternion.Euler(-xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
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
