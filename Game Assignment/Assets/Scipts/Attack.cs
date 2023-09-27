using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firePoint;     //place that the bullet come out
    public GameObject bulletPrefab; // the bullet prefab
    public float bulletForce = 20f;
    public int attackspeed;
    MenuSoundManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shooting();
        }
    }

    void shooting()
    {
        //create a bullet at fire
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce * attackspeed, ForceMode2D.Impulse);
        //play bullet fly out from gun
        audioManager.PlaySFX(audioManager.shoot);
    }
}
