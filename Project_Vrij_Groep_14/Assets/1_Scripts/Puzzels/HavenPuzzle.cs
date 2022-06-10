using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavenPuzzle : Puzzle
{
    public PuzzleItem[] puzzleItems;
    public GameObject solvedTargetTransform;

    public Transform scaleTransform;

    public List<Collider> scaleColliders = new List<Collider>();

    private void Update()
    {
        switch (puzzleStatus)
        {
            case Status.Unsolved:
                solvedTargetTransform.SetActive(false);
                if (AllItemsOnScale() == true && IsBalanced() == true)
                {
                    puzzleStatus = Status.Solved;
                }
                break;
            case Status.Solved:
                solvedTargetTransform.SetActive(true);
                if (AllItemsOnScale() == false || IsBalanced() == false)
                {
                    puzzleStatus = Status.Unsolved;
                }
                break;
        }
    }
    public bool IsBalanced()
    {
        if(scaleTransform.rotation.eulerAngles.z <= 1 || scaleTransform.rotation.eulerAngles.z >= -1)
        {
            return true;
        }
        return false;
    }

    public bool AllItemsOnScale() {
        
        foreach(PuzzleItem item in puzzleItems) {
            if(item.onScale == false) {
                return false;
            }
        }
        return true;
    }
}

           

