using UnityEngine;
using UnityEditor;

public class Tools : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // [MenuItem("Tools/Find Missing references in scene")]

    // public static void FindMissingReferences()

    // {

    //     var objects = GameObject.FindObjectsOfType<GameObject>();

    

    //     foreach (var go in objects)


    //     {

    //         var components = go.GetComponents();

    

    //         foreach (var c in components)

    //         {

    //             SerializedObject so = new SerializedObject(c);

    //             var sp = so.GetIterator();

    

    //             while (sp.NextVisible(true))

    //             {

    //                 if (sp.propertyType == SerializedPropertyType.ObjectReference)

    //                 {

    //                     if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)

    //                     {

    //                         ShowError(FullObjectPath(go), sp.name);

    //                     }

    //                 }

    //             }

    //         }

    //     }

    // }

    

    // private static void ShowError (string objectName, string propertyName)

    // {

    //     Debug.LogError("Missing reference found in: " + objectName + ", Property : " + propertyName);

    // }

    

    // private static string FullObjectPath(GameObject go)

    // {

    //     return go.transform.parent == null ? go.name : FullObjectPath(go.transform.parent.gameObject) + "/" + go.name;

    // }
}
