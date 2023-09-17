using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombikod : MonoBehaviour
{
    public float speed;
    public float durmamesafesi;
    public float rotationSpeed;

    SpriteRenderer spriteRenderer;
    gameManager1 managergame;
    

    private Transform target; //hedefimiz karakterin pozisyonu

    public float destroyDelay = 10f;

    [Header("SES")]
    public AudioSource ses;
    public AudioClip hasar;

    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       managergame = GameObject.Find("gameManager1").GetComponent<gameManager1>();
       Invoke("DestroyObject", destroyDelay);
       Time.timeScale=1;
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void enemyFollow()
    {
    if (Time.timeScale == 1)
    {
        if (Vector2.Distance(transform.position, target.position) > durmamesafesi)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Dönme işlemi
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    }
    
    void DestroyObject()
    {
        Destroy(gameObject);
    }
    Vector2 move;
    void Update()
    {

         enemyFollow();

         if(Time.timeScale == 0){
          Destroy(gameObject);
         }

        
    }
     public GameObject blood;
     public int health;
    public int inithealth;
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mermi")
        {
            Instantiate(blood,transform.position, Quaternion.identity);
            StartCoroutine(FlashColor());
            health -= 2;
            ses.PlayOneShot(hasar);
            if(health <= 0)
            {
                Destroy(gameObject);
                managergame.skoruarttir();
            }
        }
    }
   
    public Color damageColor = Color.red;
    public Color defaultColor = Color.white;
    public float flashDuration = 1f;
     private IEnumerator FlashColor()
    {
        // Hasar rengi olarak ayarla
        spriteRenderer.color = damageColor;

        // Belirtilen süre boyunca beklet
        yield return new WaitForSeconds(flashDuration);

        // Varsayılan rengi geri yükle
        spriteRenderer.color = defaultColor;
    }

}
