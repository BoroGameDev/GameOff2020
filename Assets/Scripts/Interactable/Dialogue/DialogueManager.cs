using System.Collections;
using System.Collections.Generic;
using Moonshot.GameManagement;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public Queue<string> sentences;
	public static DialogueManager Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			DestroyImmediate(gameObject);
			return;
		}
	}

	void Start() {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue) {
		animator.SetBool("IsOpen", true);
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences) {
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
		GameEvents.Instance.DialogueEnded();
	}
}
