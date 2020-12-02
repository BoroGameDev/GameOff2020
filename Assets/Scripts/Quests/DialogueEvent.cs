using Moonshot.GameManagement;
using Moonshot.Items;

namespace Moonshot.Quests {
	public class DialogueEvent : BaseEvent {

		public NPC npc;
		public Item item;
		public Dialogue successDialogue;

		public DialogueEvent(Quest _p, string _n, string _d, NPC _npc, Dialogue _success) : base(_p, _n, _d) {
			npc = _npc;
			successDialogue = _success;
			GameEvents.Instance.onDialogueStarted += DialogueStarted;
			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		protected void DialogueStarted(NPC _npc) {
			if (Parent.completed) { return; }
			if (!Parent.started) { return; }
			if (_npc != npc) { return; }

			GameEvents.Instance.DialogueOpen();
		}

		protected void DialogueEnded(NPC _npc) {
			if (this.Status != EventStatus.CURRENT) { return; }
			if (Parent.completed) { return; }
			if (!Parent.started) { return; }
			if (_npc != npc) { return; }

			_npc.dialogue = successDialogue;
			this.UpdateEvent(EventStatus.DONE);

		}
	}
}
