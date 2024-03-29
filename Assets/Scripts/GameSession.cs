﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    // Use this for initialization

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    void Awake()
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {

            Destroy(gameObject);
        }

        else
        {

            DontDestroyOnLoad(gameObject);

        }

    }

    void Start()
    {

        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();

    }

    public void AddToScore(int pointsToAdd)
    {

        score += pointsToAdd;
        scoreText.text = score.ToString();

    }

    public void ProcessPlayerDeath()
    {

        if (playerLives > 1)
        {

            playerLives--;
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            livesText.text = playerLives.ToString();

        }
        else
        {

            SceneManager.LoadScene(0);
            DestroyObject(gameObject);

        }


    }

}
