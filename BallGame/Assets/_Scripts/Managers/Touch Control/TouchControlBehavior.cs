using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Managers
{
    [ExecuteInEditMode]
    public class TouchControlBehavior : MonoBehaviour
    {
        public static TouchControlBehavior TouchInstance;

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
                gameObject.SetActive(false);
        }

        public void DisplayTouchControls(bool activate)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(activate);
            }
        }
    }
}