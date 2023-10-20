using System;
using System.IO;
using UnityEngine;
using System.Xml;

public static class FileManager
{
    #region File Paths 

    // The file path for storing user data.
    private static string UserDataPath = Application.persistentDataPath + "/user_data.json";

    #endregion


    #region User Data Management
    /// <summary>
    /// Saves user data to a file.
    /// </summary>
    /// <param name="userData">The dictionary containing user data to be saved.</param>
    public static void SaveUserData(UserData myData)
    {
        try
        {
            // Serialize the UserData object to JSON
            string jsonData = JsonUtility.ToJson(myData);
            //Debug.Log("Save user data to file: " + jsonData);


            // Write the JSON data to the file
            File.WriteAllText(UserDataPath, jsonData);
            // Step 1: Create a new file to store the user data

        }
        catch (Exception e)
        {
            // An exception occurred during the saving process, log the error
            Debug.LogError("Error saving user data: " + e.Message);
        }
    }

    /// <summary>
    /// Loads user data from a file.
    /// </summary>
    /// <returns>The dictionary containing the loaded user data, or the default data if the file doesn't exist.</returns>
    public static UserData LoadUserData()
    {
        // Step 1: Initialize a dictionary to store user data
        UserData myData = new UserData();

        // Step 2: Check if the file exists
        if (File.Exists(UserDataPath))
        {
            try
            {
                // Read the JSON data from the file
                string jsonData = File.ReadAllText(UserDataPath);
                //Debug.Log("Get user data from file: " + jsonData);
                // Deserialize the JSON data to a UserData object
                UserData userData = JsonUtility.FromJson<UserData>(jsonData);

                return userData;
            }
            catch (Exception e)
            {
                // An exception occurred during the loading process, log the error
                Debug.LogError("Error loading user data: " + e.Message);
            }
        }
        else
        {
            // Step 3.2: If the file doesn't exist, retrieve the default data and save it to the file
            myData = new UserData();
            SaveUserData(myData);
        }

        // Step 4: Return the loaded user data
        return myData;
    }
    #endregion

   

}