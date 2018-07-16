document.addEventListener('contextmenu', event => event.preventDefault());
class MouseInterface{
   static mouseDown(e){
      let x = e.clientX - canvasOffsetLeft;
      let y = e.clientY - canvasOffsetTop;
      MouseInterface.buttonsPressed[e.button] = true;
      MouseInterface.lastPosition = MouseInterface.position;
      MouseInterface.position = new Vector2(x, y);
      MouseInterface.deltaPosition =
         MouseInterface.position.subtract(
         MouseInterface.lastPosition);
      for(let aux = 0;aux < MouseInterface.mouseDownFunctions.length; aux ++){
         if(MouseInterface.mouseDownFunctions[aux][1] == e.button){
            MouseInterface.mouseDownFunctions[aux][0](
               MouseInterface.position,
               MouseInterface.deltaPosition,
               MouseInterface.buttonsPressed
            );
         }
      }
   }

   static mouseMove(e){
      let x = e.clientX - canvasOffsetLeft;
      let y = e.clientY - canvasOffsetTop;
      MouseInterface.lastPosition = MouseInterface.position;
      MouseInterface.position = new Vector2(x, y);
      MouseInterface.deltaPosition = MouseInterface.position.subtract(
         MouseInterface.lastPosition
      );
      for(let aux = 0;aux < MouseInterface.mouseMoveFunctions.length; aux ++){
         MouseInterface.mouseMoveFunctions[aux](
            MouseInterface.position,
            MouseInterface.deltaPosition,
            MouseInterface.buttonsPressed
         );
      }
   }

   static mouseUp(e){
      let x = e.clientX - canvasOffsetLeft;
      let y = e.clientY - canvasOffsetTop;
      MouseInterface.buttonsPressed[e.button] = false;
      MouseInterface.lastPosition = MouseInterface.position;
      MouseInterface.position = new Vector2(x, y);
      MouseInterface.deltaPosition =
         MouseInterface.position.subtract(
         MouseInterface.lastPosition);
      for(let aux = 0;aux < MouseInterface.mouseUpFunctions.length; aux ++){
         if(MouseInterface.mouseUpFunctions[aux][1] == e.button){
            MouseInterface.mouseUpFunctions[aux][0](
               MouseInterface.position,
               MouseInterface.deltaPosition,
               MouseInterface.buttonsPressed
            );
         }
      }
   }

   static mouseWheel(e){
      for(let aux = 0;aux < MouseInterface.mouseWheelFunctions.length; aux ++){
         MouseInterface.mouseWheelFunctions[aux](
            e.deltaY,
            MouseInterface.position,
            MouseInterface.deltaPosition,
            MouseInterface.buttonsPressed
         );
      }
   }
   
   static addMouseDownBehaviour(func, button = 0){
      MouseInterface.mouseDownFunctions.push([func, button]);
   }

   static addMouseUpBehaviour(func, button = 0){
      MouseInterface.mouseUpFunctions.push([func, button]);
   }

   static addMouseMoveBehaviour(func){
      MouseInterface.mouseMoveFunctions.push(func);
   }

   static addMouseWheelBehaviour(func){
      MouseInterface.mouseWheelFunctions.push(func);
   }
}

MouseInterface.position = new Vector2(0, 0);
MouseInterface.lastPosition = new Vector2(0, 0);
MouseInterface.deltaPosition = new Vector2(0, 0);
MouseInterface.deltaWheel = 0;
MouseInterface.buttonsPressed = 
   [false, false, false, false, false, false, false, false, false, false];

MouseInterface.mouseDownFunctions = [];
MouseInterface.mouseUpFunctions = [];
MouseInterface.mouseMoveFunctions = [];
MouseInterface.mouseWheelFunctions = [];

function mouseMoveInitializer(e){
   let x = e.clientX - canvasOffsetLeft;
   let y = e.clientY - canvasOffsetTop;
   MouseInterface.position = new Vector2(x, y);
   MouseInterface.lastPosition = MouseInterface.position.duplicate();
   MouseInterface.mouseMove(e);
   body.removeEventListener("mousemove", mouseMoveInitializer);
   body.addEventListener("mousemove", MouseInterface.mouseMove);
   body.removeEventListener("mousedown", mouseDownInitializer);
   body.addEventListener("mousedown", MouseInterface.mouseDown);
   body.removeEventListener("mouseup", mouseUpInitializer);
   body.addEventListener("mouseup", MouseInterface.mouseUp);
   body.removeEventListener("wheel", mouseWheelInitializer);
   body.addEventListener("wheel", MouseInterface.mouseWheel);
}

function mouseDownInitializer(e){
   let x = e.clientX - canvasOffsetLeft;
   let y = e.clientY - canvasOffsetTop;
   MouseInterface.position = new Vector2(x, y);
   MouseInterface.lastPosition = MouseInterface.position.duplicate();
   MouseInterface.mouseDown(e);
   body.removeEventListener("mousemove", mouseMoveInitializer);
   body.addEventListener("mousemove", MouseInterface.mouseMove);
   body.removeEventListener("mousedown", mouseDownInitializer);
   body.addEventListener("mousedown", MouseInterface.mouseDown);
   body.removeEventListener("mouseup", mouseUpInitializer);
   body.addEventListener("mouseup", MouseInterface.mouseUp);
   body.removeEventListener("wheel", mouseWheelInitializer);
   body.addEventListener("wheel", MouseInterface.mouseWheel);
}

function mouseUpInitializer(e){
   let x = e.clientX - canvasOffsetLeft;
   let y = e.clientY - canvasOffsetTop;
   MouseInterface.position = new Vector2(x, y);
   MouseInterface.lastPosition = MouseInterface.position.duplicate();
   MouseInterface.mouseUp(e);
   body.removeEventListener("mousemove", mouseMoveInitializer);
   body.addEventListener("mousemove", MouseInterface.mouseMove);
   body.removeEventListener("mousedown", mouseDownInitializer);
   body.addEventListener("mousedown", MouseInterface.mouseDown);
   body.removeEventListener("mouseup", mouseUpInitializer);
   body.addEventListener("mouseup", MouseInterface.mouseUp);
   body.removeEventListener("wheel", mouseWheelInitializer);
   body.addEventListener("wheel", MouseInterface.mouseWheel);
}

function mouseWheelInitializer(e){
   let x = e.clientX - canvasOffsetLeft;
   let y = e.clientY - canvasOffsetTop;
   MouseInterface.position = new Vector2(x, y);
   MouseInterface.lastPosition = MouseInterface.position.duplicate();
   MouseInterface.mouseWheel(e);
   body.removeEventListener("mousemove", mouseMoveInitializer);
   body.addEventListener("mousemove", MouseInterface.mouseMove);
   body.removeEventListener("mousedown", mouseDownInitializer);
   body.addEventListener("mousedown", MouseInterface.mouseDown);
   body.removeEventListener("mouseup", mouseUpInitializer);
   body.addEventListener("mouseup", MouseInterface.mouseUp);
   body.removeEventListener("wheel", mouseWheelInitializer);
   body.addEventListener("wheel", MouseInterface.mouseWheel);
}

let body = document.getElementsByTagName("body")[0];
body.addEventListener("mousemove", mouseMoveInitializer);
body.addEventListener("mousedown", mouseDownInitializer);
body.addEventListener("mouseup", mouseUpInitializer);
body.addEventListener("wheel", mouseUpInitializer);
