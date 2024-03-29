using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class TVInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Renderer screenRenderer;
	[SerializeField]
	private Material tvOnMaterial;

    private Material currentMaterial;
    private Material tvOffMaterial;

    private bool tvOn;

    void Awake()
    {
        currentMaterial = new Material(screenRenderer.material);
        tvOffMaterial = new Material(screenRenderer.material);

        tvOn = false;
	}

	public void StartHovering()
    {
        screenRenderer.material.color = currentMaterial.color * 2f;
    }

	public void Interact()
	{
        SetTVState(!tvOn);
        GameManager.Instance.SetTVState(this, tvOn);
        screenRenderer.material.color = currentMaterial.color * 2f;
    }

	public void StopHovering()
    {
        screenRenderer.material.color = currentMaterial.color;
    }

    public void SetTVState(bool tvOn)
    {
        this.tvOn = tvOn;
        currentMaterial = tvOn ? tvOnMaterial : tvOffMaterial;
        screenRenderer.material = currentMaterial;
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
