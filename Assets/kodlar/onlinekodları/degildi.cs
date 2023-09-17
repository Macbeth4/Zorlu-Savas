using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class degildi : MonoBehaviour
{
    PhotonView pw;
    private void Start() {
        pw = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            pw.RPC("silin", RpcTarget.All);
        }
    }

    [PunRPC]
    public void silinn(){
        Destroy(this.gameObject);
    }
}
