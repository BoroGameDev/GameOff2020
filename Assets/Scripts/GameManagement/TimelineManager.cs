using UnityEngine;
using UnityEngine.Playables;

namespace Moonshot.GameManagement {

	[RequireComponent(typeof(PlayableDirector))]
	public class TimelineManager : MonoBehaviour {

		private PlayableDirector Director;
		public static TimelineManager Instance { get; private set; }

		void Awake() {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				DestroyImmediate(gameObject);
				return;
			}

			Director = GetComponent<PlayableDirector>();
		}
	}
}
