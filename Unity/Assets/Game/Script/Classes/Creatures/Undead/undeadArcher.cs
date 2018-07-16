using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadArcher : Creature {
   public static GameObject prefab;

   const string _name = "Archer";
   const string _teamName = "Undeads";
   const int _maxHealth = 300;
   const int _maxActionPoints = 3;
   const int _baseDodge = 15;
   const int _defenseHeal = 30;
   const int _defenseResistance = 20;
   const int _attackDamage = 30;
   const int _attackRange = 2;

   public UndeadArcher(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {

   }
}
