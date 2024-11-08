using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jump_force = 20f;
    [SerializeField] private int lives = 3;
    [SerializeField] private int coins = 0;

    private Rigidbody2D rd;
    private Animator anim;
    private SpriteRenderer sprite;
    
    public Transform CheckGround;
    public LayerMask Ground;
    public Text count_coins;
    public Image[] Hearts;
    public Image[] Hearts_empty;
    public Text Count_money;
    public Canvas game_over;
    public Image[] hearts;
    public Sprite full_heart;
    public Sprite empty_heart;

    private bool flag;
    private bool Check_hurt = false;
    private bool isGround;
    private bool Oncollision= true;

    private Collider2D enemy_col;


    private States State
    {
        get { return (States)anim.GetInteger("s"); }
        set { anim.SetInteger("s", (int)value); }
    }


    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        
    }
    
    void Start()
    {
        game_over.enabled = false;
        Count_money.enabled = false;
        for(int i=0;i< hearts.Length; i++)
        {
            hearts[i].sprite = full_heart;
            Hearts[i].enabled = false;
            Hearts_empty[i].enabled = false;
        }
    }
 

    void Update()
    {
        CheckingGround();
        if (isGround && !Check_hurt) State = States.Rest;
        
        

        
        
        if (Input.GetButton("Horizontal"))
        {
            Move();
           
        }
        if (isGround && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Check_hurt) Check_hurt = false;


    }



    void Move()
    {
        if (isGround && !Check_hurt) State = States.Run;
        Vector3 tempvector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
        if(tempvector.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
    void Jump()
    {

        rd.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
        

    }

   
    private void CheckingGround()
    {
        isGround = Physics2D.OverlapCircle(CheckGround.position, 0.5f, Ground);
       
        if (!isGround && !Check_hurt) State = States.Jump;
        

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "enemy_p")
        {
            
           
            
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, Oncollision);
            Enemy_patrule enemy = collision.collider.GetComponent<Enemy_patrule>();
            flag = false;
            
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                         enemy.Hurt();
                         flag = true;
                         
                }
                    
            }
            if (!flag)
            {

                Check_hurt = true;
                State = States.Die_1;
                lives--;
                Hurt();
                enemy_col = collision.collider;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
                Invoke("EnableCollision", 1f);





            }
            

        }
        if(collision.gameObject.tag == "enemy")
        {
            Check_hurt = true;
            State = States.Die_1;
            lives--;
            Hurt();
            enemy_col = collision.collider;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
            Invoke("EnableCollision", 1f);


        }

        if (collision.gameObject.tag == "thorns")
        {
            lives = 0;
            Hurt();
            
            
        }
        if(collision.gameObject.tag == "coin")
        {
            coins++;
            Destroy(collision.gameObject);
            count_coins.text = coins.ToString();

        }
        if (collision.gameObject.tag == "exit")
        {
            Die_Win();

        }
        

    }
    private void EnableCollision()
    {
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy_col, false);
    }
    private void Hurt()
    {
       
       
        hearts[lives].sprite = empty_heart;
        
        if (lives == 0)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = empty_heart;
            }
            coins = 0;
            Die_Win();
        }
        
    }
    private void Die_Win()
    {
        Time.timeScale = 0;
        game_over.enabled = true;
        Count_money.enabled = true;
        for (int i = 0; i < lives; i++)
        {

            Hearts[i].enabled = true;

        }
        for (int i = lives; i < 3; i++)
        {
            Hearts_empty[i].enabled = true;
        }
        Count_money.text += " "+coins.ToString();
        Count_money.enabled = true;
        

        
    }
    

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_1");
    }

    public void Restart3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level3");
    }

}

public enum States { 
    Rest,
    Run, 
    Jump,
    Die_1
}



