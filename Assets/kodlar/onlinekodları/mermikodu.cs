using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class mermikodu : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "kutu"){Destroy(this.gameObject);}
   }
}
