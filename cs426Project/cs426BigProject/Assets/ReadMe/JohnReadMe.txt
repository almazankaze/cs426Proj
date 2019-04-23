Followed a tutorial to learn how to setup a basic first person controller, script in video was used. All credit goes to this video. PlayerController.cs and CameraControl.cs

https://www.youtube.com/watch?v=n-KX8AeGK7E&list=FLaADp1x39RRwwmsmobX9mkQ&index=16

What I learned:

1. I learned about serial field which makes it so you can have your variables be private, but you can still edit/see them in the unity interface. Very Helpful.

2. I also lerned from the video the concept of raycasting where you shoot a ray to detect a collision. This was shown in the video as a way to detect if your on a slope by comparing
the length of the ray to a small value which signifies that your on a slope. If your on a slope, downward force was applied to the player to slant them a bit to make it look like they are on a slope amd eliminate some jittering.

3. I learned how to move the camera on the horizontal axis by using euler angle function and using that rotation to also rotate the player by changing the player 
transform component. For the vertival axis, I learned how to clamp the rotation so that the camera did not rotate 360 degrees above the character or below.

4. I learned how to implement basic running with the press of a button where the movement speed would just be increased while run button was held down.

5. I learned how to implement jumping using a coroutine and an animation curve which controlled how much force would be applied to player when jump key was pressed.
Coroutines stop execution for a frame before next instruction is executed. You can add a delay longer than that if you want. This will be useful for reloading a gun later on.

Script to spawn enemy in networking used, all credit to this video
https://www.youtube.com/watch?v=xe40VS_SB6Q

Script to assign each player a unique name was used, all credit to this video. This script will be used to handle hit detection of bullets to players. PlayerID.cs
https://www.youtube.com/watch?v=eKkE7ki0XbA&index=9&list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG

Simple Script that uses raycasting to shoot was used as a starting point, all credit to this video. Goes into depth on how to handle shooting in networking. GunScript.cs
https://www.youtube.com/watch?v=_u_0IphF33o&list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG&index=10

Simple Health script used to give player health, all credit to this video. PlayerHealth.cs
https://www.youtube.com/watch?v=R14oxS3IY3A&list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG&index=11

Basic patrol AI script was used from this tutorial. Goes into depth on how to assign patrol spots. All credit for script goes to this video. BasicPatrol.cs
https://www.youtube.com/watch?v=8eWbSN2T8TE

Tutorial on displaying a message on the screen for an event was followed
https://codingchronicles.com/unity-vr-development/day-21-creating-game-ui-unity

Tutorial on making a textbox was followed and I learned how to create a dialogue box using an array. Script for the text box is from video not mine. All credit to original creator.
https://www.youtube.com/watch?v=xcgqSxPgrBA

Assets used were taken from the unity asset store:

sound for the gun
https://assetstore.unity.com/packages/audio/sound-fx/weapons/futuristic-gun-soundfx-100851
https://assetstore.unity.com/packages/3d/environments/abandoned-buildings-62875

material for the cloth
https://assetstore.unity.com/packages/2d/textures-materials/fabric/yughues-free-fabric-materials-13002

the gun model
https://assetstore.unity.com/packages/3d/props/weapons/weapon-master-scifi-weapon-1-lite-134423

for the muzzle flash of gun
https://assetstore.unity.com/packages/2d/textures-materials/sprite-muzzle-flashes-83068

model for the eagle
https://assetstore.unity.com/packages/3d/characters/animals/egypt-pack-eagle-140079

model for the player
https://assetstore.unity.com/packages/3d/characters/robots/tiny-robots-pack-98930

models for buildings
https://assetstore.unity.com/packages/3d/environments/urban/russian-buildings-lowpoly-pack-80518
https://assetstore.unity.com/packages/3d/characters/village-houses-pack-63695

for items
https://assetstore.unity.com/packages/3d/characters/wood-products-80094
https://assetstore.unity.com/packages/3d/props/small-survival-pack-20565

game over sound
https://assetstore.unity.com/packages/audio/sound-fx/voices/a-voice-over-for-game-over-132171

cutscene bg music
https://assetstore.unity.com/packages/audio/ambient/horror-sound-atmospheres-and-fx-96731