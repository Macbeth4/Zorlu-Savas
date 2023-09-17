using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System.Threading.Tasks;
using System;

public class karakter : MonoBehaviour
{
   public FixedJoystick joystick;

   public static bool IsPointerDown = false;
   Rigidbody2D rb;
   Animator anim;
   Vector2 move;
   public float speed;
   

   [Header("MERMİ SİSTEMİ")]
   public GameObject bullet;
   public Transform atesnoktasi;
   public float mermihizi;
   public float mermisüre;
   public float mermisayisi;
   float yokolmasüresi = 2;
   public TextMeshProUGUI mermisayisitext;

   [Header("OYUNCU SESİ")]
   public AudioSource ses;
   public AudioClip mermisesi;
   public AudioClip cansesi;
   public AudioClip hasar;
   public AudioClip mermialma;
   
   [Header("CAN SİSTEMİ")]
   public Image[] canlar = new Image[3];
   public int can = 3;
   int maxcan = 3;

    [Header("ONLİNE KISIM")]
    PhotonView pw;
    public TextMeshProUGUI yazi;

   SpriteRenderer spriteRenderer;
   public Button atesetbutton;

    public GameObject kazandinpanel;
    public GameObject kaybettinpanel;
   private void Start()
   {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Canvas objesini bul
        GameObject canvas = GameObject.Find("Canvas");

        // Canvas objesinin altındaki panelleri bul
        kazandinpanel = canvas.transform.Find("kazandin").gameObject;
        kaybettinpanel = canvas.transform.Find("kaybettin").gameObject;
       
      mermisayisitext = GameObject.Find("Canvas/mermisayi").GetComponent<TMPro.TextMeshProUGUI>();
      yazi = GameObject.Find("Canvas/bekleniyor").GetComponent<TMPro.TextMeshProUGUI>();
      GameObject can1 = GameObject.Find("can1");
      GameObject can2 = GameObject.Find("can2");
      GameObject can3 = GameObject.Find("can3");
    joystick = FindObjectOfType<FixedJoystick>();
      pw = GetComponent<PhotonView>();
      rb = GetComponent<Rigidbody2D>();
      //anim.GetComponent<Animator>();
      
     if(pw.IsMine)
     {

        joystick = FindObjectOfType<FixedJoystick>();
        joystick.gameObject.SetActive(false);
        

         atesetbutton = GameObject.Find("ateset").GetComponent<Button>();
         atesetbutton.onClick.AddListener(()=> ateset());
         atesetbutton.gameObject.SetActive(false);

      if(PhotonNetwork.IsMasterClient){
            transform.position = new Vector3(-7,0,0);  
            InvokeRepeating("oyuncukontrol",0,0.5f);  
        }
        else if(!PhotonNetwork.IsMasterClient){
             transform.position = new Vector3(7,0,0);
             InvokeRepeating("oyuncukontrol1",0,0.5f);  
        }
     }

      canlar[0] = can1.GetComponent<Image>();
      canlar[1] = can2.GetComponent<Image>();
      canlar[2] = can3.GetComponent<Image>();
   }


    [PunRPC]
    public void ShowWinPanel()
    {
         kazandinpanel.SetActive(true);
         Time.timeScale = 0;
         joystick = FindObjectOfType<FixedJoystick>();
         joystick.gameObject.SetActive(false);
         atesetbutton = GameObject.Find("ateset").GetComponent<Button>();
         atesetbutton.gameObject.SetActive(false);
    }

    void oyuncukontrol()
   {

    if(PhotonNetwork.PlayerList.Length == 2)
    {
        pw.RPC("yazisil",RpcTarget.All,null);
        
        GameObject.Find("sayacseyi").GetComponent<PhotonView>().RPC("gerisaymayabasla",RpcTarget.All,null);

        CancelInvoke("oyuncukontrol");
    }
    if(PhotonNetwork.PlayerList.Length == 1){
       
    }
   }

   void oyuncukontrol1()
   {
    if(PhotonNetwork.PlayerList.Length == 2)
    {
        pw.RPC("yazisil",RpcTarget.All,null);

         CancelInvoke("oyuncukontrol1");
    }
   }
   [PunRPC]
   public async void yazisil()
   { 
     yazi.text = null;
     await Task.Delay(TimeSpan.FromSeconds(4.0f));
     joystick.gameObject.SetActive(true);
     atesetbutton.gameObject.SetActive(true); 
   }



    bool oyunbitti = true;
   private void Update() {
      mermisüre-=Time.deltaTime;

      if(can > maxcan){can = maxcan;}

        if(pw.IsMine){
          if(can == 0 && oyunbitti == true){
            kaybettinpanel.SetActive(true);
            Time.timeScale = 0;
            joystick = FindObjectOfType<FixedJoystick>();
            joystick.gameObject.SetActive(false);
            atesetbutton = GameObject.Find("ateset").GetComponent<Button>();
            atesetbutton.gameObject.SetActive(false);
            pw.RPC("ShowWinPanel", RpcTarget.Others);
            oyunbitti = false;
          }

          mermisayisitext.text = mermisayisi.ToString();
        }
   }

    private void FixedUpdate()
    {
       if(pw.IsMine)
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
     
    }

    

    public void Shoot()
    {
        if(pw.IsMine){
        if(mermisayisi > 0){
        GameObject mermi = PhotonNetwork.Instantiate("mermi",atesnoktasi.position,Quaternion.identity,0);
        ses.PlayOneShot(mermisesi);
        Rigidbody2D rigid2d = mermi.GetComponent<Rigidbody2D>();
        rigid2d.velocity = mermihizi * transform.up;
        mermisayisi--;
        Destroy(mermi,yokolmasüresi);
        }
        }
    }

    public void ateset(){
        
        if(mermisüre <= 0 ){Shoot(); mermisüre = 0.5f;}
       
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
        if(pw.IsMine)
        {
        if (other.gameObject.tag == "mermi")
        {
            if(can > 0)
            {
            Damage(1);
            ses.PlayOneShot(hasar);
            pw.RPC("SpawnBloodRPC", RpcTarget.All);
            pw.RPC("FlashColorRPC", RpcTarget.All);
            pw.RPC("silinmermi", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
            }
        }
        if(other.gameObject.tag == "canarttir")
        {
            if(can < maxcan)
            {
            Regen(1);
            ses.PlayOneShot(cansesi);
            pw.RPC("silin", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
            }

        }
        if(other.gameObject.tag == "mermiarttir"){
            mermisayisi+= 3;
            ses.PlayOneShot(mermialma);
            pw.RPC("silin", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        }
    }
    }

   [PunRPC]
public void silin(int objeID)
{
    // İlgili objeyi bul ve sil
    GameObject obje = PhotonView.Find(objeID).gameObject;
    Destroy(obje);
}

[PunRPC]
public void silinmermi(int objeID1)
{
    // İlgili objeyi bul ve sil
    GameObject obje1 = PhotonView.Find(objeID1).gameObject;
    Destroy(obje1);
}

[PunRPC]
   public void oyundan_kacti(){
    InvokeRepeating("oyuncukontrol",0,0.5f);
    yazi.text = "OYUNCU BEKLENIYOR...";
    
   }

    public Color damageColor = Color.red;
    public Color defaultColor = Color.white;
    public float flashDuration = 1f;

     [PunRPC]
    private void FlashColorRPC()
    {
        StartCoroutine(FlashColor());
    }

     private IEnumerator FlashColor()
    {
        // Hasar rengi olarak ayarla
        spriteRenderer.color = damageColor;

        // Belirtilen süre boyunca beklet
        yield return new WaitForSeconds(flashDuration);

        // Varsayılan rengi geri yükle
        spriteRenderer.color = defaultColor;
    }
    public GameObject bloodPrefab;
    [PunRPC]
    private void SpawnBloodRPC()
    {
        // Kan efektini oluştur ve senkronize et
        GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);

    }
}
