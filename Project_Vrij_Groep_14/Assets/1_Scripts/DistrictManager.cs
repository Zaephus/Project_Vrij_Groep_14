using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    //public List<District> districts;
    public District harbor;

    public District currentDistrict;

    public Tutorial tutorialPuzzle;

    [Header("Audio")]
    [SerializeField] AudioManager audioManager;

    public void Start() {
        tutorialPuzzle.IsSolved += OnTutorialSolved;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LiftCurse(currentDistrict);
        }
    }

    //functie om uit te roepen wanneer glas in lood gefixt
    void LiftCurse(District district)
    {
            district.curseStatus = District.Curse.Lifted;
            PlayCutscene(district);

            foreach(District next in district.nextDistrict)
            {
                next.OpenGate();
                currentDistrict = next;
            }
        //if (district.districtPuzzle.puzzleStatus == Puzzle.Status.Solved)
        //{
        //}

        return;
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
}
