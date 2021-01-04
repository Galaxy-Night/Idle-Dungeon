using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class MonsterDefinitions
{
	private const string FILE_LOCATION = "Assets/prefabs/enemy/lvl";

	public static HashSet<GameObject> LoadLevel(int level) {
		HashSet<GameObject> returned = new HashSet<GameObject>();
		Object[] levelbelow = Resources.LoadAll(FILE_LOCATION + 
			(level - 1).ToString());
		Object[] currentlevel = Resources.LoadAll(FILE_LOCATION +
					level.ToString());
		Object[] levelabove = Resources.LoadAll(FILE_LOCATION +
			(level + 1).ToString());
		foreach (object item in levelbelow)
			returned.Add((GameObject)item);
		foreach (object item in currentlevel)
			returned.Add((GameObject)item);
		foreach (object item in levelabove)
			returned.Add((GameObject)item);
		return returned;
	}

	public static GameObject[] TEMPLoadLevel() {
		GameObject[] returned;
		HashSet<GameObject> set = new HashSet<GameObject>();
		Object[] monsters = Resources.LoadAll("lvl0");
		foreach (object monster in monsters)
			set.Add((GameObject)monster);
		returned = new GameObject[set.Count];
		set.CopyTo(returned);
		return returned;
	}
}
