using System.Collections;
using System.Collections.Generic;
using Moonshot.GameManagement;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public Queue<string> sentences = new Queue<string>();
	private NPC npc;

	void Start() {
		GameEvents.Instance.onDialogueStarted += DialogueStarted;
	}

	private void DialogueStarted(NPC _npc) {
		npc = _npc;
		StartDialogue();
	}

	public void StartDialogue(string name, Dialogue dialogue) {
		if (name == "") {
			EndDialogue();
			return;
		}
		animator.SetBool("IsOpen", true);
		nameText.text = name;

		sentences.Clear();

		foreach (string sentence in npc.dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		// TODO: Be able to exit dialogue on ESC
		// TODO: If count is one, change the text on the button
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";
		foreach (char letter in sentence) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue() {
		animator.SetBool("IsOpen", false);
		GameEvents.Instance.DialogueEnded(npc);
	}
}
