using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class DispenserInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private DrinkSpawner drinkSpawner;

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
        drinkSpawner.Spawn();
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
