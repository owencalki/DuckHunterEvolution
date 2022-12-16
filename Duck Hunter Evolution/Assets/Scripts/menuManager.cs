using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{

    float timeBetweenAnimations = 10f;
    float lastAnimationTime;
    public Animator anim;
    public AudioManager Amanager;
    public GameObject settingsPanel;
    public GameObject buttonPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        if (Time.time - lastAnimationTime > timeBetweenAnimations)
        {
            anim.SetInteger("AnimNum", Random.Range(1, 4));


            lastAnimationTime = Time.time;
        }
        else
        {
            anim.SetInteger("AnimNum", 0);
        }
    }

    public void PlayButton()
    {
        ButtonSound();
        SceneManager.LoadScene(1); 
    }

    public void SettingsButton()
    {
        ButtonSound();
        buttonPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void QuitButton()
    {
        ButtonSound();
        Application.Quit();
    }
    
    public void ButtonSound()
    {
        Amanager.Play("Button Sound",0);
    }

    public void BackButton()
    {
        ButtonSound();
        buttonPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    
}
