using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {
	private bool switch1;
    private bool switch2;
    private bool switch3;
    private bool switch4;

    void Update () {
		if (switch1 && switch2 && switch3 && switch4) {
            Destroy(this.gameObject);
        }
	}

    public void open(int switchNumber) {
        if (switchNumber == 1) {
            switch1 = true;
        } else if (switchNumber == 2) {
            switch2 = true;
        } else if (switchNumber == 3) {
            switch3 = true;
        } else if (switchNumber == 4) {
            switch4 = true;
        }
    }

}
