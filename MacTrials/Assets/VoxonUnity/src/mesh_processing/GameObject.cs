using UnityEngine;
using System.Collections.Generic;

namespace Voxon
{
    public class GameObject : MonoBehaviour
    {

        // Lifespan Variables
        private const float MAX_LIFE_SPAN = 300.0f;
        private bool can_degen = true;
        private bool degen = false;
        private float life_span = MAX_LIFE_SPAN;

        private VoxieCaptureVolume capture_volume;

        // private List<T> SkinnedMeshRenderer;
        // private List<T> MeshFilterer;

        public void Start()
        {
            // Debug.Log("New Game Object:" + gameObject.name);
            // We will use this to add our components to draw list
            capture_volume = FindObjectOfType<VoxieCaptureVolume>();

            // VoxieDraw objects cannot degenerate
            if (gameObject.tag == "VoxieDraw")
            {
                can_degen = false;
            }

            // We always want animations to be computed (as otherwise they would only appear when a camera was active)
            if (gameObject.GetComponent<Animator>())
            {
                Animator Anima = transform.root.gameObject.GetComponent<Animator>();
                if(Anima)
                {
                    Anima.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                }

                var Animators = transform.root.gameObject.GetComponentsInChildren<Animator>();
                foreach(var A in Animators)
                {
                    A.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                }
            }

            foreach (SkinnedMeshRenderer child in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if(child.gameObject.tag == "VoxieHide")
                {
                    continue;
                }
                child.gameObject.AddComponent<Component>();
                capture_volume.AddComponent(child.gameObject.GetComponent<Component>());
            }

            foreach (MeshRenderer child in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                if (child.gameObject.tag == "VoxieHide")
                {
                    continue;
                }
                child.gameObject.AddComponent<Component>();
                capture_volume.AddComponent(child.gameObject.GetComponent<Component>());
            }


        }

        void OnDestroy()
        {
            foreach (SkinnedMeshRenderer child in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                Destroy(child.gameObject.GetComponent<Component>());
            }

            foreach (MeshRenderer child in gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                Destroy(child.gameObject.GetComponent<Component>());
            }
        }

        /// <summary>  
        ///  To reduce load on VX1, we want Drawables to be removed a few seconds off screen.
        ///  This won't impact the actual model, just stop it being computed for drawing until it reenters the scene
        ///  </summary>
        private void Update()
        {
            if (life_span <= 0)
            {
                Debug.Log("Destroying " + gameObject.name + " due to degen (out of collider for too long)");
                Destroy(this);
            }
            else if (can_degen && degen)
            {
                life_span--;
            }

        }

        /// <summary>  
        ///  Set Degen on the object; triggered true when drawable leaves capture volume, false when entering
        ///  </summary>
        public void Set_Degen(bool start_degen)
        {
            if (!start_degen)
            {
                life_span = MAX_LIFE_SPAN;
            }
            degen = start_degen;
        }
    }
}
