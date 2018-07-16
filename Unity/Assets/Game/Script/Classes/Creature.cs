using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature {
   public string name;
   public string teamName;
   public int health;
   public int maxHealth;
   public int attackDamage;
   public int maxActionPoints;
   public int attackRange;
   public int actionPoints;
   public int defenseHeal;
   public int defenseResistance;
   public int dodge;

   public int snareDuration = 0;

   public bool hasAttacked = false;
   public bool isDefending = false;
   public bool wasDefenseHealApplied = false;

   public int x, y;
   public int team;
   public Terrain terrain;
   protected GameObject spriteInstance;

   public static List<Creature> allCreatures = new List<Creature>();

   public Creature(GameObject prefab, int x, int y, int maxActionPoints, int team, int maxHealth = 10, int attackDamage = 1, int attackRange = 1, int defenseHeal = 10, int defenseResistance = 10, int dodge = 10, string name = null, string teamName = null) {
      this.x = x;
      this.y = y;
      this.team = team;
      this.maxActionPoints = maxActionPoints;
      this.attackRange = attackRange;
      this.maxHealth = maxHealth;
      this.health = maxHealth;
      this.attackDamage = attackDamage;
      this.defenseHeal = defenseHeal;
      this.defenseResistance = defenseResistance;
      this.dodge = dodge;
      this.name = name;
      this.teamName = teamName;

      this.terrain = Terrain.allTiles[y][x];
      this.terrain.creature = this;
      this.spriteInstance = MonoBehaviour.Instantiate(prefab);
      this.spriteInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      this.spriteInstance.transform.position = this.terrain.spriteInstance.transform.position + new Vector3(0, 0, -0.0001f);

      if(terrain is Fortress)
         this.defenseResistance += (terrain as Fortress).defenseBonus;

      else if(terrain is Forest)
         this.dodge += (terrain as Forest).dodgeBonus;

      this.actionPoints = maxActionPoints;
      allCreatures.Add(this);
   }

   public virtual bool defend() {
      if(hasAttacked){
         Debug.Log("Cant defend in a turn that attacked");
         return false;
      }
      if(isDefending){
         Debug.Log("im already defending");
         return false;
      }
      Debug.Log("defending");
      isDefending = true;
      wasDefenseHealApplied = false;
      return true;
   }

   public virtual void changeHealth(int ammount) {
      health += ammount;
      if (health > maxHealth) health = maxHealth;
      if (health <= 0) die();
   }

   public virtual void die() {
      terrain.creature = null;
      allCreatures.Remove(this);
      MonoBehaviour.Destroy(spriteInstance);
   }

   public virtual void newTeamTurn() {
      this.actionPoints = this.maxActionPoints;
      if(snareDuration > 0){
         actionPoints = 0;
         snareDuration--;
      }
      hasAttacked = false;
      isDefending = false;
   }

   public virtual void newTurn(int turnNumber) {
      if (turnNumber == this.team) newTeamTurn();
      if (isDefending && !wasDefenseHealApplied) {
         changeHealth(defenseHeal);
         Debug.Log("Healing " + defenseHeal + ". health is now " + health);
         wasDefenseHealApplied = true;
      }
   }

   public virtual void receiveAttack(Creature attacker){
      int damage = attacker.attackDamage;
      int myDefense = defenseResistance;

      if(Random.Range(0, 100) <= dodge){
         Debug.Log("But the attack was dodged!");
         return;
      }

      if((attacker.terrain is Fortress && terrain is Fortress) || attacker is HumanSiege || attacker is UndeadSiege){
         Debug.Log("fortress bonus denied");
         myDefense -= (terrain as Fortress).defenseBonus;
      }

      if(myDefense < 0) myDefense = 0;
      damage = (int)(damage * (1.0f - myDefense/100.0f));
      if(damage < 0) damage = 0;
      changeHealth(-damage);
      Debug.Log(damage + " went through the total of " + myDefense + " defense. " + health + " Hp left");
   }

   public virtual void attack(Creature victim) {
      if(isDefending){
         Debug.Log("i was defending, but since i attacked, im cancelling my defense");
         isDefending = false;
      }
      hasAttacked = true;
      Debug.Log("dealt " + attackDamage + " damage");
      victim.receiveAttack(this);
      this.actionPoints = 0;
   }

   public void setPos(int x, int y) {
      Terrain lastTerrain = terrain;
      this.x = x;
      this.y = y;
      this.terrain.creature = null;
      this.terrain = Terrain.allTiles[y][x];
      this.terrain.creature = this;
      spriteInstance.transform.position = this.terrain.spriteInstance.transform.position + new Vector3(0, 0, -0.0001f);
      if(!(lastTerrain is Fortress) && terrain is Fortress){
         Debug.Log("Stepping in a fortress");
         this.defenseResistance += (terrain as Fortress).defenseBonus;
      }
      else if(lastTerrain is Fortress && !(terrain is Fortress)){
         Debug.Log("Stepping out of fortress");
         defenseResistance -= (lastTerrain as Fortress).defenseBonus;
      }
      if(!(lastTerrain is Forest) && terrain is Forest){
         Debug.Log("Stepping in forest");
         dodge += (terrain as Forest).dodgeBonus;
      }
      else if(lastTerrain is Forest && !(terrain is Forest)){
         Debug.Log("Stepping out of forest");
         dodge -= (lastTerrain as Forest).dodgeBonus;
      }
   }

   public bool useActionPoints(int ammount) {
      if (actionPoints < ammount) return false;
      actionPoints -= ammount;
      return true;
   }

   public virtual void move(int x, int y, int distance) {
      setPos(x, y);
      if(!useActionPoints(distance)){
         Debug.Log("not enough points for walking");
      }
      List<Trap> trapsInRange = Trap.TrapsInRange(x, y);
      foreach(Trap trap in trapsInRange){
         trap.activate(this);
      }
   }

   public virtual Surroundings mouseDown() {
      Surroundings surroundings = terrain.expandByAP(this.actionPoints);
      surroundings.paint(Color.red);
      return surroundings;
   }

   public virtual void mouseUp(Surroundings surroundings) {
      surroundings.clear();
   }
}
