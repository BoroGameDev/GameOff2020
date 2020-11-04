using UnityEngine;

namespace Moonshot.Player {

	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(BaseInput))]
	public class AnimationController : MonoBehaviour {

		private BaseInput input;
		private Animator anim;

		private void Awake() {
			input = GetComponent<BaseInput>();
			anim = GetComponent<Animator>();
		}

		void Update() {
			Vector2 move = new Vector2(input.Horizontal, input.Vertical);
			anim.SetBool("Moving", move.magnitude != 0f);

			if (move == Vector2.zero) {
				return; 
			}

			anim.SetFloat("MoveX", move.x);
			anim.SetFloat("MoveY", move.y);
		}
	}
}
