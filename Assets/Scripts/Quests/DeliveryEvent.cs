using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

namespace Moonshot.Quests {
	public class DeliveryEvent : BaseEvent {

		public NPC npc;
		public Item item;
		public int CurrentAmount = 0;
		public int RequiredAmount = 1;
		public Dialogue successDialogue;
		public Dialogue failDialogue;
		private bool delivered = false;

		public DeliveryEvent(Quest _p, string _n, string _d, Item _item, NPC _npc, int _requiredAmount, Dialogue _success, Dialogue _fail) : base(_p, _n, _d) {
			npc = _npc;
			item = _item;
			successDialogue = _success;
			failDialogue = _fail;
			RequiredAmount = _requiredAmount;
			GameEvents.Instance.onDialogueStarted += DialogueStarted;
			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		protected void DialogueEnded(NPC _npc) { 
			if (Parent.completed) { return; }
			if (!Parent.started) { return; }
			if (_npc != npc) { return; }
			if (Status == EventStatus.DONE) { return; }
			if (!delivered) { return; }

			this.UpdateEvent(EventStatus.DONE);
		}

		protected void DialogueStarted(NPC _npc) {
			if (Parent.completed) { return; }
			if (!Parent.started) { return; }
			if (_npc != npc) { return; }
			if (!IsRequirementSatisfied()) {
				_npc.dialogue = failDialogue;
				GameEvents.Instance.DialogueOpen();
				return; 
			}

			_npc.dialogue = successDialogue;
			for (int i = 0; i < RequiredAmount; i++) {
				Inventory.Instance.Remove(item);
			}
			GameEvents.Instance.DialogueOpen();
			delivered = true;

		}

		protected override bool IsRequirementSatisfied() {
			CurrentAmount = Inventory.Instance.CountItems(item);
			return CurrentAmount >= RequiredAmount;
		}
	}
}
