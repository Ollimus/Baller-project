using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileMonster : MonoBehaviour
{

    public GameObject projectile;
    private int projectileAmount = 5;
    public float fireRate = 1f;

    private Collider2D playerCollision;
    private List<GameObject> projectiles = new List<GameObject>();
    private float cooldown;

    //In 0 rotation, the angle of the rocket is pointing left corner, 255 is the rotation it requires for to be aiming up.
    //This way, it points the same way than the game object that it is attached to.
    private readonly int spriteAngle = 225;

    private void Start()
    {
        DynamicObjects dynamic = DynamicObjects.DynamicObjectInstance;
        Transform objectPooler;

        if (dynamic == null)
        {
            Debug.Log("Error finding location for dynamic objects.");
            return;
        }

        else
        {
            objectPooler = dynamic.CreateNewFolder(projectile.name);
        }

        for (int i = 0; i < projectileAmount; i++)
        {
            if (objectPooler.childCount == projectileAmount)
            {
                foreach (Transform childTransform in objectPooler)
                {
                    projectiles.Add(childTransform.gameObject);
                }
            }

            else
            {
                GameObject projectileObject = Instantiate(projectile);

                projectileObject.transform.parent = objectPooler.transform;
                projectileObject.SetActive(false);

                projectiles.Add(projectileObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerCollision = collision;

        if (cooldown < Time.time)
            Invoke("Fire", 0);  //Warning; Adding delay to fire invoke will cause it to fire multiple times.
    }

    private void Fire()
    {
        if (playerCollision == null)
            return;

        cooldown = Time.time + fireRate;

        for (int i = 0; i < projectileAmount; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                if (playerCollision == null)
                {
                    Debug.LogError("Cannot target player.");
                    return;
                }

                projectiles[i].transform.position = transform.position;

                Vector3 vectorToTarget = playerCollision.transform.position - transform.position;

                //Calculate the angle of target in radians and multiply it to degrees. 
                //As explained, the spriteAngle is the correction to make the missile point player.
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - spriteAngle;

                //Rotate missile correctly pointing to player.
                Quaternion correctedRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                projectiles[i].transform.rotation = correctedRotation;

                //Get the component of missile, give it player transform and call the function.
                projectiles[i].GetComponent<Missile>().TargetReference(playerCollision.transform);
                projectiles[i].SetActive(true);

                break;
            }
        }
    }
}