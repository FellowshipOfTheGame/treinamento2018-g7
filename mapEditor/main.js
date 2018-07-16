let canClickTerrain = false;

function receiveFile(e){
   log("file dropped");
   e.preventDefault();
   let file = e.dataTransfer.items[0].getAsFile();
   let reader = new FileReader();
   reader.readAsText(file);
   reader.onloadend = ()=>{
      terrain.parseFile(reader.result);
   }
}

function dragOverHandler(e){
   e.preventDefault();
}

function updateTerrainSize(){
   let width = parseInt(widthInput.value);
   let height = parseInt(heightInput.value);
   terrain.updateTerrain(width, height)
}

function frame(){
   ctx.clearRect(0, 0, canvasWidth, canvasHeight);
   let pos = MouseInterface.position;
   if(canClickTerrain){
      terrain.click(camera.screen2World(pos), selector);
   }
   camera.drawTerrain(terrain);
   terrainsMenu.draw();
   creaturesMenu.draw();
   selector.viewer.draw();
}

let terrain = new Terrain(64, 40);
let camera = new Camera([canvasWidth/2, canvasHeight/2]);

MouseInterface.addMouseMoveBehaviour((pos, delta, isDown)=>{
   if(isDown[2]){
      camera.move(delta.scale(1/camera.zoom));
   }
});

MouseInterface.addMouseDownBehaviour((pos)=>{
   if(!terrainsMenu.click(pos.x, pos.y) && !creaturesMenu.click(pos.x, pos.y))
      canClickTerrain = true;
});

MouseInterface.addMouseUpBehaviour((pos)=>{
   canClickTerrain = false;
});

MouseInterface.addMouseWheelBehaviour((delta, pos)=>{
   if(delta > 0) camera.changeZoom(-0.05 * camera.zoom);
   else camera.changeZoom(0.05 * camera.zoom, pos);
});

setInterval(frame, 0);
