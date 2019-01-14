using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Voxon
{
    public class TextComponent : VoxieHelper
    {

        private VoxieCaptureVolume capture_volume;
        
        point3d pr, pd, pp;

        private static System.Text.Encoding enc = System.Text.Encoding.ASCII;
        byte[] ts;
        // Use this for initialization
        void Start()
        {
            
            capture_volume = FindObjectOfType<VoxieCaptureVolume>();
            capture_volume.AddText(this);

            string Text = "";
            ts = enc.GetBytes(Text);

            pr.x = 0.1f; pd.x = 0.0f; pp.x = -0.75f;
            pr.y = 0.0f; pd.y = 0.5f; pp.y = +0.5f;
            pr.z = 0.0f; pd.z = 0.0f; pp.z = 0.0f;
        }

        public void SetString(string Text)
        {
            // Get Char Values for String
            byte[] tmp = enc.GetBytes(Text);
            ts = new byte[tmp.Length+1];
            tmp.CopyTo(ts, 0);
            // Append 0 to end string
            ts[tmp.Length] = 0;
            Debug.Log("String Out");
            for(int idx = 0; idx < ts.Length; idx++)
            {
                Debug.Log("-"+ts[idx]);
            }
        }

        public void SetLocation(point3d position)
        {
            pp = position;
        }

        public void SetCharWidth(point3d width)
        {
            pd = width;
        }

        public void SetCharHeight(point3d height)
        {
            pr = height;
        }

        /// <summary>  
        ///  Draw the drawable mesh; Uses Capture Volume's transform to determine if play space has changed
        ///  Animated meshes are set to redraw every frame while statics only redrawn on them or the volume
        ///  changing transform.
        ///  </summary>
        public void DrawMesh(ref voxie_frame_t vf, Transform voxieCameraTransform)
        {
            voxie_printalph(ref vf, ref pp, ref pr, ref pd, 0xffffff, ts);
        }

        /// <summary>  
        ///  Generates relevant transform for mesh type against capture volume transform
        ///  Passes to the Compute Shader for processing
        ///  </summary>
        private void BuildMesh(Transform voxieCameraTransform)
        {
        }

        /// <summary>  
        ///  Sets up the buffers and loads our compute shader
        ///  </summary>
        private void build_shader()
        {
        }


        /// <summary>  
        ///  Compute Shader call. Set up Kernel, define tranform values and dispatches GPU threads
        ///  Currently only sends thin batches; should see to increase this in future.
        ///  </summary>
        private void compute_transform(Matrix4x4 Transform)
        {
        }


        private void translate_triangles()
        {
        }

        private void load_textures()
        {
        }
    }
}