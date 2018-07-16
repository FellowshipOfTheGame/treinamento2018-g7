using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : Terrain {
   const int _walkSpeed = 3;
   public static GameObject prefab;
   public River(int x, int y) : base(prefab, x, y, _walkSpeed) {
   }
}