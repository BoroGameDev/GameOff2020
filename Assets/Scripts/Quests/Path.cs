namespace Moonshot.Quests {
	public class Path {
		public QuestEvent startEvent;
		public QuestEvent endEvent;

		public Path(QuestEvent _from, QuestEvent _to) {
			startEvent = _from;
			endEvent = _to;
		}
	}
}
