using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonshot.Player {
	public class Interact : MonoBehaviour {
		public Interactable currentInteractable = null;
		private BaseInput input;
		private Vector3 playerFacing;
		private int maxDistance = 10;
		private bool coolDown = false;
		private float coolDownDuration = 2f;

		void Start() {
			input = GetComponent<BaseInput>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Interactable")) {
				return;
			}

			other.GetComponentInParent<Interactable>().Interact();
		}

		void OnTriggerExit2D(Collider2D other) {
			currentInteractable = null;
		}

		void FixedUpdate() {
			Vector3 dir = new Vector3(input.Horizontal, input.Vertical, 0);
			if (dir == Vector3.zero) {
				dir = playerFacing;
			} else {
				playerFacing = dir;
			}

			if (input.Interact && !coolDown) {
				CheckHit(dir);
			}
		}

		void ResetCooldown() {
			coolDown = false;
		}

		void CheckHit(Vector3 dir) {
			if (!coolDown) {
				coolDown = true;
				Invoke("ResetCooldown", coolDownDuration);
			}
			/*
                NOTE: hitting self is prevented by unchecking "Project Settings > Physics2D > Queries Start In Colliders"
                This can also be avoided by using layers which are interactable.
                
                See https://stackoverflow.com/a/24577236/6650850.
            */

			// TODO: need to cast multiple rays for small objects
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDistance);

			Debug.DrawRay(transform.position, dir * maxDistance, Color.red, 1f);

			if (hit) {
				Debug.Log("hit: " + hit.collider.name);
				Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
				if (interactable) {
					interactable.Interact();
				}
			} else {
				Debug.Log("miss");
			}

			Debug.Log(transform.position);
			Debug.Log(dir);
		}
	}

}
