using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

/// <summary>
/// <c>Utility</c> is a namespace used to store functions that handle general operations, rather
/// than any logic that impacts the game
/// </summary>
namespace Utility {
	/// <summary>
	/// <c>FileIO</c> is a class to handle reading in and saving to files
	/// </summary>
	public static class FileIO {
		/// <summary>
		/// <c>GetUnlockCosts</c> loads the costs to unlock party members from the specified location,
		/// and formats them into a list of tuples that consist of the unlock cost and the relevant 
		/// <c>PartyMemberData</c>
		/// </summary>
		/// <param name="location">The location of the file containing the unlock costs</param>
		/// <returns>a list of tuples that consist of the unlock cost and the corresponding 
		/// <c>PartyMemberData</c></returns>
		public static List<Tuple<int, PartyMemberData>> GetUnlockCosts(string location) {
			List<Tuple<int, PartyMemberData>> returned = new List<Tuple<int, PartyMemberData>>();
			foreach(var line in File.ReadLines(location)) {
				var data = line.Split(' ');
				PartyMemberData partyMember = new PartyMemberData(data[0], int.Parse(data[1]), int.Parse(data[2]), float.Parse(data[3]));
				Tuple<int, PartyMemberData> added = new Tuple<int, PartyMemberData>(int.Parse(data[2]), partyMember);
				returned.Add(added);
			}
			return returned;
		} 

		public static List<EnemyData> GetFloorEnemies(string location, int floor) {
			List<EnemyData> returned = new List<EnemyData>();
			foreach(var line in File.ReadLines(location + "lvl" + floor.ToString() + "_defs.txt")) {
				var data = line.Split(' ');
				string name = "";
				for (int i = 6; i < data.Length; i++)
					name += data[i];
				EnemyData enemy = new EnemyData(name, int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), 
					int.Parse(data[4]), int.Parse(data[5]));
				returned.Add(enemy);
			}
			return returned;
		}
	}

	/// <summary>
	/// A class to handle the manipulation of strings
	/// </summary>
	public static class StringManip {
		/// <summary>
		/// Removes the whitespace characters from a string
		/// </summary>
		/// <param name="toStrip">the string from which the whitespace is to be
		/// removed</param>
		/// <returns>A string with the whitespace removed</returns>
		public static string StripWhitespace(string toStrip) {
			return Regex.Replace(toStrip, @"\s+", String.Empty);
		}
	}
}
