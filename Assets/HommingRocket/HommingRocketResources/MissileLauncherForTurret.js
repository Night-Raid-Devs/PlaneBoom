#pragma strict
var missile: GameObject;
private var time = 0.1;
var timeFromDestroy = 5;
var Delay = 10;
private var rocketStarted = false;
private var clone;
function Update() {
    time = time + Time.deltaTime;

    if (time > Delay && !rocketStarted) {
       clone = Instantiate(missile, transform.position, transform.rotation);
        rocketStarted = true;
    }

    if (time > timeFromDestroy + Delay) {
        Destroy(clone, 1.0);
        time = 0.0;
        rocketStarted = false;
    }
}