using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

   float scrollAmmount = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      if (Input.GetMouseButton(1)) {

         float dx = Input.GetAxisRaw("Mouse X");
         float dy = Input.GetAxisRaw("Mouse Y");
         Vector3 deltaCamera = new Vector3(-dx, -dy, 0) * -scrollAmmount / 5;
         transform.position += deltaCamera;
      }
      scrollAmmount += Input.mouseScrollDelta.y;
      if (scrollAmmount > -0.5f) scrollAmmount = -0.5f;
      float x = transform.position.x;
      float y = transform.position.y;
      transform.position = new Vector3(x, y, scrollAmmount);
   }
}
