using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public AudioSource audioSource;
    public AudioClip gunshot;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(gunshot);
    }
}
