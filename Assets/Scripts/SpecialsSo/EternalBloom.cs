using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Specials/Eternal Bloom")]
public class EternalBloom : SpecialSO
{

    //public float cooldown;
    public override IEnumerator Use(GameObject owner)
    { 

        GameManager.instance.SetSpecialCooldown(cooldown);
        float speedOg = GameManager.instance.GetSpeed();
        GameManager.instance.SetSpeed(speedOg * 1.5f);
        Time.timeScale = 0.3f;
        float time = 0f;
        while (time < 3)
        {
            yield return null;
            if (GameManager.instance.paused)
            continue;
            time += Time.deltaTime;
        }
        //yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        GameManager.instance.SetSpeed(speedOg);
    }


}



