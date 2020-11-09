using Moonshot.Inventories;
using Moonshot.Items;

using UnityEngine;

namespace Moonshot.Interactables {
	[RequireComponent(typeof(SpriteRenderer))]
	public class ItemPickup : Interactable {
		public Item item;
		private SpriteRenderer spriteRenderer;

		private void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = item.icon;
		}

		public override void Interact() {
			base.Interact();

			Pickup();
		}

		private void Pickup() {
			Debug.Log($"Picking up {item.name}");

			Inventory.Instance.Add(item);

			Destroy(gameObject);
		}
	}
}
