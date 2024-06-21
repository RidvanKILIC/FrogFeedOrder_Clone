using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (other.CompareTag("Fruit") || other.CompareTag("Rotater"))
        {
            if (other.gameObject.GetComponent<Items>().getItemColor() == this.transform.parent.GetComponent<Items>().getItemColor())
            {
                //Debug.Log(other.gameObject.GetComponent<Items>().getItemColor() + " " + this.transform.parent.GetComponent<Items>().getItemColor());
                var interact = other.gameObject.GetComponent<ICollectable>();
                if (interact != null)
                    interact.useEffect(this.transform.parent.gameObject);
                other.gameObject.GetComponent<Collider>().enabled = false;

            }
            else
            {
                
                this.transform.parent.GetComponent<FrogTongue>().startRetracting();
                _gameManager.collectInteractables();
                //Retract And OtherStuff
            }
        }
        else
        {
            //Debug.Log("Return");
            this.transform.parent.GetComponent<FrogTongue>().startRetracting();
            _gameManager.collectInteractables();
            //Retract And OtherStuff
        }
    }
}
