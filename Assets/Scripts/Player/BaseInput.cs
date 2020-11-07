using UnityEngine;

namespace Moonshot.Player {

	public class BaseInput : MonoBehaviour {

		#region Variables
		protected float horizontal = 0f;
		protected float vertical = 0f;
		protected bool sneak = false;
		protected bool run = false;
		protected bool interact = false;
		#endregion

		#region Properties
		public float Horizontal { get { return horizontal; } }
		public float Vertical { get { return vertical; } }
		public bool Sneak { get { return sneak; } }
		public bool Run { get { return run; } }
		public bool Interact { get { return interact; } }
		#endregion

		#region Built-In Methods
		private void Update() {
			HandleInput();
		}
		#endregion

		#region Custom Methods
		protected virtual void HandleInput() {
			horizontal = Input.GetAxisRaw("Horizontal");
			vertical = Input.GetAxisRaw("Vertical");
			sneak = Input.GetButton("Sneak");
			run = Input.GetButton("Run");
			interact = Input.GetButtonDown("Fire1");
		}
		#endregion

	}
}
