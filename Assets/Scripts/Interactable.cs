using UnityEngine;

public class Interactable : MonoBehaviour {
	public float radius = 3f;

	public virtual void Interact() {
		Debug.Log("Interaction not implemented");
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
