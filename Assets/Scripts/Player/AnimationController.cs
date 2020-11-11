using Moonshot.GameManagement;
using UnityEngine;

namespace Moonshot.Player {

	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(BaseInput))]
	public class AnimationController : MonoBehaviour {

		private BaseInput input;
		private Animator anim;
		private bool shouldAnimate = true;

		private void Awake() {
			input = GetComponent<BaseInput>();
			anim = GetComponent<Animator>();

			GameEvents.Instance.onDialogueStarted += SetShouldNotAnimate;
			GameEvents.Instance.onDialogueEnded += SetShouldAnimate;
		}

		void OnDestroy() {
			GameEvents.Instance.onDialogueStarted -= SetShouldNotAnimate;
			GameEvents.Instance.onDialogueEnded -= SetShouldAnimate;
		}

		void Update() {
			if (!shouldAnimate) {
				return;
			}

			Vector2 move = new Vector2(input.Horizontal, input.Vertical);
			anim.SetBool("Moving", move.magnitude != 0f);

			if (move == Vector2.zero) {
				return;
			}

			anim.SetFloat("MoveX", move.x);
			anim.SetFloat("MoveY", move.y);
		}

		private void SetShouldAnimate(NPC _npc) {
			shouldAnimate = true;
		}

		private void SetShouldNotAnimate(NPC _npc) {
			shouldAnimate = false;
		}
	}
}
