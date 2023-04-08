using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneFadeAnimation : MonoBehaviour
{
    public float fadeAnimationDuration;
    public AnimationCurve fadeAnimation;
    public Image fadeImage;

    private float timer;
    private bool fadeIn;

	void Awake()
	{
        timer = 0;
        fadeIn = true;
    }

    void Update()
    {
        if(fadeIn && timer < fadeAnimationDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0,0,0,Mathf.Lerp(1, 0, fadeAnimation.Evaluate(timer / fadeAnimationDuration)));
        }
        if(fadeIn && timer >= fadeAnimationDuration)
        	fadeImage.gameObject.SetActive(false);

        if(!fadeIn && timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
                timer = 0;
            fadeImage.color = new Color(0,0,0,Mathf.Lerp(1, 0, fadeAnimation.Evaluate(timer / fadeAnimationDuration)));
        }
    }

    public void FadeOut()
    {
    	fadeIn = false;
    	fadeImage.gameObject.SetActive(true);
    }

}
