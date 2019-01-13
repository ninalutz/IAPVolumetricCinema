using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace Voxon
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Particle : VoxieHelper {

        private int draw_flags = 2 | 1 << 3; // 2 - Fill, and Draw from Texture buffer
        
        private poltex_t[] _plane;
        private ParticleSystem ps;

        ParticleSystem.Particle[] m_Particles;
        int n_Particles;
        Matrix4x4 Matrix;


        // Use this for initialization
        void Start() {
            ps = GetComponent<ParticleSystem>();
            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            var main = ps.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }

        // Update is called once per frame
        void Update () {
	    }

        public void Draw(ref voxie_frame_t vf, Transform voxieCameraTransform)
        {   
            if (m_Particles != null)
            {
                n_Particles = ps.GetParticles(m_Particles);

                Vector3 l;

                foreach (var par in m_Particles)
                {   
                    float size = par.GetCurrentSize(ps) / 50;

                    l = Matrix * par.position;
                    voxie_drawbox(ref vf, l.x - size, -l.y, l.z - size, l.x + size, -l.y, l.z + size, 2, (par.GetCurrentColor(ps)).toInt());
                }
            }
        }

        public void BuildMesh(Transform voxieCameraTransform)
        {
            try
            {
                // Set Model View transform
                Matrix = transform.localToWorldMatrix;
                Matrix = voxieCameraTransform.worldToLocalMatrix * Matrix;
                Matrix = Matrix4x4.Scale(new Vector3(2.0f, 0.8f, 2.0f)) * Matrix;
            }
            catch (Exception E)
            {
                UnityEngine.Debug.LogError("Error while Building Mesh " + gameObject.name + "\n" + E.Message);
            }
        }
    }
}