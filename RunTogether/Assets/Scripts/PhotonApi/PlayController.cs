using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public RectTransform player;
    public PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        player = GetComponent<RectTransform>();
        player.SetParent(FindObjectOfType<PhotonManager>().bg);
        player.anchoredPosition = Vector2.zero;
    }
    void Update()
    {
        if (!view.IsMine) return;
        if (Input.GetKey(KeyCode.W))
        {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, player.anchoredPosition.y + 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x - 1, player.anchoredPosition.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, player.anchoredPosition.y - 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x + 1, player.anchoredPosition.y);
        }
    }
}
