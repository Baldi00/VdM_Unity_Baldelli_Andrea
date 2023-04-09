using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private float closedAngle;
    [SerializeField]
    private float openAngle;
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private AnimationCurve animationSmoothing;
    [SerializeField]
    private bool startClosed;

    private Renderer rend;
	private Material material;

    private float currentCloseAngle;
    private float currentOpenAngle;
    private float currentAngle;
    private float animationTimer;
    private bool isClosed;

	void Awake()
	{
        rend = GetComponent<Renderer>();
        material = new Material(rend.material);
        isClosed = startClosed;
        animationTimer = animationDuration;
        currentAngle = startAngle;
    }

    void Update()
    {
        if (animationTimer < animationDuration)
        {
            animationTimer += Time.deltaTime;

            float t = Mathf.Min(animationTimer / animationDuration, 1f);

            if (isClosed)
                currentAngle = Mathf.Lerp(currentOpenAngle, currentCloseAngle, animationSmoothing.Evaluate(t));
            else
                currentAngle = Mathf.Lerp(currentCloseAngle, currentOpenAngle, animationSmoothing.Evaluate(t));

            transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, currentAngle, transform.parent.rotation.eulerAngles.z);
        }
    }

    public void StartHovering()
	{
        rend.material.color = material.color * 2f;
    }

	public void Interact()
	{
        isClosed = !isClosed;
        GameManager.Instance.SetDoorState(this, isClosed);

        if (isClosed)
        {
            currentOpenAngle = currentAngle;
            currentCloseAngle = closedAngle;
            AudioManager.Instance.PlayDoorClose();
        }
        else
        {
            currentOpenAngle = openAngle;
            currentCloseAngle = currentAngle;
            AudioManager.Instance.PlayDoorOpen();
        }

        animationTimer = 0;
    }

	public void StopHovering()
    {
        rend.material.color = material.color;
    }
    public void SetDoorState(bool isClosed)
    {
        this.isClosed = isClosed;

        if (isClosed)
        {
            transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, closedAngle, transform.parent.rotation.eulerAngles.z);
            startAngle = closedAngle;
        }
        else
        {
            transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, openAngle, transform.parent.rotation.eulerAngles.z);
            startAngle = openAngle;
        }
        currentAngle = startAngle;
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
