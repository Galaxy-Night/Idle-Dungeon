using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>MonsterDefinitions</c> is a class to handle loading the enemy prefabs
/// from the resources folder for level generation
/// </summary>
public static class MonsterDefinitions
{
	private const string FILE_LOCATION = "Assets/prefabs/enemy/lvl";

	/// <summary>
	/// <c>LoadLevel</c> loads all of the enemies that may appear on a particular
	/// level
	/// </summary>
	/// <param name="level">The level number for which the appropriate monsters
	/// are to be loaded</param>
	/// <returns>A HashSet of the enemies that may be generated on <c>level</c>
	/// </returns>
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

	/// <summary>
	/// A temporoary function that loads the monsters that are eligible to appear
	/// on level zero. Will be removed later
	/// </summary>
	/// <returns>An array of gameobjects representing the enemies that are
	/// eligible to be loaded</returns>
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
