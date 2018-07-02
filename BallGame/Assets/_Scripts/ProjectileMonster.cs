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

                //Get the Screen positions of the object
                Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

                //Get the Screen position of the mouse
                Vector2 mouseOnScreen = (Vector2)Camera.main.WorldToViewportPoint(playerCollision.transform.position);

                //Get the angle between the points
                float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;

                projectiles[i].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

                projectiles[i].GetComponent<SeekingMissile>().TargetReference(playerCollision.transform);
                projectiles[i].SetActive(true);

                break;
                //projectileObject.GetComponent<SeekingMissile>().TargetReference(collision.transform);
            }
        }
    }
}