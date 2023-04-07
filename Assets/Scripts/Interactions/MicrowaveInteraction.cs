using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MicrowaveInteraction : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Material material;
    private GameObject microwaveLight;
    private bool isOn;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);
        microwaveLight = transform.GetChild(0).gameObject;
        isOn = false;
    }

    public void StartHovering()
    {
        rend.material.color = material.color * 2f;
    }

    public void Interact()
    {
        isOn = !isOn;
        microwaveLight.SetActive(isOn);
    }

    public void StopHovering()
    {
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
