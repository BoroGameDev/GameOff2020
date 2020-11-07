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

		public Quest testQuest = new Quest();

		private void Start() {
			QuestEvent a = testQuest.AddEvent("Discover Moonlight Mine", "You remember stories of moonlight cave from your childhood.");
			QuestEvent b = testQuest.AddEvent("Jump down shaft", "From what you remember it shouldn't be too deep.");
			QuestEvent c = testQuest.AddEvent("Find another route", "Maybe it's not worth the jump afterall...");
			QuestEvent d = testQuest.AddEvent("Discover Mine", "Moonstone should be in the mine right?");
			QuestEvent e = testQuest.AddEvent("Collect moonstone ore", "This is hard enough to withstand impact with the moon!");

			testQuest.AddPath(a.Id, b.Id);
			testQuest.AddPath(a.Id, c.Id);
			testQuest.AddPath(b.Id, d.Id);
			testQuest.AddPath(c.Id, d.Id);
			testQuest.AddPath(d.Id, e.Id);

			testQuest.BFS(a.Id);

			testQuest.PrintPath();
		}
	}
}
