using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _levelProperties
{
    [System.Serializable]
    public class levelProperties
    {
       public int levelIndex;
       public int numberOfMovesLimit;
       public bool isUnlocked;
       public Color levelBGColor;
        public levelMatrix matrix;
    }
}
