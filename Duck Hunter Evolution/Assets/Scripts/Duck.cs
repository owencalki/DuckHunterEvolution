using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] rbs;
    GameObject pattern;
    List <Transform> targets;
    public float speed = 0.5f;
    public float rotSpeed = 3f;


    void Start()
    {
        speed = speed * Random.Range(0.8f,1.2f);

        pattern = GameObject.FindGameObjectWithTag("Pattern");
        targets = new List<Transform>(pattern.GetComponentsInChildren<Transform>());
    }

    void Update()
    {

        Transform nearestTarget = NearestTarget(targets);
        if (targets.Count > 1) //If more than one target is avalible moves to the one given as the nearest.
        {
            transform.position = transform.position + transform.forward * speed*Time.deltaTime;
            if (Vector3.Distance(transform.position, nearestTarget.position) < 0.5)
            {
                targets.Remove(NearestTarget(targets));
            }
        }
        else if(targets.Count==0) //If the duck has run out of targets it will destroy itself
        {
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nearestTarget.position - transform.position), Time.deltaTime * rotSpeed);
    }
   
    public void Shot()
    {

    }

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
    } //Given a list of transforms finds the closest one and outputs that transfrom as the nearestTarget
}
