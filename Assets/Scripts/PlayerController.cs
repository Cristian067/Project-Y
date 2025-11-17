using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            coolCount += Time.deltaTime;
            //Debug.Log(coolCount);
            if (coolCount > basicCooldown)
            {
                GameObject bulletOut = Instantiate(bulllet, transform.position, Quaternion.identity);

                Destroy(bulletOut, 5f);
                coolCount = 0f;
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
            if (transform.position.x > limitX)
            {
                transform.position = new Vector3(-limitX, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -limitX)
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
