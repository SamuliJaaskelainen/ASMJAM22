using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerGraphics;
    [SerializeField] float movementSpeed = 2.5f;
    PlayerInput playerInput;

    void Start()
    {
        transform.parent = Player.Instance.transform;
        transform.localPosition = Vector3.zero;
        playerInput = GetComponent<PlayerInput>();
        playerGraphics.SetActive(false);
    }

    void Update()
    {
        if (playerInput.actions["Quit"].triggered)
        {
            Application.Quit();
        }

        if (playerInput.actions["Start"].triggered)
        {
            Player.Instance.StartPressed();
        }

        if (Player.Instance.IsGameOn())
        {
            if (!playerGraphics.activeSelf)
            { 
                playerGraphics.SetActive(true);
            }

            float x = playerInput.actions["Right-Axis"].ReadValue<float>();
            float y = playerInput.actions["Up-Axis"].ReadValue<float>();

            Vector3 movement = (Vector3.up * y + Vector3.right * x) * movementSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + movement;

            Vector3 targetAngles = new Vector3(y * 22.0f, x * -22.0f, 0.0f);
            playerGraphics.transform.localRotation = Quaternion.Slerp(playerGraphics.transform.localRotation, Quaternion.Euler(targetAngles), Time.smoothDeltaTime * 8.0f);

            if (Player.Instance.movementArea.bounds.Contains(newPos))
            {
                transform.position = newPos;
            }
        }
        else if(playerGraphics.activeSelf)
        {
            playerGraphics.SetActive(false);
        }
    }
}
