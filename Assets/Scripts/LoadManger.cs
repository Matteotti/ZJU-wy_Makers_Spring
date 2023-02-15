using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class LoadManger : MonoBehaviour
{
    public GameObject loadScreen;
    public Text loadText;

    public GameObject settingMenu;
    public GameObject title;
    public GameObject mainMenu;
    public AudioMixer audioMixer;
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
        title.SetActive(false);
    }
    public void SSettingBacks()
    {
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);
        title.SetActive(true);
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

    public void SetLowQuailty()
    {
        Screen.SetResolution(1280, 723, false, 60);
    }
    public void SetMidQuailty()
    {
        Screen.SetResolution(1920, 1080, true, 60);

    }
    public void SetHighQuailty()
    {
        Screen.SetResolution(2560, 1440, true, 60);
    }

    IEnumerator LoadLevel()

    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            loadText.text = operation.progress * 100 + "%";
            if (operation.progress >= 0.9f)
                    {
                        loadText.text = "Press any key to continue";

                        if (Input.anyKeyDown)
                        {
                            operation.allowSceneActivation = true;
                        }
                    }
            yield return null;

        }
        
    }
}
