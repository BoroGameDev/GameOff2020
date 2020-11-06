﻿using Moonshot.GameManagement;
using UnityEngine;

public class NPC : Interactable {

	private string[] dialogues = { "Hello.", "Goodbye.", "I am an NPC!" };
	public Dialogue dialogue;
	public override void Interact() {
		/*
			TODO:
			- check if quest in progress
			- if yes
				- either use reminder of quest or use default dialogue
			- if not
				- get related quest (If multiple, grab the first)
				- check if can do quest (Existing dependencies)
				- init quest dialog
			- 
		*/
		// FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		DialogueManager.Instance.StartDialogue(dialogue);
		GameEvents.Instance.DialogueStarted();
		// Respond();
	}

	public virtual void Respond(string message = "") {
		if (message.Length == 0) {
			if (dialogues.Length == 0) {
				return;
			}
			int idx = Random.Range(0, dialogues.Length);
			message = dialogues[idx];
		}
		Debug.Log(message);
	}
}