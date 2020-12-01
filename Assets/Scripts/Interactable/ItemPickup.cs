using Moonshot.Inventories;
using Moonshot.Items;

using UnityEngine;

namespace Moonshot.Interactables {
	[RequireComponent(typeof(SpriteRenderer))]
	public class ItemPickup : Interactable {
		public Item item;
		private SpriteRenderer _spriteRenderer;

		private void Awake() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_spriteRenderer.sprite = item.icon;
		}

		public override void Interact() {
			Pickup();
		}

		private void Pickup() {
			Debug.Log($"Picking up {item.name}");

			Inventory.Instance.Add(item);

			Destroy(gameObject);
		}
	}
}
