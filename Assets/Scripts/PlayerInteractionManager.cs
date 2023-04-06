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

	void Update()
	{
		ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~LayerMask.NameToLayer("Interactable")))
		{
			if (currentInteractableObject == null)
			{
				currentInteractableObject = hit.collider.GetComponent<IInteractable>();
				currentInteractableObject.StartHovering();
			}
		}
		else if(currentInteractableObject != null)
		{
			currentInteractableObject.StopHovering();
			currentInteractableObject = null;
		}

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentInteractableObject != null)
            currentInteractableObject.Interact();
        if (Input.GetKey(KeyCode.Mouse0) && currentInteractableObject != null)
            currentInteractableObject.InteractContinuously();
        if (Input.GetKeyUp(KeyCode.Mouse0) && currentInteractableObject != null)
            currentInteractableObject.StopContinuousInteraction();
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentInteractableObject != null)
            currentInteractableObject.SecondaryInteraction();
    }
}
