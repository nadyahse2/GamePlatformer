using UnityEngine;
using UnityEngine.U2D;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Spring_controller : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite_spring;

    public Sprite spring1;// Спрайт пружины в начальном состоянии
    public Sprite spring2;// Спрайт пружины после активации

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        sprite_spring = GetComponentInChildren<SpriteRenderer>();
    }

    // Устанавливаем начальный спрайт пружины
    void Start()
    {
        sprite_spring.sprite = spring1;
    }

    
    void Update()
    {
        
    }
    // Проверяем, столкнулись ли с объектом героя  и если да, активируем пружину 
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "hero")
        {
            sprite_spring.sprite = spring2;
            Invoke("Delay", 0.3f);


        }


    }
    // (функция для задержки) Добавляем импульс к объекту героя для прыжка
    void Delay()
    {
        Rigidbody2D rb = FindObjectOfType<Hero>().GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 30f, ForceMode2D.Impulse);
        sprite_spring.sprite = spring1;
    }
    
}
