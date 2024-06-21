using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _itemProperties;
public class itemList : MonoBehaviour
{
    [SerializeField] List<_item> items = new List<_item>();
    [SerializeField] List<_itemPrefab> itemsPrefabs = new List<_itemPrefab>();
    // Start is called before the first frame update
    public List<_item> getItemList()
    {
        return items  ;
    }
    public List<_itemPrefab> getItemPrefabList()
    {
        return itemsPrefabs;
    }
    public GameObject getItemPrefab(_type Type)
    {
        _itemPrefab objectToReturn = itemsPrefabs.Find(x => x.itemType == (Type));
        if (objectToReturn.itemPrefab != null)
            return objectToReturn.itemPrefab;
        else return null;
    }
    public Sprite getItemTexture(_type Type,_color Color)
    {
        _item textureTotReturn = items.Find(x => x.itemType.Equals(Type) && x.itemColor.Equals(Color));
        if (textureTotReturn.itemSprite != null)
            return textureTotReturn.itemSprite;
        else
            return null;
    }
}
