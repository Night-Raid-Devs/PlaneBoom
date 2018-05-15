using UnityEngine;

public class LauncherOnRocket : MonoBehaviour
{
    public GameObject missile;

    private static int MissleNumber = 1;
    private static int LastNumber = MissleNumber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (name == "missile" + MissleNumber)
            {
                Destroy(gameObject);
                var pos = transform.position;
                pos.y -= 2;
                var rot = transform.eulerAngles;
                transform.eulerAngles = new Vector3(rot.x, rot.y + 90, rot.z);
                Instantiate(missile, pos, transform.rotation);
                LastNumber = MissleNumber;
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            MissleNumber = LastNumber + 1;
        }
    }
}
