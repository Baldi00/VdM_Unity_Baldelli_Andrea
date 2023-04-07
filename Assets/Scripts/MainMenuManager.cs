using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MainMenuManager : MonoBehaviour
{
    public string saveFileName;
    public string savedBlackboardTextureFileName;
    public Button continueButton;

	void Awake()
	{
		if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(saveFileName)))
			continueButton.interactable = true;
		else
            continueButton.interactable = false;
    }

	public void NewGame()
    {
        SavesManager.DeleteSaves(saveFileName);
        SavesManager.DeleteSaves(savedBlackboardTextureFileName);
        SceneManager.LoadScene("MainScene");
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
