using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public float speed;
    [SerializeField] private int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void ChangePointsValue(int newValue)
    {
        points = newValue;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collect")
        {
            //Debug.Log("Yoink");
            GameManager.instance.AddPoints(points);
            Destroy(gameObject);

           
        }
    }
}
