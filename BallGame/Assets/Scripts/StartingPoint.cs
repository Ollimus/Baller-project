using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour {

	private GameObject playerObject;

	void Start ()
	{
		/*try
		{
			playerObject = GameObject.FindGameObjectWithTag("Player");

			if (playerObject == null)		
			{
				Instantiate(player, transform.position, transform.rotation);
				doesPlayerExist = true;
			}	

			else
			{
				doesPlayerExist = true;
			}
		}

		catch (Exception e)
		{			
			Debug.Log ("Error creating a player: " + e);
		}*/
	}
}
