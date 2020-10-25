using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCheckScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hit;
    private void OnTriggerStay2D (Collider2D collision)
    {
        hit = true;
    }
    private void OnTriggerExit2D (Collider2D collision)
    {
        hit = false;
    }
}
