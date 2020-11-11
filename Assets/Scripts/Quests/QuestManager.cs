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

		public Quest testQuest = new Quest("Find Frame");
		[Header("Quest Items")]
		[SerializeField] private Item scrapSteelItem;
		[Header("Quest Locations")]
		[SerializeField] private Location rockQuarryLocation;

		[Header("Quest Dialogue")]
		[SerializeField] private NPC OldMan;

		[SerializeField] private Dialogue successDialogue;
		[SerializeField] private Dialogue failDialogue;

		private bool questStarted = false;

		private void SetupQuest() {
			BaseEvent a = testQuest.AddEvent(new LocationEvent(testQuest, "Visit the grass patch", "", rockQuarryLocation));
			BaseEvent b = testQuest.AddEvent(new CollectionEvent(testQuest, "Collect Frame", "You should be able to find the useable frame in one of those cars", 1, scrapSteelItem));
			BaseEvent c = testQuest.AddEvent(new DeliveryEvent(testQuest, "Drop off", "Drop off the scrap steel at the build site", scrapSteelItem, OldMan, successDialogue, failDialogue));

			testQuest.AddPath(a.Id, b.Id);
			testQuest.AddPath(b.Id, c.Id);

			testQuest.BFS(a.Id);

			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		private void DialogueEnded(NPC _npc) {
			if (_npc != OldMan) { return; }
			if (questStarted) { return; }

			testQuest.Start();
			questStarted = true;
			StartCoroutine("PrintQuest");
		}

		private IEnumerator PrintQuest() { 
			while (!testQuest.completed) {
				testQuest.PrintPath();
				yield return new WaitForSeconds(3f);
			}
		}
	}
}
