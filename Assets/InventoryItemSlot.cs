using Moonshot.Items;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemSlot : MonoBehaviour {
	private Image itemSprite;
	public Item item;

	private void Awake() {
		itemSprite = GetComponent<Image>();
	}

	public void Draw() {
		itemSprite.sprite = item.icon;
	}
}
