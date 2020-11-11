using UnityEngine;

namespace Moonshot.Locations {

	[CreateAssetMenu(fileName = "New Location", menuName = "Level Desgin/Location")]
	public class Location : ScriptableObject {
		public string Name;
		public Sprite Icon;
	}
}
