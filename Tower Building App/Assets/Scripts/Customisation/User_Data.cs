using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;

[System.Serializable]
public class User_Data : MonoBehaviour{
    public static User_Data data;
    public GameObject UserProfile;
    public string Username,json;
    public int global_xp;

    public int temp_primary;
    public int temp_secondary;
    public int temp_model;
    public int temp_height;
    public List<Building> building_stats = new List<Building>();
    
    //public Button file1,file2;
    
    //This script will store all of the data assigned to a single user
    // It will contain the object it travels thorugh the scenes on (Empty GameObject)
    // the details of the player (username, game-wide-XP, ect...)
    // and a List to store the data on each of the buildings (building name, building xp, wallcolour, ect...)

    void Start(){
        DontDestroyOnLoad(UserProfile);
        data = this;

        //CREATE BUILDING INSTANCES HERE
        createBuildings();

        // The stages of starting the game; authenticate user, get users data (username, XP), get that users building data

        // Login 
        // GetRequest("User");
        // GetRequest("Buildings");

        // OLD CODE leaving for referal to for now
        //file1.onClick.AddListener(() => LoadJson("Assets/JSON/file1.json"));
        Debug.Log("Run from User Data");
        CreateRequest("GET", "Models", 0);



    }

    private void createBuildings(){
        Building Main = new Building(100,4,0,500,4);
        building_stats.Add(Main);
        Building Main2 = new Building(101,5,1,500,2);
        building_stats.Add(Main2);
        Building Main3 = new Building(102,9,2,500,1);
        building_stats.Add(Main3);
        Building Main4 = new Building(103,11,3,500,3);
        building_stats.Add(Main4);
        Building Art = new Building(-1,-1,0,450,1);
        building_stats.Add(Art);
        Building Biology_Chemistry = new Building(3,5,0,400,1);
        building_stats.Add(Biology_Chemistry);
        Building ComputerScience = new Building(-1,-1,0,350,1);
        building_stats.Add(ComputerScience);
        Building Engineering = new Building(-1,-1,0,300,1);
        building_stats.Add(Engineering);
        Building Geography_History = new Building(-1,-1,0,250,1);
        building_stats.Add(Geography_History);
        Building Languages = new Building(-1,-1,0,200,1);
        building_stats.Add(Languages);
        Building Law_Politics = new Building(-1,-1,0,150,1);
        building_stats.Add(Law_Politics);
        Building Physics_Maths = new Building(-1,-1,0,100,1);
        building_stats.Add(Physics_Maths);
    }

    private void LoadJson(string filename)
    {
        JSONNode node;
        using (StreamReader r = new StreamReader(filename))
        {
            //read in the json
            json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }
        string username = JSON.Parse(node["username"].Value);
        Debug.Log(username);
        Username = username;
        int xp = int.Parse(node["global_xp"].Value);
        Debug.Log(global_xp);
        global_xp = xp;
        building_stats.Clear();
        for (int i=0; i<2; i++){
            int primary_colour = int.Parse(node["buildings"][i]["primary_colour"].Value);
            Debug.Log(primary_colour);
            int secondary_colour = int.Parse(node["buildings"][i]["secondary_colour"].Value);
            Debug.Log(secondary_colour);
            int model = int.Parse(node["buildings"][i]["model"].Value);
            Debug.Log(model);
            int building_xp = int.Parse(node["buildings"][i]["building_xp"].Value);
            Debug.Log(building_xp);
            int m_height = 1;
            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, m_height);
            building_stats.Add(newBuilding);
        }
    }

    /* 
        RequestType = "GET" or "Update"
        Table = "Users" or "Models"
     */
    private void CreateRequest(string RequestType, string Table, int id = -1)
    {
        // Building name, User name. User -> 
        string apiString = "api/";

        // Go to Users table or the Buildings Table.
        if (RequestType == "GET")
        {
            // Get all the buildings.
            apiString = string.Concat(apiString, Table);

            if (id > -1)
            {
                string requestedId = string.Concat("/", id.ToString());
                apiString = string.Concat(apiString, requestedId);
            }
            apiString = string.Concat("http://localhost:8080/", apiString);
            StartCoroutine(GetRequest(apiString));

        }
    }

    private void CreateBuildingJSON(){
        // Create the JSON file storing the building data for writing to the database
    }

    // Translation Functions

    private void CreateUserJSON(){
        // Create the JSON file storing the User login data for writing to the database
    }

    private void TranslateBuildingJSON(){
        // Reads a JSON file from the database to create / update the Building_Stats list stored in Unity
    }

    private void TranslateUserJSON(){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 
    }


    IEnumerator GetRequest(string targetAPI){

        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending " + uwr.error);
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);
            BuildingTemp modelGot = JsonUtility.FromJson<BuildingTemp>(raw);
            Debug.Log("The model given was " + modelGot.name);

        }

        // targetAPI determines whether a BuildingAPI or UserAPI request will be made
    }

    private void PutRequest(string targetAPI){
        // Constructs and sends a PUT request to the database to update it with the given JSON file

        // targetAPI determines whether a BuildingAPI or UserAPI request will be made
    }
}

public class Building{
    public int primary_colour;
    public int secondary_colour;
    public int model;
    public int building_xp;
    public int m_height;

    public Building(int primary, int secondary, int m, int xp, int h){
        primary_colour = primary;
        secondary_colour = secondary;
        model = m;
        building_xp = xp;
        m_height = h;
    }
}

public class BuildingTemp
{
    public long buildingCode;
    public string name;
    public long group;  

}