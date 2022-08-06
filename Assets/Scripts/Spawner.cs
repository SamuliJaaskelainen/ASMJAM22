using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    float spawnerTimer;

    void Update()
    {
        if(Time.time > spawnerTimer)
        {
            spawnerTimer = Time.time + Random.Range(1.0f, 5.0f);
            GameObject newObject = Instantiate(objects[Random.Range(0, objects.Count)], transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))) as GameObject;
            Transform objectGraphics = newObject.transform.GetChild(0).transform;
            objectGraphics.localEulerAngles = new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            objectGraphics.localScale = objectGraphics.localScale * Random.Range(0.8f, 1.4f);
        }
    }
}
