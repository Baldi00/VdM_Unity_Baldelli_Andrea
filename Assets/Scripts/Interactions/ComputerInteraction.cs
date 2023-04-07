using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform monitorStation;
	[SerializeField]
	private Material monitorOnMaterial;

    private Renderer[] screens;
    private Renderer rend;
    private Material material;
    private Material monitorOffMaterial;

    private bool pcAndMonitorOn;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);

        screens = monitorStation.GetComponentsInChildren<Renderer>().Where<Renderer>(x => x.name == "Screen").ToArray<Renderer>();

        monitorOffMaterial = new Material(screens[0].material);
        pcAndMonitorOn = false;
	}

	public void StartHovering()
    {
        rend.material.color = material.color * 2f;
    }

	public void Interact()
	{
        SetPCState(!pcAndMonitorOn);
        GameManager.Instance.SetPCState(this, pcAndMonitorOn);
	}

	public void StopHovering()
    {
        rend.material.color = material.color;
    }

    public void SetPCState(bool pcAndMonitorOn)
    {
        this.pcAndMonitorOn = pcAndMonitorOn;
        foreach (Renderer screen in screens)
            screen.material = pcAndMonitorOn ? monitorOnMaterial : monitorOffMaterial;
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
