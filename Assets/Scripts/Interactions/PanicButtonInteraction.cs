using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PanicButtonInteraction : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private Material material;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);
    }

    public void StartHovering()
    {
        rend.material.color = material.color * 2f;
    }

    public void Interact()
    {
        AudioManager.Instance.PlayPanicButton();
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
