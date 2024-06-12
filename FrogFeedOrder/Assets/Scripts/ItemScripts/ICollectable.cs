using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    public int _point { get; set; }
    public void collect();
}
