using System.Collections;
using System.Collections.Generic;
using Moonshot.GameManagement;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public Queue<Sentence> sentences = new Queue<Sentence>();
	private NPC npc;

	void Awake() {
		GameEvents.Instance.onDialogueStarted += DialogueStarted;
		GameEvents.Instance.onDialogueOpen += DialogueOpen;
	}

	private void OnDestroy() {
		GameEvents.Instance.onDialogueStarted -= DialogueStarted;
		GameEvents.Instance.onDialogueOpen -= DialogueOpen;
	}

	private void DialogueStarted(NPC _npc) {
		npc = _npc;
	}

	private void DialogueOpen() { 
		StartDialogue(npc.dialogue);
	}

	public void StartDialogue(Dialogue _dialogue) {
		animator.SetBool("IsOpen", true);

		sentences.Clear();

		foreach (Sentence sentence in _dialogue.sentences) {
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

		Sentence sentence = sentences.Dequeue();
		nameText.text = sentence.Name;
		dialogueText.text = sentence.Lines;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence.Lines));
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
		npc = null;
	}
}
