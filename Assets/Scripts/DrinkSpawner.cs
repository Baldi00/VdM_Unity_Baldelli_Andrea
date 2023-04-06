using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DrinkSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject drinkPrefab;
    [SerializeField]
    private float force;

    public void Spawn()
	{
        GameObject drink = Instantiate(drinkPrefab, transform.position, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
        Vector3 dir = Random.onUnitSphere;
        if (Vector3.Dot(dir, transform.forward) < 0)
            dir = -dir;

        drink.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
    }
}
