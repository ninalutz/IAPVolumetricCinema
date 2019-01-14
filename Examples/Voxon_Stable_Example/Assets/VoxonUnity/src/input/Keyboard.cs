using UnityEngine;
using System.Collections;

namespace Voxon
{
    public class Keyboard : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public static KeyCode VKtoKC(int vkCode)
        {
            switch (vkCode)
            {
                // VK_LBUTTON - Left mouse button
                case 0x01:
                    return KeyCode.Mouse0;
                // VK_RBUTTON - Right mouse button
                case 0x02:
                    return KeyCode.Mouse1;
                // VK_CANCEL - Control-break processing
                case 0x03:
                // TODO                
                // VK_MBUTTON - Middle mouse button (three-button mouse)
                case 0x04:
                    return KeyCode.Mouse2;
                // VK_XBUTTON1 - X1 mouse button
                case 0x05:
                    return KeyCode.Mouse3;
                // VK_XBUTTON2 - X2 mouse button
                case 0x06:
                    return KeyCode.Mouse4;
                // Undefined
                // case 0x07:

                // VK_BACK - BACKSPACE key
                case 0x08:
                    return KeyCode.Backspace;
                // VK_TAB - TAB key
                case 0x09:
                    return KeyCode.Tab;
                // Reserved
                // case 0x0A:

                // Reserved
                // case 0x0B:

                // VK_CLEAR - CLEAR key
                case 0x0C:
                    return KeyCode.Clear;
                // VK_RETURN - ENTER key
                case 0x0D:
                    return KeyCode.Return;
                // Undefined
                // case 0x0E:

                // Undefined
                // case 0x0F:

                // VK_SHIFT - SHIFT key
                case 0x10:
                // TODO
                // VK_CONTROL - CTRL key
                case 0x11:
                // TODO
                // VK_MENU - ALT key
                case 0x12:
                // TODO
                // VK_PAUSE - PAUSE key
                case 0x13:
                    return KeyCode.Pause;
                // VK_CAPITAL - CAPS LOCK key
                case 0x14:
                    return KeyCode.CapsLock;

                // VK_KANA - IME Kana mode
                // case 0x15:

                // VK_HANGUEL - IME Hanguel mode (maintained for compatibility; use // VK_HANGUL)
                // case 0x15:

                // VK_HANGUL - IME Hangul mode
                // case 0x15:

                // Undefined
                // case 0x16:

                // VK_JUNJA - IME Junja mode
                // case 0x17:

                // VK_FINAL - IME final mode
                // case 0x18:

                // VK_HANJA - IME Hanja mode
                // case 0x19:

                // VK_KANJI - IME Kanji mode
                // case 0x19:

                // Undefined
                // case 0x1A:

                // VK_ESCAPE - ESC key
                case 0x1B:
                    return KeyCode.Escape;
                // VK_CONVERT - IME convert
                // case 0x1C:

                // VK_NONCONVERT - IME nonconvert
                // case 0x1D:

                // VK_ACCEPT - IME accept
                // case 0x1E:

                // VK_MODECHANGE - IME mode change request
                // case 0x1F:

                // VK_SPACE - SPACEBAR
                case 0x20:
                    return KeyCode.Space;
                // VK_PRIOR - PAGE UP key
                case 0x21:
                    return KeyCode.PageUp;
                // VK_NEXT - PAGE DOWN key
                case 0x22:
                    return KeyCode.PageDown;
                // VK_END - END key
                case 0x23:
                    return KeyCode.End;
                // VK_HOME - HOME key
                case 0x24:
                    return KeyCode.Home;
                // VK_LEFT - LEFT ARROW key
                case 0x25:
                    return KeyCode.LeftArrow;
                // VK_UP - UP ARROW key
                case 0x26:
                    return KeyCode.UpArrow;
                // VK_RIGHT - RIGHT ARROW key
                case 0x27:
                    return KeyCode.RightArrow;
                // VK_DOWN - DOWN ARROW key
                case 0x28:
                    return KeyCode.DownArrow;
                // VK_SELECT - SELECT key
                case 0x29:
                // TODO
                // VK_PRINT - PRINT key
                case 0x2A:
                    return KeyCode.Print; // TODO COMPARE PRINT ? PRINTSCREEN
                // VK_EXECUTE - EXECUTE key
                case 0x2B:
                // TODO
                // VK_SNAPSHOT - PRINT SCREEN key
                case 0x2C:
                    return KeyCode.Print;
                // VK_INSERT - INS key
                case 0x2D:
                    return KeyCode.Insert;
                // VK_DELETE - DEL key
                case 0x2E:
                    return KeyCode.Delete;
                // VK_HELP - HELP key
                case 0x2F:
                    return KeyCode.Help;
                // 0 key
                case 0x30:
                    return KeyCode.Alpha0;
                // 1 key
                case 0x31:
                    return KeyCode.Alpha1;
                // 2 key
                case 0x32:
                    return KeyCode.Alpha2;
                // 3 key
                case 0x33:
                    return KeyCode.Alpha3;
                // 4 key
                case 0x34:
                    return KeyCode.Alpha4;
                // 5 key
                case 0x35:
                    return KeyCode.Alpha5;
                // 6 key
                case 0x36:
                    return KeyCode.Alpha6;
                // 7 key
                case 0x37:
                    return KeyCode.Alpha7;
                // 8 key
                case 0x38:
                    return KeyCode.Alpha8;
                // 9 key
                case 0x39:
                    return KeyCode.Alpha9;
                // Undefined
                // case 0x3A - 40:

                // A key
                case 0x41:
                    return KeyCode.A;
                // B key
                case 0x42:
                    return KeyCode.B;
                // C key
                case 0x43:
                    return KeyCode.C;
                // D key
                case 0x44:
                    return KeyCode.D;
                // E key
                case 0x45:
                    return KeyCode.E;
                // F key
                case 0x46:
                    return KeyCode.F;
                // G key
                case 0x47:
                    return KeyCode.G;
                // H key
                case 0x48:
                    return KeyCode.H;
                // I key
                case 0x49:
                    return KeyCode.I;
                // J key
                case 0x4A:
                    return KeyCode.J;
                // K key
                case 0x4B:
                    return KeyCode.K;
                // L key
                case 0x4C:
                    return KeyCode.L;
                // M key
                case 0x4D:
                    return KeyCode.M;
                // N key
                case 0x4E:
                    return KeyCode.N;
                // O key
                case 0x4F:
                    return KeyCode.O;
                // P key
                case 0x50:
                    return KeyCode.P;
                // Q key
                case 0x51:
                    return KeyCode.Q;
                // R key
                case 0x52:
                    return KeyCode.R;
                // S key
                case 0x53:
                    return KeyCode.S;
                // T key
                case 0x54:
                    return KeyCode.T;
                // U key
                case 0x55:
                    return KeyCode.U;
                // V key
                case 0x56:
                    return KeyCode.V;
                // W key
                case 0x57:
                    return KeyCode.W;
                // X key
                case 0x58:
                    return KeyCode.X;
                // Y key
                case 0x59:
                    return KeyCode.Y;
                // Z key
                case 0x5A:
                    return KeyCode.Z;
                // VK_LWIN - Left Windows key (Natural keyboard)
                case 0x5B:
                    return KeyCode.LeftWindows;
                // VK_RWIN - Right Windows key (Natural keyboard)
                case 0x5C:
                    return KeyCode.RightWindows;
                // VK_APPS - Applications key (Natural keyboard)
                case 0x5D:
                // TODO
                // Reserved
                // case 0x5E:

                // VK_SLEEP - Computer Sleep key
                case 0x5F:
                // TODO
                // VK_NUMPAD0 - Numeric keypad 0 key
                case 0x60:
                    return KeyCode.Keypad0;
                // VK_NUMPAD1 - Numeric keypad 1 key
                case 0x61:
                    return KeyCode.Keypad1;
                // VK_NUMPAD2 - Numeric keypad 2 key
                case 0x62:
                    return KeyCode.Keypad2;
                // VK_NUMPAD3 - Numeric keypad 3 key
                case 0x63:
                    return KeyCode.Keypad3;
                // VK_NUMPAD4 - Numeric keypad 4 key
                case 0x64:
                    return KeyCode.Keypad4;
                // VK_NUMPAD5 - Numeric keypad 5 key
                case 0x65:
                    return KeyCode.Keypad5;
                // VK_NUMPAD6 - Numeric keypad 6 key
                case 0x66:
                    return KeyCode.Keypad6;
                // VK_NUMPAD7 - Numeric keypad 7 key
                case 0x67:
                    return KeyCode.Keypad7;
                // VK_NUMPAD8 - Numeric keypad 8 key
                case 0x68:
                    return KeyCode.Keypad8;
                // VK_NUMPAD9 - Numeric keypad 9 key
                case 0x69:
                    return KeyCode.Keypad9;
                // VK_MULTIPLY - Multiply key
                case 0x6A:
                    return KeyCode.KeypadMultiply;
                // VK_ADD - Add key
                case 0x6B:
                    return KeyCode.KeypadPlus;
                // VK_SEPARATOR - Separator key
                case 0x6C:
                // TODO
                // VK_SUBTRACT - Subtract key
                case 0x6D:
                    return KeyCode.KeypadMinus;
                // VK_DECIMAL - Decimal key
                case 0x6E:
                    return KeyCode.KeypadPeriod;
                // VK_DIVIDE - Divide key
                case 0x6F:
                    return KeyCode.KeypadDivide;
                // VK_F1 - F1 key
                case 0x70:
                    return KeyCode.F1;
                // VK_F2 - F2 key
                case 0x71:
                    return KeyCode.F2;
                // VK_F3 - F3 key
                case 0x72:
                    return KeyCode.F3;
                // VK_F4 - F4 key
                case 0x73:
                    return KeyCode.F4;
                // VK_F5 - F5 key
                case 0x74:
                    return KeyCode.F5;
                // VK_F6 - F6 key
                case 0x75:
                    return KeyCode.F6;
                // VK_F7 - F7 key
                case 0x76:
                    return KeyCode.F7;
                // VK_F8 - F8 key
                case 0x77:
                    return KeyCode.F8;
                // VK_F9 - F9 key
                case 0x78:
                    return KeyCode.F9;
                // VK_F10 - F10 key
                case 0x79:
                    return KeyCode.F10;
                // VK_F11 - F11 key
                case 0x7A:
                    return KeyCode.F11;
                // VK_F12 - F12 key
                case 0x7B:
                    return KeyCode.F12;
                // VK_F13 - F13 key
                case 0x7C:
                    return KeyCode.F13;
                // VK_F14 - F14 key
                case 0x7D:
                    return KeyCode.F14;
                // VK_F15 - F15 key
                case 0x7E:
                    return KeyCode.F15;
                // VK_F16 - F16 key
                case 0x7F:
                // TODO
                // VK_F17 - F17 key
                case 0x80:
                // TODO
                // VK_F18 - F18 key
                case 0x81:
                // TODO
                // VK_F19 - F19 key
                case 0x82:
                // TODO
                // VK_F20 - F20 key
                case 0x83:
                // TODO
                // VK_F21 - F21 key
                case 0x84:
                // TODO
                // VK_F22 - F22 key
                case 0x85:
                // TODO
                // VK_F23 - F23 key
                case 0x86:
                // TODO
                // VK_F24 - F24 key
                case 0x87:
                // TODO
                // Unassigned
                // case 0x88 - 8F:

                // VK_NUMLOCK - NUM LOCK key
                case 0x90:
                    return KeyCode.Numlock;
                // VK_SCROLL - SCROLL LOCK key
                case 0x91:
                    return KeyCode.ScrollLock;
                // OEM specific
                // case 0x92 - 96:

                // Unassigned
                // case 0x97 - 9F:

                // VK_LSHIFT - Left SHIFT key
                case 0xA0:
                    return KeyCode.LeftShift;
                // VK_RSHIFT - Right SHIFT key
                case 0xA1:
                    return KeyCode.RightShift;
                // VK_LCONTROL - Left CONTROL key
                case 0xA2:
                    return KeyCode.LeftControl;
                // VK_RCONTROL - Right CONTROL key
                case 0xA3:
                    return KeyCode.RightControl;
                // VK_LMENU - Left MENU key
                case 0xA4:
                // TODO
                // VK_RMENU - Right MENU key
                case 0xA5:
                // TODO
                // VK_BROWSER_BACK - Browser Back key
                // case 0xA6:

                // VK_BROWSER_FORWARD - Browser Forward key
                // case 0xA7:

                // VK_BROWSER_REFRESH - Browser Refresh key
                // case 0xA8:

                // VK_BROWSER_STOP - Browser Stop key
                // case 0xA9:

                // VK_BROWSER_SEARCH - Browser Search key
                // case 0xAA:

                // VK_BROWSER_FAVORITES - Browser Favorites key
                // case 0xAB:

                // VK_BROWSER_HOME - Browser Start and Home key
                // case 0xAC:

                // VK_VOLUME_MUTE - Volume Mute key
                // case 0xAD:

                // VK_VOLUME_DOWN - Volume Down key
                // case 0xAE:

                // VK_VOLUME_UP - Volume Up key
                // case 0xAF:

                // VK_MEDIA_NEXT_TRACK - Next Track key
                // case 0xB0:

                // VK_MEDIA_PREV_TRACK - Previous Track key
                // case 0xB1:

                // VK_MEDIA_STOP - Stop Media key
                // case 0xB2:

                // VK_MEDIA_PLAY_PAUSE - Play/Pause Media key
                // case 0xB3:

                // VK_LAUNCH_MAIL - Start Mail key
                // case 0xB4:

                // VK_LAUNCH_MEDIA_SELECT - Select Media key
                // case 0xB5:

                // VK_LAUNCH_APP1 - Start Application 1 key
                // case 0xB6:

                // VK_LAUNCH_APP2 - Start Application 2 key
                // case 0xB7:

                // Reserved
                // case 0xB8 - B9:

                // VK_OEM_1 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key
                // TODO - Documentation showing only one can be selected (VK treats as same, Unity doesn't)
                case 0xBA:
                    return KeyCode.Colon;

                // VK_OEM_PLUS - For any country/region, the '+' key
                case 0xBB:
                    return KeyCode.Plus;
                // VK_OEM_COMMA - For any country/region, the ',' key
                case 0xBC:
                    return KeyCode.Comma;
                // VK_OEM_MINUS - For any country/region, the '-' key
                case 0xBD:
                    return KeyCode.Minus;
                // VK_OEM_PERIOD - For any country/region, the '.' key
                case 0xBE:
                    return KeyCode.Period;
                // VK_OEM_2 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key
                // TODO - Documentation about duplicate char
                case 0xBF:
                    return KeyCode.Slash;

                // VK_OEM_3 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '`~' key
                case 0xC0:
                    return KeyCode.BackQuote;

                // Reserved
                // case 0xC1 - D7:

                // Unassigned
                // case 0xD8 - DA:

                // VK_OEM_4 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '[{' key
                case 0xDB:
                    return KeyCode.LeftBracket;

                // VK_OEM_5 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\|' key
                case 0xDC:
                    return KeyCode.Backslash;

                // VK_OEM_6 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']}' key
                case 0xDD:
                    return KeyCode.RightBracket;

                // VK_OEM_7 - Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote' key
                case 0xDE:
                    return KeyCode.Quote;

                // VK_OEM_8 - Used for miscellaneous characters; it can vary by keyboard.
                case 0xDF:
                // TODO
                // Reserved
                // case 0xE0:

                // OEM specific
                case 0xE1:
                // TODO
                // VK_OEM_102 - Either the angle bracket key or the backslash key on the RT 102-key keyboard
                case 0xE2:
                // TODO
                // OEM specific
                // case 0xE3 - E4:
                // TODO

                // VK_PROCESSKEY
                case 0xE5:
                // TODO
                // IME PROCESS key - OEM specific
                case 0xE6:
                // TODO
                // VK_PACKET - Used to pass Unicode characters as if they were keystrokes. The // VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
                case 0xE7:
                // TODO
                // Unassigned
                // case 0xE8:

                // OEM specific
                // case 0xE9 - F5:
                // TODO
                // VK_ATTN - Attn key
                case 0xF6:
                // TODO
                // VK_CRSEL - CrSel key
                case 0xF7:
                // TODO
                // VK_EXSEL - ExSel key
                case 0xF8:
                // TODO
                // VK_EREOF - Erase EOF key
                case 0xF9:
                // TODO
                // VK_PLAY - Play key
                case 0xFA:
                // TODO
                // VK_ZOOM - Zoom key
                case 0xFB:
                // TODO

                // VK_NONAME - Reserved
                // case 0xFC:

                // VK_PA1 - PA1 key
                // case 0xFD:

                // VK_OEM_CLEAR - Clear key
                case 0xFE:
                    return KeyCode.Clear;
                default:
                    break;
            }
            return KeyCode.None;
        }

        public static int StringToDIK(string keyname)
        {
            switch (keyname)
            {
                case "-":
                    return 0x0C;
                case "'":
                    return 0x28;
                    
                case " ;":
                    return 0x27;
                    
                case "NUMPAD-":
                    return 0x4A;
                    
                case "NUMPAD*":
                    return 0x37;
                    
                case "NUMPAD.":
                    return 0x53;
                    
                case "NUMPAD/":
                    return 0xE0;
                    
                case "NUMPAD+":
                    return 0x4E;
                    
                case "NUMPAD0":
                    return 0x52;
                    
                case "NUMPAD1":
                    return 0x4F;
                    
                case "NUMPAD2":
                    return 0x50;
                    
                case "NUMPAD3":
                    return 0x51;
                    
                case "NUMPAD4":
                    return 0x4B;
                    
                case "NUMPAD5":
                    return 0x4C;
                    
                case "NUMPAD6":
                    return 0x4D;
                    
                case "NUMPAD7":
                    return 0x47;
                    
                case "NUMPAD8":
                    return 0x48;
                    
                case "NUMPAD9":
                    return 0x49;
                    
                case "NUMPADEnter":
                    return 0xE0;
                    
                case ",":
                    return 0x33;
                    
                case ".":
                    return 0x34;
                    
                case "/":
                    return 0x35;
                    
                case "[":
                    return 0x1A;
                    
                case "\\":
                    return 0x2B;
                    
                case "]":
                    return 0x1B;
                    
                case "`":
                    return 0x29;
                    
                case "=":
                    return 0x0D;
                    
                case "0":
                    return 0x0B;
                    
                case "1":
                    return 0x02;
                    
                case "2":
                    return 0x03;
                    
                case "3":
                    return 0x04;
                    
                case "4":
                    return 0x05;
                    
                case "5":
                    return 0x06;
                    
                case "6":
                    return 0x07;
                    
                case "7":
                    return 0x08;
                    
                case "8":
                    return 0x09;
                    
                case "9":
                    return 0x0A;
                    
                case "A":
                    return 0x1E;
                    
                case "Apps":
                    return 0xE0;
                    
                case "B":
                    return 0x30;
                    
                case "Backspace":
                    return 0x0E;
                    
                case "C":
                    return 0x2E;
                    
                case "CapsLock":
                    return 0x3A;
                    
                case "Down":
                    return 0xE0;
                    
                case "Left":
                    return 0xE0;
                    
                case "Right":
                    return 0xE0;
                    
                case "Up":
                    return 0xE0;
                    
                case "D":
                    return 0x20;
                    
                case "Delete":
                    return 0xE0;
                    
                case "E":
                    return 0x12;
                    
                case "End":
                    return 0xE0;
                    
                case "Enter":
                    return 0x1C;
                    
                case "Escape":
                    return 0x01;
                    
                case "F":
                    return 0x21;
                    
                case "F1":
                    return 0x3B;
                    
                case "F10":
                    return 0x44;
                    
                case "F11":
                    return 0x57;
                    
                case "F12":
                    return 0x58;
                    
                case "F2":
                    return 0x3C;
                    
                case "F3":
                    return 0x3D;
                    
                case "F4":
                    return 0x3E;
                    
                case "F5":
                    return 0x3F;
                    
                case "F6":
                    return 0x40;
                    
                case "F7":
                    return 0x41;
                    
                case "F8":
                    return 0x42;
                    
                case "F9":
                    return 0x43;
                    
                case "G":
                    return 0x22;
                    
                case "H":
                    return 0x23;
                    
                case "Home":
                    return 0xE0;
                    
                case "I":
                    return 0x17;
                    
                case "Insert":
                    return 0xE0;
                    
                case "J":
                    return 0x24;
                    
                case "K":
                    return 0x25;
                    
                case "L":
                    return 0x26;
                    
                case "Left Alt":
                    return 0x38;
                    
                case "Left Control":
                    return 0x1D;
                    
                case "Left GUI":
                    return 0xE0;
                    
                case "Left Shift":
                    return 0x2A;
                    
                case "M":
                    return 0x32;
                    
                case "N":
                    return 0x31;
                    
                case "NumLock":
                    return 0x45;
                    
                case "O":
                    return 0x18;
                    
                case "P":
                    return 0x19;
                    
                case "Page Down":
                    return 0xE0;
                    
                case "Page Up":
                    return 0xE0;
                    
                case "Pause":
                    return 0xE1;
                    
                case "Print Screen":
                    return 0xE0;
                    
                case "Q":
                    return 0x10;
                    
                case "R":
                    return 0x13;
                    
                case "Right Alt":
                    return 0xE0;
                    
                case "Right Control":
                    return 0xE0;
                    
                case "Right GUI":
                    return 0xE0;
                    
                case "Right Shift":
                    return 0x36;
                    
                case "S":
                    return 0x1F;
                    
                case "ScrollLock":
                    return 0x46;
                    
                case "Space":
                    return 0x39;
                    
                case "T":
                    return 0x14;
                    
                case "Tab":
                    return 0x0F;
                    
                case "U":
                    return 0x16;
                    
                case "V":
                    return 0x2F;
                    
                case "W":
                    return 0x11;
                    
                case "X":
                    return 0x2D;
                    
                case "Y":
                    return 0x15;
                    
                case "Z":
                    return 0x2C;
                    
                default:
                    return 0x00;
                    
            }
        }
    }
}