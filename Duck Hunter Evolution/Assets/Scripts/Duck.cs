using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Duck : MonoBehaviour
{
    GameObject pattern;
    List <Transform> targets;
    public float speed = 0.5f;
    public float rotSpeed = 3f;
    bool alive = true;
    Rigidbody[] rbs;
    public ParticleSystem blood;
    public float randomness = 2;
    public UIManager UImanager;
    public AudioManager Amanager;
    bool gameOver = false;


    void Start()
    {
        UImanager = GameObject.Find("GameManager").GetComponent<UIManager>();
        Amanager = GameObject.Find("Player").GetComponent<AudioManager>();


        rbs = gameObject.GetComponents<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }

        speed = speed * Random.Range(0.8f,1.2f);

        pattern = GameObject.FindGameObjectWithTag("Pattern");
        targets = new List<Transform>(pattern.GetComponentsInChildren<Transform>());
    }

    void Update()
    {
        if (alive == true)
        {
            //Adding randomness to the position found by the Nearest Target Method
            Transform nearestTarget = NearestTarget(targets);

            float randX = Random.Range(-randomness, randomness);
            float randY = Random.Range(-randomness, randomness);
            float randZ = Random.Range(-randomness, randomness);
            Vector3 randomizedTargetPos = new Vector3(nearestTarget.position.x + randX, nearestTarget.position.y + randY, nearestTarget.position.z + randZ);


            if (targets.Count > 1) //If more than one target is avalible moves to the one given as the nearest.
            {
                transform.position = transform.position + transform.forward * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, randomizedTargetPos) < 1f)
                {
                    targets.Remove(NearestTarget(targets));
                }
            }
            else if (targets.Count == 1 && gameOver == false) //If the duck has run out of targets it will call end game
            {
                StartCoroutine(GameOver());
                gameOver = true;
            }
            //Rotating duck towards the randomized target location
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(randomizedTargetPos - transform.position), Time.deltaTime * rotSpeed);
        }
        else
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }
        }
    }
   
    public void Death()
    {
        gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
        Destroy(gameObject.GetComponent<Animator>());
        alive = false;
        blood.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (MeshRenderer mesh in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
        Destroy(gameObject,2f);
        Amanager.Play("Quack", 0);
        UImanager.AddMoney(10);
    }


    //Given a list of transforms finds the closest one and outputs that transfrom as the nearestTarget
    Transform NearestTarget(List<Transform> targets)
    {
        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;


        foreach (Transform n in targets)
        {
            float distance = Vector3.Distance(transform.position, n.position);

            if (distance < minDistance)
            {
                nearestTarget = n;
                minDistance = distance;
            }
        }
        return nearestTarget;
    }



    IEnumerator GameOver()
    {
        StartCoroutine(GameObject.Find("GameManager").GetComponent<UIManager>().DisplayTitleText("GAME OVER",5));
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);

    }





}
