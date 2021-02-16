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
    public string UserID, Username, Email, Password;
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

    void Start() {
        DontDestroyOnLoad(UserProfile);

        //data = this;

        //CREATE BUILDING INSTANCES HERE
        createBuildings();

        // The stages of starting the game; authenticate user, get users data (username, XP), get that users building data

        // Login 
        // GetRequest("User");
        // GetRequest("Buildings");

        /* CODE FOR TESTING THE TRANSLATION OF BUILDING DATA

        Debug.Log("Starting translation...");
        TranslateBuildingJSON("Assets/Scripts/Customisation/test.json");
        Debug.Log(building_stats[0].primary_colour);
        Debug.Log(building_stats[0].secondary_colour);
        Debug.Log(building_stats[0].building_xp);
        Debug.Log(building_stats[0].model);
        Debug.Log(building_stats[0].m_height);
        */


        /*
         *      GET REQUESTS
        */
        //Debug.Log("Running the GET requests");
        //// Get a saved user
        //CreateRequest("GET", "Users", "bce13125-3d7f-4452-8428-efaecb8be59e");
        //// Get all saved users
        //CreateRequest("GET", "Users");

        /*
         *      POST REQUESTS
        */
        Debug.Log("Start");

        //var jsonStringData = JsonUtility.ToJson(data) ?? "";

        // Dev User
        UserID = System.Guid.NewGuid().ToString();
        Username = "Jim";
        Email = "Jim@email.com";
        Password = "7638";
        global_xp = 500;

        var stringUserJSONData = CreateUserJSON();
        Debug.Log(stringUserJSONData);

        // Dev Building
        DatabaseBuildings currentBuilding = new DatabaseBuildings(140, "Effiel Tower", 0, -1, 4, -1, -1);
        var stringBuildingJsonData = JsonUtility.ToJson(currentBuilding);

        Debug.Log("Running the POST request");
        // Create a new user

        //CreateRequest("POST", "Users", data: stringUserJSONData);

        //// Create a new model
        //CreateRequest("POST", "Models", data: data);

        //// Edit a existing user's personal details.
        //CreateRequest("POST", "Users", "5d1841f8-8049-44a0-9fbf-992de0240e07", data: stringUserJSONData);

        // Add/Remove a building from an existing user.
        CreateRequest("POST", "Users", "5d1841f8-8049-44a0-9fbf-992de0240e07", 140, stringBuildingJsonData);


    }

    public void Update() {
        if (Input.GetKeyDown("t")){
            /* CODE FOR TESTING JSON CREATION */
            UserID = "abc";
            Username = "BobertRoss";
            Email = "bobert@bobert.com";
            Password = "321password";
            global_xp = 17;
            string stringOutput = CreateUserJSON();
            Debug.Log(stringOutput);
        }
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

    /* 
        RequestType = "GET" or "Update"
        Table = "Users" or "Models"
        id = URI of either an User or Building.
     */
    private void CreateRequest(string RequestType, string Table, string id = "-1", int buildingid = -1, string data = "-1")
    {
        // Building name, User name. User -> 
        string apiString = string.Concat("api/", Table);

        if (RequestType == "GET")
        {
            // Want to get a specfic User/Building.
            if (id != "-1")
            {
                string requestedId = "";
                // Want to get all the attributes of a particular user (USE CASE: to get all the buildings belonging to the user at the start of the game).
                requestedId = "/" + id;
                apiString = string.Concat(apiString, requestedId);
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(GetRequest(apiString));
            } 
            // Get all the Users/Buildings
            else
            {
                apiString = string.Concat(apiString + "/");
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(GetRequest(apiString));
            }
        }
        else
        {
            // POST 
            if (id == "-1")
            {
                // Create a new User
                apiString = string.Concat(apiString + "/");
                apiString = string.Concat("http://localhost:8080/", apiString);
                Debug.Log(apiString);
                StartCoroutine(PostRequest(apiString, data));
            }
            else
            {
                if (buildingid == -1)
                {
                    // Change the Users personal details 
                    apiString = apiString + "/" + id;
                    apiString = "http://localhost:8080/" + apiString;
                    Debug.Log(apiString);
                    StartCoroutine(PostRequest(apiString, data, "PUT"));
                }
                else
                {
                    // Update the property of one of the buildings belonging to the user, e.g. increasing the EXP.
                    apiString = apiString + "/" + id + "/" + "Buildings" + "/" + buildingid;
                    apiString = "http://localhost:8080/" + apiString;
                    Debug.Log(apiString);
                    StartCoroutine(PostRequest(apiString, data, "POST"));
                }
            }
        }
    }

    /*
            JSON will be formatted as such:
            {"id":???, 
                "userName":"example", 
                "email":email@email.com,
                "password":aifbreiu,
                "userBuildings":
                    [{buildingCode:12, 
                        buildingName:"Tower1",
                        building_xp:1454, 
                        "height":1, 
                        "primaryColour":104, 
                        "secondaryColour":201, 
                        "modelGroup":2}, ... , ],
                "totalExp":27353
            }
        */
    
    // WRITE
    private string CreateBuildingJSON(){
        // Create the JSON file storing the building data for writing to the database

        string BuildingJSON = "{userBuildings:[";
        
        // Loop through the buildings to add their data to the JSON string
        string toAppend = "";
        for (int i=0; i<12; i++){
            string bc = i.ToString() + building_stats[i].model.ToString(); // The unique code for the model within the subject
            int name_index = (i*10) + building_stats[i].model;
            string bn = CodeConverter.codes.buildingName_map[name_index]; // The name of the building (MAKE dictionary to map int to name)
            string bx = building_stats[i].building_xp.ToString(); // The specific xp of the building
            string h = building_stats[i].m_height.ToString(); // The height of the building (only differs for the Main)
            string mg = i.ToString(); // The subject index
            string pc = building_stats[i].primary_colour.ToString(); // The primary colour of the building
            string sc = building_stats[i].secondary_colour.ToString(); // The secondary colour of the building
            
            string ParttoAppend = "{buildingCode: " + bc + ",buildingName:" + bn + ",building_xp:" + bx + ",height:" + h + ",primary_colour:" + pc + ",secondary_colour:" + sc + ",modelGroup:" + mg + "},";
            toAppend = toAppend + ParttoAppend;
        }
        
        BuildingJSON = BuildingJSON + toAppend;
        BuildingJSON = BuildingJSON + "]}";

        return BuildingJSON;

    }

    public List<DatabaseBuildings> CreateBuildingJSON2() {
        
        List<DatabaseBuildings> uB = new List<DatabaseBuildings>();
        
        for (int i=0; i<12; i++){
            int bc = (i*10) + building_stats[i].model; // The unique code for the model within the subject
            string bn = CodeConverter.codes.buildingName_map[bc]; // The name of the building (MAKE dictionary to map int to name)
            int bx = building_stats[i].building_xp; // The specific xp of the building
            int h = building_stats[i].m_height; // The height of the building (only differs for the Main)
            int mg = i; // The subject index
            int pc = building_stats[i].primary_colour; // The primary colour of the building
            int sc = building_stats[i].secondary_colour; // The secondary colour of the building
            
            DatabaseBuildings currentBuilding = new DatabaseBuildings(bc,bn,bx,h,mg,pc,sc);
            uB.Add(currentBuilding);
        }
        return uB;
    }
    
    // WRITE
    private string CreateUserJSON() {
        // Create the JSON file storing the User login data for writing to the database
        // id, userName, email, password, userBuidlings, totalExp
        string UserJSON = "{id:" + UserID;
        UserJSON = UserJSON + ", userName:" + Username;
        UserJSON = UserJSON + ", email:" + Email;
        UserJSON = UserJSON + ", password" + Password;
        //UserJSON = UserJSON + CreateBuildingJSON();
        UserJSON = UserJSON + ", totalExp:" + global_xp.ToString() + "}";

        // OR
        // If the string method above does not work - try the following method which uses SimpleJSON

        List<DatabaseBuildings> uB = new List<DatabaseBuildings>();
        DatabaseUser putData = new DatabaseUser(UserID, Username, Email, Password, uB, global_xp);
        string UserJSON2 = JsonUtility.ToJson(putData);

        return UserJSON2;
    }

    private void TranslateBuildingJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Building_Stats list stored in Unity
        
        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            string json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }

        //Clears the Unity building list representation so it can be created fresh with the correct data
        building_stats.Clear();

        // Loop through the buildings to create their Unity representations 
        for (int j=0; j<2; j++){
            // Might need to get the modelGroup as well if the buildings are not sent in order
            
            int primary_colour = int.Parse(node["userBuildings"][j]["primaryColour"].Value);
            int secondary_colour = int.Parse(node["userBuildings"][j]["secondaryColour"].Value);
            // Model integer is the final digit in the buildingCode
            string model_code = JSON.Parse(node["userBuildings"][j]["buildingCode"].Value);
            //string model_string = model_code.ToString();
            model_code = model_code.Substring(model_code.Length - 1);
            int model = int.Parse(model_code);

            int building_xp = int.Parse(node["userBuildings"][j]["building_xp"].Value);

            int m_height = int.Parse(node["userBuildings"][j]["height"].Value);

            Building newBuilding = new Building(primary_colour,secondary_colour,model,building_xp, m_height);
            building_stats.Add(newBuilding);
        }
    }

    private void TranslateUserJSON(string rawJSON){
        // Reads a JSON file from the database to create / update the Users data stored in Unity 

        JSONNode node;
        using (StreamReader r = new StreamReader(rawJSON)) {
            //read in the json
            string json = r.ReadToEnd();

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }
        string userid = JSON.Parse(node["id"].Value);
        string username = JSON.Parse(node["userName"].Value);
        string email = JSON.Parse(node["email"].Value);
        string password = JSON.Parse(node["password"].Value);
        int totalExp = int.Parse(node["totalExp"].Value);
        UserID = userid;
        Username = username;
        Email = email;
        Password = password;
        global_xp = totalExp;
    }


    IEnumerator GetRequest(string targetAPI){

        Debug.Log(targetAPI);
        // Constructs and sends a GET request to the database to retreive a JSON file
        UnityWebRequest uwr = UnityWebRequest.Get(targetAPI);
        Debug.Log("Got the data");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("An Internal Server Error Was Encountered");
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE

            // BuildingTrue modelGot = JsonUtility.FromJson<DatabaseBuildings>(raw);
            // Debug.Log("The model given was " + modelGot.building_name);
        }
    }

    IEnumerator PostRequest(string targetAPI, string data, string type = "POST")
    {
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(data);

        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.method = type;
        uwr.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending the data ");
        Debug.Log("Data : " + data);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("An Internal Server Error Was Encountered");
        }
        else
        {
            // The POST request also returns the object it entered into the database.
            string raw = uwr.downloadHandler.text;
            Debug.Log("Received: " + raw);

            // TRANSLATION CODE HERE (to check if data was correctly entered).

        }
        
    }

    IEnumerator PutRequest(string targetAPI, string json){
        // Constructs and sends a PUT request to the database to update it with the given JSON file

        Debug.Log(targetAPI);
        
        byte[] rawData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(targetAPI, rawData);
        uwr.SetRequestHeader("Content-Type", "application/json"); 
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending " + uwr.error);
        } 
        else
        {
            string raw = uwr.downloadHandler.text;
            Debug.Log("Message Recieved - data updated");
        }
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

[System.Serializable]
public class DatabaseUser {
    public string id;
    public string userName;
    public string email;
    public string password;
    public List<DatabaseBuildings> userBuildings;
    public long totalExp;

    public DatabaseUser(string userid, string un, string e, string p, List<DatabaseBuildings> uB, long xp) {
        id = userid;
        userName = un;
        email = e;
        password = p;
        userBuildings = uB;
        totalExp = xp;
    }
}

[System.Serializable]
public class DatabaseBuildings
{
    public long buildingCode;
    public string buildingName;
    public int building_xp;
    public int height;
    public long modelGroup;
    public int primaryColour;
    public int secondaryColour;

    public DatabaseBuildings(long bc, string bn, int bx, int h, long mg, int pc, int sc)
    {
        buildingCode = bc;
        buildingName = bn;
        building_xp = bx;
        height = h;
        modelGroup = mg;
        primaryColour = pc;
        secondaryColour = sc;
    }
}

