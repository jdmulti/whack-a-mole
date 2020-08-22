# Whack a Goomba

![Screen Start](docs/images/game_screen_start.jpg)
![Screen Game](docs/images/game_screen_game.jpg)

---

## Information
- Created by: Nathan van Hulst
- Unity3D 2019.4.8f1 (LTS)

---

## Devices

Application has been tested on a Samsung Galaxy S7 with Android installed.

---

## Gameplay

Game can be started by pressing the `Start` button on the start screen.

During play, user can tap the screen whenever a `Goomba` (Mole) is appearing at a `Warp pipe` (Hole). The user has one hammer to be used, tapping several goomba's rappidly doesn't have any succes as the `hammer` first has to slam onto a goomba before it can be used on another.

While the game is progressing, the difficulty will ramp up which can be seen at the `Wave` counter which is shown on the left side of the screen. The game will start at wave 1 and progress up to wave 4. The total time of the current gameplay session is set to `60 seconds` and can be seen on the right side of the screen.
The total score is displayed at the left side of the screen. Whenever a goomba is succesfully hit with Mario's hammer, the user will score a point. The total score depends on the user quick reaction as well the right timing of using the hammer after it already has hit a goomba.

At the score screen the user will be notified with his current score and if this is a new highscore and is not, how much he has scores and what the current set highscore is.

---

## Project

### Scene Layout Gamelogic
All specific areas that combine the scene into a final games are seperated by seperators which can be identified as GameObjects with names consisting of several dashes.

The scene has been split into the following sections:
- Camera
- Lighting
- GameLogic (where all game logic)
- GUI (2D graphical user interface, where all UI game logic is)
- 3D (where all 3d elements are)

#### ComponentSystem
Elliminates broad use of Unity's standard methods. Doing so will make the game perform better as every Unity standard method comes with an overhead. The component system script is the only script containing such standard methods.

#### GameController
Where all logic comes together and data is managed between different managers and objects.

#### CountdownTimer
As time passes away, this timer is used to be able to display the current time.

#### CountdownTimerRandomAppear
While the naming of the game object is different, it used the same class as CountdownTimer. However the usecase is different as this timer is used for random appearing goomba's.

#### HoleManager
Managing the warp pipes (holes) by just initalizing and resetting them.

#### MoleManager
Managing the goomba's (moles), by positioning, resetting and animating them.

#### InputManager
Handles touch input on screen and raycasting goomba's.

#### ScoreManager
Keeping tracking of player scores of current game session as well the overal player highscore between game sessions.

#### DifficultyManager
Manager to ramp up the difficulty trough `DifficultySettings` `ScriptableObjects` based on the current time of `CountdownTimer` and the total game duration.

#### AudioManager
Handling playing audio sound effects and music.

---

