using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonshot.Player {
	public class Interact : MonoBehaviour {
		public GameObject currentInteractable = null;
		private BaseInput input;
		private Vector3 playerFacing;

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, -transform.up);
		}

		void Start() {
			input = GetComponent<BaseInput>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			var interactable = FindClosestWithTag("Interactable", other.gameObject);
			if (interactable != null) {
				currentInteractable = interactable;
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			currentInteractable = null;
		}

		GameObject FindClosestWithTag(string tag, GameObject obj) {
			if (obj.CompareTag(tag)) {
				return obj;
			}

			var current = obj.transform.parent;
			while (current != null) {
				if (current.CompareTag(tag)) {
					return current.gameObject;
				} else {
					current = current.transform.parent;
				}
			}
			return null;
		}

		void FixedUpdate() {
			int maxDistance = 10;
			Vector3 dir = new Vector3(input.Horizontal, input.Vertical, 0);
			if (dir.magnitude == 0) {
				dir = playerFacing;
			} else {
				playerFacing = dir;
			}

			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDistance);

			Debug.DrawRay(transform.position, dir, Color.red);
			Debug.Log(dir);
			// Debug.Log("(" + input.Horizontal + "," + input.Vertical + ")");
			if (hit) {
				Debug.Log("hit");
				var interactable = FindClosestWithTag("Interactable", hit.collider.gameObject);
				if (interactable) {
					currentInteractable = interactable;
				}
			} else {
				// currentInteractable = null;
			}
		}
	}

}
