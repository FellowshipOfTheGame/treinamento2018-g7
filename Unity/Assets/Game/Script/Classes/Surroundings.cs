using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------NOTE-----------------------------------------
//this is an auxiliar class, with no real impact on the game
//it is used to help open around the terrains, it contains the 
//terrains dictionaries and the creatures list, along with some
//handling functions
//-------------------------------------------------------------

public class Surroundings {
   public Dictionary<string, Trio<int, int, Terrain>> terrains;
   public List<Pair<int, Creature>> creatures;

   //initializes stuff
   public Surroundings() {
      terrains = new Dictionary<string, Trio<int, int, Terrain>>();
      creatures = new List<Pair<int, Creature>>();
   }

   //transforms the coords into a string for the dictionary hashing
   static private string stringifyCoords(int x, int y) {
      return x + ", " + y;
   }

   public int findCreatureIndex(Creature creature) {
      for(int aux = 0; aux < creatures.Count; aux ++){
         if (creatures[aux].second == creature) return aux;
      }
      return -1;
   }

   public bool hasCreature(Creature creature){
      return findCreatureIndex(creature) != -1;
   }

   //tries to get the terrain from the given x and y coords
   public bool tryGetTerrain(int x, int y, out Trio<int, int, Terrain> t) {
      return terrains.TryGetValue(stringifyCoords(x, y), out t);
   }

   public bool hasTerrain(int x, int y){
      var trio = new Trio<int, int, Terrain>(0, 0, null);
      return tryGetTerrain(x, y, out trio);
   }

   public bool addOrUpdateTerrainForSmallerValues(Terrain t, int x, int y, int dist, int ap) {
      Trio<int, int, Terrain> p;
      if (tryGetTerrain(x, y, out p)) { //if terrain was already inserted
         int creatureIndex = 0;
         bool flag = false;
         if (p.third.creature != null)
            creatureIndex = findCreatureIndex(t.creature);

         if (dist < p.first) { //update its distance if found a smaller one
            p.first = dist;
            creatures[creatureIndex].first = dist;
            flag = true;
         }
         if (ap < p.second) { //update its ap if found a smaller one
            p.second = ap;
            flag = true;
         }
         return flag;
      }
      else { //if terrain was not inserted
         addTerrain(t, x, y, dist, ap);
         return true;
      }
   }

   //add terrain and its creature, if its there
   public void addTerrain(Terrain t, int x, int y, int dist, int ap) {
      terrains.Add(x + ", " + y, new Trio<int, int, Terrain>(dist, ap, t));
      if (t.creature != null && findCreatureIndex(t.creature) == -1)
         this.addCreature(dist, t.creature);
   }

   public void addCreature(int dist, Creature creature) {
      creatures.Add(new Pair<int, Creature>(dist, creature));
   }

   //removes terrain
   public void removeTerrain(int x, int y) {
      terrains.Remove(stringifyCoords(x, y));
   }

   public void clear() {
      foreach (var item in terrains) {
         item.Value.third.setColor(Color.white);
      }
   }

   public void paint(Color color) {
      foreach (var item in terrains) {
         item.Value.third.setColor(color);
      }
      /*
      for (int aux = 1; aux < creatures.Count; aux ++) {
         var item = creatures[aux];
         int x = item.second.x, y = item.second.y;
         paintRecursive(x, y, item.second.attackRange);
      }*/
   }

   /*private void paintRecursive(int x, int y, int dist) {
      Pair<int, Terrain> p;
      if (tryGetTerrain(x, y, out p) == false || dist < 0) return;
      Terrain.allTiles[y][x].setColor(Color.green);
      foreach (var item in Terrain.allTiles[y][x].getNeighboursCoords()) {
         paintRecursive(item.first, item.second, dist - 1);
      }
   }*/
}
