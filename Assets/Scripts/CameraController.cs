using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 temp = transform.position;
        pos = player.position;
        temp.x = pos.x;
        temp.y = pos.y+5f;
        transform.position = temp;

        
    }
}
