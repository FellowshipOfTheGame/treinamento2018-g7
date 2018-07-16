using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellControl : MonoBehaviour {

   public Terrain cell;
   void Start () {
	}
	
	void Update () {
   }
   void OnMouseDown() {
      cell.mouseDown();
   }
   void OnMouseUp() {
      cell.mouseUp();
   }
}
