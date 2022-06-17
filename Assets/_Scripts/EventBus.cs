using System;

namespace EventsNamespace
{
	public static class EventBus
	{
		public static Action onRunStart;
		public static Action onRunStop;

		public static Action onWheatFound;
		public static Action onWheatCut;

		public static Action onAttackStart;
		public static Action onAttackStop;

		//наыхе янашрхъ дкъ бяеу оьемхж пюанрючр ндмнбпелеммн
		public static Action onGrowthStart;
		public static Action onGrowthStop;
	}
}
