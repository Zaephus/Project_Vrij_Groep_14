using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavenPuzzle : MonoBehaviour
{
    public PuzzleItem[] puzzleItems;
    public PuzzleItem referenceOutcome;

    public List<Collider> scaleColliders = new List<Collider>();

    public bool AllItemsOnScale() {
        
        foreach(PuzzleItem item in puzzleItems) {
            if(item.onScale == false) {
                return false;
            }
        }

        return true;

    }
}

           

