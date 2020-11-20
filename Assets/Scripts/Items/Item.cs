using UnityEngine;
using Moonshot.Inventories;

namespace Moonshot.Items {
	[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
	[System.Serializable]
	public class Item : ScriptableObject {

		new public string name = "New Item";
		public Sprite icon = null;
		public bool showInInventory = true;

		public virtual void Use() {
		}

		public void RemoveFromInventory() {
			Inventory.Instance.Remove(this);
		}

	}
}
