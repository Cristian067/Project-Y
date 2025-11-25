using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehavior : MonoBehaviour
{

    [SerializeField] private float speed = 2f;

    [SerializeField] private bool enemy = true;

    public enum BulletsType
    {
        Normal,
        Bomb,
        Unbreakable,

    }

    [SerializeField] private BulletsType type;

    public float damage;

    [Header("BombBehavior")]
    [SerializeField] public GameObject bulletForExplode;
    [SerializeField] private float timeForExplode;
    [SerializeField] private int bulletsInExplosion;
    [SerializeField] private float dispersionSpeed;


    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case BulletsType.Bomb:
                Invoke("Bomb",timeForExplode);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.position.z > 16 || transform.position.z < -10 || transform.position.x >10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void Bomb()
    {
        for(int i = 0; i < bulletsInExplosion; i++)
        {
            float radual = 360/ bulletsInExplosion;
            Instantiate(bulletForExplode,transform.position,Quaternion.Euler(0,(i+1)*radual,0));
        }
        Destroy(gameObject);
    }

    public void SetBomb(float time)
    {
        timeForExplode = time;

    }
    public void SetBomb(float time, int bullets)
    {
        timeForExplode = time;
        bulletsInExplosion = bullets;
        
    }
    public void SetBomb(float time,int bullets,float speed)
    {
        timeForExplode = time;
        bulletsInExplosion = bullets;
        dispersionSpeed = speed;
    }

    public void ChangeEnemy(bool isEnemy)
    {
        enemy = isEnemy;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void OnTriggerEnter(Collider other)
    {

        if (enemy)
        {
            if(other.gameObject.tag == "Barrier")
            {
                GameManager.instance.DestroyBarrier();
                Destroy(gameObject);
            }
            if (other.gameObject.tag == "Hitbox")
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
                other.gameObject.GetComponent<EnemyBehavior>().Hurt(damage);
                Destroy(gameObject);


            }
        }
    }



}
