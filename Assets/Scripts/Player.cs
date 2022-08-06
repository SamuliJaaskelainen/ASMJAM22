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
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] AudioRender.WireframeRenderer wireframeRenderer;
    [SerializeField] float gameSpeedIncrease = 0.002f;
    [SerializeField] GameObject title;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject game;
    [SerializeField] GameObject ball;
    public BoxCollider movementArea;
    int balls = 0;
    GameState gameState = GameState.title;
    bool startPressed = false;
    bool playerResized = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetGameState(GameState.title);
    }

    public void StartPressed()
    {
        startPressed = true;
    }

    void Update()
    {
        if(!playerResized && transform.childCount > 1)
        {
            transform.GetChild(0).localScale *= 0.5f;
            transform.GetChild(1).localScale *= 0.5f;
            playerResized = true;
        }

        switch (gameState)
        {
            case GameState.title:
                if(startPressed)
                {
                    SetGameState(GameState.gamePlay);
                }
                break;

            case GameState.gamePlay:
                
                Time.timeScale += Time.deltaTime * gameSpeedIncrease;
                break;

            case GameState.gameOver:
                if (startPressed)
                {
                    SceneManager.LoadScene(0);
                }
                break;
        }
        startPressed = false;

        wireframeRenderer.randomOffset -= Time.unscaledDeltaTime * 0.01f;
        if(wireframeRenderer.randomOffset < 0.0f)
        {
            wireframeRenderer.randomOffset = 0.0f;
        }

        //Debug.Log(Time.timeScale);
    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;

        switch (gameState)
        {
            case GameState.title:
                Time.timeScale = 1.0f;
                wireframeRenderer.randomOffset = 0.02f;
                title.SetActive(true);
                game.SetActive(false);
                gameOver.SetActive(false);
                F_AudioManager.instance.PlayMenuTrack();
                break;

            case GameState.gamePlay:
                wireframeRenderer.randomOffset = 0.008f;
                title.SetActive(false);
                game.SetActive(true);
                gameOver.SetActive(false);
                AquireBall(transform.position + Vector3.right * 0.6f + Vector3.forward * 4.0f);
                AquireBall(transform.position + Vector3.left * 0.6f + Vector3.forward * 8.0f);
                AquireBall(transform.position);
                F_AudioManager.instance.PlayMainTrack();
                F_AudioManager.instance.StopMenuTrack();
                break;
                

            case GameState.gameOver:
                wireframeRenderer.randomOffset = 0.07f;
                title.SetActive(false);
                game.SetActive(false);
                gameOver.SetActive(true);
                F_AudioManager.instance.StopMainTrack();
                break;
        }
    }

    public void LoseBall()
    {
        --balls;
        wireframeRenderer.randomOffset = 0.008f;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/BallLost", gameObject);

        if (balls <= 0)
        {
            SetGameState(GameState.gameOver);
        }
    }

    public void AquireBall(Vector3 pos)
    {
        ++balls;
        GameObject newBall = Instantiate(ball, pos + Vector3.forward * 0.3f, Quaternion.identity) as GameObject;
        newBall.GetComponent<Ball>().Hit((transform.position - pos).normalized, false);
    }

    public bool IsGameOn()
    {
        return gameState == GameState.gamePlay;
    }
}
