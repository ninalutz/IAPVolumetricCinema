using System;
using System.Collections.Generic;
using UnityEngine;


public class Voxon_Movement : MonoBehaviour {

    VoxieCaptureVolume vcv;
    Vector3 aspect_ratio;
    // Use this for initialization
    void Start () {
        vcv = FindObjectOfType<VoxieCaptureVolume>();
    }
	
	// Update is called once per frame
	void Update () {
        if(vcv == null)
        {
            vcv = FindObjectOfType<VoxieCaptureVolume>();
            if(vcv == null)
            {
                Debug.LogError("Capture Volume not found!");
            }
        }

        aspect_ratio = vcv.GetAspectRatio();
        if (Voxon.Input.GetKey("Grow"))
        {
            vcv.Scale += 1;
            vcv.Scale = Math.Min(vcv.Scale, 1000);
            transform.localScale = aspect_ratio * vcv.Scale;
            Debug.Log("Scale: " + vcv.Scale);
        }

        if (Voxon.Input.GetKey("Shrink"))
        {
            vcv.Scale -= 1;
            vcv.Scale = Math.Max(vcv.Scale, 1);
            
            transform.localScale = aspect_ratio * vcv.Scale;
            Debug.Log("Scale: " + vcv.Scale);
        }

        if (Voxon.Input.GetKey("Up"))
        {
            transform.Translate(transform.up * Time.deltaTime, Space.World);
        }

        if (Voxon.Input.GetKey("Down"))
        {
            transform.Translate(-1 * transform.up * Time.deltaTime, Space.World);
        }

        if (Voxon.Input.GetKey("Forward"))
        {
            transform.Translate(-1 * transform.forward * Time.deltaTime, Space.World);
        }
        if (Voxon.Input.GetKey("Backward"))
        {
            transform.Translate(transform.forward * Time.deltaTime, Space.World);
        }
        if (Voxon.Input.GetKey("Left"))
        {
            transform.Translate(-1 * transform.right * Time.deltaTime, Space.World);
        }
        if (Voxon.Input.GetKey("Right"))
        {
            transform.Translate(transform.right * Time.deltaTime, Space.World);
        }
    }
}
