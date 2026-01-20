using UnityEngine;

public class Magnet : MonoBehaviour
{

    public float range;

    private SphereCollider magnetCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        magnetCollider = GetComponent<SphereCollider>();
        magnetCollider.radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMagnetRange(float newRange)
    {
        range = newRange;
        magnetCollider.radius = range;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            other.GetComponent<Points>().Collect();
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
