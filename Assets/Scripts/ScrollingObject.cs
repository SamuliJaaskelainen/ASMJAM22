using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public static float scrollSpeed = 4.0f;
    [SerializeField] bool isLooping = false;
    [SerializeField] float loopZ = 20.0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.back * scrollSpeed * Time.deltaTime;
        if(transform.position.z < -10.0f)
        {
            if(isLooping)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, loopZ);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
