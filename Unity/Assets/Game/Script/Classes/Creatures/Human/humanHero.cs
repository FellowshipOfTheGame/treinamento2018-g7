using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHero : Creature {
   public static GameObject prefab;

   const string _name = "Sir Godfrey(Hero)";
   const string _teamName = "Humans";
   const int _maxHealth = 1000;
   const int _maxActionPoints = 5;
   const int _baseDodge = 15;
   const int _defenseHeal = 75;
   const int _defenseResistance = 30;
   const int _attackDamage = 100;
   const int _attackRange = 1;

   const int _areaHealingAmmount = 15;
   const int _areaHealingRange = 1;

   public HumanHero(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {

   }

   public override bool defend(){
      if(!base.defend())
         return false;
      Surroundings surroundings = terrain.expandByDistance(_areaHealingRange);
      for(int aux = 1; aux < surroundings.creatures.Count; aux ++){
         surroundings.creatures[aux].second.changeHealth(_areaHealingAmmount);
      }
      return true;
   }
}
