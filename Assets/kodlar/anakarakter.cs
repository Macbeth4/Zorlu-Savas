using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class anakarakter : MonoBehaviour
{
    public FixedJoystick joystick;
    public Button atesetbutton;

   public static bool IsPointerDown = false;
   Rigidbody2D rb;
   Animator anim;
   Vector2 move;
   public float speed;
   SpriteRenderer spriteRenderer;

   public gameManager1 managergame;
   
   public GameObject kaybettin;

   [Header("MERMİ SİSTEMİ")]
   public GameObject bullet;
   public Transform atesnoktasi;
   public float mermihizi;
   public float mermisüre;
   float yokolmasüresi = 2;

    [Header("SES SİSTEMİ")]
    public AudioSource ses;
    public AudioClip mermisesi;
    public AudioClip hasar;
    public AudioClip cansesi;
    
   
   [Header("CAN SİSTEMİ")]
   public Image[] canlar = new Image[3];
   public int can = 3;
   int maxcan = 3;

   private void Start()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
      
      rb = GetComponent<Rigidbody2D>();
      //anim.GetComponent<Animator>();

   }


   private void Update() {
      mermisüre-=Time.deltaTime;

      if(can > maxcan){can = maxcan;}

        
   }

    private void FixedUpdate()
    {
        rb.angularVelocity = 0;
       
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis,vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f,0f,-zAxis);
   
        if(IsPointerDown){rb.velocity = Vector3.zero;}
        else{ rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);}
       
     
    }

    

    public void Shoot()
    {
        GameObject mermi = Instantiate(bullet,atesnoktasi.position,Quaternion.identity);
        ses.PlayOneShot(mermisesi);
        Rigidbody2D rigid2d = mermi.GetComponent<Rigidbody2D>();
        rigid2d.velocity = mermihizi * transform.up;
        Destroy(mermi,yokolmasüresi);
        
        
    }

    public void ateset(){
        
        if(mermisüre <= 0 ){Shoot(); mermisüre = 0.2f;}
       
    }

    public void Damage(int amount){
        canlar[can - 1].enabled = false;
        can -= amount;
        
    }
    public void Regen(int amount){
        can += amount;
        for(int i = 0; i < can; i++){
            canlar[i].enabled = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        
       
        if(other.gameObject.tag == "canarttir"){
            if(can < maxcan){
            Regen(1);
            ses.PlayOneShot(cansesi);
            Destroy(other.gameObject);
            }
        }
    }
    public GameObject blood;
    private void OnCollisionEnter2D(Collision2D other) {
         if (other.gameObject.tag == "zombi")
        {
            if(can > 0){
                Instantiate(blood,transform.position, Quaternion.identity);
                ses.PlayOneShot(hasar);
                StartCoroutine(FlashColor());
                Damage(1);
            }

            if(can <= 0){
                kaybettin.SetActive(true);
                Destroy(this.gameObject);
                Time.timeScale = 0;
                managergame.scoreText.alpha=1;
                managergame.textortala();
                managergame.UpdateHiscore();
                joystick.gameObject.SetActive(false);
                atesetbutton.gameObject.SetActive(false);
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
