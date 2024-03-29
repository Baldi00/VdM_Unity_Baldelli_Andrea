using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AirDryerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Material airDryerOnLightMaterial;
    [SerializeField]
    private Material airDryerOffLightMaterial;
    [SerializeField]
    private float airDryerOnDuration;

    private Renderer rend;
    private Material material;
    private GameObject airDryerMiniLight;
    private GameObject airDryerLight;
    private bool isHovering;
    private bool isOn;
    private float timer;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);
        airDryerMiniLight = transform.GetChild(0).gameObject;
        airDryerLight = transform.GetChild(1).gameObject;
        isOn = false;
    }

    void Update()
    {
        if (isOn)
        {
            timer += Time.deltaTime;
            if(timer >= airDryerOnDuration)
            {
                isOn = false;
                airDryerMiniLight.GetComponent<Renderer>().material = airDryerOffLightMaterial;
                airDryerLight.SetActive(isOn);

                if (isHovering)
                    StartHovering();
            }
        }
    }

    public void StartHovering()
    {
        isHovering = true;
        if (!isOn)
            rend.material.color = material.color * 2f;
    }

    public void Interact()
    {
        if (!isOn)
        {
            isOn = true;
            airDryerLight.SetActive(isOn);
            timer = 0;
            rend.material.color = material.color;
            airDryerMiniLight.GetComponent<Renderer>().material = airDryerOnLightMaterial;
            AudioManager.Instance.PlayDryer();
        }
    }

    public void StopHovering()
    {
        isHovering = false;
        rend.material.color = material.color;
    }

    public void InteractContinuously()
    {
    }

    public void StopContinuousInteraction()
    {
    }

    public void SecondaryInteraction()
    {
    }
}
