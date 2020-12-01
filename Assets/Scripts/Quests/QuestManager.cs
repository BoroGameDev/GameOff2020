using Moonshot.GameManagement;
using Moonshot.Items;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Moonshot.Quests {
	public class QuestManager : MonoBehaviour {
		#region Singleton
		public static QuestManager Instance { get; private set; }

		void Awake() {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				DestroyImmediate(gameObject);
				return;
			}
		}
		#endregion

		public Queue<Quest> quests = new Queue<Quest>();

		[Header("Story Options")]
		[SerializeField] private NPC HanksTV;
		[SerializeField] private NPC OldMan;

		[Header("Quest 1")]
		[SerializeField] private Dialogue _talkToRickyDialogue;

		[Header("Quest 2")]
		[SerializeField] private Item _carFrameItem;
		[SerializeField] private Dialogue _carFrameSuccessDialogue;
		[SerializeField] private Dialogue _carFrameFailDialogue;

		public void SetupQuests() {
			Debug.Log("Setting Up Quests");

			quests.Enqueue(TalkToRicky());
			quests.Enqueue(FindCarFrame());

			GetCurrentQuest().Start();
			SetQuestNPC();

			GameEvents.Instance.onQuestCompleted += QuestCompleted;
		}

		private void OnDestroy() {
			GameEvents.Instance.onQuestCompleted -= QuestCompleted;
		}

		private void QuestCompleted() {
			quests.Dequeue();

			if (quests.Count > 0) { 
				SetQuestNPC();
				GetCurrentQuest().Start();
			}
		}

		private void SetQuestNPC() { 
			var deliveryEvents = GetCurrentQuest().events.FindAll(_e => _e is DeliveryEvent);
			foreach (DeliveryEvent _e in deliveryEvents) {
				_e.npc = OldMan.GetComponent<NPC>();
			}
			var dialogueEvents = GetCurrentQuest().events.FindAll(_e => _e is DialogueEvent);
			foreach (DialogueEvent _e in dialogueEvents) {
				_e.npc = OldMan.GetComponent<NPC>();
			}
		}

		public Quest GetCurrentQuest() {
			if (quests.Count == 0) { return null; }

			return quests.Peek();
		}

		public List<BaseEvent> GetCurrentEvents() {
			if (quests.Count == 0) { return null; }

			return GetCurrentQuest().events.FindAll(_e => _e.Status == EventStatus.CURRENT);
		}

		#region QuestBuilders
		private Quest TalkToRicky() {
			Quest quest = new Quest("Talk to Ricky");

			BaseEvent a = quest.AddEvent(new DialogueEvent(quest, "Ask Ricky about the Moon", "", OldMan, _talkToRickyDialogue));

			quest.BFS(a.Id);

			return quest;
		}

		private Quest FindCarFrame() {
			Quest quest = new Quest("Find Car Frame");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Find useable car", "", _carFrameItem));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Bring frame to Ricky", "", _carFrameItem, OldMan, _carFrameSuccessDialogue, _carFrameFailDialogue));

			quest.BFS(a.Id);

			return quest;
		}
		#endregion

		#region Debugging
		private IEnumerator PrintQuest() { 
			while (!GetCurrentQuest().completed) {
				GetCurrentQuest().PrintPath();
				yield return new WaitForSeconds(3f);
			}
		}
		#endregion
	}
}
