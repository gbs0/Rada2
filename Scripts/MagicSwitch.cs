using UnityEngine;
using System.Collections;

public class MagicSwitch : MonoBehaviour {

    public RotatingBridge bridge;
    public EndDoor door;
    public int switchNumber;

    void executeRotation(float incrementalDegree) {
        bridge.executeRotation(incrementalDegree);
    }

    void openDoor() {
        door.open(switchNumber);
        Destroy(this.gameObject);
    }

}