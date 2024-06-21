using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _itemProperties;
public class cellContainer : Items
{
    public  override void setMatarial()
    {
        if(this.gameObject.GetComponent<Renderer>().materials.Length > 0)
        {
            Material[] materials = this.gameObject.GetComponent<Renderer>().materials;
            Material[] newMaterials = new Material[materials.Length + 1];
            // Copy the original materials into the new array
            for (int i = 0; i < materials.Length; i++)
            {
                newMaterials[i] = materials[i];
            }
            // Assign the new material to the last slot
            newMaterials[materials.Length] = this.objectMaterial;
            System.Array.Reverse(newMaterials);
            this.gameObject.GetComponent<Renderer>().materials = newMaterials;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = this.objectMaterial;
        }
    }
}
