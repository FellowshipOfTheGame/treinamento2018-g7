using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plains : Terrain{
   const int _walkSpeed = 1;
   public static GameObject prefab;
   public Plains(int x, int y) : base(prefab, x, y, _walkSpeed) {
   }
}