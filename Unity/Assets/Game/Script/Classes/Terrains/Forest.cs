using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Terrain {
   const int _walkSpeed = 2;
   public static GameObject prefab;

   public int dodgeBonus;
   public Forest(int x, int y) : base(prefab, x, y, _walkSpeed) {
   }
}