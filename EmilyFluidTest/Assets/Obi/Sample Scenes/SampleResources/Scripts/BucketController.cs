using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour {

	public Obi.ObiEmitter emitter;
    public Obi.ObiEmitter milkEmitter;

    private bool pouring = false;
    private bool milkPouring = false;

    void Update ()
    {
		if (Input.GetKey(KeyCode.R))
        {
			emitter.KillAll();
            milkEmitter.KillAll();
		}

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (pouring)
            {
                emitter.speed = 0;
            }
            else
            {
                emitter.speed = 4;
            }
            pouring = !pouring;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (milkPouring)
            {
                milkEmitter.speed = 0;
            }
            else
            {
                milkEmitter.speed = 4;
            }
            milkPouring = !milkPouring;
        }
	}
}
