using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitContaniner : Items,ICollectable
{
    public int _point { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float RotationAngle { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void setMatarial()
    {
        this.gameObject.GetComponent<Renderer>().material = this.objectMaterial;
    }
    public void collect()
    {
        _point = this.point;

    }

    public void useEffect(GameObject interactedObj)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().increasePointEarned();
        GameObject.Find("GameManager").GetComponent<GameManager>().addItemToCurrentInteractedList(this.gameObject);
    }
}
