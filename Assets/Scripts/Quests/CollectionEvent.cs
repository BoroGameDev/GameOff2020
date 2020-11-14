using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Moonshot.Quests {
	public class CollectionEvent : BaseEvent {

		public int CurrentAmount = 0;
		public int RequiredAmount = 1;
		public Item item;

		public CollectionEvent(Quest _p, string _n, string _d, Item _item, int _requiredAmount = 1) : base(_p, _n, _d) {
			item = _item;
			RequiredAmount = _requiredAmount;
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
