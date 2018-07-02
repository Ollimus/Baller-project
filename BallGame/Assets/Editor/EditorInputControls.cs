using UnityEditor;
using UnityEngine;

public class EditorInputControls : EditorWindow
{
   bool touchControlsActivated;

    void Start()
    {
        bool touchControlActivated = PlayerPrefs.GetInt("InputControlSetting", 0) == 0 ? true : false;

        if (touchControlActivated)
            KeyboardControls();
        else if (!touchControlActivated)
            MobileControls();
    }

    [MenuItem("Input/Keyboard")]
    public static void KeyboardControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        TouchControlBehavior.TouchInstance.SetActive(false);

        PlayerPrefs.SetInt("InputControlSetting", 0);
    }

    [MenuItem("Input/Mobile Controls")]    
    public static void MobileControls()
    {
        if (TouchControlBehavior.TouchInstance == null) return;

        TouchControlBehavior.TouchInstance.SetActive(true);

        PlayerPrefs.SetInt("InputControlSetting", 1);
    }
}