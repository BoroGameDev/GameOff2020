using Moonshot.GameManagement;
using Moonshot.Inventories;
using Moonshot.Items;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Moonshot.Quests {
	[CreateAssetMenu(fileName = "New Collection Event", menuName = "Quests/Collection Event")]
	public class CollectionEvent : BaseEvent {

		[HideInInspector] public int CurrentAmount = 0;
		public int RequiredAmount = 1;
		public Item item;

		public override void Init() {
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
