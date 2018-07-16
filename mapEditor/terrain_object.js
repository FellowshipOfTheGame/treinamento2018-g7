class Terrain{
   constructor(width, height){
      this.allTiles = [];
      this.width = width;
      this.height = height;
      this.makeTerrain();
   }

   updateTerrain(w, h){
      this.width = w;
      this.height = h;
      this.makeTerrain();
   }

   makeTerrain(){
      this.allTiles = [];
      for(let height = 0; height < this.height; height ++){
         this.allTiles.push([]);
         for(let width = 0; width < this.width; width ++){
            let pos = new Vector2(height%2 == 0? width : width + 0.5, -height * 0.9);
            pos.scaleRef(HEXAGON_SIZE * 1.9);
            this.allTiles[height].push(new Hexagon(pos));
         }
      }
   }

   drawByCamera(camera){
      for(let height = 0; height < this.allTiles.length; height ++){
         for(let width = 0; width < this.allTiles[height].length; width ++){
            this.allTiles[height][width].drawByCamera(camera);
         }
      }
   }

   click(position, selector){
      let y = Math.floor(
         -(position.y - HEXAGON_SIZE) /
         (HEXAGON_SIZE * 1.9 * 0.9)
      );
      let x = Math.floor(
         (position.x + HEXAGON_SIZE * 1.73205080757 / 2)/
         (HEXAGON_SIZE * 1.9)
      );

      for(let nx = x - 1; nx <= x + 1; nx ++){
         for(let ny = y - 1; ny <= y + 1; ny ++){
            if(nx >= 0 && nx < this.width && ny >= 0 && ny < this.height){
               if(this.allTiles[ny][nx].isPointInsideMe(position)){
                  if(selector.type == "terrain"){
                     this.allTiles[ny][nx].setType(selector.terrain);
                  }
                  else if(selector.type == "creature"){
                     if(selector.creature != "clear")
                        this.allTiles[ny][nx].creature = new Creature(
                           Creature.type2Color(selector.race, selector.creature),
                           selector.race,
                           selector.creature,
                           selector.team
                        );
                     else
                        this.allTiles[ny][nx].creature = null;
                  }
               }
            }
         }
      }
   }

   generateFile(){
      let fileString = this.height + ", " + this.width + ",\n\n";
      let creatureString = "";

      for(let h = 0; h < this.height; h ++){
         for(let w = 0; w < this.width; w ++){
            if(this.allTiles[h][w].creature != null){
               let creature = this.allTiles[h][w].creature;
               creatureString += "\n";
               creatureString += Creature.type2Letter(creature.type);
               creatureString += ", ";
               creatureString += Creature.race2Number(creature.race);
               creatureString += ", " + creature.team + ", ";
               creatureString += h;
               creatureString += ", ";
               creatureString += w;
               creatureString += ",";
            }
            fileString += Terrain.type2Letter(this.allTiles[h][w].type);
            if(h < this.height-1 || w < this.width-1 || creatureString.length != 0)
               fileString += ", ";
         }
         fileString += "\n";
      }
      fileString += creatureString.substring(0, creatureString.length - 1);

      let file = new Blob([fileString]);
      let fileURL = URL.createObjectURL(file);

      let a = document.createElement("a");
      a.download = "map";
      a.href = fileURL;
      a.click();
      URL.revokeObjectURL(fileURL);
   }

   parseFile(fileString){
      fileString = fileString.replace(/\n/g, "");
      fileString = fileString.replace(/ /g, "");
      let tokens = fileString.split(",");
      this.updateTerrain(parseInt(tokens[1]), parseInt(tokens[0]));
      tokens.shift();
      tokens.shift();
      for(let h = 0; h < this.height; h ++){
         for(let w = 0; w < this.width; w ++){
            this.allTiles[h][w].setType(
               Terrain.letter2Type(tokens[h * this.width + w])
            );
         }
      }
      for(let aux = this.height * this.width; aux < tokens.length; aux += 5){
         let type = Creature.letter2Type(tokens[aux + 0]);
         let race = Creature.number2Race(parseInt(tokens[aux + 1]));
         let team = parseInt(tokens[aux + 2]);
         let y = parseInt(tokens[aux + 3]);
         let x = parseInt(tokens[aux + 4]);
         this.allTiles[y][x].creature = new Creature(Creature.type2Color(race, type), race, type, team);
      }
   }

   static letter2Type(letter){
      if(letter == "p") return "plains";
      else if(letter == "f") return "forest";
      else if(letter == "r") return "river";
      else if(letter == "t") return "fortress";
      else if(letter == "m") return "mountain";
      else return false;
   }

   static type2Color(type){
      if(type == "plains") return PLAINS_COLOR;
      else if(type == "forest") return FOREST_COLOR;
      else if(type == "river") return RIVER_COLOR;
      else if(type == "fortress") return FORTRESS_COLOR;
      else if(type == "mountain") return MOUNTAIN_COLOR;
      else return false;
   }

   static type2Letter(type){
      if(type == "plains") return "p";
      else if(type == "forest") return "f";
      else if(type == "river") return "r";
      else if(type == "fortress") return "t";
      else if(type == "mountain") return "m";
      else return false;
   }
}
