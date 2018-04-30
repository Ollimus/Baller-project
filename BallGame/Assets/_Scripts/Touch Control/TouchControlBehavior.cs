using UnityEngine;
using UnityEngine.SceneManagement;


//Will be used in the future.
public class TouchControlBehavior : MonoBehaviour
{
    private void Start()
    {
        string OS = SystemInfo.operatingSystem;

        if (SceneManager.GetActiveScene().name == "00_MainMenu" || OS.Contains("Windows") || OS.Contains("Linux") || OS.Contains("Mac"))
            gameObject.SetActive(false);
    }
}
