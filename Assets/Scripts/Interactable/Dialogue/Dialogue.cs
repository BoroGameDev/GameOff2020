using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "NPC/Dialogue")]
public class Dialogue : ScriptableObject {
	[TextArea(3, 10)]
	public string[] sentences;
}
