using System;

using UnityEditor;

using UnityEngine;

namespace Moonshot.Quests {
	public class Edge {
		public EdgeConnection inPoint;
		public EdgeConnection outPoint;
		public Action<Edge> OnClickRemoveEdge;

		public Edge(EdgeConnection _inPoint, EdgeConnection _outPoint, Action<Edge> _onCLickRemoveEdge) {
			this.inPoint = _inPoint;
			this.outPoint = _outPoint;
			this.OnClickRemoveEdge = _onCLickRemoveEdge;
		}

		public void Draw() {
			Handles.DrawBezier(
				inPoint.rect.center,
				outPoint.rect.center,
				inPoint.rect.center + Vector2.left * 50f,
				outPoint.rect.center - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap)) {
				if (OnClickRemoveEdge != null) {
					OnClickRemoveEdge(this);
				}
			}
		}
	}
}
