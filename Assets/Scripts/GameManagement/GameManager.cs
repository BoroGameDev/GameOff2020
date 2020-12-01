using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Moonshot.UI;

namespace Moonshot.GameManagement {

	public class GameManager : MonoBehaviour {
		#region Singleton
		public static GameManager Instance { get; private set; }

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

		private bool paused = false;
		public bool Paused { get { return paused; } }

		private SceneIndexes currentScene;
		public SceneIndexes CurrentScene { get { return currentScene; } }

		public GameObject Player { get; private set; }

		[SerializeField] private GameObject LoadingScreen;
		[SerializeField] private ProgressBar m_ProgressBar;

		List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
		float TotalProgress;

		#region Unity Methods
		private void Start() {
			SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
			currentScene = SceneIndexes.TITLE_SCREEN;
		}

		private void Update() {
			if (currentScene != SceneIndexes.TEST_LEVEL) { return; }

			if (Input.GetButtonDown("Cancel")) {
				if (paused) {
					UnpauseGame();
				} else {
					PauseGame();
				}
			}
		}
		#endregion

		#region Custom Methods
		public void PauseGame() {
			SceneManager.LoadSceneAsync((int)SceneIndexes.PAUSE_MENU, LoadSceneMode.Additive);
			currentScene = SceneIndexes.PAUSE_MENU;
			paused = true;
		}

		public void UnpauseGame() {
			SceneManager.UnloadSceneAsync((int)SceneIndexes.PAUSE_MENU);
			currentScene = SceneIndexes.TEST_LEVEL;
			paused = false;
		}

		public void SetPlayer(GameObject _player) {
			Player = _player;
		}

		public void LoadGameScene() {
			LoadingScreen.SetActive(true);

			scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
			scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TEST_LEVEL, LoadSceneMode.Additive));

			StartCoroutine(GetSceneLoadProgress());

			//WorldState state = LoadGame();
			//if (state != null) {
			//	Player.transform.position = new Vector3(
			//		state.playerPosition[0],
			//		state.playerPosition[1],
			//		state.playerPosition[2]
			//		);
			//	Inventory.Instance.items = state.items;
			//}

			currentScene = SceneIndexes.TEST_LEVEL;
			GameEvents.Instance.SceneLoaded(SceneIndexes.TEST_LEVEL);
			LoadingScreen.SetActive(false);
		}

		public IEnumerator GetSceneLoadProgress() {
			for (int i = 0; i < scenesLoading.Count; i++) {
				while (!scenesLoading[i].isDone) {
					TotalProgress = 0;

					foreach (AsyncOperation operation in scenesLoading) {
						TotalProgress += operation.progress;
					}

					TotalProgress = (TotalProgress / scenesLoading.Count) * 100f;

					m_ProgressBar.Current = Mathf.RoundToInt(TotalProgress);
					yield return null;
				}
			}

		}

		public void ToTitle() {
			LoadingScreen.SetActive(true);

			//SaveGame(Player.transform.position);

			scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.PAUSE_MENU));
			scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TEST_LEVEL));
			scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive));

			StartCoroutine(GetSceneLoadProgress());
			currentScene = SceneIndexes.TITLE_SCREEN;
			GameEvents.Instance.SceneLoaded(SceneIndexes.TITLE_SCREEN);
		}

		public void QuitGame() {
			//SaveGame(Player.transform.position);
			Application.Quit();
		}

		//public void SaveGame(Vector3 playerPosition) {
		//	BinaryFormatter formatter = new BinaryFormatter();
		//	string path = $"{Application.persistentDataPath}/worldstate.moonshot";
		//	FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

		//	WorldState state = new WorldState(playerPosition, Inventory.Instance);

		//	formatter.Serialize(stream, state);
		//	stream.Close();
		//}

		//public WorldState LoadGame() { 
		//	BinaryFormatter formatter = new BinaryFormatter();
		//	string path = $"{Application.persistentDataPath}/worldstate.moonshot";
		//	if (File.Exists(path)) {
		//		FileStream stream = new FileStream(path, FileMode.Open);
		//		WorldState state = formatter.Deserialize(stream) as WorldState;
		//		stream.Close();

		//		return state;
		//	} else {
		//		Debug.Log("No save file exists");
		//		return null;
		//	}
		//}
		#endregion

	}

}