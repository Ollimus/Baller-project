using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControlBehavior : MonoBehaviour {

    public bool debuggingMode = true;

    // checks whether user is in mainmenu and disables touch controls if this is the same.
    private void Awake()
    {
        string OS = SystemInfo.operatingSystem;

        if (SceneManager.GetActiveScene().name == "00_MainMenu" || OS.Contains("Windows") || OS.Contains("Linux") || OS.Contains("Mac"))
            FindObjectOfType<GeneralManager>().DisableManager(gameObject);
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
            FindObjectOfType<GeneralManager>().DisableManager(gameObject);
    }    
}
