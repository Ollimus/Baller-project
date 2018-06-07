using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EditorInputControls : EditorWindow
{
    [MenuItem("Input/Keyboard")]
    public static void KeyboardControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        EditorPrefs.SetInt("InputControlSetting", 0);

        TouchControlBehavior.TouchInstance.ControlControls(false);
    }

    [MenuItem("Input/Mobile Controls")]    
    public static void MobileControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        EditorPrefs.SetInt("InputControlSetting", 1);

        TouchControlBehavior.TouchInstance.ControlControls(true);
    }
}