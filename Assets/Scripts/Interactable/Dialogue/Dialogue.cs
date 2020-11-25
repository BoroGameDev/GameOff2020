using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "NPC/Dialogue")]
public class Dialogue : ScriptableObject {
	// TODO: USING THE SCRIPTABLE OBJECT IS DOING WEIRD STUFF (LOOKS LIKE REUSING THE SAME DIALOG OBJECT AND IN SOME CASES IS NULL)
	public Sentence[] sentences;
}
