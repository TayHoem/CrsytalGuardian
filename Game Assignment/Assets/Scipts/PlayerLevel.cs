using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [Header("Hero Sprite")]
    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    [SerializeField] private AudioSource levelUpSound;
    [Header("Level")]
    public int level;
    public float currentExp;
    public float requiredExp;
    float newExp = 0;
    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontExpBar;
    public Image backExpBar;

    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        avatar1.gameObject.SetActive(true);
        avatar2.gameObject.SetActive(false);
        avatar3.gameObject.SetActive(false);

        frontExpBar.fillAmount = currentExp / requiredExp;
        backExpBar.fillAmount = currentExp / requiredExp;

        levelText.text = "LV " + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExpUI();
    }

    private void OnEnable()
    {
        if (ExperienceManager.Instance == null)
        {
            Debug.LogError("ExperienceManager.Instance is null in Hero script!");
        }
        else
        {
            ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
        }
    }

    private void OnDisable()
    {
        //Unsubcribing from Event
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
        }
    }

    private void HandleExperienceChange(float newExperince)
    {
        if (level > 0 && level <= 2)
        {
            this.newExp = newExperince;
            currentExp += newExp;
            lerpTimer = 0f;
            delayTimer = 0f;
            if (currentExp >= requiredExp)
            {
                LevelUp();
            }
        }
    }

    public void UpdateExpUI()
    {
        if(level == 3)
        {
            currentExp = requiredExp;
        }

        float expFraction = currentExp / requiredExp;
        float fillExp = frontExpBar.fillAmount;

        if (fillExp < expFraction)
        {
            delayTimer += Time.deltaTime;
            backExpBar.fillAmount = expFraction;

            if (delayTimer > 0)
            {
                this.lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;

                frontExpBar.fillAmount = Mathf.Lerp(fillExp, backExpBar.fillAmount, percentComplete);
            }

        }
        
    }

    public void LevelUp()
    {
        level++;
        levelUpSound.Play();
        frontExpBar.fillAmount = 0f;
        backExpBar.fillAmount = 0f;
        currentExp = Mathf.RoundToInt(currentExp - requiredExp);
        requiredExp += (float)1500;
        levelText.text = "LV " + level;
        switchAvatar(level);
    }

    public void switchAvatar(int level)
    {
        switch (level)
        {
            case 2:
                {
                    avatar1.gameObject.SetActive(false);
                    avatar2.gameObject.SetActive(true);
                    avatar3.gameObject.SetActive(false);
                    Bullet.bullletDamageUp();
                    PlayerMovement.moveSpeed += 2;
                    avatar2.transform.position = avatar1.transform.position;
                }
                break;
            case 3:
                {
                    avatar1.gameObject.SetActive(false);
                    avatar2.gameObject.SetActive(false);
                    avatar3.gameObject.SetActive(true);
                    Bullet.bullletDamageUp();
                    PlayerMovement.moveSpeed += 2;
                    avatar3.transform.position = avatar2.transform.position;
                }
                break;
        }

    }

}