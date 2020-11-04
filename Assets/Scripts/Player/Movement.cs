using System.Collections.Generic;

using UnityEngine;

namespace Moonshot.Player {

	[RequireComponent(typeof(Rigidbody2D))]
	public class Movement : MonoBehaviour {
		enum MovementType {
			SNEAK = 1,
			WALK = 2,
			RUN = 3
		};

		[SerializeField]
		private float Speed = 4f;

		private Rigidbody2D body;
		private float horizontal;
		private float vertical;

		private MovementType currentMovementType = MovementType.WALK;
		private Dictionary<MovementType, float> movementSpeedModifier = new Dictionary<MovementType, float>() {
			{MovementType.SNEAK, 0.5f},
			{MovementType.WALK, 1f},
			{MovementType.RUN, 2f}
		};

		void Start() {
			body = GetComponent<Rigidbody2D>();
		}

		void Update() {
			horizontal = Input.GetAxisRaw("Horizontal");
			vertical = Input.GetAxisRaw("Vertical");

			if (Input.GetButton("Sneak")) {
				currentMovementType = MovementType.SNEAK;
			} else if (Input.GetButton("Run")) {
				currentMovementType = MovementType.RUN;
			} else {
				currentMovementType = MovementType.WALK;
			}
		}

		void FixedUpdate() {
			if (horizontal != 0 || vertical != 0) { 
				MoveCharacter();
			}
		}

		public void MoveCharacter() { 
			Vector3 move = new Vector3(horizontal, vertical, 0);
			move = move.normalized * Speed * movementSpeedModifier[currentMovementType];
			body.MovePosition(transform.position + move * Time.deltaTime);
		}
	}
}
