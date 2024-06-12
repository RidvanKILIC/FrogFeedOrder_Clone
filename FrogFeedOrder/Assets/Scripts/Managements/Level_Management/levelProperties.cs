using System.Collections.Generic;
using UnityEngine;
using _itemProperties;

namespace _levelProperties
{
    [System.Serializable]
    public class levelProperties
    {
        public int levelIndex;
        public int numberOfMovesLimit;
        public bool isUnlocked;
        public Color levelBGColor;

        [SerializeField]
        public List<_levelItem>[][] levelArray;

        public int rows = 5; // Set default rows to 5
        public int columns = 5; // Set default columns to 5

        // Constructor to initialize default values
        public levelProperties()
        {
            levelIndex = 0;
            numberOfMovesLimit = 0;
            isUnlocked = false;
            levelBGColor = Color.white;
            InitializeArray();
        }

        // Method to initialize the levelArray
        public void InitializeArray()
        {
            levelArray = new List<_levelItem>[rows][];
            for (int i = 0; i < rows; i++)
            {
                levelArray[i] = new List<_levelItem>[columns];
                for (int j = 0; j < columns; j++)
                {
                    levelArray[i][j] = new List<_levelItem>();
                }
            }
        }
    }
}
