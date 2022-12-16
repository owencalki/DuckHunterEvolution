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
    public GunCreator hoveredGun;
    public List<GunCreator> purchasedGuns;

    public CanvasGroup descriptionCanvas;
    public TMPro.TextMeshProUGUI descriptionText;
    public TMPro.TextMeshProUGUI pickupText;
    public TMPro.TextMeshProUGUI moneyText;

    GameObject lastSeen;

    public AudioManager audioManager;

    private void Start()
    {
        moneyText.text = "$" + money;
    }


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
                    if (lastSeen != hit.collider.gameObject)
                    {
                        audioManager.Play("Menu Click",0f);
                    }

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
                lastSeen = null;
            }
        }


    }

    public IEnumerator DisplayTitleText(string displayText,float displayTime)
    {
        text.text = displayText;
        startTime = Time.time;

        fadeIn = true;
        yield return new WaitForSeconds(displayTime +fadeTime);
        titleCanvas.alpha = 1;
        fadeIn = false;

        startTime = Time.time;
        fadeOut = true;
        yield return new WaitForSeconds(fadeTime);
        titleCanvas.alpha = 0;
        fadeOut = false;
    }

    string costString;
    public bool purchased;
    void DisplayItemDescription(GunCreator hoveredGun)
    {
        foreach (GunCreator gun in purchasedGuns)
        {
            if (hoveredGun.name == gun.name)
            {
                purchased = true;
                costString = "";
                pickupText.text = "F to Swap";
                break;
            }
            else
            {
                purchased = false;
                costString = "\nCost: $" + hoveredGun.cost;
                pickupText.text = "F to Purchase";
            }
        }



        descriptionText.text = 
            
            "Name: " + hoveredGun.name +
            "\nAmmo: " + hoveredGun.ammoCount + 
            "\nBullet Spread: " + hoveredGun.bulletSpread +
            "\nRange: " + hoveredGun.range +
            costString;



        descriptionCanvas.alpha = 1;
    }
    public int money;
    public void AddMoney(int moneyAdded)
    {
        money += moneyAdded;
        moneyText.text = "$"+money;
    }
    public bool UseMoney(int cost)
    {

        if(purchased==true)
        {
            return true;
        }
        else if(money-cost>0)
        {
            purchasedGuns.Add(hoveredGun);
            money -= cost;
            moneyText.text = "$" + money;
            return true;
        }
        else
        {
            return false;
        }
    }


}
