using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class yonet : MonoBehaviourPunCallbacks
{

    static yonet yonetici=null;

   private void Start() 
   {
    if(yonetici==null){yonetici=this;  DontDestroyOnLoad(this.gameObject);}
    else{Destroy(this.gameObject);}
        
    PhotonNetwork.ConnectUsingSettings();

   }

    public override void OnConnectedToMaster()
    {
        Debug.Log("servere girildi");
        if(!PhotonNetwork.InLobby){
        PhotonNetwork.JoinLobby();
        }
    
    }

    public override void OnJoinedLobby()
    {
        print("lobiye girildi");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("odaya girildi");

        GameObject yenioyuncu = PhotonNetwork.Instantiate("karakter",Vector3.zero,Quaternion.identity,0,null);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        GameObject.FindWithTag("Player").GetComponent<PhotonView>().RPC("oyundan_kacti",RpcTarget.All,null);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("menu");
        Debug.Log("odadan cikildi");
    }

    public override void OnJoinRoomFailed(short returnCode,string message){
        Debug.Log("HATA : oda giris hatasi");
    }

    public override void OnJoinRandomFailed(short returnCode,string message){
        Debug.Log("HATA : herhangi bir oda giris hatasi");
    }

    public override void OnCreateRoomFailed(short returnCode,string message){
        Debug.Log("HATA : oda kurulamadı");
    }
    



}

