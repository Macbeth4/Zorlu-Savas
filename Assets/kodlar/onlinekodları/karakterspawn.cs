using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class karakterspawn : MonoBehaviour
{
    private void Start() {
        
   GameObject yenioyuncu = PhotonNetwork.Instantiate("karakter", Vector3.zero, Quaternion.identity, 0);

    }
}
