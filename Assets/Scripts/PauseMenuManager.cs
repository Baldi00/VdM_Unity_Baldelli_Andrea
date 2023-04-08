using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
    private AudioMixer audioMixer;
	[SerializeField]
	private Slider ambientVolumeSlider;
	[SerializeField]
	private Slider musicVolumeSlider;
	[SerializeField]
	private Slider effectsVolumeSlider;

    private bool isPaused;

    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Escape))
    	{
    		GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;

    		if (GameManager.Instance.IsPaused)
    		{
    			Cursor.visible = true;
    			pauseMenu.SetActive(true);
    			Time.timeScale = 0f;
    		}
    		else
    		{
    			Cursor.visible = false;
    			pauseMenu.SetActive(false);
    			Time.timeScale = 1f;
    		}
    	}
    }

    public void Resume()
    {
    	GameManager.Instance.IsPaused = false;
    	Cursor.visible = false;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    }

    public void Save()
    {
    	GameManager.Instance.Save();
    }

    public void Exit()
    {
    	Application.Quit();
    }

    public void GoToMainMenu()
    {
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("MainMenu");
    }

    public void SetAmbientVolume(float value)
    {
    	audioMixer.SetFloat("AmbientVolume", Mathf.Log10(value) * 20);
    	GameManager.Instance.SetAmbientVolume(value);
    }

    public void SetMusicVolume(float value)
    {
    	audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    	GameManager.Instance.SetMusicVolume(value);
    }

    public void SetEffectsVolume(float value)
    {
    	audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
    	GameManager.Instance.SetEffectsVolume(value);
    }

    public void SetAmbientVolumeSliderValue(float value)
    {
    	ambientVolumeSlider.value = value;
    }

    public void SetMusicVolumeSliderValue(float value)
    {
    	musicVolumeSlider.value = value;
    }

    public void SetEffectsVolumeSliderValue(float value)
    {
    	effectsVolumeSlider.value = value;
    }
}
