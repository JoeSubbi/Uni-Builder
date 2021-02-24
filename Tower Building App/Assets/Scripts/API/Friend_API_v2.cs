﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Friend_API_v2 : MonoBehaviour
{

    // A list that stores the username and userid of each player the current user has marked as a friend
    public List<Friends> friendslist = new List<Friends>();
    //Use the prefab participant
    public Transform FriendPrefab;
    //Leaderboardlist to be able to store all instances
    public Transform FriendListTransform;
    private TextMeshProUGUI textXP;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI rankText;
    /*  JSON formatting
        {[
            {"userId":"e6j8g6", "friendId":"c2j2f8"},
            ...
            {"userId":"e6j8g6", "friendId":"g4h5g3"}
        ]}
    */

    void Start()
    {
        // GET request - Given a userID return all entries in the FRIENDS table with that userID in the 'USER' column
        // Translate the data retrieved from the GET request to a string list of friend ids
        // for each friend id - do a get request of that id to get the username

        User_Data.data.UserID = "c5db6db8-d979-4feb-abb3-395747cd9196";

        /* CreateRequest("Get_FriendIDs"); */
        Debug.Log("Finding the users friends...");
        CreateRequest("GET_FriendIDs");

        /* Code for testing getting the length of a list in JSON and looping over it */
        /* Debug.Log("Starting read operation...");
        using (StreamReader r = new StreamReader("Assets/Scripts/API/friends.json")) {
            string json = r.ReadToEnd();
            Debug.Log(json);
            TranslateToStringList(json);
        } */
    }

    private void CreateRequest(string RequestType, string friendID = "-1")
    {
        string apiString = "http://localhost:8080/api/Users/";
        // Needs refactoring
        apiString = apiString + User_Data.data.UserID + "/Friends/";

        StartCoroutine(GetRequest(apiString));

        //if (RequestType == "GET_FriendIDs")
        //{
        //    // Target API: apiString/{id}/Friends
        //    apiString = apiString + User_Data.data.UserID + "/Friends/";
        //    StartCoroutine(GetRequest(apiString, "Multiple"));

        //}
        //else if (RequestType == "GET_User")
        //{
        //    // Target API: apiString/{id}/Friends
        //    apiString = apiString + friendID;
        //    StartCoroutine(GetRequest(apiString, "Single"));
        //}

        //yield return 1;
    }

    IEnumerator GetRequest(string targetAPI)
    {
        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("An Internal Server Error Was Encountered");
        }
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            AddToFriendsList(raw);
        }
    }

    // private void
    //IEnumerator TranslateToStringList(string rawJSON)
    //{
    //    JSONNode node;
    //    node = JSON.Parse(rawJSON);

    //    int list_length = node.Count;
    //    Debug.Log(list_length);

    //    Debug.Log("Starting the requestFriendData coroutine...");
    //    yield return StartCoroutine(requestFriendData(node, list_length));
    //    displayData();
    //}

    //IEnumerator requestFriendData(JSONNode node, int list_length)
    //{
    //    for (int i = 0; i < list_length; i++)
    //    {
    //        string friendID = JSON.Parse(node[i]["friendId"].Value);
    //        Debug.Log(friendID);
    //        yield return StartCoroutine(CreateRequest("GET_User", friendID));
    //    }
    //    Debug.Log("Starting the displayData function...");
    //}

    public void displayData()
    {
        // Display the data using the UI
        foreach (Friends data in friendslist)
        {
            int index = friendslist.IndexOf(data);

            //Create instance(user) as each data loop
            var instance = Instantiate(FriendPrefab);
            //Set their parent to FriendList
            instance.SetParent(FriendListTransform, false);
            textName = instance.Find("NameText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            textXP = instance.Find("XPText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText = instance.Find("RankingText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            rankText.text = (friendslist.IndexOf(data) + 1).ToString() + ".";
            textName.text = data.UserName;
            textXP.text = data.totalExp.ToString();
            Debug.Log(friendslist.IndexOf(data));
            Debug.Log(data.UserName + " " + data.totalExp);
        }

    }

    private void AddToFriendsList(string rawJSON)
    {
        JSONNode node;
        node = JSON.Parse(rawJSON);

        int list_length = node.Count;
        Debug.Log(list_length);



        Debug.Log("Adding to friends list");
        for (int i = 0; i < list_length; i++)
        {
            string friendID = JSON.Parse(node[i]["id"].Value);
            string friendUsername = JSON.Parse(node[i]["userName"].Value);
            int friendXP = JSON.Parse(node[i]["totalExp"].Value);

            Friends newFriend = new Friends(friendID, friendUsername, friendXP);
            friendslist.Add(newFriend);
        }

        displayData();

    }
}

public class Friends
{
    public string UserId;
    public string UserName;
    public int totalExp;

    public Friends(string ui, string un, int xp)
    {
        UserId = ui;
        UserName = un;
        totalExp = xp;
    }
}

[System.Serializable]
public class FriendLink
{
    string userID;
    string friendID;

    public FriendLink(string ui, string fi)
    {
        userID = ui;
        friendID = fi;
    }
}