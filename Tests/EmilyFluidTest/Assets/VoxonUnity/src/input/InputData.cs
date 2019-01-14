using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Used to maintain keybindings, as well as handle saved and loaded bindings
///  </summary>
public class InputData
{

    public KeyBindings Keyboard;
    public MouseBindings Mouse;
    public ButtonBindings J1Buttons;
    public AxisBindings J1Axis;
    public ButtonBindings J2Buttons;
    public AxisBindings J2Axis;
    public ButtonBindings J3Buttons;
    public AxisBindings J3Axis;
    public ButtonBindings J4Buttons;
    public AxisBindings J4Axis;

    // Use this for initialization
    public InputData()
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
    }

    public void From_IC(InputController ic)
    {

        KeyBindCopy(ic.Keyboard, Keyboard);
        MouseBindCopy(ic.Mouse, Mouse);

        ButBindCopy(InputController.Instance.J1Buttons, J1Buttons);
        ButBindCopy(InputController.Instance.J2Buttons, J2Buttons);
        ButBindCopy(InputController.Instance.J3Buttons, J3Buttons);
        ButBindCopy(InputController.Instance.J4Buttons, J4Buttons);

        AxisBindCopy(InputController.Instance.J1Axis, J1Axis);
        AxisBindCopy(InputController.Instance.J2Axis, J2Axis);
        AxisBindCopy(InputController.Instance.J3Axis, J3Axis);
        AxisBindCopy(InputController.Instance.J4Axis, J4Axis);
    }

    public void To_IC(InputController ic)
    {
        KeyBindCopy(Keyboard, ic.Keyboard);
        MouseBindCopy(Mouse, ic.Mouse);

        ButBindCopy(J1Buttons, InputController.Instance.J1Buttons);
        ButBindCopy(J2Buttons, InputController.Instance.J2Buttons);
        ButBindCopy(J3Buttons, InputController.Instance.J3Buttons);
        ButBindCopy(J4Buttons, InputController.Instance.J4Buttons);

        AxisBindCopy(J1Axis, InputController.Instance.J1Axis);
        AxisBindCopy(J2Axis, InputController.Instance.J2Axis);
        AxisBindCopy(J3Axis, InputController.Instance.J3Axis);
        AxisBindCopy(J4Axis, InputController.Instance.J4Axis);
    }

    private void KeyBindCopy(KeyBindings from, KeyBindings to)
    {
        to.Clear();
        
        foreach (string key in from.Keys)
        {
            to.Add(key, from[key]);
        }
    }

    private void MouseBindCopy(MouseBindings from, MouseBindings to)
    {
        to.Clear();

        foreach (string key in from.Keys)
        {
            to.Add(key, from[key]);
        }
    }

    private void ButBindCopy(ButtonBindings from, ButtonBindings to)
    {
        to.Clear();

        foreach (string key in from.Keys)
        {
            to.Add(key, from[key]);
        }
    }

    private void AxisBindCopy(AxisBindings from, AxisBindings to)
    {
        to.Clear();
        
        foreach (string key in from.Keys)
        {
            to.Add(key, from[key]);
        }
    }
}