using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    public GameObject GroundImpact_Spawn, kick_Fx_Spawn, Fire_Tornado_Spawn, Fire_Shield_Spawn;

    public GameObject Ground_Imapct_Prefab, Kick_Fx_Prefab, Fire_Tornado_Prefab, Fire_Shield_Prefab, Heal_Fx_Prefab, Thunder_Fx_Prefab;

    void GroundImpact()
    {
        Instantiate(Ground_Imapct_Prefab, GroundImpact_Spawn.transform.position, Quaternion.identity);
    }
    void Kick()
    {
        Instantiate(Kick_Fx_Prefab, kick_Fx_Spawn.transform.position, Quaternion.identity);
    }
    void FireTornado()

    {
        Instantiate(Fire_Tornado_Prefab, Fire_Tornado_Spawn.transform.position, Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireObj = Instantiate(Fire_Shield_Prefab, Fire_Shield_Spawn.transform.position, Quaternion.identity) as GameObject;
        fireObj.transform.SetParent(transform);
    }
    void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;
      GameObject healObj =  Instantiate(Heal_Fx_Prefab, temp, Quaternion.identity) as GameObject;
        healObj.transform.SetParent(transform);
    }

    void ThunderAttack()
    {
        for(int i =0; i<8;i++)
        {
            Vector3 pos = Vector3.zero;
            if(i==0)
            {
                pos = new Vector3(transform.position.x - 4f, transform.position.y + 2f, transform.position.z);
            }
           else if (i == 1)
            {
                pos = new Vector3(transform.position.x + 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 2)
            {
                pos = new Vector3(transform.position.x , transform.position.y + 2f, transform.position.z-4f);
            }
            else if (i == 3)
            {
                pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
            }
            else if (i == 4)
            {
                pos = new Vector3(transform.position.x +2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }


            Instantiate(Thunder_Fx_Prefab, pos, Quaternion.identity);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
