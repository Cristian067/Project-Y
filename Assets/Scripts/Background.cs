using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

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
        if (backgroundGO == null)
        {
            backgroundGO = GameObject.Find("BackgroundPlane");
        }
        backgroundMaterial = backgroundGO.GetComponent<MeshRenderer>().material;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.paused)
        {
            return;
        }

            float textureLength =
            backgroundGO.GetComponent<Renderer>().bounds.size.z /
            backgroundMaterial.GetVector("_Tiling").y;


        backgroundCount += (backgroundSpeed/(backgroundGO.GetComponent<Renderer>().bounds.size.z/backgroundMaterial.GetVector("_Tiling").y ))  * Time.deltaTime;
        if(backgroundCount > 2)
        {
            backgroundCount = 1;
        }
        backgroundMaterial.SetVector("_Offset",new Vector2(1,backgroundCount) );
        try
        {
            foreach(var block in backgroundsBlocks)
            {
                //GameObject enviorement = Instantiate(block);
                block.transform.Translate( new Vector3(0,0,-1f) * backgroundSpeed * Time.deltaTime);

                //  1.5 / 1 * 40 = 60

                Renderer render = block.GetComponent<Renderer>();

                if (block.transform.position.z < -17.5f)
                {
                    //Debug.Log(block.transform.);
                    //

                    block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, 24);
                    //Debug.Log("si");
                }



                
            }
        }
        catch
        {
            Debug.Log("blocks error");
        }
        


    }
}
