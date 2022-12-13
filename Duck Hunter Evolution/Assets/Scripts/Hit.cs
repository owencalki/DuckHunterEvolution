using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    float taken = 0;

    public void Shot(float damage)
    {
        taken += damage;
        
        Debug.Log(taken);
    }

}
