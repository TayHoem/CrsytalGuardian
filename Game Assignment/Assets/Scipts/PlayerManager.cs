using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public static Vector2 lastCheckPointPos = new Vector2(-3, 0);
    public GameObject[] playerPrefabs;
    public static int numberOfCoins;
    int characterIndex;
    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false); ;
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("NewGame");
    }
}
