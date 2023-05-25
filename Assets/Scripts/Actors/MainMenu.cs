using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
//    [SerializeField] Toggle fullscreenToggle;

    void Start() {
//        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    public void CloseOptions() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // public void ToggleFullscreen(bool on) {
    //     Screen.fullScreen = on;
    // }

    // public void QuitGame() {
    //     Application.Quit();
    // }
}
