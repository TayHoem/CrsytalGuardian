using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EnemyKillCount : MonoBehaviour
{
    public static int countKill = 0;
    public int killUpdate = 0;
    public TextMeshProUGUI killCountText;

    // Start is called before the first frame update
    void Start()
    {
        killCountText.text = "" + killUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        if(killUpdate < countKill)
        {
            killUpdate = countKill;
            killCountText.text = "" + killUpdate;
        }
    }
}
