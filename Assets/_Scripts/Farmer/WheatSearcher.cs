using UnityEngine;
using static EventsNamespace.EventBus;

public class WheatSearcher : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if (other.GetComponent<Wheat>().isGrowing == false)
		{
			onWheatFound?.Invoke();
		}
	}
}
