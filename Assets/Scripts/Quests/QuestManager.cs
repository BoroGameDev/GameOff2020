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

		public Quest testQuest;
		[SerializeField] private NPC OldMan;

		private bool questStarted = false;

		private void SetupQuest() {
			testQuest.Init();
			testQuest.BFS(testQuest.events[0].Id);

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
