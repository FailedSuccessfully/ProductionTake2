using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPauseOn = false;


    public GameObject PauseMenuComponents;
    public GameObject OptionsMenuComponents;


    public AudioMixer audioMixer;


    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && isPauseOn == false)
		{
            PauseGame();
            isPauseOn = true;
		} else if (Input.GetKeyDown(KeyCode.Escape) && isPauseOn == true)
		{
            ResumeGame();
            isPauseOn = false;
        }

    }

	public void PauseGame()
	{
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

   public void ResumeGame()
	{
        Time.timeScale = 1;
        PauseMenu.SetActive(false);

    }

    public void TurnOffComponents()
    {
        PauseMenuComponents.SetActive(false);
        OptionsMenuComponents.SetActive(true);


    }
    public void TurnOnComponent()
    {
        PauseMenuComponents.SetActive(true);
        OptionsMenuComponents.SetActive(false);

    }

    public void QuitToMainMenu()
	{
        SceneManager.LoadScene(0);
    }

    //Current in project.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
