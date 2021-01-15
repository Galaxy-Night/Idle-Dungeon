using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnMouseDown is called when the user clicks on the object
    private void OnMouseDown()
	{
        SendMessageUpwards("UnlockMessage");
	}
}
