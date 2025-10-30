using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private GameObject bulllet;
    [SerializeField] private float basicCooldown = 0.3f;
    float coolCount = 0f;


    [SerializeField] private float speed = 10f;

    [SerializeField] private float limitX;

    //[SerializeField] private Collider hitbox;


    private float horizontalInput;
    private float verticalInput;


    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = gameManager.GetSpeed();

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 1) * verticalInput * speed * Time.deltaTime);


        

        if(Input.GetKey(KeyCode.Z))
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

        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    co
        //}


        //limitar limites

        if (transform.position.y < -1)
        {

            transform.position = new Vector3(transform.position.x, -1, transform.position.z);

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            gameManager.LoseLife();

        }
    }
}
