using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField]private Terrain terrainGO;
    

    [SerializeField]private float backgroundSpeed;
    private float backgroundCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        
        
    }

    // Update is called once per frame
    void Update()
    {

        
        backgroundCount += 0.1f * backgroundSpeed*Time.deltaTime;
        if(backgroundCount > 2)
        {
            backgroundCount = 0;
        }

        terrainGO.terrainData.terrainLayers[0].tileOffset = new Vector2(1,backgroundCount);
    }
}
