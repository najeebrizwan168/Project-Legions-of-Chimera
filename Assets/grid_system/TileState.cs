using UnityEngine;

public class TileState : MonoBehaviour
{
    public bool isUsed = false;
    public GameObject occupyingUnit;

    public bool HasPlayerUnit()
    {
        return isUsed && occupyingUnit != null;
    }
}
