using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class TurretDemo2 : MonoBehaviour
{
    public GameObject[] turrets;

    public virtual void Update(){
        foreach(GameObject turret in (this.turrets as GameObject[]))
            turret.SendMessage("Target", this.transform.position);
    }

}