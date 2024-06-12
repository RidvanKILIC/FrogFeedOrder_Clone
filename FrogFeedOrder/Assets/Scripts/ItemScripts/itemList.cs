using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _itemProperties;
public class itemList : MonoBehaviour
{
    [SerializeField] List<_item> items = new List<_item>();
    // Start is called before the first frame update
    public _item getItem()
    {
        _item itemToReturn = new _item();
        return itemToReturn  ;
    }
   
}
