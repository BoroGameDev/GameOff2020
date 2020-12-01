using Moonshot.GameManagement;
using Moonshot.Quests;

using UnityEngine;

namespace Moonshot.Player {

	public class Controller : MonoBehaviour {
		[SerializeField] private DialogueManager dialogueManager;
		[SerializeField] private Dialogue dialogue;

		private void Start() {
			GameManager.Instance.SetPlayer(gameObject);
			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		public void OnOpeningCutsceneFinished() {
			dialogueManager.StartDialogue(dialogue);
		}

		void DialogueEnded(NPC _npc) {
			if (_npc != null) { return; }

			Debug.Log("Starting Quests");
			QuestManager.Instance.SetupQuests();
		}
	}

}
