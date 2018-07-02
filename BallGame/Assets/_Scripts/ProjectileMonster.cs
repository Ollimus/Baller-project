using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMonster : MonoBehaviour {

    public GameObject projectile;
    public int projectileAmount = 20;
    public float fireRate = 0.5f;

    private Collider2D playerCollision;
    private List<GameObject> projectiles = new List<GameObject>();

    private void Start()
    {

        for (int i = 0; i < projectileAmount; i++)
        {
            GameObject projectileObject = Instantiate(projectile);
            projectileObject.SetActive(false);
            projectiles.Add(projectileObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerCollision = collision;
        Invoke("Fire", fireRate);
    }

    private void Fire()
    {
        if (playerCollision == null)
            return;

        for (int i = 0; i < projectiles.Count; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                projectiles[i].transform.position = transform.position;
                projectiles[i].transform.rotation = transform.rotation;
                projectiles[i].GetComponent<SeekingMissile>().TargetReference(playerCollision.transform);
                projectiles[i].SetActive(true);

                break;
                //projectileObject.GetComponent<SeekingMissile>().TargetReference(collision.transform);
            }
        }
    }
}