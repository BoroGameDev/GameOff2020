using Moonshot.Inventories;
using Moonshot.Items;

using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class WorldState {

	public float[] playerPosition = new float[3];
	public List<Item> items;

	public WorldState(Vector3 _playerPosition, Inventory inv) {
		playerPosition[0] = _playerPosition.x;
		playerPosition[1] = _playerPosition.y;
		playerPosition[2] = _playerPosition.z;

		items = inv.items;
	}
}
