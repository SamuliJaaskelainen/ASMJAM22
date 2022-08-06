using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{
    SphereCollider sphere;
    void Start()
    {
        sphere = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.GetChild(0).position) < sphere.radius)
        {
            AquireNewBall();
        }
        else if(Player.Instance.transform.childCount > 1)
        { 
            if (Vector3.Distance(transform.position, Player.Instance.transform.GetChild(1).position) < sphere.radius)
            {
                AquireNewBall();
            }
        }
    }

    void AquireNewBall()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/BallAcquired", gameObject);
        Player.Instance.AquireBall(transform.position);
        Destroy(gameObject);
    }
}
