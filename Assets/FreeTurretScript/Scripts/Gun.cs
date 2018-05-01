using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Gun : MonoBehaviour
{
    public Camera targeter;

    public GameObject bullet;

    public int weaponRange = 10;

    private RaycastHit hit;

    private Vector3 target;
    private float counter = 0;

    public virtual void Update(){
        counter += Time.deltaTime;
        if (counter > 1)
        {
            UnityEngine.Object.Instantiate(this.bullet, this.transform.position, this.transform.rotation);
            counter = 0;
        }  
    }
}