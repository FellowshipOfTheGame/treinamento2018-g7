using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanKnight : Creature {
   public static GameObject prefab;

   const string _name = "Knight";
   const string _teamName = "Humans";
   const int _maxHealth = 250;
   const int _maxActionPoints = 5;
   const int _baseDodge = 10;
   const int _defenseHeal = 20;
   const int _defenseResistance = 20;
   const int _attackDamage = 70;
   const int _attackRange = 1;

   const int assaultMaxCD = 5;
   private bool isAssaulting = false;
   private int assaultCD = 0;
   private bool hasTerrainSpeedup = false;

   public HumanKnight(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {
      if (terrain is Plains) hasTerrainSpeedup = true;
   }

   public override void attack(Creature victim) {
      if (isAssaulting) {
         isAssaulting = false;
         attackDamage += 70;
         base.attack(victim);
         attackDamage -= 70;
      }
      else {
         int ap = this.actionPoints - 1;
         base.attack(victim);
         this.actionPoints = ap;
      }
   }

   public override void newTeamTurn() {
      base.newTeamTurn();
      assaultCD--;
      if (assaultCD < 0) assaultCD = 0;
   }

   public override void newTurn(int turnNumber) {
      base.newTurn(turnNumber);
      isAssaulting = false;
      if (terrain is Plains) {
         hasTerrainSpeedup = true;
      }
      else
         hasTerrainSpeedup = false;
   }

   public override void move(int x, int y, int distance) {
      if (hasTerrainSpeedup) {
         hasTerrainSpeedup = false;
         actionPoints++;
      }
      base.move(x, y, distance);
   }

   public override Surroundings mouseDown() {
      Surroundings surroundings;
      if (hasTerrainSpeedup) {
         this.actionPoints++;
         surroundings = base.mouseDown();
         this.actionPoints--;
      }
      else {
         surroundings = base.mouseDown();
      }
      return surroundings;
   }

   public void assault() {
      if (assaultCD > 0) {
         Debug.Log("wait " + assaultCD + " turns to use this hability");
         return;
      }
      if (isAssaulting) return;
      assaultCD = assaultMaxCD;
      Debug.Log("knight entered assault mode");
      isAssaulting = true;
   }
}