using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{
    [SerializeField] int hitsToDestroy = 3;
    [SerializeField] AnimatorOverrideController[] animatorOverrideControllers;
    //[SerializeField] float ;

    public bool canPickUpWeapons = false;

    bool on = false;

    int counter = 0;

    Animator animator;
    SkinnedMeshRenderer[] skinnedMeshRenderers;
    Renderer[] meshRenderers;
    Vector3 eulerAngels;

    bool test = true;
    bool qotorotwoto = true;
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
            if (Input.GetMouseButton(0))
            {
                if (counter != hitsToDestroy)
                {
                    //test = true;
                    eulerAngels = transform.localRotation.eulerAngles;
                    StartCoroutine(TBRotatin());
                    animator.runtimeAnimatorController = animatorOverrideControllers[1];
                }

                
            }





        }


       print(counter);
       //print(transform.localRotation.eulerAngles.x);
        

    }

    IEnumerator TBRotatin()
    {
        while (test)
        {
            print("ONNNN");

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-21, 0, 0), Time.deltaTime / 3);

            if (eulerAngels.x > 337)
            {
                StopCoroutine(TBRotatin());
                qotorotwoto = true;
                StartCoroutine(TBDefultRotation());
            }
            yield return new WaitForFixedUpdate();
        }

    } 
    IEnumerator TBDefultRotation()
    {

        while (qotorotwoto)
        {
            yield return new WaitForSeconds(1);
            test = false;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime / 9);

            if (eulerAngels.x > 356 && eulerAngels.x > 0)
            {
                StopCoroutine(TBDefultRotation());
                counter++;
                qotorotwoto = false;

            }
            yield return new WaitForFixedUpdate();
        }
     

    }



}
  
