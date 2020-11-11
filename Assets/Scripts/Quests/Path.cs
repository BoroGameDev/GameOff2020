namespace Moonshot.Quests {
	public class Path {
		public BaseEvent startEvent;
		public BaseEvent endEvent;

		public Path(BaseEvent _from, BaseEvent _to) {
			startEvent = _from;
			endEvent = _to;
		}
	}
}
