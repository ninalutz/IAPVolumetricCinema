using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethan_Spawner : MonoBehaviour {

    public GameObject spawnable;
    public int max_ethans;
    List<GameObject> ethans;
    
	// Use this for initialization
	void Start () {
        ethans = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % 20 == 0 && ethans.Count < max_ethans)
        {
            ethans.Add(Instantiate(spawnable, new Vector3(Random.Range(-1.5f, 1.5f), 2, Random.Range(-1.5f, 1.5f)), Quaternion.identity));
        }

        if (Time.frameCount % 20 == 0 && ethans.Count >= max_ethans)
        {
            GameObject fatal_ethan = ethans[0];
            ethans.RemoveAt(0);
            Destroy(fatal_ethan);
        }

    }
}
