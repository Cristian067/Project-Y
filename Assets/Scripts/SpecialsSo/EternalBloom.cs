using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Specials/Eternal Bloom")]
public class EternalBloom : SpecialSO
{

    public override IEnumerator Use(GameObject owner)
    { 
        float speedOg = GameManager.instance.GetSpeed();
        GameManager.instance.SetSpeed(speedOg * 1.5f);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(5);
        Time.timeScale = 1;
        GameManager.instance.SetSpeed(speedOg);
    }

}



