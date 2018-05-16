using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour {

    public GameObject borderPrefab;
    public float switchDistance;
    public Transform helpTransform;
    public Transform player;
    public Transform radarCamera;
    GameObject[] trackedObjects;
    List<GameObject> borderObjects;

	void Start () {
        CreateBorderObjects();
	}

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            radarCamera.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }

        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (trackedObjects[i] == null)
            {
                if (borderObjects[i] != null)
                {
                    Destroy(borderObjects[i]);
                }

                continue;
            }

            var curr2DPos = new Vector2(trackedObjects[i].transform.position.x, trackedObjects[i].transform.position.z);
            var obj2DPos = new Vector2(transform.position.x, transform.position.z);
            if (Vector2.Distance(curr2DPos, obj2DPos) > switchDistance)
            {
                var trackedPos = new Vector3(trackedObjects[i].transform.position.x, helpTransform.position.y, trackedObjects[i].transform.position.z);
                helpTransform.LookAt(trackedPos);
                var newBorderObjectPos = switchDistance * helpTransform.forward;
                newBorderObjectPos.y = helpTransform.localPosition.y;
                borderObjects[i].transform.localPosition = newBorderObjectPos;
                borderObjects[i].layer = LayerMask.NameToLayer("Radar");
            }
            else
            {
                borderObjects[i].layer = LayerMask.NameToLayer("Invisible");
            }
        }
    }

    void CreateBorderObjects()
    {
        borderObjects = new List<GameObject>();
        trackedObjects = GameObject.FindGameObjectsWithTag("radar");
        foreach (GameObject o in trackedObjects)
        {
            GameObject j = Instantiate(borderPrefab, transform);
            borderObjects.Add(j);
        }
    }
}
