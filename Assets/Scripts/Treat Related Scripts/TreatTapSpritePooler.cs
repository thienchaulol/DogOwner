using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreatTapSpritePooler : MonoBehaviour {

	public GameObject pooledObject;	//gameobject used to pool objects

	public int pooledAmount;	//initial number of pooled objects

	List<GameObject> pooledObjects;	//list of pooled objects

	void Start () {
		pooledObjects = new List<GameObject> ();	//allocate new list of game objects

		for (int i = 0; i < pooledAmount; i++) {	//create initial number of pooled objects
			GameObject obj = (GameObject)Instantiate (pooledObject);	//instantiate(create) object
			obj.SetActive (false);	//set objects to false until they are needed
			pooledObjects.Add (obj);	//add onto list
		}
	}

	public GameObject GetPooledObject(){	//called in other functions to acquire UNACTIVE pooled object
		for(int i = 0; i < pooledObjects.Count; i++){	//iterate through list of objects
			if (!pooledObjects [i].activeInHierarchy) {	//if the object is not being used
				return pooledObjects [i];				//return that object
			}
		}

		//if all objects in the list are being used, create a new object
		GameObject obj = (GameObject)Instantiate (pooledObject);	//create new object
		obj.SetActive (false);	//set it to false
		pooledObjects.Add (obj);	//add it to the list for future use
		return obj;	//return this new object to be used
	}
}
