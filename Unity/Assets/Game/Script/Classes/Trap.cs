using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap{
   public int x, y;
   public int team;
   public int lifeSpam;
   public int activationRange;
   public GameObject spriteInstance;
   public Terrain terrain;
   public Surroundings surroundings;

   public static List<Trap> allTraps = new List<Trap>();

   public Trap(GameObject prefab, int x, int y, int team, int lifeSpam = 15, int activationRange = 0){
      this.x = x;
      this.y = y;
      this.team = team;
      this.lifeSpam = lifeSpam;
      this.activationRange = activationRange;

      terrain = Terrain.allTiles[y][x];
      terrain.trap = this;
      spriteInstance = MonoBehaviour.Instantiate(prefab);
      spriteInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      spriteInstance.transform.position = terrain.spriteInstance.transform.position + new Vector3(0, 0, -0.0001f);
      surroundings = terrain.expandByDistance(activationRange);
      allTraps.Add(this);
   }

   public static List<Trap> TrapsInRange(int x, int y){
      List<Trap> traps = new List<Trap>();
      foreach(Trap trap in allTraps){
         if(trap.willActivate(x, y))
            traps.Add(trap);
      }
      return traps;
   }

   public virtual void dieFromAge(){
      Debug.Log("Trap dead");
      terrain.trap = null;
      allTraps[allTraps.IndexOf(this)] = null;
   }

   public virtual void dieFromActivation(){
      terrain.trap = null;
      allTraps.Remove(this);
   }

   public virtual bool willActivate(int x, int y){
      Trio<int, int, Terrain> t;
      if(surroundings.tryGetTerrain(x, y, out t) && t.third.creature.team != team)
         return true;
      return false;
   }

   public virtual void activate(Creature creture){
      Debug.Log("Trap: Gotcha!");
      dieFromActivation();
   }

   public virtual void newTeamTurn(){

   }

   public virtual void newTurn(int turn){
      this.lifeSpam --;
      if(lifeSpam == 0) dieFromAge();
      if(turn == team) newTeamTurn();
   }
}