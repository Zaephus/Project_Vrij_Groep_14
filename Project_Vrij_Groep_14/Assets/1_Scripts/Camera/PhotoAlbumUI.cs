using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumUI : MonoBehaviour
{
    [SerializeField]
    public PhotoAlbum photoAlbum;
    [SerializeField]
    private Transform itemSlotContainer;
    [SerializeField]
    private Transform itemSlotTemplate;


    private void OnEnable()
    {
        photoAlbum.OnPhotoListChanged += PhotoAlbum_OnPhotoListChanged;
    }

    private void OnDisable()
    {
        photoAlbum.OnPhotoListChanged -= PhotoAlbum_OnPhotoListChanged;
    }

    void PhotoAlbum_OnPhotoListChanged(object sender, System.EventArgs e)
    {
        RefreshPhotoAlbum();
    }

    void RefreshPhotoAlbum()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 250f;

        foreach (Sprite photo in photoAlbum.photos)
        {
            if (photo == null)
            {
                photoAlbum.RemovePhoto(photo);
            }
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            Sprite sprite = itemSlotRectTransform.GetComponent<PhotoUI>().photo.sprite;
            sprite=photo;
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if (x >= 3)
            {
                x = 0;
                y--;
            }
        }
    }
}
