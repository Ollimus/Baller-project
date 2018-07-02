using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingMissile : MonoBehaviour
{
    public float missileSpeed = 0.8f;
    Vector3 playerLocation;

    private void TargetPlayer()
    {

        if (playerLocation != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLocation, missileSpeed * Time.deltaTime);

            if (transform.position == playerLocation)
            {
                CancelInvoke("TargetPlayer");
                StartCoroutine("Destroy");
            }
            
        }
    }

    private IEnumerator Destroy()
    {

        Animator anim = GetComponent<Animator>();

        anim.Play("Explosion");

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        anim.Rebind();

        // when gameobjective gets deactivated, it stops the couritne of killzone.

        gameObject.SetActive(false);
    }

    public void TargetReference(Transform player)
    {
        playerLocation = player.position;

        InvokeRepeating("TargetPlayer", Time.deltaTime, Time.deltaTime);

        //InvokeRepeating(TargetPlayer(player.position), 0, Time.time);   
    }
}
