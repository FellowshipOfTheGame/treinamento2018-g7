using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSiege : Creature {
   public static GameObject prefab;

   const string _name = " Siege";
   const string _teamName = "Undeads";
   const int _maxHealth = 500;
   const int _maxActionPoints = 2;
   const int _baseDodge = 0;
   const int _defenseHeal = 50;
   const int _defenseResistance = 30;
   const int _attackDamage = 20;
   const int _attackRange = 1;

   public UndeadSiege(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {

   }
}
