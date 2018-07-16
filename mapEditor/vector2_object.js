class Vector2{
   constructor(x, y){
      if(x === undefined){
         this.x = 0;
         this.y = 0;
      }
      else if(x.constructor == Array){
         this.x = x[0];
         this.y = x[1];
      }
      else if(x.constructor == Vector2){
         this.x = x.x;
         this.y = x.y;
      }
      else{
         this.x = x;
         this.y = y;
      }
   }

   draw(offset){
      offset = new Vector2(offset);

      let arrow1 = new Vector2(this.x * 0.866025 - this.y * 0.5, this.x * 0.5 + this.y * 0.866025);
      let arrow2 = new Vector2(this.x * 0.866025 + this.y * 0.5,-this.x * 0.5 + this.y * 0.866025);

      arrow1.normalizeRef().scaleRef(20);
      arrow2.normalizeRef().scaleRef(20);

      ctx.beginPath();
      ctx.moveTo(offset.x, offset.y);
      ctx.lineTo(this.x + offset.x           , this.y + offset.y           );
      ctx.lineTo(this.x + offset.x - arrow1.x, this.y + offset.y - arrow1.y);
      ctx.moveTo(this.x + offset.x           , this.y + offset.y           );
      ctx.lineTo(this.x + offset.x - arrow2.x, this.y + offset.y - arrow2.y);
      ctx.stroke();
   }

   toString(){
      return "(" + this.x + ", " + this.y + ")";
   }

   duplicate(){
      return new Vector2(this.x, this.y);
   }

   copy(vec){
      this.x = vec.x;
      this.y = vec.y;
      return this;
   }

   add(vec){
      return new Vector2(this.x + vec.x, this.y + vec.y);
   }

   subtract(vec){
      return new Vector2(this.x - vec.x, this.y - vec.y);
   }

   scale(factor){
      return new Vector2(this.x*factor, this.y*factor);
   }

   magnitude(){
      return this.x*this.x+this.y*this.y;
   }

   sqrtMagnitude(){
      return Math.sqrt(this.magnitude());
   }

   normalize(){
      let mag = this.sqrtMagnitude();
      if(mag == 0) return new Vector2(0, 0);
      return new Vector2(this.x / mag, this.y / mag);
   }

   dotProduct(vec){
      return this.x * vec.x + this.y * vec.y;
   }

   crossProductZValue(vec){
      return this.x * vec.y - this.y * vec.x
   }

   projectionMag(vec){
      let mag = vec.sqrtMagnitude();
      if(mag == 0) return 0;
      return this.dotProduct(vec)/mag;
   }

   projection(vec){
      let mag = vec.magnitude();
      if(mag == 0) return this.duplicate();
      return vec.scale(this.dotProduct(vec) / mag);
   }

   rotate(angle){
      angle *= Math.PI/180;

      let cos = Math.cos(angle);
      let sin = Math.sin(angle);

      return new Vector2(this.x * cos - this.y * sin, this.x * sin + this.y * cos);
   }

   transform(matrix){
      return new Vector2(
         this.x * matrix.val[0][0] + this.y * matrix.val[0][1],
         this.x * matrix.val[1][0] + this.y * matrix.val[1][1]
      );
   }

   //--------------------------------------------------------------------------

   addRef(vec){
      this.x += vec.x;
      this.y += vec.y;
      return this;
   }

   subtractRef(vec){
      this.x -= vec.x;
      this.y -= vec.y;
      return this;
   }

   scaleRef(factor){
      this.x *= factor;
      this.y *= factor;
      return this;
   }

   normalizeRef(){
      let mag = this.sqrtMagnitude();
      if(mag == 0) return this;
      this.x /= mag;
      this.y /= mag;
      return this;
   }

   projectionRef(vec){
      let mag = vec.magnitude();
      if(mag == 0) return this;
      this.copy(vec.scale(this.dotProduct(vec) / mag));
      return this;
   }

   rotateRef(angle){
      angle *= Math.PI/180;

      let cos = Math.cos(angle);
      let sin = Math.sin(angle);

      let newX = this.x * cos - this.y * sin;
      let newY = this.x * sin + this.y * cos;

      this.x = newX;
      this.y = newY;
      return this;
   }

   transformRef(matrix){
      let newX = this.x * matrix.val[0][0] + this.y * matrix.val[0][1];
      let newY = this.x * matrix.val[1][0] + this.y * matrix.val[1][1];
      this.x = newX;
      this.y = newY;
      return this;
   }
}
