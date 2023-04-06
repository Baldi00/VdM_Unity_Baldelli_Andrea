using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HandWasherInteraction : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Material material;
    private ParticleSystem water;
    private bool isOpened;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);
        water = transform.GetChild(0).GetComponent<ParticleSystem>();
        isOpened = false;
    }

    public void StartHovering()
    {
        rend.material.color = material.color * 2f;
    }

    public void Interact()
    {
        isOpened = !isOpened;
        if (isOpened)
            water.Play();
        else
            water.Stop();
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
