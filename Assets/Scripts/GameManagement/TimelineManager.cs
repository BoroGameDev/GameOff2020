using System.Collections;

using UnityEngine;
using UnityEngine.Playables;

namespace Moonshot.GameManagement {

	[RequireComponent(typeof(PlayableDirector))]
	public class TimelineManager : MonoBehaviour {

		private PlayableDirector Director;
		public static TimelineManager Instance { get; private set; }

		[SerializeField] private NPC HanksTV = null;

		private bool played = false;

		void Awake() {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				DestroyImmediate(gameObject);
				return;
			}

			Director = GetComponent<PlayableDirector>();
			GameEvents.Instance.onDialogueEnded += DialogueEnded;
		}

		private void Start() {
			StartCoroutine("StartDialogue");
		}

		IEnumerator StartDialogue() {
			yield return new WaitForSeconds(2f);

			GameEvents.Instance.DialogueStarted(HanksTV);
		}

		void DialogueEnded(NPC _npc) {
			if (played || _npc != HanksTV) { return; }

			Director.Play();
			played = true;
		}
	}
}
