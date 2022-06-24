using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    public District harbor;
    public District endDistrict;

    public Tutorial tutorialPuzzle;
    public HavenPuzzle havenPuzzle;

    [Header("Audio")]
    [SerializeField] AudioManager audioManager;

    public void Start() {
        tutorialPuzzle.IsSolved += OnTutorialSolved;
        havenPuzzle.IsSolved += OnHavenSolved;
    }

    void PlayCutscene(District target)
    {
        audioManager.Play("Level Complete");
    }

    void OnTutorialSolved(object sender,System.EventArgs e) {
        Debug.Log("Tutorial is solved");
        harbor.OpenGate();
        tutorialPuzzle.IsSolved -= OnTutorialSolved;
    }

    void OnHavenSolved(object sender,System.EventArgs e) {
        Debug.Log("Haven is solved");
        endDistrict.OpenGate();
        havenPuzzle.IsSolved -= OnHavenSolved;
    }

    void OnTriggerEnter(Collider other) {
        //Go to End Screen;
    }

}
