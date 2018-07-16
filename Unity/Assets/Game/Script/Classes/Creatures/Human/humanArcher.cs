using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArcher : Creature {
   public static GameObject prefab;

   const string _name = "Archer";
   const string _teamName = "Humans";
   const int _maxHealth = 300;
   const int _maxActionPoints = 3;
   const int _baseDodge = 10;
   const int _defenseHeal = 30;
   const int _defenseResistance = 20;
   const int _attackDamage = 60;
   const int _attackRange = 2;

   const int _trapMaxCooldown = 6;
   const int _trapAPCost = 1;

   private bool movedLastTurn = true;
   private bool movedThisTurn = false;
   private int trapCooldown = 0;
   private List<Creature> enemiesInTheBorder;

   public HumanArcher(int x, int y, int team) : base(prefab, x, y, _maxActionPoints, team, _maxHealth, _attackDamage, _attackRange, _defenseHeal, _defenseResistance, _baseDodge, _name, _teamName) {
      enemiesInTheBorder = new List<Creature>();
   }

   public override void newTeamTurn() {
      base.newTeamTurn();
      movedLastTurn = movedThisTurn;
      movedThisTurn = false;
      if (!movedLastTurn) this.attackRange = _attackRange + 1;
      if(trapCooldown > 0) trapCooldown --;
      else this.attackRange = _attackRange;
   }

   public override void move(int x, int y, int distance) {
      base.move(x, y, distance);
      movedThisTurn = true;
   }

   public override Surroundings mouseDown() {
      Surroundings surroundings = base.mouseDown();

      if (movedLastTurn) return surroundings;

      foreach (var item in surroundings.creatures) {
         if (item.first == _attackRange + 1)
            enemiesInTheBorder.Add(item.second);
      }
      return surroundings;
   }

   public override void attack(Creature victim) {
      if (!movedLastTurn && enemiesInTheBorder.Contains(victim)) {
         attackDamage /= 2;
         base.attack(victim);
         attackDamage = _attackDamage;
      }
      else {
         base.attack(victim);
      }
   }

   public Surroundings previewTrap(){
      if(trapCooldown > 0){
         Debug.Log("Wait more " + trapCooldown + " turns");
         return null;
      }
      if(!useActionPoints(_trapAPCost)){
         Debug.Log("Not enough action points");
         return null;
      }
      Debug.Log("Previewing trap");
      return terrain.expandByDistance(terrain.movePointsRequired);
   }

   public void setTrap(int x, int y){
      Debug.Log("Setting trap");
      new HumanArcherTrap(x, y, team);
      actionPoints --;
      trapCooldown = _trapMaxCooldown;
   }
}
