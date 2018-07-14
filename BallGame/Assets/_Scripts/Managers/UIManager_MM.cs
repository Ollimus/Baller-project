using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Managers
{
    public partial class UIManager : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject skinMenu;

        public void ReturnToMainMenu(GameObject currentMenu)
        {
            currentMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void ChangeToMenu(GameObject desiredMenu)
        {
            mainMenu.SetActive(false);
            desiredMenu.SetActive(true);
        }
    }
}

