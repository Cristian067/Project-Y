using System.Collections;
using UnityEngine;

public abstract class Moveset : MonoBehaviour
{

    public GameObject bullet;


    public abstract IEnumerator Use();
    
    

}
