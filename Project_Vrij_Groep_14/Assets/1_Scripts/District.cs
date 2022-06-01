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

    //event van maken??
    public void BreakCurse()
    {
        curseStatus = Curse.Lifted;
        //layer aanpassen ofzo?? even afwachten wat er volgens GDES moet gebeuren
    }

    public void OpenGate()
    {
        currentStatus = Status.Open;
        districtGate.Open();
    }

    public void CloseGate()
    {
        currentStatus = Status.Closed;
        districtGate.Close();
    }
}

