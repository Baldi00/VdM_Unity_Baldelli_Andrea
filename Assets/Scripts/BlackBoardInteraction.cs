using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BlackBoardInteraction : MonoBehaviour, IInteractable
{
	[SerializeField]
	private GameObject virtualPenPrefab;
	[SerializeField]
	private float maxDistance;

	private GameObject currentPen;
	private Vector3 currentPenPosition;
	private Ray ray;
	private List<GameObject> virtualPens;

	void Awake()
	{
		virtualPens = new List<GameObject>();
	}

	public void InteractContinuously()
	{
        ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~LayerMask.NameToLayer("Interactable")))
			currentPenPosition = hit.point;

		if (currentPen == null)
		{
			currentPen = Instantiate(virtualPenPrefab, currentPenPosition, Quaternion.identity);
			virtualPens.Add(currentPen);
		}

		currentPen.transform.position = currentPenPosition;
	}

	public void StartHovering()
	{
	}

	public void Interact()
	{
	}

	public void StopContinuousInteraction()
	{
		currentPen = null;
	}

	public void StopHovering()
    {
        currentPen = null;
    }

    public void SecondaryInteraction()
    {
		foreach (GameObject virtualPen in virtualPens)
			Destroy(virtualPen);
    }
}
