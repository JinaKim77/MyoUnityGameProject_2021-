using UnityEngine;

public class RadialBeltController : MonoBehaviour, IFireable
{

    public Transform radialBeltMount;
    public RadialBelt _equippedBelt;

    public void OnTriggerHold()
    {
        if (_equippedBelt != null) _equippedBelt.OnTriggerHold();
    }
    public void OnTriggerRelease()
    {
        if (_equippedBelt != null) _equippedBelt.OnTriggerRelease();
    }

}
