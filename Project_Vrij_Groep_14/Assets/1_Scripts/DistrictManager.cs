using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    public List<District> districts;
    public GameObject[] glassPanels;
    
    // Start is called before the first frame update
    void Start()
    {
        districts.AddRange(FindObjectsOfType<District>());              //zoekt alle districts en voegt toe aan lijst
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    for(int i = 0; i<districts.Count; i++)
    //    {
    //        districts[i].OnUpdate();                                    //update alle districts
    //    }
    //}


    //functie om uit te roepen wanneer glas in lood gefixt
    void LiftCurse(District district)
    {
        if (district.districtPuzzle.puzzleStatus == Puzzle.Status.Solved)
        {
            district.curseStatus = District.Curse.Lifted;
            district.CheckCurse();
            PlayCutscene(district);

            foreach(District next in district.nextDistrict)
            {
                next.currentStatus = District.Status.Open;
            }
        }

        return;
    }

    void PlayCutscene(District target)
    {

    }
}
