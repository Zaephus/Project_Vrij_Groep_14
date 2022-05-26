using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotoAlbum")]
[System.Serializable]
public class PhotoAlbum : ScriptableObject
{
    public event EventHandler OnPhotoListChanged;
    public List<Sprite> photos;

    public void AddPhoto(Sprite photo)
    {
        photos.Add(photo);
        OnPhotoListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePhoto(Sprite photo)
    {
        photos.Remove(photo);
        OnPhotoListChanged?.Invoke(this, EventArgs.Empty);
    }
}
