using System;

namespace EventsNamespace
{
	public static class EventBus
	{
		public static Action onRunStart;
		public static Action onRunStop;

		public static Action onWheatFound;
		public static Action onWheatCut;

		public static Action onHayPickup;

		public static Action onInventoryFull;

		public static Action onAttackStart;
		public static Action onAttackStop;

		public static Action onGrowthStart;
		public static Action onGrowthStop;
	}
}
