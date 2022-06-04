using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Camera Parameters")]
    [SerializeField] float cameraCooldownLength;
    [SerializeField] bool cameraTimerOn;
    [SerializeField] float cameraTimerLength;
    float cameraCooldown;
    float cameraTimer;
    public bool cameraOn;
    
    [Header("Photo Taker")]
    [SerializeField] Image photoDisplayArea;
    [SerializeField] GameObject photoFrame;
    
    Texture2D screenCapture;
    public bool viewingPhoto;

    [Header ("Photo Holding")]
    [SerializeField] Image photoholdDisplayArea;
    [SerializeField] GameObject photoholdFrame;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] PlayerManager player;


    public event EventHandler OnLookThroughCamera;
    bool firstTimeLook = false;

    public event EventHandler OnTakePicture;
    bool firstTimePicture;


    // Start is called before the first frame update
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    // Update is called once per frame
    void Update()
    {
        //camera aanzetten
        if (Input.GetMouseButtonDown(1))
        {
            if (!cameraOn)
            {
                if (!firstTimeLook)                //check of het de eerste keer is dat speler door de lens kijkt
                {
                    OnLookThroughCamera?.Invoke(this, EventArgs.Empty);
                    firstTimeLook = true;
                }

                cameraOn = true;
                player.animator.SetBool("IsHolding",false);
            }
            else
            {
                cameraOn = false;
            }
        }

        //foto nemen
        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto && cameraOn)
            {
                if (!firstTimePicture)                //check of het de eerste keer is dat speler een foto neemt
                {
                    OnTakePicture?.Invoke(this, EventArgs.Empty);
                    firstTimePicture = true;
                }
                StartCoroutine(CapturePhoto());
            }
        }

        int playerLayer = LayerMask.NameToLayer("PlayerBody");
        if(cameraOn) {
            player.playerCamera.nearClipPlane = 0.01f;
            player.playerCamera.cullingMask &= ~(1 << playerLayer);
        }
        else {
            player.playerCamera.nearClipPlane = 0.15f;
            player.playerCamera.cullingMask |= (1 << playerLayer);
        }

        cameraCooldown -= Time.deltaTime;
        cameraTimer -= Time.deltaTime;

        animator.SetBool("CameraOn", cameraOn);

        if(player.animator.GetBool("IsHolding") == false) 
        { 
            photoholdFrame.SetActive(false);
        }
    }

    //enum en coroutine zodat het zeker is dat alle items in beeld zijn geladen voordat de foto wordt genomen
    IEnumerator CapturePhoto()
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        cameraOn = false;
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);

        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Pause;

    }

    public void SavePhoto()
    {
        MenuManager photoAlbumUI = FindObjectOfType<MenuManager>();
        photoAlbumUI.photoDisplayArea.sprite = photoDisplayArea.sprite;
        photoholdFrame.SetActive(false);
        RemovePhoto();
    }

    public void HoldPhoto()
    {
        photoholdDisplayArea.sprite = photoDisplayArea.sprite;
        player.animator.SetBool("IsHolding",true);
        photoholdFrame.SetActive(true);
        RemovePhoto();
    }

    public void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraTimer = cameraTimerLength;
        cameraCooldown = cameraCooldownLength;
        FindObjectOfType<MenuManager>().gameState = MenuManager.GameState.Play;
    }

}
