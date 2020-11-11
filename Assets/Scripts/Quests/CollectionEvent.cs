using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Moonshot.Quests {
	public class CollectionEvent : BaseEvent {

		public int CurrentAmount { get; protected set; }
		public int RequiredAmount { get; protected set; }
		public Item item { get; protected set; }

		public CollectionEvent(string _n, string _d, int _requiredAmount, Item _item) : base(_n, _d) {
			CurrentAmount = 0;
			RequiredAmount = _requiredAmount;
			item = _item;

			GameEvents.Instance.onInventoryUpdated += CheckItems;
		}

		public override void Start() {
			base.Start();

			CheckItems();
		}

		protected void CheckItems() {
			if (Status != EventStatus.CURRENT) { return; }

			List<Item> matchedItems = Inventory.Instance.items.Where<Item>(i => i == item).ToList<Item>();

			CurrentAmount = matchedItems.Count;

			Debug.Log($"{item.name}: {CurrentAmount}/{RequiredAmount}");

			if (IsRequirementSatisfied()) {
				this.UpdateEvent(EventStatus.DONE);
			}
		}

		protected override bool IsRequirementSatisfied() {
			return CurrentAmount >= RequiredAmount;
		}
	}
}
