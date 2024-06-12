using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _itemProperties
{
    [System.Serializable]
    public class itemProperties : MonoBehaviour
    {
        public GameObject cellPrefab;
        public GameObject fruitPrefab;
        public GameObject frogPrefab;
    }
    [System.Serializable]
    public struct _levelItem
    {
        public _levelCellItem cellObj;
        public _levelInteractableItem itemObj;    
    }
    [System.Serializable]
    public struct _levelCellItem
    {
        public _color cellColor;
    }
    [System.Serializable]
    public struct _levelInteractableItem
    {
        public _color itemColor;
        public _type itemType;
        public _rotation itemRotation;
    }
    [System.Serializable]
    public struct _item
    {
        public _color itemColor;
        public _type itemType;
        public Sprite itemSprite;
        public _rotation itemRotation;
        public Space _rotationSprite;
    }
    [System.Serializable]
    public enum _color
    {
        Blue,
        Green,
        Yellow,
        Purple,
        Orange
    }
    [System.Serializable]
    public enum _type
    {
        Null,
        Frog,
        Fruit,
        Rotater,
        Cell
    }
    [System.Serializable]
    public enum _rotation
    {
        None,
        Up,
        Left,
        Right,
        Down
    }
}

