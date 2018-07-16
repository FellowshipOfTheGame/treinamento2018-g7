using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextTurnButtonControll : MonoBehaviour {

   public GameObject player1;
   public GameObject player2;

   private CanvasRenderer renderer1;
   private CanvasRenderer renderer2;

   public void click() {
      TurnController.nextTurn();
      if (TurnController.turn == 0) {
         renderer1.SetAlpha(1);
         renderer2.SetAlpha(0);
      }
      else {
         renderer1.SetAlpha(0);
         renderer2.SetAlpha(1);
      }
   }

	void Start () {
      renderer1 = player1.GetComponent<CanvasRenderer>();
      renderer2 = player2.GetComponent<CanvasRenderer>();
      renderer2.SetAlpha(0);
   }
	
	// Update is called once per frame
	void Update () {
		
	}
}
