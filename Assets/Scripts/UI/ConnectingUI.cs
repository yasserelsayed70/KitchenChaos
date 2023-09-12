using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ConnectingUI : MonoBehaviour
{

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnTringToJoinGame += KitchenGameMultiplayer_OnTringToJoinGame;
        KitchenGameMultiplayer.Instance.OnFailToJoinGame += KitchenGameMultiplayer_OnFailToJoinGame;

        Hide();
    }

    private void KitchenGameMultiplayer_OnFailToJoinGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameMultiplayer_OnTringToJoinGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnTringToJoinGame -= KitchenGameMultiplayer_OnTringToJoinGame;
        KitchenGameMultiplayer.Instance.OnFailToJoinGame -= KitchenGameMultiplayer_OnFailToJoinGame;
    }
}
