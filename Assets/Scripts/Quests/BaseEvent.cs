using System.Collections.Generic;

namespace Moonshot.Quests {

	public enum EventStatus { WAITING, CURRENT, DONE }

	public class BaseEvent {

		public Quest Parent { get; protected set; }
		public string Id { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public EventStatus Status { get; protected set; }
		public List<Path> Paths { get; protected set; }
		public int Order = -1;

		public BaseEvent(Quest _parent, string _name, string _description) {
			Parent = _parent;
			Id = System.Guid.NewGuid().ToString();
			Name = _name;
			Description = _description;
			Paths = new List<Path>();
			Status = EventStatus.WAITING;
		}

		protected virtual bool IsRequirementSatisfied() {
			return Status == EventStatus.DONE;
		}

		public virtual void Start() { 
			UpdateEvent(EventStatus.CURRENT);
		}

		public void UpdateEvent(EventStatus _status) {
			Status = _status;

			if (_status == EventStatus.DONE) {
				foreach (Path p in Paths) {
					p.endEvent.Start();
				}
			}
			Parent.CheckEvents();
		}
	}
}
