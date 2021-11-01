using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private bool isShielded;

    private Animator anim;
    private Image health_Img;
    private AudioSource audioSource;
    public AudioClip hurtClip, dieClip;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health_Img = GameObject.Find("Health Icon").GetComponent<Image>();
    }

    public bool Shielded
    {
        get
        {
            return isShielded;
        }
        set
        {
            isShielded = value;
        }
    }

    public void TakeDamage(float amount, GameObject enemy)
    {
        if(!isShielded)
        {
            health -= amount;
            health_Img.fillAmount = health / 100f;
            audioSource.PlayOneShot(hurtClip);

            if (health <= 0)
            {
                anim.SetBool("Death", true);
                audioSource.PlayOneShot(dieClip);
                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") 
                    && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
                {
                    enemy.GetComponent<EnemyController>().enemy_State = EnemyState.PATROL;
                    enemy.GetComponent<EnemyController>().target = null;
                    enemy.GetComponent<EnemyAttack>().playerTarget = null;
                }
            }
        }
      
    }

    public void HealPlayer(float healAmount)
    {
        health += healAmount;

        if (health > 100f)
            health = 100f;

        health_Img.fillAmount = health / 100f;
    }
}
