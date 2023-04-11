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
    public float fadeAnimationDuration;
    public AnimationCurve fadeAnimation;
    public CanvasGroup canvasGroup;
    public GameObject loadingScreen;

    private float timer;
    private bool fadeIn;
    private bool ignoreInput;

	void Awake()
	{
        timer = 0;
        fadeIn = true;
        Time.timeScale = 1f;
        Cursor.visible = true;
		if (SavesManager.IsSavePresent(SavesManager.GetSaveFilePath(saveFileName)))
			continueButton.interactable = true;
		else
            continueButton.interactable = false;
        ignoreInput = false;
    }

    void Update()
    {
        if(fadeIn && timer < fadeAnimationDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, fadeAnimation.Evaluate(timer / fadeAnimationDuration));
        }

        if(!fadeIn && timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
                timer = 0;
            canvasGroup.alpha = Mathf.Lerp(0, 1, fadeAnimation.Evaluate(timer / fadeAnimationDuration));
        }
    }

	public void NewGame()
    {
        if(ignoreInput)
            return;
        ignoreInput = true;

        SavesManager.DeleteSaves(saveFileName);
        SavesManager.DeleteSaves(savedBlackboardTextureFileName);
        loadingScreen.SetActive(true);
        fadeIn = false;
        StartCoroutine(LoadMainScene());
    }

    public void Continue()
    {
        if(ignoreInput)
            return;
        ignoreInput = true;

        loadingScreen.SetActive(true);
        fadeIn = false;
        StartCoroutine(LoadMainScene());
    }

    public void Exit()
    {
        if(ignoreInput)
            return;
        ignoreInput = true;
        
        Application.Quit();
    }

    private IEnumerator LoadMainScene()
    {
        yield return new WaitForSecondsRealtime(fadeAnimationDuration);
        SceneManager.LoadScene("MainScene");
    }
}
