using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;

public class RoomListPrefab : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public RoomInfo info;

    public void SetInfo(RoomInfo _info)
    {
        info = _info;
        text.text = info.PlayerCount.ToString() + " / " + info.MaxPlayers.ToString() + "  " + info.Name;
    }

    public void ClickRoom ()
    {
        Sunucu.Instance.RoomJoin(info);
    }
}
