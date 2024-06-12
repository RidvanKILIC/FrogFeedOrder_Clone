using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _levelProperties;
public class levelManager : MonoBehaviour
{
  public List<levelProperties> levelItemsList = new List<levelProperties>();
    // Initialize the list with some default level properties
    void Awake()
    {
        levelItemsList.Add(new levelProperties());
    }
}

