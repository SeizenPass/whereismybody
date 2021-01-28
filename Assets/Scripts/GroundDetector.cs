using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    int collisions = 0;
    public bool IsAtGround => collisions != 0;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        collisions++;    
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        collisions--;    
    }
}
