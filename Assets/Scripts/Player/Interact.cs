using UnityEngine;

namespace Moonshot.Player {
	public class Interact : MonoBehaviour {
		private BaseInput input;

		[SerializeField]
		[Range(0f, 10f)]
		private float radius = 3f;

		[SerializeField] private LayerMask InteractablesLayer;

		private bool hasCollided = false;
		private string prompt = "Press F";

		void Start() {
			input = GetComponent<BaseInput>();
		}

		void Update() {
			if (input.Interact) {
				CheckHit();
			}
		}

		void OnGUI() {
			if (hasCollided == true) {
				GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 120), prompt);
			}
		}

		void CheckHit() {
			Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, radius, InteractablesLayer);

			if (interactables.Length > 0) {
				HandleInteractions(interactables);
			}
		}

		void HandleInteractions(Collider2D[] interactables) {
			// TODO: will need to sort out which interactable to choose
			Interactable interactable = interactables[0].GetComponentInParent<Interactable>();
			if (interactable) {
				interactable.Interact();
			}
		}

		void OnTriggerEnter2D(Collider2D other) {
			Interactable interactable = other.GetComponentInParent<Interactable>();
			if (interactable) {
				hasCollided = true;
				interactable.Highlight();
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			Interactable interactable = other.GetComponentInParent<Interactable>();
			if (interactable) {
				hasCollided = false;
				interactable.Unhighlight();
			}
		}
	}

}
