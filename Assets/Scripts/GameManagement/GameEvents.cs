using Moonshot.Locations;

using System;

namespace Moonshot.GameManagement {

	public class GameEvents {
		#region Singleton
		private static GameEvents instance = null;
		private static readonly object padlock = new object();

		GameEvents() { }

		public static GameEvents Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new GameEvents();
					}
					return instance;
				}
			}
		}
		#endregion

		public event Action<NPC> onDialogueStarted;
		public void DialogueStarted(NPC _npc) {
			if (onDialogueStarted != null) {
				onDialogueStarted(_npc);
			}
		}

		public event Action<NPC> onDialogueEnded;
		public void DialogueEnded(NPC _npc) {
			if (onDialogueEnded != null) {
				onDialogueEnded(_npc);
			}
		}

		public event Action<SceneIndexes> onSceneLoaded;
		public void SceneLoaded(SceneIndexes _index) {
			if (onSceneLoaded != null) {
				onSceneLoaded(_index);
			}
		}

		public event Action onInventoryUpdated;
		public void InventoryUpdated() {
			if (onInventoryUpdated != null) {
				onInventoryUpdated();
			}
		}

		public event Action<Location> onEnteredLocation;
		public void EnteredLocation(Location _location) {
			if (onEnteredLocation != null) {
				onEnteredLocation(_location);
			}
		}

		public event Action onQuestEventCompleted;
		public void QuestEventCompleted() {
			if (onQuestEventCompleted != null) {
				onQuestEventCompleted();
			}
		}

		public event Action onQuestCompleted;
		public void QuestCompleted() {
			if (onQuestCompleted != null) {
				onQuestCompleted();
			}
		}

	}
}
