using Moonshot.GameManagement;
using Moonshot.Items;
using Moonshot.Locations;

using System;
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
			SetupQuest();
		}
		#endregion

		public Quest testQuest = new Quest("Building the Yeet Cannon");
		[SerializeField] private NPC OldMan;

		[Header("Quest Event Variables")]
		[SerializeField] private Location _location;
		[SerializeField] private Item _frame;
		[SerializeField] private Dialogue _frameEventSuccess;
		[SerializeField] private Dialogue _frameEventFail;

		private bool questStarted = false;

		private void SetupQuest() {
			BaseEvent a = testQuest.AddEvent(new LocationEvent(testQuest, "Go play in the grass", "That'll be nice.. plus you might find a frame there", _location));
			BaseEvent b = testQuest.AddEvent(new CollectionEvent(testQuest, "Find car frame", "Well the grass was nice... now to find a car frame", _frame));
			BaseEvent c = testQuest.AddEvent(new DeliveryEvent(testQuest, "Bring frame back to Old Man River", "Follow the sounds of his banjo", _frame, OldMan, _frameEventSuccess, _frameEventFail));

			testQuest.AddPath(a.Id, b.Id);
			testQuest.AddPath(b.Id, c.Id);

			testQuest.BFS(a.Id);

			var deliveryEvents = testQuest.events.FindAll(_e => _e is DeliveryEvent);
			foreach (DeliveryEvent _e in deliveryEvents) {
				_e.npc = OldMan.GetComponent<NPC>();
			}

			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		private void DialogueEnded(NPC _npc) {
			if (_npc != OldMan) { return; }
			if (questStarted) { return; }

			testQuest.Start();
			questStarted = true;
			StartCoroutine("PrintQuest");
		}

		public Quest GetCurrentQuest() {
			return testQuest;
		}

		public List<BaseEvent> GetCurrentEvents() {
			return testQuest.events.FindAll(_e => _e.Status == EventStatus.CURRENT);
		}

		private IEnumerator PrintQuest() { 
			while (!testQuest.completed) {
				testQuest.PrintPath();
				yield return new WaitForSeconds(3f);
			}
		}
	}
}
