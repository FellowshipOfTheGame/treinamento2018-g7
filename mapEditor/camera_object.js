class Camera{
   constructor(pos, zoom = 1){
      this.position = new Vector2(pos);
      this.zoom = zoom;
   }

   move(offset){
      this.position.addRef(offset);
   }

   changeZoom(ammount, mouseCenter = new Vector2(canvasWidth/2, canvasHeight/2)){
      this.zoom += ammount;
      if(this.zoom < 0.01) this.zoom = 0.01;
   }

   drawTerrain(terrain){
      terrain.drawByCamera(this);
   }

   drawHexagon(hexagon){
      hexagon.drawByCamera(this);
   }

   screen2World(coords){
      return coords.subtract(new Vector2(canvasWidth/2, canvasHeight/2)).scale(1/this.zoom).subtractRef(this.position);
   }
}
