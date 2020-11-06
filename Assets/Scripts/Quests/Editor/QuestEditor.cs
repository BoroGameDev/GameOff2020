using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Moonshot.Quests {

	public class QuestEditor : EditorWindow {

		private List<Node> nodes;
		private List<Edge> edges;

		private GUIStyle nodeStyle;
		private GUIStyle selectedNodeStyle;
		private GUIStyle inPointStyle;
		private GUIStyle outPointStyle;

		private EdgeConnection selectedInPoint;
		private EdgeConnection selectedOutPoint;

		private Vector2 offset;
		private Vector2 drag;

		[MenuItem("Window/Quest Graph Editor")]
		private static void OpenWindow() {
			QuestEditor window = GetWindow<QuestEditor>();
			window.titleContent = new GUIContent("Quest Editor");
		}

		private void OnEnable() {
			nodeStyle = new GUIStyle();
			nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node5.png") as Texture2D;
			nodeStyle.border = new RectOffset(12, 12, 12, 12);

			selectedNodeStyle = new GUIStyle();
			selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
			selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

			inPointStyle = new GUIStyle();
			inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
			inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
			inPointStyle.border = new RectOffset(4, 4, 12, 12);

			outPointStyle = new GUIStyle();
			outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
			outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
			outPointStyle.border = new RectOffset(4, 4, 12, 12);
		}

		private void OnGUI() {
			DrawGrid(20, 0.2f, Color.gray);
			DrawGrid(100, 0.4f, Color.gray);
			DrawNodes();
			DrawEdges();

			DrawEdgesLine(Event.current);

			ProcessNodeEvents(Event.current);
			ProcessEvents(Event.current);

			if (GUI.changed) {
				Repaint();
			}
		}

		private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor) {
			int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
			int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			offset += drag * 0.5f;
			Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

			for (int i = 0; i < widthDivs; i++) {
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
			}

			for (int j = 0; j < heightDivs; j++) {
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
			}

			Handles.color = Color.white;
			Handles.EndGUI();
		}

		private void DrawNodes() {
			if (nodes == null) { return; }

			foreach (Node node in nodes) {
				node.Draw();
			}
		}

		private void DrawEdges() {
			if (edges == null) { return; }

			foreach (Edge edge in edges) {
				edge.Draw();
			}
		}

		private void DrawEdgesLine(Event e) {
			if (selectedInPoint != null && selectedOutPoint == null) {
				Handles.DrawBezier(
					selectedInPoint.rect.center,
					e.mousePosition,
					selectedInPoint.rect.center + Vector2.left * 50f,
					e.mousePosition - Vector2.left * 50f,
					Color.white,
					null,
					2f
				);

				GUI.changed = true;
			}

			if (selectedOutPoint != null && selectedInPoint == null) {
				Handles.DrawBezier(
					selectedOutPoint.rect.center,
					e.mousePosition,
					selectedOutPoint.rect.center - Vector2.left * 50f,
					e.mousePosition + Vector2.left * 50f,
					Color.white,
					null,
					2f
				);

				GUI.changed = true;
			}
		}

		private void ProcessEvents(Event _current) {
			drag = Vector2.zero;

			switch (_current.type) {
				case EventType.MouseDown:
					if (_current.button == 1) {
						ProcessContextMenu(_current.mousePosition);
					}
					break;
				case EventType.MouseDrag:
					if (_current.button == 0) {
						OnDrag(_current.delta);
					}
					break;
			}
		}

		private void OnDrag(Vector2 delta) {
			drag = delta;

			if (nodes != null) {
				foreach (Node node in nodes) {
					node.Drag(delta);
				}
			}

			GUI.changed = true;
		}

		private void ProcessNodeEvents(Event _current) {
			if (nodes == null) { return; }

			foreach (Node node in nodes) {
				bool guiChanged = node.ProcessEvents(_current);

				if (guiChanged) {
					GUI.changed = true;
				}
			}
		}

		private void ProcessContextMenu(Vector2 mousePosition) {
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Add quest"), false, () => OnClickAddNode(mousePosition));
			genericMenu.ShowAsContext();
		}

		private void OnClickAddNode(Vector2 mousePosition) {
			if (nodes == null) {
				nodes = new List<Node>();
			}

			nodes.Add(new Node(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
		}

		private void OnClickRemoveNode(Node _node) {
			if (edges == null) { return; }

			List<Edge> edgesToRemove = new List<Edge>();

			foreach (Edge edge in edges) {
				if (edge.inPoint == _node.inPoint || edge.outPoint == _node.outPoint) {
					edgesToRemove.Add(edge);
				}
			}

			foreach (Edge edge in edgesToRemove) {
				edges.Remove(edge);
			}

			edgesToRemove = null;

			nodes.Remove(_node);
		}

		private void OnClickInPoint(EdgeConnection _inPoint) {
			selectedInPoint = _inPoint;

			if (selectedOutPoint == null) { return; }

			if (selectedOutPoint.node != selectedInPoint.node) {
				CreateEdge();
				ClearEdgeSelection();
			} else {
				ClearEdgeSelection();
			}
		}

		private void OnClickOutPoint(EdgeConnection _outPoint) {
			selectedOutPoint = _outPoint;

			if (selectedInPoint == null) { return; }

			if (selectedOutPoint.node != selectedInPoint.node) {
				CreateEdge();
				ClearEdgeSelection();
			} else {
				ClearEdgeSelection();
			}
		}

		private void OnClickRemoveEdge(Edge _edge) {
			edges.Remove(_edge);
		}

		private void CreateEdge() {
			if (edges == null) {
				edges = new List<Edge>();
			}

			edges.Add(new Edge(selectedInPoint, selectedOutPoint, OnClickRemoveEdge));
		}

		private void ClearEdgeSelection() {
			selectedInPoint = null;
			selectedOutPoint = null;
		}
	}
}
