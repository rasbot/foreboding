<p align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/foreboding.gif" width="850" height="auto"/>
</p>

Foreboding is a virtual reality game based off HP Lovecraft and tries to invoke a feeling of uneasiness and foreboding. The player is in an old and decrepit mansion. Notes scattered throughout the mansion which gives clues to what is happening. The mansion has an alter buried deep within it, and when 4 pages of the Necronomicon are found and placed around the alter, the player will invoke the Old Ones. If the player collects all the notes in the game, the ending will show a conglomeration of glowing spheres and large, ominous beings appear before the player loses their sanity. If all note pages are not collected, Cthulhu will appear and consume the player...

<p align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/cthulhu.jpg" width="850" height="auto"/>
</p>

Cthulhu was 3D modeled in a VR program called Google Blocks. This game features an immersive environment and experimental VR interactions and mechanics.

## Environment

Using a mansion from the Unity asset store, the mansion was extended to create an underground chamber. The player has a flashlight and explores this decaying old mansion. Particle effects and fog add to the environment. 

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/chamber.png" width="850" height="auto"/>
  <p>A view into the chamber.</p>
</div>

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/mansion.gif" width="850" height="auto"/>
  <p>Exploring the mansion.</p>
</div>

The mansion has 3 stories. The top story has rooms which contain pages of the book the player must collect. These rooms are broken up into different types of interactions.

### Digging Through a Fleshy Blob

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/blobby.gif" width="850" height="auto"/>
  <p>Fleshy blob.</p>
</div>

In virtual reality, one difficulty in implementing game mechanics is that the player's hand/controller can move freely through any object because their actual hand does not have any resistance to movement. Inside the blob there is a page that the player needs to find. To simulate some sort of inertia while the player attempts to reach the page is done by adding haptic feedback to the controller. The controller vibrates as the hand drags through the blob. The faster the hand moves, the stronger the feedback. The effect does convey the feeling of resistance to some level. The blob was an animated simple model created in Google Blocks. The room also has buzzing flies which the player has to walk through. In VR, it can be slightly uncomfortable.

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/flies.gif" width="650" height="auto"/>
</div>

### Painting Puzzle

This room has 3 paintings which have faded symbols on them. A TV in the room has images which occasionally flash a symbol with a blood spatter pattern. This can be decoded to figure out the order of the symbols. The player can touch the paintings, which changes their form:

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/painting.gif" width="850" height="auto"/>
</div>

If the player does not get the order correct, a monsterish sound will emerge from behind and the paintings will emit particle effects.

<div align="center">
  <img src="https://raw.githubusercontent.com/rasbot/foreboding/master/Images/painting_wrong.gif" width="850" height="auto"/>
</div>

The vast majority of my time was spent testing experimental interactions, most of which did not feel right or work out how I had hoped. My goal was to convey foreboding through different types of interactions while keeping things unclear as to if there is something sinister lurking in the 

**Here is a link to a video walkthrough of the game:**

- https://youtu.be/uXzsqyT7rvQ
