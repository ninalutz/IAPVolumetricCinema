using System; //keep this
using System.Collections;//keep this
using System.Collections.Generic;//keep this
using UnityEngine;//keep this


public class KeyboardInput : MonoBehaviour {//keep this

	private Renderer rend;
	public GameObject player;
	public float speed;
	public GameObject captureVolume;

	VoxieCaptureVolume vcv;//keep this
	Vector3 aspect_ratio;//keep this
	// Use this for initialization
	void Start () {//keep this
		vcv = FindObjectOfType<VoxieCaptureVolume>();//keep this
		rend = player.GetComponent<Renderer>();
	}//keep this

	// Update is called once per frame
	void Update () {//keep this
		if(vcv == null){//keep this
			vcv = FindObjectOfType<VoxieCaptureVolume>();//keep this
			if(vcv == null)//keep this
			{//keep this
				Debug.LogError("Capture Volume not found!");//you don't have to keep this, but it doesn't make much sense to change it?
			}//keep this
		}//keep this

		aspect_ratio = vcv.GetAspectRatio();//keep this


		//START AUBREY'S KEYBINDINGS (All OF THIS IS SAFE TO CHANGE)
		//these all manage movement
		if (Voxon.Input.GetKey ("MoveRight")) {
			player.transform.Translate(-Vector3.right * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveLeft")) {
			player.transform.Translate(-Vector3.left * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveForward")) {
			player.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveBack")) {
			player.transform.Translate(-Vector3.back * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveUp")) {
			player.transform.Translate(-Vector3.up * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveDown")) {
			player.transform.Translate(-Vector3.down * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("TiltUp")) {
			Debug.Log ("Rotating up now");
			captureVolume.transform.Rotate(Vector3.up * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("TiltDown")) {
			captureVolume.transform.Rotate(Vector3.down * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("TiltRight")) {
			captureVolume.transform.Rotate(Vector3.right * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("TiltLeft")) {
			captureVolume.transform.Rotate(Vector3.left * speed * Time.deltaTime);
		}
		if (Voxon.Input.GetKey ("MoveFaster")) {
			speed *= 1.1f;
		}
		if (Voxon.Input.GetKey ("MoveSlower")) {
			speed %= 1.1f;
		}
		//these also manage movement, but they do so with the mouse instead of keyboad input
		//turns the player object red.  
		if (Voxon.Input.GetKey ("ChangeColor")) {
			rend.material.SetColor("_Color", Color.red);			
		}

	}
}

