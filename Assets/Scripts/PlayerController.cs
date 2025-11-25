using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulllet;
    [SerializeField] private float basicCooldown = 0.3f;
    float coolCount = 0f;


    [SerializeField] private float speed = 10f;

    [SerializeField] private float limitX;


    [SerializeField] private float hitCooldown;

    [SerializeField] private Collider hitbox;
    [SerializeField] private Material PlayerMaterial;


    private float horizontalInput;
    private float verticalInput;


    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMaterial.color = new Vector4(PlayerMaterial.color.r, PlayerMaterial.color.g, PlayerMaterial.color.b, 1);
        //gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameManager.instance.GetSpeed();

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 1) * verticalInput * speed * Time.deltaTime);





        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.J))
        {
            if (GameManager.instance.GetUpgrades().Contains(UpgradesManager.instance.effects.magicMirror))
            {
                
                DoubleShoot();
            }
            else
            {
                Shoot();
            }
            
        }

        if((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) && GameManager.instance.GetSpecial() != null)
        {

            StartCoroutine(GameManager.instance.GetSpecial().special.Use(gameObject));
            
        }

        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    co
        //}


        //limitar limites
        if (GameManager.instance.GetUpgrades().Contains(UpgradesManager.instance.effects.sideToSideEffect))
        {
            if (transform.position.z < -2.75f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -2.75f);
            }
            if (transform.position.z > 11.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 11.5f);
            }
            if (transform.position.x > limitX+1)
            {
                transform.position = new Vector3(-limitX, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -limitX-1)
            {
                transform.position = new Vector3(limitX, transform.position.y, transform.position.z);
            }
        }
        else
        {


            if (transform.position.z < -2.75f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -2.75f);
            }
            if (transform.position.z > 11.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 11.5f);
            }

            if (transform.position.x > limitX)
            {
                transform.position = new Vector3(limitX, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -limitX)
            {
                transform.position = new Vector3(-limitX, transform.position.y, transform.position.z);
            }

        }



    }

    private void DoubleShoot()
    {
        coolCount += Time.deltaTime;
            //Debug.Log(coolCount);
            if (coolCount > basicCooldown)
            {
                GameObject bulletOut = Instantiate(bulllet, transform.position + new Vector3(0.4f,0,0), Quaternion.identity);
                GameObject bulletOut2 = Instantiate(bulllet, transform.position + new Vector3(-0.4f,0,0), Quaternion.identity);

                bulletOut.GetComponent<BulletsBehavior>().damage = GameManager.instance.GetPlayerDamage() * 0.75f;
                bulletOut2.GetComponent<BulletsBehavior>().damage = GameManager.instance.GetPlayerDamage() * 0.75f;

                Destroy(bulletOut, 5f);
                Destroy(bulletOut2, 5f);
                coolCount = 0f;
            }
    }

    private void Shoot()
    {
        coolCount += Time.deltaTime;
            //Debug.Log(coolCount);
            if (basicCooldown < coolCount)
            {
                GameObject bulletOut = Instantiate(bulllet, transform.position, Quaternion.identity);

                bulletOut.GetComponentInChildren<BulletsBehavior>().damage = GameManager.instance.GetPlayerDamage();

                Destroy(bulletOut, 5f);
                coolCount = 0f;
            }
    }

    public IEnumerator HitInCooldown()
    {
        hitbox.enabled = false;
        PlayerMaterial.color = new Vector4(PlayerMaterial.color.r, PlayerMaterial.color.g, PlayerMaterial.color.b, 0.30f);
        yield return new WaitForSeconds(hitCooldown);
        hitbox.enabled = true;
        PlayerMaterial.color = new Vector4(PlayerMaterial.color.r, PlayerMaterial.color.g, PlayerMaterial.color.b, 1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            GameManager.instance.LoseLife();

        }
    }
}
