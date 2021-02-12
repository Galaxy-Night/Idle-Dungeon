using System;
using System.IO;
using System.Collections.Generic;

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
	}
}
