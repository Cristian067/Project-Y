using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy",0,1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-7, 7);

        Instantiate(enemyPrefab, new Vector3(randomX, 0, 16), Quaternion.Euler(0,180,0));

    }


}
