using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

  [SerializeField]  private Image health_IMG;

    private Animator anim;

    private AudioSource audioSource;
    public AudioClip hurtClip, dieClip;
    public GameObject deathFX;
    public bool enemy1, enemy2, enemy3;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (tag == "Boss")
        {
            health_IMG = GameObject.Find("Health Foreground Boss").GetComponent<Image>();
        }
        else
        {
            if (enemy1)
            {
                health_IMG = GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Image>();

            }

            if (enemy2)
            {
                health_IMG = GameObject.FindGameObjectWithTag("Enemy2").GetComponent<Image>();
            }

            if (enemy3)
            {
                health_IMG = GameObject.FindGameObjectWithTag("Enemy3").GetComponent<Image>();
            }

        }
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)

    {
        health -= amount;

        health_IMG.fillAmount = health / 100f;
        audioSource.PlayOneShot(hurtClip);

        if (health <= 0)
        {
            health_IMG.fillAmount = health / 100f;
            anim.SetBool("Death",true);
            Instantiate(deathFX, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(dieClip);
            GetComponent<EnemyController>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
          //  gameObject.SetActive(false);
        }
    }

}
