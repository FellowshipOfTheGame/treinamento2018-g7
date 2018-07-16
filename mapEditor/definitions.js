let ctx = canvas.getContext("2d");

let log = console.log;

canvas.height = window.innerHeight - 100;
canvas.width = window.innerWidth - 2 * canvas.offsetLeft;

let canvasHeight = canvas.height;
let canvasWidth = canvas.width;
let canvasOffsetLeft = canvas.offsetLeft;
let canvasOffsetTop = canvas.offsetTop;

let heightInput = document.getElementById("heightInput");
let widthInput = document.getElementById("widthInput");

ctx.textAlign="center";
ctx.textBaseline="middle";
