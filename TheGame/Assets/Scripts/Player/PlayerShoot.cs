using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject ballPrefab, projectilePrefab;
    public float reloadTime = 0f;

    private float projectileTime = 0.1f;
    private float reload = 0;
    private GameObject oldObj;
    private Color oldColor;

    // Update is called once per frame
    void Update()
    {
        if (reload <= 0)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                Fire(Pole.NORTH);
            }
            else if (Input.GetAxis("Fire2") > 0)
            {
                Fire(Pole.SOUTH);
            }
        }


        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }
    }

    //private void FixedUpdate()
    //{
    //    projectileTime -= Time.fixedDeltaTime;
    //    if (projectileTime <= 0f)
    //    {
    //        //Simulate();
    //        projectileTime = 0.1f;
    //    }
    //}

    //void Simulate()
    //{
    //    var bullet = Instantiate(projectilePrefab);
    //    bullet.transform.position = transform.position;
    //    bullet.transform.rotation = transform.rotation;
    //    bullet.transform.SetParent(transform);
    //}

    void Fire(Pole type)
    {
        var bullet = Instantiate(ballPrefab);
        bullet.transform.position = transform.position + transform.forward;
        bullet.transform.rotation = transform.rotation;
        bullet.AddComponent<BulletCollide>();
        bullet.GetComponent<BulletCollide>().type = type;
        reload = reloadTime;

        // Play shooting sound
        AudioSource temp = GetComponent<AudioSource>();
        AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
        laudio.PlayOneShot((AudioClip)Resources.Load("PlayerBulletShoot"));
    }
}
