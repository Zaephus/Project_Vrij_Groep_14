using UnityEngine;

public class District : MonoBehaviour
{
    public enum Status { Open, Closed }
    public Status currentStatus;

    public enum Curse { Lifted, Cursed}
    public Curse curseStatus;

    public Puzzle districtPuzzle;
    public Gate districtGate;

    public District[] nextDistrict;

    public void Update()
    {
        switch (currentStatus)
        {
            case Status.Closed:
                break;
            case Status.Open:
                break;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenGate();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CloseGate();
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

    public void OpenGate()
    {
        districtGate.Open();
    }

    public void CloseGate()
    {
        districtGate.Close();
    }
}

