class UIItem{
   constructor(position, size, color = "#020202"){
      this.relativePosition = new Vector2(position);
      this.relativeSize = new Vector2(size);
      this.position = new Vector2(0, 0);
      this.size = new Vector2(0, 0);
      this.color = color;
      this.children = [];
      this.clickFunctions = [];
      this.fit();
   }

   fit(offset = new Vector2(0, 0), size = new Vector2(canvasWidth, canvasHeight)){
      this.position.x = this.relativePosition.x * size.x/100;
      this.position.y = this.relativePosition.y * size.y/100;
      this.position.addRef(offset);
      this.size.x = this.relativeSize.x * size.x/100;
      this.size.y = this.relativeSize.y * size.y/100;
   }

   addClickFunction(func){
      this.clickFunctions.push(func);
   }

   addChild(child, callback){
      child.fit(this.position, this.size);
      this.children.push(child);
      if(callback != undefined)
         callback(child);
      return child;
   }

   click(x, y){
      let pos = this.position;
      let size = this.size;
      if(x < pos.x || x > pos.x + size.x || y < pos.y || y > pos.y + size.y)
         return false;

      for(let aux = 0; aux < this.children.length; aux ++){
         this.children[aux].click(x, y);
      }

      for(let aux = 0; aux < this.clickFunctions.length; aux ++)
         this.clickFunctions[aux]();
      return true;
   }
}

class UIBox extends UIItem{
   constructor(position, size, color = "#020202"){
      super(position, size, color);
   }

   draw(offset){
      offset = new Vector2(offset);
      let pos = offset.addRef(this.position);

      ctx.beginPath();
      ctx.fillStyle = this.color;
      ctx.moveTo(pos.x, pos.y);
      ctx.lineTo(pos.x + this.size.x, pos.y);
      ctx.lineTo(pos.x + this.size.x, pos.y + this.size.y);
      ctx.lineTo(pos.x, pos.y + this.size.y);
      ctx.lineTo(pos.x, pos.y);
      ctx.fill();
      for(var aux = 0; aux < this.children.length; aux ++){
         this.children[aux].draw();
      }
   }
}

class Text extends UIItem{
   constructor(message, position, size, color, args = {}){
      super(position, size, color);
      this.message = message;
      this.wrappedMessage = [message];
      this.isFit = false;

      if(args.wrap === undefined)
         this.wrap = true;
      else
         this.wrap = args.wrap;

      if(args.align === undefined)
         this.align = "left";
      else
         this.align = args.align;

      if(args.baseLine === undefined)
         this.baseLine = "top";
      else
         this.baseLine = args.baseLine;
   }

   wrap(){
      let letterWidth = this.fontSize * 0.6;
      let letterHeight = this.fontSize;
      let maxLettersPerLine = Math.floor(this.size.x / letterWidth);
      let message = this.message;
      let linesCount = 1;
      this.wrappedMessage = [];
      while(message.length > 0 && linesCount * letterHeight <= this.size.y){
         let lettersToBeCut = maxLettersPerLine < message.length? maxLettersPerLine : message.length;
         this.wrappedMessage.push(message.slice(0, lettersToBeCut));
         message = message.slice(lettersToBeCut, message.length);
         linesCount ++;
      }
   }

   doesItFit(size){
      let width = Math.floor(this.size.x / (size * 0.6));
      if(width == 0) return false;
      let height = Math.ceil(this.message.length / width) * size;
      if(height > this.size.y) return false;
      return true;
   }

   fitToBox(){
      let max = 9999;
      let min = 1;
      let middle;
      while(max > min + 1){
         middle = Math.floor((max + min) / 2);
         if(this.doesItFit(middle))
            min = middle;
         else
            max = middle - 1;
      }
      while(!this.doesItFit(middle))
         middle--;
      this.fontSize = middle;
      this.isFit = true;
      this.wrap();
   }

   fitToLine(){
      let widthMax = Math.floor(this.size.x / (this.message.length * 0.6));
      let heightMax = Math.floor(this.size.y);
      this.fontSize = widthMax > heightMax? heightMax : widthMax;
      this.isFit = true;
   }

   draw(){
      if(!this.isFit){
         if(this.wrap) this.fitToBox();
         else this.fitToLine();
      }
      ctx.font = this.fontSize + "px Courier New";
      ctx.fillStyle = this.color;
      ctx.textAlign = this.align;
      ctx.textBaseline = this.baseLine;
      for(let aux = 0; aux < this.wrappedMessage.length; aux ++){
         ctx.fillText(this.wrappedMessage[aux], this.position.x, this.position.y + aux * this.fontSize);
      }
   }
}
