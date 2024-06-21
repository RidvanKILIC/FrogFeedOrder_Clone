using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _itemProperties;
public abstract class Items : MonoBehaviour
{
    public _type itemType;
    public Sprite itemSprite;
    public _rotation itemRotation;
    public Material objectMaterial;
    public _color itemColor;
    public int point;
    public int rowIndex;
    public int columnIndex;
    public int listIndex;
    public  void setType(_type Type) 
    {
        itemType = Type;
    }
    public void setItemColor(_color color)
    {
        itemColor = color;
    }
    public _color getItemColor()
    {
        return itemColor;
    }
    public  void setSprite(Sprite _Sprite)
    {
        itemSprite = _Sprite;
    }
    public void setRotation(_rotation Rotation)
    {
        itemRotation = Rotation;
    }
    public void setIndexes(int row,int column,int index)
    {
        rowIndex = row;
        columnIndex = column;
        listIndex = index;
    }
    public void setPoint(int _point)
    {
        point = _point;
    }
    public void createAndSetMaterial()
    {
        if (itemSprite != null)
        {
            Material _mat = new Material(Shader.Find("Unlit/Texture"));
            if (_mat.shader == null)
            {
                Debug.LogError("Shader not found!");
            }
            else
            {
                _mat.SetTexture("_MainTex", itemSprite.texture);
                objectMaterial = _mat;
            }

        }
        else
        {
            Debug.Log("Set Sprite First");
        }

    }
    public abstract void setMatarial();
    public void rotateObject(GameObject item)
    {
        //Debug.Log(itemRotation);
        switch (itemRotation)
        {
            case _rotation.Down:
                item.transform.rotation =  Quaternion.Euler(item.transform.rotation.x, -90, item.transform.rotation.z);
                break;
            case _rotation.Up:
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, 90, item.transform.rotation.z);
                break;
            case _rotation.Right:
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, 180, item.transform.rotation.z);
                break;
            case _rotation.Left:
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, 0, item.transform.rotation.z);
                break;
            case _rotation.None:
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, 0, item.transform.rotation.z);
                break;
            default:
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, 0, item.transform.rotation.z);
                break;

        }
    }
}
