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
    [SerializeField]
    private bool magicDrinkSpawner;

    public void Spawn()
	{
        GameObject drink = Instantiate(drinkPrefab, transform.position, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
        Vector3 dir = Random.onUnitSphere;
        if (Vector3.Dot(dir, transform.forward) < 0)
            dir = -dir;

        drink.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        drink.GetComponent<Renderer>().material.color = color;

        if (magicDrinkSpawner)
            drink.GetComponent<Light>().color = color;

        DrinkInfo drinkInfo = new()
        {
            color = color,
            isMagic = magicDrinkSpawner
        };

        GameManager.Instance.AddDrink(drink.transform, drinkInfo);
    }
}
