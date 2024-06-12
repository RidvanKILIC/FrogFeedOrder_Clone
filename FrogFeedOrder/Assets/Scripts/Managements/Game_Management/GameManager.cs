using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] levelManager _levelManager;
    [SerializeField]
    public GameObject[,] positionObjects = new GameObject[5, 5]; // Initialize as 5x5 matrix

}
