using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public enum Status { Solved, Unsolved}
    public Status puzzleStatus = Status.Unsolved;
}
