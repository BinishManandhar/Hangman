using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderscoreScript : MonoBehaviour
{
    
    GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "Underscores") {
            Debug.Log("It is an Underscores");
            player = GameObject.FindGameObjectsWithTag("Player");
            
            //DestroyObject();
        }
        else
        {
            Debug.Log("Not an Underscores");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject() {
        for (int i = 0; i < player.Length; i++) {
            //Debug.Log("Player " + i + ": " + player[i].gameObject.ToString);
            Destroy(player[i].gameObject, 3.0f+i);
        }
    }
}
