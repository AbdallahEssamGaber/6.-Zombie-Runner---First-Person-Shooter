using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{
    [SerializeField] int hitsToDestroy = 3;
    [SerializeField] AnimatorOverrideController[] animatorOverrideControllers;
    [SerializeField] float xTarget = -21f;
    [SerializeField] float eulerX = 340f;
    [SerializeField] float EnzelMaraWheda = 0.5f;
    [SerializeField] float backNormalSpeed = 10f;
    [SerializeField] float rotateSpeed = 2f;

    public bool canPickUpWeapons = false;

    bool on = false;

    int counter = 0;

    Animator animator;
    Renderer[] meshRenderers;
    Vector3 eulerAngels;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = animatorOverrideControllers[0];
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pole")
        {
            on = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pole")
        {
            on = false;
        }
    }


    void Update()
    {
        eulerAngels = transform.localRotation.eulerAngles;

       
        if (!on && counter < hitsToDestroy)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|Right TB_004") || (animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|Break Right")))

            {
                animator.runtimeAnimatorController = animatorOverrideControllers[0];

            }

        }



        if (counter >= hitsToDestroy)
        {
            canPickUpWeapons = true;
            if (counter == hitsToDestroy) animator.runtimeAnimatorController = animatorOverrideControllers[2];


            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //> 1 means Finished
            {
                meshRenderers = GetComponentsInChildren<Renderer>();
                foreach (Renderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = false;
                }

                enabled = false;

            }

      
        }




        if (on)
        {
            animator.runtimeAnimatorController = animatorOverrideControllers[1];

            if (Input.GetMouseButtonDown(0))
            {
                if (counter != hitsToDestroy)
                {
                
                    StartCoroutine(TBRotatin());

                }

                counter++;

            }





        }

        //if (test)
        //{
        //    StartCoroutine(TBRotatin());
        //    test = false;
        //}
        print(counter);
       //print(transform.localRotation.eulerAngles.x);
       

    }

    IEnumerator TBRotatin()
    {
        while (true)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xTarget, 0, 0), Time.deltaTime * rotateSpeed);

            if (eulerAngels.x < eulerX && eulerAngels.x > 0)
            {

                StopAllCoroutines();
                StartCoroutine(TBDefultRotation());
            }
            yield return new WaitForFixedUpdate();
        }
       
    } 
    IEnumerator TBDefultRotation()
    {
        yield return new WaitForSeconds(EnzelMaraWheda);
        while (true)
        {
           
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * backNormalSpeed);

            if (eulerAngels.x > 359 && eulerAngels.x > 0)
            {
               
                StopAllCoroutines();
                
            }
            yield return new WaitForFixedUpdate();
        }
     

    }



}
  
