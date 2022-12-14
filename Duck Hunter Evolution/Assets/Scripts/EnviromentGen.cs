using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentGen : MonoBehaviour
{
    public List <GameObject> poi;
    
    [Range(0,1000)]
    public int poiNum;
    [Range(0, 500)]
    public float bigPoiDistance = 150f;
    [Range(0, 100)]
    public float smallPoiDistance = 25f;

    Ray ray;

    void Start()
    {
        //POI Generation--------------------------------------------------------------------------------------------------------------------------------------------------
        for (int i = 0; i < poiNum; i++)
        {
            int xSpot = Random.Range(-80, 80);
            int zSpot = Random.Range(-80, 80);
            float ySpot = 0f;
            int n = Random.Range(0, poi.Count);

            ray.origin = new Vector3(xSpot, 50, zSpot);
            ray.direction = Vector3.down;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
            {
                ySpot = hit.point.y;
            }


            GameObject newPoi = Instantiate(poi[n], new Vector3(xSpot, ySpot, zSpot), Quaternion.Euler(new Vector3(-90, Random.Range(0, 360), 0)));
            float size = Random.Range(1, 3f);
            newPoi.transform.localScale = new Vector3(size, size, size);
            if (Vector3.Distance(gameObject.transform.position, newPoi.transform.position) < bigPoiDistance && newPoi.CompareTag("BigPoi")) { Destroy(newPoi); }
            if (Vector3.Distance(gameObject.transform.position, newPoi.transform.position) < smallPoiDistance && newPoi.CompareTag("SmallPoi")) { Destroy(newPoi); }
            if(xSpot<-10 &&xSpot>-60)
            {
                if(zSpot<30 && zSpot>-30)
                {
                    Destroy(newPoi);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Target Generation--------------------------------------------------------------------------------------------------------------------------------------------------
        //for (int i = 0; i < targetNum; i++)
        //{
        //    int xSpot = Random.Range(-300, 300);
        //    int zSpot = Random.Range(-50, -150);
        //    float ySpot = Random.Range(5, 35);
        //    Instantiate(target, new Vector3(xSpot, ySpot, zSpot), Quaternion.identity);
        //}
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
