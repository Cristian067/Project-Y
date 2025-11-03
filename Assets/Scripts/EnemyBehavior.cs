using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{



    [SerializeField] private int life;
    [SerializeField] private float speed;

    [SerializeField] private bool canShoot;
    

    [SerializeField] private GameObject pointGo;

    public enum States
    {
        Stop,
        Moving,

    }

    private States actualState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);


        if(transform.position.z <= -5.5f)
        {
            Destroy(gameObject);
        }
    }


    public void Go()
    {

    }


    public void Hurt(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Instantiate(pointGo,transform.position,Quaternion.Euler(0, 180, 0));
            Destroy(gameObject);
        }
    }

    // void OnDestroy()
    // {
        
    // }


}
