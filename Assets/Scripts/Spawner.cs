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
            Instantiate(objects[Random.Range(0, objects.Count)], transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        }
    }
}
