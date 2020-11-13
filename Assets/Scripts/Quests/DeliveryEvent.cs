using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

using UnityEngine;

namespace Moonshot.Quests {
	[CreateAssetMenu(fileName = "New Delivery Event", menuName = "Quests/Delivery Event")]
	public class DeliveryEvent : BaseEvent {

		public NPC npc;
		public Item item;
		public Dialogue successDialogue;
		public Dialogue failDialogue;

		public override void Init() {
			GameEvents.Instance.onDialogueStarted += DialogueStarted;
		}

		protected void DialogueStarted(NPC _npc) {
			if (!Parent.started) { return; }
			if (_npc != npc) { return; }
			if (!Inventory.Instance.Contains(item)) {
				_npc.dialogue = failDialogue;
				return; 
			}

			_npc.dialogue = successDialogue;
			Inventory.Instance.Remove(item);
			this.UpdateEvent(EventStatus.DONE);

		}
	}
}
