using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ses : MonoBehaviour
{

        private static ses instance;
       public AudioSource sess;
    
    void Awake()
    {
        // Eğer başka bir SingletonObject nesnesi varsa bu nesneyi yok et
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Singleton nesneyi ayarla ve sahneler arasında yok etme
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Ekran çözünürlüğünü sadece başlangıç sahnesinde değiştir
        if (SceneManager.GetActiveScene().name == "giris")
       {
            sess.Play();
       }
        
    }

}
