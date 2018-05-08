using UnityEngine;
using UnityEngine.SceneManagement;


//Will be used in the future.
public class TouchControlBehavior : MonoBehaviour
{
    public static GameObject TouchInstance;

    private void Start()
    {
        if (TouchInstance == null)
            TouchInstance = gameObject;
        else
        {
            Destroy(gameObject);
            return;
        }

        string OS = SystemInfo.operatingSystem;

        if (SceneManager.GetActiveScene().name == "00_MainMenu" || OS.Contains("Windows") || OS.Contains("Linux") || OS.Contains("Mac"))
            gameObject.SetActive(false);
    }
}
