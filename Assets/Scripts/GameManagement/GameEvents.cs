using System;

namespace Moonshot.GameManagement {

	public class GameEvents {
		#region Singleton
		private static GameEvents instance = null;
		private static readonly object padlock = new object();

		GameEvents() { }

		public static GameEvents Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new GameEvents();
					}
					return instance;
				}
			}
		}
		#endregion

		// This is an example event... delete when you make a real one
		public event Action onEvent;
		public void Event() {
			if (onEvent != null) {
				onEvent();
			}
		}
	}
}
