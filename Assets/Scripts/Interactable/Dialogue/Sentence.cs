using UnityEngine;

[System.Serializable]
public class Sentence {
	public string Name;
	[TextArea(3, 10)]
	public string Lines;
}
