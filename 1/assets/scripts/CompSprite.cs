//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//CompSprite functions 
function EditorToy::loadCompSpriteAsset(%this)
{
	%mName = EditorToy.moduleName;
	%CompSpriteAsset = new OpenFileDialog();
	%CompSpriteAsset.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%CompSpriteAsset.Title = "Choose ImageAsset for CompositeSprite";
	%CompSpriteAsset.MustExist = true;
	%CompSpriteAsset.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%CompSpriteAsset.ChangePath = false;
	
	if(%CompSpriteAsset.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %CompSpriteAsset.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%compSpriteBase = fileBase(%defaultBase);
		%CompSpriteAsset.delete();
		%this.createCompSprite(%compSpriteBase);
	}
	if(isObject(%CompSpriteAsset))
	{
		%CompSpriteAsset.delete();
	}
}

function CompSpriteLockBttn::update(%this)
{
	%obj = EditorToy.selObject;
	%value = %obj.locked;
	if(%value == 1)
	{
		CompSpriteScroll.setVisible(0);
	}
	%this.setStateOn(%value);
}

function EditorToy::createRectComp(%this)
{
	CompSpritePopout.setVisible(0);
	RectDesigner.setVisible(1);
}

function EditorToy::finalizeRect(%this)
{
	%this.updateCompSpriteLayout("rect");
	RectDesigner.setVisible(0);
	EditorToy.compRectXNum = CSDefCountX.getText();
	EditorToy.compRectYNum = CSDefCountY.getText();
	EditorToy.compDefSpriteWidth = CSDefSizeX.getText();
	EditorToy.compDefSpriteHeight = CSDefSizeY.getText();
	EditorToy.compDefSpriteStrideX = CSDefStrideX.getText();
	EditorToy.compDefSpriteStrideY = CSDefStrideY.getText();
	
	%this.createCompSprite();
}

function CSDefCountX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compRectXNum = %value;
}

function CSDefCountY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compRectYNum = %value;
}

function CSDefSizeX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compDefSpriteWidth = %value;
}

function CSDefSizeY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compDefSpriteHeight = %value;
}

function CSDefStrideX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compDefSpriteStrideX = %value;
}

function CSDefStrideY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.compDefSpriteStrideY = %value;
}

function EditorToy::createNoLayoutComp(%this)
{
	%this.updateCompSpriteLayout("off");
	CompSpritePopout.setVisible(0);
	%mName = EditorToy.moduleName;
	%NoLayDefImg = new OpenFileDialog();
	%NoLayDefImg.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%NoLayDefImg.Title = "Choose ImageAsset for Painting";
	%NoLayDefImg.MustExist = true;
	%NoLayDefImg.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%NoLayDefImg.ChangePath = false;
	
	if(%NoLayDefImg.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %NoLayDefImg.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%imgName = fileBase(%defaultBase);
		%NoLayDefImg.delete();
		%this.updateEditImage(%imgName);
		%this.updateEditImageFrame(0);
		%this.createCompSprite();
		CSEditImage.update();
	}
	if(isObject(%NoLayDefImg))
	{
		%NoLayDefImg.delete();
	}
}

function EditorToy::createCompSprite(%this)
{
	//Universals
	%scene = EditorToy.activeScene;
	%cPX = EditorToy.compPosX;
	%cPY = EditorToy.compPosY;
	%cBL = EditorToy.compBatchLayout;
	%cBSM = EditorToy.compBatchSortMode;
	%cDSSX = EditorToy.compDefSpriteStrideX;
	%cDSSY = EditorToy.compDefSpriteStrideY;
	%cDSW = EditorToy.compDefSpriteWidth;
	%cDSH = EditorToy.compDefSpriteHeight;
	
	//RectSpecific. 
	%cRXN = EditorToy.compRectXNum;
	%cRYN = EditorToy.compRectYNum;
	
	%composite = new CompositeSprite();

    // Set the default sprite stride.
    // This is used in rectilinear layout mode to scale the specified logical position arguments.	
    %composite.setDefaultSpriteStride( %cDSSX, %cDSSY );
    
    // Set the default sprite size used to a little less than the stride so we get a "gap"
    // in between the sprites.
    %composite.setDefaultSpriteSize( %cDSW, %cDSH );
	
	// Set the batch layout mode.  We must do this before we add any sprites.
    %composite.SetBatchLayout( %cBL );
	%composite.setPickingAllowed(true);
	%composite.setBodyType(static);
	// Set the batch sort mode for when we're render isolated.
	%composite.SetBatchSortMode( %cBSM );	

	if(%cBL $= "rect")
	{
		// Calculate a range.
		// for OOBB to be correct fit we need to work out half for each
		// because we want it offset by half the size of a sprite we need to
		// take away 0.5.
		%xRange = (%cRXN * 0.5) - 0.5;
		%yRange = (%cRYN * 0.5) - 0.5;
		
		for ( %y = -%yRange; %y <= %yRange; %y = %y + 1 )
		{
			for ( %x = -%xRange; %x <= %xRange; %x = %x + 1 )
			{
				// Add a sprite with the specified logical position.
				// In rectilinear layout this two-part position is 
				// scaled by the default sprite-stride.
				%composite.addSprite( %x SPC %y );

				// Set the sprite image with a random frame.
				// We could also use an animation here.
				%composite.setSpriteImage( "EditorToy:rectDefault");
			}
		}
	}
	else if(%cBL $= "off")
	{
		%mName = EditorToy.moduleName;
		%imgName = EditorToy.editImage;
		
		%composite.addSprite();
		%composite.setSpriteImage( %mName @ ":" @ %imgName);
	}
	// Add to the scene.
	//Composite should be set to inactive as well
	%composite.setActive(0);
	%scene.add( %composite );
}

function EditorToy::updateCompSprite(%this)
{
	//Initialize our data
	%obj = EditorToy.selCompSprite;
	%name = EditorToy.compSpriteName;
	%class = EditorToy.compSpriteClass;
	%posX = EditorToy.compSpritePosX;
	%posY = EditorToy.compSpritePosY;
	%size = %width SPC %height;
	%pos = %posX SPC %posY;
	%batchSort = EditorToy.compBatchSortMode;
	%batchIsolate = EditorToy.compSpriteIsolated;
	%batchCull = EditorToy.compSpriteCull;
	%body = EditorToy.compSpriteBody;
	%ang = EditorToy.compSpriteAngle;
	%fixAng = EditorToy.compSpriteFixedAngle;
	%angDam = EditorToy.compSpriteAngDamp;
	%angVel = EditorToy.compSpriteAngVel;
	%linVelX = EditorToy.compSpriteLinVelX;
	%linVelY = EditorToy.compSpriteLinVelY;
	%linVelPolAng = EditorToy.compSpriteLinPolAngle;
	%linVelPolSpeed = EditorToy.compSpriteLinPolSpeed;
	%linDam = EditorToy.compSpriteLinDamp;
	%defDen = EditorToy.compSpriteDefDensity;
	%defFri = EditorToy.compSpriteDefFriction;
	%defRes = EditorToy.compSpriteDefRestitution;
	%collSupp = EditorToy.compSpriteCollSupp;
	%collOne = EditorToy.compSpriteCollOne;
	%colShape = EditorToy.polyListPosLocal;
	%frame = EditorToy.compSpriteFrame;
	%grav = EditorToy.compSpriteGravity;
	%sceneLay = EditorToy.compSpriteSceneLayer;
	%sceneGroup = EditorToy.compSpriteSceneGroup;
	%collLayers = EditorToy.compSpriteCollLayers;
	%collGroups = EditorToy.compSpriteCollGroups;
	%blendMode = EditorToy.compSpriteBlendMode;
	%srcBlend = EditorToy.compSpriteSrcBlend;
	%dstBlend = EditorToy.compSpriteDstBlend;
	%alphaTest = EditorToy.compSpriteAlphaTest;
	%blendR = EditorToy.compSpriteBlendR;
	%blendG = EditorToy.compSpriteBlendG;
	%blendB = EditorToy.compSpriteBlendB;
	%blendA = EditorToy.compSpriteBlendA;
	
	//set data to selected compSprite
	%obj.setName(%name);
	%obj.setClassNamespace(%class);
	%obj.setPosition(%pos);
	%obj.setBatchSortMode(%batchSort);
	%obj.setBatchIsolated(%batchIsolate);
	%obj.setBatchCulling(%batchCull);
	%obj.setBodyType(%body);
	%obj.setAngle(%ang);
	%obj.setFixedAngle(%fixAng);
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
	
	//%obj.setImageFrame(%frame);
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
//Update CompSprite Values
function EditorToy::updateSelCompSprite(%this, %obj)
{
	%this.selCompSprite = %obj;
}
function EditorToy::updateCompSpritePosX(%this, %value)
{
	%this.compSpritePosX = %value;
}

function EditorToy::updateCompSpritePosY(%this, %value)
{
	%this.compSpritePosY = %value;
}

function EditorToy::updateCompSpriteWidth(%this, %value)
{
	%this.compSpriteWidth = %value;
}

function EditorToy::updateCompSpriteHeight(%this, %value)
{
	%this.compSpriteHeight = %value;
}

function EditorToy::updateCompSpriteFlipX(%this, %value)
{
	%this.compSpriteFlipX = %value;
}

function EditorToy::updateCompSpriteFlipY(%this, %value)
{
	%this.compSpriteFlipY = %value;
}

function EditorToy::updateCompSpriteName(%this, %value)
{
	%this.compSpriteName = %value;
}

function EditorToy::updateCompSpriteClass(%this, %value)
{
	%this.compSpriteClass = %value;
}

function EditorToy::updateCompSpriteAngle(%this, %value)
{
	%this.compSpriteAngle = %value;
}

function EditorToy::updateCompSpriteFixedAngle(%this, %value)
{
	%this.compSpriteFixedAngle = %value;
}

function EditorToy::updateCompSpriteAngDamp(%this, %value)
{
	%this.compSpriteAngDamp = %value;
}

function EditorToy::updateCompSpriteAngVel(%this, %value)
{
	%this.compSpriteAngVel = %value;
}

function EditorToy::updateCompSpriteLinVelX(%this, %value)
{
	%this.compSpriteLinVelX = %value;
}

function EditorToy::updateCompSpriteLinVelY(%this, %value)
{
	%this.compSpriteLinVelY = %value;
}

function EditorToy::updateCompSpriteLinVelPolAngle(%this, %value)
{
	%this.compSpriteLinVelPolAngle = %value;
}

function EditorToy::updateCompSpriteLinVelPolSpeed(%this, %value)
{
	%this.compSpriteLinVelPolSpeed = %value;
}

function EditorToy::updateCompSpriteLinDamp(%this, %value)
{
	%this.compSpriteLinDamp = %value;
}

function EditorToy::updateCompSpriteDefDensity(%this, %value)
{
	%this.compSpriteDefDensity = %value;
}

function EditorToy::updateCompSpriteDefFriction(%this, %value)
{
	%this.compSpriteDefFriction = %value;
}

function EditorToy::updateCompSpriteDefRestitution(%this, %value)
{
	%this.compSpriteDefRestitution = %value;
}

function EditorToy::updateCompSpriteCollSupp(%this, %value)
{
	%this.compSpriteCollSupp = %value;
}

function EditorToy::updateCompSpriteCollOne(%this, %value)
{
	%this.compSpriteCollOne = %value;
}

function EditorToy::updateCompSpriteBody(%this, %value)
{
	%this.compSpriteBody = %value;
}

function EditorToy::updateCompSpriteLayout(%this, %value)
{
	%this.compBatchLayout = %value;
}

function EditorToy::updateCompBatchSortMode(%this, %value)
{
	%this.compBatchSortMode = %value;
}

function EditorToy::updateCompSpriteIsolated(%this, %value)
{
	%this.compSpriteIsolated = %value;
}

function EditorToy::updateCompSpriteCull(%this, %value)
{
	%this.compSpriteCull = %value;
}

function EditorToy::updateCompSpriteCollShapeCount(%this)
{
	%obj = EditorToy.selCompSprite;
	%colNum = %obj.getCollisionShapeCount();
	%this.compSpriteCollShapeCount = %colNum;
}

function EditorToy::updateCompSpriteFrame(%this, %value)
{
	%this.compSpriteFrame = %value;
}

function EditorToy::updateCompSpriteGravity(%this, %value)
{
	%this.compSpriteGravity = %value;
}

function EditorToy::updateCompSpriteSceneLayer(%this, %value)
{
	%this.compSpriteSceneLayer = %value;
}

function EditorToy::updateCompSpriteSceneGroup(%this, %value)
{
	%this.compSpriteSceneGroup = %value;
}

function EditorToy::updateCompSpriteCollLayers(%this, %value)
{
	%this.compSpriteCollLayers = %value;
}

function EditorToy::updateCompSpriteCollGroups(%this, %value)
{
	%this.compSpriteCollGroups = %value;
}

function EditorToy::updateCompSpriteAlphaTest(%this, %value)
{
	%this.compSpriteAlphaTest = %value;
}

function EditorToy::updateCompSpriteBlendMode(%this, %value)
{
	%this.compSpriteBlendMode = %value;
}

function EditorToy::updateCompSpriteSrcBlend(%this, %value)
{
	%this.compSpriteSrcBlend = %value;
}

function EditorToy::updateCompSpriteDstBlend(%this, %value)
{
	%this.compSpriteDstBlend = %value;
}

function EditorToy::updateCompSpriteBlendR(%this, %value)
{
	%this.compSpriteBlendR = %value;
}

function EditorToy::updateCompSpriteBlendG(%this, %value)
{
	%this.compSpriteBlendG = %value;
}

function EditorToy::updateCompSpriteBlendB(%this, %value)
{
	%this.compSpriteBlendB = %value;
}

function EditorToy::updateCompSpriteBlendA(%this, %value)
{
	%this.compSpriteBlendA = %value;
}

//Load arrays for collision layers and groups
function EditorToy::loadCompSpriteCollLayerArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.compSpriteCollLayer[0] = false;
	EditorToy.compSpriteCollLayer[1] = false;
	EditorToy.compSpriteCollLayer[2] = false;
	EditorToy.compSpriteCollLayer[3] = false;
	EditorToy.compSpriteCollLayer[4] = false;
	EditorToy.compSpriteCollLayer[5] = false;
	EditorToy.compSpriteCollLayer[6] = false;
	EditorToy.compSpriteCollLayer[7] = false;
	EditorToy.compSpriteCollLayer[8] = false;
	EditorToy.compSpriteCollLayer[9] = false;
	EditorToy.compSpriteCollLayer[10] = false;
	EditorToy.compSpriteCollLayer[11] = false;
	EditorToy.compSpriteCollLayer[12] = false;
	EditorToy.compSpriteCollLayer[13] = false;
	EditorToy.compSpriteCollLayer[14] = false;
	EditorToy.compSpriteCollLayer[15] = false;
	EditorToy.compSpriteCollLayer[16] = false;
	EditorToy.compSpriteCollLayer[17] = false;
	EditorToy.compSpriteCollLayer[18] = false;
	EditorToy.compSpriteCollLayer[19] = false;
	EditorToy.compSpriteCollLayer[20] = false;
	EditorToy.compSpriteCollLayer[21] = false;
	EditorToy.compSpriteCollLayer[22] = false;
	EditorToy.compSpriteCollLayer[23] = false;
	EditorToy.compSpriteCollLayer[24] = false;
	EditorToy.compSpriteCollLayer[25] = false;
	EditorToy.compSpriteCollLayer[26] = false;
	EditorToy.compSpriteCollLayer[27] = false;
	EditorToy.compSpriteCollLayer[28] = false;
	EditorToy.compSpriteCollLayer[29] = false;
	EditorToy.compSpriteCollLayer[30] = false;
	EditorToy.compSpriteCollLayer[31] = false;
	
	%collLayers = EditorToy.compSpriteCollLayers;
	%count = getWordCount( %collLayers);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collLayers,%i);
		EditorToy.compSpriteCollLayer[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	CompSpriteCollLay0.update();
	CompSpriteCollLay1.update();
	CompSpriteCollLay2.update();
	CompSpriteCollLay3.update();
	CompSpriteCollLay4.update();
	CompSpriteCollLay5.update();
	CompSpriteCollLay6.update();
	CompSpriteCollLay7.update();
	CompSpriteCollLay8.update();
	CompSpriteCollLay9.update();
	CompSpriteCollLay10.update();
	CompSpriteCollLay11.update();
	CompSpriteCollLay12.update();
	CompSpriteCollLay13.update();
	CompSpriteCollLay14.update();
	CompSpriteCollLay15.update();
	CompSpriteCollLay16.update();
	CompSpriteCollLay17.update();
	CompSpriteCollLay18.update();
	CompSpriteCollLay19.update();
	CompSpriteCollLay20.update();
	CompSpriteCollLay21.update();
	CompSpriteCollLay22.update();
	CompSpriteCollLay23.update();
	CompSpriteCollLay24.update();
	CompSpriteCollLay25.update();
	CompSpriteCollLay26.update();
	CompSpriteCollLay27.update();
	CompSpriteCollLay28.update();
	CompSpriteCollLay29.update();
	CompSpriteCollLay30.update();
	CompSpriteCollLay31.update();
	
}

function EditorToy::stringCompSpriteCollLayerArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.compSpriteCollLayer[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.compSpriteCollLayer[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the compSprites settings
	EditorToy.updateCompSpriteCollLayers(%layerString);
	//Update the gui
	EditorToy.loadCompSpriteCollLayerArray();
}

function EditorToy::loadCompSpriteCollGroupArray(%this)
{
	//Only numbers in this string are activated
	//so each corresponding array needs to be changed
	//to 1.
	//Reset Defaults
	EditorToy.compSpriteCollGroup[0] = false;
	EditorToy.compSpriteCollGroup[1] = false;
	EditorToy.compSpriteCollGroup[2] = false;
	EditorToy.compSpriteCollGroup[3] = false;
	EditorToy.compSpriteCollGroup[4] = false;
	EditorToy.compSpriteCollGroup[5] = false;
	EditorToy.compSpriteCollGroup[6] = false;
	EditorToy.compSpriteCollGroup[7] = false;
	EditorToy.compSpriteCollGroup[8] = false;
	EditorToy.compSpriteCollGroup[9] = false;
	EditorToy.compSpriteCollGroup[10] = false;
	EditorToy.compSpriteCollGroup[11] = false;
	EditorToy.compSpriteCollGroup[12] = false;
	EditorToy.compSpriteCollGroup[13] = false;
	EditorToy.compSpriteCollGroup[14] = false;
	EditorToy.compSpriteCollGroup[15] = false;
	EditorToy.compSpriteCollGroup[16] = false;
	EditorToy.compSpriteCollGroup[17] = false;
	EditorToy.compSpriteCollGroup[18] = false;
	EditorToy.compSpriteCollGroup[19] = false;
	EditorToy.compSpriteCollGroup[20] = false;
	EditorToy.compSpriteCollGroup[21] = false;
	EditorToy.compSpriteCollGroup[22] = false;
	EditorToy.compSpriteCollGroup[23] = false;
	EditorToy.compSpriteCollGroup[24] = false;
	EditorToy.compSpriteCollGroup[25] = false;
	EditorToy.compSpriteCollGroup[26] = false;
	EditorToy.compSpriteCollGroup[27] = false;
	EditorToy.compSpriteCollGroup[28] = false;
	EditorToy.compSpriteCollGroup[29] = false;
	EditorToy.compSpriteCollGroup[30] = false;
	EditorToy.compSpriteCollGroup[31] = false;
	
	%collGroups = EditorToy.compSpriteCollGroups;
	%count = getWordCount( %collGroups);
	
	
	for(%i = 0; %i < %count; %i++)
	{
		%value = getWord(%collGroups,%i);
		EditorToy.compSpriteCollGroup[%value] = true;
	}
	
	//Update layer gui
	//There may be a way to do this with less code but did not
	//want to risk any errors doing this inside a loop.
	CompSpriteCollGroup0.update();
	CompSpriteCollGroup1.update();
	CompSpriteCollGroup2.update();
	CompSpriteCollGroup3.update();
	CompSpriteCollGroup4.update();
	CompSpriteCollGroup5.update();
	CompSpriteCollGroup6.update();
	CompSpriteCollGroup7.update();
	CompSpriteCollGroup8.update();
	CompSpriteCollGroup9.update();
	CompSpriteCollGroup10.update();
	CompSpriteCollGroup11.update();
	CompSpriteCollGroup12.update();
	CompSpriteCollGroup13.update();
	CompSpriteCollGroup14.update();
	CompSpriteCollGroup15.update();
	CompSpriteCollGroup16.update();
	CompSpriteCollGroup17.update();
	CompSpriteCollGroup18.update();
	CompSpriteCollGroup19.update();
	CompSpriteCollGroup20.update();
	CompSpriteCollGroup21.update();
	CompSpriteCollGroup22.update();
	CompSpriteCollGroup23.update();
	CompSpriteCollGroup24.update();
	CompSpriteCollGroup25.update();
	CompSpriteCollGroup26.update();
	CompSpriteCollGroup27.update();
	CompSpriteCollGroup28.update();
	CompSpriteCollGroup29.update();
	CompSpriteCollGroup30.update();
	CompSpriteCollGroup31.update();
	
}

function EditorToy::stringCompSpriteCollGroupArray(%this)
{
	%layerString = "";
	%n = 0;
	for(%i = 0; %i < 32; %i++)
	{
		//first succesful layer neeeds to be added as its own string
		if(%n < 1)
		{
			if(EditorToy.compSpriteCollGroup[%i] == 1)
			{
				%layerString = %i;
				%n = 1;
			}
		}
		else
		{
			//Each layer afterwards needs a SPC between
			if(EditorToy.compSpriteCollGroup[%i] == 1)
			{
				%layerString = %layerString SPC %i;
			}
		}
	}
	//Update the compSprites settings
	EditorToy.updateCompSpriteCollGroups(%layerString);
	//Update the gui
	EditorToy.loadCompSpriteCollGroupArray();
}

//-----------------------------------------------------------------------------
//Update CompSprite Menu
//CompSprite Pos X
function CompSpritePosX::onAdd(%this)
{
	%text = EditorToy.compSpritePosX;
	%this.setText(%text);
}

function CompSpritePosX::update(%this)
{
	%text = EditorToy.compSpritePosX;
	%this.setText(%text);
}

function CompSpritePosX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpritePosX(%value);
	EditorToy.updateCompSprite();
}

function CompSpritePosX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpritePosX(%value);
	EditorToy.updateCompSprite();
}

function CompSpritePosX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpritePosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpritePosX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpritePosX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Y Pos
function CompSpritePosY::onAdd(%this)
{
	%text = EditorToy.compSpritePosY;
	%this.setText(%text);
}

function CompSpritePosY::update(%this)
{
	%text = EditorToy.compSpritePosY;
	%this.setText(%text);
}

function CompSpritePosY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpritePosY(%value);
	EditorToy.updateCompSprite();
}

function CompSpritePosY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpritePosY(%value);
	EditorToy.updateCompSprite();
}

function CompSpritePosY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpritePosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpritePosY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpritePosY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Body Type
function CompSpriteBodyList::onAdd(%this)
{
	%this.add( "Static", 1);
	%this.add( "Dynamic", 2);
	%this.add( "Kinematic", 3);
}

function CompSpriteBodyList::update(%this)
{
	%value = EditorToy.compSpriteBody;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CompSpriteBodyList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateCompSpriteBody(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Batch Sort Mode
function CompSpriteBatchList::onAdd(%this)
{
	%this.add("Off",1);
	%this.add("New",2);
	%this.add("Old",3);
	%this.add("Batch",4);
	%this.add("Group",5);
	%this.add("X",6);
	%this.add("Y",7);
	%this.add("Z",8);
	%this.add("-X",9);
	%this.add("-Y",10);
	%this.add("-Z",11);
}

function CompSpriteBatchList::update(%this)
{
	%value = EditorToy.compBatchSortMode;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CompSpriteLayoutMode::update(%this)
{
	%value = EditorToy.compBatchLayout;
	%this.setText(%value);
}

function CompSpriteBatchList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateCompBatchSortMode(%value);
	EditorToy.updateCompSprite();
}


//CompSprite Isolated
function CompSpriteIsolated::onAdd(%this)
{
	%value = EditorToy.compSpriteIsolated;
	%this.setStateOn(%value);
}

function CompSpriteIsolated::update(%this)
{
	%value = EditorToy.compSpriteIsolated;
	%this.setStateOn(%value);
}

function CompSpriteIsolated::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteIsolated(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Cull
function CompSpriteCull::onAdd(%this)
{
	%value = EditorToy.compSpriteCull;
	%this.setStateOn(%value);
}

function CompSpriteCull::update(%this)
{
	%value = EditorToy.compSpriteCull;
	%this.setStateOn(%value);
}

function CompSpriteCull::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteCull(%value);
	EditorToy.updateCompSprite();
}


//------------------------------------------------------------------------
//Collision Shapes gui
function EditorToy::updateCompSpriteCollGui(%this)
{
	if(isObject (CompSpriteMainCollContainer))
	{
		CompSpriteCollisionShapeStack.remove(CompSpriteMainCollContainer);
	}
	
	%obj = EditorToy.selCompSprite;
	%colNum = EditorToy.compSpriteCollShapeCount;
	CompSpriteCollisionShapeContainer.setExtent(197,150);
	
	if(%colNum == 0)
		return;
	if(%colNum > 0)
	{
		%mainContainer = new GuiControl(CompSpriteMainCollContainer){
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
						class = "CompSpriteCollDensity";
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
						class = "CompSpriteCollFriction";
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
						class = "CompSpriteCollRestitution";
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
						class = "CompSpriteCollRadius";
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
						class = "CompSpriteCollLocalX";
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
						class = "CompSpriteCollLocalY";
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
						command = "EditorToy.deleteCompSpriteColShape("@ %i @ ");";
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
				class="CompSpriteCollSensor";
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
					class = "CompSpriteCollDensity";
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
					class = "CompSpriteCollFriction";
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
					class = "CompSpriteCollRestitution";
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
					command = "EditorToy.deleteCompSpriteColShape("@ %i @ ");";
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
				class="CompSpriteCollSensor";
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
					class = "CompSpriteCollDensity";
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
					class = "CompSpriteCollFriction";
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
					class = "CompSpriteCollRestitution";
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
					command = "EditorToy.deleteCompSpriteColShape("@ %i @ ");";
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
				class="CompSpriteCollSensor";
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
					class = "CompSpriteCollDensity";
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
					class = "CompSpriteCollFriction";
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
					class = "CompSpriteCollRestitution";
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
					command = "EditorToy.deleteCompSpriteColShape("@ %i @ ");";
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
				class="CompSpriteCollSensor";
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
	CompSpriteCollisionShapeStack.add(%mainContainer);
	CompSpriteCollisionShapeContainer.setExtent(197,%mainContainerExtent + 150);
	CompSpriteCollisionShapeRollout.sizeToContents();
}

function CompSpriteCollLocalX::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function CompSpriteCollLocalX::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%colCirRad,%text,%colCirLoc.y);
}

function CompSpriteCollLocalY::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function CompSpriteCollLocalY::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%colCirRad,%colCirLoc.x, %text);
}

function CompSpriteCollRadius::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function CompSpriteCollRadius::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%colCirLoc = %obj.getCircleCollisionShapeLocalPosition(%objId);
	%colCirRad = %obj.getCircleCollisionShapeRadius(%objId);
	%text = %this.getText();
	if(%text < 0.1)
	{
		%text = 0.1;
	}
	%obj.deleteCollisionShape(%objId);
	EditorToy.createCompSpriteCircCollision(%text,%colCirLoc.x,%colCirLoc.y);
}

function EditorToy::createCompSpriteCircCollision(%this, %rad, %locX, %locY)
{	
	%obj = EditorToy.selCompSprite;
	%obj.createCircleCollisionShape(%rad, %locX SPC %locY);
	%this.updateCompSpriteCollShapeCount();
	%this.updateCompSpriteCollGui();
}

function CompSpriteCollDensity::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function CompSpriteCollDensity::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeDensity(%objId,%text);
}

function CompSpriteCollFriction::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function CompSpriteCollFriction::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%text = %this.getText();
	%obj.setCollisionShapeFriction(%objId,%text);
}

function CompSpriteCollRestitution::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
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

function CompSpriteCollRestitution::onLoseFirstResponder(%this)
{
	%obj = EditorToy.selCompSprite;
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

function CompSpriteCollSensor::onClick(%this)
{
	%value = %this.getStateOn();
	%obj = EditorToy.selCompSprite;
	%objId = %this.ObjId;
	%obj.setCollisionShapeIsSensor(%objId,%value);
}

function EditorToy::deleteCompSpriteColShape(%this, %collId)
{
	%obj = EditorToy.selCompSprite;
	%obj.deleteCollisionShape(%collId);
	%this.updateCompSpriteCollShapeCount();
	EditorToy.updateCompSpriteCollGui();
}

//Collision Shapes gui END
//------------------------------------------------------------------------

//CompSprite Frame
function CompSpriteFrame::onAdd(%this)
{
	%text = EditorToy.compSpriteFrame;
	%this.setText(%text);
}

function CompSpriteFrame::update(%this)
{
	%text = EditorToy.compSpriteFrame;
	%this.setText(%text);
}

function CompSpriteFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteFrame(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteFrame(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateCompSpriteFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteFrame::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCompSpriteFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Gravity
function CompSpriteGravity::onAdd(%this)
{
	%text = EditorToy.compSpriteGravity;
	%this.setText(%text);
}

function CompSpriteGravity::update(%this)
{
	%text = EditorToy.compSpriteGravity;
	%this.setText(%text);
}

function CompSpriteGravity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteGravity(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteGravity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteGravity(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteGravity::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateCompSpriteGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteGravity::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCompSpriteGravity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Scene Layer
function CompSpriteSceneLayer::onAdd(%this)
{
	%text = EditorToy.compSpriteSceneLayer;
	%this.setText(%text);
}

function CompSpriteSceneLayer::update(%this)
{
	%text = EditorToy.compSpriteSceneLayer;
	%this.setText(%text);
}

function CompSpriteSceneLayer::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteSceneLayer(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneLayer::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteSceneLayer(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneLayer::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateCompSpriteSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneLayer::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateCompSpriteSceneLayer(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Scene Group
function CompSpriteSceneGroup::onAdd(%this)
{
	%text = EditorToy.compSpriteSceneGroup;
	%this.setText(%text);
}

function CompSpriteSceneGroup::update(%this)
{
	%text = EditorToy.compSpriteSceneGroup;
	%this.setText(%text);
}

function CompSpriteSceneGroup::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteSceneGroup(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneGroup::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteSceneGroup(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneGroup::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateCompSpriteSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteSceneGroup::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	if(%value > 31)
		%value = 31;
	EditorToy.updateCompSpriteSceneGroup(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layers
//CompSprite Collision Layer 0
function CompSpriteCollLay0::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[0];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay0::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[0];
	%this.setStateOn(%value);
}

function CompSpriteCollLay0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[0] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 1
function CompSpriteCollLay1::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[1];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay1::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[1];
	%this.setStateOn(%value);
}

function CompSpriteCollLay1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[1] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 2
function CompSpriteCollLay2::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[2];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay2::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[2];
	%this.setStateOn(%value);
}

function CompSpriteCollLay2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[2] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 3
function CompSpriteCollLay3::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[3];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay3::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[3];
	%this.setStateOn(%value);
}

function CompSpriteCollLay3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[3] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 4
function CompSpriteCollLay4::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[4];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay4::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[4];
	%this.setStateOn(%value);
}

function CompSpriteCollLay4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[4] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 5
function CompSpriteCollLay5::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[5];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay5::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[5];
	%this.setStateOn(%value);
}

function CompSpriteCollLay5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[5] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 6
function CompSpriteCollLay6::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[6];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay6::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[6];
	%this.setStateOn(%value);
}

function CompSpriteCollLay6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[6] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 7
function CompSpriteCollLay7::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[7];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay7::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[7];
	%this.setStateOn(%value);
}

function CompSpriteCollLay7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[7] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 8
function CompSpriteCollLay8::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[8];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay8::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[8];
	%this.setStateOn(%value);
}

function CompSpriteCollLay8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[8] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 9
function CompSpriteCollLay9::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[9];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay9::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[9];
	%this.setStateOn(%value);
}

function CompSpriteCollLay9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[9] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 10
function CompSpriteCollLay10::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[10];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay10::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[10];
	%this.setStateOn(%value);
}

function CompSpriteCollLay10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[10] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 11
function CompSpriteCollLay11::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[11];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay11::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[11];
	%this.setStateOn(%value);
}

function CompSpriteCollLay11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[11] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 12
function CompSpriteCollLay12::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[12];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay12::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[12];
	%this.setStateOn(%value);
}

function CompSpriteCollLay12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[12] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 13
function CompSpriteCollLay13::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[13];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay13::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[13];
	%this.setStateOn(%value);
}

function CompSpriteCollLay13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[13] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 14
function CompSpriteCollLay14::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[14];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay14::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[14];
	%this.setStateOn(%value);
}

function CompSpriteCollLay14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[14] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 15
function CompSpriteCollLay15::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[15];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay15::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[15];
	%this.setStateOn(%value);
}

function CompSpriteCollLay15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[15] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 16
function CompSpriteCollLay16::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[16];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay16::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[16];
	%this.setStateOn(%value);
}

function CompSpriteCollLay16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[16] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 17
function CompSpriteCollLay7::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[17];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay17::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[17];
	%this.setStateOn(%value);
}

function CompSpriteCollLay17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[17] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 18
function CompSpriteCollLay18::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[18];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay18::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[18];
	%this.setStateOn(%value);
}

function CompSpriteCollLay18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[18] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 19
function CompSpriteCollLay19::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[19];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay19::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[19];
	%this.setStateOn(%value);
}

function CompSpriteCollLay19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[19] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 20
function CompSpriteCollLay20::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[20];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay20::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[20];
	%this.setStateOn(%value);
}

function CompSpriteCollLay20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[20] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 21
function CompSpriteCollLay1::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[21];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay21::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[21];
	%this.setStateOn(%value);
}

function CompSpriteCollLay21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[21] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 22
function CompSpriteCollLay22::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[22];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay22::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[22];
	%this.setStateOn(%value);
}

function CompSpriteCollLay22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[22] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 23
function CompSpriteCollLay3::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[23];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay23::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[23];
	%this.setStateOn(%value);
}

function CompSpriteCollLay23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[23] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 24
function CompSpriteCollLay24::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[24];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay24::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[24];
	%this.setStateOn(%value);
}

function CompSpriteCollLay24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[24] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 25
function CompSpriteCollLay25::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[25];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay25::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[25];
	%this.setStateOn(%value);
}

function CompSpriteCollLay25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[25] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 26
function CompSpriteCollLay26::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[26];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay26::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[26];
	%this.setStateOn(%value);
}

function CompSpriteCollLay26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[26] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 27
function CompSpriteCollLay27::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[27];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay27::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[27];
	%this.setStateOn(%value);
}

function CompSpriteCollLay27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[27] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 28
function CompSpriteCollLay28::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[28];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay28::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[28];
	%this.setStateOn(%value);
}

function CompSpriteCollLay28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[28] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 29
function CompSpriteCollLay29::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[29];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay29::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[29];
	%this.setStateOn(%value);
}

function CompSpriteCollLay29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[29] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 30
function CompSpriteCollLay30::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[30];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay30::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[30];
	%this.setStateOn(%value);
}

function CompSpriteCollLay30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[30] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Layer 31
function CompSpriteCollLay31::onAdd(%this)
{
	%value = EditorToy.compSpriteCollLayer[31];
	
	%this.setStateOn(%value);
}

function CompSpriteCollLay31::update(%this)
{
	%value = EditorToy.compSpriteCollLayer[31];
	%this.setStateOn(%value);
}

function CompSpriteCollLay31::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.compSpriteCollLayer[31] = %value;
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

function CompSpriteCollLayAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.compSpriteCollLayer[%i] = 1;
	}
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

function CompSpriteCollLayNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.compSpriteCollLayer[%i] = 0;
	}
	EditorToy.stringCompSpriteCollLayerArray();
	EditorToy.updateCompSprite();
}

//-----------------------------------------------------------------------------

//CompSprite Collision Groups
//CompSprite Collision Group 0
function CompSpriteCollGroup0::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[0];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup0::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[0];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup0::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[0] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 1
function CompSpriteCollGroup1::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[1];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup1::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[1];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup1::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[1] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 2
function CompSpriteCollGroup2::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[2];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup2::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[2];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup2::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[2] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 3
function CompSpriteCollGroup3::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[3];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup3::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[3];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup3::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[3] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 4
function CompSpriteCollGroup4::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[4];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup4::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[4];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup4::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[4] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 5
function CompSpriteCollGroup5::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[5];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup5::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[5];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup5::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[5] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 6
function CompSpriteCollGroup6::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[6];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup6::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[6];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup6::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[6] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 7
function CompSpriteCollGroup7::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[7];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup7::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[7];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup7::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[7] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 8
function CompSpriteCollGroup8::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[8];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup8::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[8];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup8::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[8] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 9
function CompSpriteCollGroup9::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[9];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup9::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[9];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup9::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[9] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 10
function CompSpriteCollGroup10::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[10];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup10::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[10];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup10::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[10] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 11
function CompSpriteCollGroup11::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[11];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup11::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[11];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup11::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[11] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 12
function CompSpriteCollGroup12::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[12];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup12::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[12];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup12::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[12] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 13
function CompSpriteCollGroup13::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[13];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup13::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[13];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup13::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[13] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 14
function CompSpriteCollGroup14::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[14];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup14::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[14];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup14::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[14] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 15
function CompSpriteCollGroup15::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[15];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup15::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[15];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup15::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[15] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 16
function CompSpriteCollGroup16::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[16];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup16::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[16];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup16::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[16] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 17
function CompSpriteCollGroup7::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[17];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup17::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[17];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup17::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[17] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 18
function CompSpriteCollGroup18::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[18];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup18::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[18];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup18::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[18] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 19
function CompSpriteCollGroup19::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[19];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup19::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[19];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup19::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[19] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 20
function CompSpriteCollGroup20::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[20];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup20::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[20];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup20::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[20] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 21
function CompSpriteCollGroup1::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[21];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup21::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[21];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup21::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[21] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 22
function CompSpriteCollGroup22::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[22];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup22::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[22];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup22::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[22] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 23
function CompSpriteCollGroup3::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[23];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup23::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[23];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup23::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[23] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 24
function CompSpriteCollGroup24::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[24];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup24::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[24];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup24::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[24] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 25
function CompSpriteCollGroup25::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[25];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup25::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[25];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup25::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[25] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 26
function CompSpriteCollGroup26::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[26];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup26::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[26];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup26::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[26] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 27
function CompSpriteCollGroup27::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[27];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup27::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[27];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup27::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[27] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 28
function CompSpriteCollGroup28::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[28];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup28::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[28];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup28::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[28] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 29
function CompSpriteCollGroup29::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[29];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup29::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[29];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup29::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[29] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 30
function CompSpriteCollGroup30::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[30];
	
	%this.setStateOn(%value);
}

function CompSpriteCollGroup30::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[30];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup30::onReturn(%this)
{
	%value = %this.getStateOn();
	
	EditorToy.CompSpriteCollGroup[30] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Collision Group 31
function CompSpriteCollGroup31::onAdd(%this)
{
	%value = EditorToy.CompSpriteCollGroup[31];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup31::update(%this)
{
	%value = EditorToy.CompSpriteCollGroup[31];
	%this.setStateOn(%value);
}

function CompSpriteCollGroup31::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.CompSpriteCollGroup[31] = %value;
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

function CompSpriteCollGroupAll::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.CompSpriteCollGroup[%i] = 1;
	}
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

function CompSpriteCollGroupNone::onReturn(%this)
{
	for(%i = 0; %i < 32; %i++)
	{
		EditorToy.CompSpriteCollGroup[%i] = 0;
	}
	EditorToy.stringCompSpriteCollGroupArray();
	EditorToy.updateCompSprite();
}

//CompSprite Source Blend Factor List
function CompSpriteSrcBlendList::onAdd(%this)
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

function CompSpriteSrcBlendList::update(%this)
{
	%value = EditorToy.compSpriteSrcBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CompSpriteSrcBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteSrcBlend(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Destination Blend Factor List
function CompSpriteDstBlendList::onAdd(%this)
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

function CompSpriteDstBlendList::update(%this)
{
	%value = EditorToy.compSpriteDstBlend;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CompSpriteDstBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDstBlend(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Blend Mode
function CompSpriteBlendMode::onAdd(%this)
{
	%value = EditorToy.compSpriteBlendMode;
	%this.setStateOn(%value);
}

function CompSpriteBlendMode::update(%this)
{
	%value = EditorToy.compSpriteBlendMode;
	%this.setStateOn(%value);
}

function CompSpriteBlendMode::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteBlendMode(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Alpha Test
function CompSpriteAlphaTest::onAdd(%this)
{
	%text = EditorToy.compSpriteAlphaTest;
	%this.setText(%text);
}

function CompSpriteAlphaTest::update(%this)
{
	%text = EditorToy.compSpriteAlphaTest;
	%this.setText(%text);
}

function CompSpriteAlphaTest::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAlphaTest(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAlphaTest::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAlphaTest(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAlphaTest::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 0.1;
	if(%value < 0.0)
		%value = -1.0;
	EditorToy.updateCompSpriteAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteAlphaTest::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 0.1;
	if(%value < 0.0)
		%value = 0.0;
	if(%value > 1)
		%value = 1.0;
	EditorToy.updateCompSpriteAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteColorSelect::onReturn(%this)
{
	%value = %this.getValue();
	CompSpriteColorBlend.setValue(%value);
}

function CompSpriteColorBlend::onReturn(%this)
{
	%color = %this.getValue();
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	
	EditorToy.updateCompSpriteBlendR(%r);
	EditorToy.updateCompSpriteBlendG(%g);
	EditorToy.updateCompSpriteBlendB(%b);
	
	EditorToy.updateCompSprite();
	
}

//CompSprite Blend A
function CompSpriteBlendA::update(%this)
{
	%value = EditorToy.compSpriteBlendA;
	%this.setValue(%value);
}

function CompSpriteBlendA::onReturn(%this)
{
	%value = %this.getValue();
	echo(%value);
	EditorToy.updateCompSpriteBlendA(%value);
	EditorToy.updateCompSprite();
}


//CompSprite FixedAngle
function CompSpriteFixedAngle::onAdd(%this)
{
	%value = EditorToy.compSpriteFixedAngle;
	%this.setStateOn(%value);
}

function CompSpriteFixedAngle::update(%this)
{
	%value = EditorToy.compSpriteFixedAngle;
	%this.setStateOn(%value);
}

function CompSpriteFixedAngle::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteFixedAngle(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Angle
function CompSpriteAngle::onAdd(%this)
{
	%text = EditorToy.compSpriteAngle;
	%this.setText(%text);
}

function CompSpriteAngle::update(%this)
{
	%text = EditorToy.compSpriteAngle;
	%this.setText(%text);
}

function CompSpriteAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngle(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngle(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Angle Vel
function CompSpriteAngularVel::onAdd(%this)
{
	%text = EditorToy.compSpriteAngVel;
	%this.setText(%text);
}

function CompSpriteAngularVel::update(%this)
{
	%text = EditorToy.compSpriteAngVel;
	%this.setText(%text);
}

function CompSpriteAngularVel::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngVel(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularVel::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngVel(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularVel::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularVel::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteAngVel(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Angle Damp
function CompSpriteAngularDamp::onAdd(%this)
{
	%text = EditorToy.compSpriteAngDamp;
	%this.setText(%text);
}

function CompSpriteAngularDamp::update(%this)
{
	%text = EditorToy.compSpriteAngDamp;
	%this.setText(%text);
}

function CompSpriteAngularDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngDamp(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteAngDamp(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteAngularDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteAngDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Linear VelX
function CompSpriteLinearVelX::onAdd(%this)
{
	%text = EditorToy.compSpriteLinVelX;
	%this.setText(%text);
}

function CompSpriteLinearVelX::update(%this)
{
	%text = EditorToy.compSpriteLinVelX;
	%this.setText(%text);
}

function CompSpriteLinearVelX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelX(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelX(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteLinVelX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Linear VelY
function CompSpriteLinearVelY::onAdd(%this)
{
	%text = EditorToy.compSpriteLinVelY;
	%this.setText(%text);
}

function CompSpriteLinearVelY::update(%this)
{
	%text = EditorToy.compSpriteLinVelY;
	%this.setText(%text);
}

function CompSpriteLinearVelY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelY(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelY(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteLinVelY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite LinearVel PolAngle
function CompSpriteLinearVelPolAngle::onAdd(%this)
{
	%text = EditorToy.compSpriteLinVelPolAngle;
	%this.setText(%text);
}

function CompSpriteLinearVelPolAngle::update(%this)
{
	%text = EditorToy.compSpriteLinVelPolAngle;
	%this.setText(%text);
}

function CompSpriteLinearVelPolAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelPolAngle(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelPolAngle(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteLinVelPolAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite LinearVel PolSpeed
function CompSpriteLinearVelPolSpeed::onAdd(%this)
{
	%text = EditorToy.compSpriteLinVelPolSpeed;
	%this.setText(%text);
}

function CompSpriteLinearVelPolSpeed::update(%this)
{
	%text = EditorToy.compSpriteLinVelPolSpeed;
	%this.setText(%text);
}

function CompSpriteLinearVelPolSpeed::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelPolSpeed(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolSpeed::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinVelPolSpeed(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolSpeed::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearVelPolSpeed::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteLinVelPolSpeed(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Linear Damp
function CompSpriteLinearDamp::onAdd(%this)
{
	%text = EditorToy.compSpriteLinDamp;
	%this.setText(%text);
}

function CompSpriteLinearDamp::update(%this)
{
	%text = EditorToy.compSpriteLinDamp;
	%this.setText(%text);
}

function CompSpriteLinearDamp::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinDamp(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearDamp::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteLinDamp(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearDamp::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteLinearDamp::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteLinDamp(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Default Density
function CompSpriteDefDensity::onAdd(%this)
{
	%text = EditorToy.compSpriteDefDensity;
	%this.setText(%text);
}

function CompSpriteDefDensity::update(%this)
{
	%text = EditorToy.compSpriteDefDensity;
	%this.setText(%text);
}

function CompSpriteDefDensity::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefDensity(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefDensity::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefDensity(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefDensity::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteDefDensity::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteDefDensity(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Default Friction
function CompSpriteDefFriction::onAdd(%this)
{
	%text = EditorToy.compSpriteDefFriction;
	%this.setText(%text);
}

function CompSpriteDefFriction::update(%this)
{
	%text = EditorToy.compSpriteDefFriction;
	%this.setText(%text);
}

function CompSpriteDefFriction::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefFriction(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefFriction::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefFriction(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefFriction::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteDefFriction::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteDefFriction(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Default Restitution
function CompSpriteDefRestitution::onAdd(%this)
{
	%text = EditorToy.compSpriteDefRestitution;
	%this.setText(%text);
}

function CompSpriteDefRestitution::update(%this)
{
	%text = EditorToy.compSpriteDefRestitution;
	%this.setText(%text);
}

function CompSpriteDefRestitution::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefRestitution(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefRestitution::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteDefRestitution(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteDefRestitution::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	EditorToy.updateCompSpriteDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

function CompSpriteDefRestitution::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1.0;
	EditorToy.updateCompSpriteDefRestitution(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCompSprite();
}

//CompSprite Coll Suppress
function CompSpriteCollSuppress::onAdd(%this)
{
	%value = EditorToy.compSpriteCollSupp;
	%this.setStateOn(%value);
}

function CompSpriteCollSuppress::update(%this)
{
	%value = EditorToy.compSpriteCollSupp;
	%this.setStateOn(%value);
}

function CompSpriteCollSuppress::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteCollSupp(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Coll One Way
function CompSpriteCollOneWay::onAdd(%this)
{
	%value = EditorToy.compSpriteCollOne;
	%this.setStateOn(%value);
}

function CompSpriteCollOneWay::update(%this)
{
	%value = EditorToy.compSpriteCollOne;
	%this.setStateOn(%value);
}

function CompSpriteCollOneWay::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCompSpriteCollOne(%value);
	EditorToy.updateCompSprite();
}


//CompSprite Name
function CompSpriteName::onAdd(%this)
{
	%text = EditorToy.compSpriteName;
	%this.setText(%text);
}

function CompSpriteName::update(%this)
{
	%text = EditorToy.compSpriteName;
	%this.setText(%text);
}

function CompSpriteName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteName(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteName(%value);
	EditorToy.updateCompSprite();
}

//CompSprite Class
function CompSpriteClass::onAdd(%this)
{
	%text = EditorToy.compSpriteClass;
	%this.setText(%text);
}

function CompSpriteClass::update(%this)
{
	%text = EditorToy.compSpriteClass;
	%this.setText(%text);
}

function CompSpriteClass::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteClass(%value);
	EditorToy.updateCompSprite();
}

function CompSpriteClass::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCompSpriteClass(%value);
	EditorToy.updateCompSprite();
}

//----------------------------------------------------------------------
//COMP SPRITE EDIT FUNCTIONS AND UPDATES

function EditorToy::updateCssName(%this, %value)
{
	%this.cssName = %value;
}

function EditorToy::updateCssFrame(%this, %value)
{
	%this.cssFrame = %value;
}

function EditorToy::updateCssLocalPositionX(%this, %value)
{
	%this.cssLocPosX = %value;
}

function EditorToy::updateCssLocalPositionY(%this, %value)
{
	%this.cssLocPosY = %value;
}

function EditorToy::updateCssLogicalPositionX(%this, %value)
{
	%this.cssLogPosX = %value;
}

function EditorToy::updateCssLogicalPositionY(%this, %value)
{
	%this.cssLogPosY = %value;
}

function EditorToy::updateCssSizeWidth(%this, %value)
{
	%this.cssSizeX = %value;
}

function EditorToy::updateCssSizeHeight(%this, %value)
{
	%this.cssSizeY = %value;
}

function EditorToy::updateCssAngle(%this, %value)
{
	%this.cssAng = %value;
}

function EditorToy::updateCssFlipX(%this, %value)
{
	%this.cssFlipX = %value;
}

function EditorToy::updateCssFlipY(%this, %value)
{
	%this.cssFlipY = %value;
}

function EditorToy::updateCssAlphaTest(%this, %value)
{
	%this.cssAlpha = %value;
}

function EditorToy::updateCssBlendAlpha(%this, %value)
{
	%this.cssBlendAlpha = %value;
}

function EditorToy::updateCssBlendR(%this, %value)
{
	%this.cssBlendR = %value;
}

function EditorToy::updateCssBlendG(%this, %value)
{
	%this.cssBlendG = %value;
}

function EditorToy::updateCssBlendB(%this, %value)
{
	%this.cssBlendB = %value;
}

function EditorToy::updateCssBlendMode(%this, %value)
{
	%this.cssBlendMode = %value;
}

function EditorToy::updateCssSrcBlendFactor(%this, %value)
{
	%this.cssSrcBlendFac = %value;
}

function EditorToy::updateCssDstBlendFactor(%this, %value)
{
	%this.cssDstBlendFac = %value;
}

function EditorToy::updateEditImage(%this, %value)
{
	%this.editImage = %value;
}

function EditorToy::updateEditImageFrame(%this, %value)
{
	%this.editImageFrame = %value;
}

function EditorToy::enterEditMode(%this)
{
	EditorToy.mouseMode = "compositeEdit";
	%lay = EditorToy.compBatchLayout;
	CompSpriteEditMenu.setVisible(1);
	
	if(%lay $= "rect")
	{
		//Switch off wrong tools just incase
		NoLayToolsDisplay.setVisible(0);
		RectToolsDisplay.setVisible(1);
	}
	else if(%lay $= "off")
	{
		RectToolsDisplay.setVisible(0);
		NoLayToolsDisplay.setVisible(1);
	}
}

function EditorToy::exitEditMode(%this)
{
	EditorToy.mouseMode = "default";
	if(EditorToy.compEditMode !$= "select")
	{
		EditorToy.compEditMode = "select";
		Canvas.resetCursor();
	}
	if(RectToolsDisplay.isVisible())
	{
		RectToolsDisplay.setVisible(0);
	}
	
	if(NoLayToolsDisplay.isVisible())
	{
		NoLayToolsDisplay.setVisible(0);
	}
	
	if(CompSpriteSpriteMenu.isVisible())
	{
		CompSpriteSpriteMenu.setVisible(0);
	}
	
	CompSpriteEditMenu.setVisible(0);
}

function CSEditImage::update(%this)
{
	%mName = EditorToy.moduleName;
	%imgName = EditorToy.editImage;
	%imgFrame = EditorToy.editImageFrame;
	%this.setImage(%mName @ ":" @ %imgName );
	%this.setImageFrame(%imgFrame);
}

function EditorToy::loadEditImage(%this)
{
	%mName = EditorToy.moduleName;
	%NoLayDefImg = new OpenFileDialog();
	%NoLayDefImg.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%NoLayDefImg.Title = "Choose ImageAsset for Painting";
	%NoLayDefImg.MustExist = true;
	%NoLayDefImg.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%NoLayDefImg.ChangePath = false;
	
	if(%NoLayDefImg.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %NoLayDefImg.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%imgName = fileBase(%defaultBase);
		%NoLayDefImg.delete();
		%this.updateEditImage(%imgName);
		%this.updateEditImageFrame(0);
		CSEditImage.update();
	}
	if(isObject(%NoLayDefImg))
	{
		%NoLayDefImg.delete();
	}
}

function CSEditImageFrame::onAdd(%this)
{
	%text = EditorToy.editImageFrame;
	%this.setText(%text);
}

function CSEditImageFrame::update(%this)
{
	%text = EditorToy.editImageFrame;
	%this.setText(%text);
}

function CSEditImageFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateEditImageFrame(%value);
	CSEditImage.update();
}

function CSEditImageFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateEditImageFrame(%value);
	CSEditImage.update();
}

function CSEditImageFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1.0;
	if(%value < 0)
	{
		%value = 0;
	}
	EditorToy.updateEditImageFrame(%value);
    %text = %value;
    %this.setText(%text);
	CSEditImage.update();
}

function CSEditImageFrame::raiseAmount(%this)
{
	
	%asset = EditorToy.editImage;
	%image = AssetDatabase.acquireAsset(%asset);
	%frameCount = %image.getFrameCount();
	//Sprite ImageFrame starts at 0
	%frameCount = %frameCount - 1;
    %value = %this.getText();
	%value++;
	if(%value > %frameCount)
		%value = %frameCount;
	EditorToy.updateEditImageFrame(%value);
    %text = %value;
    %this.setText(%text);
	CSEditImage.update();
}

function EditorToy::csPaintMode(%this)
{
	CompSpriteSpriteMenu.setVisible(0);
	EditorToy.compEditMode = "paint";
	
	Canvas.setCursor(PaintCursor);
}

function EditorToy::csEraserMode(%this)
{
	CompSpriteSpriteMenu.setVisible(0);
	EditorToy.compEditMode = "delete";
	
	Canvas.setCursor(EraserCursor);
}

function EditorToy::csSelectorMode(%this)
{
	if(EditorToy.compEditMode !$= "select")
	{
		EditorToy.compEditMode = "select";
		Canvas.resetCursor();
	}
}

function EditorToy::csBucketMode(%this)
{
	return;
}

function EditorToy::updateCss(%this)
{
	//Initialize
	%selectId = EditorToy.selectedCss;
	%compositeSprite = EditorToy.selCompSprite;
	%name = EditorToy.cssName;
	%frame = EditorToy.cssFrame;
	%locPosX = EditorToy.cssLocPosX;
	%locPosY = EditorToy.cssLocPosY;
	%width = EditorToy.cssSizeX;
	%height = EditorToy.cssSizeY;
	%ang = EditorToy.cssAng;
	%flipX = EditorToy.cssFlipX;
	%flipY = EditorToy.cssFlipY;
	%srcBlendFac = EditorToy.cssSrcBlendFac;
	%dstBlendFac = EditorToy.cssDstBlendFac;
	%blendR = EditorToy.cssBlendR;
	%blendG = EditorToy.cssBlendG;
	%blendB = EditorToy.cssBlendB;
	%blendMode = EditorToy.cssBlendMode;
	%alphaTest = EditorToy.cssAlpha;
	%blendAlpha = EditorToy.cssBlendAlpha;
	
	//Make sure our sprite is selected
	%compositeSprite.selectSpriteId( %selectId);
	
	//Set new properties
	%compositeSprite.setSpriteName(%name);
	%compositeSprite.setSpriteImageFrame(%frame);
	%compositeSprite.setSpriteLocalPosition(%locPosX, %locPosY);
	%compositeSprite.setSpriteSize(%width, %height);
	%compositeSprite.setSpriteAngle(%ang);
	%compositeSprite.setSpriteFlipX(%flipX);
	%compositeSprite.setSpriteFlipY(%flipY);
	%compositeSprite.setSpriteBlendMode(%blendMode);
	%compositeSprite.setSpriteAlphaTest(%alphaTest);
	%compositeSprite.setSpriteBlendColor(%blendR,%blendG,%blendB);
	%compositeSprite.setSpriteBlendAlpha(%blendAlpha);
	%compositeSprite.setSpriteSrcBlendFactor(%srcBlendFac);
	%compositeSprite.setSpriteDstBlendFactor(%dstBlendFac);
}

function EditorToy::deleteCss(%this)
{
	%selectId = EditorToy.selectedCss;
	%compositeSprite = EditorToy.selCompSprite;
	
	//Make sure our sprite is selected
	%compositeSprite.selectSpriteId( %selectId);
	
	%compositeSprite.removeSprite();
}

function EditorToy::addEditImage(%this)
{
	%compositeSprite = EditorToy.selCompSprite;
	
	%mName = EditorToy.moduleName;
	%imgName = EditorToy.editImage;
	%imgFrame = EditorToy.editImageFrame;
	%imgSizeX = CSImageSizeX.getText();
	%imgSizeY = CSImageSizeY.getText();
	
	if(%imgSizeX $= "" || %imgSizeX < 0)
	{
		%imgSizeX = 1;
	}
	
	if(%imgSizeY $= "" || %imgSizeY < 0)
	{
		%imgSizeY = 1;
	}
		
	%compositeSprite.addSprite();
	%compositeSprite.setSpriteImage( %mName @ ":" @ %imgName);
	%compositeSprite.setSpriteImageFrame(%imgFrame);
	%compositeSprite.setSpriteSize(%imgSizeX, %imgSizeY);
}

//CSS Name
function CSSName::onAdd(%this)
{
	%text = EditorToy.cssName;
	%this.setText(%text);
}

function CSSName::update(%this)
{
	%text = EditorToy.cssName;
	%this.setText(%text);
}

function CSSName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssName(%value);
	EditorToy.updateCss();
}

function CSSName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssName(%value);
	EditorToy.updateCss();
}

//CSS Frame
function CSSFrame::onAdd(%this)
{
	%text = EditorToy.cssFrame;
	%this.setText(%text);
}

function CSSFrame::update(%this)
{
	%text = EditorToy.cssFrame;
	%this.setText(%text);
}

function CSSFrame::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssFrame(%value);
	EditorToy.updateCss();
}

function CSSFrame::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssFrame(%value);
	EditorToy.updateCss();
}

function CSSFrame::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	if(%value < 0)
		%value = 0;
	EditorToy.updateCssFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSFrame::raiseAmount(%this)
{
	%comp = EditorToy.selCompSprite;
	%asset = %comp.getSpriteImage();
	%image = AssetDatabase.acquireAsset(%asset);
	%frameCount = %image.getFrameCount();
	//Sprite ImageFrame starts at 0
	%frameCount = %frameCount - 1;
    %value = %this.getText();
	%value++;
	if(%value > %frameCount)
		%value = %frameCount;
	EditorToy.updateCssFrame(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS LocPos X
function CSSPosX::onAdd(%this)
{
	%text = EditorToy.cssLocPosX;
	%this.setText(%text);
}

function CSSPosX::update(%this)
{
	%text = EditorToy.cssLocPosX;
	%this.setText(%text);
}

function CSSPosX::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssLocalPositionX(%value);
	EditorToy.updateCss();
}

function CSSPosX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssLocalPositionX(%value);
	EditorToy.updateCss();
}

function CSSPosX::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	EditorToy.updateCssLocalPositionX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSPosX::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCssLocalPositionX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS LocPos Y
function CSSPosY::onAdd(%this)
{
	%text = EditorToy.cssLocPosY;
	%this.setText(%text);
}

function CSSPosY::update(%this)
{
	%text = EditorToy.cssLocPosY;
	%this.setText(%text);
}

function CSSPosY::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssLocalPositionY(%value);
	EditorToy.updateCss();
}

function CSSPosY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssLocalPositionY(%value);
	EditorToy.updateCss();
}

function CSSPosY::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	EditorToy.updateCssLocalPositionY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSPosY::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCssLocalPositionY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS Size Width
function CSSWidth::onAdd(%this)
{
	%text = EditorToy.cssSizeX;
	%this.setText(%text);
}

function CSSWidth::update(%this)
{
	%text = EditorToy.cssSizeX;
	%this.setText(%text);
}

function CSSWidth::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssSizeWidth(%value);
	EditorToy.updateCss();
}

function CSSWidth::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssSizeWidth(%value);
	EditorToy.updateCss();
}

function CSSWidth::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	EditorToy.updateCssSizeWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSWidth::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCssSizeWidth(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS Size Height
function CSSHeight::onAdd(%this)
{
	%text = EditorToy.cssSizeY;
	%this.setText(%text);
}

function CSSHeight::update(%this)
{
	%text = EditorToy.cssSizeY;
	%this.setText(%text);
}

function CSSHeight::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssSizeHeight(%value);
	EditorToy.updateCss();
}

function CSSHeight::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssSizeHeight(%value);
	EditorToy.updateCss();
}

function CSSHeight::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	EditorToy.updateCssSizeHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSHeight::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCssSizeHeight(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS Angle
function CSSAngle::onAdd(%this)
{
	%text = EditorToy.cssAng;
	%this.setText(%text);
}

function CSSAngle::update(%this)
{
	%text = EditorToy.cssAng;
	%this.setText(%text);
}

function CSSAngle::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssAngle(%value);
	EditorToy.updateCss();
}

function CSSAngle::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssAngle(%value);
	EditorToy.updateCss();
}

function CSSAngle::lowerAmount(%this)
{
    %value = %this.getText();
	%value--;
	EditorToy.updateCssAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSAngle::raiseAmount(%this)
{
    %value = %this.getText();
	%value++;
	EditorToy.updateCssSizeAngle(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

//CSS FlipX
function CSSFlipX::onAdd(%this)
{
	%value = EditorToy.cssFlipX;
	%this.setStateOn(%value);
}

function CSSFlipX::update(%this)
{
	%value = EditorToy.cssFlipX;
	%this.setStateOn(%value);
}

function CSSFlipX::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCssFlipX(%value);
	EditorToy.updateCss();
}

//CSS FlipY
function CSSFlipY::onAdd(%this)
{
	%value = EditorToy.cssFlipY;
	%this.setStateOn(%value);
}

function CSSFlipY::update(%this)
{
	%value = EditorToy.cssFlipY;
	%this.setStateOn(%value);
}

function CSSFlipY::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCssFlipY(%value);
	EditorToy.updateCss();
}

//CSS Source Blend Factor List
function CSSSrcBlendList::onAdd(%this)
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

function CSSSrcBlendList::update(%this)
{
	%value = EditorToy.cssSrcBlendFac;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CSSSrcBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssSrcBlendFactor(%value);
	EditorToy.updateCss();
}

//CSS Destination Blend Factor List
function CSSDstBlendList::onAdd(%this)
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

function CSSDstBlendList::update(%this)
{
	%value = EditorToy.cssDstBlendFac;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function CSSDstBlendList::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.cssDstBlendFac(%value);
	EditorToy.updateCss();
}

//CSS Blend Mode
function CSSBlendMode::onAdd(%this)
{
	%value = EditorToy.cssBlendMode;
	%this.setStateOn(%value);
}

function CSSBlendMode::update(%this)
{
	%value = EditorToy.cssBlendMode;
	%this.setStateOn(%value);
}

function CSSBlendMode::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateCssBlendMode(%value);
	EditorToy.updateCss();
}

//CSS Alpha Test
function CSSAlphaTest::onAdd(%this)
{
	%text = EditorToy.cssAlpha;
	%this.setText(%text);
}

function CSSAlphaTest::update(%this)
{
	%text = EditorToy.cssAlpha;
	%this.setText(%text);
}

function CSSAlphaTest::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateCssAlphaTest(%value);
	EditorToy.updateCss();
}

function CSSAlphaTest::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateCssAlphaTest(%value);
	EditorToy.updateCss();
}

function CSSAlphaTest::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 0.1;
	if(%value < 0.0)
		%value = -1.0;
	EditorToy.updateCssAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSAlphaTest::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 0.1;
	if(%value < 0.0)
		%value = 0.0;
	if(%value > 1)
		%value = 1.0;
	EditorToy.updateCssAlphaTest(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.updateCss();
}

function CSSColorSelect::onReturn(%this)
{
	%value = %this.getValue();
	CSSColorBlend.setValue(%value);
}

function CSSColorBlend::onReturn(%this)
{
	%color = %this.getValue();
	%r = getWord(%color,0);
	%g = getWord(%color,1);
	%b = getWord(%color,2);
	
	EditorToy.updateCssBlendR(%r);
	EditorToy.updateCssBlendG(%g);
	EditorToy.updateCssBlendB(%b);
	
	EditorToy.updateCss();
	
}

//CSSBlend A
function CSSBlendA::update(%this)
{
	%value = EditorToy.cssBlendAlpha;
	%this.setValue(%value);
}

function CSSBlendA::onReturn(%this)
{
	%value = %this.getValue();
	EditorToy.updateCssBlendAlpha(%value);
	EditorToy.updateCss();
}

function EditorToy::updateCompSpriteBehavior(%this)
{

	CompSpriteBehaviorContainer.setExtent(197,70);
	CompSpriteBehaviorStack.clear();
	%obj = EditorToy.selCompSprite;
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
					class = "CompSpriteBehaviorField";
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
					class = "CompSpriteBehaviorField";
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
					class = "CompSpriteBehaviorFieldList";
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
			CompSpriteBehaviorStack.add(%container);
		}
	}
	%extent = CompSpriteBehaviorStack.getExtent();
	%height = getWord(%extent, 1);
	%height = %height + 70;
	CompSpriteBehaviorContainer.setExtent(197, %height);
	CompSpriteBehaviorRollout.sizeToContents();
}

function CompSpriteBehaviorList::update(%this)
{
	%count = BehaviorSet.getCount();
	%this.clear();
	for(%i = 0; %i < %count; %i++)
	{
		%this.add(BehaviorSet.getObject(%i).getName(), %i);
	}
}

function EditorToy::addCompSpriteBehavior(%this)
{
	%obj = EditorToy.selCompSprite;
	%behavior = CompSpriteBehaviorList.getText();
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
	EditorToy.createCompSpriteBehaviorField();
}

function EditorToy::createCompSpriteBehaviorField(%this)
{
	%obj = EditorToy.selCompSprite;
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

	EditorToy.updateCompSpriteBehavior();
}

function CompSpriteBehaviorField::onReturn(%this)
{
	%obj = EditorToy.selCompSprite;
	%behaviorId = %this.bId;
	%word = %this.word;
	%value = %this.getText();
	%field = %obj.getFieldValue(%behaviorId);
	%fieldUp = setField(%field, %word, %value);
	%obj.setFieldValue(%behaviorId,%fieldUp);
}

function CompSpriteBehaviorFieldList::onSelect(%this)
{
	%obj = EditorToy.selCompSprite;
	%behaviorId = %this.bId;
	%word = %this.word;
	%value = %this.getText();
	%field = %obj.getFieldValue(%behaviorId);
	%fieldUp = setField(%field, %word, %value);
	%obj.setFieldValue(%behaviorId,%fieldUp);
}