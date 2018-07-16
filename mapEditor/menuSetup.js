let terrainsMenu = new UIBox([80, 0], [20, 50], "#d0d0d0");
let creaturesMenu = new UIBox([0, 0], [20, 50], "#d0d0d0");

let selector = {
   type: "creature",
   race: "",
   creature: "clear",
   team: 0,
   terrain: "plains",
   viewer: new UIBox([45, 3], [10, 2], "#ffffff"),
   update: function(){
      if(this.type == "terrain"){
         this.viewer.color = Terrain.type2Color(this.terrain);
      }
      else if(this.type == "creature"){
         this.viewer.color = Creature.type2Color(this.race, this.creature);
      }
   }
}

creaturesMenu.addChild(new UIBox([3, 3], [15, 10], HUMAN_SOLDIER_COLOR), (t)=>{
   t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "human";
      selector.creature = "soldier";
      selector.update();
   });
   t.addChild(new Text("soldado", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([23, 3], [15, 10], HUMAN_ARCHER_COLOR), (t)=>{
   t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "human";
      selector.creature = "archer";
      selector.update();
   });
   t.addChild(new Text("arqueiro", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([43, 3], [15, 10], HUMAN_KNIGHT_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "human";
      selector.creature = "knight";
      selector.update();
   });
   t.addChild(new Text("cavaleiro", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([63, 3], [15, 10], HUMAN_HERO_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "human";
      selector.creature = "hero";
      selector.update();
   });
   t.addChild(new Text("heroi", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([83, 3], [15, 10], HUMAN_SIEGE_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "human";
      selector.creature = "siege";
      selector.update();
   });
   t.addChild(new Text("cerco", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([3, 18], [15, 10], UNDEAD_SOLDIER_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "undead";
      selector.creature = "soldier";
      selector.update();
   });
});

creaturesMenu.addChild(new UIBox([23, 18], [15, 10], UNDEAD_ARCHER_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "undead";
      selector.creature = "archer";
      selector.update();
   });
});

creaturesMenu.addChild(new UIBox([43, 18], [15, 10], UNDEAD_KNIGHT_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "undead";
      selector.creature = "knight";
      selector.update();
   });
});

creaturesMenu.addChild(new UIBox([63, 18], [15, 10], UNDEAD_HERO_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "undead";
      selector.creature = "hero";
      selector.update();
   });
});

creaturesMenu.addChild(new UIBox([83, 18], [15, 10], UNDEAD_SIEGE_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "undead";
      selector.creature = "siege";
      selector.update();
   });
});

creaturesMenu.addChild(new UIBox([25, 33], [50, 10], "#ffffff"), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "creature";
      selector.race = "";
      selector.creature = "clear";
      selector.update();
   });
   t.addChild(new Text("sem criatura", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

creaturesMenu.addChild(new UIBox([25, 48], [50, 10], "#ffffff"), (t)=>{
	t.addClickFunction(()=>{
	  selector.team = 1 - selector.team;
	  t.children[0].wrappedMessage = [selector.team.toString()];
      selector.update();
   });
   t.addChild(new Text("0", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([3, 3], [15, 10], PLAINS_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "terrain";
      selector.terrain = "plains";
      selector.update();
   });
   t.addChild(new Text("planice", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([23, 3], [15, 10], FOREST_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "terrain";
      selector.terrain = "forest";
      selector.update();
   });
   t.addChild(new Text("floresta", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([43, 3], [15, 10], MOUNTAIN_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "terrain";
      selector.terrain = "mountain";
      selector.update();
   });
   t.addChild(new Text("montanha", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([63, 3], [15, 10], FORTRESS_COLOR), (t)=>{
	t.addClickFunction(()=>{
      selector.type = "terrain";
      selector.terrain = "fortress";
      selector.update();
   });
   t.addChild(new Text("fortaleza", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([83, 3], [15, 10], RIVER_COLOR), (t)=>{
   t.addClickFunction(()=>{
      selector.type = "terrain";
      selector.terrain = "river";
      selector.update();
   });
   t.addChild(new Text("rio", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});

terrainsMenu.addChild(new UIBox([15, 23], [75, 10], "#ffff00"), (t)=>{
   t.addClickFunction(()=>{
      terrain.generateFile();
   });
   t.addChild(new Text("gerar arquivo", [50, 50], [100, 100], "#000000", {
      wrap : false, align:"center", baseLine:"middle"
   }));
});
