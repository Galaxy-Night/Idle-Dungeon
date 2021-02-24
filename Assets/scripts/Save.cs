using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Save
{
	public DateTime lastPlayed;
	public EnemyData currentEnemy;
	public GameData gameData;
	public List<PartyMemberData> memberData;

	public Save(EnemyData enemy, GameData data, List<PartyMemberHandler> memberHandlers)
	{
		lastPlayed = DateTime.UtcNow;
		Debug.Log(lastPlayed);
		currentEnemy = enemy;
		gameData = data;
		memberData = new List<PartyMemberData>();
		if(memberHandlers.Count != 0) {
			foreach (PartyMemberHandler handler in memberHandlers)
				memberData.Add(handler.data);
		}
	}

	public void Serialize() {
		//Save the information
		string fileLocation = Application.persistentDataPath + "/DungeonState.save";
		Debug.Log(fileLocation);

		if (File.Exists(fileLocation))
			File.Delete(fileLocation);

		FileStream file = File.Create(fileLocation);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(file, this);
		file.Close();
	}

	public static Save Deserialize() {
		string fileLocation = Application.persistentDataPath + "/DungeonState.save";
		FileStream file;
		Save save;

		if (!File.Exists(fileLocation))
			return null;
		else
			file = File.OpenRead(fileLocation);

		BinaryFormatter formatter = new BinaryFormatter();
		try {
			save = (Save)formatter.Deserialize(file);
		} 
		catch (SerializationException e) {
			file.Close();
			Debug.Log(e);
			return null;
		}

		file.Close();
		return save;
	}
}
