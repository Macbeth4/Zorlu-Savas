using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Sunucu : MonoBehaviourPunCallbacks
{
   // public static Sunucu Instance=null;

    [SerializeField] TMP_InputField RoomName;
    [SerializeField] TMP_Text RoomNameText;
    [SerializeField] TMP_Text Error;
    [SerializeField] Transform RoomContent;
    [SerializeField] GameObject RoomList;
    [SerializeField] GameObject PlayerList;
    [SerializeField] Transform PlayerContent;
    [SerializeField] Button baslat;

    public static Sunucu Instance = null;

    private void Awake() {
      // if(Instance==null){Instance=this;  DontDestroyOnLoad(this.gameObject);}
    //else{Destroy(this.gameObject);}
    }
    void Start()
    {

        
        if(!PhotonNetwork.IsConnected){
        print("Sunucuya Bağlanılıyor");
        PhotonNetwork.ConnectUsingSettings();
        }
    }
   public void Update()
{
   if (!PhotonNetwork.IsConnected)
    {
        // Photon ağına bağlı değilseniz
        // Disconnect işlemini gerçekleştirin
        PhotonNetwork.Disconnect();

        // Ardından ConnectUsingSettings işlemini gerçekleştirin
        PhotonNetwork.ConnectUsingSettings();
    }
}


    public override void OnConnectedToMaster()
    {
        print("Sunucuya Bağlanıldı");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Lobiye Girildi");
        PhotonNetwork.NickName = "OYUNCU" + Random.Range(0, 10).ToString();
        //Nick.text = PhotonNetwork.NickName;
        MenuManager.Instance.OpenMenu("Title");
    }

    public void OdaKur ()
    {
        if(PhotonNetwork.InLobby){
           
            StartCoroutine(OdaKurCoroutine());
        }
        else{
        print("Sunucuya Bağlanılıyor");
        Start();
        OnConnectedToMaster();
        OnJoinedLobby();
        }
    }
    
private IEnumerator OdaKurCoroutine()
{
    yield return new WaitForSeconds(2f);

    if (string.IsNullOrEmpty(RoomName.text))
    {
        MenuManager.Instance.OpenMenu("Title");
    }
    else
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default, null);
        MenuManager.Instance.OpenMenu("Loading");
    }
}
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("RoomMenu");
        RoomNameText.text = RoomName.text;
        
        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in PlayerContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerList, PlayerContent).GetComponent<PlayerListPrefab>().SetPlayer(players[i]);
        }
        if(PhotonNetwork.CountOfPlayers >= 1)
        {
            baslat.interactable = true;
        }
        else
        {
            baslat.interactable = false;
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Error.text = "Oda Olustururken Bir Hata Oluştu" + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public void Odadancik()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("onlinegiris");
        Debug.Log("odadan cikildi");
        MenuManager.Instance.OpenMenu("Title");
    }

    public void RoomJoin(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
        RoomNameText.text = RoomName.text;
    }

    public void RandomJoinRoom ()
    {
        PhotonNetwork.JoinRandomRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in RoomContent)
        {
            Destroy(transform.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(RoomList, RoomContent).GetComponent<RoomListPrefab>().SetInfo(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerList, PlayerContent).GetComponent<PlayerListPrefab>().SetPlayer(newPlayer);
    }


    public void OyunuBaslat()
{
    if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
    {
        photonView.RPC("RPC_OyunuBaslat", RpcTarget.All);
    }
}

[PunRPC]
private void RPC_OyunuBaslat()
{
    PhotonNetwork.LoadLevel("onlinesahne");
}


}
