using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingMenu;
    public AudioMixer audioMixer;

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0f)
            {
                ResumeGame();
            }

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void SettingsMenu()
    {
        PauseMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }

    public void SettingsBack()
    {
        PauseMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}

