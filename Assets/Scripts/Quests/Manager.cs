using Moonshot.Items;
using Moonshot.Locations;

using System;
using System.Collections;

using UnityEngine;

namespace Moonshot.Quests {
	public class Manager : MonoBehaviour {
		#region Singleton
		public static Manager Instance { get; private set; }

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

		public Quest testQuest = new Quest("Find Scrap Steel");
		[SerializeField] private Item scrapSteelItem;
		[SerializeField] private Location rockQuarryLocation;

		private void Start() {
			BaseEvent a = testQuest.AddLocationEvent("Go to Rock Quarry", "You're pretty sure there's some useable steel in your junkyard.", rockQuarryLocation);
			BaseEvent b = testQuest.AddEvent("Check Trashcans", "Not sure why there would be scrap steel in your trash cans but maybe?");
			BaseEvent c = testQuest.AddCollectionEvent("Collect Scrap Steel", "10 pieces should be enough.", 10, scrapSteelItem);
			BaseEvent d = testQuest.AddEvent("Drop off", "Drop off the scrap steel at the build site");

			testQuest.AddPath(a.Id, c.Id);
			testQuest.AddPath(b.Id, c.Id);
			testQuest.AddPath(c.Id, d.Id);

			testQuest.BFS(a.Id);
			testQuest.BFS(b.Id);

			a.UpdateEvent(EventStatus.CURRENT);

			StartCoroutine("PrintQuest");
		}

		private IEnumerator PrintQuest() { 
			while (true) {
				testQuest.PrintPath();
				yield return new WaitForSeconds(3f);
			}
		}
	}
}
