using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touched : MonoBehaviour {
    int transpose = -4;  // transpose in semitones
    AudioSource sound;

    public int position = 0;
    public int sampleRate = 44100;
    public int frequency = 440;
    AudioClip myClip;

    // Use this for initialization
    void Start () {
		if(GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }

        AudioClip myClip = AudioClip.Create("MyTone", 44100, 1, 24500, false, true, OnAudioRead, OnAudioSetPosition);
        sampleRate = AudioSettings.outputSampleRate;
        sound = GetComponent<AudioSource>();
        sound.clip = myClip;
        
        sound.minDistance = 1;
        sound.minDistance = 1;
        sound.volume = 0.25f;
        sound.spatialBlend = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Renderer>().sharedMaterial.color != Color.white)
        {
            Color tmp = gameObject.GetComponent<Renderer>().sharedMaterial.color;
            if (tmp.r < 1.0)
            {
                tmp.r += 0.01f;
            }
            if (tmp.b < 1.0)
            {
                tmp.b += 0.01f;
            }
            if (tmp.g < 1.0)
            {
                tmp.g += 0.01f;
            }

            sound.volume = sound.volume - 0.025f;
            gameObject.GetComponent<Renderer>().sharedMaterial.color = tmp;
        }
        else if (sound.isPlaying)
        {
            sound.Stop();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Color col = collision.gameObject.GetComponent<Renderer>().sharedMaterial.color;
        gameObject.GetComponent<Renderer>().sharedMaterial.color = col;

        float note = -1; // invalid value to detect when note is pressed
        
        if(col == Color.red) note = 0;  // C
        else if (col == Color.green) note = 2;  // D
        else if (col == Color.blue) note = 4;  // E
        else if (col == Color.cyan) note = 5;  // G
        else if (col == Color.yellow) note = 7;  // A
        else if (col == Color.magenta) note = 9;  // B
        else if (col == Color.white) note = 11;  // C
        else
        {
            Debug.Log("Unknown Color");
            note = 12;
        }

        if (note >= 0)
        {
            sound.volume = 0.25f;
            sound.pitch = Mathf.Pow(2, (note + transpose) / 12.0f);
            sound.Play();

            if(sound.isPlaying) Debug.Log("Playing");

        }
    }


    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency * position / sampleRate));
            position++;
            count++;
        }
    }
    void OnAudioSetPosition(int newPosition)
    {
        position = newPosition;
    }
}
