using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {
    Vector3 min_limits = new Vector3(-4.5f, -1, -4.5f);
    Vector3 max_limits = new Vector3(4.5f, -1, 4.5f);

    public Mesh[] Meshes = new Mesh[4];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Mouse_Position mp = Voxon.Input.GetMousePos();

        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + mp.x, min_limits.x, max_limits.x),
            Mathf.Clamp(gameObject.transform.position.y + mp.z, min_limits.y, max_limits.y),
            Mathf.Clamp(gameObject.transform.position.z - mp.y, min_limits.z, max_limits.z));

        if(Voxon.Input.GetMouseButtonDown("Left"))
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        }

        if (Voxon.Input.GetMouseButtonDown("Right"))
        {
            transform.localScale = new Vector3(Random.Range(0.25f, 3f), Random.Range(0.25f, 3f), Random.Range(0.25f, 3f));
        }

    }
}
