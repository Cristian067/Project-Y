using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField]private GameObject backgroundGO;
    [SerializeField] private Material backgroundMaterial;

    [SerializeField]private float backgroundSpeed;

    [SerializeField] private GameObject[] backgroundsBlocks;
    private float backgroundCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        backgroundMaterial = backgroundGO.GetComponent<MeshRenderer>().material;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        backgroundCount += 0.1f * backgroundSpeed*Time.deltaTime;
        if(backgroundCount > 2)
        {
            backgroundCount = 1;
        }
        backgroundMaterial.SetVector("_Offset",new Vector2(1,backgroundCount) );

        foreach(var block in backgroundsBlocks)
        {
            block.transform.Translate( new Vector3(0,0,1)* backgroundSpeed*Time.deltaTime);
            Renderer render = block.GetComponent<Renderer>();

            if(block.transform.position.z < -17.5f)
            {
                Debug.Log("si");
            }



            
        }


    }
}
