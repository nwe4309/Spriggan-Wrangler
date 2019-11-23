using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelCreate : MonoBehaviour
{
    int[,] level;

    public bool levelUsesIce;
    public bool levelUsesFire;
    public bool levelUsesWind;

    public int numOfIceUses;
    public int numOfFireUses;
    public int numOfWindUses;

    public GameObject iceSpell;
    public GameObject fireSpell;
    public GameObject windSpell;

    public List<GameObject> spellsInUse;

    public GameObject spellUIMidpoint;

    public GameObject solidBlock;
    public GameObject emptyBlock;
    public GameObject cageBlock; //prefab for cage
    public GameObject spriggan;
    public GameObject woodBlock;

    public GameObject spellBGLeft;
    public GameObject spellBGRight;
    public GameObject spellBGMid;
    public GameObject spellBGMidLeft;
    public GameObject spellBGMidRight;

    public Camera camera;

    public GameObject spellUsesText;

    Vector3 topLeftOfCamera;

    // For block randomization
    private RandPrefab randPrefabComponent;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel(StaticGameInformation.LevelNameToLoad);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CreateLevel(string levelName)
    {
        // Initializes level to be a 2D array that is 32 by 16
        level = new int[32, 16];

        // Grab adjacent script
        randPrefabComponent = GetComponent<RandPrefab>();

        topLeftOfCamera = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));

        LoadLevelFromFile(levelName);

        GenerateLevel();

        GenerateSpells();

        GenerateSpellBackground();

        gameObject.GetComponent<SpellManager>().usableSpells[1] = numOfIceUses;
        gameObject.GetComponent<SpellManager>().usableSpells[0] = numOfFireUses;
        gameObject.GetComponent<SpellManager>().usableSpells[2] = numOfWindUses;
    }

    public void LoadLevelFromFile(string levelName)
    {
        // Gets the path of the level textfile
        string path = string.Format("Assets/Resources/{0}.txt", levelName);

        // Creates a new StreamReader
        //StreamReader reader = new StreamReader(path);

        // Splits the textfile into an array of strings that are each line of the textfile
        string[] lines;// = reader.ReadToEnd().Split('|');

        TextAsset levelTxt = (TextAsset)Resources.Load(levelName, typeof(TextAsset));
        lines = levelTxt.text.Split('|');

        // Keeps track of which row is being filled
        int currentRow = 0;
        // Loop through the list of lines, stop at 17 since past that is not for the actual block placement
        // Start at index 1 to skip the first line
        for (int i = 1; i < 17; i++)
        {
            // Splits the current line into their individual values
            string[] levelValues = lines[i].Split(',');

            // Loop through that array of values
            for(int j = 0; j < levelValues.Length; j ++)
            {
                // Add that value into the level array
                level[j, currentRow] = int.Parse(levelValues[j]);
            }
            // Increment the current row
            currentRow++;
        }

        List<int> rawSpellInfo = new List<int>();
        // Now shifts focus to reading info on what and how many spells to be used
        // Starts at 18 to skip line of instructions for human use (level creator)
        for (int i = 18; i < lines.Length; i++)
        {
            // Splits the current line into their individual values
            string[] spellInfo = lines[i].Split(',');

            // Loop through that array of values, but only look at every other values (aka skip the words and only get the numbers)
            for(int j = 1; j < spellInfo.Length; j +=2)
            {
                // Add the numbers into an int list for use in variable initialization later
                rawSpellInfo.Add(int.Parse(spellInfo[j]));
            }
        }

        // Actually put the info from the text file into variables
        // If the 1st value in the list is a 1, set levelUsesIce to true, else set it to false
        levelUsesIce = (rawSpellInfo[0] == 1);
        // Sets the number of ice spell uses to the 2nd value
        numOfIceUses = rawSpellInfo[1];

        // If the 3rd value in the list is a 1, set levelUsesFire to true, else set it to false
        levelUsesFire = (rawSpellInfo[2] == 1);
        // Sets the number of fire spell uses to the 4th value
        numOfFireUses = rawSpellInfo[3];

        // If the 5th value in the list is a 1, set levelUsesWind to true, else set it to false
        levelUsesWind = (rawSpellInfo[4] == 1);
        // Sets the number of wind spell uses to the 6th value
        numOfWindUses = rawSpellInfo[5];

        //reader.Close();
    }

    public void GenerateLevel()
    {
        // Loops through the 2D level array
        for(int row = 0; row < level.GetLength(0); row ++)
        {
            for(int col = 0; col < level.GetLength(1); col ++)
            {
                // Gets an x & y position for the tile to be placed
                float xPos = ((topLeftOfCamera.x+0.5f) + row * 1);
                float yPos = ((topLeftOfCamera.y-0.5f) + col * -1);

                // Switch statement for the different types of tiles
                switch(level[row,col])
                {
                    // Places a transparent block if 0 (Replace with nothing after testing)
                    case 0:
                        emptyBlock.transform.position = new Vector2(xPos, yPos);
                        Instantiate(emptyBlock);
                        break;
                    // Places a solid block if 1
                    case 1:
                        solidBlock = randPrefabComponent.PickPrefab();
                        solidBlock.transform.position = new Vector2(xPos, yPos);
                        Instantiate(solidBlock);
                        break;
                    case 2: //spriggan
                        //instantiate an empty block behind the spriggan so that there isn't an empty space where the spriggan is spawned
                        emptyBlock.transform.position = new Vector2(xPos, yPos);
                        Instantiate(emptyBlock);

                        spriggan.transform.position = new Vector2(xPos, yPos);
                        Instantiate(spriggan);
                        break;
                    case 3: //cage block
                        cageBlock.transform.position = new Vector2(xPos, yPos);
                        Instantiate(cageBlock);
                        break;
                    // Places a wood block if 4
                    case 4:
                        woodBlock.transform.position = new Vector2(xPos, yPos);
                        Instantiate(woodBlock);
                        break;
                }
            }
        }

    }

    public void GenerateSpells()
    {
        // Finds what spells are in use for the current level and adds them to the list of spells
        if (levelUsesIce)
            spellsInUse.Add(iceSpell);
        if (levelUsesFire)
            spellsInUse.Add(fireSpell);
        if (levelUsesWind)
            spellsInUse.Add(windSpell);

        int counter = 1;
        for(int i = 0; i < spellsInUse.Count; i+=2)
        {
            // If the amount of spells to be placed is odd
            if(spellsInUse.Count % 2 == 1)
            {
                // If you are at the beginning of the loop
                if(i == 0)
                {
                    // Generate the first spell in the list at the UI midpoint (bottom center of the screen)
                    spellsInUse[i].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i].transform.Translate(0, 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i].transform.position);
                    spellUsesText.name = spellsInUse[i].name;
                    Instantiate(spellsInUse[i]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform,true));
                }
                else
                {
                    // Since the spells are being generated 2 at a time, the loop goes up by 2
                    // Generate the spell that is 1 before i in the list at the midpoint, then move it left counter number of blocks
                    spellsInUse[i - 1].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i - 1].transform.Translate(-1*counter, 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i - 1].transform.position);
                    spellUsesText.name = spellsInUse[i - 1].name;
                    Instantiate(spellsInUse[i - 1]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));

                    // Generate the spell that is at i in the list at the midpoint, then move it right counter number of blocks
                    spellsInUse[i].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i].transform.Translate(1*counter, 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i].transform.position);
                    spellUsesText.name = spellsInUse[i].name;
                    Instantiate(spellsInUse[i]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));
                    // Increase the counter used for spacing
                    counter++;
                }
            }
            // If the amount of spells to be place is even
            else
            {
                // If you are at the beginning of the loop
                if (i == 0)
                {
                    //Generate the spell at i in the list at the midpoint, then move it left counter number of half blocks
                    spellsInUse[i].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i].transform.Translate(-0.5f * counter, 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i].transform.position);
                    spellUsesText.name = spellsInUse[i].name;
                    Instantiate(spellsInUse[i]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));

                    //Generate the spell at i+1 in the list at the midpoint, then move it right counter number of half blocks
                    spellsInUse[i + 1].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i + 1].transform.Translate(0.5f * counter, 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i + 1].transform.position);
                    spellUsesText.name = spellsInUse[i + 1].name;
                    Instantiate(spellsInUse[i + 1]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));
                }
                else
                {
                    //Generate the spell at i in the list at the midpoint, then move it left counter number of half blocks
                    spellsInUse[i].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i].transform.Translate(0.5f + (-1 * counter), 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i].transform.position);
                    spellUsesText.name = spellsInUse[i].name;
                    Instantiate(spellsInUse[i]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));

                    //Generate the spell at i+1 in the list at the midpoint, then move it right counter number of half blocks
                    spellsInUse[i + 1].transform.position = spellUIMidpoint.transform.position;
                    // Move the spell back one in the z axis so it is on top of the spell background
                    spellsInUse[i + 1].transform.Translate(-0.5f + (1 * counter), 0, -1);
                    spellUsesText.transform.position = camera.WorldToScreenPoint(spellsInUse[i + 1].transform.position);
                    spellUsesText.name = spellsInUse[i + 1].name;
                    Instantiate(spellsInUse[i + 1]);
                    gameObject.GetComponent<SpellManager>().spellUsesTextList.Add(Instantiate(spellUsesText, GameObject.Find("Canvas").transform, true));
                }
                // Increase the counter used for spacing
                counter++;
            }
        }
    }

    public void GenerateSpellBackground()
    {
        int counter = 1;
        for(int i = 0; i < 5+spellsInUse.Count; i +=2)
        {
            // If the amount of spells to be placed is odd
            if (spellsInUse.Count % 2 == 1)
            {
                // If you are at the beginning of the loop
                if (i == 0)
                {
                    // Generate the middle background segment at the UI midpoint (bottom center of the screen)
                    spellBGMid.transform.position = spellUIMidpoint.transform.position;
                    Instantiate(spellBGMid);
                }
                else
                {
                    // Since the background segments are being generated 2 at a time, the loop goes up by 2
                    
                    // If there are only 2 segments left to place
                    if(i+2 >= 5+spellsInUse.Count)
                    {
                        // Place the left end bit at UI midpoint and move it left counter number of blocks
                        spellBGLeft.transform.position = spellUIMidpoint.transform.position;
                        spellBGLeft.transform.Translate(-1 * counter, 0, 0);
                        Instantiate(spellBGLeft);

                        // Place the right end bit at UI midpoint and move it right counter number of blocks
                        spellBGRight.transform.position = spellUIMidpoint.transform.position;
                        spellBGRight.transform.Translate(1 * counter, 0, 0);
                        Instantiate(spellBGRight);
                    }
                    // If there are 4 segements left to place
                    else if(i+4 >= 5+spellsInUse.Count)
                    {
                        // Place the left mid bit at UI midpoint and move it left counter number of blocks
                        spellBGMidLeft.transform.position = spellUIMidpoint.transform.position;
                        spellBGMidLeft.transform.Translate(-1 * counter, 0, 0);
                        Instantiate(spellBGMidLeft);

                        // Place the right mid bit at UI midpoint and move it right counter number of blocks
                        spellBGMidRight.transform.position = spellUIMidpoint.transform.position;
                        spellBGMidRight.transform.Translate(1 * counter, 0, 0);
                        Instantiate(spellBGMidRight);
                    }
                    else
                    {
                        // Place a mid bit at UI midpoint and move it left counter number of blocks
                        spellBGMid.transform.position = spellUIMidpoint.transform.position;
                        spellBGMid.transform.Translate(-1 * counter, 0, 0);
                        Instantiate(spellBGMid);

                        // Place a mid bit at UI midpoint and move it right counter number of blocks
                        spellBGMid.transform.position = spellUIMidpoint.transform.position;
                        spellBGMid.transform.Translate(1 * counter, 0, 0);
                        Instantiate(spellBGMid);
                    }
                    // Increase the counter used for spacing
                    counter++;
                }
            }
            // If the amount of spells to be placed is even
            else
            {
                // If you are at the beginning of the loop
                if (i == 0)
                {
                    // Place a mid bit at UI midpoint and move it left counter number of blocks
                    spellBGMid.transform.position = spellUIMidpoint.transform.position;
                    spellBGMid.transform.Translate(-0.5f * counter, 0, 0);
                    Instantiate(spellBGMid);

                    // Place a mid bit at UI midpoint and move it right counter number of blocks
                    spellBGMid.transform.position = spellUIMidpoint.transform.position;
                    spellBGMid.transform.Translate(0.5f * counter, 0, 0);
                    Instantiate(spellBGMid);
                }
                else
                {
                    // If there are only 2 segments left to place
                    if (i + 2 >= 5 + spellsInUse.Count)
                    {
                        // Place the left end bit at UI midpoint and move it left counter number of blocks
                        spellBGLeft.transform.position = spellUIMidpoint.transform.position;
                        spellBGLeft.transform.Translate(0.5f + (-1 * counter), 0, 0);
                        Instantiate(spellBGLeft);

                        // Place the right end bit at UI midpoint and move it right counter number of blocks
                        spellBGRight.transform.position = spellUIMidpoint.transform.position;
                        spellBGRight.transform.Translate(-0.5f + (1 * counter), 0, 0);
                        Instantiate(spellBGRight);
                    }
                    // If there are 4 segements left to place
                    else if (i + 4 >= 5 + spellsInUse.Count)
                    {
                        // Place the left mid bit at UI midpoint and move it left counter number of blocks
                        spellBGMidLeft.transform.position = spellUIMidpoint.transform.position;
                        spellBGMidLeft.transform.Translate(0.5f + (-1 * counter), 0, 0);
                        Instantiate(spellBGMidLeft);

                        // Place the right mid bit at UI midpoint and move it right counter number of blocks
                        spellBGMidRight.transform.position = spellUIMidpoint.transform.position;
                        spellBGMidRight.transform.Translate(-0.5f + (1 * counter), 0, 0);
                        Instantiate(spellBGMidRight);
                    }
                    else
                    {
                        // Place a mid bit at UI midpoint and move it left counter number of blocks
                        spellBGMid.transform.position = spellUIMidpoint.transform.position;
                        spellBGMid.transform.Translate(0.5f + (-1 * counter), 0, 0);
                        Instantiate(spellBGMid);

                        // Place a mid bit at UI midpoint and move it right counter number of blocks
                        spellBGMid.transform.position = spellUIMidpoint.transform.position;
                        spellBGMid.transform.Translate(-0.5f + (1 * counter), 0, 0);
                        Instantiate(spellBGMid);
                    }
                    // Increase the counter used for spacing
                    counter++;
                }
            }
           
        }
    }
}
