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

		[Header("Quest 3")]
		[SerializeField] private Item _propaneTanksItem;
		[SerializeField] private Dialogue _propaneTanksSuccessDialogue;
		[SerializeField] private Dialogue _propaneTanksFailDialogue;

		[Header("Quest 4")]
		[SerializeField] private Item _sewagePipesItem;
		[SerializeField] private Dialogue _sewagePipesSuccessDialogue;
		[SerializeField] private Dialogue _sewagePipesFailDialogue;

		[Header("Quest 5")]
		[SerializeField] private Item _moonshineItem;
		[SerializeField] private Dialogue _moonshineSuccessDialogue;
		[SerializeField] private Dialogue _moonshineFailDialogue;

		[Header("Quest 6")]
		[SerializeField] private Item _ductTapeItem;
		[SerializeField] private Dialogue _fixitSuccessDialogue;
		[SerializeField] private Dialogue _fixitFailDialogue;

		public void SetupQuests() {
			Debug.Log("Setting Up Quests");

			quests.Enqueue(TalkToRicky());
			quests.Enqueue(FindCarFrame());
			quests.Enqueue(FindPropaneTanks());
			quests.Enqueue(FindSewagePipes());
			quests.Enqueue(GetMoonshine());
			quests.Enqueue(GetFixitCombo());

			StartCoroutine("PrintQuest");

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
			Quest quest = new Quest("Find Cockpit");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Find useable car", "", _carFrameItem));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Bring frame to Ricky", "", _carFrameItem, OldMan, 1, _carFrameSuccessDialogue, _carFrameFailDialogue));

			quest.AddPath(a.Id, b.Id);
			quest.BFS(a.Id);

			return quest;
		}

		private Quest FindPropaneTanks() {
			Quest quest = new Quest("Find Thrusters");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Get 2 Propane Tanks", "", _propaneTanksItem, 2));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Bring tanks to Ricky", "", _propaneTanksItem, OldMan, 2, _propaneTanksSuccessDialogue, _propaneTanksFailDialogue));

			quest.AddPath(a.Id, b.Id);
			quest.BFS(a.Id);

			return quest;
		}

		private Quest FindSewagePipes() {
			Quest quest = new Quest("Find Fuel Tanks");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Get 2 Sewage Pipes", "", _sewagePipesItem, 2));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Bring tanks to Ricky", "", _sewagePipesItem, OldMan, 2, _sewagePipesSuccessDialogue, _sewagePipesFailDialogue));

			quest.AddPath(a.Id, b.Id);
			quest.BFS(a.Id);

			return quest;
		}

		private Quest GetMoonshine() {
			Quest quest = new Quest("Get Moonshine");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Collect all your moonshine", "", _moonshineItem, 5));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Give it to Ricky", "", _moonshineItem, OldMan, 5, _moonshineSuccessDialogue, _moonshineFailDialogue));

			quest.AddPath(a.Id, b.Id);
			quest.BFS(a.Id);

			return quest;
		}
		private Quest GetFixitCombo() {
			Quest quest = new Quest("Get Fix-It Combo Pack");

			BaseEvent a = quest.AddEvent(new CollectionEvent(quest, "Get DuctTape and WD-30", "", _ductTapeItem, 1));
			BaseEvent b = quest.AddEvent(new DeliveryEvent(quest, "Give it to Ricky", "", _ductTapeItem, OldMan, 1, _fixitSuccessDialogue, _fixitFailDialogue));

			quest.AddPath(a.Id, b.Id);
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
