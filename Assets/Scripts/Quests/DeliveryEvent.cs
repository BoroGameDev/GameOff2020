using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

namespace Moonshot.Quests {
	public class DeliveryEvent : BaseEvent {

		public NPC npc { get; protected set; }
		public Item item { get; protected set; }
		public Dialogue successDialogue;
		public Dialogue failDialogue;

		public DeliveryEvent(Quest _p, string _n, string _d, Item _item, NPC _npc, Dialogue _success, Dialogue _fail) : base(_p, _n, _d) {
			npc = _npc;
			item = _item;

			successDialogue = _success;
			failDialogue = _fail;

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
