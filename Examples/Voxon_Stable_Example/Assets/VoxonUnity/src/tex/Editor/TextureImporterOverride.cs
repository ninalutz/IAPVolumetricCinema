using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
class TextureImporterOverride : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;
        textureImporter.isReadable = true;
    }
}