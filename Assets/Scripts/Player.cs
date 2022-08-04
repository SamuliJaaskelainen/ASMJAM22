using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        float x = playerInput.actions["Right-Axis"].ReadValue<float>();
        float y = playerInput.actions["Up-Axis"].ReadValue<float>();

        Vector3 movement = (Vector3.up * y + Vector3.right * x) * Time.deltaTime;
        transform.position = transform.position + movement;
    }
}
