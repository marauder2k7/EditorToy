//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//ParticlePlayer functions 
function ParticlePlayerLockBttn::update(%this)
{
	%obj = EditorToy.selObject;
	%value = %obj.locked;
	if(%value == 1)
	{
		ParticlePlayerScroll.setVisible(0);
	}
	%this.setStateOn(%value);
}

function EditorToy::createParticlePlayer(%this, %assetFile)
{

	//Spawn particlePlayers at camera position
	%scene = EditorToy.activeScene;
	%pos = SandboxWindow.getCameraPosition();
	%asset = ParticleSim.getObject(0).getName();
	%mName = EditorToy.moduleName;
	%size = EditorToy.sceneObjectX SPC EditorToy.sceneObjectY;
	%particlePlayerBase = %assetFile;
	// Create the particlePlayer.
    %object = new ParticlePlayer();
    %object.Size = %size;
	%object.SetBodyType( static );
	%object.Particle = %asset;
	%object.Position = %pos;
	//Objects in editor are inactive until unleashed into
	//the hell we have created for our own enjoyment >:)
	//in other words until scene is unpaused...
	%object.setActive(0);
	%scene.add(%object);
}

function EditorToy::updateParticlePlayer(%this)
{
	//Initialize our data
	%obj = EditorToy.selParticlePlayer;
	%name = EditorToy.particlePlayerName;
	%class = EditorToy.particlePlayerClass;
	%posX = EditorToy.particlePlayerPosX;
	%posY = EditorToy.particlePlayerPosY;
	%width = EditorToy.particlePlayerWidth;
	%height = EditorToy.particlePlayerHeight;
	%size = %width SPC %height;
	%pos = %posX SPC %posY;
	%body = EditorToy.particlePlayerBody;
	%ang = EditorToy.particlePlayerAngle;
	%fixAng = EditorToy.particlePlayerFixedAngle;
	%angDam = EditorToy.particlePlayerAngDamp;
	%angVel = EditorToy.particlePlayerAngVel;
	%linVelX = EditorToy.particlePlayerLinVelX;
	%linVelY = EditorToy.particlePlayerLinVelY;
	%linVelPolAng = EditorToy.particlePlayerLinPolAngle;
	%linVelPolSpeed = EditorToy.particlePlayerLinPolSpeed;
	%linDam = EditorToy.particlePlayerLinDamp;
	%defDen = EditorToy.particlePlayerDefDensity;
	%defFri = EditorToy.particlePlayerDefFriction;
	%defRes = EditorToy.particlePlayerDefRestitution;
	%collSupp = EditorToy.particlePlayerCollSupp;
	%collOne = EditorToy.particlePlayerCollOne;
	%colShape = EditorToy.polyListPosLocal;
	%grav = EditorToy.particlePlayerGravity;
	%sceneLay = EditorToy.particlePlayerSceneLayer;
	%sceneGroup = EditorToy.particlePlayerSceneGroup;
	%collLayers = EditorToy.particlePlayerCollLayers;
	%collGroups = EditorToy.particlePlayerCollGroups;
	%blendMode = EditorToy.particlePlayerBlendMode;
	%srcBlend = EditorToy.particlePlayerSrcBlend;
	%dstBlend = EditorToy.particlePlayerDstBlend;
	%alphaTest = EditorToy.particlePlayerAlphaTest;
	%blendR = EditorToy.particlePlayerBlendR;
	%blendG = EditorToy.particlePlayerBlendG;
	%blendB = EditorToy.particlePlayerBlendB;
	%blendA = EditorToy.particlePlayerBlendA;
	
	//set data to selected particlePlayer
	%obj.setName(%name);
	%obj.class = %class;
	%obj.setPosition(%pos);
	%obj.setSize(%size);
	%obj.setBodyType(%body);
	%obj.setAngle(%ang);
	%obj.setFixedAngle(%fixAng);
	%obj.setAngularDamping(%angDam);
	%obj.setAngularVelocity(%angVel);
	%obj.setLinearVelocity(%linVelX, %linVelY);
	%obj.setLinearVelocityPolar(%linVelPolAng,%linVelPolSpeed);
	%obj.setLinearDamping(%linVel);
	//We dont want to change the values of all collision shapes by default
	%obj.setDefaultDensity(%defDen, false);
	%obj.setDefaultFriction(%defFri, false);
	%obj.setDefaultRestitution(%defRes, false);
	%obj.setCollisionSuppress(%collSupp);
	%obj.setCollisionOneWay(%collOne);
	
	if(%colShape !$= "")
	{
		%type = EditorToy.drawType;
		if(%type $= "polyCol")
		{
			%obj.createPolygonCollisionShape(%colShape);
		}
		else if(%type $= "edgeCol")
		{
			%colEdgeLocSX = getWord(%colShape,1);
			%colEdgeLocSY = getWord(%colShape,2);
			%colEdgeLocEX = getWord(%colShape,3);
			%colEdgeLocEY = getWord(%colShape,4);
			%obj.createEdgeCollisionShape(%colEdgeLocSX, %colEdgeLocSY, %colEdgeLocEX, %colEdgeLocEY);
		}
		else if(%type $= "chainCol")
		{
			%obj.createChainCollisionShape(%colShape);
		}
	}
	%obj.setGravityScale(%grav);
	%obj.setSceneLayer(%sceneLay);
	%obj.setSceneGroup(%sceneGroup);
	%layerCount = getWordCount(%collLayers);
	%groupCount = getWordCount(%collGroups);
	if(%layerCount >= 1)
	{
		%obj.setCollisionLayers(%collLayers);
	}
	else
	{
		%obj.setCollisionLayers( none );
	}
	
	if(%groupCount >= 1)
	{
		%obj.setCollisionGroups(%collGroups);
	}
	else
	{
		%obj.setCollisionGroups( none );
	}
	%obj.setAlphaTest(%alphaTest);
	%obj.setBlendMode(%blendMode);
	%obj.setSrcBlendFactor(%srcBlend);
	%obj.setDstBlendFactor(%dstBlend);
	%obj.setBlendColor(%blendR, %blendG, %blendB);
	%obj.setBlendAlpha(%blendA);
	
	%this.resetPolyListPosLocal();
	
}

//-----------------------------------------------------------------------------
//Update ParticlePlayer Values
function EditorToy::updateSelParticlePlayer(%this, %obj)
{
	%this.selParticlePlayer = %obj;
}
function EditorToy::updateParticlePlayerPosX(%this, %value)
{
	%this.particlePlayerPosX = %value;
}

function EditorToy::updateParticlePlayerPosY(%this, %value)
{
	%this.particlePlayerPosY = %value;
}

function EditorToy::updateParticlePlayerWidth(%this, %value)
{
	%this.particlePlayerWidth = %value;
}

function EditorToy::updateParticlePlayerHeight(%this, %value)
{
	%this.particlePlayerHeight = %value;
}

function EditorToy::updateParticlePlayerFlipX(%this, %value)
{
	%this.particlePlayerFlipX = %value;
}

function EditorToy::updateParticlePlayerFlipY(%this, %value)
{
	%this.particlePlayerFlipY = %value;
}

function EditorToy::updateParticlePlayerName(%this, %value)
{
	%this.particlePlayerName = %value;
}

function EditorToy::updateParticlePlayerClass(%this, %value)
{
	%this.particlePlayerClass = %value;
}

function EditorToy::updateParticlePlayerAngle(%this, %value)
{
	%this.particlePlayerAngle = %value;
}

function EditorToy::updateParticlePlayerFixedAngle(%this, %value)
{
	%this.particlePlayerFixedAngle = %value;
}

function EditorToy::updateParticlePlayerAngDamp(%this, %value)
{
	%this.particlePlayerAngDamp = %value;
}

function EditorToy::updateParticlePlayerAngVel(%this, %value)
{
	%this.particlePlayerAngVel = %value;
}

function EditorToy::updateParticlePlayerLinVelX(%this, %value)
{
	%this.particlePlayerLinVelX = %value;
}

function EditorToy::updateParticlePlayerLinVelY(%this, %value)
{
	%this.particlePlayerLinVelY = %value;
}

function EditorToy::updateParticlePlayerLinVelPolAngle(%this, %value)
{
	%this.particlePlayerLinVelPolAngle = %value;
}

function EditorToy::updateParticlePlayerLinVelPolSpeed(%this, %value)
{
	%this.particlePlayerLinVelPolSpeed = %value;
}

function EditorToy::updateParticlePlayerLinDamp(%this, %value)
{
	%this.particlePlayerLinDamp = %value;
}

function EditorToy::updateParticlePlayerDefDensity(%this, %value)
{
	%this.particlePlayerDefDensity = %value;
}

function EditorToy::updateParticlePlayerDefFriction(%this, %value)
{
	%this.particlePlayerDefFriction = %value;
}

function EditorToy::updateParticlePlayerDefRestitution(%this, %value)
{
	%this.particlePlayerDefRestitution = %value;
}

function EditorToy::updateParticlePlayerCollSupp(%this, %value)
{
	%this.particlePlayerCollSupp = %value;
}

function EditorToy::updateParticlePlayerCollOne(%this, %value)
{
	%this.particlePlayerCollOne = %value;
}

function EditorToy::updateParticlePlayerBody(%this, %value)
{
	%this.particlePlayerBody = %value;
}

function EditorToy::updateParticlePlayerCollShapeCount(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%colNum = %obj.getCollisionShapeCount();
	%this.particlePlayerCollShapeCount = %colNum;
}

function EditorToy::updateParticlePlayerFrame(%this, %value)
{
	%this.particlePlayerFrame = %value;
}

function EditorToy::updateParticlePlayerGravity(%this, %value)
{
	%this.particlePlayerGravity = %value;
}

function EditorToy::updateParticlePlayerSceneLayer(%this, %value)
{
	%this.particlePlayerSceneLayer = %value;
}

function EditorToy::updateParticlePlayerSceneGroup(%this, %value)
{
	%this.particlePlayerSceneGroup = %value;
}

function EditorToy::updateParticlePlayerCollLayers(%this, %value)
{
	%this.particlePlayerCollLayers = %value;
}

function EditorToy::updateParticlePlayerCollGroups(%this, %value)
{
	%this.particlePlayerCollGroups = %value;
}

function EditorToy::updateParticlePlayerAlphaTest(%this, %value)
{
	%this.particlePlayerAlphaTest = %value;
}

function EditorToy::updateParticlePlayerBlendMode(%this, %value)
{
	%this.particlePlayerBlendMode = %value;
}

function EditorToy::updateParticlePlayerSrcBlend(%this, %value)
{
	%this.particlePlayerSrcBlend = %value;
}

function EditorToy::updateParticlePlayerDstBlend(%this, %value)
{
	%this.particlePlayerDstBlend = %value;
}

function EditorToy::updateParticlePlayerBlendR(%this, %value)
{
	%this.particlePlayerBlendR = %value;
}

function EditorToy::updateParticlePlayerBlendG(%this, %value)
{
	%this.particlePlayerBlendG = %value;
}

function EditorToy::updateParticlePlayerBlendB(%this, %value)
{
	%this.particlePlayerBlendB = %value;
}

function EditorToy::updateParticlePlayerBlendA(%this, %value)
{
	%this.particlePlayerBlendA = %value;
}

//Load arrays for collision layers and groups
function EditorToy::loadParticlePlayerCollLayerArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.particlePlayerCollLayer[0] = false;
	EditorToy.particlePlayerCollLayer[1] = false;
	EditorToy.particlePlayerCollLayer[2] = false;
	EditorToy.particlePlayerCollLayer[3] = false;
	EditorToy.particlePlayerCollLayer[4] = false;
	EditorToy.particlePlayerCollLayer[5] = false;
	EditorToy.particlePlayerCollLayer[6] = false;
	EditorToy.particlePlayerCollLayer[7] = false;
	EditorToy.particlePlayerCollLayer[8] = false;
	EditorToy.particlePlayerCollLayer[9] = false;
	EditorToy.particlePlayerCollLayer[10] = false;
	EditorToy.particlePlayerCollLayer[11] = false;
	EditorToy.particlePlayerCollLayer[12] = false;
	EditorToy.particlePlayerCollLayer[13] = false;
	EditorToy.particlePlayerCollLayer[14] = false;
	EditorToy.particlePlayerCollLayer[15] = false;
	EditorToy.particlePlayerCollLayer[16] = false;
	EditorToy.particlePlayerCollLayer[17] = false;
	EditorToy.particlePlayerCollLayer[18] = false;
	EditorToy.particlePlayerCollLayer[19] = false;
	EditorToy.particlePlayerCollLayer[20] = false;
	EditorToy.particlePlayerCollLayer[21] = false;
	EditorToy.particlePlayerCollLayer[22] = false;
	EditorToy.particlePlayerCollLayer[23] = false;
	EditorToy.particlePlayerCollLayer[24] = false;
	EditorToy.particlePlayerCollLayer[25] = false;
	EditorToy.particlePlayerCollLayer[26] = false;
	EditorToy.particlePlayerCollLayer[27] = false;
	EditorToy.particlePlayerCollLayer[28] = false;
	EditorToy.particlePlayerCollLayer[29] = false;
	EditorToy.particlePlayerCollLayer[30] = false;
	EditorToy.particlePlayerCollLayer[31] = false;
	
	%collLayers = EditorToy.particlePlayerCollLayers;
	%count = getWordCount( %collLayers);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collLayers,%i);
		EditorToy.particlePlayerCollLayer[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	ParticlePlayerCollLay0.update();
	ParticlePlayerCollLay1.update();
	ParticlePlayerCollLay2.update();
	ParticlePlayerCollLay3.update();
	ParticlePlayerCollLay4.update();
	ParticlePlayerCollLay5.update();
	ParticlePlayerCollLay6.update();
	ParticlePlayerCollLay7.update();
	ParticlePlayerCollLay8.update();
	ParticlePlayerCollLay9.update();
	ParticlePlayerCollLay10.update();
	ParticlePlayerCollLay11.update();
	ParticlePlayerCollLay12.update();
	ParticlePlayerCollLay13.update();
	ParticlePlayerCollLay14.update();
	ParticlePlayerCollLay15.update();
	ParticlePlayerCollLay16.update();
	ParticlePlayerCollLay17.update();
	ParticlePlayerCollLay18.update();
	ParticlePlayerCollLay19.update();
	ParticlePlayerCollLay20.update();
	ParticlePlayerCollLay21.update();
	ParticlePlayerCollLay22.update();
	ParticlePlayerCollLay23.update();
	ParticlePlayerCollLay24.update();
	ParticlePlayerCollLay25.update();
	ParticlePlayerCollLay26.update();
	ParticlePlayerCollLay27.update();
	ParticlePlayerCollLay28.update();
	ParticlePlayerCollLay29.update();
	ParticlePlayerCollLay30.update();
	ParticlePlayerCollLay31.update();
	
}

function EditorToy::stringParticlePlayerCollLayerArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.particlePlayerCollLayer[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.particlePlayerCollLayer[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the particlePlayers settings
	EditorToy.updateParticlePlayerCollLayers(%layerString);
	//Update the gui
	EditorToy.loadParticlePlayerCollLayerArray();
}

function EditorToy::loadParticlePlayerCollGroupArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.particlePlayerCollGroup[0] = false;
	EditorToy.particlePlayerCollGroup[1] = false;
	EditorToy.particlePlayerCollGroup[2] = false;
	EditorToy.particlePlayerCollGroup[3] = false;
	EditorToy.particlePlayerCollGroup[4] = false;
	EditorToy.particlePlayerCollGroup[5] = false;
	EditorToy.particlePlayerCollGroup[6] = false;
	EditorToy.particlePlayerCollGroup[7] = false;
	EditorToy.particlePlayerCollGroup[8] = false;
	EditorToy.particlePlayerCollGroup[9] = false;
	EditorToy.particlePlayerCollGroup[10] = false;
	EditorToy.particlePlayerCollGroup[11] = false;
	EditorToy.particlePlayerCollGroup[12] = false;
	EditorToy.particlePlayerCollGroup[13] = false;
	EditorToy.particlePlayerCollGroup[14] = false;
	EditorToy.particlePlayerCollGroup[15] = false;
	EditorToy.particlePlayerCollGroup[16] = false;
	EditorToy.particlePlayerCollGroup[17] = false;
	EditorToy.particlePlayerCollGroup[18] = false;
	EditorToy.particlePlayerCollGroup[19] = false;
	EditorToy.particlePlayerCollGroup[20] = false;
	EditorToy.particlePlayerCollGroup[21] = false;
	EditorToy.particlePlayerCollGroup[22] = false;
	EditorToy.particlePlayerCollGroup[23] = false;
	EditorToy.particlePlayerCollGroup[24] = false;
	EditorToy.particlePlayerCollGroup[25] = false;
	EditorToy.particlePlayerCollGroup[26] = false;
	EditorToy.particlePlayerCollGroup[27] = false;
	EditorToy.particlePlayerCollGroup[28] = false;
	EditorToy.particlePlayerCollGroup[29] = false;
	EditorToy.particlePlayerCollGroup[30] = false;
	EditorToy.particlePlayerCollGroup[31] = false;
	
	%collGroups = EditorToy.particlePlayerCollGroups;
	%count = getWordCount( %collGroups);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collGroups,%i);
		EditorToy.particlePlayerCollGroup[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	ParticlePlayerCollGroup0.update();
	ParticlePlayerCollGroup1.update();
	ParticlePlayerCollGroup2.update();
	ParticlePlayerCollGroup3.update();
	ParticlePlayerCollGroup4.update();
	ParticlePlayerCollGroup5.update();
	ParticlePlayerCollGroup6.update();
	ParticlePlayerCollGroup7.update();
	ParticlePlayerCollGroup8.update();
	ParticlePlayerCollGroup9.update();
	ParticlePlayerCollGroup10.update();
	ParticlePlayerCollGroup11.update();
	ParticlePlayerCollGroup12.update();
	ParticlePlayerCollGroup13.update();
	ParticlePlayerCollGroup14.update();
	ParticlePlayerCollGroup15.update();
	ParticlePlayerCollGroup16.update();
	ParticlePlayerCollGroup17.update();
	ParticlePlayerCollGroup18.update();
	ParticlePlayerCollGroup19.update();
	ParticlePlayerCollGroup20.update();
	ParticlePlayerCollGroup21.update();
	ParticlePlayerCollGroup22.update();
	ParticlePlayerCollGroup23.update();
	ParticlePlayerCollGroup24.update();
	ParticlePlayerCollGroup25.update();
	ParticlePlayerCollGroup26.update();
	ParticlePlayerCollGroup27.update();
	ParticlePlayerCollGroup28.update();
	ParticlePlayerCollGroup29.update();
	ParticlePlayerCollGroup30.update();
	ParticlePlayerCollGroup31.update();
	
}

function EditorToy::stringParticlePlayerCollGroupArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.particlePlayerCollGroup[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.particlePlayerCollGroup[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the particlePlayers settings
	EditorToy.updateParticlePlayerCollGroups(%layerString);
	//Update the gui
	EditorToy.loadParticlePlayerCollGroupArray();
}

//-----------------------------------------------------------------------------
//Update ParticlePlayer Menu
//ParticlePlayer Pos X
function ParticlePlayerPosX::onAdd(%this)
{
	%text = EditorToy.particlePlayerPosX;
	%this.setText(%text);
}

function ParticlePlayerPosX::update(%this)
{
	%text = EditorToy.particlePlayerPosX;
	%this.setText(%text);
}

function ParticlePlayerPosX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerPosX(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerPosX(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerPosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerPosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Y Pos
function ParticlePlayerPosY::onAdd(%this)
{
	%text = EditorToy.particlePlayerPosY;
	%this.setText(%text);
}

function ParticlePlayerPosY::update(%this)
{
	%text = EditorToy.particlePlayerPosY;
	%this.setText(%text);
}

function ParticlePlayerPosY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerPosY(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerPosY(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerPosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerPosY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerPosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Width
function ParticlePlayerWidth::onAdd(%this)
{
	%text = EditorToy.particlePlayerWidth;
	%this.setText(%text);
}

function ParticlePlayerWidth::update(%this)
{
	%text = EditorToy.particlePlayerWidth;
	%this.setText(%text);
}

function ParticlePlayerWidth::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerWidth(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerWidth::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerWidth(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerWidth::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerWidth::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Height
function ParticlePlayerHeight::onAdd(%this)
{
	%text = EditorToy.particlePlayerHeight;
	%this.setText(%text);
}

function ParticlePlayerHeight::update(%this)
{
	%text = EditorToy.particlePlayerHeight;
	%this.setText(%text);
}

function ParticlePlayerHeight::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerHeight(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerHeight::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerHeight(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerHeight::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerHeight::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer FlipX
function ParticlePlayerFlipX::onAdd(%this)
{
	%value = EditorToy.particlePlayerFlipX;
	%this.setStateOn(%value);
}

function ParticlePlayerFlipX::update(%this)
{
	%value = EditorToy.particlePlayerFlipX;
	%this.setStateOn(%value);
}

function ParticlePlayerFlipX::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerFlipX(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer FlipY
function ParticlePlayerFlipY::onAdd(%this)
{
	%value = EditorToy.particlePlayerFlipY;
	%this.setStateOn(%value);
}

function ParticlePlayerFlipY::update(%this)
{
	%value = EditorToy.particlePlayerFlipY;
	%this.setStateOn(%value);
}

function ParticlePlayerFlipY::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerFlipY(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Body Type
function ParticlePlayerBodyList::onAdd(%this)
{
	%this.add( "Static", 1);
	%this.add( "Dynamic", 2);
	%this.add( "Kinematic", 3);
}

function ParticlePlayerBodyList::update(%this)
{
	%value = EditorToy.particlePlayerBody;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticlePlayerBodyList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateParticlePlayerBody(%value);
	EditorToy.updateParticlePlayer();
}

//Collision Shapes gui
function EditorToy::updateParticlePlayerCollGui(%this)
{
	if(isObject (ParticlePlayerMainCollContainer))
	{
		ParticlePlayerCollisionShapeStack.remove(ParticlePlayerMainCollContainer);
	}
	
	%obj = EditorToy.selParticlePlayer;
	%colNum = EditorToy.particlePlayerCollShapeCount;
	ParticlePlayerCollisionShapeContainer.setExtent(197,150);
	
	if(%colNum == 0)
		return;
	if(%colNum > 0)
	{
		%mainContainer = new GuiControl(ParticlePlayerMainCollContainer){
			position = "0 0";
			extent = "197 0";
			isContainer = "1";
		};
		
		%mainStack = new GuiStackControl(){
			position = "0 0";
			extent = "197 0";
			stackingType = "Vertical";
			horizStacking = "Left to Right";
			vertStacking = "Top to Bottom";
			padding = "3";
		};
	
		%mainContainer.add(%mainStack);

		for(%i = 0; %i < %colNum; %i++)
		{
			%colType = %obj.getCollisionShapeType(%i);
			//Build menu for circle collision shape
			if(%colType $= "Circle")
			{
				%colDen = %obj.getCollisionShapeDensity(%i);
				%colFri = %obj.getCollisionShapeFriction(%i);
				%colRes = %obj.getCollisionShapeRestitution(%i);
				%colSen = %obj.getCollisionShapeIsSensor(%i);
				%colArea = %obj.getCollisionShapeArea(%i);
				%colCirRad = %obj.getCircleCollisionShapeRadius(%i);
				%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%i);
				%mainContainerExtent = %mainContainerExtent + 242;
				
				%container = new GuiControl() {
					position = "0 0";
					extent = "197 239";
					minExtent = "197 239";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiDefaultBorderProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";

					new GuiTextCtrl() {
						text = %colType SPC %i;
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 11";
						extent = "182 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Density";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 43";
						extent = "64 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollDensity";
						text = %colDen;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "72 43";
						extent = "110 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Friction";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 75";
						extent = "64 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollFriction";
						text = %colFri;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "72 75";
						extent = "110 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Restitution";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 107";
						extent = "64 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollRestitution";
						text = %colRes;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "72 107";
						extent = "110 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Radius";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 139";
						extent = "64 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollRadius";
						text = %colCirRad;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "72 139";
						extent = "110 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Local Position";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "8 163";
						extent = "68 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "X:";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "14 182";
						extent = "8 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollLocalX";
						text = %colCirLoc.x;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "25 182";
						extent = "51 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
						text = "Y:";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "122 182";
						extent = "8 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
						ObjId = %i;
						class = "ParticlePlayerCollLocalY";
						text = %colCirLoc.y;
						historySize = "0";
						tabComplete = "0";
						sinkAllKeyEvents = "0";
						password = "0";
						passwordMask = "*";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "132 182";
						extent = "51 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextEditProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					
					new GuiBitmapButtonCtrl() {
						bitmap = "^EditorToy/assets/gui/images/DeleteBttn";
						ObjId = %i;
						command = "EditorToy.deleteParticlePlayerColShape("@ %i @ ");";
						bitmapMode = "Stretched";
						autoFitExtents = "0";
						useModifiers = "0";
						useStates = "1";
						masked = "0";
						groupNum = "-1";
						buttonType = "PushButton";
						useMouseEvents = "0";
						position = "160 208";
						extent = "25 25";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiDefaultProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "0";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
						text = "Is Sensor";
						maxLength = "1024";
						margin = "0 0 0 0";
						padding = "0 0 0 0";
						anchorTop = "1";
						anchorBottom = "0";
						anchorLeft = "1";
						anchorRight = "0";
						position = "32 214";
						extent = "64 18";
						minExtent = "8 2";
						horizSizing = "right";
						vertSizing = "bottom";
						profile = "GuiEditorTextProfile";
						visible = "1";
						active = "1";
						tooltipProfile = "GuiToolTipProfile";
						hovertime = "1000";
						isContainer = "1";
						canSave = "1";
						canSaveDynamicFields = "0";
					};
				};
				
				%sensor = new GuiCheckBoxCtrl() {
				text = " ";
				ObjId = %i;
				class="ParticlePlayerCollSensor";
				groupNum = "-1";
				buttonType = "ToggleButton";
				useMouseEvents = "0";
				position = "8 208";
				extent = "33 30";
				minExtent = "8 2";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiCheckBoxProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "0";
				canSave = "1";
				canSaveDynamicFields = "0";
				};
					
				%sensor.setStateOn(%colSen);
				
				%container.add(%sensor);
				
				%mainStack.add(%container);
				
			}
			
			//Build menu for polygon collision shape
			if(%colType $= "Polygon")
			{
				%colDen = %obj.getCollisionShapeDensity(%i);
				%colFri = %obj.getCollisionShapeFriction(%i);
				%colRes = %obj.getCollisionShapeRestitution(%i);
				%colSen = %obj.getCollisionShapeIsSensor(%i);
				%colCount = %obj.getPolygonCollisionShapePointCount(%i);
				%mainContainerExtent = %mainContainerExtent + 203;
				
				%container = new GuiControl() {
				position = "0 0";
				extent = "197 200";
				minExtent = "197 200";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiDefaultBorderProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "1";
				canSave = "1";
				canSaveDynamicFields = "0";
				
					new GuiTextCtrl() {
					text = %colType SPC %i;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 11";
					extent = "182 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Density";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 43";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollDensity";
					text = %colDen;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 43";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Friction";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 75";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollFriction";
					text = %colFri;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 75";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Restitution";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 107";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollRestitution";
					text = %colRes;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 107";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Points:";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 139";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
					text = %colCount;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "48 139";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Is Sensor";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "32 174";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiBitmapButtonCtrl() {
					bitmap = "^EditorToy/assets/gui/images/DeleteBttn";
					ObjId = %i;
					command = "EditorToy.deleteParticlePlayerColShape("@ %i @ ");";
					bitmapMode = "Stretched";
					autoFitExtents = "0";
					useModifiers = "0";
					useStates = "1";
					masked = "0";
					groupNum = "-1";
					buttonType = "PushButton";
					useMouseEvents = "0";
					position = "160 168";
					extent = "25 25";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiDefaultProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "0";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				};
				
				%sensor = new GuiCheckBoxCtrl() {
				text = " ";
				ObjId = %i;
				class="ParticlePlayerCollSensor";
				groupNum = "-1";
				buttonType = "ToggleButton";
				useMouseEvents = "0";
				position = "8 168";
				extent = "33 30";
				minExtent = "8 2";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiCheckBoxProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "0";
				canSave = "1";
				canSaveDynamicFields = "0";
				};
					
				%sensor.setStateOn(%colSen);
				
				%container.add(%sensor);
				
				%mainStack.add(%container);
				
			}
			
			//Build menu for chain collision shape
			if(%colType $= "Chain")
			{
				%colDen = %obj.getCollisionShapeDensity(%i);
				%colFri = %obj.getCollisionShapeFriction(%i);
				%colRes = %obj.getCollisionShapeRestitution(%i);
				%colSen = %obj.getCollisionShapeIsSensor(%i);
				%colCount = %obj.getChainCollisionShapePointCount(%i);
				%mainContainerExtent = %mainContainerExtent + 203;
				
				%container = new GuiControl() {
				position = "0 0";
				extent = "197 200";
				minExtent = "197 200";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiDefaultBorderProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "1";
				canSave = "1";
				canSaveDynamicFields = "0";
				
					new GuiTextCtrl() {
					text = %colType SPC %i;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 11";
					extent = "182 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Density";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 43";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollDensity";
					text = %colDen;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 43";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Friction";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 75";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollFriction";
					text = %colFri;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 75";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Restitution";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 107";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollRestitution";
					text = %colRes;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 107";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Points:";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 139";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					new GuiTextCtrl() {
					text = %colCount;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "48 139";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Is Sensor";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "32 174";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiBitmapButtonCtrl() {
					bitmap = "^EditorToy/assets/gui/images/DeleteBttn";
					ObjId = %i;
					command = "EditorToy.deleteParticlePlayerColShape("@ %i @ ");";
					bitmapMode = "Stretched";
					autoFitExtents = "0";
					useModifiers = "0";
					useStates = "1";
					masked = "0";
					groupNum = "-1";
					buttonType = "PushButton";
					useMouseEvents = "0";
					position = "160 168";
					extent = "25 25";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiDefaultProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "0";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				};
				
				%sensor = new GuiCheckBoxCtrl() {
				text = " ";
				ObjId = %i;
				class="ParticlePlayerCollSensor";
				groupNum = "-1";
				buttonType = "ToggleButton";
				useMouseEvents = "0";
				position = "8 168";
				extent = "33 30";
				minExtent = "8 2";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiCheckBoxProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "0";
				canSave = "1";
				canSaveDynamicFields = "0";
				};
					
				%sensor.setStateOn(%colSen);
				
				%container.add(%sensor);
				
				%mainStack.add(%container);
				
				
			}
			
			//Build menu for Edge collision shape
			if(%colType $= "Edge")
			{
				%colDen = %obj.getCollisionShapeDensity(%i);
				%colFri = %obj.getCollisionShapeFriction(%i);
				%colRes = %obj.getCollisionShapeRestitution(%i);
				%colSen = %obj.getCollisionShapeIsSensor(%i);
				%mainContainerExtent = %mainContainerExtent + 203;
				
				%container = new GuiControl() {
				position = "0 0";
				extent = "197 200";
				minExtent = "197 200";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiDefaultBorderProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "1";
				canSave = "1";
				canSaveDynamicFields = "0";
				
					new GuiTextCtrl() {
					text = %colType SPC %i;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 11";
					extent = "182 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Density";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 43";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollDensity";
					text = %colDen;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 43";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Friction";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 75";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollFriction";
					text = %colFri;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 75";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				
					new GuiTextCtrl() {
					text = "Restitution";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "8 107";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					new GuiTextEditCtrl() {
					ObjId = %i;
					class = "ParticlePlayerCollRestitution";
					text = %colRes;
					historySize = "0";
					tabComplete = "0";
					sinkAllKeyEvents = "0";
					password = "0";
					passwordMask = "*";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "72 107";
					extent = "110 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextEditProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiTextCtrl() {
					text = "Is Sensor";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "32 174";
					extent = "64 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
					
					new GuiBitmapButtonCtrl() {
					bitmap = "^EditorToy/assets/gui/images/DeleteBttn";
					ObjId = %i;
					command = "EditorToy.deleteParticlePlayerColShape("@ %i @ ");";
					bitmapMode = "Stretched";
					autoFitExtents = "0";
					useModifiers = "0";
					useStates = "1";
					masked = "0";
					groupNum = "-1";
					buttonType = "PushButton";
					useMouseEvents = "0";
					position = "160 168";
					extent = "25 25";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiDefaultProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "0";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				};
				
				%sensor = new GuiCheckBoxCtrl() {
				text = " ";
				ObjId = %i;
				class="ParticlePlayerCollSensor";
				groupNum = "-1";
				buttonType = "ToggleButton";
				useMouseEvents = "0";
				position = "8 168";
				extent = "33 30";
				minExtent = "8 2";
				horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiCheckBoxProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
				isContainer = "0";
				canSave = "1";
				canSaveDynamicFields = "0";
				};
					
				%sensor.setStateOn(%colSen);
				
				%container.add(%sensor);
				
				%mainStack.add(%container);
			}
		}	
	}
	%mainContainer.setExtent(197,%mainContainerExtent);
	ParticlePlayerCollisionShapeStack.add(%mainContainer);
	ParticlePlayerCollisionShapeContainer.setExtent(197,%mainContainerExtent + 150);
	ParticlePlayerCollisionShapeRollout.sizeToContents();
}

function ParticlePlayerCollLocalX::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function ParticlePlayerCollLocalX::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function ParticlePlayerCollLocalY::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function ParticlePlayerCollLocalY::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function ParticlePlayerCollRadius::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function ParticlePlayerCollRadius::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createParticlePlayerCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function EditorToy::createParticlePlayerCircCollision(%this, %rad, %locX, %locY)
{	
	%obj = EditorToy.selParticlePlayer;
	%obj.createCircleCollisionShape(%rad, %locX SPC %locY);
	%this.updateParticlePlayerCollShapeCount();
	%this.updateParticlePlayerCollGui();
}

function ParticlePlayerCollDensity::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function ParticlePlayerCollDensity::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function ParticlePlayerCollFriction::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function ParticlePlayerCollFriction::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function ParticlePlayerCollRestitution::onReturn(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
	if(%text < 0.0)
	{
		%text = 0.0;
	}
	else if(%text > 1.0)
	{
		%text = 1.0;
	}
	%obj.setCollisionShapeRestitution(%objId,%text);
}

function ParticlePlayerCollRestitution::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%text = %this.getText();
		if(%text < 0.0)
	{
		%text = 0.0;
	}
	else if(%text > 1.0)
	{
		%text = 1.0;
	}
	%obj.setCollisionShapeRestitution(%objId,%text);
}

function ParticlePlayerCollSensor::onClick(%this)
{
	%value = %this.getStateOn();
	%obj = EditorToy.selParticlePlayer;
	%objId = %this.ObjId;
	%obj.setCollisionShapeIsSensor(%objId,%value);
}

function EditorToy::deleteParticlePlayerColShape(%this, %collId)
{
	%obj = EditorToy.selParticlePlayer;
	%obj.deleteCollisionShape(%collId);
	%this.updateParticlePlayerCollShapeCount();
	EditorToy.updateParticlePlayerCollGui();
}

//ParticlePlayer Frame
function ParticlePlayerFrame::onAdd(%this)
{
	%text = EditorToy.particlePlayerFrame;
	%this.setText(%text);
}

function ParticlePlayerFrame::update(%this)
{
	%text = EditorToy.particlePlayerFrame;
	%this.setText(%text);
}

function ParticlePlayerFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerFrame(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerFrame(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateParticlePlayerFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerFrame::raiseAmount(%this)
{
	%asset = EditorToy.selParticlePlayer.getImage();
	%image = AssetDatabase.acquireAsset(%asset);
	%frameCount = %image.getFrameCount();
	//ParticlePlayer ImageFrame starts at 0
	%frameCount = %frameCount - 1;
    %value = %this.getText();
	%value++;
	if(%value > %frameCount)
		%value = %frameCount;
	EditorToy.updateParticlePlayerFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Gravity
function ParticlePlayerGravity::onAdd(%this)
{
	%text = EditorToy.particlePlayerGravity;
	%this.setText(%text);
}

function ParticlePlayerGravity::update(%this)
{
	%text = EditorToy.particlePlayerGravity;
	%this.setText(%text);
}

function ParticlePlayerGravity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerGravity(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerGravity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerGravity(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerGravity::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateParticlePlayerGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerGravity::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateParticlePlayerGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Scene Layer
function ParticlePlayerSceneLayer::onAdd(%this)
{
	%text = EditorToy.particlePlayerSceneLayer;
	%this.setText(%text);
}

function ParticlePlayerSceneLayer::update(%this)
{
	%text = EditorToy.particlePlayerSceneLayer;
	%this.setText(%text);
}

function ParticlePlayerSceneLayer::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerSceneLayer(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneLayer::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerSceneLayer(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneLayer::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateParticlePlayerSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneLayer::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateParticlePlayerSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Scene Group
function ParticlePlayerSceneGroup::onAdd(%this)
{
	%text = EditorToy.particlePlayerSceneGroup;
	%this.setText(%text);
}

function ParticlePlayerSceneGroup::update(%this)
{
	%text = EditorToy.particlePlayerSceneGroup;
	%this.setText(%text);
}

function ParticlePlayerSceneGroup::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerSceneGroup(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneGroup::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerSceneGroup(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneGroup::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateParticlePlayerSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerSceneGroup::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateParticlePlayerSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layers
//ParticlePlayer Collision Layer 0
function ParticlePlayerCollLay0::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[0];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay0::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[0];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[0] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 1
function ParticlePlayerCollLay1::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[1];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay1::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[1];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[1] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 2
function ParticlePlayerCollLay2::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[2];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay2::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[2];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[2] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 3
function ParticlePlayerCollLay3::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[3];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay3::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[3];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[3] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 4
function ParticlePlayerCollLay4::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[4];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay4::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[4];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[4] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 5
function ParticlePlayerCollLay5::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[5];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay5::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[5];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[5] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 6
function ParticlePlayerCollLay6::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[6];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay6::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[6];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[6] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 7
function ParticlePlayerCollLay7::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[7];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay7::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[7];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[7] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 8
function ParticlePlayerCollLay8::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[8];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay8::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[8];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[8] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 9
function ParticlePlayerCollLay9::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[9];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay9::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[9];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[9] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 10
function ParticlePlayerCollLay10::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[10];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay10::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[10];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[10] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 11
function ParticlePlayerCollLay11::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[11];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay11::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[11];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[11] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 12
function ParticlePlayerCollLay12::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[12];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay12::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[12];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[12] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 13
function ParticlePlayerCollLay13::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[13];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay13::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[13];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[13] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 14
function ParticlePlayerCollLay14::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[14];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay14::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[14];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[14] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 15
function ParticlePlayerCollLay15::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[15];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay15::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[15];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[15] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 16
function ParticlePlayerCollLay16::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[16];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay16::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[16];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[16] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 17
function ParticlePlayerCollLay7::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[17];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay17::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[17];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[17] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 18
function ParticlePlayerCollLay18::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[18];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay18::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[18];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[18] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 19
function ParticlePlayerCollLay19::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[19];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay19::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[19];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[19] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 20
function ParticlePlayerCollLay20::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[20];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay20::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[20];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[20] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 21
function ParticlePlayerCollLay1::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[21];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay21::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[21];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[21] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 22
function ParticlePlayerCollLay22::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[22];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay22::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[22];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[22] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 23
function ParticlePlayerCollLay3::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[23];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay23::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[23];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[23] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 24
function ParticlePlayerCollLay24::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[24];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay24::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[24];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[24] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 25
function ParticlePlayerCollLay25::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[25];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay25::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[25];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[25] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 26
function ParticlePlayerCollLay26::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[26];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay26::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[26];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[26] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 27
function ParticlePlayerCollLay27::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[27];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay27::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[27];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[27] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 28
function ParticlePlayerCollLay28::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[28];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay28::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[28];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[28] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 29
function ParticlePlayerCollLay29::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[29];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay29::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[29];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[29] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 30
function ParticlePlayerCollLay30::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[30];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay30::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[30];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[30] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Layer 31
function ParticlePlayerCollLay31::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollLayer[31];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay31::update(%this)
{
	%value = EditorToy.particlePlayerCollLayer[31];
	%this.setStateOn(%value);
}

function ParticlePlayerCollLay31::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.particlePlayerCollLayer[31] = %value;
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerCollLayAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.particlePlayerCollLayer[%i] = 1;
	}
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerCollLayNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.particlePlayerCollLayer[%i] = 0;
	}
	EditorToy.stringParticlePlayerCollLayerArray();
	EditorToy.updateParticlePlayer();
}

//-----------------------------------------------------------------------------

//ParticlePlayer Collision Groups
//ParticlePlayer Collision Group 0
function ParticlePlayerCollGroup0::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[0];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup0::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[0];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[0] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 1
function ParticlePlayerCollGroup1::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[1];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup1::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[1];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[1] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 2
function ParticlePlayerCollGroup2::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[2];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup2::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[2];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[2] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 3
function ParticlePlayerCollGroup3::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[3];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup3::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[3];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[3] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 4
function ParticlePlayerCollGroup4::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[4];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup4::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[4];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[4] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 5
function ParticlePlayerCollGroup5::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[5];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup5::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[5];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[5] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 6
function ParticlePlayerCollGroup6::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[6];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup6::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[6];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[6] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 7
function ParticlePlayerCollGroup7::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[7];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup7::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[7];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[7] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 8
function ParticlePlayerCollGroup8::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[8];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup8::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[8];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[8] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 9
function ParticlePlayerCollGroup9::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[9];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup9::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[9];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[9] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 10
function ParticlePlayerCollGroup10::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[10];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup10::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[10];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[10] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 11
function ParticlePlayerCollGroup11::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[11];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup11::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[11];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[11] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 12
function ParticlePlayerCollGroup12::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[12];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup12::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[12];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[12] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 13
function ParticlePlayerCollGroup13::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[13];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup13::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[13];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[13] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 14
function ParticlePlayerCollGroup14::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[14];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup14::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[14];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[14] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 15
function ParticlePlayerCollGroup15::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[15];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup15::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[15];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[15] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 16
function ParticlePlayerCollGroup16::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[16];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup16::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[16];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[16] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 17
function ParticlePlayerCollGroup7::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[17];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup17::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[17];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[17] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 18
function ParticlePlayerCollGroup18::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[18];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup18::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[18];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[18] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 19
function ParticlePlayerCollGroup19::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[19];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup19::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[19];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[19] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 20
function ParticlePlayerCollGroup20::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[20];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup20::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[20];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[20] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 21
function ParticlePlayerCollGroup1::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[21];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup21::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[21];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[21] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 22
function ParticlePlayerCollGroup22::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[22];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup22::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[22];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[22] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 23
function ParticlePlayerCollGroup3::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[23];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup23::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[23];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[23] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 24
function ParticlePlayerCollGroup24::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[24];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup24::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[24];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[24] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 25
function ParticlePlayerCollGroup25::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[25];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup25::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[25];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[25] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 26
function ParticlePlayerCollGroup26::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[26];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup26::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[26];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[26] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 27
function ParticlePlayerCollGroup27::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[27];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup27::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[27];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[27] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 28
function ParticlePlayerCollGroup28::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[28];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup28::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[28];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[28] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 29
function ParticlePlayerCollGroup29::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[29];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup29::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[29];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[29] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 30
function ParticlePlayerCollGroup30::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[30];
	
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup30::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[30];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ParticlePlayerCollGroup[30] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Collision Group 31
function ParticlePlayerCollGroup31::onAdd(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[31];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup31::update(%this)
{
	%value = EditorToy.ParticlePlayerCollGroup[31];
	%this.setStateOn(%value);
}

function ParticlePlayerCollGroup31::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.ParticlePlayerCollGroup[31] = %value;
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerCollGroupAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.ParticlePlayerCollGroup[%i] = 1;
	}
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerCollGroupNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.ParticlePlayerCollGroup[%i] = 0;
	}
	EditorToy.stringParticlePlayerCollGroupArray();
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Source Blend Factor List
function ParticlePlayerSrcBlendList::onAdd(%this)
{
	%this.add( "ZERO"	, 1);
	%this.add( "ONE", 2);
	%this.add( "DST_COLOR" , 3);
	%this.add( "ONE_MINUS_DST_COLOR" , 4);
	%this.add( "SRC_ALPHA" , 5);
	%this.add( "ONE_MINUS_SRC_ALPHA" , 6);
	%this.add( "DST_ALPHA" , 7);
	%this.add( "ONE_MINUS_DST_ALPHA" , 8);
	%this.add( "SRC_ALPHA_SATURATE" , 9);
	
}

function ParticlePlayerSrcBlendList::update(%this)
{
	%value = EditorToy.particlePlayerSrcBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticlePlayerSrcBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerSrcBlend(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Destination Blend Factor List
function ParticlePlayerDstBlendList::onAdd(%this)
{
	%this.add( "ZERO"	, 1);
	%this.add( "ONE", 2);
	%this.add( "SRC_COLOR" , 3);
	%this.add( "ONE_MINUS_SRC_COLOR" , 4);
	%this.add( "SRC_ALPHA" , 5);
	%this.add( "ONE_MINUS_SRC_ALPHA" , 6);
	%this.add( "DST_ALPHA" , 7);
	%this.add( "ONE_MINUS_DST_ALPHA" , 8);
	
}

function ParticlePlayerDstBlendList::update(%this)
{
	%value = EditorToy.particlePlayerDstBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticlePlayerDstBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDstBlend(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Blend Mode
function ParticlePlayerBlendMode::onAdd(%this)
{
	%value = EditorToy.particlePlayerBlendMode;
	%this.setStateOn(%value);
}

function ParticlePlayerBlendMode::update(%this)
{
	%value = EditorToy.particlePlayerBlendMode;
	%this.setStateOn(%value);
}

function ParticlePlayerBlendMode::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerBlendMode(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Alpha Test
function ParticlePlayerAlphaTest::onAdd(%this)
{
	%text = EditorToy.particlePlayerAlphaTest;
	%this.setText(%text);
}

function ParticlePlayerAlphaTest::update(%this)
{
	%text = EditorToy.particlePlayerAlphaTest;
	%this.setText(%text);
}

function ParticlePlayerAlphaTest::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAlphaTest(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAlphaTest::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAlphaTest(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAlphaTest::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 0.1;
	if(%value < 0.0)
		%value = -1.0;
	EditorToy.updateParticlePlayerAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAlphaTest::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 0.1;
	if(%value < 0.0)
		%value = 0.0;
	if(%value > 1)
		%value = 1.0;
	EditorToy.updateParticlePlayerAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerColorSelect::onReturn(%this)
{
	%value = %this.getValue();
	ParticlePlayerColorBlend.setValue(%value);
}

function ParticlePlayerColorBlend::onReturn(%this)
{
	%color = %this.getValue();
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	
	EditorToy.updateParticlePlayerBlendR(%r);
	EditorToy.updateParticlePlayerBlendG(%g);
	EditorToy.updateParticlePlayerBlendB(%b);
	
	EditorToy.updateParticlePlayer();
	
}

//ParticlePlayer Blend A
function ParticlePlayerBlendA::update(%this)
{
	%value = EditorToy.particlePlayerBlendA;
	%this.setValue(%value);
}

function ParticlePlayerBlendA::onReturn(%this)
{
	%value = %this.getValue();
	echo(%value);
	EditorToy.updateParticlePlayerBlendA(%value);
	EditorToy.updateParticlePlayer();
}


//ParticlePlayer FixedAngle
function ParticlePlayerFixedAngle::onAdd(%this)
{
	%value = EditorToy.particlePlayerFixedAngle;
	%this.setStateOn(%value);
}

function ParticlePlayerFixedAngle::update(%this)
{
	%value = EditorToy.particlePlayerFixedAngle;
	%this.setStateOn(%value);
}

function ParticlePlayerFixedAngle::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerFixedAngle(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Angle
function ParticlePlayerAngle::onAdd(%this)
{
	%text = EditorToy.particlePlayerAngle;
	%this.setText(%text);
}

function ParticlePlayerAngle::update(%this)
{
	%text = EditorToy.particlePlayerAngle;
	%this.setText(%text);
}

function ParticlePlayerAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngle(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngle(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Angle Vel
function ParticlePlayerAngularVel::onAdd(%this)
{
	%text = EditorToy.particlePlayerAngVel;
	%this.setText(%text);
}

function ParticlePlayerAngularVel::update(%this)
{
	%text = EditorToy.particlePlayerAngVel;
	%this.setText(%text);
}

function ParticlePlayerAngularVel::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngVel(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularVel::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngVel(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularVel::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularVel::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Angle Damp
function ParticlePlayerAngularDamp::onAdd(%this)
{
	%text = EditorToy.particlePlayerAngDamp;
	%this.setText(%text);
}

function ParticlePlayerAngularDamp::update(%this)
{
	%text = EditorToy.particlePlayerAngDamp;
	%this.setText(%text);
}

function ParticlePlayerAngularDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngDamp(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerAngDamp(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerAngularDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Linear VelX
function ParticlePlayerLinearVelX::onAdd(%this)
{
	%text = EditorToy.particlePlayerLinVelX;
	%this.setText(%text);
}

function ParticlePlayerLinearVelX::update(%this)
{
	%text = EditorToy.particlePlayerLinVelX;
	%this.setText(%text);
}

function ParticlePlayerLinearVelX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelX(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelX(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Linear VelY
function ParticlePlayerLinearVelY::onAdd(%this)
{
	%text = EditorToy.particlePlayerLinVelY;
	%this.setText(%text);
}

function ParticlePlayerLinearVelY::update(%this)
{
	%text = EditorToy.particlePlayerLinVelY;
	%this.setText(%text);
}

function ParticlePlayerLinearVelY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelY(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelY(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer LinearVel PolAngle
function ParticlePlayerLinearVelPolAngle::onAdd(%this)
{
	%text = EditorToy.particlePlayerLinVelPolAngle;
	%this.setText(%text);
}

function ParticlePlayerLinearVelPolAngle::update(%this)
{
	%text = EditorToy.particlePlayerLinVelPolAngle;
	%this.setText(%text);
}

function ParticlePlayerLinearVelPolAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelPolAngle(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelPolAngle(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer LinearVel PolSpeed
function ParticlePlayerLinearVelPolSpeed::onAdd(%this)
{
	%text = EditorToy.particlePlayerLinVelPolSpeed;
	%this.setText(%text);
}

function ParticlePlayerLinearVelPolSpeed::update(%this)
{
	%text = EditorToy.particlePlayerLinVelPolSpeed;
	%this.setText(%text);
}

function ParticlePlayerLinearVelPolSpeed::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelPolSpeed(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolSpeed::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinVelPolSpeed(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolSpeed::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearVelPolSpeed::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Linear Damp
function ParticlePlayerLinearDamp::onAdd(%this)
{
	%text = EditorToy.particlePlayerLinDamp;
	%this.setText(%text);
}

function ParticlePlayerLinearDamp::update(%this)
{
	%text = EditorToy.particlePlayerLinDamp;
	%this.setText(%text);
}

function ParticlePlayerLinearDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinDamp(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerLinDamp(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerLinearDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Default Density
function ParticlePlayerDefDensity::onAdd(%this)
{
	%text = EditorToy.particlePlayerDefDensity;
	%this.setText(%text);
}

function ParticlePlayerDefDensity::update(%this)
{
	%text = EditorToy.particlePlayerDefDensity;
	%this.setText(%text);
}

function ParticlePlayerDefDensity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefDensity(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefDensity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefDensity(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefDensity::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefDensity::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Default Friction
function ParticlePlayerDefFriction::onAdd(%this)
{
	%text = EditorToy.particlePlayerDefFriction;
	%this.setText(%text);
}

function ParticlePlayerDefFriction::update(%this)
{
	%text = EditorToy.particlePlayerDefFriction;
	%this.setText(%text);
}

function ParticlePlayerDefFriction::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefFriction(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefFriction::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefFriction(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefFriction::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefFriction::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Default Restitution
function ParticlePlayerDefRestitution::onAdd(%this)
{
	%text = EditorToy.particlePlayerDefRestitution;
	%this.setText(%text);
}

function ParticlePlayerDefRestitution::update(%this)
{
	%text = EditorToy.particlePlayerDefRestitution;
	%this.setText(%text);
}

function ParticlePlayerDefRestitution::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefRestitution(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefRestitution::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerDefRestitution(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefRestitution::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateParticlePlayerDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerDefRestitution::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateParticlePlayerDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Coll Suppress
function ParticlePlayerCollSuppress::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollSupp;
	%this.setStateOn(%value);
}

function ParticlePlayerCollSuppress::update(%this)
{
	%value = EditorToy.particlePlayerCollSupp;
	%this.setStateOn(%value);
}

function ParticlePlayerCollSuppress::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerCollSupp(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Coll One Way
function ParticlePlayerCollOneWay::onAdd(%this)
{
	%value = EditorToy.particlePlayerCollOne;
	%this.setStateOn(%value);
}

function ParticlePlayerCollOneWay::update(%this)
{
	%value = EditorToy.particlePlayerCollOne;
	%this.setStateOn(%value);
}

function ParticlePlayerCollOneWay::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateParticlePlayerCollOne(%value);
	EditorToy.updateParticlePlayer();
}


//ParticlePlayer Name
function ParticlePlayerName::onAdd(%this)
{
	%text = EditorToy.particlePlayerName;
	%this.setText(%text);
}

function ParticlePlayerName::update(%this)
{
	%text = EditorToy.particlePlayerName;
	%this.setText(%text);
}

function ParticlePlayerName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerName(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerName(%value);
	EditorToy.updateParticlePlayer();
}

//ParticlePlayer Class
function ParticlePlayerClass::onAdd(%this)
{
	%text = EditorToy.particlePlayerClass;
	%this.setText(%text);
}

function ParticlePlayerClass::update(%this)
{
	%text = EditorToy.particlePlayerClass;
	%this.setText(%text);
}

function ParticlePlayerClass::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerClass(%value);
	EditorToy.updateParticlePlayer();
}

function ParticlePlayerClass::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateParticlePlayerClass(%value);
	EditorToy.updateParticlePlayer();
}

