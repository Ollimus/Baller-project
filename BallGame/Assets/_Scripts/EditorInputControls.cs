using UnityEditor;
using UnityEngine;

public class EditorInputControls : EditorWindow
{    
    [MenuItem("Input/Keyboard")]
    public static void KeyboardControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        TouchControlBehavior.TouchInstance.SetActive(false);
    }

    [MenuItem("Input/Mobile Controls")]    
    public static void MobileControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        TouchControlBehavior.TouchInstance.SetActive(true);
    }
}