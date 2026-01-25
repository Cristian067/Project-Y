using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulllet;
    [SerializeField] private float basicCooldown = 0.3f;
    float coolCount = 0f;

    float charge = 0;


    [SerializeField] private float speed = 10f;

    [SerializeField] private float limitX;
    [SerializeField] private float upperLimitZ;
    [SerializeField] private float bottomLimitZ;


    [SerializeField] private float hitCooldown;

    [SerializeField] private Collider hitbox;
    [SerializeField] private GameObject PlayerMaterialGO;



    private float horizontalInput;
    private float verticalInput;


    public UnityEvent pauseMovement;


    //private bool isInPause;


    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMaterialGO.GetComponent<MeshRenderer>().material.color = new Vector4(PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.r, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.g, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.b, 1);
        //PlayerMaterial.color ;
        //gameManager = FindObjectOfType<GameManager>();

        //pauseMovement.AddListener


    }

    public void PauseMovement()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameManager.instance.GetSpeed();


        if (GameManager.instance.paused)
        {
            return;
        }
        
            
            
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 1) * verticalInput * speed * Time.deltaTime);



            //shoot

        if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.chargedShoot))
        {
            if (Input.GetButton("Fire") && !GameManager.instance.paused)
            {
                if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.magicMirror))
                {
                    
                    DoubleShoot();
                }
                else
                {
                    ChargedShoot(true);
                }
                
            }
            else if(Input.GetButtonUp("Fire") && !GameManager.instance.paused)
            {
                ChargedShoot(false);
            }
        }

        else if (Input.GetButton("Fire") && !GameManager.instance.paused)
        {
            if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.magicMirror))
            {
                
                DoubleShoot();
            }
            else
            {
                Shoot();
            }
               
        }

        if(Input.GetButtonDown("Special") && UpgradesManager.instance.special != null && !GameManager.instance.paused && !GameManager.instance.specialInCooldown && GameManager.instance.specials > 0)
        {
            GameManager.instance.specials -= 1;
            StartCoroutine(UpgradesManager.instance.special.special.Use(gameObject));
               
        }
        
        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    co
        //}


        //limitar limites
        if (UpgradesManager.instance.upgrades.Contains(UpgradesManager.instance.effects.sideToSideEffect))
        {
            if (transform.position.z < bottomLimitZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, bottomLimitZ);
            }
            if (transform.position.z > upperLimitZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, upperLimitZ);
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


            if (transform.position.z < bottomLimitZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, bottomLimitZ);
            }
            if (transform.position.z > upperLimitZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, upperLimitZ);
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

    private void UpdateAnimations()
    {
        
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

    private void ChargedShoot(bool charging)
    {
        if (charging)
        {
            charge += Time.deltaTime*3;

            if(charge > 2)
            {
                charge = 2;
            }
        }

        else
        {
            
            GameObject bulletOut = Instantiate(bulllet, transform.position, Quaternion.identity);

            bulletOut.GetComponentInChildren<BulletsBehavior>().damage = GameManager.instance.GetPlayerDamage() * charge;
            bulletOut.transform.localScale = new Vector3(charge/2, charge/2,charge/2);

            Destroy(bulletOut, 5f);
            charge =0;
            
        }
        


    }

    public IEnumerator HitInCooldown()
    {
        hitbox.enabled = false;
        PlayerMaterialGO.GetComponent<MeshRenderer>().material.color = new Vector4(PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.r, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.g, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.b, 0.30f);
        yield return new WaitForSeconds(hitCooldown);
        hitbox.enabled = true;
        PlayerMaterialGO.GetComponent<MeshRenderer>().material.color = new Vector4(PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.r, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.g, PlayerMaterialGO.GetComponent<MeshRenderer>().material.color.b, 1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (GameManager.instance.isBarrierActive())
            {
                Destroy(other.GetComponentInParent<Transform>().GetComponentInParent<Transform>().gameObject);
                GameManager.instance.DestroyBarrier();
            }
            else
            {
                GameManager.instance.LoseLife();
            }
            

        }
    }
}
