using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSiege : Creature {
   public static GameObject prefab;

   const string _name = " Siege";
   const string _teamName = "Humans";
   const int _maxHealth = 300;
   const int _maxActionPoints = 3;
   const int _baseDodge = 0;
   const int _defenseHeal = 35;
   const int _defenseResistance = 10;
   const int _attackDamage = 70;
   const int _attackRange = 3;

   const int _invertMaxCD = 4;
   const int _pushMaxCD = 6;
   const int _pushAPCost = 2;
   const int _splashDamage = 50;
   const int _mountAPCost = 2;
   const int _pushAmmount = 3;
   const int _pushRange = 1;

   private int pushCD = 0;
   private bool isMounted = false;
   private int invertCD = 0;
   private bool isInverted = false;

   public HumanSiege(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {
   }

   public override void attack(Creature victim) {
      if (!isMounted) {
         Debug.Log("Must mount to attack");
         return;
      }
      if (isInverted) {
         attackDamage = _splashDamage;
         base.attack(victim);
         attackDamage = _attackDamage;
      }
      else {
         base.attack(victim);
         attackDamage = _splashDamage;
      }

      List<Pair<int, int>> neighbours = victim.terrain.getNeighboursCoords();
      for (int aux = 0; aux < neighbours.Count; aux++) {
         var coord = neighbours[aux];
         var creature = Terrain.allTiles[coord.second][coord.first].creature;

         if (creature != null && creature.team != this.team)
            base.attack(creature);
      }
      this.attackDamage = _attackDamage;
   }

   public void toggleMount() {
      if (!useActionPoints(_mountAPCost)) {
         Debug.Log("Not enough action points");
         return;
      }
      isMounted = !isMounted;
      if (isMounted)
         Debug.Log("Mounted");
      else
         Debug.Log("Unmounted");
   }

   public override void newTeamTurn() {
      base.newTeamTurn();
      if (invertCD > 0) invertCD--;
      if (pushCD > 0) pushCD--;
   }

   public bool habilityInvert() {
      if (invertCD > 0) {
         Debug.Log("wait " + invertCD + " turns to use this hability");
         return false;
      }
      invertCD = _invertMaxCD;
      Debug.Log("inverted damage");
      isInverted = true;
      return true;
   }

   private void habilityPushAux(Creature creature) {
      Terrain creatureTerrain = creature.terrain;
      Pair<int, int> pair = terrain.getDirectionToTerrain(creatureTerrain);
      for(int aux = 0; aux < _pushAmmount; aux ++){
         Terrain neighbour = creatureTerrain.getNeighbourFromDirection(pair.first, pair.second);
         if(neighbour == null || neighbour.creature != null || neighbour is Mountain)
            break;
         creatureTerrain = neighbour;
         if(creatureTerrain.trap != null) break;
      }
      creature.move(creatureTerrain.x, creatureTerrain.y, 0);
   }

   public bool habilityPush() {
      if (pushCD > 0) {
         Debug.Log("wait " + pushCD + " turns to use this hability");
         return false;
      }
      if (!useActionPoints(_pushAPCost)){
         Debug.Log("not enough action points");
         return false;
      }
      pushCD = _pushMaxCD;
      Debug.Log("Pushed!");

      Surroundings surroundings = terrain.expandByDistance(_pushRange);
      for(int aux = 1; aux < surroundings.creatures.Count; aux ++){
         habilityPushAux(surroundings.creatures[aux].second);
      }
      return true;
   }

   public override void move(int x, int y, int distance) {
      if (!isMounted)
         base.move(x, y, distance);
      else
         Debug.Log("Cant move while mounted");
   }

   public override Surroundings mouseDown() {
      Surroundings surroundings;
      int ap = actionPoints;
      if (isMounted) {
         if (actionPoints > 0) {
            actionPoints = attackRange;
         }
      }
      surroundings = base.mouseDown();
      actionPoints = ap;
      return surroundings;
   }
}