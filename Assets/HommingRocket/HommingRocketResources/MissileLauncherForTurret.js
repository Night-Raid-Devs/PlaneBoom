#pragma strict
import System.Collections.Generic;

private var rocketList = new List.<GameObject>(); // Insert TYPE
var missile: GameObject;
private var timeToFire = 0.1f;
private var timeToDestroyRecket = 0.1f;
var timeToDestroy = 5.0f;
var Delay = 10.0f;

function Update() {
    timeToFire += Time.deltaTime;
    timeToDestroyRecket += Time.deltaTime;

    if (timeToFire > Delay) {

        rocketList.Add(Instantiate(missile, transform.position, transform.rotation));
        timeToFire = 0.0;
    }

    if (timeToDestroyRecket > timeToDestroy) {
        if (rocketList.Count > 0) {
            for (var rocket: GameObject in rocketList) {
                Destroy(rocket, 1.0);
            }
        }
        timeToDestroyRecket = 0.0;
    }
}