using System;
using UnityEngine;

namespace Moonshot.Quests {
	public enum EdgeConnectionType { In, Out }

	public class EdgeConnection {
		public Rect rect;
		public EdgeConnectionType type;

		public Node node;
		public GUIStyle style;
		public Action<EdgeConnection> OnClickEdgeConnection;

		public EdgeConnection(Node _node, EdgeConnectionType _type, GUIStyle _style, Action<EdgeConnection> _onClickEdgeConnection) {
			this.node = _node;
			this.type = _type;
			this.style = _style;
			this.OnClickEdgeConnection = _onClickEdgeConnection;
			rect = new Rect(0, 0, 10f, 20f);
		}

		public void Draw() { 
			rect.y = node.rect.y + node.rect.height * 0.5f - rect.height * 0.5f;

			switch (type) {
				case EdgeConnectionType.In:
					rect.x = node.rect.x - rect.width + 8f;
					break;
				case EdgeConnectionType.Out:
					rect.x = node.rect.x + node.rect.width - 8f;
					break;
			}

			if (GUI.Button(rect, "", style)) {
				if (OnClickEdgeConnection != null) {
					OnClickEdgeConnection(this);
				}
			}
		}
	}
}
