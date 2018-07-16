using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
   const int squareHeight = 40;
   const int squareWidth = 64;

   public GameObject plainsPrefab;
   public GameObject forestPrefab;
   public GameObject mountainPrefab;
   public GameObject riverPrefab;
   public GameObject fortressPrefab;

   public GameObject humanSoldierPrefab;
   public GameObject humanArcherPrefab;
   public GameObject humanKnightPrefab;
   public GameObject humanSiegePrefab;
   public GameObject humanHeroPrefab;
   public GameObject humanArcherTrapPrefab;
   public GameObject humanWoodenFortressPrefab;

   public GameObject undeadSoldierPrefab;
   public GameObject undeadArcherPrefab;
   public GameObject undeadKnightPrefab;
   public GameObject undeadSiegePrefab;
   public GameObject undeadHeroPrefab;

   public TextAsset terrainFile;

   void Generate(string[] tokens) {
      int terrainHeight = int.Parse(tokens[0]);
      int terrainWidth = int.Parse(tokens[1]);
      Terrain[][] allTiles;
      allTiles = new Terrain[terrainHeight][];

      Terrain.allTiles = allTiles;
      Terrain.terrainHeight = terrainHeight;
      Terrain.terrainWidth = terrainWidth;

      for (int height = 0; height < terrainHeight; height++) {
         allTiles[height] = new Terrain[terrainWidth];
         for (int width = 0; width < terrainWidth; width++) {
            int tokenPos = height * terrainWidth + width + 2;
            if (tokens[tokenPos] == "p") // planice
               allTiles[height][width] = new Plains(width, height);
            else if (tokens[tokenPos] == "f") // floresta
               allTiles[height][width] = new Forest(width, height);
            else if (tokens[tokenPos] == "m") // montanha, impassavel
               allTiles[height][width] = new Mountain(width, height);
            else if (tokens[tokenPos] == "r") // rio
               allTiles[height][width] = new River(width, height);
            else if (tokens[tokenPos] == "t") // fortaleza
               allTiles[height][width] = new Fortress(width, height);
            else Debug.Log(tokens[tokenPos]);
         }
      }
      for (int aux = terrainHeight * terrainWidth + 2; aux < tokens.Length; aux += 5) {
         int y = int.Parse(tokens[aux + 3]);
         int x = int.Parse(tokens[aux + 4]);
         int team = int.Parse(tokens[aux + 2]);
         int race = int.Parse(tokens[aux + 1]);

         if (tokens[aux] == "s") { // escudeiro
            if (race == 0) new HumanSoldier(x, y, team);
            else new UndeadSoldier(x, y, team);
         }
         else if (tokens[aux] == "a") { // archer
            if (race == 0) new HumanArcher(x, y, team);
            else new UndeadArcher(x, y, team);
         }
         else if (tokens[aux] == "t") {// siege
            if (race == 0) new HumanSiege(x, y, team);
            else new UndeadSiege(x, y, team);
         }
         else if (tokens[aux] == "k") { // knight
            if (race == 0) new HumanKnight(x, y, team);
            else new UndeadKnight(x, y, team);
         }
         else if (tokens[aux] == "h") { // hero
            if (race == 0) new HumanHero(x, y, team);
            else new UndeadHero(x, y, team);
         }
      }
   }

   void Start() {
      //setup everyone's prefab
      Plains.prefab = plainsPrefab;
      Mountain.prefab = mountainPrefab;
      Forest.prefab = forestPrefab;
      Fortress.prefab = fortressPrefab;
      River.prefab = riverPrefab;

      HumanArcher.prefab = humanArcherPrefab;
      HumanSoldier.prefab = humanSoldierPrefab;
      HumanHero.prefab = humanHeroPrefab;
      HumanSiege.prefab = humanSiegePrefab;
      HumanKnight.prefab = humanKnightPrefab;
      
      UndeadArcher.prefab = undeadArcherPrefab;
      UndeadSoldier.prefab = undeadSoldierPrefab;
      UndeadHero.prefab = undeadHeroPrefab;
      UndeadSiege.prefab = undeadSiegePrefab;
      UndeadKnight.prefab = undeadKnightPrefab;

      HumanArcherTrap.prefab = humanArcherTrapPrefab;
      HumanWoodenFortress.prefab = humanWoodenFortressPrefab;

      //parse input terrain text
      string terrainString = terrainFile.text;
      string[] terrainTokens;
      terrainString = terrainString.Replace(" ", string.Empty);
      terrainString = terrainString.Replace("\n", string.Empty);
      terrainString = terrainString.Replace("\r", string.Empty);
      terrainTokens = terrainString.Split(',');

      //generate terrain
      Generate(terrainTokens);
   }
}