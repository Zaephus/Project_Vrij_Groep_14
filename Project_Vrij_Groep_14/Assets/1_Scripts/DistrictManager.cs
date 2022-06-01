using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    public List<District> districts;
    public GameObject[] glassPanels;

    public District currentDistrict;

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

    }
}
