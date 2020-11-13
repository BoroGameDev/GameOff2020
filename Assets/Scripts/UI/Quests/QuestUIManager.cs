using UnityEngine;

namespace Moonshot.UI.Quests {
	public class QuestUIManager : MonoBehaviour {
		private Radar radarSystem;

		private void Awake() {
			radarSystem = GetComponentInChildren<Radar>();
		}
	}
}
