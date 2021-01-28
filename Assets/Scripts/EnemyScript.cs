using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyScript : MonoBehaviour
{
    GroundDetector groundDetector;

    private GameObject player;

    private PossessScript ps;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundDetector = GetComponentInChildren<GroundDetector>();
        ps = GetComponentInChildren<PossessScript>();
    }

    // Update is called once per frame
    
    
    
    private void OnPossess()
    {
        if (ps.playerAround)
        {
            if (player.GetComponent<PlayerScript>().enabled)
            {
                GetComponent<PlayerScript>().enabled = true;
                player.GetComponent<PlayerScript>().enabled = false;
            }
            else
            {
                GetComponent<PlayerScript>().enabled = false;
                player.GetComponent<PlayerScript>().enabled = true;
            }
        }
    }
    
    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }
}
