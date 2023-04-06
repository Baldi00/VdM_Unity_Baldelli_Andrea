using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MirrorCameraMover : MonoBehaviour
{
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minMainCamX;
    [SerializeField]
    private float maxMainCamX;

    void Update()
	{
		transform.forward = Vector3.Reflect(Camera.main.transform.forward, Vector3.forward);
		transform.transform.position = new Vector3(Mathf.Lerp(minX, maxX, Mathf.InverseLerp(minMainCamX, maxMainCamX, Camera.main.transform.position.x)), transform.position.y, transform.position.z);
	}
}
