using System.Collections.Generic;
using Moonshot.GameManagement;

using UnityEngine;

namespace Moonshot.Player {

	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BaseInput))]
	public class Movement : MonoBehaviour {
		enum MovementType {
			SNEAK = 1,
			WALK = 2,
			RUN = 3
		};

		[SerializeField]
		private float Speed = 4f;

		private BaseInput input;
		private Rigidbody2D body;

		private MovementType currentMovementType = MovementType.WALK;
		private Dictionary<MovementType, float> movementSpeedModifier = new Dictionary<MovementType, float>() {
			{MovementType.SNEAK, 0.5f},
			{MovementType.WALK, 1f},
			{MovementType.RUN, 2f}
		};
		private bool isMoveable = true;

		void Start() {
			body = GetComponent<Rigidbody2D>();
			input = GetComponent<BaseInput>();
			GameEvents.Instance.onDialogueStarted += SetUnmoveable;
			GameEvents.Instance.onDialogueEnded += SetMoveable;

			body.freezeRotation = true;
		}

		void OnDestroy() {
			GameEvents.Instance.onDialogueStarted -= SetUnmoveable;
			GameEvents.Instance.onDialogueEnded -= SetMoveable;
		}

		void Update() {
			if (input.Sneak) {
				currentMovementType = MovementType.SNEAK;
			} else if (input.Run) {
				currentMovementType = MovementType.RUN;
			} else {
				currentMovementType = MovementType.WALK;
			}
		}

		void FixedUpdate() {
			if (input.Horizontal != 0f || input.Vertical != 0) {
				MoveCharacter();
			}
		}

		public void MoveCharacter() {
			if (!isMoveable || GameManager.Instance.Paused) {
				return;
			}
			Vector3 move = new Vector3(input.Horizontal, input.Vertical, 0);
			move = move.normalized * Speed * movementSpeedModifier[currentMovementType];
			body.MovePosition(transform.position + move * Time.deltaTime);
		}

		private void SetMoveable(NPC _npc) {
			isMoveable = true;
		}

		private void SetUnmoveable(NPC _npc) {
			isMoveable = false;
		}
	}
}
