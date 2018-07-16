using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain {
   Renderer renderer;
   public GameObject spriteInstance;
   public Terrain[] neighbours = new Terrain[6];
   public int neighboursCount = 0;

   public int movePointsRequired;
   public Creature creature = null;
   public Trap trap = null;
   public int x, y;

   public static Terrain[][] allTiles;
   public static int terrainHeight;
   public static int terrainWidth;

   public Terrain(GameObject prefab, int x, int y, int walkSpeed = 1) {
      this.x = x;
      this.y = y;
      spriteInstance = MonoBehaviour.Instantiate(prefab);
      spriteInstance.transform.position = new Vector3(x + (y % 2 == 0 ? 0 : 0.5f), y * 0.9f, 0) * 0.9f;
      renderer = spriteInstance.GetComponent<Renderer>();
      this.spriteInstance.GetComponent<cellControl>().cell = this;
      this.movePointsRequired = walkSpeed;

      //setting up neighbours
      if (y > 0) {
         this.addMutualNeighbour(allTiles[y - 1][x]);
         if (y % 2 == 0) {
            if (x > 0) this.addMutualNeighbour(allTiles[y - 1][x - 1]);
         }
         else {
            if (x < terrainWidth - 1) this.addMutualNeighbour(allTiles[y - 1][x + 1]);
         }
      }
      if (x > 0) this.addMutualNeighbour(allTiles[y][x - 1]);
   }

   public static bool areCoordsValid(int x, int y){
      if(x < 0 || y < 0 || x >= terrainWidth || y >= terrainHeight)
         return false;
      return true;
   }

   public bool isShifted() {
      return y % 2 == 1;
   }

   public List<Pair<int, int>> getNeighboursCoords() {
      List<Pair<int, int>> list = new List<Pair<int, int>>();
      if (x > 0) list.Add(new Pair<int, int>(x + 1, y));
      if (x < terrainWidth - 1) list.Add(new Pair<int, int>(x - 1, y));
      if (y > 0) list.Add(new Pair<int, int>(x, y - 1));
      if (y < terrainHeight - 1) list.Add(new Pair<int, int>(x, y + 1));

      if (isShifted()) {
         if (x < terrainWidth - 1) {
            if (y > 0) list.Add(new Pair<int, int>(x + 1, y - 1));
            if (y < terrainHeight) list.Add(new Pair<int, int>(x + 1, y + 1));
         }
      }
      else {
         if (x > 0) {
            if (y > 0) list.Add(new Pair<int, int>(x - 1, y - 1));
            if (y < terrainHeight) list.Add(new Pair<int, int>(x - 1, y + 1));
         }
      }
      return list;
   }

   public Terrain getTopLeftNeighbour(){
      int x = this.x;
      int y = this.y + 1;

      if(!isShifted()) x --;
      if(!areCoordsValid(x, y)) return null;
      
      return Terrain.allTiles[y][x];
   }
   
   public Terrain getTopRightNeighbour(){
      int x = this.x;
      int y = this.y + 1;

      if(isShifted()) x ++;
      if(!areCoordsValid(x, y)) return null;
      
      return Terrain.allTiles[y][x];
   }
   
   public Terrain getBottomRightNeighbour(){
      int x = this.x;
      int y = this.y - 1;

      if(isShifted()) x ++;
      if(!areCoordsValid(x, y)) return null;
      
      return Terrain.allTiles[y][x];
   }
   
   public Terrain getBottomLeftNeighbour(){
      int x = this.x;
      int y = this.y - 1;

      if(!isShifted()) x --;
      if(!areCoordsValid(x, y)) return null;
      
      return Terrain.allTiles[y][x];
   }
   
   public Terrain getLeftNeighbour(){
      int x = this.x - 1;
      int y = this.y;
      if(!areCoordsValid(x, y)) return null;
      return Terrain.allTiles[y][x];
   }
   
   public Terrain getRightNeighbour(){
      int x = this.x + 1;
      int y = this.y;
      if(!areCoordsValid(x, y)) return null;
      return Terrain.allTiles[y][x];
   }

   public bool isTerrainMyNeighbour(Terrain terrain){
      if(terrain.y - y > 1 || terrain.y - y < -1) return false;
      if(terrain.x - x > 1 || terrain.x - x < -1) return false;

      if(terrain.y != y){
         if(isShifted() && terrain.x < x) return false;
         if(!isShifted() && terrain.x > x) return false;
      }
      else{
         if(terrain.x == x) return false;
      }
      return true;
   }

   public Pair<int, int> getDirectionToTerrain(Terrain terrain){
      int dx = 0;
      int dy = 0;
      if(!isTerrainMyNeighbour(terrain)) return null;
      if(terrain.y != y){
         if(isShifted()){
            if(terrain.x == x) dx = -1;
            else dx = 1;
         }
         else{
            if(terrain.x == x) dx = 1;
            else dx = -1;
         }
         if(terrain.y > y) dy = 1;
         else dy = -1;
      }
      else{
         if(terrain.x > x) dx = 1;
         else dx = -1;
      }

      return new Pair<int, int>(dx, dy);
   }

   public Terrain getNeighbourFromDirection(int x, int y){
      if(x == 0) return null;
      if(x > 0 && y == 0) return getRightNeighbour();
      else if(x < 0 && y == 0) return getLeftNeighbour();
      else if(x > 0 && y > 0) return getTopRightNeighbour();
      else if(x < 0 && y > 0) return getTopLeftNeighbour();
      else if(x > 0 && y < 0) return getBottomRightNeighbour();
      else if(x < 0 && y < 0) return getBottomLeftNeighbour();
      else return null;
   }

   public void mouseDown() {
      GameController.clickTerrain(this);
   }

   public void mouseUp() {
      // if (this.creature != null)
      //this.creature.mouseUp();
   }

   private void expandRecursiveByAP(int dist, int AP, int maxAP, Surroundings surroundings) {
      if (AP > maxAP) return;
      if (!surroundings.addOrUpdateTerrainForSmallerValues(this, x, y, dist, AP))
         return;
      for (int aux = 0; aux < this.neighboursCount; aux++) {
         if (this.neighbours[aux].movePointsRequired != -1) {
            neighbours[aux].expandRecursiveByAP(
               dist + 1, AP + movePointsRequired, maxAP, surroundings
            );
         }
      }
   }

   private void expandRecursiveByDistance(int dist, int AP, int maxDist, Surroundings surroundings) {
      if (dist > maxDist) return;
      if (!surroundings.addOrUpdateTerrainForSmallerValues(this, x, y, dist, AP))
         return;
      for (int aux = 0; aux < this.neighboursCount; aux++) {
         if (this.neighbours[aux].movePointsRequired != -1) {
            neighbours[aux].expandRecursiveByDistance(
               dist + 1, AP + movePointsRequired, maxDist, surroundings
            );
         }
      }
   }

   public Surroundings expandByAP(int maxAp) {
      Surroundings surroundings = new Surroundings();
      this.expandRecursiveByAP(0, 0, maxAp, surroundings);
      return surroundings;
   }
   
   public Surroundings expandByDistance(int maxDist) {
      Surroundings surroundings = new Surroundings();
      this.expandRecursiveByDistance(0, 0, maxDist, surroundings);
      return surroundings;
   }

   private void pushNeighbour(Terrain neighbour) {
      this.neighbours[this.neighboursCount] = neighbour;
      this.neighboursCount++;
   }

   //adds the neighbour to its neighbours list, and adds itself
   //to the neighbour's neighbours list
   private void addMutualNeighbour(Terrain neighbour) {
      this.pushNeighbour(neighbour);
      neighbour.pushNeighbour(this);
   }

   public void setColor(Color color) {
      renderer.material.color = color;
   }

   //sets all neighbours red;
   public void setNeighboursRed() {
      for (int aux = 0; aux < this.neighboursCount; aux++) {
         this.neighbours[aux].setColor(Color.red);
      }
   }
}