using System.Collections.Generic;

using UnityEngine;

namespace Moonshot.Quests {

	public enum EventStatus { WAITING, CURRENT, DONE }


	[CreateAssetMenu(fileName = "New Quest Event", menuName = "Quests/Event")]
	public class BaseEvent : ScriptableObject {

		[HideInInspector] public Quest Parent;
		public string Id { get; protected set; }
		public string Name;
		public string Description;
		public EventStatus Status = EventStatus.WAITING;
		public List<Path> Paths = new List<Path>();
		[HideInInspector] public int Order = -1;

		public virtual void Init() {
			Id = System.Guid.NewGuid().ToString();
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
