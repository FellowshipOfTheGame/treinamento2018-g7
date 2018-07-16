using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trio<U, V, W> {
   public U first;
   public V second;
   public W third;
   public Trio(U u, V v, W w) {
      first = u;
      second = v;
      third = w;
   }
}
