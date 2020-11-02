using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moonshot.GameManagement {

	public class GameManager : MonoBehaviour {
		#region Singleton
		private static GameManager instance = null;
		private static readonly object padlock = new object();

		GameManager() { }

		public static GameManager Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new GameManager();
					}
					return instance;
				}
			}
		}
		#endregion

		#region Unity Methods
		private void Awake() {
			SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCENE, LoadSceneMode.Additive);
		}
		#endregion
	}

}