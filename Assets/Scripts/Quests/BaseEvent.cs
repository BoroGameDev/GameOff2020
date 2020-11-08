using System.Collections.Generic;

namespace Moonshot.Quests {

	public enum EventStatus { WAITING, CURRENT, DONE }

	public class BaseEvent {

		public string Id { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public EventStatus Status { get; protected set; }
		public List<Path> Paths { get; protected set; }
		public int Order = -1;

		public BaseEvent(string _name, string _description) {
			Id = System.Guid.NewGuid().ToString();
			Name = _name;
			Description = _description;
			Paths = new List<Path>();
			Status = EventStatus.WAITING;
		}

		public void UpdateEvent(EventStatus _status) {
			Status = _status;
		}
	}
}
