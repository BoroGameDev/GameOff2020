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

		public event Action onDialogueStarted;
		public void DialogueStarted() {
			if (onDialogueStarted != null) {
				onDialogueStarted();
			}
		}

		public event Action onDialogueEnded;
		public void DialogueEnded() {
			if (onDialogueEnded != null) {
				onDialogueEnded();
			}
		}

		public event Action<SceneIndexes> onSceneLoaded;
		public void SceneLoaded(SceneIndexes _index) {
			if (onSceneLoaded != null) {
				onSceneLoaded(_index);
			}
		}
	}
}
