using System;
using System.Collections.Generic;

using UnityEngine;

using Utility;

/// <summary>
/// <c>Game is a class designed to handle the logic of the game</c>
/// </summary>
public class Game : MonoBehaviour
{
    private const int PARTY_MEMBER_OFFSET = -75;

    private GameData data;
    [SerializeField]
    private GameObject uiPrefab;

    private List<GameObject> partyMemberHandlers;
    private int partyMembersUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        partyMemberHandlers = new List<GameObject>();
        partyMembersUnlocked = 1;
        ConstructPartyMember(data.unlockCost[0].Item2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// <c>ConstructPartyMember</c> creates the appropriate <c>GameObjects</c>
    /// to represent the party member
    /// </summary>
    /// <param name="_data"></param>
    private void ConstructPartyMember(PartyMemberData _data)
    {
        //get party member container's transform
        Transform parent = GameObject.Find("party_members").GetComponent<Transform>();
        //get UI container's transform
        Transform uiParent = GameObject.Find("Content").GetComponent<Transform>();
        //construct new party member handler and UI objects
        GameObject newMember = new GameObject(_data.MemberName);

        newMember.AddComponent<PartyMemberHandler>();
        //set member variables
        newMember.GetComponent<PartyMemberHandler>().ui = Instantiate(uiPrefab, uiParent);
        newMember.GetComponent<PartyMemberHandler>().data = _data;
        //initialize UI data
        newMember.GetComponent<PartyMemberHandler>().initialize();

        newMember.transform.SetParent(parent);
        newMember.GetComponent<PartyMemberHandler>().ui.GetComponent<RectTransform>().localPosition = 
            new Vector3Int(0, PARTY_MEMBER_OFFSET * partyMembersUnlocked, 0);

        partyMemberHandlers.Add(newMember);
    }
}
