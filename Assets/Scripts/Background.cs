using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Background : MonoBehaviour
{

    [SerializeField]private GameObject backgroundGO;
    [SerializeField] private Material backgroundMaterial;

    [SerializeField]private float backgroundSpeed;
    [SerializeField] float visualSpeedMultiplier = 1.08f;

    [SerializeField] private GameObject[] backgroundsEnvironmentObjects;
    private float backgroundCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundGO == null)
        {
            backgroundGO = GameObject.Find("BackgroundPlane");
        }
        backgroundMaterial = backgroundGO.GetComponent<MeshRenderer>().material;
        backgroundGO.GetComponent<Renderer>().material.SetFloat("_Speed", backgroundSpeed);
        backgroundsEnvironmentObjects = GameObject.FindGameObjectsWithTag("Environment");

        //Debug.Log(backgroundGO.GetComponent<Renderer>().bounds.size);


    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.paused)
        {
            backgroundGO.GetComponent<Renderer>().material.SetFloat("_Speed", 0);

            return;
        }
        backgroundGO.GetComponent<Renderer>().material.SetFloat("_Speed", backgroundSpeed);
        //backgroundCount += (backgroundSpeed/(backgroundGO.GetComponent<Renderer>().bounds.size.z/backgroundMaterial.GetVector("_Tiling").y ))  * Time.deltaTime;
        //if(backgroundCount > 2)
        //{
        //    backgroundCount = 1;
        //}
        //backgroundMaterial.SetVector("_Offset",new Vector2(1,backgroundCount) );

        try
        {
            foreach(var block in backgroundsEnvironmentObjects)
            {
                //GameObject enviorement = Instantiate(block);
                block.transform.Translate( new Vector3(0,0,-1f) * ((backgroundSpeed * Time.deltaTime) *visualSpeedMultiplier));

                //  1.5 / 1 * 40 = 60
                if (block.transform.position.z < -12.5f)
                {
                    //Debug.Log(block.transform.);
                    //

                    block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, 25);
                    //Debug.Log("si");
                }



                
            }
        }
        catch
        {
            Debug.Log("blocks error");
        }
        


    }
    public void ChangeSpeed(float newSpeed)
    {
        backgroundSpeed = newSpeed;
        //backgroundGO.GetComponent<Renderer>().material.SetFloat("_Speed", backgroundSpeed);
    }
}
