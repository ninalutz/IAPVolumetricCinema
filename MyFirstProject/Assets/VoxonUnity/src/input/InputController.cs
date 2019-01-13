using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Keycodings for Keyboard Input
///  </summary>
///  <remarks>
///  Default keybinding file name is default.json (clearing the file name without entering a new will load this file)
///  Default return value from key or button functions is Keys._ or Buttons._ .
/// </remarks>
public enum Keys
{
    _ = 0x00,
    Escape = 0x01,
    _1 = 0x02,
    _2 = 0x03,
    _3 = 0x04,
    _4 = 0x05,
    _5 = 0x06,
    _6 = 0x07,
    _7 = 0x08,
    _8 = 0x09,
    _9 = 0x0A,
    _0 = 0x0B,
    Dash = 0x0C,
    Equals = 0x0D,
    Backspace = 0x0E,
    Tab = 0x0F,
    Q = 0x10,
    W = 0x11,
    E = 0x12,
    R = 0x13,
    T = 0x14,
    Y = 0x15,
    U = 0x16,
    I = 0x17,
    O = 0x18,
    P = 0x19,
    OpenSquareBracket = 0x1A,
    CloseSquareBracket = 0x1B,
    Enter = 0x1C,
    LeftControl = 0x1D,
    A = 0x1E,
    S = 0x1F,
    D = 0x20,
    F = 0x21,
    G = 0x22,
    H = 0x23,
    J = 0x24,
    K = 0x25,
    L = 0x26,
    SemiColon = 0x27,
    SingleQuote = 0x28,
    Tilde = 0x29,
    LeftShift = 0x2A,
    //BackSlash = 0x2B, (Owned by VX1)
    Z = 0x2C,
    X = 0x2D,
    C = 0x2E,
    V = 0x2F,
    B = 0x30,
    N = 0x31,
    M = 0x32,
    Comma = 0x33,
    Dot = 0x34,
    Divide = 0x35,
    RightShift = 0x36,
    NUMPAD_Multiply = 0x37,
    LeftAlt = 0x38,
    Space = 0x39,
    CapsLock = 0x3A,
    F1 = 0x3B,
    F2 = 0x3C,
    F3 = 0x3D,
    F4 = 0x3E,
    F5 = 0x3F,
    F6 = 0x40,
    F7 = 0x41,
    F8 = 0x42,
    F9 = 0x43,
    F10 = 0x44,
    NumLock = 0x45,
    ScrollLock = 0x46,
    Home_NUMPAD7 = 0x47,
    Up_NUMPAD8 = 0x48,
    PageUp_NUMPAD9 = 0x49,
    NUMPAD_Dash = 0x4A,
    Left_NUMPAD4 = 0x4B,
    NUMPAD5 = 0x4C,
    Right_NUMPAD6 = 0x4D,
    NUMPAD_Plus = 0x4E,
    End_NUMPAD1 = 0x4F,
    Down_NUMPAD2 = 0x50,
    PageDown_NUMPAD3 = 0x51,
    Insert_NUMPAD0 = 0x52,
    Delete_NUMPAD_Dot = 0x53,
    F11 = 0x57,
    F12 = 0x58
};

/// <summary>  
///  Keycoding for XBox controller buttons
///  </summary>
public enum Buttons
{
    _,
    DPad_Up = 0x0001,
    DPad_Down = 0x0002,
    DPad_Left = 0x0004,
    DPad_Right = 0x0008,
    Start = 0x0010,
    Back = 0x0020,
    Left_Thumb = 0x0040,
    Right_Thumb = 0x0080,
    Left_Shoulder = 0x0100,
    Right_Shoulder = 0x0200,
    A = 0x1000,
    B = 0x2000,
    X = 0x4000,
    Y = 0x8000
};

/// <summary>  
///  Keycoding for XBox controller Axis
///  </summary>
public enum Axis
{
    _,
    LeftTrigger = 1,
    RightTrigger = 2,
    LeftStickX = 3,
    LeftStickY = 4,
    RightStickX = 5,
    RightStickY = 6
}

public enum Mouse_Button
{
    _,
    Mouse_1 = 0x1,
    Mouse_2 = 0x2,
    Mouse_3 = 0x4
}


public struct Mouse_Position
{
    public float x;
    public float y;
    public float z;

    public Mouse_Position(Vector3 pos)
    {
        this.x = pos.x / 100;
        this.y = pos.y / 100;
        this.z = pos.z / 100;
    }
}

[System.Serializable] public class KeyBindings : SerializableDictionary<string, Keys> { }
[System.Serializable] public class MouseBindings : SerializableDictionary<string, Mouse_Button> { }
[System.Serializable] public class ButtonBindings : SerializableDictionary<string, Buttons> { }
[System.Serializable] public class AxisBindings : SerializableDictionary<string, Axis> { }

/// <summary>  
///  Input Controller handles keybindings, Allows Saving and Loading of keybinding.json files
///  </summary>
public class InputController : Singleton<InputController>
{

    [Header("Load / Save")]
    public string filename;
    // private string old_filename;
    public bool Load, Save, New_File;

    [Header("Keyboard")]
    public KeyBindings Keyboard;

    [Header("Mouse")]
    public MouseBindings Mouse;

    [Header("Joy 1")]
    public ButtonBindings J1Buttons;
    public AxisBindings J1Axis;
    [Header("Joy 2")]
    public ButtonBindings J2Buttons;
    public AxisBindings J2Axis;
    [Header("Joy 3")]
    public ButtonBindings J3Buttons;
    public AxisBindings J3Axis;
    [Header("Joy 4")]
    public ButtonBindings J4Buttons;
    public AxisBindings J4Axis;
    

    // Use this for initialization
    void Start()
    {
        Keyboard = new KeyBindings();
        Mouse = new MouseBindings();
        J1Buttons = new ButtonBindings();
        J1Axis = new AxisBindings();
        J2Buttons = new ButtonBindings();
        J2Axis = new AxisBindings();
        J3Buttons = new ButtonBindings();
        J3Axis = new AxisBindings();
        J4Buttons = new ButtonBindings();
        J4Axis = new AxisBindings();

        LoadData(filename);
    }

    public void LoadData(string gameDataFileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        if (File.Exists(filePath))
        {
            // Read the JSON from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the JSON to JSON Utility, and tell it to make a gameobject from it
            InputData Loaded = JsonUtility.FromJson<InputData>(dataAsJson);

            Loaded.To_IC(this);
        }
        else
        {
            Debug.Log("Cannot load input data!");
        }
    }

    public void SaveData(string gameDataFileName)
    {
        InputData Save = new InputData();
        Save.From_IC(this);
        
        string dataAsJson = JsonUtility.ToJson(Save, true);
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        File.WriteAllText(filePath, dataAsJson);
    }

    public Keys GetKey(string key)
    {
        if (Keyboard.ContainsKey(key))
        {
            return Keyboard[key];
        }
        else
        {
            UnityEngine.Debug.Log("Does not contain this string: " + key);
            return Keys._;
        }
    }

    public Buttons GetButton(string button, int joystick)
    {
        switch (joystick)
        {
            case 1:
                if (J1Buttons.ContainsKey(button))
                {
                    return J1Buttons[button];
                }
                else
                {
                    return Buttons._;
                }

            case 2:
                if (J2Buttons.ContainsKey(button))
                {
                    return J2Buttons[button];
                }
                else
                {
                    return Buttons._;
                }

            case 3:
                if (J3Buttons.ContainsKey(button))
                {
                    return J3Buttons[button];
                }
                else
                {
                    return Buttons._;
                }

            case 4:
                if (J4Buttons.ContainsKey(button))
                {
                    return J4Buttons[button];
                }
                else
                {
                    return Buttons._;
                }

            default:
                break;
        }
        return Buttons._;
    }

    public Axis GetAxis(string axis, int joystick)
    {
        switch (joystick)
        {
            case 1:
                if (J1Axis.ContainsKey(axis))
                {
                    return J1Axis[axis];
                }
                else
                {
                    return Axis._;
                }

            case 2:
                if (J2Axis.ContainsKey(axis))
                {
                    return J2Axis[axis];
                }
                else
                {
                    return Axis._;
                }

            case 3:
                if (J3Axis.ContainsKey(axis))
                {
                    return J3Axis[axis];
                }
                else
                {
                    return Axis._;
                }

            case 4:
                if (J4Axis.ContainsKey(axis))
                {
                    return J4Axis[axis];
                }
                else
                {
                    return Axis._;
                }

            default:
                break;
        }
        return Axis._;
    }

    public Mouse_Button GetMouseButton(string key)
    {
        if (Mouse.ContainsKey(key))
        {
            return Mouse[key];
        }
        else
        {
            UnityEngine.Debug.Log("Does not contain this string: " + key);
            return Mouse_Button._;
        }
    }

    [ExecuteInEditMode]
    void OnEnable()
    {
        if (filename == "")
        {
            filename = "default.json";
        }
    }

    void OnValidate()
    {
        if (filename == "")
        {
            filename = "default.json";
        }

        if (Save)
        {
            SaveData(filename);
            Save = false;
        }
        if (Load)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, filename);

            if (File.Exists(filePath))
            {
                LoadData(filename);
            }
            Load = false;
        }
        if (New_File)
        {
            Keyboard.Clear();
            Mouse.Clear();
            J1Axis.Clear();
            J1Buttons.Clear();
            J2Axis.Clear();
            J2Buttons.Clear();
            J3Axis.Clear();
            J3Buttons.Clear();
            J4Axis.Clear();
            J4Buttons.Clear();

            New_File = false;
        }
    }
}