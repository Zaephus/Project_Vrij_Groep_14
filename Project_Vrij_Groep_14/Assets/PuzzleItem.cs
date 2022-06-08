using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public bool onScale = false;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < FindObjectOfType<HavenPuzzle>().scaleColliders.Count; i++)
        {
            if (other == FindObjectOfType<HavenPuzzle>().scaleColliders[i])
            {
                onScale = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < FindObjectOfType<HavenPuzzle>().scaleColliders.Count; i++)
        {
            if (other == FindObjectOfType<HavenPuzzle>().scaleColliders[i])
            {
                onScale = false; ;
            }
        }
    }
}
