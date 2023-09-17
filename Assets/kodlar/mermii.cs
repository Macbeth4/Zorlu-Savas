using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermii : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "zombi"){Destroy(this.gameObject);}
   }
}
