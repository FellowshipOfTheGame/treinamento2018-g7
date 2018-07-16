function rgb(r, g, b){
   return "#"+r.toString(16) + g.toString(16) + b.toString(16);
}

const HEXAGON_SIZE = 20;
const PLAINS_COLOR = rgb(107, 130, 67);
const FOREST_COLOR = rgb(63, 84, 33);
const RIVER_COLOR = rgb(83, 147,144);
const FORTRESS_COLOR = rgb(23, 39, 59);
const MOUNTAIN_COLOR = rgb(152, 163, 147);

class Hexagon{
   constructor(position){
      this.position = new Vector2(position);
      this.points = [];
      this.generatePoints();
      this.creature = null;
      this.type = "plains";
      this.color = PLAINS_COLOR;
   }

   setType(type){
      this.type = type;
      this.color = Terrain.type2Color(type);
   }

   generatePoints(){
      let point = (new Vector2(HEXAGON_SIZE, 0)).rotateRef(30);
      for(let aux = 0; aux < 6; aux ++){
         point.rotateRef(60);
         this.points.push(point.duplicate());
      }
   }

   isPointInsideMe(point){
      let dist = point.subtract(this.position);
      if(dist.magnitude() < HEXAGON_SIZE * HEXAGON_SIZE * 3 / 4)
         return true;
      return false;
   }

   drawByCamera(camera){
      let mousePos = MouseInterface.position;
      let pos = this.position.add(camera.position).scale(camera.zoom).add(new Vector2(canvasWidth/2, canvasHeight/2));
      let point = this.points[this.points.length-1].scale(camera.zoom);

      ctx.beginPath();
      ctx.fillStyle = this.color;
      ctx.moveTo(pos.x + point.x, pos.y + point.y);
      for(let aux = 0; aux < this.points.length; aux ++){
         point = this.points[aux].scale(camera.zoom);
         ctx.lineTo(point.x + pos.x, point.y + pos.y);
      }
      ctx.fill();
      if(this.creature != null) this.creature.draw(pos, camera.zoom * (HEXAGON_SIZE - 5));
   }
}
