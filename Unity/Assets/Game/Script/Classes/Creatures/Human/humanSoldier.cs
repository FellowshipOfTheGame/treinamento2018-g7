using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSoldier : Creature {
   public static GameObject prefab;

   const string _name = "Soldier";
   const string _teamName = "Humans";
   const int _maxHealth = 400;
   const int _maxActionPoints = 3;
   const int _baseDodge = 10;
   const int _defenseHeal = 40;
   const int _defenseResistance = 30;
   const int _attackDamage = 40;
   const int _attackRange = 1;

   const int _raiseShieldsMaxCooldown = 3;
   const int _raiseShieldsAPCost = 3;

   private bool areShieldsRaised = false;
   private int raiseShieldsCooldown = 0;

   public HumanSoldier(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {

   }

   public override void newTeamTurn(){
      if(areShieldsRaised){
         defenseHeal /= 2;
         defenseResistance /= 3;
      }
      if(raiseShieldsCooldown > 0) raiseShieldsCooldown--;
      base.newTeamTurn();
   }

   public bool raiseShields(){
      if(raiseShieldsCooldown > 0){
         Debug.Log("wait " + raiseShieldsCooldown + " turns");
         return false;
      }
      if(!useActionPoints(_raiseShieldsAPCost)){
         Debug.Log("Not enough action points");
         return false;
      }
      Debug.Log("raising shields, also automatically defending");
      areShieldsRaised = true;
      defenseHeal *= 2;
      defenseResistance *= 3;
      raiseShieldsCooldown = _raiseShieldsMaxCooldown;
      defend();
      return true;
   }
}
