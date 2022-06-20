using UnityEngine;
using static EventsNamespace.EventBus;

public class HayPackPickuper : MonoBehaviour
{
    public FarmerInventory inventory;

	private void OnTriggerEnter(Collider other)
	{
		inventory.AddHayPack(other.gameObject);
	}
}
