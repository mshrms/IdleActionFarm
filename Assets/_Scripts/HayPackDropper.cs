using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventsNamespace.EventBus;

public class HayPackDropper : MonoBehaviour
{
    public float dropForce;
	public GameObject hayPackPrefab;

	private void OnEnable()
	{
		//onWheatCut += DropHayPack;
	}

	private void OnDisable()
	{
		//onWheatCut -= DropHayPack;
	}

    public void DropHayPack()
	{
		GameObject hayPackInstance = Instantiate(hayPackPrefab, transform.position, Quaternion.identity);
		Rigidbody rb = hayPackInstance.GetComponent<Rigidbody>();

		//Vector3 dropDirection = new Vector3(Random.Range(10, 40), dropForce, Random.Range(10, 40));

		Vector3 dropDirection = Vector3.up * dropForce;
		rb.AddForce(dropDirection, ForceMode.Impulse);
	}
}
