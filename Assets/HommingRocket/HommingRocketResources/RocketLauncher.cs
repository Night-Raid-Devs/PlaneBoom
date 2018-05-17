using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject missile;

    public GameObject[] rockets;

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < rockets.Length; i++)
            {
                if (rockets[i].activeSelf)
                {
                    var rot = transform.eulerAngles;
                    var pos = rockets[i].transform.position + transform.forward * 20;
                    Instantiate(missile, pos, Quaternion.Euler(rot));
                    rockets[i].SetActive(false);
                    break;
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    RechargeRockets();
        //}
    }

    public void RechargeRockets()
    {
        for (int i = 0; i < rockets.Length; i++)
        {
            if (!rockets[i].activeSelf)
            {
                rockets[i].SetActive(true);
            }
        }
    }
}
