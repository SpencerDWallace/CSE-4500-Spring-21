# CSE-4500-Spring-21
Core Assignments - 02/02/2021 - Report #1
This folder contains core assignments 2 and 3

Project Folder - 02/02/2021 - Report #1
(I believe it is dependent on local file paths to run at the moment)
Start of Project Assignment, getting used to  node.js, javascript, and html. No exact plan for what project I want to do yet. 
- main.js sets up the port for a server and renders ejs files to each path
- routes.js sets up the paths on the server and the ejs file for each path (just index.ejs and home.ejs for now)
- home.js is mostly just a copy of index.ejs, this file was used to learn adding an additional path to the server
- index.ejs is the main page for the server, it has some html formatting and loads canvas.js, which has a canvas including a 'ball'
with click and drag physics (by this I mean click and drag your mouse, then let go, and it launches the ball, not click and drag the ball around)

Project Assignment Update - 02/17/2021 - Report #2
What I have been working on is under the spencerwallace007463307.github.io folder, this folder name is also the name of the github page hosting the app.

I have been having some fun experimenting with raycasting. I have created a web app that renders an array which serves as a map into a 3D scene.
I added a thumbstick/joystick which can be used for mobile devices with touch screen, I've only tested this with iOS devices but it has worked
on multiple iPhones and an iPad, and worked on safari and chrome. My next goal is to create a better method for a light source as well as
optimizing the code, I'd also like to add a sign in feature. I think after that I may continue trying to learn more complex rendering techniques
in Unity as I've been having a lot of fun with graphics. 

Project Assignment Update - 03/03/2021 - Report #3
Work for this period is under the folder "Some C# script"

I didn't have as much time as I would have liked to start trying to learn using Unity and C#, but over this period for the report I've watched some introduction to Unity and C# videos as well as some tutorials for inspiration. I made a script that controls a gameObject using the arrowkeys or the WASD keys, that lets you go forward and backward using up and down, and the left and right keys change the angle the object is facing rather than moving left and right. I think this could be changed so that the up and down arrow keys also change the angle, and maybe the spacebar controls acceleration as a system for controlling a spaceship; or maybe I'll use the mouse coordinates to change the angles, and the arrow keys can be used to tilt directions and accelerate/decelerate and then the spacebar or a mouseclick can be for shooting. I've been watching a series for planet generation, there's also script I wrote along with this included that procedurally generates a sphere mesh.

Project Assignment Update - 03/18/2021 - Report #4
Work for this period is under the folder "Update_4"

I have been continuing to work on the script for planet generation. Most of the features I wanted to implement have been, it now includes settings for adjusting the resolution of the planet, which creates more triangles in the mesh creating smaller areas for more accurate details, and gives a more spherical shape. Other settings include biome generation which allows you to put a tint over the planet from a certain height and is blended using noise functions I found online, similar to this there is also color control for the planet's terrain and the ocean's, and terrain manipulation using the same noise functions. I've also improved my script that controls the player (spaceship), the mouse is used to point the ship in the direction from the center of the screen to the mouse, and the arrow or WASD keys are used to accelerate or decelerate (W and S), and to roll/rotate along the z axis (A and D). My next steps are to try to implement the boids algorithm to simulate 'enemies' flocking around a main enemy ship, and maybe alter it so that some target the player as well, I'd also like to add collision detection to the planet(s), and look into using quadtrees to increase the level of detail of the planet depending on how close the player is and what is in the players field of view.
