using Moonshot.Inventories;
using Moonshot.Items;

using System.Collections.Generic;
using System.Linq;

namespace Moonshot.Quests {
	public class CollectionEvent : BaseEvent {

		public int CurrentAmount { get; protected set; }
		public int RequiredAmount { get; protected set; }
		public Item item { get; protected set; }

		public CollectionEvent(string _n, string _d, int _requiredAmount, Item _item) : base(_n, _d) {
			CurrentAmount = 0;
			RequiredAmount = _requiredAmount;
			item = _item;

			Inventory.Instance.onItemChangedCallback += CheckNewItem;
		}

		protected void CheckNewItem() {
			List<Item> matchedItems = (List<Item>)Inventory.Instance.items.Where(i => i == item);

			CurrentAmount = matchedItems.Count;

			if (CurrentAmount >= RequiredAmount) {
				this.UpdateEvent(EventStatus.DONE);
			}
		}
	}
}
