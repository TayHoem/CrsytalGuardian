using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
}
