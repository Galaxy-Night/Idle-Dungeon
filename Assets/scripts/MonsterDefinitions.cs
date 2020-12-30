using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class MonsterDefinitions
{
	private const string FILE_LOCATION = "Assets/prefabs/enemy/lvl";

	public static HashSet<GameObject> LoadLevel(int level) {
		HashSet<GameObject> returned = new HashSet<GameObject>();
		Object[] levelbelow = AssetDatabase.LoadAllAssetsAtPath(FILE_LOCATION +
			(level - 1).ToString());
		Object[] currentlevel = AssetDatabase.LoadAllAssetsAtPath(FILE_LOCATION +
					level.ToString());
		Object[] levelabove = AssetDatabase.LoadAllAssetsAtPath(FILE_LOCATION +
			(level + 1).ToString());
		foreach (object item in levelbelow)
			returned.Add((GameObject)item);
		foreach (object item in currentlevel)
			returned.Add((GameObject)item);
		foreach (object item in levelabove)
			returned.Add((GameObject)item);
		return returned;
	}

	public static HashSet<GameObject> TEMPLoadLevel() {
		HashSet<GameObject> returned = new HashSet<GameObject>();
		Object[] monsters = Resources.LoadAll("lvl0");
		foreach (object monster in monsters)
			returned.Add((GameObject)monster);
		return returned;
	}
}
