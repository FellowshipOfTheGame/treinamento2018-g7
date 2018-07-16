using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair <V, U>{
   public V first;
   public U second;

   public Pair(V first, U second) {
      this.first = first;
      this.second = second;
   }
}
