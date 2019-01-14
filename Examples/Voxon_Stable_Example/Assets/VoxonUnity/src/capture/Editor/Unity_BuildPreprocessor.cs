using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

[InitializeOnLoadAttribute]
public class Unity_BuildPreprocessor : IPreprocessBuild {

    public int callbackOrder {  get { return 0;  } }
	public void OnPreprocessBuild(BuildTarget target, string path)
    {
        if(!AssetDatabase.IsValidFolder("Assets/StreamingAssets"))
        {
            Debug.LogError("You should never see this; Editor Handler should have fixed this already");

            System.IO.Directory.CreateDirectory("Assets/StreamingAssets");
            InputController.Instance.SaveData(InputController.Instance.filename);
            Debug.LogWarning("Assets/StreamingAssets didn't exist. Created and Input File Saved (used loaded filename)");
        }
        else if(InputController.Instance.GetKey("Quit") == 0)
        {
            throw new BuildFailedException("Input controller requires 'Quit' to be bound (and saved)");
        }
    }
}
