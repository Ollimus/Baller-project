using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControlBehavior : MonoBehaviour {

    public bool debuggingMode = true;

    // checks whether user is in mainmenu and disables touch controls if this is the same.
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "00_MainMenu")
            FindObjectOfType<GeneralManager>().DisableManager(gameObject);
    }
}
