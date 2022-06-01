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
                cameraOn = true;
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
               StartCoroutine(CapturePhoto());
            }
        }

        cameraCooldown -= Time.deltaTime;
        cameraTimer -= Time.deltaTime;

        animator.SetBool("CameraOn", cameraOn);
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
