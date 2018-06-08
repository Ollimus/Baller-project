using UnityEditor;
using UnityEngine;
using Managers;
    
[ExecuteInEditMode]
public class EditorInputControls : EditorWindow
{
    [MenuItem("Input/Keyboard")]
    public static void KeyboardControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        EditorPrefs.SetInt("InputControlSetting", 0);

        TouchControlBehavior.TouchInstance.DisplayTouchControls(false);
    }

    [MenuItem("Input/Mobile Controls")]    
    public static void MobileControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        EditorPrefs.SetInt("InputControlSetting", 1);

        TouchControlBehavior.TouchInstance.DisplayTouchControls(true);
    }
}