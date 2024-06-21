using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotaterContainer : Items,ICollectable
{
    public int _point { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float RotationAngle { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
    }
    public override void setMatarial()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = this.objectMaterial;
    }
    public void collect()
    {
        
    }

    public void useEffect(GameObject iteractedObj)
    {
        Vector3 rotation=Vector3.zero;
        switch (this.itemRotation)
        {
            case _itemProperties._rotation.Down:
                rotation = Vector3.back;
                break;
            case _itemProperties._rotation.Up:
                rotation= Vector3.forward;
                break;
            case _itemProperties._rotation.Right:
                rotation = Vector3.right;
                break;
            case _itemProperties._rotation.Left:
                rotation = Vector3.left;
                break;
        }
        iteractedObj.GetComponent<FrogTongue>().ChangeDirection(rotation);
        GameObject.Find("GameManager").GetComponent<GameManager>().addItemToCurrentInteractedList(this.gameObject);
    }
}
