using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duck;
    float nextWaveTimer;
    float timeBetweenWaves = 5;
    int wavesPerRound = 0;
    int ducksPerSpawn;
    int roundIndex = 1;
    bool activeRound = false;

    public AnimationCurve wavesPerRoundCurve;
    public AnimationCurve ducksPerSpawnCurve;
    public AnimationCurve timeBetweenWavesCurve;



    Vector3 startPos = new Vector3(-140,0,-100);

    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(gameObject.GetComponent<UIManager>().DisplayTitleText("Round " + roundIndex + " Started", 2));
            RoundStart(roundIndex);
        }




        nextWaveTimer -= Time.deltaTime;
        if(nextWaveTimer<=0 && wavesPerRound>0)
        {
            wavesPerRound -= 1;
            nextWaveTimer = timeBetweenWaves;
            SpawnWave(ducksPerSpawn);
        }
        else if (wavesPerRound<=0 && GameObject.FindGameObjectWithTag("Duck")==null && activeRound==true)
        {
            StartCoroutine(gameObject.GetComponent<UIManager>().DisplayTitleText("Round Over", 1.5f));
            activeRound = false;
        }

    }

    

    void SpawnWave(int numberOfDucks)
    {
        for (int i = 0; i < numberOfDucks; i++)
        {
            Instantiate(duck, startPos, Quaternion.identity);
        }
    }

    void RoundStart(int round)
    {
        Debug.Log("ROUND STARTED");
        activeRound = true;

        roundIndex++;

        wavesPerRound = Mathf.RoundToInt(wavesPerRoundCurve.Evaluate(round));
        ducksPerSpawn = Mathf.RoundToInt(ducksPerSpawnCurve.Evaluate(round));
        timeBetweenWaves = timeBetweenWavesCurve.Evaluate(round);


    }
     
}
