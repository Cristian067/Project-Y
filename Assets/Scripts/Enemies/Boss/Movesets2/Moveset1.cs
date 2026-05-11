using System.Collections;
using UnityEngine;

public class Moveset2_1 : Moveset
{
    public override IEnumerator Use()
    {

        for (int i = 0; i < 5; i++)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(1.5f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-1.5f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(3f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-3f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(4.5f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-4.5f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(6f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-6f, 0, 0), Quaternion.Euler(0, 180, 0));

            
            float time = 0f;
            while (time < 0.5f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }

            //Instantiate(bullet, transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(0.75f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-0.75f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(2.25f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-2.25f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(3.75f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-3.75f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(5.25f, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(bullet, transform.position+ new Vector3(-5.25f, 0, 0), Quaternion.Euler(0, 180, 0));
            time = 0f;
            while (time < 0.5f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }

        }

        

        GetComponent<BossBehavior>().ChangeInAttack(false);
        yield return null;
    }
}
