using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEngine.Profiling;


public enum PlayerArrangement
{
    OneSideOfVX1 = 0,
    AroundVX1 = 1
};
/// <summary>  
///  VoxieCaptureVolume is the window through which objects will be reflected on the VX1. 
///  </summary>
///  <remarks>
///  This singleton gameobject handles connecting to the VX1, the list of drawable objects within the scene,
///  Game controllers, and handles adding / removing drawable objects.
///  Drawables are detected via collision, both as an ongoing check per update, and via collision
/// </remarks>  
[RequireComponent(typeof(Mesh))]
public sealed class VoxieCaptureVolume : VoxieHelper
{
    bool VX1_Shutdown = false;
    // Singleton Controls
    private static bool first_load = true;
    private static GameObject instance = null;
    private static readonly object padlock = new object();
    private static Vector3 position;
    private static Quaternion rotation;

    // Controller Constants
    const int MAXCONTROLLERS = 4;

    [Tooltip("Scale capture volume (Higher values will cause content to appear smaller)")]
    [Range(1, 100)]
    public float Scale;

    [Tooltip("Will show capture volume of VX1 while emulating.\nWARNING: will cause animated meshes to 'twitch'")]
    public bool guidelines = false;

    [Tooltip("How players are arranged around VX1")]
    public PlayerArrangement PlayerStandingPosition = PlayerArrangement.OneSideOfVX1;

    private Vector3 AspectRatio;
    private Collider CollidingVolume;

    private voxie_wind_t vw = new voxie_wind_t();
    private voxie_frame_t vf = new voxie_frame_t();
    private voxie_inputs_t ins = new voxie_inputs_t();

    private Color32[] colorArray;

    private Vector3 currentPosition;
    // private Vector3 localScale;

    private List<Voxon.Component> drawable_components = new List<Voxon.Component>();
    private List<Voxon.TextComponent> text_components = new List<Voxon.TextComponent>();
    private List<Voxon.Particle> particle_components = new List<Voxon.Particle>();

    // Controls
    private struct xbox_input
    {
        public voxie_xbox_t input;
        public voxie_xbox_t offset;
        public voxie_xbox_t last_frame;
    }

    private xbox_input[] controllers = new xbox_input[MAXCONTROLLERS];

    private void Awake()
    {
        if (instance)
        {
            if (instance != gameObject)
            {
                position = transform.position;
                rotation = transform.rotation;
                Debug.Log("Destroying Extra instance");
                Destroy(gameObject);
                return;
            }
            drawable_components.Clear();
            text_components.Clear();
            Load();
            return;
        }
        else
        {
            instance = gameObject;
        }
    }
    // Use this for initialization
    void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(this);

        Application.targetFrameRate = 30;

        // Set up our collision
        CollidingVolume = gameObject.GetComponent<Collider>();
        if (!CollidingVolume)
        {
            Debug.Log("VoxieCaptureVolume: No collider attached to GameObject. Generating Collider");
            gameObject.AddComponent<Collider>();
            CollidingVolume = gameObject.GetComponent<Collider>();
        }

        foreach (var ps in FindObjectsOfType<ParticleSystem>())
        {
            var par = ps.gameObject.AddComponent<Voxon.Particle>();
            AddParticle(par);
        }

        voxie_loadini_int(ref vw); // Load settings from our ini files.

        voxie_init(ref vw); //Start video and audio.

        voxie_setview(ref vf, -vw.aspx, -vw.aspy, -vw.aspz, vw.aspx, vw.aspy, vw.aspz); // Set out view dimentions

        // Start VX1 Loop
        StartCoroutine(VoxieUpdateLoop());

        Load();

    }

    private void Load()
    {
        // Anything that's required to be drawn?
        try
        {
            foreach (GameObject required_draw_object in GameObject.FindGameObjectsWithTag("VoxieDraw"))
            {
                required_draw_object.AddComponent<Voxon.GameObject>();
            }
        }
        catch
        {
            Debug.LogWarning("Tag 'VoxieDraw' does not exist");
        }


        // Are there any new meshes to be added?
        OverlapCheck();

        // Set up controls
        for (int idx = 0; idx < MAXCONTROLLERS; ++idx)
        {
            controllers[idx].input = new voxie_xbox_t();
            controllers[idx].offset = new voxie_xbox_t();
            voxie_xbox_read(idx, ref controllers[idx].offset);
        }
    }

    /// <summary>  
    ///  This is our primary VX1 loop; we detach to ensure it will continue without blocking
    ///  </summary>
    public IEnumerator VoxieUpdateLoop()
    {
        // Base loop; provide ins to collect input for the frame
        while (voxie_breath(ref ins) == 0)
        {
            try
            {
                Profiler.BeginSample("Frame Start");
                voxie_frame_start(ref vf);
                Profiler.EndSample();

                Profiler.BeginSample("Update Aspect Ratio");
                UpdateAspectRatio();
                Profiler.EndSample();

                //draw wireframe box (provides destinct borders) but causes animated meshes to twitch
                Profiler.BeginSample("Guidelines");
                if (guidelines)
                {
                    voxie_drawbox(ref vf, -vw.aspx + 1e-3f, -vw.aspy + 1e-3f, -vw.aspz, +vw.aspx - 1e-3f, +vw.aspy - 1e-3f, +vw.aspz, 1, 0xffffff);
                }
                Profiler.EndSample();

                Profiler.BeginSample("Remove Undrawable");
                // Clear out any destroyed drawables (note: this is the drawable component not gameobject itself)
                drawable_components.RemoveAll(item => item == null);
                particle_components.RemoveAll(item => item == null);
                text_components.RemoveAll(item => item == null);

                Profiler.EndSample();

                Profiler.BeginSample("Draw Components");
                foreach (Voxon.Component comp in drawable_components)
                {
                    comp.DrawMesh(ref vf, gameObject.transform);
                }
                Profiler.EndSample();

                Profiler.BeginSample("Draw Text");
                foreach (Voxon.TextComponent text in text_components)
                {
                    text.DrawMesh(ref vf, gameObject.transform);
                }
                Profiler.EndSample();

                Profiler.BeginSample("Draw Particles");
                foreach (Voxon.Particle par in particle_components)
                {
                    par.Draw(ref vf, gameObject.transform);
                }
                Profiler.EndSample();

                // hasChanged warrents an update for all included meshes; after an update reset our changed status
                transform.hasChanged = false;

                Profiler.BeginSample("End Frame");
                voxie_frame_end();
                Profiler.EndSample();

                Profiler.BeginSample("Get VW");
                voxie_getvw(ref vw);
                Profiler.EndSample();
            }
            catch (Exception E)
            {
                Debug.LogError(E.Message);
                ShutdownVX1();
                break;
            }
            // Ensure we keep to a sensible frame rate
            yield return new WaitForEndOfFrame();
        }

        // Debug.Log("Voxie Quit. Code: " + voxie_breath(ref ins));

        Application.Quit();

    }

    public void Unload()
    {
        Debug.Log("Unloading Old Scene");
        drawable_components.Clear();
        text_components.Clear();
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Application Quitting");
        // Ensure All VX1 processes have finished before stopping program
        if (!VX1_Shutdown)
        {
            ShutdownVX1();
        }
    }

    public void ShutdownVX1()
    {
        Debug.Log("Shutting down VX1");
        VX1_Shutdown = true;
#if UNITY_EDITOR
        // Ensure All VX1 processes have finished before stopping program
        voxie_quitloop();
        voxie_frame_end();
        voxie_getvw(ref vw);
        voxie_breath(ref ins);
        voxie_uninit_int();
        UnityEditor.EditorApplication.isPlaying = false;
#else
            voxie_quitloop();
			voxie_frame_end(); 
			voxie_getvw(ref vw);  
			voxie_breath(ref ins);
            voxie_uninit_int(0);
#endif

        StopAllCoroutines();
        Application.Quit();
    }

    public void Update()
    {
        Profiler.BeginSample("Quit Check");
        if (Voxon.Input.GetButtonDown("Quit") || Voxon.Input.GetKeyDown("Quit") || InputController.Instance.GetKey("Quit") == 0)
        {
            Debug.Log("Quit via button");
            ShutdownVX1();
        }
        Profiler.EndSample();

        Profiler.BeginSample("Get Input");
        // Update our controller input details
        for (int idx = 0; idx < MAXCONTROLLERS; ++idx)
        {
            controllers[idx].last_frame = controllers[idx].input;
            voxie_xbox_read(idx, ref controllers[idx].input);
        }
        Profiler.EndSample();

        Profiler.BeginSample("Overlap Check");
        // Check if any new items entered frame
        OverlapCheck();
        Profiler.EndSample();

    }

    public void AddComponent(Voxon.Component comp)
    {
        if (!drawable_components.Contains(comp))
        {
            drawable_components.Add(comp);
        }
    }

    public void AddText(Voxon.TextComponent text)
    {
        if (!text_components.Contains(text))
        {
            text_components.Add(text);
        }
    }

    public void AddParticle(Voxon.Particle part)
    {
        if (!particle_components.Contains(part))
        {
            particle_components.Add(part);
            part.BuildMesh(transform);
        }
    }

    public void AddParticle(ParticleSystem ps)
    {
        var par = ps.gameObject.AddComponent<Voxon.Particle>();
        AddParticle(par);
    }

    /// <summary>  
    ///  Check for colliders within the Capture volume. Required for any objects spawned within the capture volume (such as projectiles, or just loaded objects)
    ///  </summary>
    void OverlapCheck()
    {

        Collider[] colliders;

        if ((colliders = Physics.OverlapBox(transform.position, transform.localScale, transform.rotation)).Length > 0) //Presuming the object you are testing also has a collider 0 otherwise
        {
            foreach (var collider in colliders)
            {
                var go = collider.transform.root.gameObject; //This is the game object you collided with

                if (go.gameObject.tag == "VoxieHide" || go.GetComponent<Voxon.GameObject>() || !go.activeInHierarchy)
                {
                    continue;
                }
                else
                {
                    go.AddComponent<Voxon.GameObject>();
                }

            }
        }
    }

    // Collision event triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Voxon.GameObject>())
        {
            other.gameObject.GetComponent<Voxon.GameObject>().Set_Degen(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Voxon.GameObject>())
        {
            other.gameObject.GetComponent<Voxon.GameObject>().Set_Degen(true);
        }
    }

    // Input Functions (handle Controller IO)
    public int GetButtons(int player)
    {
        return controllers[player].input.but;
    }

    public int GetOldButtons(int player)
    {
        return controllers[player].last_frame.but;
    }

    public float GetAxis(int axis, int player)
    {
        if (axis == 1)
        {
            return Convert.ToSingle(controllers[player].input.lt) / 0x7FFF;
        }
        else if (axis == 2)
        {
            return Convert.ToSingle(controllers[player].input.rt) / 0x7FFF;
        }
        else if (PlayerStandingPosition == PlayerArrangement.OneSideOfVX1)
        {
            switch (axis)
            {
                case 3: // LeftStickX
                    return (Convert.ToSingle(controllers[player].input.tx0) / 0x7FFF);
                case 4: // LeftStickY
                    return Convert.ToSingle(controllers[player].input.ty0) / 0x7FFF;
                case 5: // RightStickX
                    return (Convert.ToSingle(controllers[player].input.tx1) / 0x7FFF);
                case 6: // RightStickY
                    return Convert.ToSingle(controllers[player].input.ty1) / 0x7FFF;
                default:
                    return 0;
            }
        }
        else
        {
            switch (axis)
            {
                case 3: // LeftStickX
                    switch (player)
                    {
                        case 0:
                            return (Convert.ToSingle(controllers[player].input.tx0) / 0x7FFF);
                        case 1:
                            return -1 * (Convert.ToSingle(controllers[player].input.tx0) / 0x7FFF);
                        case 2:
                            return (Convert.ToSingle(controllers[player].input.ty0) / 0x7FFF);
                        case 3:
                            return -1 * (Convert.ToSingle(controllers[player].input.ty0) / 0x7FFF);
                        default:
                            break;
                    }
                    break;
                case 4: // LeftStickY
                    switch (player)
                    {
                        case 0:
                            return (Convert.ToSingle(controllers[player].input.ty0) / 0x7FFF);
                        case 1:
                            return -1 * (Convert.ToSingle(controllers[player].input.ty0) / 0x7FFF);
                        case 2:
                            return (Convert.ToSingle(controllers[player].input.tx0) / 0x7FFF);
                        case 3:
                            return -1 * (Convert.ToSingle(controllers[player].input.tx0) / 0x7FFF);
                        default:
                            break;
                    }
                    break;
                case 5: // RightStickX
                    switch (player)
                    {
                        case 0:
                            return -1 * (Convert.ToSingle(controllers[player].input.tx1) / 0x7FFF);
                        case 1:
                            return (Convert.ToSingle(controllers[player].input.tx1) / 0x7FFF);
                        case 2:
                            return -1 * (Convert.ToSingle(controllers[player].input.ty1) / 0x7FFF);
                        case 3:
                            return (Convert.ToSingle(controllers[player].input.ty1) / 0x7FFF);
                        default:
                            break;
                    }
                    break;
                case 6: // RightStickY
                    switch (player)
                    {
                        case 0:
                            return (Convert.ToSingle(controllers[player].input.ty1) / 0x7FFF);
                        case 1:
                            return -1 * (Convert.ToSingle(controllers[player].input.ty1) / 0x7FFF);
                        case 2:
                            return (Convert.ToSingle(controllers[player].input.tx1) / 0x7FFF);
                        case 3:
                            return -1 * (Convert.ToSingle(controllers[player].input.tx1) / 0x7FFF);
                        default:
                            break;
                    }
                    break;
                default:
                    return 0;
            }
            return 0;
        }
    }

    public voxie_inputs_t GetMouse()
    {
        return ins;
    }

    public void SetMouseSeen(int but)
    {
        ins.obstat = ins.obstat | but;
    }


    public Vector3 GetAspectRatio()
    {
        return new Vector3(vw.aspx, vw.aspz, vw.aspy);
    }

    public void SetAspectRatio(Vector3 NewRation)
    {
        vw.aspx = NewRation.x;
        vw.aspy = NewRation.y;
        vw.aspz = NewRation.z;
    }

    private void UpdateAspectRatio()
    {
        AspectRatio = new Vector3(vw.aspx, vw.aspz, vw.aspy);
        transform.localScale = AspectRatio * Scale;
    }

    public void SetPosition(Vector3 new_position)
    {
        transform.SetPositionAndRotation(new_position, transform.rotation);
    }

    public void SetScale(float new_scale)
    {
        if(Scale > 0)
        {
            Scale = new_scale;
        }
        
    }

    // Editor Functions
    [ExecuteInEditMode]
    void OnEnable()
    {
        // We don't want to send this mesh to voxie
        gameObject.GetComponent<Collider>().tag = "VoxieHide";

        // Load Voxie aspect ratio details
        voxie_loadini_int(ref vw);

        // UpdateAspectRatio();
        AspectRatio = new Vector3(1.0f, 0.44f, 1.0f);
        transform.localScale = AspectRatio * Scale;

        // Set up build options

    }

    void OnValidate()
    {
        // Load Voxie aspect ratio details
        voxie_loadini_int(ref vw);

        //UpdateAspectRatio();
        AspectRatio = new Vector3(1.0f, 0.444f, 1.0f);
        transform.localScale = AspectRatio * Scale;
    }

}
