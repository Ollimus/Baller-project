using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 0.8f;

    private Vector3 playerLocation;

    //Since parameters cannot be passed through invoke, connect the TargetReference parameter into gameobject variable.
    public void TargetReference(Transform player)
    {
        playerLocation = player.position;

        //Call invoke TargetPlayer every frame.
        InvokeRepeating("TargetPlayer", Time.deltaTime, Time.deltaTime);
    }

    //Missile moves towards player position.
    private void TargetPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, missileSpeed * Time.deltaTime);

        //If position has been reached, commence destroying.
        if (transform.position == playerLocation)
        {
            DestroyGameObject();
        }
    }

    //If missile comes in touch with player, explode on contact.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            DestroyGameObject();
    }

    //Cancels the player targeting function to make sure nothing funky happens and starts the coroutine of destroy.
    private void DestroyGameObject()
    {
        CancelInvoke("TargetPlayer");
        StartCoroutine("Destroy");
    }


    private IEnumerator Destroy()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("Explosion");

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        //Reset animation to start from the beginning.
        //Since the object is not created again, but disabled instead, the animation loop would start at incorrect position.
        anim.Rebind();

        gameObject.SetActive(false);    // Deactivate object to return it back to the pool.
    }


}
