using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class menubutton : MonoBehaviourPunCallbacks
{
    public PhotonView pw;
    public void odadancikk(){
        PhotonNetwork.LoadLevel("onlinegiris");
    }
}