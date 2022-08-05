using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject ball;
    [SerializeField] BoxCollider movementArea;
    int balls = 0;
    PlayerInput playerInput;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        float x = playerInput.actions["Right-Axis"].ReadValue<float>();
        float y = playerInput.actions["Up-Axis"].ReadValue<float>();

        Vector3 movement = (Vector3.up * y + Vector3.right * x) * movementSpeed * Time.deltaTime;
        Vector3 newPos =transform.position + movement;

        if(movementArea.bounds.Contains(newPos))
        {
            transform.position = newPos;
        }
    }

    public void LoseBall()
    {
        --balls;
        // TODO: Play lose ball sfx

        if(balls <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AquireBall(Vector3 pos)
    {
        ++balls;
        GameObject newBall = Instantiate(ball, pos + Vector3.forward * 0.3f, Quaternion.identity) as GameObject;
        newBall.GetComponent<Ball>().Hit((transform.position - pos).normalized);
    }
}
