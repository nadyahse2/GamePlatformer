using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private float speed = 5f;
    private float jump_force = 20f;
    private int lives = 3;
    private int coins = 0;

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
    public AudioSource damage;
    public AudioSource coin;
    private int[] mas = { 4, 8,10,12 };

    public bool Check_win;
    public bool Check_die;
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
        Check_win = false; 
        Check_die = false;
        int ind_scene = SceneManager.GetActiveScene().buildIndex;
        string Posx = "PosX" + ind_scene;
        string Posy = "PosY" + ind_scene;
        string Posz = "PosZ" + ind_scene;
        if (PlayerPrefs.HasKey(Posx) && PlayerPrefs.HasKey(Posy) && PlayerPrefs.HasKey(Posz))
        {
            float posx = PlayerPrefs.GetFloat(Posx);
            float posy = PlayerPrefs.GetFloat(Posy);
            float posz = PlayerPrefs.GetFloat(Posz);
            transform.position = new Vector3(posx, posy, posz);
            PlayerPrefs.DeleteKey(Posx);
            PlayerPrefs.DeleteKey(Posy);
            PlayerPrefs.DeleteKey(Posz);

        }
        
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
                enemy_col = collision.collider;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
                damage.Play();
                
                Invoke("EnableCollision", 1.2f);
                Invoke("StopSound", 0.2f);
                
                

            }
            
            

        }
        if(collision.gameObject.tag == "enemy")
        {
            Check_hurt = true;
            State = States.Die_1;
            lives--;
            enemy_col = collision.collider;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
            
            damage.Play();
            Invoke("EnableCollision", 1.2f);
            Invoke("StopSound", 0.2f);
        
            
            

        }

        if (collision.gameObject.tag == "thorns")
        {
            lives = 0;
            Hurt();
            
            
        }
        if(collision.gameObject.tag == "coin")
        {
            coin.Play();
            coins++;
            Destroy(collision.gameObject);
            count_coins.text = coins.ToString();
            Invoke("StopSound", 0.4f);
        }
        if (collision.gameObject.tag == "exit")
        {
            Check_win = true;
            int ind_l = SceneManager.GetActiveScene().buildIndex;
            if(coins == mas[ind_l])
            {
                string lev = "L" + ind_l;
                PlayerPrefs.SetInt(lev, 1);
            }
            
            Die_Win();

        }
        if(collision.gameObject.tag == "disappear")
        {
            Destroy(collision.gameObject,0.3f);
            
        }
        

    }
    private void EnableCollision()
    {
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy_col, false);
        Check_hurt = false;
        Hurt();
    }

    private void StopSound()
    {
        damage.Stop();
        coin.Stop();
    }
    private void Hurt()
    {

        if (lives > 0) { hearts[lives].sprite = empty_heart; }
        
        
        if (lives == 0)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = empty_heart;
            }
            coins = 0;
            Check_die = true;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {

        SceneManager.LoadScene("MainMenu");
    }

}

public enum States { 
    Rest,
    Run, 
    Jump,
    Die_1
}



