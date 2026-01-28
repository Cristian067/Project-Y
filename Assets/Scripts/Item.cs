using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed;
    [SerializeField] private int points;

    public enum ItemType
    {
        Point,
        Health,
        Special
    }

    public ItemType type;

    public Material[] itemMaterials;

    public bool random;

    private bool collected;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 3);

        int p = Random.Range(0, 100);
        
        if (p < 75)
        {
            r = 0;
        }
        else if (p < 85)
        {
            r = 1;
        }
        else
        {
            r = 2;
        }

            type = (ItemType)r;


        gameObject.GetComponent<MeshRenderer>().material = itemMaterials[r];


        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        if (collected)
        {
            transform.Translate(-(GameObject.Find("Player").transform.position - transform.position).normalized * speed * Time.deltaTime);
        }
        else
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


    }

    public void ChangePointsValue(int newValue)
    {
        points = newValue;
    }
    
    public void Collect()
    {
        collected = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collect")
        {

            if (type == ItemType.Point)
            {
                GameManager.instance.AddPoints(points);
            }

            else if (type == ItemType.Health)
            {
                GameManager.instance.Heal(1);
            }
            else if (type == ItemType.Special)
            {
                GameManager.instance.RechargeSpecial(0.5f);
            }
            //Debug.Log("Yoink");
            
            Destroy(gameObject);

           
        }
    }
}
