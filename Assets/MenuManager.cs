using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
	
	public GameObject MainMenuComponents;
	public GameObject OptionsComponents;

	

	public AudioMixer audioMixer;


    public void PlayGame()
	{
        SceneManager.LoadScene(1);
	}

	public void OptionsMenu()
	{
		TurnOffComponents();
	}

    

    public void QuitGame()
	{
        Application.Quit();
	}


	//Extended Options Menu
	public void TurnOffComponents()
	{
		MainMenuComponents.SetActive(false);
		OptionsComponents.SetActive(true);


	}
	public void TurnOnComponent()
	{
		MainMenuComponents.SetActive(true);
		OptionsComponents.SetActive(false);

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


	//Future Settings.
	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

}
