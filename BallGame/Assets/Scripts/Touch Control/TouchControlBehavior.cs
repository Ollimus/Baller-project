using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControlBehavior : MonoBehaviour {

    // checks whether user is in mainmenu and disables touch controls if this is the same.
    private void Start()
    {
        string OS = SystemInfo.operatingSystem;

        if (SceneManager.GetActiveScene().name == "00_MainMenu" || OS.Contains("Windows") || OS.Contains("Linux") || OS.Contains("Mac"))
            FindObjectOfType<GeneralManager>().DisableManager(gameObject);
    }
}
