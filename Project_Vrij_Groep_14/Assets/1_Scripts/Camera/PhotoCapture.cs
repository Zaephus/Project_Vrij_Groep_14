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
    [SerializeField] GameObject photoHoldFramePrefab;
    //[SerializeField] Image photoholdDisplayArea;
    //[SerializeField] GameObject photoholdFrame;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] PlayerManager player;

    [Header("Audio")]
    [SerializeField] AudioManager audio;


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
        if(player.hasCamera && FindObjectOfType<MenuManager>().gameState == MenuManager.GameState.Play) {
            //camera aanzetten
            if (Input.GetMouseButtonDown(1))
            {
                audio.Play("Camera Equip");
                audio.Play("Ambience 2");
                if (!cameraOn)
                {
                    if (!firstTimeLook)                //check of het de eerste keer is dat speler door de lens kijkt
                    {
                        OnLookThroughCamera?.Invoke(this, EventArgs.Empty);
                        firstTimeLook = true;
                    }

                    cameraOn = true;
                    if(player.playerInteract.isHolding) {
                        player.playerInteract.dropable.DropItem();
                    }
                    player.playerInteract.isHolding = false;
                }
                else
                {
                    cameraOn = false;
                    audio.Stop("Ambience 2");
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
                        cameraOn = false;
                    }
                    else {
                        StartCoroutine(CapturePhoto());
                    }
                }
            }

            cameraCooldown -= Time.deltaTime;
            cameraTimer -= Time.deltaTime;

            animator.SetBool("CameraOn", cameraOn);
        }

        int playerLayer = LayerMask.NameToLayer("PlayerBody");
        if(cameraOn) {
            player.playerCamera.nearClipPlane = 0.01f;
            player.playerCamera.cullingMask &= ~(1 << playerLayer);
        }
        else {
            player.playerCamera.nearClipPlane = 0.3f;
            player.playerCamera.cullingMask |= (1 << playerLayer);
        }

    }

    //enum en coroutine zodat het zeker is dat alle items in beeld zijn geladen voordat de foto wordt genomen
    IEnumerator CapturePhoto()
    {
        audio.Play("Camera Sound");
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
        audio.Play("Picture Accept");
        MenuManager photoAlbumUI = FindObjectOfType<MenuManager>();
        photoAlbumUI.photoDisplayArea.sprite = photoDisplayArea.sprite;
        RemovePhoto();
    }

    public void HoldPhoto()
    {
        GameObject holdPhoto = Instantiate(photoHoldFramePrefab,player.playerInteract.holdTransform.position,player.playerInteract.holdTransform.rotation);
        player.playerInteract.dropable = holdPhoto.GetComponent<IDropable>();
        player.playerInteract.isHolding = true;
        HoldPhotoFrame holdPhotoFrame = holdPhoto.GetComponent<HoldPhotoFrame>();
        holdPhotoFrame.photoHoldDisplayArea.sprite = photoDisplayArea.sprite;
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
