using Moonshot.GameManagement;
using Moonshot.Quests;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Moonshot.UI.Quests {
	[RequireComponent(typeof(AudioSource))]
	public class QuestUIManager : MonoBehaviour {
		[SerializeField] private Text QuestName;
		[SerializeField] private Text QuestEvents;

		private AudioSource mAudioSource;
		private Quest currentQuest;
		private List<BaseEvent> currentQuestEvents;

		private void Start() {
			mAudioSource = GetComponent<AudioSource>();
			GameEvents.Instance.onQuestCompleted += QuestCompleted;
		}

		private void OnDestroy() {
			GameEvents.Instance.onQuestCompleted -= QuestCompleted;
		}

		private void QuestCompleted() {
			mAudioSource.Play();
		}

		private void Update() {
			HandleQuest();
			HandleQuestEvents();
		}

		private void HandleQuestEvents() {
			currentQuestEvents = QuestManager.Instance.GetCurrentEvents();
			if (currentQuestEvents == null) {
				QuestEvents.gameObject.SetActive(false);
				return; 
			}

			string output = "";
			foreach (BaseEvent _e in currentQuestEvents) {
				output += $"{_e.Name}\n";
			}
			QuestEvents.text = output;
			QuestEvents.gameObject.SetActive(true);
		}

		private void HandleQuest() {
			currentQuest = QuestManager.Instance.GetCurrentQuest();
			if (currentQuest == null || (!currentQuest.started && !currentQuest.completed)) {
				QuestName.gameObject.SetActive(false);
				return; 
			}

			string output = "";
			output = $"{currentQuest.title}";
			QuestName.text = output;
			QuestName.gameObject.SetActive(true);
		}

		//private void HandleRadar() {
		//	radarSystem.destination = currentQuestEvents[0].GetDestination();
		//}
	}
}
