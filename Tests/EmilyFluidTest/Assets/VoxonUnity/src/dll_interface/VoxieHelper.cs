using System;
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine;

/// <summary>  
///  VoxieHelper is a collection of VX1 dll specific structures and DLL 
///  function bindings
///  </summary>
public class VoxieHelper : MonoBehaviour
    {

        protected struct voxie_xbox_t
        {
        public short but;       //XBox controller buttons (same layout as XInput)
        public short lt, rt;    //XBox controller left&right triggers (0..255)
        public short tx0, ty0;  //XBox controller left  joypad (-32768..32767)
        public short tx1, ty1;  //XBox controller right joypad (-32768..32767)
    }

        protected struct point2d
        {
            public float x, y;
        }

        protected struct voxie_disp_t
        {
        //settings for quadrilateral (keystone) compensation (use voxiedemo mode 2 to calibrate)
        public point2d keyst0, keyst1, keyst2, keyst3, keyst4, keyst5, keyst6, keyst7;
        public int colo_r, colo_g, colo_b; //initial values at startup for rgb color mode
        public int mono_r, mono_g, mono_b; //initial values at startup for mono mode
        public int mirrorx, mirrory;       //projector hardware flipping (I suggest avoiding this)
        }

        protected struct voxie_wind_t
        {
        //Emulation
        public int useemu;             //0=Voxiebox, 1=emulation in 2D window (representative of actual view on HW), 2=emulation in 2D window (made for 2D display)
        public float emuhang;          //emulator horizontal angle (radians)
        public float emuvang;          //emulator vertical   angle (radians)
        public float emudist;          //emulator distance; minimum is 2000.0.

        //Display
        public int xdim, ydim;         //projector dimensions (912,1140)
        public int projrate;           //projector rate in Hz {60..107}
        public int framepervol;        //# projector frames per volume {1..16}. Ex:framepervol=3 at projrate=60
                                       //gives 60/3 = 20Hz volume rate
        public int usecol;             //0=mono white, 1=full color time multiplexed,
                                       //-1=red, -2=green, -3=yellow, -4=blue, -5=magenta, -6=cyan
        public int dispnum;            //number of displays to search for and use (typically 1)
        public int HighLumenDangerProtect; //if enabled (nonzero), protects ledr/g/b from going too high
        public voxie_disp_t disp0, disp1, disp2; //see voxie_disp_t

        //Actuator
        public int hwsync_frame0;      //first frame offset (-1 to disable sync hw)
        public int hwsync_phase;       //high precision phase offset
        public int hwsync_amp0, hwsync_amp1, hwsync_amp2, hwsync_amp3;      //amplitude {0..65536} for each channel
        public int hwsync_pha0, hwsync_pha1, hwsync_pha2, hwsync_pha3;      //phase {0..65536} for each channel
        public int hwsync_levthresh;   //threshold ADC value for peak detection {0..1024, but typically in range: 256..512}
        public int voxie_vol;          //amplitude scale when using sine wave audio output {0..100}

        //Render
        public int ilacemode;
        public int drawstroke;         //1=draw on up stroke, 2=draw on down stroke, 3=draw on both up&down
        public int dither;             //0=dither off, 1=dither mono only, 2=dither RGB only, 3=dither both
        public int smear;              //1+=increase brightness in x-y at post processing (slower render), 0=not
        public int usekeystone;        //0=no keystone compensation (for testing only), 1=keystone quadrilateral compensation (default) - see proj[]
        public int flip;               //flip coordinate system in voxiebox.dll
        public int menu_on_voxie;      //1=display menu on voxiebox view
        
        public float aspx, aspy, aspz; //aspect ratio, loaded from voxiebox.ini, used by application. Values typically around 1.f
        public float gamma;            //gamma value for interpolating in voxie_drawspr()/voxie_drawheimap().
        public float density;          //scale factor controlling dot density of STL model rendering (default:1.f)

        //Audio
        public int sndfx_vol;          //amplitude scale of sound effects {0..100}
        public int voxie_aud;          //audio channel index of motor. (Playback devices..Speakers..Configure..
                                       //   Audio Channels)
        public int excl_audio;         //1=exclusive audio mode (much faster & more stable sync - recommended!
                                       //0=shared audio mode (if audio access to other programs required)
        public int sndfx_aud0, sndfx_aud1;       //audio channel indices of sound effects, [0]=left, [1]=right
        public int playsamprate;       //sample rate used by audio driver (written by voxie_init()). 0 is written
                                       //   if no audio channels are enabled in voxiebox.ini.
        public int playnchans;         //number of audio channels expected to be rendered by user audio mixer
        public int recsamprate;        //recording sample rate - to use, must write before voxie_init()
        public int recnchans;          //number of audio channels in recording callback

        //Misc.
        public int isrecording;        //0=normally, 1 when .REC file recorder is in progress (written by DLL)
        public int excl_mouse;         //1=exclusive mouse, 0=not
        public int dispcur;            //current display selected in menus {0..dispnum-1}

        //Obsolete
        public double freq;            //starting value in Hz (must be set before first call to voxie_init()); obsolete - not used by current hardware
        public double phase;           //phase lock {0.0..1.0} (can be updated on later calls to voxie_init()); obsolete - not used by current hardware
    }

        public struct voxie_frame_t
        {
            public IntPtr f;              //Pointer to top-left-up of current frame to draw
            public IntPtr p;              //Number of bytes per horizontal line (x)
            public IntPtr fp;             //Number of bytes per 24-plane frame (1/3 of screen)
            public int x, y;               //Width and height of viewport
            public int usecol;             //Tells whether color mode is selected
            public int drawplanes;         //Tells how many planes to draw
            public int x0, y0, x1, y1;     //Viewport extents
            public float xmul, ymul, zmul; //Transform for medium and high level graphics functions..
            public float xadd, yadd, zadd; //Transform is: actual_x = passed_x * xmul + xadd
        }

        public struct point3d
        {
            public float x, y, z;
        }

        public struct point3dcol_t
        {
            public float x, y, z;
            public int col;
        }

        public struct pol_t
        {
            public float x, y, z;
            public int p2;
        }

        public struct poltex_t
        {
            public float x, y, z, u, v;
            public int col;
        }

        public struct tiletype
        {
            public IntPtr first_pixel;          // pointer to first pixel of the texture (usually the top-left corner)
            public Int64 pitch;                   // pitch, or number of bytes per horizontal line (usually x*4)
            public Int64 height, width;          // width & height of texture
        }

        public struct voxie_inputs_t
        {
            public int bstat, obstat, dmousx, dmousy, dmousz;
        }

        protected struct tri_t
        {
            public float x0, y0, z0;
            public int n0;
            public float x1, y1, z1;
            public int n1;
            public float x2, y2, z2;
            public int n2;
        }

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_load(ref voxie_wind_t vw);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_loadini_int(ref voxie_wind_t vw);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_getvw(ref voxie_wind_t vw);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_init(ref voxie_wind_t vw);

        // Used for VX1
        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_uninit_int(int id);

        // Used for Unity Play function
        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_uninit_int();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_breath(ref voxie_inputs_t ins);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_quitloop();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern double voxie_klock();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_keystat(int i);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_keyread();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_doscreencap();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_setview(ref voxie_frame_t vf, float x0, float y0, float z0, float x1, float y1,
            float z1);
    
        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_frame_start(ref voxie_frame_t vf);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_frame_end();

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_setleds(int r, int g, int b);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawvox(ref voxie_frame_t vf, float fx, float fy, float fz, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawbox(ref voxie_frame_t vf, float x0, float y0, float z0, float x1, float y1,
            float z1, int fillmode, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawlin(ref voxie_frame_t vf, float x0, float y0, float z0, float x1, float y1,
            float z1, int col);

#if !FIX_AMD64_HACK
        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawpol(ref voxie_frame_t vf, ref pol_t pt, int n, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawmesh(ref voxie_frame_t vf, point3dcol_t[] vt, int vtn, int[] mesh, int meshn, int fillmode, int col);

    // voxie_drawmeshtex() also supports rendering from a texture in memory. To do so, do the following:
    // Add(1<<3) to the flags parameter
    // texnam(the 2nd parameter) is now treated as a pointer to a tiletype structure(tiletype*) instead of a filename string.
    // All textures must use 32-bit ARGB color format - with blue as least significant.
    [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawmeshtex(ref voxie_frame_t vf, ref tiletype texture, poltex_t[] vt, int vtn, int[] mesh, int meshn,
            int flags, int col);

    [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
    protected static extern void voxie_drawmeshtex(ref voxie_frame_t vf, int nullptr, poltex_t[] vt, int vtn, int[] mesh, int meshn,
            int flags, int col);
#else
	[DllImport("voxiebox",CallingConvention=CallingConvention.Cdecl)] protected extern static void   voxie_drawpol    (ref voxie_frame_t vf, ref tri_t tri, int n, int col);
	[DllImport("voxiebox",CallingConvention=CallingConvention.Cdecl)] protected extern static void   voxie_drawmesh   (ref voxie_frame_t vf, ref point3d pt, int ptn, ref int mesh, int meshn, int fillmode, int col);
#endif

    [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawsph(ref voxie_frame_t vf, float fx, float fy, float fz, float rad,
            int issol, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawcone(ref voxie_frame_t vf, float x0, float y0, float z0, float r0, float x1,
            float y1, float z1, float r1, int issol, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_drawspr(ref voxie_frame_t vf, [MarshalAs(UnmanagedType.LPStr)] string st,
            ref point3d p, ref point3d r, ref point3d d, ref point3d f, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_printalph(ref voxie_frame_t vf, ref point3d p, ref point3d r, ref point3d d,
            int col, byte[] st);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_drawcube(ref voxie_frame_t vf, ref point3d p, ref point3d r, ref point3d d,
            ref point3d f, int fillmode, int col);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern float voxie_drawheimap(ref voxie_frame_t vf, [MarshalAs(UnmanagedType.LPStr)] string st,
            ref point3d p, ref point3d r, ref point3d d, ref point3d f, int colorkey, int heimin, int flags);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_playsound([MarshalAs(UnmanagedType.LPStr)] string st, int chan, int volperc0,
            int volperc1, float frqmul);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int voxie_xbox_read (int id, ref voxie_xbox_t vx);

        [DllImport("voxiebox", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void voxie_xbox_write (int id, float lmot, float rmot);
}
