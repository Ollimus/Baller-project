using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

namespace Managers
{
    [ExecuteInEditMode]
    public class TouchControlBehavior : MonoBehaviour
    {
        public static TouchControlBehavior TouchInstance;
        public bool touchControlEnabled;

        private TouchControlJoystick joystickCache;

        private void Awake()
        {
            if (TouchInstance == null)
                TouchInstance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            string OS = SystemInfo.operatingSystem;

            /*if (EditorPrefs.GetInt("InputControlSetting") == 1)
            {
                DisplayTouchControls(true);
                return;
            }

            else if (EditorPrefs.GetInt("InputControlSetting") == 0)
            {
                DisplayTouchControls(false);
                return;
            }*/

            if (SceneManager.GetActiveScene().name == "00_MainMenu" || OS.Contains("Windows") || OS.Contains("Linux") || OS.Contains("Mac"))
                DisplayTouchControls(false);
            else
                DisplayTouchControls(true);
        }

        public void DisplayTouchControls(bool activate)
        {
            touchControlEnabled = activate;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(activate);
            }
        }

        public void RefreshPlayerCache(PlayerMovementController player)
        {
           gameObject.transform.GetComponentInChildren<TouchControlJoystick>().PlayerMovement = player;
        }
    }
}