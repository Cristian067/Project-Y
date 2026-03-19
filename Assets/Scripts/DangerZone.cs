using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public enum Type
    {
        Square,
        Cylinder
    }

    public Type type;


    public GameObject square;
    public GameObject cylinder;


    public void SetUp(Vector3 pos, Vector3 scale, Type _type)
    {

        type = _type;

        transform.position = pos;

        if (type == Type.Square)
        {
            square.SetActive(true);
            cylinder.SetActive(false);

            square.transform.localScale = scale;
        }
        else if (type == Type.Cylinder)
        {
            square.SetActive(false);
            cylinder.SetActive(true);

            cylinder.transform.localScale = scale;

        }
        
    }
}
