const HUMAN_SOLDIER_COLOR = "#ff0000";
const HUMAN_ARCHER_COLOR = "#00ff00";
const HUMAN_KNIGHT_COLOR = "#0000ff";
const HUMAN_SIEGE_COLOR = "#ffff00";
const HUMAN_HERO_COLOR = "#00ffff";

const UNDEAD_SOLDIER_COLOR = "#990000";
const UNDEAD_ARCHER_COLOR = "#009900";
const UNDEAD_KNIGHT_COLOR = "#000099";
const UNDEAD_SIEGE_COLOR = "#999900";
const UNDEAD_HERO_COLOR = "#009999";

class Creature{
   constructor(color, race, type, team){
      this.color = color;
      this.team = team;
      this.race = race;
      this.type = type;
   }

   draw(position, radius){
      ctx.beginPath();
      ctx.fillStyle = this.color;
      ctx.arc(position.x, position.y, radius * 0.9, 0, Math.PI * 2);
      ctx.fill();
      ctx.fillStyle = "#000000";
      ctx.font = radius + "px Courier News";
      ctx.textAlign = "center";
      ctx.textBaseline = "middle";
      ctx.fillText(this.team, position.x, position.y);
   }

   static type2Color(race, type){
      if(race == "human"){
         if(type == "soldier") return HUMAN_SOLDIER_COLOR;
         else if(type == "knight") return HUMAN_KNIGHT_COLOR;
         else if(type == "archer") return HUMAN_ARCHER_COLOR;
         else if(type == "siege") return HUMAN_SIEGE_COLOR;
         else if(type == "hero") return HUMAN_HERO_COLOR;
         else return false;
      }
      else if(race == "undead"){
         if(type == "soldier") return UNDEAD_SOLDIER_COLOR;
         else if(type == "knight") return UNDEAD_KNIGHT_COLOR;
         else if(type == "archer") return UNDEAD_ARCHER_COLOR;
         else if(type == "siege") return UNDEAD_SIEGE_COLOR;
         else if(type == "hero") return UNDEAD_HERO_COLOR;
         else return false;
      }
      else if(type == "clear") return "#ffffff";
      else return false;
   }

   static letter2Type(letter){
      if(letter == "s") return "soldier";
      else if(letter == "a") return "archer";
      else if(letter == "k") return "knight";
      else if(letter == "t") return "siege";
      else if(letter == "h") return "hero";
      else return false;
   }

   static race2Number(race){
      if(race == "human") return 0;
      else if(race == "undead") return 1;
      else return false;
   }

   static number2Race(race){
      if(race == 0) return "human";
      else if(race == 1) return "undead";
      else return false;
   }

   static type2Letter(type){
      if(type == "soldier") return "s";
      else if(type == "archer") return "a";
      else if(type == "knight") return "k";
      else if(type == "siege") return "t";
      else if(type == "hero") return "h";
      else return false;
   }
}
