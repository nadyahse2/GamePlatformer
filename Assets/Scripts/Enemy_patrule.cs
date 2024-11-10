using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy_patrule : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private bool left_move = true;
    public Transform Checkground;
    public Transform Checkground1;
    public LayerMask Ground;
    private bool Onground;
    private int dir = 1;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        ChekingGround();
        Move();
    }
     void Move()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime*dir);
        if (!Onground)
        {
            if (left_move)
            {
                dir = -1;
                left_move = false;
                sprite.flipX = true;
                
            }
            else
            {
                dir = 1;
                left_move = true;
                sprite.flipX = false;
                
            }
        }
    }
    void ChekingGround()
    {
        if (left_move)
        {
            Onground = (Physics2D.OverlapCircle(Checkground.position, 0.5f, Ground));
        }
        else
        {
            Onground = (Physics2D.OverlapCircle(Checkground1.position, 0.5f, Ground));
        }
    }
    public void Hurt()
    {
        Destroy(gameObject);
    }
}
