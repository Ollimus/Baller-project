using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjects : MonoBehaviour {

	public static DynamicObjects DynamicObjectInstance;

    private void Start()
    {
        if (DynamicObjectInstance == null)
            DynamicObjectInstance = this;

        else
            Destroy(gameObject);
    }

    /*
     *Creates a new gameobject "folder" under "Dynamic" -gameobject. If it already exists, return the transform. 
    */
    public Transform CreateNewFolder(string name)
    {
        Transform doesObjextExist = gameObject.transform.Find(name);

        if (doesObjextExist != null)
            return doesObjextExist;

        GameObject folder = new GameObject(name);   //Create a new "folder" with the given name. Usually you pass prefab.name as folder name.
        folder.transform.parent = gameObject.transform; //Set dynamic folder as parent folder.

        return folder.transform;
    }
}
