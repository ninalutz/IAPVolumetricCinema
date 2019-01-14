using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public Material[] Mats;
	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 25 == 0)
        {
            int type = Random.Range(0, 5);

            while (type == 4)
            {
                type = Random.Range(0, 5);
            }
            
            GameObject H = GameObject.CreatePrimitive((PrimitiveType)type);
            
            if (H.GetComponent<Renderer>() == null)
            {
                H.AddComponent<Renderer>();
            }
            H.GetComponent<Renderer>().sharedMaterial = Mats[Random.Range(0, Mats.Length-1)];

            H.transform.SetPositionAndRotation(new Vector3(Random.Range(-3.5f,3.5f), 7.0f, Random.Range(-3.5f, 3.5f)), new Quaternion (Random.Range(-10f, 10f), Random.Range(-10f, 3.5f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
			Rigidbody Bod = H.AddComponent<Rigidbody>();
			Bod.AddForce(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, -100.0f), Random.Range(-10.0f, 10.0f)));
		}
	}
}
