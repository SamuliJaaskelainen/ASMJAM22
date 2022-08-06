using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum GameState
    {
        title,
        gamePlay,
        gameOver
    }

    public static Player Instance;
    [SerializeField] AudioRender.WireframeRenderer wireframeRenderer;
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject player;
    [SerializeField] GameObject title;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject game;
    [SerializeField] GameObject ball;
    [SerializeField] BoxCollider movementArea;
    int balls = 0;
    PlayerInput playerInput;
    GameState gameState = GameState.title;
   
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        SetGameState(GameState.title);
    }

    void Update()
    {
        if (playerInput.actions["Quit"].triggered)
        {
            Application.Quit();
        }

        switch(gameState)
        {
            case GameState.title:
                if(playerInput.actions["Start"].triggered)
                {
                    SetGameState(GameState.gamePlay);
                }
                break;

            case GameState.gamePlay:
                float x = playerInput.actions["Right-Axis"].ReadValue<float>();
                float y = playerInput.actions["Up-Axis"].ReadValue<float>();

                Vector3 movement = (Vector3.up * y + Vector3.right * x) * movementSpeed * Time.deltaTime;
                Vector3 newPos = transform.position + movement;

                Vector3 targetAngles = new Vector3(y * 20.0f, x * -20.0f, 0.0f);
                player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(targetAngles), Time.smoothDeltaTime * 3.0f);

                if (movementArea.bounds.Contains(newPos))
                {
                    transform.position = newPos;
                }
                break;

            case GameState.gameOver:
                if (playerInput.actions["Start"].triggered)
                {
                    SceneManager.LoadScene(0);
                }
                break;
        }

        wireframeRenderer.randomOffset -= Time.deltaTime * 0.01f;
        if(wireframeRenderer.randomOffset < 0.0f)
        {
            wireframeRenderer.randomOffset = 0.0f;
        }
    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;

        switch (gameState)
        {
            case GameState.title:
                wireframeRenderer.randomOffset = 0.028f;
                title.SetActive(true);
                game.SetActive(false);
                player.SetActive(false);
                gameOver.SetActive(false);
                break;

            case GameState.gamePlay:
                wireframeRenderer.randomOffset = 0.008f;
                title.SetActive(false);
                game.SetActive(true);
                player.SetActive(true);
                gameOver.SetActive(false);
                AquireBall(transform.position + Vector3.right * 0.6f + Vector3.forward * 4.0f);
                AquireBall(transform.position + Vector3.left * 0.6f + Vector3.forward * 8.0f);
                AquireBall(transform.position);
                F_AudioManager.instance.PlayMainTrack();
                break;
                

            case GameState.gameOver:
                wireframeRenderer.randomOffset = 0.08f;
                title.SetActive(false);
                game.SetActive(false);
                player.SetActive(false);
                gameOver.SetActive(true);
                F_AudioManager.instance.StopMainTrack();
                break;
        }
    }

    public void LoseBall()
    {
        --balls;
        wireframeRenderer.randomOffset = 0.008f;
        // TODO: Play lose ball sfx

        if (balls <= 0)
        {
            SetGameState(GameState.gameOver);
        }
    }

    public void AquireBall(Vector3 pos)
    {
        ++balls;
        GameObject newBall = Instantiate(ball, pos + Vector3.forward * 0.3f, Quaternion.identity) as GameObject;
        newBall.GetComponent<Ball>().Hit((transform.position - pos).normalized);
    }
}
