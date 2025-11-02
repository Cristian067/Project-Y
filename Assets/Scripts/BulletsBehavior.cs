using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehavior : MonoBehaviour
{

    [SerializeField] private float speed = 2f;

    [SerializeField] private bool enemy = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }



    void OnTriggerEnter(Collider other)
    {

        if (enemy)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.instance.LoseLife();
                //other.gameObject.GetComponent<EnemyBehavior>().Hurt(GameManager.instance.GetPlayerDamage());
                Destroy(gameObject);


            }

        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyBehavior>().Hurt(GameManager.instance.GetPlayerDamage());
                Destroy(gameObject);


            }
        }
    }



}
