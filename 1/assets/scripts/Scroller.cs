//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//Scroller functions 
function EditorToy::loadScrollerAsset(%this)
{
	%mName = EditorToy.moduleName;
	%ScrollerAssetLoad = new OpenFileDialog();
	%ScrollerAssetLoad.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%ScrollerAssetLoad.Title = "Choose ImageAsset for Scroller";
	%ScrollerAssetLoad.MustExist = true;
	%ScrollerAssetLoad.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%ScrollerAssetLoad.ChangePath = false;
	
	if(%ScrollerAssetLoad.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %ScrollerAssetLoad.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%scrollerBase = fileBase(%defaultBase);
		%ScrollerAssetLoad.delete();
		%this.createScroller(%scrollerBase);
	}
	if(isObject(%ScrollerAssetLoad))
	{
		%ScrollerAssetLoad.delete();
	}
}

function ScrollerLockBttn::update(%this)
{
	%obj = EditorToy.selObject;
	%value = %obj.locked;
	if(%value == 1)
	{
		ScrollerScroll.setVisible(0);
	}
	%this.setStateOn(%value);
}

function EditorToy::createScroller(%this, %assetFile)
{
	%scene = EditorToy.activeScene;
	//Spawn scrollers at camera position
	%pos = SandboxWindow.getCameraPosition();
	%mName = EditorToy.moduleName;
	%scrollerBase = %assetFile;
	// Create the scroller.
    %object = new Scroller();
    %object.Size = "10 10";
	%object.SetBodyType( static );
	%object.Position = %pos;
    %object.Image = %mName @ ":" @%scrollerBase;
	
	%object.setActive( 0 );
	%scene.add(%object);
}

function EditorToy::updateScroller(%this)
{
	//Initialize our data
	%obj = EditorToy.selScroller;
	%name = EditorToy.scrollerName;
	%class = EditorToy.scrollerClass;
	%posX = EditorToy.scrollerPosX;
	%posY = EditorToy.scrollerPosY;
	%speedX = EditorToy.scrollerSpeedX;
	%speedY = EditorToy.scrollerSpeedY;
	%repX = EditorToy.scrollerScrollRepX;
	%repY = EditorToy.scrollerScrollRepY;
	%width = EditorToy.scrollerWidth;
	%height = EditorToy.scrollerHeight;
	%size = %width SPC %height;
	%pos = %posX SPC %posY;
	%body = EditorToy.scrollerBody;
	%ang = EditorToy.scrollerAngle;
	%fixAng = EditorToy.scrollerFixedAngle;
	%angDam = EditorToy.scrollerAngDamp;
	%angVel = EditorToy.scrollerAngVel;
	%linVelX = EditorToy.scrollerLinVelX;
	%linVelY = EditorToy.scrollerLinVelY;
	%linVelPolAng = EditorToy.scrollerLinPolAngle;
	%linVelPolSpeed = EditorToy.scrollerLinPolSpeed;
	%linDam = EditorToy.scrollerLinDamp;
	%defDen = EditorToy.scrollerDefDensity;
	%defFri = EditorToy.scrollerDefFriction;
	%defRes = EditorToy.scrollerDefRestitution;
	%collSupp = EditorToy.scrollerCollSupp;
	%collOne = EditorToy.scrollerCollOne;
	%colShape = EditorToy.polyListPosLocal;
	%frame = EditorToy.scrollerFrame;
	%grav = EditorToy.scrollerGravity;
	%sceneLay = EditorToy.scrollerSceneLayer;
	%sceneGroup = EditorToy.scrollerSceneGroup;
	%collLayers = EditorToy.scrollerCollLayers;
	%collGroups = EditorToy.scrollerCollGroups;
	%blendMode = EditorToy.scrollerBlendMode;
	%srcBlend = EditorToy.scrollerSrcBlend;
	%dstBlend = EditorToy.scrollerDstBlend;
	%alphaTest = EditorToy.scrollerAlphaTest;
	%blendR = EditorToy.scrollerBlendR;
	%blendG = EditorToy.scrollerBlendG;
	%blendB = EditorToy.scrollerBlendB;
	%blendA = EditorToy.scrollerBlendA;
	
	//set data to selected scroller
	%obj.setName(%name);
	%obj.setClassNamespace(%class);
	%obj.setPosition(%pos);
	%obj.setScrollX(%speedX);
	%obj.setScrollY(%speedY);
	%obj.setRepeatX(%repX);
	%obj.setRepeatY(%repY);
	%obj.setSize(%size);
	%obj.setBodyType(%body);
	%obj.setAngularDamping(%angDam);
	%obj.setAngularVelocity(%angVel);
	%obj.setLinearVelocityX(%linVelX);
	%obj.setLinearVelocityY(%linVelY);
	%obj.setLinearVelocityPolar(%linVelPolAng,%linVelPolSpeed);
	%obj.setLinearDamping(%linVel);
	%obj.setDefaultDensity(%defDen);
	%obj.setDefaultFriction(%defFri);
	%obj.setDefaultRestitution(%defRes);
	%obj.setCollisionSuppress(%collSupp);
	%obj.setCollisionOneWay(%collOne);
	
	if(%colShape !$= "")
	{
		%type = EditorToy.drawType;
		if(%type $= "polyCol")
		{
			%obj.createPolygonCollisionShape(%colShape);
		}else if(%type $= "edgeCol")
		{
			%colEdgeLocSX = getWord(%colShape,1);
			%colEdgeLocSY = getWord(%colShape,2);
			%colEdgeLocEX = getWord(%colShape,3);
			%colEdgeLocEY = getWord(%colShape,4);
			%obj.createEdgeCollisionShape(%colEdgeLocSX, %colEdgeLocSY, %colEdgeLocEX, %colEdgeLocEY);
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
//Update Scroller Values
function EditorToy::updateSelScroller(%this, %obj)
{
	%this.selScroller = %obj;
}
function EditorToy::updateScrollerPosX(%this, %value)
{
	%this.scrollerPosX = %value;
}

function EditorToy::updateScrollerPosY(%this, %value)
{
	%this.scrollerPosY = %value;
}

function EditorToy::updateScrollerSpeedX(%this, %value)
{
	%this.scrollerSpeedX = %value;
}

function EditorToy::updateScrollerSpeedY(%this, %value)
{
	%this.scrollerSpeedY = %value;
}

function EditorToy::updateScrollerScrollRepX(%this, %value)
{
	%this.scrollerScrollRepX = %value;
}

function EditorToy::updateScrollerScrollRepY(%this, %value)
{
	%this.scrollerScrollRepY = %value;
}

function EditorToy::updateScrollerWidth(%this, %value)
{
	%this.scrollerWidth = %value;
}

function EditorToy::updateScrollerHeight(%this, %value)
{
	%this.scrollerHeight = %value;
}

function EditorToy::updateScrollerName(%this, %value)
{
	%this.scrollerName = %value;
}

function EditorToy::updateScrollerClass(%this, %value)
{
	%this.scrollerClass = %value;
}

function EditorToy::updateScrollerAngDamp(%this, %value)
{
	%this.scrollerAngDamp = %value;
}

function EditorToy::updateScrollerAngVel(%this, %value)
{
	%this.scrollerAngVel = %value;
}

function EditorToy::updateScrollerLinVelX(%this, %value)
{
	%this.scrollerLinVelX = %value;
}

function EditorToy::updateScrollerLinVelY(%this, %value)
{
	%this.scrollerLinVelY = %value;
}

function EditorToy::updateScrollerLinVelPolAngle(%this, %value)
{
	%this.scrollerLinVelPolAngle = %value;
}

function EditorToy::updateScrollerLinVelPolSpeed(%this, %value)
{
	%this.scrollerLinVelPolSpeed = %value;
}

function EditorToy::updateScrollerLinDamp(%this, %value)
{
	%this.scrollerLinDamp = %value;
}

function EditorToy::updateScrollerDefDensity(%this, %value)
{
	%this.scrollerDefDensity = %value;
}

function EditorToy::updateScrollerDefFriction(%this, %value)
{
	%this.scrollerDefFriction = %value;
}

function EditorToy::updateScrollerDefRestitution(%this, %value)
{
	%this.scrollerDefRestitution = %value;
}

function EditorToy::updateScrollerCollSupp(%this, %value)
{
	%this.scrollerCollSupp = %value;
}

function EditorToy::updateScrollerCollOne(%this, %value)
{
	%this.scrollerCollOne = %value;
}

function EditorToy::updateScrollerBody(%this, %value)
{
	%this.scrollerBody = %value;
}

function EditorToy::updateScrollerCollShapeCount(%this)
{
	%obj = EditorToy.selScroller;
	%colNum = %obj.getCollisionShapeCount();
	%this.scrollerCollShapeCount = %colNum;
}

function EditorToy::updateScrollerFrame(%this, %value)
{
	%this.scrollerFrame = %value;
}

function EditorToy::updateScrollerGravity(%this, %value)
{
	%this.scrollerGravity = %value;
}

function EditorToy::updateScrollerSceneLayer(%this, %value)
{
	%this.scrollerSceneLayer = %value;
}

function EditorToy::updateScrollerSceneGroup(%this, %value)
{
	%this.scrollerSceneGroup = %value;
}

function EditorToy::updateScrollerCollLayers(%this, %value)
{
	%this.scrollerCollLayers = %value;
}

function EditorToy::updateScrollerCollGroups(%this, %value)
{
	%this.scrollerCollGroups = %value;
}

function EditorToy::updateScrollerAlphaTest(%this, %value)
{
	%this.scrollerAlphaTest = %value;
}

function EditorToy::updateScrollerBlendMode(%this, %value)
{
	%this.scrollerBlendMode = %value;
}

function EditorToy::updateScrollerSrcBlend(%this, %value)
{
	%this.scrollerSrcBlend = %value;
}

function EditorToy::updateScrollerDstBlend(%this, %value)
{
	%this.scrollerDstBlend = %value;
}

function EditorToy::updateScrollerBlendR(%this, %value)
{
	%this.scrollerBlendR = %value;
}

function EditorToy::updateScrollerBlendG(%this, %value)
{
	%this.scrollerBlendG = %value;
}

function EditorToy::updateScrollerBlendB(%this, %value)
{
	%this.scrollerBlendB = %value;
}

function EditorToy::updateScrollerBlendA(%this, %value)
{
	%this.scrollerBlendA = %value;
}

//Load arrays for collision layers and groups
function EditorToy::loadScrollerCollLayerArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.scrollerCollLayer[0] = false;
	EditorToy.scrollerCollLayer[1] = false;
	EditorToy.scrollerCollLayer[2] = false;
	EditorToy.scrollerCollLayer[3] = false;
	EditorToy.scrollerCollLayer[4] = false;
	EditorToy.scrollerCollLayer[5] = false;
	EditorToy.scrollerCollLayer[6] = false;
	EditorToy.scrollerCollLayer[7] = false;
	EditorToy.scrollerCollLayer[8] = false;
	EditorToy.scrollerCollLayer[9] = false;
	EditorToy.scrollerCollLayer[10] = false;
	EditorToy.scrollerCollLayer[11] = false;
	EditorToy.scrollerCollLayer[12] = false;
	EditorToy.scrollerCollLayer[13] = false;
	EditorToy.scrollerCollLayer[14] = false;
	EditorToy.scrollerCollLayer[15] = false;
	EditorToy.scrollerCollLayer[16] = false;
	EditorToy.scrollerCollLayer[17] = false;
	EditorToy.scrollerCollLayer[18] = false;
	EditorToy.scrollerCollLayer[19] = false;
	EditorToy.scrollerCollLayer[20] = false;
	EditorToy.scrollerCollLayer[21] = false;
	EditorToy.scrollerCollLayer[22] = false;
	EditorToy.scrollerCollLayer[23] = false;
	EditorToy.scrollerCollLayer[24] = false;
	EditorToy.scrollerCollLayer[25] = false;
	EditorToy.scrollerCollLayer[26] = false;
	EditorToy.scrollerCollLayer[27] = false;
	EditorToy.scrollerCollLayer[28] = false;
	EditorToy.scrollerCollLayer[29] = false;
	EditorToy.scrollerCollLayer[30] = false;
	EditorToy.scrollerCollLayer[31] = false;
	
	%collLayers = EditorToy.scrollerCollLayers;
	%count = getWordCount( %collLayers);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collLayers,%i);
		EditorToy.scrollerCollLayer[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	ScrollerCollLay0.update();
	ScrollerCollLay1.update();
	ScrollerCollLay2.update();
	ScrollerCollLay3.update();
	ScrollerCollLay4.update();
	ScrollerCollLay5.update();
	ScrollerCollLay6.update();
	ScrollerCollLay7.update();
	ScrollerCollLay8.update();
	ScrollerCollLay9.update();
	ScrollerCollLay10.update();
	ScrollerCollLay11.update();
	ScrollerCollLay12.update();
	ScrollerCollLay13.update();
	ScrollerCollLay14.update();
	ScrollerCollLay15.update();
	ScrollerCollLay16.update();
	ScrollerCollLay17.update();
	ScrollerCollLay18.update();
	ScrollerCollLay19.update();
	ScrollerCollLay20.update();
	ScrollerCollLay21.update();
	ScrollerCollLay22.update();
	ScrollerCollLay23.update();
	ScrollerCollLay24.update();
	ScrollerCollLay25.update();
	ScrollerCollLay26.update();
	ScrollerCollLay27.update();
	ScrollerCollLay28.update();
	ScrollerCollLay29.update();
	ScrollerCollLay30.update();
	ScrollerCollLay31.update();
	
}

function EditorToy::stringScrollerCollLayerArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.scrollerCollLayer[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.scrollerCollLayer[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the scrollers settings
	EditorToy.updateScrollerCollLayers(%layerString);
	//Update the gui
	EditorToy.loadScrollerCollLayerArray();
}

function EditorToy::loadScrollerCollGroupArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.scrollerCollGroup[0] = false;
	EditorToy.scrollerCollGroup[1] = false;
	EditorToy.scrollerCollGroup[2] = false;
	EditorToy.scrollerCollGroup[3] = false;
	EditorToy.scrollerCollGroup[4] = false;
	EditorToy.scrollerCollGroup[5] = false;
	EditorToy.scrollerCollGroup[6] = false;
	EditorToy.scrollerCollGroup[7] = false;
	EditorToy.scrollerCollGroup[8] = false;
	EditorToy.scrollerCollGroup[9] = false;
	EditorToy.scrollerCollGroup[10] = false;
	EditorToy.scrollerCollGroup[11] = false;
	EditorToy.scrollerCollGroup[12] = false;
	EditorToy.scrollerCollGroup[13] = false;
	EditorToy.scrollerCollGroup[14] = false;
	EditorToy.scrollerCollGroup[15] = false;
	EditorToy.scrollerCollGroup[16] = false;
	EditorToy.scrollerCollGroup[17] = false;
	EditorToy.scrollerCollGroup[18] = false;
	EditorToy.scrollerCollGroup[19] = false;
	EditorToy.scrollerCollGroup[20] = false;
	EditorToy.scrollerCollGroup[21] = false;
	EditorToy.scrollerCollGroup[22] = false;
	EditorToy.scrollerCollGroup[23] = false;
	EditorToy.scrollerCollGroup[24] = false;
	EditorToy.scrollerCollGroup[25] = false;
	EditorToy.scrollerCollGroup[26] = false;
	EditorToy.scrollerCollGroup[27] = false;
	EditorToy.scrollerCollGroup[28] = false;
	EditorToy.scrollerCollGroup[29] = false;
	EditorToy.scrollerCollGroup[30] = false;
	EditorToy.scrollerCollGroup[31] = false;
	
	%collGroups = EditorToy.scrollerCollGroups;
	%count = getWordCount( %collGroups);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collGroups,%i);
		EditorToy.scrollerCollGroup[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	ScrollerCollGroup0.update();
	ScrollerCollGroup1.update();
	ScrollerCollGroup2.update();
	ScrollerCollGroup3.update();
	ScrollerCollGroup4.update();
	ScrollerCollGroup5.update();
	ScrollerCollGroup6.update();
	ScrollerCollGroup7.update();
	ScrollerCollGroup8.update();
	ScrollerCollGroup9.update();
	ScrollerCollGroup10.update();
	ScrollerCollGroup11.update();
	ScrollerCollGroup12.update();
	ScrollerCollGroup13.update();
	ScrollerCollGroup14.update();
	ScrollerCollGroup15.update();
	ScrollerCollGroup16.update();
	ScrollerCollGroup17.update();
	ScrollerCollGroup18.update();
	ScrollerCollGroup19.update();
	ScrollerCollGroup20.update();
	ScrollerCollGroup21.update();
	ScrollerCollGroup22.update();
	ScrollerCollGroup23.update();
	ScrollerCollGroup24.update();
	ScrollerCollGroup25.update();
	ScrollerCollGroup26.update();
	ScrollerCollGroup27.update();
	ScrollerCollGroup28.update();
	ScrollerCollGroup29.update();
	ScrollerCollGroup30.update();
	ScrollerCollGroup31.update();
	
}

function EditorToy::stringScrollerCollGroupArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.scrollerCollGroup[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.scrollerCollGroup[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the scrollers settings
	EditorToy.updateScrollerCollGroups(%layerString);
	//Update the gui
	EditorToy.loadScrollerCollGroupArray();
}

//-----------------------------------------------------------------------------
//Update Scroller Menu
//Scroller Pos X
function ScrollerPosX::onAdd(%this)
{
	%text = EditorToy.scrollerPosX;
	%this.setText(%text);
}

function ScrollerPosX::update(%this)
{
	%text = EditorToy.scrollerPosX;
	%this.setText(%text);
}

function ScrollerPosX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerPosX(%value);
	EditorToy.updateScroller();
}

function ScrollerPosX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerPosX(%value);
	EditorToy.updateScroller();
}

function ScrollerPosX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerPosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerPosX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerPosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Y Pos
function ScrollerPosY::onAdd(%this)
{
	%text = EditorToy.scrollerPosY;
	%this.setText(%text);
}

function ScrollerPosY::update(%this)
{
	%text = EditorToy.scrollerPosY;
	%this.setText(%text);
}

function ScrollerPosY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerPosY(%value);
	EditorToy.updateScroller();
}

function ScrollerPosY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerPosY(%value);
	EditorToy.updateScroller();
}

function ScrollerPosY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerPosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerPosY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerPosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Speed X
function ScrollerSpeedX::onAdd(%this)
{
	%text = EditorToy.scrollerSpeedX;
	%this.setText(%text);
}

function ScrollerSpeedX::update(%this)
{
	%text = EditorToy.scrollerSpeedX;
	%this.setText(%text);
}

function ScrollerSpeedX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSpeedX(%value);
	EditorToy.updateScroller();
}

function ScrollerSpeedX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSpeedX(%value);
	EditorToy.updateScroller();
}

function ScrollerSpeedX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerSpeedX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerSpeedX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerSpeedX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Speed Y
function ScrollerSpeedY::onAdd(%this)
{
	%text = EditorToy.scrollerSpeedY;
	%this.setText(%text);
}

function ScrollerSpeedY::update(%this)
{
	%text = EditorToy.scrollerSpeedY;
	%this.setText(%text);
}

function ScrollerSpeedY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSpeedY(%value);
	EditorToy.updateScroller();
}

function ScrollerSpeedY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSpeedY(%value);
	EditorToy.updateScroller();
}

function ScrollerSpeedY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerSpeedY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerSpeedY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerSpeedY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller ScrollRep X
function ScrollerScrollRepX::onAdd(%this)
{
	%text = EditorToy.scrollerScrollRepX;
	%this.setText(%text);
}

function ScrollerScrollRepX::update(%this)
{
	%text = EditorToy.scrollerScrollRepX;
	%this.setText(%text);
}

function ScrollerScrollRepX::onReturn(%this)
{
	%value = %this.getText();
	if(%value < 1.0)
		%value = 1.0;
	EditorToy.updateScrollerScrollRepX(%value);
	EditorToy.updateScroller();
}

function ScrollerScrollRepX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerScrollRepX(%value);
	EditorToy.updateScroller();
}

function ScrollerScrollRepX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	if(%value < 1.0)
		%value = 1.0;
	EditorToy.updateScrollerScrollRepX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerScrollRepX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerScrollRepX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller ScrollRep Y
function ScrollerScrollRepY::onAdd(%this)
{
	%text = EditorToy.scrollerScrollRepY;
	%this.setText(%text);
}

function ScrollerScrollRepY::update(%this)
{
	%text = EditorToy.scrollerScrollRepY;
	%this.setText(%text);
}

function ScrollerScrollRepY::onReturn(%this)
{
	%value = %this.getText();
	if(%value < 1.0)
		%value = 1.0;
	EditorToy.updateScrollerScrollRepY(%value);
	EditorToy.updateScroller();
}

function ScrollerScrollRepY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerScrollRepY(%value);
	EditorToy.updateScroller();
}

function ScrollerScrollRepY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	if(%value < 1.0)
		%value = 1.0;
	EditorToy.updateScrollerScrollRepY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerScrollRepY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerScrollRepY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Width
function ScrollerWidth::onAdd(%this)
{
	%text = EditorToy.scrollerWidth;
	%this.setText(%text);
}

function ScrollerWidth::update(%this)
{
	%text = EditorToy.scrollerWidth;
	%this.setText(%text);
}

function ScrollerWidth::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerWidth(%value);
	EditorToy.updateScroller();
}

function ScrollerWidth::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerWidth(%value);
	EditorToy.updateScroller();
}

function ScrollerWidth::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerWidth::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Height
function ScrollerHeight::onAdd(%this)
{
	%text = EditorToy.scrollerHeight;
	%this.setText(%text);
}

function ScrollerHeight::update(%this)
{
	%text = EditorToy.scrollerHeight;
	%this.setText(%text);
}

function ScrollerHeight::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerHeight(%value);
	EditorToy.updateScroller();
}

function ScrollerHeight::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerHeight(%value);
	EditorToy.updateScroller();
}

function ScrollerHeight::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerHeight::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Body Type
function ScrollerBodyList::onAdd(%this)
{
	%this.add( "Static", 1);
	%this.add( "Dynamic", 2);
	%this.add( "Kinematic", 3);
}

function ScrollerBodyList::update(%this)
{
	%value = EditorToy.scrollerBody;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ScrollerBodyList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateScrollerBody(%value);
	EditorToy.updateScroller();
}

//Collision Shapes gui
function EditorToy::updateScrollerCollGui(%this)
{
	if(isObject (ScrollerMainCollContainer))
	{
		ScrollerCollisionShapeStack.remove(ScrollerMainCollContainer);
	}
	
	%obj = EditorToy.selScroller;
	%colNum = EditorToy.scrollerCollShapeCount;
	ScrollerCollisionShapeContainer.setExtent(197,150);
	
	if(%colNum == 0)
		return;
	if(%colNum > 0)
	{
		%mainContainer = new GuiControl(ScrollerMainCollContainer){
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
						class = "ScrollerCollDensity";
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
						class = "ScrollerCollFriction";
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
						class = "ScrollerCollRestitution";
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
						class = "ScrollerCollRadius";
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
						class = "ScrollerCollLocalX";
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
						class = "ScrollerCollLocalY";
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
						command = "EditorToy.deleteScrollerColShape("@ %i @ ");";
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
				class="ScrollerCollSensor";
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
					class = "ScrollerCollDensity";
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
					class = "ScrollerCollFriction";
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
					class = "ScrollerCollRestitution";
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
					command = "EditorToy.deleteScrollerColShape("@ %i @ ");";
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
				class="ScrollerCollSensor";
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
					class = "ScrollerCollDensity";
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
					class = "ScrollerCollFriction";
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
					class = "ScrollerCollRestitution";
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
					command = "EditorToy.deleteScrollerColShape("@ %i @ ");";
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
				class="ScrollerCollSensor";
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
					class = "ScrollerCollDensity";
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
					class = "ScrollerCollFriction";
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
					class = "ScrollerCollRestitution";
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
					command = "EditorToy.deleteScrollerColShape("@ %i @ ");";
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
				class="ScrollerCollSensor";
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
	ScrollerCollisionShapeStack.add(%mainContainer);
	ScrollerCollisionShapeContainer.setExtent(197,%mainContainerExtent + 150);
	ScrollerCollisionShapeRollout.sizeToContents();
}

function ScrollerCollLocalX::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function ScrollerCollLocalX::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function ScrollerCollLocalY::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function ScrollerCollLocalY::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function ScrollerCollRadius::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircScrollerCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function ScrollerCollRadius::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createScrollerCircScrollerCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function EditorToy::createScrollerCircCollision(%this, %rad, %locX, %locY)
{	
	%obj = EditorToy.selScroller;
	%obj.createCircleCollisionShape(%rad, %locX SPC %locY);
	%this.updateScrollerCollShapeCount();
	%this.updateScrollerCollGui();
}

function ScrollerCollDensity::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollDensity::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollFriction::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollFriction::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollRestitution::onReturn(%this)
{
	%obj = EditorToy.selScroller;
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
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollRestitution::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selScroller;
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
	EditorToy.updateScrollerCollGui();
}

function ScrollerCollSensor::onClick(%this)
{
	%value = %this.getStateOn();
	%obj = EditorToy.selScroller;
	%objId = %this.ObjId;
	%obj.setCollisionShapeIsSensor(%objId,%value);
	EditorToy.updateScrollerCollGui();
}

function EditorToy::deleteScrollerColShape(%this, %collId)
{
	%obj = EditorToy.selScroller;
	%obj.deleteCollisionShape(%collId);
	EditorToy.updateScrollerCollGui();
}

//Scroller Frame
function ScrollerFrame::onAdd(%this)
{
	%text = EditorToy.scrollerFrame;
	%this.setText(%text);
}

function ScrollerFrame::update(%this)
{
	%text = EditorToy.scrollerFrame;
	%this.setText(%text);
}

function ScrollerFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerFrame(%value);
	EditorToy.updateScroller();
}

function ScrollerFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerFrame(%value);
	EditorToy.updateScroller();
}

function ScrollerFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateScrollerFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerFrame::raiseAmount(%this)
{
	%scroll = EditorToy.selScroller;
	%asset = %scroll.getImage();
	%image = AssetDatabase.acquireAsset(%asset);
	%frameCount = %image.getFrameCount();
	//Sprite ImageFrame starts at 0
	%frameCount = %frameCount - 1;
    %value = %this.getText();
	%value++;
	if(%value > %frameCount)
		%value = %frameCount;
	EditorToy.updateScrollerFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Gravity
function ScrollerGravity::onAdd(%this)
{
	%text = EditorToy.scrollerGravity;
	%this.setText(%text);
}

function ScrollerGravity::update(%this)
{
	%text = EditorToy.scrollerGravity;
	%this.setText(%text);
}

function ScrollerGravity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerGravity(%value);
	EditorToy.updateScroller();
}

function ScrollerGravity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerGravity(%value);
	EditorToy.updateScroller();
}

function ScrollerGravity::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateScrollerGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerGravity::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateScrollerGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Scene Layer
function ScrollerSceneLayer::onAdd(%this)
{
	%text = EditorToy.scrollerSceneLayer;
	%this.setText(%text);
}

function ScrollerSceneLayer::update(%this)
{
	%text = EditorToy.scrollerSceneLayer;
	%this.setText(%text);
}

function ScrollerSceneLayer::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSceneLayer(%value);
	EditorToy.updateScroller();
}

function ScrollerSceneLayer::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSceneLayer(%value);
	EditorToy.updateScroller();
}

function ScrollerSceneLayer::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateScrollerSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerSceneLayer::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateScrollerSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Scene Group
function ScrollerSceneGroup::onAdd(%this)
{
	%text = EditorToy.scrollerSceneGroup;
	%this.setText(%text);
}

function ScrollerSceneGroup::update(%this)
{
	%text = EditorToy.scrollerSceneGroup;
	%this.setText(%text);
}

function ScrollerSceneGroup::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSceneGroup(%value);
	EditorToy.updateScroller();
}

function ScrollerSceneGroup::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSceneGroup(%value);
	EditorToy.updateScroller();
}

function ScrollerSceneGroup::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateScrollerSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerSceneGroup::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateScrollerSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Collision Layers
//Scroller Collision Layer 0
function ScrollerCollLay0::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[0];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay0::update(%this)
{
	%value = EditorToy.scrollerCollLayer[0];
	%this.setStateOn(%value);
}

function ScrollerCollLay0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[0] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 1
function ScrollerCollLay1::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[1];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay1::update(%this)
{
	%value = EditorToy.scrollerCollLayer[1];
	%this.setStateOn(%value);
}

function ScrollerCollLay1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[1] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 2
function ScrollerCollLay2::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[2];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay2::update(%this)
{
	%value = EditorToy.scrollerCollLayer[2];
	%this.setStateOn(%value);
}

function ScrollerCollLay2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[2] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 3
function ScrollerCollLay3::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[3];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay3::update(%this)
{
	%value = EditorToy.scrollerCollLayer[3];
	%this.setStateOn(%value);
}

function ScrollerCollLay3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[3] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 4
function ScrollerCollLay4::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[4];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay4::update(%this)
{
	%value = EditorToy.scrollerCollLayer[4];
	%this.setStateOn(%value);
}

function ScrollerCollLay4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[4] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 5
function ScrollerCollLay5::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[5];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay5::update(%this)
{
	%value = EditorToy.scrollerCollLayer[5];
	%this.setStateOn(%value);
}

function ScrollerCollLay5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[5] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 6
function ScrollerCollLay6::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[6];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay6::update(%this)
{
	%value = EditorToy.scrollerCollLayer[6];
	%this.setStateOn(%value);
}

function ScrollerCollLay6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[6] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 7
function ScrollerCollLay7::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[7];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay7::update(%this)
{
	%value = EditorToy.scrollerCollLayer[7];
	%this.setStateOn(%value);
}

function ScrollerCollLay7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[7] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 8
function ScrollerCollLay8::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[8];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay8::update(%this)
{
	%value = EditorToy.scrollerCollLayer[8];
	%this.setStateOn(%value);
}

function ScrollerCollLay8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[8] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 9
function ScrollerCollLay9::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[9];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay9::update(%this)
{
	%value = EditorToy.scrollerCollLayer[9];
	%this.setStateOn(%value);
}

function ScrollerCollLay9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[9] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 10
function ScrollerCollLay10::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[10];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay10::update(%this)
{
	%value = EditorToy.scrollerCollLayer[10];
	%this.setStateOn(%value);
}

function ScrollerCollLay10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[10] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 11
function ScrollerCollLay11::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[11];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay11::update(%this)
{
	%value = EditorToy.scrollerCollLayer[11];
	%this.setStateOn(%value);
}

function ScrollerCollLay11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[11] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 12
function ScrollerCollLay12::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[12];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay12::update(%this)
{
	%value = EditorToy.scrollerCollLayer[12];
	%this.setStateOn(%value);
}

function ScrollerCollLay12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[12] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 13
function ScrollerCollLay13::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[13];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay13::update(%this)
{
	%value = EditorToy.scrollerCollLayer[13];
	%this.setStateOn(%value);
}

function ScrollerCollLay13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[13] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 14
function ScrollerCollLay14::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[14];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay14::update(%this)
{
	%value = EditorToy.scrollerCollLayer[14];
	%this.setStateOn(%value);
}

function ScrollerCollLay14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[14] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 15
function ScrollerCollLay15::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[15];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay15::update(%this)
{
	%value = EditorToy.scrollerCollLayer[15];
	%this.setStateOn(%value);
}

function ScrollerCollLay15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[15] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 16
function ScrollerCollLay16::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[16];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay16::update(%this)
{
	%value = EditorToy.scrollerCollLayer[16];
	%this.setStateOn(%value);
}

function ScrollerCollLay16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[16] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 17
function ScrollerCollLay7::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[17];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay17::update(%this)
{
	%value = EditorToy.scrollerCollLayer[17];
	%this.setStateOn(%value);
}

function ScrollerCollLay17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[17] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 18
function ScrollerCollLay18::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[18];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay18::update(%this)
{
	%value = EditorToy.scrollerCollLayer[18];
	%this.setStateOn(%value);
}

function ScrollerCollLay18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[18] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 19
function ScrollerCollLay19::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[19];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay19::update(%this)
{
	%value = EditorToy.scrollerCollLayer[19];
	%this.setStateOn(%value);
}

function ScrollerCollLay19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[19] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 20
function ScrollerCollLay20::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[20];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay20::update(%this)
{
	%value = EditorToy.scrollerCollLayer[20];
	%this.setStateOn(%value);
}

function ScrollerCollLay20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[20] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 21
function ScrollerCollLay1::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[21];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay21::update(%this)
{
	%value = EditorToy.scrollerCollLayer[21];
	%this.setStateOn(%value);
}

function ScrollerCollLay21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[21] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 22
function ScrollerCollLay22::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[22];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay22::update(%this)
{
	%value = EditorToy.scrollerCollLayer[22];
	%this.setStateOn(%value);
}

function ScrollerCollLay22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[22] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 23
function ScrollerCollLay3::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[23];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay23::update(%this)
{
	%value = EditorToy.scrollerCollLayer[23];
	%this.setStateOn(%value);
}

function ScrollerCollLay23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[23] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 24
function ScrollerCollLay24::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[24];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay24::update(%this)
{
	%value = EditorToy.scrollerCollLayer[24];
	%this.setStateOn(%value);
}

function ScrollerCollLay24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[24] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 25
function ScrollerCollLay25::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[25];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay25::update(%this)
{
	%value = EditorToy.scrollerCollLayer[25];
	%this.setStateOn(%value);
}

function ScrollerCollLay25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[25] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 26
function ScrollerCollLay26::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[26];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay26::update(%this)
{
	%value = EditorToy.scrollerCollLayer[26];
	%this.setStateOn(%value);
}

function ScrollerCollLay26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[26] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 27
function ScrollerCollLay27::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[27];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay27::update(%this)
{
	%value = EditorToy.scrollerCollLayer[27];
	%this.setStateOn(%value);
}

function ScrollerCollLay27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[27] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 28
function ScrollerCollLay28::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[28];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay28::update(%this)
{
	%value = EditorToy.scrollerCollLayer[28];
	%this.setStateOn(%value);
}

function ScrollerCollLay28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[28] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 29
function ScrollerCollLay29::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[29];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay29::update(%this)
{
	%value = EditorToy.scrollerCollLayer[29];
	%this.setStateOn(%value);
}

function ScrollerCollLay29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[29] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 30
function ScrollerCollLay30::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[30];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay30::update(%this)
{
	%value = EditorToy.scrollerCollLayer[30];
	%this.setStateOn(%value);
}

function ScrollerCollLay30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[30] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//Scroller Collision Layer 31
function ScrollerCollLay31::onAdd(%this)
{
	%value = EditorToy.scrollerCollLayer[31];
	
	%this.setStateOn(%value);
}

function ScrollerCollLay31::update(%this)
{
	%value = EditorToy.scrollerCollLayer[31];
	%this.setStateOn(%value);
}

function ScrollerCollLay31::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.scrollerCollLayer[31] = %value;
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

function ScrollerCollLayAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.scrollerCollLayer[%i] = 1;
	}
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

function ScrollerCollLayNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.scrollerCollLayer[%i] = 0;
	}
	EditorToy.stringScrollerCollLayerArray();
	EditorToy.updateScroller();
}

//-----------------------------------------------------------------------------

//Scroller Collision Groups
//Scroller Collision Group 0
function ScrollerCollGroup0::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[0];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup0::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[0];
	%this.setStateOn(%value);
}

function ScrollerCollGroup0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[0] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 1
function ScrollerCollGroup1::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[1];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup1::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[1];
	%this.setStateOn(%value);
}

function ScrollerCollGroup1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[1] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 2
function ScrollerCollGroup2::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[2];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup2::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[2];
	%this.setStateOn(%value);
}

function ScrollerCollGroup2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[2] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 3
function ScrollerCollGroup3::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[3];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup3::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[3];
	%this.setStateOn(%value);
}

function ScrollerCollGroup3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[3] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 4
function ScrollerCollGroup4::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[4];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup4::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[4];
	%this.setStateOn(%value);
}

function ScrollerCollGroup4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[4] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 5
function ScrollerCollGroup5::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[5];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup5::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[5];
	%this.setStateOn(%value);
}

function ScrollerCollGroup5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[5] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 6
function ScrollerCollGroup6::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[6];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup6::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[6];
	%this.setStateOn(%value);
}

function ScrollerCollGroup6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[6] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 7
function ScrollerCollGroup7::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[7];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup7::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[7];
	%this.setStateOn(%value);
}

function ScrollerCollGroup7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[7] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 8
function ScrollerCollGroup8::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[8];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup8::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[8];
	%this.setStateOn(%value);
}

function ScrollerCollGroup8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[8] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 9
function ScrollerCollGroup9::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[9];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup9::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[9];
	%this.setStateOn(%value);
}

function ScrollerCollGroup9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[9] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 10
function ScrollerCollGroup10::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[10];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup10::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[10];
	%this.setStateOn(%value);
}

function ScrollerCollGroup10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[10] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 11
function ScrollerCollGroup11::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[11];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup11::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[11];
	%this.setStateOn(%value);
}

function ScrollerCollGroup11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[11] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 12
function ScrollerCollGroup12::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[12];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup12::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[12];
	%this.setStateOn(%value);
}

function ScrollerCollGroup12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[12] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 13
function ScrollerCollGroup13::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[13];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup13::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[13];
	%this.setStateOn(%value);
}

function ScrollerCollGroup13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[13] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 14
function ScrollerCollGroup14::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[14];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup14::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[14];
	%this.setStateOn(%value);
}

function ScrollerCollGroup14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[14] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 15
function ScrollerCollGroup15::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[15];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup15::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[15];
	%this.setStateOn(%value);
}

function ScrollerCollGroup15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[15] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 16
function ScrollerCollGroup16::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[16];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup16::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[16];
	%this.setStateOn(%value);
}

function ScrollerCollGroup16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[16] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 17
function ScrollerCollGroup7::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[17];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup17::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[17];
	%this.setStateOn(%value);
}

function ScrollerCollGroup17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[17] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 18
function ScrollerCollGroup18::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[18];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup18::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[18];
	%this.setStateOn(%value);
}

function ScrollerCollGroup18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[18] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 19
function ScrollerCollGroup19::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[19];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup19::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[19];
	%this.setStateOn(%value);
}

function ScrollerCollGroup19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[19] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 20
function ScrollerCollGroup20::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[20];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup20::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[20];
	%this.setStateOn(%value);
}

function ScrollerCollGroup20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[20] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 21
function ScrollerCollGroup1::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[21];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup21::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[21];
	%this.setStateOn(%value);
}

function ScrollerCollGroup21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[21] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 22
function ScrollerCollGroup22::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[22];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup22::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[22];
	%this.setStateOn(%value);
}

function ScrollerCollGroup22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[22] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 23
function ScrollerCollGroup3::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[23];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup23::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[23];
	%this.setStateOn(%value);
}

function ScrollerCollGroup23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[23] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 24
function ScrollerCollGroup24::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[24];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup24::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[24];
	%this.setStateOn(%value);
}

function ScrollerCollGroup24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[24] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 25
function ScrollerCollGroup25::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[25];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup25::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[25];
	%this.setStateOn(%value);
}

function ScrollerCollGroup25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[25] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 26
function ScrollerCollGroup26::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[26];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup26::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[26];
	%this.setStateOn(%value);
}

function ScrollerCollGroup26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[26] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 27
function ScrollerCollGroup27::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[27];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup27::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[27];
	%this.setStateOn(%value);
}

function ScrollerCollGroup27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[27] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 28
function ScrollerCollGroup28::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[28];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup28::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[28];
	%this.setStateOn(%value);
}

function ScrollerCollGroup28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[28] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 29
function ScrollerCollGroup29::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[29];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup29::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[29];
	%this.setStateOn(%value);
}

function ScrollerCollGroup29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[29] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 30
function ScrollerCollGroup30::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[30];
	
	%this.setStateOn(%value);
}

function ScrollerCollGroup30::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[30];
	%this.setStateOn(%value);
}

function ScrollerCollGroup30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.ScrollerCollGroup[30] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Collision Group 31
function ScrollerCollGroup31::onAdd(%this)
{
	%value = EditorToy.ScrollerCollGroup[31];
	%this.setStateOn(%value);
}

function ScrollerCollGroup31::update(%this)
{
	%value = EditorToy.ScrollerCollGroup[31];
	%this.setStateOn(%value);
}

function ScrollerCollGroup31::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.ScrollerCollGroup[31] = %value;
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

function ScrollerCollGroupAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.ScrollerCollGroup[%i] = 1;
	}
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

function ScrollerCollGroupNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.ScrollerCollGroup[%i] = 0;
	}
	EditorToy.stringScrollerCollGroupArray();
	EditorToy.updateScroller();
}

//Scroller Source Blend Factor List
function ScrollerSrcBlendList::onAdd(%this)
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

function ScrollerSrcBlendList::update(%this)
{
	%value = EditorToy.scrollerSrcBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ScrollerSrcBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerSrcBlend(%value);
	EditorToy.updateScroller();
}

//Scroller Destination Blend Factor List
function ScrollerDstBlendList::onAdd(%this)
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

function ScrollerDstBlendList::update(%this)
{
	%value = EditorToy.scrollerDstBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ScrollerDstBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDstBlend(%value);
	EditorToy.updateScroller();
}

//Scroller Blend Mode
function ScrollerBlendMode::onAdd(%this)
{
	%value = EditorToy.scrollerBlendMode;
	%this.setStateOn(%value);
}

function ScrollerBlendMode::update(%this)
{
	%value = EditorToy.scrollerBlendMode;
	%this.setStateOn(%value);
}

function ScrollerBlendMode::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateScrollerBlendMode(%value);
	EditorToy.updateScroller();
}

//Scroller Alpha Test
function ScrollerAlphaTest::onAdd(%this)
{
	%text = EditorToy.scrollerAlphaTest;
	%this.setText(%text);
}

function ScrollerAlphaTest::update(%this)
{
	%text = EditorToy.scrollerAlphaTest;
	%this.setText(%text);
}

function ScrollerAlphaTest::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAlphaTest(%value);
	EditorToy.updateScroller();
}

function ScrollerAlphaTest::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAlphaTest(%value);
	EditorToy.updateScroller();
}

function ScrollerAlphaTest::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 0.1;
	if(%value < 0.0)
		%value = -1.0;
	EditorToy.updateScrollerAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerAlphaTest::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 0.1;
	if(%value < 0.0)
		%value = 0.0;
	if(%value > 1)
		%value = 1.0;
	EditorToy.updateScrollerAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerColorSelect::onReturn(%this)
{
	%value = %this.getValue();
	ScrollerColorBlend.setValue(%value);
}

function ScrollerColorBlend::onReturn(%this)
{
	%color = %this.getValue();
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	
	EditorToy.updateScrollerBlendR(%r);
	EditorToy.updateScrollerBlendG(%g);
	EditorToy.updateScrollerBlendB(%b);
	
	EditorToy.updateScroller();
	
}

//Scroller Blend A
function ScrollerBlendA::update(%this)
{
	%value = EditorToy.scrollerBlendA;
	%this.setValue(%value);
}

function ScrollerBlendA::onReturn(%this)
{
	%value = %this.getValue();
	echo(%value);
	EditorToy.updateScrollerBlendA(%value);
	EditorToy.updateScroller();
}

//Scroller Angle Vel
function ScrollerAngularVel::onAdd(%this)
{
	%text = EditorToy.scrollerAngVel;
	%this.setText(%text);
}

function ScrollerAngularVel::update(%this)
{
	%text = EditorToy.scrollerAngVel;
	%this.setText(%text);
}

function ScrollerAngularVel::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAngVel(%value);
	EditorToy.updateScroller();
}

function ScrollerAngularVel::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAngVel(%value);
	EditorToy.updateScroller();
}

function ScrollerAngularVel::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerAngularVel::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Angle Damp
function ScrollerAngularDamp::onAdd(%this)
{
	%text = EditorToy.scrollerAngDamp;
	%this.setText(%text);
}

function ScrollerAngularDamp::update(%this)
{
	%text = EditorToy.scrollerAngDamp;
	%this.setText(%text);
}

function ScrollerAngularDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAngDamp(%value);
	EditorToy.updateScroller();
}

function ScrollerAngularDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerAngDamp(%value);
	EditorToy.updateScroller();
}

function ScrollerAngularDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerAngularDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Linear VelX
function ScrollerLinearVelX::onAdd(%this)
{
	%text = EditorToy.scrollerLinVelX;
	%this.setText(%text);
}

function ScrollerLinearVelX::update(%this)
{
	%text = EditorToy.scrollerLinVelX;
	%this.setText(%text);
}

function ScrollerLinearVelX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelX(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelX(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerLinearVelX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Linear VelY
function ScrollerLinearVelY::onAdd(%this)
{
	%text = EditorToy.scrollerLinVelY;
	%this.setText(%text);
}

function ScrollerLinearVelY::update(%this)
{
	%text = EditorToy.scrollerLinVelY;
	%this.setText(%text);
}

function ScrollerLinearVelY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelY(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelY(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerLinearVelY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller LinearVel PolAngle
function ScrollerLinearVelPolAngle::onAdd(%this)
{
	%text = EditorToy.scrollerLinVelPolAngle;
	%this.setText(%text);
}

function ScrollerLinearVelPolAngle::update(%this)
{
	%text = EditorToy.scrollerLinVelPolAngle;
	%this.setText(%text);
}

function ScrollerLinearVelPolAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelPolAngle(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelPolAngle(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller LinearVel PolSpeed
function ScrollerLinearVelPolSpeed::onAdd(%this)
{
	%text = EditorToy.scrollerLinVelPolSpeed;
	%this.setText(%text);
}

function ScrollerLinearVelPolSpeed::update(%this)
{
	%text = EditorToy.scrollerLinVelPolSpeed;
	%this.setText(%text);
}

function ScrollerLinearVelPolSpeed::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelPolSpeed(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolSpeed::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinVelPolSpeed(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolSpeed::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerLinearVelPolSpeed::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Linear Damp
function ScrollerLinearDamp::onAdd(%this)
{
	%text = EditorToy.scrollerLinDamp;
	%this.setText(%text);
}

function ScrollerLinearDamp::update(%this)
{
	%text = EditorToy.scrollerLinDamp;
	%this.setText(%text);
}

function ScrollerLinearDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinDamp(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerLinDamp(%value);
	EditorToy.updateScroller();
}

function ScrollerLinearDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerLinearDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Default Density
function ScrollerDefDensity::onAdd(%this)
{
	%text = EditorToy.scrollerDefDensity;
	%this.setText(%text);
}

function ScrollerDefDensity::update(%this)
{
	%text = EditorToy.scrollerDefDensity;
	%this.setText(%text);
}

function ScrollerDefDensity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefDensity(%value);
	EditorToy.updateScroller();
}

function ScrollerDefDensity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefDensity(%value);
	EditorToy.updateScroller();
}

function ScrollerDefDensity::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerDefDensity::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Default Friction
function ScrollerDefFriction::onAdd(%this)
{
	%text = EditorToy.scrollerDefFriction;
	%this.setText(%text);
}

function ScrollerDefFriction::update(%this)
{
	%text = EditorToy.scrollerDefFriction;
	%this.setText(%text);
}

function ScrollerDefFriction::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefFriction(%value);
	EditorToy.updateScroller();
}

function ScrollerDefFriction::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefFriction(%value);
	EditorToy.updateScroller();
}

function ScrollerDefFriction::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerDefFriction::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Default Restitution
function ScrollerDefRestitution::onAdd(%this)
{
	%text = EditorToy.scrollerDefRestitution;
	%this.setText(%text);
}

function ScrollerDefRestitution::update(%this)
{
	%text = EditorToy.scrollerDefRestitution;
	%this.setText(%text);
}

function ScrollerDefRestitution::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefRestitution(%value);
	EditorToy.updateScroller();
}

function ScrollerDefRestitution::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerDefRestitution(%value);
	EditorToy.updateScroller();
}

function ScrollerDefRestitution::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateScrollerDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

function ScrollerDefRestitution::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateScrollerDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateScroller();
}

//Scroller Coll Suppress
function ScrollerCollSuppress::onAdd(%this)
{
	%value = EditorToy.scrollerCollSupp;
	%this.setStateOn(%value);
}

function ScrollerCollSuppress::update(%this)
{
	%value = EditorToy.scrollerCollSupp;
	%this.setStateOn(%value);
}

function ScrollerCollSuppress::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateScrollerCollSupp(%value);
	EditorToy.updateScroller();
}

//Scroller Coll One Way
function ScrollerCollOneWay::onAdd(%this)
{
	%value = EditorToy.scrollerCollOne;
	%this.setStateOn(%value);
}

function ScrollerCollOneWay::update(%this)
{
	%value = EditorToy.scrollerCollOne;
	%this.setStateOn(%value);
}

function ScrollerCollOneWay::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateScrollerCollOne(%value);
	EditorToy.updateScroller();
}

//Scroller Name
function ScrollerName::onAdd(%this)
{
	%text = EditorToy.scrollerName;
	%this.setText(%text);
}

function ScrollerName::update(%this)
{
	%text = EditorToy.scrollerName;
	%this.setText(%text);
}

function ScrollerName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerName(%value);
	EditorToy.updateScroller();
}

function ScrollerName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerName(%value);
	EditorToy.updateScroller();
}

//Scroller Class
function ScrollerClass::onAdd(%this)
{
	%text = EditorToy.scrollerClass;
	%this.setText(%text);
}

function ScrollerClass::update(%this)
{
	%text = EditorToy.scrollerClass;
	%this.setText(%text);
}

function ScrollerClass::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerClass(%value);
	EditorToy.updateScroller();
}

function ScrollerClass::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateScrollerClass(%value);
	EditorToy.updateScroller();
}

function EditorToy::updateScrollerBehavior(%this)
{

	ScrollerBehaviorContainer.setExtent(197,70);
	ScrollerBehaviorStack.clear();
	%obj = EditorToy.selScroller;
	%bCount = getWordCount( %obj.BehaviorList);
	if(%bCount == 0)
		return;
	for(%i = 0; %i < %bCount; %i++)
	{
		%template = getWord(%obj.BehaviorList, %i);
		%dCount = %template.getBehaviorFieldcount();
		%container = new GuiControl() {
			position = "0 0";
			extent = "197 " @ %dCount * 30 + 30;
			minExtent = "197 " @ %dCount * 30 + 30;
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
				text = %template;
				maxLength = "1024";
				margin = "0 0 0 0";
				padding = "0 0 0 0";
				anchorTop = "1";
				anchorBottom = "0";
				anchorLeft = "1";
				anchorRight = "0";
				position = "10 8";
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
			
		};
		
		for(%n = 0; %n < %dCount; %n++)
		{
			%field = %template.getBehaviorField(%n);
			%fDesc = %template.getBehaviorFieldDescription(%n);
			%fName = getWord(%field,0);
			%fType = getField(%field,1);
			%behaviorDynamic = "Behavior" @ %i;
			%bDynamic = %obj.getFieldValue(%behaviorDynamic);
			%bdCount = getWordCount(%bDynamic);
			for(%j = 0; %j < %bdCount; %j++)
			{
				%word = getWord(%bDynamic, %j);
				if(%word $= %fName)
				{
					%fValue = getField(%bDynamic, %j + 1);
					%fWord = %j + 1;
				}
			}
			
			%fContainer = new GuiControl() {
			position = "0 " @ (%n + 1) * 30;
			extent = "197 30";
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
					text = %fName;
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "10 8";
					extent = "88 15";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiEditorTextProfile";
					visible = "1";
					active = "1";
					tooltip = %fDesc;
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
				};
			};
					
			if(%fType $= "int")
			{
				%gProfile = "GuiEditorTextEditNumProfile";
				%edit = new GuiTextEditCtrl() {
					bId = %behaviorDynamic;
					word = %fWord;
					class = "ScrollerBehaviorField";
					text = %fValue;
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
					position = "94 6";
					extent = "100 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = %gProfile;
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
				};
				%fContainer.add(%edit);
			}
			else if(%fType $= "float" || %fType $= "keybind" || %fType $= "object")
			{
				%gProfile = "GuiEditorTextEditProfile";
				%edit = new GuiTextEditCtrl() {
					bId = %behaviorDynamic;
					word = %fWord;
					class = "ScrollerBehaviorField";
					text = %fValue;
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
					position = "94 6";
					extent = "100 18";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = %gProfile;
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
					};
				%fContainer.add(%edit);
			}
			else if
			(%fType $= "enum")
			{
				%pop = new GuiPopUpMenuCtrl() {
					maxPopupHeight = "200";
					sbUsesNAColor = "0";
					bId = %behaviorDynamic;
					word = %fWord;
					class = "ScrollerBehaviorFieldList";
					reverseTextList = "0";
					bitmapBounds = "16 16";
					maxLength = "1024";
					margin = "0 0 0 0";
					padding = "0 0 0 0";
					anchorTop = "1";
					anchorBottom = "0";
					anchorLeft = "1";
					anchorRight = "0";
					position = "94 6";
					extent = "100 20";
					minExtent = "8 2";
					horizSizing = "right";
					vertSizing = "bottom";
					profile = "GuiPopUpMenuProfile";
					visible = "1";
					active = "1";
					tooltipProfile = "GuiToolTipProfile";
					hovertime = "1000";
					isContainer = "1";
					canSave = "1";
					canSaveDynamicFields = "0";
				};
						
				%fEnums = %template.getBehaviorFieldUserData(%n);
				%fEcount = getWordCount(%fEnums);
				for(%k = 0; %k < %fEcount; %k++)
				{
					%enumField = getField(%fEnums,%k);
					echo(%enumField);
					%pop.add(%enumField ,%k);
				}
				%value = %pop.findText(%fValue);
				%pop.setSelected(%value);
				echo("pop added");
				%fContainer.add(%pop);
				
			}	
			%container.add(%fContainer);
			ScrollerBehaviorStack.add(%container);
		}
	}
	%extent = ScrollerBehaviorStack.getExtent();
	%height = getWord(%extent, 1);
	%height = %height + 70;
	ScrollerBehaviorContainer.setExtent(197, %height);
	ScrollerBehaviorRollout.sizeToContents();
}

function ScrollerBehaviorList::update(%this)
{
	%count = BehaviorSet.getCount();
	%this.clear();
	for(%i = 0; %i < %count; %i++)
	{
		%this.add(BehaviorSet.getObject(%i).getName(), %i);
	}
}

function EditorToy::addScrollerBehavior(%this)
{
	%obj = EditorToy.selScroller;
	%behavior = ScrollerBehaviorList.getText();
	%count = getWordCount( %obj.BehaviorList);
	for(%i = 0; %i < %count; %i++)
	{
		%word = getWord(%obj.BehaviorList, %i);
		if(%word $= %behavior)
		{
			//dont want to add the same behavior twice
			echo("%---Behavior already bound to this object---%");
			return;
		}
	}
	%obj.BehaviorList = setWord( %obj.BehaviorList, getWordCount( %obj.BehaviorList), %behavior);
	EditorToy.createScrollerBehaviorField();
}

function EditorToy::createScrollerBehaviorField(%this)
{
	%obj = EditorToy.selScroller;
	%count = getWordCount( %obj.BehaviorList );
	for(%i = 0; %i < %count; %i++)
	{
		%behavior = getWord(%obj.BehaviorList, %i);
		%fieldValue = %behavior;
		%bCount = %behavior.getBehaviorFieldCount();
		echo(%bCount);
		for(%j = 0; %j < %bCount; %j++)
		{
			%field = %behavior.getBehaviorField(%j);
			%fieldName = getWord(%field, 0);
			%fieldDef = getWord(%field, 2);
			if(%fieldDef $= "")
				%fieldDef = "Null";
			%field = %fieldName TAB %fieldDef;
			%fieldValue = %fieldValue TAB %field;
		}
		%dynField = "Behavior" @ %i;
		
		%command = %obj @ "." @ %dynField @ " = \"" @ %fieldValue @ "\";" ;
		
        eval(%command);
	}

	EditorToy.updateScrollerBehavior();
}

function ScrollerBehaviorField::onReturn(%this)
{
	%obj = EditorToy.selScroller;
	%behaviorId = %this.bId;
	%word = %this.word;
	%value = %this.getText();
	%field = %obj.getFieldValue(%behaviorId);
	%fieldUp = setField(%field, %word, %value);
	%obj.setFieldValue(%behaviorId,%fieldUp);
}

function ScrollerBehaviorFieldList::onSelect(%this)
{
	%obj = EditorToy.selScroller;
	%behaviorId = %this.bId;
	%word = %this.word;
	%value = %this.getText();
	%field = %obj.getFieldValue(%behaviorId);
	%fieldUp = setField(%field, %word, %value);
	%obj.setFieldValue(%behaviorId,%fieldUp);
}