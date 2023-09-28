using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreenObject;
    private int totalFinalKill;
    public TextMeshProUGUI finalKill;

    public void Awake()
    {
        isGameOver = false;
    }


    private void Update()
    {   
        if (isGameOver)
        { 
            gameOverScreenObject.SetActive(true);
            finalKill.text = EnemyKillCount.countKill.ToString();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("NewGame");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
