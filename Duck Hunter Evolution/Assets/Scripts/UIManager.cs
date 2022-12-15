using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UIManager : MonoBehaviour
{
    public bool fadeIn = false;
    public bool fadeOut = false;

    public TMPro.TextMeshProUGUI text;
    public CanvasGroup titleCanvas;

    float fadeTime = 2f;
    float startTime;
    public Camera cam;
    public LayerMask mask;
    public List<GunCreator> possibleGuns;
    GunCreator hoveredGun;

    public CanvasGroup descriptionCanvas;
    public TMPro.TextMeshProUGUI descriptionText;
    GameObject lastSeen;




    void Update()
    {
        if(fadeIn==true && Time.time - startTime<fadeTime)
        {
            titleCanvas.alpha = (Time.time - startTime) / fadeTime;
        }
        if (fadeOut == true && Time.time - startTime < fadeTime)
        {
            titleCanvas.alpha = 1 - (Time.time - startTime) / fadeTime;
        }

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, 2f, mask))
        {
            foreach(GunCreator gun in possibleGuns)
            {
                if(gun.name == hit.collider.name)
                {
                    hoveredGun = gun;

                    lastSeen = hit.collider.gameObject;

                    lastSeen.GetComponent<Outline>().OutlineColor = Color.white;
                }
            }
            DisplayItemDescription(hoveredGun);
        }
        else
        {
            descriptionCanvas.alpha = 0;
            if(lastSeen!=null)
            {
                lastSeen.GetComponent<Outline>().OutlineColor = Color.clear;
            }
        }


    }

    public IEnumerator DisplayTitleText(string displayText,float displayTime)
    {
        text.text = displayText;
        startTime = Time.time;

        fadeIn = true;
        yield return new WaitForSeconds(displayTime +fadeTime);
        fadeIn = false;

        startTime = Time.time;
        fadeOut = true;
        yield return new WaitForSeconds(fadeTime);
        fadeOut = false;
    }

    private void OnMouseOver()
    {
        
    }

    void DisplayItemDescription(GunCreator hoveredGun)
    {
        descriptionText.text = 
            
            "Name: " + hoveredGun.name +
            "\nAmmo: " + hoveredGun.ammoCount + 
            "\nBullet Spread: " + hoveredGun.bulletSpread +
            "\nRange: " + hoveredGun.range +
            "\nDamage: " + hoveredGun.damage;



        descriptionCanvas.alpha = 1;
    }
}
