var canvas;
let x = 1535/2,y = 575/2, z = 50, floor = 544, count = 0, x2 = 0, y2 = 0, mouseWC = false;
let xVelocity = 3, xRate;
let yVelocity = 0, yCap = 25, moves = 50, rate, mX = null, mY = null, mXR = null, mYR = null;
let fontsize = 80;

function setup(){
    canvas = createCanvas(1535,575);
textSize(80);
}

//physics for the circle, acceleration decreases each 'bounce'
function motion() {
    if(mouseWC){
        mouseWC = false;
        xVelocity += -1*(mXR - mX)/20;
        yVelocity += -1*(mYR - mY)/12;
    }
        //xRate = (
       /* rate = (floor-yCap)/moves;
        yVelocity += 0.5*(rate/moves);*/
        if(xVelocity < 0){
            xVelocity -= xVelocity/200;
        }
        else
            xVelocity -= xVelocity/200;
        if(yVelocity < 0){
            yVelocity -= yVelocity/(200/3);
        }
        else
            yVelocity -= yVelocity/(200/3);

    if(yCap < 574) {
        x += xVelocity;
        y += yVelocity;
    }

    if (x < 25) {
        xVelocity = -xVelocity;
        x = 25
    }
    else if(x > 1510){
        xVelocity = -xVelocity;
        x = 1510
    }

    if (y > 545) {
        count++;
        yVelocity = -yVelocity;
        y = 545
       // yCap += 50;
        moves = (floor - yCap)/yVelocity;

    }
    else if (y < yCap) {
        y = yCap + 1;
        yVelocity = -yVelocity;

    }
}

function drawLine(){

    if(mX!= null && mouseIsPressed){
        strokeWeight(5);
        line(mX, mY, mouseX, mouseY);
    }
}

function draw(){
    textAlign(CENTER);

    //background(mouseX/5, mouseY/1.5,mouseX/4 - mouseY/1.5);
    background(x/6, y/2,x/6 - y/2);
    drawWords(width * 0.5);
    fill(255);
    circle(x,y,z);
    stroke(255);
    drawLine();
    strokeWeight(3);
    motion();
}


function mousePressed(){
    mX = mouseX;
    mY = mouseY;

    return false;
}
function mouseReleased() {
    mXR = mouseX;
    mYR = mouseY;
    mouseWC = true

    return false;
}
function drawWords(x) {

    fill(random(50,250),random(50,250),random(50,250));
    text('BRRRRRRRRRRRRRRRRRRRRRRRR', x, 180);

}