﻿using UnityEngine;

namespace Moonshot.Player {
	public class Interact : MonoBehaviour {
		private BaseInput input;

		[SerializeField]
		[Range(0f, 10f)]
		private float radius = 3f;
		[SerializeField] private LayerMask InteractablesLayer;

		void Start() {
			input = GetComponent<BaseInput>();
		}

		void Update() {
			if (input.Interact) {
				Debug.Log("Interact!");
				CheckHit();
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
			Debug.Log("hit: " + interactable.name);
			if (interactable) {
				interactable.Interact();
			}
		}
	}

}
