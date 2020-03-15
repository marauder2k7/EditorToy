//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//Sprite functions 
function EditorToy::loadSpriteAsset(%this)
{
	%mName = EditorToy.moduleName;
	%SpriteAssetLoad = new OpenFileDialog();
	%SpriteAssetLoad.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%SpriteAssetLoad.Title = "Choose ImageAsset for Sprite";
	%SpriteAssetLoad.MustExist = true;
	%SpriteAssetLoad.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%SpriteAssetLoad.ChangePath = false;
	
	if(%SpriteAssetLoad.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %SpriteAssetLoad.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%spriteBase = fileBase(%defaultBase);
		%SpriteAssetLoad.delete();
		%this.createSprite(%spriteBase);
	}
	if(isObject(%SpriteAssetLoad))
	{
		%SpriteAssetLoad.delete();
	}
}

function EditorToy::createSprite(%this, %assetFile)
{

	//Spawn sprites at camera position
	%scene = EditorToy.activeScene;
	%pos = SandboxWindow.getCameraPosition();
	%mName = EditorToy.moduleName;
	%spriteBase = %assetFile;
	// Create the sprite.
    %object = new Sprite();
    %object.Size = "10 10";
	%object.SetBodyType( static );
	%object.Position = %pos;
    %object.Image = %mName @ ":" @%spriteBase;
	//Objects in editor are inactive until unleashed into
	//the hell we have created for our own enjoyment >:)
	//in other words until scene is unpaused...
	%object.setActive(0);
	%scene.add(%object);
}

function EditorToy::updateSprite(%this)
{
	//Initialize our data
	%obj = EditorToy.selSprite;
	%name = EditorToy.spriteName;
	%class = EditorToy.spriteClass;
	%posX = EditorToy.spritePosX;
	%posY = EditorToy.spritePosY;
	%width = EditorToy.spriteWidth;
	%height = EditorToy.spriteHeight;
	%flipX = EditorToy.spriteFlipX;
	%flipY = EditorToy.spriteFlipY;
	%size = %width SPC %height;
	%pos = %posX SPC %posY;
	%body = EditorToy.spriteBody;
	%ang = EditorToy.spriteAngle;
	%fixAng = EditorToy.spriteFixedAngle;
	%angDam = EditorToy.spriteAngDamp;
	%angVel = EditorToy.spriteAngVel;
	%linVelX = EditorToy.spriteLinVelX;
	%linVelY = EditorToy.spriteLinVelY;
	%linVelPolAng = EditorToy.spriteLinPolAngle;
	%linVelPolSpeed = EditorToy.spriteLinPolSpeed;
	%linDam = EditorToy.spriteLinDamp;
	%defDen = EditorToy.spriteDefDensity;
	%defFri = EditorToy.spriteDefFriction;
	%defRes = EditorToy.spriteDefRestitution;
	%collSupp = EditorToy.spriteCollSupp;
	%collOne = EditorToy.spriteCollOne;
	%colShape = EditorToy.polyListPosLocal;
	%frame = EditorToy.spriteFrame;
	%grav = EditorToy.spriteGravity;
	%sceneLay = EditorToy.spriteSceneLayer;
	%sceneGroup = EditorToy.spriteSceneGroup;
	%collLayers = EditorToy.spriteCollLayers;
	%collGroups = EditorToy.spriteCollGroups;
	%blendMode = EditorToy.spriteBlendMode;
	%srcBlend = EditorToy.spriteSrcBlend;
	%dstBlend = EditorToy.spriteDstBlend;
	%alphaTest = EditorToy.spriteAlphaTest;
	%blendR = EditorToy.spriteBlendR;
	%blendG = EditorToy.spriteBlendG;
	%blendB = EditorToy.spriteBlendB;
	%blendA = EditorToy.spriteBlendA;
	
	//set data to selected sprite
	%obj.setName(%name);
	%obj.class = %class;
	%obj.setPosition(%pos);
	%obj.setSize(%size);
	%obj.setFlipX(%flipX);
	%obj.setFlipY(%flipY);
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
	
	%obj.setImageFrame(%frame);
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
//Update Sprite Values
function EditorToy::updateSelSprite(%this, %obj)
{
	%this.selSprite = %obj;
}
function EditorToy::updateSpritePosX(%this, %value)
{
	%this.spritePosX = %value;
}

function EditorToy::updateSpritePosY(%this, %value)
{
	%this.spritePosY = %value;
}

function EditorToy::updateSpriteWidth(%this, %value)
{
	%this.spriteWidth = %value;
}

function EditorToy::updateSpriteHeight(%this, %value)
{
	%this.spriteHeight = %value;
}

function EditorToy::updateSpriteFlipX(%this, %value)
{
	%this.spriteFlipX = %value;
}

function EditorToy::updateSpriteFlipY(%this, %value)
{
	%this.spriteFlipY = %value;
}

function EditorToy::updateSpriteName(%this, %value)
{
	%this.spriteName = %value;
}

function EditorToy::updateSpriteClass(%this, %value)
{
	%this.spriteClass = %value;
}

function EditorToy::updateSpriteAngle(%this, %value)
{
	%this.spriteAngle = %value;
}

function EditorToy::updateSpriteFixedAngle(%this, %value)
{
	%this.spriteFixedAngle = %value;
}

function EditorToy::updateSpriteAngDamp(%this, %value)
{
	%this.spriteAngDamp = %value;
}

function EditorToy::updateSpriteAngVel(%this, %value)
{
	%this.spriteAngVel = %value;
}

function EditorToy::updateSpriteLinVelX(%this, %value)
{
	%this.spriteLinVelX = %value;
}

function EditorToy::updateSpriteLinVelY(%this, %value)
{
	%this.spriteLinVelY = %value;
}

function EditorToy::updateSpriteLinVelPolAngle(%this, %value)
{
	%this.spriteLinVelPolAngle = %value;
}

function EditorToy::updateSpriteLinVelPolSpeed(%this, %value)
{
	%this.spriteLinVelPolSpeed = %value;
}

function EditorToy::updateSpriteLinDamp(%this, %value)
{
	%this.spriteLinDamp = %value;
}

function EditorToy::updateSpriteDefDensity(%this, %value)
{
	%this.spriteDefDensity = %value;
}

function EditorToy::updateSpriteDefFriction(%this, %value)
{
	%this.spriteDefFriction = %value;
}

function EditorToy::updateSpriteDefRestitution(%this, %value)
{
	%this.spriteDefRestitution = %value;
}

function EditorToy::updateSpriteCollSupp(%this, %value)
{
	%this.spriteCollSupp = %value;
}

function EditorToy::updateSpriteCollOne(%this, %value)
{
	%this.spriteCollOne = %value;
}

function EditorToy::updateSpriteBody(%this, %value)
{
	%this.spriteBody = %value;
}

function EditorToy::updateSpriteCollShapeCount(%this)
{
	%obj = EditorToy.selSprite;
	%colNum = %obj.getCollisionShapeCount();
	%this.spriteCollShapeCount = %colNum;
}

function EditorToy::updateSpriteFrame(%this, %value)
{
	%this.spriteFrame = %value;
}

function EditorToy::updateSpriteGravity(%this, %value)
{
	%this.spriteGravity = %value;
}

function EditorToy::updateSpriteSceneLayer(%this, %value)
{
	%this.spriteSceneLayer = %value;
}

function EditorToy::updateSpriteSceneGroup(%this, %value)
{
	%this.spriteSceneGroup = %value;
}

function EditorToy::updateSpriteCollLayers(%this, %value)
{
	%this.spriteCollLayers = %value;
}

function EditorToy::updateSpriteCollGroups(%this, %value)
{
	%this.spriteCollGroups = %value;
}

function EditorToy::updateSpriteAlphaTest(%this, %value)
{
	%this.spriteAlphaTest = %value;
}

function EditorToy::updateSpriteBlendMode(%this, %value)
{
	%this.spriteBlendMode = %value;
}

function EditorToy::updateSpriteSrcBlend(%this, %value)
{
	%this.spriteSrcBlend = %value;
}

function EditorToy::updateSpriteDstBlend(%this, %value)
{
	%this.spriteDstBlend = %value;
}

function EditorToy::updateSpriteBlendR(%this, %value)
{
	%this.spriteBlendR = %value;
}

function EditorToy::updateSpriteBlendG(%this, %value)
{
	%this.spriteBlendG = %value;
}

function EditorToy::updateSpriteBlendB(%this, %value)
{
	%this.spriteBlendB = %value;
}

function EditorToy::updateSpriteBlendA(%this, %value)
{
	%this.spriteBlendA = %value;
}

//Load arrays for collision layers and groups
function EditorToy::loadSpriteCollLayerArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.spriteCollLayer[0] = false;
	EditorToy.spriteCollLayer[1] = false;
	EditorToy.spriteCollLayer[2] = false;
	EditorToy.spriteCollLayer[3] = false;
	EditorToy.spriteCollLayer[4] = false;
	EditorToy.spriteCollLayer[5] = false;
	EditorToy.spriteCollLayer[6] = false;
	EditorToy.spriteCollLayer[7] = false;
	EditorToy.spriteCollLayer[8] = false;
	EditorToy.spriteCollLayer[9] = false;
	EditorToy.spriteCollLayer[10] = false;
	EditorToy.spriteCollLayer[11] = false;
	EditorToy.spriteCollLayer[12] = false;
	EditorToy.spriteCollLayer[13] = false;
	EditorToy.spriteCollLayer[14] = false;
	EditorToy.spriteCollLayer[15] = false;
	EditorToy.spriteCollLayer[16] = false;
	EditorToy.spriteCollLayer[17] = false;
	EditorToy.spriteCollLayer[18] = false;
	EditorToy.spriteCollLayer[19] = false;
	EditorToy.spriteCollLayer[20] = false;
	EditorToy.spriteCollLayer[21] = false;
	EditorToy.spriteCollLayer[22] = false;
	EditorToy.spriteCollLayer[23] = false;
	EditorToy.spriteCollLayer[24] = false;
	EditorToy.spriteCollLayer[25] = false;
	EditorToy.spriteCollLayer[26] = false;
	EditorToy.spriteCollLayer[27] = false;
	EditorToy.spriteCollLayer[28] = false;
	EditorToy.spriteCollLayer[29] = false;
	EditorToy.spriteCollLayer[30] = false;
	EditorToy.spriteCollLayer[31] = false;
	
	%collLayers = EditorToy.spriteCollLayers;
	%count = getWordCount( %collLayers);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collLayers,%i);
		EditorToy.spriteCollLayer[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	SpriteCollLay0.update();
	SpriteCollLay1.update();
	SpriteCollLay2.update();
	SpriteCollLay3.update();
	SpriteCollLay4.update();
	SpriteCollLay5.update();
	SpriteCollLay6.update();
	SpriteCollLay7.update();
	SpriteCollLay8.update();
	SpriteCollLay9.update();
	SpriteCollLay10.update();
	SpriteCollLay11.update();
	SpriteCollLay12.update();
	SpriteCollLay13.update();
	SpriteCollLay14.update();
	SpriteCollLay15.update();
	SpriteCollLay16.update();
	SpriteCollLay17.update();
	SpriteCollLay18.update();
	SpriteCollLay19.update();
	SpriteCollLay20.update();
	SpriteCollLay21.update();
	SpriteCollLay22.update();
	SpriteCollLay23.update();
	SpriteCollLay24.update();
	SpriteCollLay25.update();
	SpriteCollLay26.update();
	SpriteCollLay27.update();
	SpriteCollLay28.update();
	SpriteCollLay29.update();
	SpriteCollLay30.update();
	SpriteCollLay31.update();
	
}

function EditorToy::stringSpriteCollLayerArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.spriteCollLayer[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.spriteCollLayer[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the sprites settings
	EditorToy.updateSpriteCollLayers(%layerString);
	//Update the gui
	EditorToy.loadSpriteCollLayerArray();
}

function EditorToy::loadSpriteCollGroupArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.spriteCollGroup[0] = false;
	EditorToy.spriteCollGroup[1] = false;
	EditorToy.spriteCollGroup[2] = false;
	EditorToy.spriteCollGroup[3] = false;
	EditorToy.spriteCollGroup[4] = false;
	EditorToy.spriteCollGroup[5] = false;
	EditorToy.spriteCollGroup[6] = false;
	EditorToy.spriteCollGroup[7] = false;
	EditorToy.spriteCollGroup[8] = false;
	EditorToy.spriteCollGroup[9] = false;
	EditorToy.spriteCollGroup[10] = false;
	EditorToy.spriteCollGroup[11] = false;
	EditorToy.spriteCollGroup[12] = false;
	EditorToy.spriteCollGroup[13] = false;
	EditorToy.spriteCollGroup[14] = false;
	EditorToy.spriteCollGroup[15] = false;
	EditorToy.spriteCollGroup[16] = false;
	EditorToy.spriteCollGroup[17] = false;
	EditorToy.spriteCollGroup[18] = false;
	EditorToy.spriteCollGroup[19] = false;
	EditorToy.spriteCollGroup[20] = false;
	EditorToy.spriteCollGroup[21] = false;
	EditorToy.spriteCollGroup[22] = false;
	EditorToy.spriteCollGroup[23] = false;
	EditorToy.spriteCollGroup[24] = false;
	EditorToy.spriteCollGroup[25] = false;
	EditorToy.spriteCollGroup[26] = false;
	EditorToy.spriteCollGroup[27] = false;
	EditorToy.spriteCollGroup[28] = false;
	EditorToy.spriteCollGroup[29] = false;
	EditorToy.spriteCollGroup[30] = false;
	EditorToy.spriteCollGroup[31] = false;
	
	%collGroups = EditorToy.spriteCollGroups;
	%count = getWordCount( %collGroups);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collGroups,%i);
		EditorToy.spriteCollGroup[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	SpriteCollGroup0.update();
	SpriteCollGroup1.update();
	SpriteCollGroup2.update();
	SpriteCollGroup3.update();
	SpriteCollGroup4.update();
	SpriteCollGroup5.update();
	SpriteCollGroup6.update();
	SpriteCollGroup7.update();
	SpriteCollGroup8.update();
	SpriteCollGroup9.update();
	SpriteCollGroup10.update();
	SpriteCollGroup11.update();
	SpriteCollGroup12.update();
	SpriteCollGroup13.update();
	SpriteCollGroup14.update();
	SpriteCollGroup15.update();
	SpriteCollGroup16.update();
	SpriteCollGroup17.update();
	SpriteCollGroup18.update();
	SpriteCollGroup19.update();
	SpriteCollGroup20.update();
	SpriteCollGroup21.update();
	SpriteCollGroup22.update();
	SpriteCollGroup23.update();
	SpriteCollGroup24.update();
	SpriteCollGroup25.update();
	SpriteCollGroup26.update();
	SpriteCollGroup27.update();
	SpriteCollGroup28.update();
	SpriteCollGroup29.update();
	SpriteCollGroup30.update();
	SpriteCollGroup31.update();
	
}

function EditorToy::stringSpriteCollGroupArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.spriteCollGroup[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.spriteCollGroup[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the sprites settings
	EditorToy.updateSpriteCollGroups(%layerString);
	//Update the gui
	EditorToy.loadSpriteCollGroupArray();
}

//-----------------------------------------------------------------------------
//Update Sprite Menu
//Sprite Pos X
function SpritePosX::onAdd(%this)
{
	%text = EditorToy.spritePosX;
	%this.setText(%text);
}

function SpritePosX::update(%this)
{
	%text = EditorToy.spritePosX;
	%this.setText(%text);
}

function SpritePosX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpritePosX(%value);
	EditorToy.updateSprite();
}

function SpritePosX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpritePosX(%value);
	EditorToy.updateSprite();
}

function SpritePosX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpritePosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpritePosX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpritePosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Y Pos
function SpritePosY::onAdd(%this)
{
	%text = EditorToy.spritePosY;
	%this.setText(%text);
}

function SpritePosY::update(%this)
{
	%text = EditorToy.spritePosY;
	%this.setText(%text);
}

function SpritePosY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpritePosY(%value);
	EditorToy.updateSprite();
}

function SpritePosY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpritePosY(%value);
	EditorToy.updateSprite();
}

function SpritePosY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpritePosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpritePosY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpritePosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Width
function SpriteWidth::onAdd(%this)
{
	%text = EditorToy.spriteWidth;
	%this.setText(%text);
}

function SpriteWidth::update(%this)
{
	%text = EditorToy.spriteWidth;
	%this.setText(%text);
}

function SpriteWidth::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteWidth(%value);
	EditorToy.updateSprite();
}

function SpriteWidth::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteWidth(%value);
	EditorToy.updateSprite();
}

function SpriteWidth::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteWidth::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Height
function SpriteHeight::onAdd(%this)
{
	%text = EditorToy.spriteHeight;
	%this.setText(%text);
}

function SpriteHeight::update(%this)
{
	%text = EditorToy.spriteHeight;
	%this.setText(%text);
}

function SpriteHeight::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteHeight(%value);
	EditorToy.updateSprite();
}

function SpriteHeight::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteHeight(%value);
	EditorToy.updateSprite();
}

function SpriteHeight::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteHeight::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite FlipX
function SpriteFlipX::onAdd(%this)
{
	%value = EditorToy.spriteFlipX;
	%this.setStateOn(%value);
}

function SpriteFlipX::update(%this)
{
	%value = EditorToy.spriteFlipX;
	%this.setStateOn(%value);
}

function SpriteFlipX::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteFlipX(%value);
	EditorToy.updateSprite();
}

//Sprite FlipY
function SpriteFlipY::onAdd(%this)
{
	%value = EditorToy.spriteFlipY;
	%this.setStateOn(%value);
}

function SpriteFlipY::update(%this)
{
	%value = EditorToy.spriteFlipY;
	%this.setStateOn(%value);
}

function SpriteFlipY::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteFlipY(%value);
	EditorToy.updateSprite();
}

//Sprite Body Type
function SpriteBodyList::onAdd(%this)
{
	%this.add( "Static", 1);
	%this.add( "Dynamic", 2);
	%this.add( "Kinematic", 3);
}

function SpriteBodyList::update(%this)
{
	%value = EditorToy.spriteBody;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function SpriteBodyList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateSpriteBody(%value);
	EditorToy.updateSprite();
}

//Collision Shapes gui
function EditorToy::updateSpriteCollGui(%this)
{
	if(isObject (SpriteMainCollContainer))
	{
		SpriteCollisionShapeStack.remove(SpriteMainCollContainer);
	}
	
	%obj = EditorToy.selSprite;
	%colNum = EditorToy.spriteCollShapeCount;
	SpriteCollisionShapeContainer.setExtent(197,150);
	
	%mainContainer = new GuiControl(SpriteMainCollContainer){
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
	
	if(%colNum == 0)
		return;
	if(%colNum > 0)
	{	
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
						class = "SpriteCollDensity";
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
						class = "SpriteCollFriction";
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
						class = "SpriteCollRestitution";
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
						class = "SpriteCollRadius";
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
						class = "SpriteCollLocalX";
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
						class = "SpriteCollLocalY";
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
						command = "EditorToy.deleteSpriteColShape("@ %i @ ");";
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
				class="SpriteCollSensor";
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
					class = "SpriteCollDensity";
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
					class = "SpriteCollFriction";
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
					class = "SpriteCollRestitution";
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
					command = "EditorToy.deleteSpriteColShape("@ %i @ ");";
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
				class="SpriteCollSensor";
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
					class = "SpriteCollDensity";
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
					class = "SpriteCollFriction";
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
					class = "SpriteCollRestitution";
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
					command = "EditorToy.deleteSpriteColShape("@ %i @ ");";
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
				class="SpriteCollSensor";
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
					class = "SpriteCollDensity";
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
					class = "SpriteCollFriction";
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
					class = "SpriteCollRestitution";
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
					command = "EditorToy.deleteSpriteColShape("@ %i @ ");";
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
				class="SpriteCollSensor";
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
	SpriteCollisionShapeStack.add(%mainContainer);
	SpriteCollisionShapeContainer.setExtent(197,%mainContainerExtent + 150);
	SpriteCollisionShapeRollout.sizeToContents();
}

function SpriteCollLocalX::onReturn(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function SpriteCollLocalX::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function SpriteCollLocalY::onReturn(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function SpriteCollLocalY::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function SpriteCollRadius::onReturn(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function SpriteCollRadius::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createSpriteCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function EditorToy::createSpriteCircCollision(%this, %rad, %locX, %locY)
{	
	%obj = EditorToy.selSprite;
	%obj.createCircleCollisionShape(%rad, %locX SPC %locY);
	%this.updateSpriteCollShapeCount();
	%this.updateSpriteCollGui();
}

function SpriteCollDensity::onReturn(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function SpriteCollDensity::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function SpriteCollFriction::onReturn(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function SpriteCollFriction::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function SpriteCollRestitution::onReturn(%this)
{
	%obj = EditorToy.selSprite;
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

function SpriteCollRestitution::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selSprite;
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

function SpriteCollSensor::onClick(%this)
{
	%value = %this.getStateOn();
	%obj = EditorToy.selSprite;
	%objId = %this.ObjId;
	%obj.setCollisionShapeIsSensor(%objId,%value);
}

function EditorToy::deleteSpriteColShape(%this, %collId)
{
	%obj = EditorToy.selSprite;
	%obj.deleteCollisionShape(%collId);
	%this.updateSpriteCollShapeCount();
	EditorToy.updateSpriteCollGui();
}

//Sprite Frame
function SpriteFrame::onAdd(%this)
{
	%text = EditorToy.spriteFrame;
	%this.setText(%text);
}

function SpriteFrame::update(%this)
{
	%text = EditorToy.spriteFrame;
	%this.setText(%text);
}

function SpriteFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteFrame(%value);
	EditorToy.updateSprite();
}

function SpriteFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteFrame(%value);
	EditorToy.updateSprite();
}

function SpriteFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateSpriteFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteFrame::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateSpriteFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Gravity
function SpriteGravity::onAdd(%this)
{
	%text = EditorToy.spriteGravity;
	%this.setText(%text);
}

function SpriteGravity::update(%this)
{
	%text = EditorToy.spriteGravity;
	%this.setText(%text);
}

function SpriteGravity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteGravity(%value);
	EditorToy.updateSprite();
}

function SpriteGravity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteGravity(%value);
	EditorToy.updateSprite();
}

function SpriteGravity::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateSpriteGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteGravity::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateSpriteGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Scene Layer
function SpriteSceneLayer::onAdd(%this)
{
	%text = EditorToy.spriteSceneLayer;
	%this.setText(%text);
}

function SpriteSceneLayer::update(%this)
{
	%text = EditorToy.spriteSceneLayer;
	%this.setText(%text);
}

function SpriteSceneLayer::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteSceneLayer(%value);
	EditorToy.updateSprite();
}

function SpriteSceneLayer::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteSceneLayer(%value);
	EditorToy.updateSprite();
}

function SpriteSceneLayer::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateSpriteSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteSceneLayer::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateSpriteSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Scene Group
function SpriteSceneGroup::onAdd(%this)
{
	%text = EditorToy.spriteSceneGroup;
	%this.setText(%text);
}

function SpriteSceneGroup::update(%this)
{
	%text = EditorToy.spriteSceneGroup;
	%this.setText(%text);
}

function SpriteSceneGroup::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteSceneGroup(%value);
	EditorToy.updateSprite();
}

function SpriteSceneGroup::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteSceneGroup(%value);
	EditorToy.updateSprite();
}

function SpriteSceneGroup::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateSpriteSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteSceneGroup::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateSpriteSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Collision Layers
//Sprite Collision Layer 0
function SpriteCollLay0::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[0];
	
	%this.setStateOn(%value);
}

function SpriteCollLay0::update(%this)
{
	%value = EditorToy.spriteCollLayer[0];
	%this.setStateOn(%value);
}

function SpriteCollLay0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[0] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 1
function SpriteCollLay1::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[1];
	
	%this.setStateOn(%value);
}

function SpriteCollLay1::update(%this)
{
	%value = EditorToy.spriteCollLayer[1];
	%this.setStateOn(%value);
}

function SpriteCollLay1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[1] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 2
function SpriteCollLay2::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[2];
	
	%this.setStateOn(%value);
}

function SpriteCollLay2::update(%this)
{
	%value = EditorToy.spriteCollLayer[2];
	%this.setStateOn(%value);
}

function SpriteCollLay2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[2] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 3
function SpriteCollLay3::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[3];
	
	%this.setStateOn(%value);
}

function SpriteCollLay3::update(%this)
{
	%value = EditorToy.spriteCollLayer[3];
	%this.setStateOn(%value);
}

function SpriteCollLay3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[3] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 4
function SpriteCollLay4::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[4];
	
	%this.setStateOn(%value);
}

function SpriteCollLay4::update(%this)
{
	%value = EditorToy.spriteCollLayer[4];
	%this.setStateOn(%value);
}

function SpriteCollLay4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[4] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 5
function SpriteCollLay5::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[5];
	
	%this.setStateOn(%value);
}

function SpriteCollLay5::update(%this)
{
	%value = EditorToy.spriteCollLayer[5];
	%this.setStateOn(%value);
}

function SpriteCollLay5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[5] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 6
function SpriteCollLay6::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[6];
	
	%this.setStateOn(%value);
}

function SpriteCollLay6::update(%this)
{
	%value = EditorToy.spriteCollLayer[6];
	%this.setStateOn(%value);
}

function SpriteCollLay6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[6] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 7
function SpriteCollLay7::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[7];
	
	%this.setStateOn(%value);
}

function SpriteCollLay7::update(%this)
{
	%value = EditorToy.spriteCollLayer[7];
	%this.setStateOn(%value);
}

function SpriteCollLay7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[7] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 8
function SpriteCollLay8::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[8];
	
	%this.setStateOn(%value);
}

function SpriteCollLay8::update(%this)
{
	%value = EditorToy.spriteCollLayer[8];
	%this.setStateOn(%value);
}

function SpriteCollLay8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[8] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 9
function SpriteCollLay9::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[9];
	
	%this.setStateOn(%value);
}

function SpriteCollLay9::update(%this)
{
	%value = EditorToy.spriteCollLayer[9];
	%this.setStateOn(%value);
}

function SpriteCollLay9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[9] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 10
function SpriteCollLay10::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[10];
	
	%this.setStateOn(%value);
}

function SpriteCollLay10::update(%this)
{
	%value = EditorToy.spriteCollLayer[10];
	%this.setStateOn(%value);
}

function SpriteCollLay10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[10] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 11
function SpriteCollLay11::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[11];
	
	%this.setStateOn(%value);
}

function SpriteCollLay11::update(%this)
{
	%value = EditorToy.spriteCollLayer[11];
	%this.setStateOn(%value);
}

function SpriteCollLay11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[11] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 12
function SpriteCollLay12::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[12];
	
	%this.setStateOn(%value);
}

function SpriteCollLay12::update(%this)
{
	%value = EditorToy.spriteCollLayer[12];
	%this.setStateOn(%value);
}

function SpriteCollLay12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[12] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 13
function SpriteCollLay13::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[13];
	
	%this.setStateOn(%value);
}

function SpriteCollLay13::update(%this)
{
	%value = EditorToy.spriteCollLayer[13];
	%this.setStateOn(%value);
}

function SpriteCollLay13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[13] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 14
function SpriteCollLay14::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[14];
	
	%this.setStateOn(%value);
}

function SpriteCollLay14::update(%this)
{
	%value = EditorToy.spriteCollLayer[14];
	%this.setStateOn(%value);
}

function SpriteCollLay14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[14] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 15
function SpriteCollLay15::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[15];
	
	%this.setStateOn(%value);
}

function SpriteCollLay15::update(%this)
{
	%value = EditorToy.spriteCollLayer[15];
	%this.setStateOn(%value);
}

function SpriteCollLay15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[15] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 16
function SpriteCollLay16::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[16];
	
	%this.setStateOn(%value);
}

function SpriteCollLay16::update(%this)
{
	%value = EditorToy.spriteCollLayer[16];
	%this.setStateOn(%value);
}

function SpriteCollLay16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[16] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 17
function SpriteCollLay7::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[17];
	
	%this.setStateOn(%value);
}

function SpriteCollLay17::update(%this)
{
	%value = EditorToy.spriteCollLayer[17];
	%this.setStateOn(%value);
}

function SpriteCollLay17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[17] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 18
function SpriteCollLay18::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[18];
	
	%this.setStateOn(%value);
}

function SpriteCollLay18::update(%this)
{
	%value = EditorToy.spriteCollLayer[18];
	%this.setStateOn(%value);
}

function SpriteCollLay18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[18] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 19
function SpriteCollLay19::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[19];
	
	%this.setStateOn(%value);
}

function SpriteCollLay19::update(%this)
{
	%value = EditorToy.spriteCollLayer[19];
	%this.setStateOn(%value);
}

function SpriteCollLay19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[19] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 20
function SpriteCollLay20::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[20];
	
	%this.setStateOn(%value);
}

function SpriteCollLay20::update(%this)
{
	%value = EditorToy.spriteCollLayer[20];
	%this.setStateOn(%value);
}

function SpriteCollLay20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[20] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 21
function SpriteCollLay1::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[21];
	
	%this.setStateOn(%value);
}

function SpriteCollLay21::update(%this)
{
	%value = EditorToy.spriteCollLayer[21];
	%this.setStateOn(%value);
}

function SpriteCollLay21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[21] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 22
function SpriteCollLay22::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[22];
	
	%this.setStateOn(%value);
}

function SpriteCollLay22::update(%this)
{
	%value = EditorToy.spriteCollLayer[22];
	%this.setStateOn(%value);
}

function SpriteCollLay22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[22] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 23
function SpriteCollLay3::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[23];
	
	%this.setStateOn(%value);
}

function SpriteCollLay23::update(%this)
{
	%value = EditorToy.spriteCollLayer[23];
	%this.setStateOn(%value);
}

function SpriteCollLay23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[23] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 24
function SpriteCollLay24::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[24];
	
	%this.setStateOn(%value);
}

function SpriteCollLay24::update(%this)
{
	%value = EditorToy.spriteCollLayer[24];
	%this.setStateOn(%value);
}

function SpriteCollLay24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[24] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 25
function SpriteCollLay25::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[25];
	
	%this.setStateOn(%value);
}

function SpriteCollLay25::update(%this)
{
	%value = EditorToy.spriteCollLayer[25];
	%this.setStateOn(%value);
}

function SpriteCollLay25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[25] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 26
function SpriteCollLay26::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[26];
	
	%this.setStateOn(%value);
}

function SpriteCollLay26::update(%this)
{
	%value = EditorToy.spriteCollLayer[26];
	%this.setStateOn(%value);
}

function SpriteCollLay26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[26] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 27
function SpriteCollLay27::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[27];
	
	%this.setStateOn(%value);
}

function SpriteCollLay27::update(%this)
{
	%value = EditorToy.spriteCollLayer[27];
	%this.setStateOn(%value);
}

function SpriteCollLay27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[27] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 28
function SpriteCollLay28::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[28];
	
	%this.setStateOn(%value);
}

function SpriteCollLay28::update(%this)
{
	%value = EditorToy.spriteCollLayer[28];
	%this.setStateOn(%value);
}

function SpriteCollLay28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[28] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 29
function SpriteCollLay29::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[29];
	
	%this.setStateOn(%value);
}

function SpriteCollLay29::update(%this)
{
	%value = EditorToy.spriteCollLayer[29];
	%this.setStateOn(%value);
}

function SpriteCollLay29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[29] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 30
function SpriteCollLay30::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[30];
	
	%this.setStateOn(%value);
}

function SpriteCollLay30::update(%this)
{
	%value = EditorToy.spriteCollLayer[30];
	%this.setStateOn(%value);
}

function SpriteCollLay30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[30] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//Sprite Collision Layer 31
function SpriteCollLay31::onAdd(%this)
{
	%value = EditorToy.spriteCollLayer[31];
	
	%this.setStateOn(%value);
}

function SpriteCollLay31::update(%this)
{
	%value = EditorToy.spriteCollLayer[31];
	%this.setStateOn(%value);
}

function SpriteCollLay31::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.spriteCollLayer[31] = %value;
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

function SpriteCollLayAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.spriteCollLayer[%i] = 1;
	}
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

function SpriteCollLayNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.spriteCollLayer[%i] = 0;
	}
	EditorToy.stringSpriteCollLayerArray();
	EditorToy.updateSprite();
}

//-----------------------------------------------------------------------------

//Sprite Collision Groups
//Sprite Collision Group 0
function SpriteCollGroup0::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[0];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup0::update(%this)
{
	%value = EditorToy.SpriteCollGroup[0];
	%this.setStateOn(%value);
}

function SpriteCollGroup0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[0] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 1
function SpriteCollGroup1::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[1];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup1::update(%this)
{
	%value = EditorToy.SpriteCollGroup[1];
	%this.setStateOn(%value);
}

function SpriteCollGroup1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[1] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 2
function SpriteCollGroup2::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[2];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup2::update(%this)
{
	%value = EditorToy.SpriteCollGroup[2];
	%this.setStateOn(%value);
}

function SpriteCollGroup2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[2] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 3
function SpriteCollGroup3::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[3];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup3::update(%this)
{
	%value = EditorToy.SpriteCollGroup[3];
	%this.setStateOn(%value);
}

function SpriteCollGroup3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[3] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 4
function SpriteCollGroup4::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[4];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup4::update(%this)
{
	%value = EditorToy.SpriteCollGroup[4];
	%this.setStateOn(%value);
}

function SpriteCollGroup4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[4] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 5
function SpriteCollGroup5::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[5];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup5::update(%this)
{
	%value = EditorToy.SpriteCollGroup[5];
	%this.setStateOn(%value);
}

function SpriteCollGroup5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[5] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 6
function SpriteCollGroup6::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[6];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup6::update(%this)
{
	%value = EditorToy.SpriteCollGroup[6];
	%this.setStateOn(%value);
}

function SpriteCollGroup6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[6] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 7
function SpriteCollGroup7::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[7];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup7::update(%this)
{
	%value = EditorToy.SpriteCollGroup[7];
	%this.setStateOn(%value);
}

function SpriteCollGroup7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[7] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 8
function SpriteCollGroup8::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[8];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup8::update(%this)
{
	%value = EditorToy.SpriteCollGroup[8];
	%this.setStateOn(%value);
}

function SpriteCollGroup8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[8] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 9
function SpriteCollGroup9::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[9];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup9::update(%this)
{
	%value = EditorToy.SpriteCollGroup[9];
	%this.setStateOn(%value);
}

function SpriteCollGroup9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[9] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 10
function SpriteCollGroup10::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[10];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup10::update(%this)
{
	%value = EditorToy.SpriteCollGroup[10];
	%this.setStateOn(%value);
}

function SpriteCollGroup10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[10] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 11
function SpriteCollGroup11::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[11];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup11::update(%this)
{
	%value = EditorToy.SpriteCollGroup[11];
	%this.setStateOn(%value);
}

function SpriteCollGroup11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[11] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 12
function SpriteCollGroup12::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[12];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup12::update(%this)
{
	%value = EditorToy.SpriteCollGroup[12];
	%this.setStateOn(%value);
}

function SpriteCollGroup12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[12] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 13
function SpriteCollGroup13::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[13];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup13::update(%this)
{
	%value = EditorToy.SpriteCollGroup[13];
	%this.setStateOn(%value);
}

function SpriteCollGroup13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[13] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 14
function SpriteCollGroup14::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[14];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup14::update(%this)
{
	%value = EditorToy.SpriteCollGroup[14];
	%this.setStateOn(%value);
}

function SpriteCollGroup14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[14] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 15
function SpriteCollGroup15::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[15];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup15::update(%this)
{
	%value = EditorToy.SpriteCollGroup[15];
	%this.setStateOn(%value);
}

function SpriteCollGroup15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[15] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 16
function SpriteCollGroup16::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[16];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup16::update(%this)
{
	%value = EditorToy.SpriteCollGroup[16];
	%this.setStateOn(%value);
}

function SpriteCollGroup16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[16] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 17
function SpriteCollGroup7::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[17];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup17::update(%this)
{
	%value = EditorToy.SpriteCollGroup[17];
	%this.setStateOn(%value);
}

function SpriteCollGroup17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[17] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 18
function SpriteCollGroup18::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[18];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup18::update(%this)
{
	%value = EditorToy.SpriteCollGroup[18];
	%this.setStateOn(%value);
}

function SpriteCollGroup18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[18] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 19
function SpriteCollGroup19::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[19];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup19::update(%this)
{
	%value = EditorToy.SpriteCollGroup[19];
	%this.setStateOn(%value);
}

function SpriteCollGroup19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[19] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 20
function SpriteCollGroup20::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[20];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup20::update(%this)
{
	%value = EditorToy.SpriteCollGroup[20];
	%this.setStateOn(%value);
}

function SpriteCollGroup20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[20] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 21
function SpriteCollGroup1::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[21];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup21::update(%this)
{
	%value = EditorToy.SpriteCollGroup[21];
	%this.setStateOn(%value);
}

function SpriteCollGroup21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[21] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 22
function SpriteCollGroup22::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[22];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup22::update(%this)
{
	%value = EditorToy.SpriteCollGroup[22];
	%this.setStateOn(%value);
}

function SpriteCollGroup22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[22] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 23
function SpriteCollGroup3::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[23];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup23::update(%this)
{
	%value = EditorToy.SpriteCollGroup[23];
	%this.setStateOn(%value);
}

function SpriteCollGroup23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[23] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 24
function SpriteCollGroup24::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[24];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup24::update(%this)
{
	%value = EditorToy.SpriteCollGroup[24];
	%this.setStateOn(%value);
}

function SpriteCollGroup24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[24] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 25
function SpriteCollGroup25::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[25];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup25::update(%this)
{
	%value = EditorToy.SpriteCollGroup[25];
	%this.setStateOn(%value);
}

function SpriteCollGroup25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[25] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 26
function SpriteCollGroup26::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[26];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup26::update(%this)
{
	%value = EditorToy.SpriteCollGroup[26];
	%this.setStateOn(%value);
}

function SpriteCollGroup26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[26] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 27
function SpriteCollGroup27::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[27];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup27::update(%this)
{
	%value = EditorToy.SpriteCollGroup[27];
	%this.setStateOn(%value);
}

function SpriteCollGroup27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[27] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 28
function SpriteCollGroup28::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[28];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup28::update(%this)
{
	%value = EditorToy.SpriteCollGroup[28];
	%this.setStateOn(%value);
}

function SpriteCollGroup28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[28] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 29
function SpriteCollGroup29::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[29];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup29::update(%this)
{
	%value = EditorToy.SpriteCollGroup[29];
	%this.setStateOn(%value);
}

function SpriteCollGroup29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[29] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 30
function SpriteCollGroup30::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[30];
	
	%this.setStateOn(%value);
}

function SpriteCollGroup30::update(%this)
{
	%value = EditorToy.SpriteCollGroup[30];
	%this.setStateOn(%value);
}

function SpriteCollGroup30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.SpriteCollGroup[30] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Collision Group 31
function SpriteCollGroup31::onAdd(%this)
{
	%value = EditorToy.SpriteCollGroup[31];
	%this.setStateOn(%value);
}

function SpriteCollGroup31::update(%this)
{
	%value = EditorToy.SpriteCollGroup[31];
	%this.setStateOn(%value);
}

function SpriteCollGroup31::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.SpriteCollGroup[31] = %value;
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

function SpriteCollGroupAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.SpriteCollGroup[%i] = 1;
	}
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

function SpriteCollGroupNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.SpriteCollGroup[%i] = 0;
	}
	EditorToy.stringSpriteCollGroupArray();
	EditorToy.updateSprite();
}

//Sprite Source Blend Factor List
function SpriteSrcBlendList::onAdd(%this)
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

function SpriteSrcBlendList::update(%this)
{
	%value = EditorToy.spriteSrcBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function SpriteSrcBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteSrcBlend(%value);
	EditorToy.updateSprite();
}

//Sprite Destination Blend Factor List
function SpriteDstBlendList::onAdd(%this)
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

function SpriteDstBlendList::update(%this)
{
	%value = EditorToy.spriteDstBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function SpriteDstBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDstBlend(%value);
	EditorToy.updateSprite();
}

//Sprite Blend Mode
function SpriteBlendMode::onAdd(%this)
{
	%value = EditorToy.spriteBlendMode;
	%this.setStateOn(%value);
}

function SpriteBlendMode::update(%this)
{
	%value = EditorToy.spriteBlendMode;
	%this.setStateOn(%value);
}

function SpriteBlendMode::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteBlendMode(%value);
	EditorToy.updateSprite();
}

//Sprite Alpha Test
function SpriteAlphaTest::onAdd(%this)
{
	%text = EditorToy.spriteAlphaTest;
	%this.setText(%text);
}

function SpriteAlphaTest::update(%this)
{
	%text = EditorToy.spriteAlphaTest;
	%this.setText(%text);
}

function SpriteAlphaTest::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAlphaTest(%value);
	EditorToy.updateSprite();
}

function SpriteAlphaTest::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAlphaTest(%value);
	EditorToy.updateSprite();
}

function SpriteAlphaTest::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 0.1;
	if(%value < 0.0)
		%value = -1.0;
	EditorToy.updateSpriteAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteAlphaTest::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 0.1;
	if(%value < 0.0)
		%value = 0.0;
	if(%value > 1)
		%value = 1.0;
	EditorToy.updateSpriteAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteColorSelect::onReturn(%this)
{
	%value = %this.getValue();
	SpriteColorBlend.setValue(%value);
}

function SpriteColorBlend::onReturn(%this)
{
	%color = %this.getValue();
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	
	EditorToy.updateSpriteBlendR(%r);
	EditorToy.updateSpriteBlendG(%g);
	EditorToy.updateSpriteBlendB(%b);
	
	EditorToy.updateSprite();
	
}

//Sprite Blend A
function SpriteBlendA::update(%this)
{
	%value = EditorToy.spriteBlendA;
	%this.setValue(%value);
}

function SpriteBlendA::onReturn(%this)
{
	%value = %this.getValue();
	echo(%value);
	EditorToy.updateSpriteBlendA(%value);
	EditorToy.updateSprite();
}


//Sprite FixedAngle
function SpriteFixedAngle::onAdd(%this)
{
	%value = EditorToy.spriteFixedAngle;
	%this.setStateOn(%value);
}

function SpriteFixedAngle::update(%this)
{
	%value = EditorToy.spriteFixedAngle;
	%this.setStateOn(%value);
}

function SpriteFixedAngle::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteFixedAngle(%value);
	EditorToy.updateSprite();
}

//Sprite Angle
function SpriteAngle::onAdd(%this)
{
	%text = EditorToy.spriteAngle;
	%this.setText(%text);
}

function SpriteAngle::update(%this)
{
	%text = EditorToy.spriteAngle;
	%this.setText(%text);
}

function SpriteAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngle(%value);
	EditorToy.updateSprite();
}

function SpriteAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngle(%value);
	EditorToy.updateSprite();
}

function SpriteAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Angle Vel
function SpriteAngularVel::onAdd(%this)
{
	%text = EditorToy.spriteAngVel;
	%this.setText(%text);
}

function SpriteAngularVel::update(%this)
{
	%text = EditorToy.spriteAngVel;
	%this.setText(%text);
}

function SpriteAngularVel::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngVel(%value);
	EditorToy.updateSprite();
}

function SpriteAngularVel::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngVel(%value);
	EditorToy.updateSprite();
}

function SpriteAngularVel::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteAngularVel::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Angle Damp
function SpriteAngularDamp::onAdd(%this)
{
	%text = EditorToy.spriteAngDamp;
	%this.setText(%text);
}

function SpriteAngularDamp::update(%this)
{
	%text = EditorToy.spriteAngDamp;
	%this.setText(%text);
}

function SpriteAngularDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngDamp(%value);
	EditorToy.updateSprite();
}

function SpriteAngularDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteAngDamp(%value);
	EditorToy.updateSprite();
}

function SpriteAngularDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteAngularDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Linear VelX
function SpriteLinearVelX::onAdd(%this)
{
	%text = EditorToy.spriteLinVelX;
	%this.setText(%text);
}

function SpriteLinearVelX::update(%this)
{
	%text = EditorToy.spriteLinVelX;
	%this.setText(%text);
}

function SpriteLinearVelX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelX(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelX(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteLinearVelX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Linear VelY
function SpriteLinearVelY::onAdd(%this)
{
	%text = EditorToy.spriteLinVelY;
	%this.setText(%text);
}

function SpriteLinearVelY::update(%this)
{
	%text = EditorToy.spriteLinVelY;
	%this.setText(%text);
}

function SpriteLinearVelY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelY(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelY(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteLinearVelY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite LinearVel PolAngle
function SpriteLinearVelPolAngle::onAdd(%this)
{
	%text = EditorToy.spriteLinVelPolAngle;
	%this.setText(%text);
}

function SpriteLinearVelPolAngle::update(%this)
{
	%text = EditorToy.spriteLinVelPolAngle;
	%this.setText(%text);
}

function SpriteLinearVelPolAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelPolAngle(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelPolAngle(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite LinearVel PolSpeed
function SpriteLinearVelPolSpeed::onAdd(%this)
{
	%text = EditorToy.spriteLinVelPolSpeed;
	%this.setText(%text);
}

function SpriteLinearVelPolSpeed::update(%this)
{
	%text = EditorToy.spriteLinVelPolSpeed;
	%this.setText(%text);
}

function SpriteLinearVelPolSpeed::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelPolSpeed(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolSpeed::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinVelPolSpeed(%value);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolSpeed::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteLinearVelPolSpeed::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Linear Damp
function SpriteLinearDamp::onAdd(%this)
{
	%text = EditorToy.spriteLinDamp;
	%this.setText(%text);
}

function SpriteLinearDamp::update(%this)
{
	%text = EditorToy.spriteLinDamp;
	%this.setText(%text);
}

function SpriteLinearDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinDamp(%value);
	EditorToy.updateSprite();
}

function SpriteLinearDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteLinDamp(%value);
	EditorToy.updateSprite();
}

function SpriteLinearDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteLinearDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Default Density
function SpriteDefDensity::onAdd(%this)
{
	%text = EditorToy.spriteDefDensity;
	%this.setText(%text);
}

function SpriteDefDensity::update(%this)
{
	%text = EditorToy.spriteDefDensity;
	%this.setText(%text);
}

function SpriteDefDensity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefDensity(%value);
	EditorToy.updateSprite();
}

function SpriteDefDensity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefDensity(%value);
	EditorToy.updateSprite();
}

function SpriteDefDensity::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteDefDensity::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Default Friction
function SpriteDefFriction::onAdd(%this)
{
	%text = EditorToy.spriteDefFriction;
	%this.setText(%text);
}

function SpriteDefFriction::update(%this)
{
	%text = EditorToy.spriteDefFriction;
	%this.setText(%text);
}

function SpriteDefFriction::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefFriction(%value);
	EditorToy.updateSprite();
}

function SpriteDefFriction::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefFriction(%value);
	EditorToy.updateSprite();
}

function SpriteDefFriction::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteDefFriction::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Default Restitution
function SpriteDefRestitution::onAdd(%this)
{
	%text = EditorToy.spriteDefRestitution;
	%this.setText(%text);
}

function SpriteDefRestitution::update(%this)
{
	%text = EditorToy.spriteDefRestitution;
	%this.setText(%text);
}

function SpriteDefRestitution::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefRestitution(%value);
	EditorToy.updateSprite();
}

function SpriteDefRestitution::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteDefRestitution(%value);
	EditorToy.updateSprite();
}

function SpriteDefRestitution::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateSpriteDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

function SpriteDefRestitution::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateSpriteDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateSprite();
}

//Sprite Coll Suppress
function SpriteCollSuppress::onAdd(%this)
{
	%value = EditorToy.spriteCollSupp;
	%this.setStateOn(%value);
}

function SpriteCollSuppress::update(%this)
{
	%value = EditorToy.spriteCollSupp;
	%this.setStateOn(%value);
}

function SpriteCollSuppress::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteCollSupp(%value);
	EditorToy.updateSprite();
}

//Sprite Coll One Way
function SpriteCollOneWay::onAdd(%this)
{
	%value = EditorToy.spriteCollOne;
	%this.setStateOn(%value);
}

function SpriteCollOneWay::update(%this)
{
	%value = EditorToy.spriteCollOne;
	%this.setStateOn(%value);
}

function SpriteCollOneWay::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateSpriteCollOne(%value);
	EditorToy.updateSprite();
}


//Sprite Name
function SpriteName::onAdd(%this)
{
	%text = EditorToy.spriteName;
	%this.setText(%text);
}

function SpriteName::update(%this)
{
	%text = EditorToy.spriteName;
	%this.setText(%text);
}

function SpriteName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteName(%value);
	EditorToy.updateSprite();
}

function SpriteName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteName(%value);
	EditorToy.updateSprite();
}

//Sprite Class
function SpriteClass::onAdd(%this)
{
	%text = EditorToy.spriteClass;
	%this.setText(%text);
}

function SpriteClass::update(%this)
{
	%text = EditorToy.spriteClass;
	%this.setText(%text);
}

function SpriteClass::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteClass(%value);
	EditorToy.updateSprite();
}

function SpriteClass::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSpriteClass(%value);
	EditorToy.updateSprite();
}

