using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float minSpeed = 0.5f;
    [SerializeField] float speedChangeMultiplier = 3.0f;
    [SerializeField] LayerMask layerMask;
    AudioRender.Rotate rotate;
    Vector3 direction;
    RaycastHit hit;
    float speed;
    bool dirSwapped = false;

    void Start()
    {
        rotate = GetComponent<AudioRender.Rotate>();
        speed = maxSpeed;
        Hit(Vector3.forward);
    }

    public void Hit(Vector3 dir)
    {
        direction = dir;
        // TODO: Play hit sfx
    }

    void Update()
    {
        Vector3 newPos = transform.position + direction * speed * Time.deltaTime;
        if(Physics.Linecast(transform.position, newPos, out hit, layerMask))
        {
            Debug.Log("Hit", hit.transform.gameObject);
            Debug.DrawRay(hit.point, hit.normal, Color.white, 2.0f);
            Hit(Vector3.Reflect(direction, hit.normal));

            if(hit.transform.tag == "Paddle")
            {
                speed = maxSpeed;
                dirSwapped = false;
            }
        }
        else
        {
            transform.position = newPos;
        }

        if(transform.position.z < (Player.Instance.transform.position.z - 1.0f))
        {
            Player.Instance.LoseBall();
            Destroy(gameObject);
        }

        if(dirSwapped)
        {
            speed += speedChangeMultiplier * Time.deltaTime;
            speed = Mathf.Min(speed, maxSpeed);
            rotate.degreesPerSecond = speed * 100.0f;
        }
        else
        {
            float newSpeed = speed - speedChangeMultiplier * Time.deltaTime;
            if (newSpeed < minSpeed && speed >= minSpeed)
            {
                dirSwapped = true;
                direction = Vector3.Reflect(direction, Vector3.back);
            }
            speed = newSpeed;
            rotate.degreesPerSecond = speed * -100.0f;
        }
    }
}