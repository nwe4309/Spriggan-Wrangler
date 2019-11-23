using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SaveMechanic : MonoBehaviour
{
    string path;

    public List<bool> levelComplete;

    // Start is called before the first frame update
    void Start()
    {
        path = "Assets/Resources/SaveFile.txt";
        levelComplete = new List<bool>() { false, false, false, false, false, false, false };

        ReadFromFile();
    }

    void Awake()
    {
        // Destroy all other versions of this when changing scenes
        GameObject[] saveManagers = GameObject.FindGameObjectsWithTag("SaveManager");
        if (saveManagers.Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            for (int i = 1; i < saveManagers.Length; i++)
            {
                Destroy(saveManagers[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadFromFile()
    {
        // If the file exists
        if(File.Exists(path))
        {
            // Open it
            //StreamReader reader = new StreamReader(path);

            // Put the data into an array of strings
            string[] data;// = reader.ReadToEnd().Split(',');
            TextAsset dataTxt = (TextAsset)Resources.Load("SaveFile", typeof(TextAsset));
            data = dataTxt.text.Split(',');

            // Loop through the data and put each string into a bool in a list of bools (skip the last one since it's just a comma)
            for (int i = 0; i < data.Length-1; i ++)
            {
                levelComplete[i] = bool.Parse(data[i]);
            }

            //reader.Close();
        }
        // If file doesn't exist
        else
        {
            SaveToFile();
        }
    }

    public void SaveToFile()
    {
        // Clears the text file before writing to it
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path,true);

        // Loop through all the booleans of the levels being complete and add it to the save file
        for(int i = 0; i < levelComplete.Count; i ++)
        {
            writer.Write(string.Format("{0},", levelComplete[i]));
        }

        writer.Close();

        // Reload the save file asset in the editor
        //AssetDatabase.ImportAsset(path);
    }
}
