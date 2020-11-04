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

		[SerializeField] private GameObject LoadingScreen;
		[SerializeField] private ProgressBar m_ProgressBar;

		List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
		float TotalProgress;

		#region Unity Methods
		private void Start() {
			SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
		}
		#endregion

		#region Custom Methods
		public void LoadGame() {
			LoadingScreen.SetActive(true);

			scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
			scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TEST_LEVEL, LoadSceneMode.Additive));

			StartCoroutine(GetSceneLoadProgress());
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

			LoadingScreen.SetActive(false);
		}
		#endregion

	}

}