using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class ExplosiveComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform monitorStation;
    [SerializeField]
    private Material monitorOnMaterial;
    [SerializeField]
    private ParticleSystem sparksParticles;

    private Renderer[] screens;
    private Renderer rend;
    private Material material;
    private Material monitorOffMaterial;

    private bool burnt;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);

        screens = monitorStation.GetComponentsInChildren<Renderer>().Where<Renderer>(x => x.name == "Screen").ToArray<Renderer>();

        monitorOffMaterial = new Material(screens[0].material);
        burnt = false;
	}

	public void StartHovering()
    {
        if(!burnt)
            rend.material.color = material.color * 2f;
    }

	public void Interact()
	{
        if(!burnt)
        {
            burnt = true;
            foreach (Renderer screen in screens)
                screen.material = monitorOnMaterial;
            GameManager.Instance.SetBurntPCState(true);
            AudioManager.Instance.PlayPcButtonPressed();
            rend.material.color = material.color;
            StartCoroutine(PcExplosion());
        }
	}

	public void StopHovering()
    {
        if(!burnt)
            rend.material.color = material.color;
    }

    public void SetPCState(bool burnt)
    {
        this.burnt = burnt;
        if (burnt)
            sparksParticles.gameObject.SetActive(false);
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

    private IEnumerator PcExplosion()
    {
        yield return new WaitForSeconds(2);

        foreach (Renderer screen in screens)
            screen.material = monitorOffMaterial;

        AudioManager.Instance.PlayPcExplosion();
        sparksParticles.gameObject.SetActive(false);
    }
}
