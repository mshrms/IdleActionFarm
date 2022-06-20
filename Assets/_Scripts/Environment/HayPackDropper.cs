using UnityEngine;
using static EventsNamespace.EventBus;

public class HayPackDropper : MonoBehaviour
{
    public float verticalDropForce;
    public float horizontalDropForceRange;
	public GameObject hayPackPrefab;

    public void DropHayPack()
	{
		GameObject hayPackInstance = Instantiate(hayPackPrefab, transform.position, Quaternion.identity);
		Rigidbody rb = hayPackInstance.GetComponent<Rigidbody>();

		Vector3 dropDirection = new Vector3(
			Random.Range(-horizontalDropForceRange, horizontalDropForceRange), 
			verticalDropForce, 
			Random.Range(-horizontalDropForceRange, horizontalDropForceRange)
			);

		rb.AddForce(dropDirection, ForceMode.Impulse);
		rb.AddTorque(dropDirection, ForceMode.Impulse);
	}
}
