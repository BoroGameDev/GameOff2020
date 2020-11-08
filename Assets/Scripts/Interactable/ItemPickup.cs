using Moonshot.Inventories;
using Moonshot.Items;

using UnityEngine;

namespace Moonshot.Interactables {
	public class ItemPickup : Interactable {
		public Item item;

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
