using System;

using UnityEditor;

using UnityEngine;

namespace Moonshot.Quests {

	public class Node {
		public Rect rect;
		public string title;
		public bool isDragged;
		public bool isSelected;

		public EdgeConnection inPoint;
		public EdgeConnection outPoint;

		public GUIStyle style;
		public GUIStyle defaultNodeStyle;
		public GUIStyle selectedNodeStyle;

		public Action<Node> OnRemoveNode;

		public Node(Vector2 _position, float _width, float _height, GUIStyle _nodeStyle, GUIStyle _selectedStyle, GUIStyle _inPointStyle, GUIStyle _outPointStyle, Action<EdgeConnection> _onClickInPoint, Action<EdgeConnection> _onClickOutPoint, Action<Node> _onRemoveNode) {
			rect = new Rect(_position.x, _position.y, _width, _height);
			style = _nodeStyle;
			defaultNodeStyle = _nodeStyle;
			selectedNodeStyle = _selectedStyle;
			inPoint = new EdgeConnection(this, EdgeConnectionType.In, _inPointStyle, _onClickInPoint);
			outPoint = new EdgeConnection(this, EdgeConnectionType.Out, _outPointStyle, _onClickOutPoint);
			OnRemoveNode = _onRemoveNode;
		}

		public void Drag(Vector2 delta) {
			rect.position += delta;
		}

		public void Draw() {
			inPoint.Draw();
			outPoint.Draw();
			GUI.Box(rect, title, style);
		}

		public bool ProcessEvents(Event e) {
			switch (e.type) {
				case EventType.MouseDown:
					if (e.button == 0) {
						if (rect.Contains(e.mousePosition)) {
							isDragged = true;
							GUI.changed = true;
							isSelected = true;
							style = selectedNodeStyle;
						} else {
							GUI.changed = true;
							isSelected = false;
							style = defaultNodeStyle;
						}
					}

					if (e.button == 1 && isSelected && rect.Contains(e.mousePosition)) {
						ProcessContextMenu();
						e.Use();
					}
					break;
				case EventType.MouseUp:
					isDragged = false;
					break;
				case EventType.MouseDrag:
					if (e.button == 0 && isDragged) {
						Drag(e.delta);
						e.Use();
						return true;
					}
					break;
			}

			return false;
		}

		private void ProcessContextMenu() {
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Remove Quest"), false, OnClickRemoveNode);
			genericMenu.ShowAsContext();
		}

		private void OnClickRemoveNode() {
			if (OnRemoveNode != null) {
				OnRemoveNode(this);
			}
		}
	}
}

