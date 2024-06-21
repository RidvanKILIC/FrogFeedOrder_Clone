using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogContainer : Items
{
    public bool clicked = false; 
    frogContainer clickable;
    public override void setMatarial()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = this.objectMaterial;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit= new RaycastHit();
            if (Physics.Raycast(ray, out hit, 100f))
            {
                // Check if the object hit has the tag "Frog"
                if (hit.collider.CompareTag("Frog"))
                {
                    // Try to get the ClickableObject component from the hit object
                     clickable= hit.collider.GetComponent<frogContainer>();
                    if ((clickable != null && !clickable.clicked) && 
                        (GameObject.Find("GameManager").GetComponent<GameManager>().getfrogsClicable() 
                        && !GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver)
                        && !GameObject.Find("GameManager").GetComponent<GameManager>().isGamePaused)
                    {
                        //Debug.Log("Clicked:" + hit.collider.gameObject.name);
                        clickable.clickAction();
                        clickable.clicked = true;
                        GameObject.Find("GameManager").GetComponent<GameManager>().decreaseNumberOfCount();
                    }
                }
            }
        }

    }
    public void clickAction()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().clearCurrentInteractedList();
        //Debug.Log("Called:" + this.gameObject.name);
        this.gameObject.GetComponent<FrogTongue>().StartExtendingTongue();
        GameObject.Find("GameManager").GetComponent<GameManager>().addItemToCurrentInteractedList(this.gameObject);
        //Debug.Log("Clicked");
    }
}
