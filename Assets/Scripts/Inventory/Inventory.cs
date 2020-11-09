using UnityEngine;
using Moonshot.Items;
using System.Collections.Generic;
using Moonshot.GameManagement;

namespace Moonshot.Inventories {
	public class Inventory : MonoBehaviour {
		#region Singleton
		public static Inventory Instance { get; private set; }

		void Awake() {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
			} else {
				DestroyImmediate(gameObject);
				return;
			}
		}
		#endregion

		public int space = 10;
		public List<Item> items = new List<Item>();

		public void Add(Item _item) {
			if (!_item.showInInventory || items.Count >= space) {
				return;
			}

			items.Add(_item);
			GameEvents.Instance.InventoryUpdated();
		}

		public void Remove(Item _item) {
			items.Remove(_item);
			GameEvents.Instance.InventoryUpdated();
		}
	}
}
