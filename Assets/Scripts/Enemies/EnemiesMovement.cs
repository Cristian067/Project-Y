using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{

    [SerializeField] public List<Vector3> positions;
    //[SerializeField] public Vector3 position;
    //[SerializeField] public List<Vector3> positions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }





    void OnDrawGizmos()
    {
        

    }

    void OnDrawGizmosSelected()
    {
        if (positions.Count > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < positions.Count; i++)
            {
                //Gizmos.color = Color.white;
                Gizmos.DrawIcon(positions[i], "Light Gizmo.tiff");
                if (i + 1 < positions.Count)
                {
                    Gizmos.DrawLine(positions[i], positions[i + 1]);
                }

            }
            
            

        }
    }

}
