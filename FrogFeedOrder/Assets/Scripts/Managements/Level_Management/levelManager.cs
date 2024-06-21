using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _levelProperties;

public class levelManager : MonoBehaviour
{
    [SerializeField]public List<levelProperties> levelItemsList = new List<levelProperties>();
    [SerializeField] public bool updateJson = false;
   
}
