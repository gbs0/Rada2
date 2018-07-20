using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{

    public float speed;

    public float duration;
    float timeElapsed;

    public bool reflectOnShieldHit;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, speed / 100);

        if (duration > 0)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > duration)
                Destroy(this.gameObject);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            switch (hit.transform.tag)
            {
                case "Player":
                    hit.transform.SendMessageUpwards("morte");
                    Destroy(this.gameObject);
                    break;
                case "BridgeSwitch":
                    hit.transform.SendMessageUpwards("executeRotation", 90f);
                    Destroy(this.gameObject);
                    break;
                case "EndSwitch":
                    hit.transform.SendMessageUpwards("openDoor", 90f);
                    Destroy(this.gameObject);
                    break;
                case "Shield":
                    if (reflectOnShieldHit == true) {
                        Reflect(hit.transform.forward);
                    } else {
                        Destroy(this.gameObject);
                    }
                    break;
                default:
                    Destroy(this.gameObject);
                    break;
            }

        }
    }

    void Reflect(Vector3 direction)
    {
        timeElapsed = 0;
        transform.LookAt(transform.position + direction);
    }
}
