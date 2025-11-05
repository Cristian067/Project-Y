using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletGo;
    [SerializeField] private float shootCooldown;

    [SerializeField] private bool targetShoot;


    [SerializeField] private float countCooldown;

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {

        //countCooldown = Time.time;

        //if (countCooldown > shootCooldown)
        //{
        //    countCooldown = 0;
        //    Shoot();

        //}

    }

    public void StartShooting()
    {
        InvokeRepeating("Shoot", shootCooldown, shootCooldown);
    }
    
    public void StopShooting()
    {
        CancelInvoke("Shoot");
    }


    private void Shoot()
    {
        if (targetShoot)
        {
            Transform playerTransform = GameObject.Find("Player").transform;

            Vector3 direction = transform.position - playerTransform.position;

            GameObject bullet = Instantiate(bulletGo,transform.position,Quaternion.identity);

            bullet.transform.forward = -direction;


        }
        else
        {
            GameObject bullet = Instantiate(bulletGo,transform.position,Quaternion.Euler(0,180,0));
        }

    }


}
