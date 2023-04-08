using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
    private AudioMixer audioMixer;
	[SerializeField]
    private InputManager inputManager;
	[SerializeField]
	private GameManager gameManager;

    private bool isPaused;

    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Escape))
    	{
    		isPaused = !isPaused;
    		inputManager.SetPaused(isPaused);

    		if (isPaused)
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
    	isPaused = false;
    	inputManager.SetPaused(isPaused);
    	Cursor.visible = false;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    }

    public void Save()
    {
    	gameManager.Save();
    }

    public void Exit()
    {
    	Application.Quit();
    }

    public void SetAmbientVolume(float value)
    {
    	audioMixer.SetFloat("AmbientVolume", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
    	audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetEffectsVolume(float value)
    {
    	audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
    }
}
