using Moonshot.GameManagement;
using Moonshot.Inventories;
using UnityEngine;

public class InventorySection : MonoBehaviour {

	[SerializeField] private GameObject itemSlotPrefab;

	void Start() {
		GameEvents.Instance.onInventoryUpdated += DrawInventory;
	}

	void OnDestroy() {
		GameEvents.Instance.onInventoryUpdated -= DrawInventory;
	}

	private void DrawInventory() {
		DesimateChildren();
		foreach (var item in Inventory.Instance.items) {
			GameObject itemSlot = Instantiate(itemSlotPrefab);
			itemSlot.transform.parent = this.transform;
			itemSlot.GetComponent<InventoryItemSlot>().item = item;
			itemSlot.GetComponent<InventoryItemSlot>().Draw();
		}
	}

	private void DesimateChildren() {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}
}
