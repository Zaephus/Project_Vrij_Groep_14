using UnityEngine;

public class District : MonoBehaviour
{
    public enum Status { Open, Closed }
    public Status currentStatus;

    public enum Curse { Lifted, Cursed}
    public Curse curseStatus;

    public Puzzle districtPuzzle;
    public GameObject districtDoor;

    public District[] nextDistrict;

    public void OnUpdate()
    {
        switch (currentStatus)
        {
            case Status.Closed:

                //deur dicht
                break;
            case Status.Open:

                //deur open
                break;
        }
    }


    //event van maken??
    public void CheckCurse()
    {
        if (curseStatus == Curse.Lifted)
        {
            //layer aanpassen ofzo?? even afwachten wat er volgens GDES moet gebeuren
        }
    }
}

