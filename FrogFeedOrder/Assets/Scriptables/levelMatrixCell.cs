using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _itemProperties;
[CreateAssetMenu(fileName = "Level Matrix Cell", menuName = "Level Matrices/New Level Matrix Cell", order = 1)]
public class levelMatrixCell : ScriptableObject
{
    public List<_levelItem> cellObjects = new List<_levelItem>();
}
