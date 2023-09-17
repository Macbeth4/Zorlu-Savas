using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class sahne : MonoBehaviourPunCallbacks
{

   public void oyna(){
     SceneManager.LoadScene("oyna");
   }

   public void online(){
    SceneManager.LoadScene("onlinegiris");
   }
    public void menu(){
    SceneManager.LoadScene("menu");
   }

   public void yenidendene(){SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}
}
