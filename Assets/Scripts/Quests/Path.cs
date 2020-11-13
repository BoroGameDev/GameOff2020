namespace Moonshot.Quests {
	[System.Serializable]
	public class Path {
		public BaseEvent startEvent;
		public BaseEvent endEvent;

		public Path(BaseEvent _from, BaseEvent _to) {
			startEvent = _from;
			endEvent = _to;
		}
	}
}
