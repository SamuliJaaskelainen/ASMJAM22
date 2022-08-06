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
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) < sphere.radius)
        {
            Player.Instance.AquireBall(transform.position);
            Destroy(gameObject);

            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/BallAcquired", gameObject);
        }
    }
}
