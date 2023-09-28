using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
     public static float timeLimit = 300;

    // Update is called once per frame
    void Update()
    {
        if(timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 4)
            {
                timeLimit -= Time.deltaTime;
                timeText.color = Color.red;
            }
        }
        else
        {
            timeLimit = 0;
            timeText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}


