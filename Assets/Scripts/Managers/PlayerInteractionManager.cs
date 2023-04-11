using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInteractionManager : MonoBehaviour
{
	[SerializeField]
	private float maxDistance;

	private Ray ray;
	private IInteractable currentInteractableObject;
	private bool interact;
	private bool interactContinuously;
	private bool stopInteractingContinuously;
	private bool secondaryInteraction;

	void Update()
	{
		if(GameManager.Instance.IsPaused)
			return;

		ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~LayerMask.NameToLayer("Interactable")))
		{
			if (currentInteractableObject == null)
			{
				currentInteractableObject = hit.collider.GetComponent<IInteractable>();
				currentInteractableObject.StartHovering();
			}
			else if(currentInteractableObject != hit.collider.GetComponent<IInteractable>())
			{
                currentInteractableObject.StopHovering();
                currentInteractableObject = hit.collider.GetComponent<IInteractable>();
                currentInteractableObject.StartHovering();
            }
		}
		else if(currentInteractableObject != null)
		{
			currentInteractableObject.StopHovering();
			currentInteractableObject = null;
		}

        if (interact && currentInteractableObject != null)
            currentInteractableObject.Interact();
        if (interactContinuously && currentInteractableObject != null)
            currentInteractableObject.InteractContinuously();
        if (stopInteractingContinuously && currentInteractableObject != null)
            currentInteractableObject.StopContinuousInteraction();
        if (secondaryInteraction && currentInteractableObject != null)
            currentInteractableObject.SecondaryInteraction();
    }

	public void SetInteract(bool interact) => this.interact = interact;
	public void SetInteractContinuously(bool interactContinuously) => this.interactContinuously = interactContinuously;
	public void SetStopInteractingContinuously(bool stopInteractingContinuously) => this.stopInteractingContinuously = stopInteractingContinuously;
	public void SetSecondaryInteraction(bool secondaryInteraction) => this.secondaryInteraction = secondaryInteraction;
}