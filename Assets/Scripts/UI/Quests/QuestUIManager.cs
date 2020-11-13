using Moonshot.Quests;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Moonshot.UI.Quests {
	public class QuestUIManager : MonoBehaviour {
		private Radar radarSystem;

		[SerializeField] private Text QuestName;
		[SerializeField] private Text QuestEvents;

		private Quest currentQuest;
		private List<BaseEvent> currentQuestEvents;

		private void Start() {
			radarSystem = GetComponentInChildren<Radar>();
		}

		private void Update() {
			//HandleRadar();
			HandleQuest();
			HandleQuestEvents();
		}

		private void HandleQuestEvents() {
			currentQuestEvents = QuestManager.Instance.GetCurrentEvents();
			if (currentQuestEvents == null) {
				QuestEvents.gameObject.SetActive(false);
				return; 
			}

			QuestEvents.gameObject.SetActive(true);
			string output = "";
			foreach (BaseEvent _e in currentQuestEvents) {
				output += $"{_e.Name}\n";
			}
			QuestEvents.text = output;
		}

		private void HandleQuest() {
			currentQuest = QuestManager.Instance.GetCurrentQuest();
			if (currentQuest == null || (!currentQuest.started && !currentQuest.completed)) {
				QuestName.gameObject.SetActive(false);
				return; 
			}

			QuestName.gameObject.SetActive(true);
			string output = "";
			output = $"{currentQuest.name}";
			QuestName.text = output;
		}

		//private void HandleRadar() {
		//	radarSystem.destination = currentQuestEvents[0].GetDestination();
		//}
	}
}
