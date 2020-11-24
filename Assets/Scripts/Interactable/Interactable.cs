using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour {

	[SerializeField]
	private InteractableSprites interactableSprites = new InteractableSprites();

	SpriteRenderer spriteRenderer;

	private CircleCollider2D viewableScope;

	private bool highlighted = false;
	private Canvas highlightUI;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = interactableSprites.Original;
		highlightUI = GetComponentInChildren<Canvas>();
		highlightUI.gameObject.SetActive(false);
	}

	public virtual void Interact() {
		Debug.Log("Interaction not implemented");
	}

	public void Highlight() {
		highlightUI.gameObject.SetActive(true);
		if (interactableSprites.Highlighted != null) {
			spriteRenderer.sprite = interactableSprites.Highlighted;
			highlighted = true;
		}
	}

	public void Unhighlight() {
		highlightUI.gameObject.SetActive(false);
		if (highlighted) {
			spriteRenderer.sprite = interactableSprites.Original;
			highlighted = false;
		}
	}
}
