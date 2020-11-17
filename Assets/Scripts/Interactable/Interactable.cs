using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour {

	[SerializeField]
	private InteractableSprites interactableSprites = new InteractableSprites();

	SpriteRenderer spriteRenderer;

	private CircleCollider2D viewableScope;

	private bool highlighted = false;

	void Start() {
		viewableScope = GetComponent<CircleCollider2D>();
		viewableScope.isTrigger = true;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = interactableSprites.Original;
	}

	public virtual void Interact() {
		Debug.Log("Interaction not implemented");
	}

	public void Highlight() {
		if (interactableSprites.Highlighted != null) {
			spriteRenderer.sprite = interactableSprites.Highlighted;
			highlighted = true;
		}
	}

	public void Unhighlight() {
		if (highlighted) {
			spriteRenderer.sprite = interactableSprites.Original;
			highlighted = false;
		}
	}
}
