using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Image fillWaitImage_1;
    public Image fillWaitImage_2;
    public Image fillWaitImage_3;           
    public Image fillWaitImage_4;
    public Image fillWaitImage_5;
    public Image fillWaitImage_6;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };

    private Animator anim;
    private bool canAttack = true;

    private PlayerNewmove playerMove;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerNewmove>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        //{
        //    canAttack = true;
        //}
        //else
        //{
        //    canAttack = false;
        //}
     
        CheckToFade();
        CheckInput();
    }

    void CheckInput()
    {
        if(anim.GetInteger("Atk")==0)
        {
            //playerMove.FinishedMovement = false;

            //if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            //{
            //    playerMove.FinishedMovement = true;
            //}

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
               
              //  playerMove.TargetPosition = transform.position;

                if(canAttack && fadeImages[0] != 1) //fadeImages[0] means image that at index
                {
                  
                    fadeImages[0] = 1;
                    anim.SetTrigger("Atk1");
                  
                }
            }
       
           else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
              //  playerMove.TargetPosition = transform.position;

                if (canAttack && fadeImages[1] != 1) //fadeImages[0] means image that at index
                {
                    fadeImages[1] = 1;
                    anim.SetTrigger("Atk2");
                }
            }
          else  if (Input.GetKeyDown(KeyCode.Alpha3))
            {
             //   playerMove.TargetPosition = transform.position;

                if (canAttack && fadeImages[2] != 1) //fadeImages[0] means image that at index
                {
                    fadeImages[2] = 1;
                    anim.SetTrigger("Atk3");
                }
            }
           else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
            //    playerMove.TargetPosition = transform.position;

                if ( canAttack && fadeImages[3] != 1) //fadeImages[0] means image that at index
                {
                    fadeImages[3] = 1;
                    anim.SetTrigger("Atk4");
                }
            }
         
           else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
             //   playerMove.TargetPosition = transform.position;

                if ( canAttack && fadeImages[4] != 1) //fadeImages[0] means image that at index
                {
                    fadeImages[4] = 1;
                    anim.SetTrigger("Atk5");
                }
            }
          else  if (Input.GetMouseButtonDown(1))
            {
              //  playerMove.TargetPosition = transform.position;

                if (canAttack && fadeImages[5] != 1) //fadeImages[0] means image that at index
                {
                    fadeImages[5] = 1;
                    anim.SetTrigger("Atk6");
                }
            }
           // else { anim.SetInteger("Atk", 0); }

            //if(Input.GetKeyUp(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha1))
            //{
            //    anim.SetInteger("Atk", 0);
            //}

            if(Input.GetKey(KeyCode.Space))
            {
                Vector3 targetPosition = Vector3.zero;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }

                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetPosition - transform.position), 15f * Time.deltaTime);
                     
            }
        }
       
    }
    void CheckToFade()
    {
        if(fadeImages[0] == 1)
        {
            if (FadeAndWait(fillWaitImage_1, 1.0f))
            {
                fadeImages[0] = 0;
            }
        }
        if (fadeImages[1] == 1)
        {
            if (FadeAndWait(fillWaitImage_2, 0.1f))
            {
                fadeImages[1] = 0;
            }
        }
        if (fadeImages[2] == 1)
        {
            if (FadeAndWait(fillWaitImage_3, 0.3f))
            {
                fadeImages[2] = 0;
            }
        }
        if (fadeImages[3] == 1)
        {
            if (FadeAndWait(fillWaitImage_4, 0.8f))
            {
                fadeImages[3] = 0;
            }
        }
        if (fadeImages[4] == 1)
        {
            if (FadeAndWait(fillWaitImage_5, 0.6f))
            {
                fadeImages[4] = 0;
            }
        }
        if (fadeImages[5] == 1)
        {
            if (FadeAndWait(fillWaitImage_6, 1.0f))
            {
                fadeImages[5] = 0;
            }
        }
    }

    bool FadeAndWait(Image fadeImg, float fadeTime)
    {
        bool faded = false;

        if(fadeImg == null)
        {
            return faded;
        }

        if(!fadeImg.gameObject.activeInHierarchy)
        {
            fadeImg.gameObject.SetActive(true);
            fadeImg.fillAmount = 1f;
        }
        fadeImg.fillAmount -= fadeTime * Time.deltaTime;

        if(fadeImg.fillAmount <= 0f)
        {
            fadeImg.gameObject.SetActive(false);
            faded = true;
        }
       
        return faded;
    }

}
