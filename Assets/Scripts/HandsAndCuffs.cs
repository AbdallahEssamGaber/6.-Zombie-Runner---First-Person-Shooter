using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{
    [SerializeField] int hitsToDestroy = 3;
    [SerializeField] AnimatorOverrideController[] animatorOverrideControllers;
    [SerializeField] bool printEuler = false;
    [SerializeField] float xTarget = -21f;
    [SerializeField] float eulerX = 340f;
    [SerializeField] float xNormal = 0;
    [SerializeField] float impactEuler = 330f;
    [SerializeField] float eulerXNorm = 359f;
    [SerializeField] float EnzelMaraWheda = 0.5f;
    [SerializeField] float backNormalSpeed = 10f;
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] float radius = 0.9f;
    [SerializeField] Transform cuffsEmptyObj;

    [System.NonSerialized]
    public bool canPickUpWeapons = false; 

    bool on = false;
    bool count = false;
    bool onCollider = false;
    bool finised = false;
    bool enter = true;

    int counter = 0;

    Animator animator;
    Renderer[] meshRenderers;
    Vector3 eulerAngels;



    int idle = 0;
    int tBRev = 1;
    int tB = 2;
    int tBSS = 3;
    int impact = 4;
    int handBreak = 5;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = animatorOverrideControllers[idle];
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pole")
        {
            on = true;
        }
        if (other.gameObject.tag == "onPole")
        {
            onCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pole")
        {
            on = false;
        }
        if (other.gameObject.tag == "onPole")
        {
            onCollider = false;
            count = false;
        }

    }


    void Update()
    {
        eulerAngels = transform.localRotation.eulerAngles;
        if (printEuler) print(transform.localRotation.eulerAngles.x);

        Collider[] hitColliders = Physics.OverlapSphere(cuffsEmptyObj.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "onPole") count = true;

        }


        if (!on && counter < hitsToDestroy)
        {
            print("sdfsd");

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|Right TB_004") || animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|Break Right") || animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|TB SS") || animator.GetCurrentAnimatorStateInfo(0).IsName("Right Hand|Hit impact right"))

            {
                
                animator.runtimeAnimatorController = animatorOverrideControllers[tBRev];
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) animator.runtimeAnimatorController = animatorOverrideControllers[idle];

            }

        }


        if (counter >= hitsToDestroy)
        {
            canPickUpWeapons = true;



            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //> 1 means Finished
            {

                meshRenderers = GetComponentsInChildren<Renderer>();
                foreach (Renderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = false;
                }

                enabled = false;

            }
            return;


        }


        if (on && counter <= hitsToDestroy)
        {


            if (animator.runtimeAnimatorController == animatorOverrideControllers[impact] && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.runtimeAnimatorController = animatorOverrideControllers[tBSS];
            }
          
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.runtimeAnimatorController != animatorOverrideControllers[idle] && animator.runtimeAnimatorController != animatorOverrideControllers[tBSS]) //> 1 means Finished
            {
                animator.runtimeAnimatorController = animatorOverrideControllers[tB];
            }
            else if (animator.runtimeAnimatorController == animatorOverrideControllers[idle])
            {
                animator.runtimeAnimatorController = animatorOverrideControllers[tB];

            }



            if (Input.GetMouseButtonDown(0))
            {
                if (counter != hitsToDestroy)
                {
                    StartCoroutine(TBRotatin());

                }


            }





        }


        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();

            StartCoroutine(TBDefultRotation());
        }


        print(animator.runtimeAnimatorController);

    }



    IEnumerator TBRotatin()
    {
        animator.runtimeAnimatorController = animatorOverrideControllers[tBSS];

        while (true)
        {

            finised = false;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xTarget, 0, 0), Time.deltaTime * rotateSpeed);

            if (eulerAngels.x < eulerX && eulerAngels.x > 0)
            {
                finised = true;
                enter = true;
                StopAllCoroutines();
            }
            yield return new WaitForFixedUpdate();
        }

    }
    IEnumerator TBDefultRotation()
    {
        yield return new WaitForSeconds(EnzelMaraWheda);
        while (true)
        {

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(xNormal, 0, 0), Time.deltaTime * backNormalSpeed);
            if (eulerAngels.x > impactEuler && eulerAngels.x > 0 && enter && count)
            {
                animator.runtimeAnimatorController = animatorOverrideControllers[impact];
                enter = false;

            }
            if (eulerAngels.x > eulerXNorm && eulerAngels.x > 0)
            {

                if (count && onCollider && finised)
                {
                    counter++;
                }
                if (counter == hitsToDestroy)
                {
                    animator.runtimeAnimatorController = animatorOverrideControllers[handBreak];
                }
                StopAllCoroutines();

            }
            yield return new WaitForFixedUpdate();
        }


    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cuffsEmptyObj.position, radius);

    }

}