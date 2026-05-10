using System.Collections;
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

    private float timer;


    public void SetUp(Vector3 pos, Vector3 scale, Type _type,float duration = 10)
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
        timer = 0;
        StartCoroutine(StartTimer(duration));
        
    }

    private IEnumerator StartTimer(float time)
    {
        float _time = 0f;
        while (_time < time)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            _time += Time.deltaTime;
        }
        Destroy(this.gameObject);
        //yield return null;
    }
}
