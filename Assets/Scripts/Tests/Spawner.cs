using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemiesPrefab;
    [SerializeField] private float spawnTime;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy",0,spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-7, 7);
        int randomEnemy = Random.Range(0, enemiesPrefab.Length);

        Instantiate(enemiesPrefab[randomEnemy], new Vector3(randomX, 0, 16), Quaternion.Euler(0,180,0));

    }


}
