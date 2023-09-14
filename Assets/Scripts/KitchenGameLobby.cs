using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class KitchenGameLobby : MonoBehaviour
{
    private Lobby joinedLobby;
    public static KitchenGameLobby Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        IntializeUnityAuthentication();
    }

    private async void IntializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(Random.Range(0, 1000).ToString());
            await UnityServices.InitializeAsync(initializationOptions);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async void CreateLobby(string lobbyName, bool isPrivate)
    {
        try
        {
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, KitchenGameMultiplayer.MAX_PLAYER_AMOUNT, new CreateLobbyOptions {
                IsPrivate = isPrivate });

            KitchenGameMultiplayer.Instance.StartHost();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }
    public async void QuickJoin()
    {
        try
        {
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();

            KitchenGameMultiplayer.Instance.StartClient();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
