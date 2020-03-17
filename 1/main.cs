//-----------------------------------------------------------------------------
// Copyright (c) 2013 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------
function EditorToy::create(%this)
{
	exec("./assets/scripts/ImageAsset.cs");
	exec("./assets/scripts/Sprite.cs");
	exec("./assets/scripts/Scroller.cs");
	exec("./assets/scripts/CompSprite.cs");
	exec("./assets/scripts/AnimationAsset.cs");
	//Load custom profiles
	exec("./assets/gui/editorGuiProfiles.cs");
	exec("./assets/gui/EditorMenu.gui");
	SandboxWindow.add(LevelEditorGui);
	//Custom GUI must be exec after defaults are set
	exec("./assets/gui/CreateModule.gui");
	exec("./assets/gui/SceneCreate.gui");
	exec("./assets/gui/ImageAssetImport.gui");
	exec("./assets/gui/PolyEditor.gui");
	exec("./assets/gui/ModuleLoad.gui");
	exec("./assets/gui/RectLayout.gui");
	%this.deactivateToolbar();
	ModuleLoadBttn.setActive(0);
	scanForModules();
	
	%csEditImage = new GuiSpriteCtrl(CSEditImage)
	{
		Image = "EditorToy:rectDefault";
		Position = "0 0";
		Extent = "100 100";
	};
	
	CSEditImageDisplay.add(%csEditImage);
	//Load editor functions for module and scene
	exec("./assets/scripts/Module.cs");
	exec("./assets/scripts/Scene.cs");
	
	//Custom Polyshape section
	new SceneWindow(PolyEditorWindow)
    {
        Profile = SandboxWindowProfile;
        Position = "0 0";
        Extent = "570 500";
    };
	
	new SceneWindow(ImagePreviewWindow)
	{
		Profile = SandboxWindowProfile;
		Position = "0 0";
		Extent = "570 500";
	};
	
	 new Scene(ImagePreviewScene);
	 
	 ImagePreviewWindow.setScene(ImagePreviewScene);
	 EditorToy.add(ImagePreviewWindow);
	
	//Set it to the scene.
    PolyEditorWindow.setScene( SandboxScene );
	EditorToy.add(PolyEditorWindow);
	
	//Editor user view
	EditorToy.aspectRatio = 0.0; 
	EditorToy.mouseMode = "default";
	EditorToy.oldPosition = "";
	EditorToy.newPosition = "";
	EditorToy.compEditMode = "select";
	EditorToy.compSpriteSelSprite = "";
	
	//Editor Movement Def.
	EditorToy.cameraSpeed = 1;
	EditorToy.objectSpeed = 1;
	
	//Module Defaults
	EditorToy.cmoduleName = "";
	EditorToy.moduleName = "";
	EditorToy.moduleDesc = "";
	EditorToy.selectedModule = "";
	EditorToy.activeModule = "";
	
	//SceneDefaults
	EditorToy.sceneCameraX = 16;
	EditorToy.sceneCameraY = 9;
	EditorToy.sceneGravityX = 0;
	EditorToy.sceneGravityY = -9.8;
	EditorToy.scenePosIter = 3;
	EditorToy.sceneVelIter = 8;
	EditorToy.sceneName = "";
	EditorToy.activeScene = null;
	EditorToy.sceneState = "pause";
	
	//Set Polylist variables
	EditorToy.selObject = null;
	EditorToy.drawMode = false;
	EditorToy.drawType = "default";
	EditorToy.polyListPosLocal = "";
	EditorToy.polyListPosWorld = "";
	
	//Set ImageAsset Defaults
	EditorToy.imageAName = "default";
	EditorToy.imageAFile = "default";
	EditorToy.cellH = 0;
	EditorToy.cellW = 0;
	EditorToy.filterMode = "Nearest";
	EditorToy.cellNX = 0;
	EditorToy.cellNY = 0;
	EditorToy.cellStrideX = 0;
	EditorToy.cellStrideY = 0;
	EditorToy.cellOffX = 0;
	EditorToy.cellOffY = 0;
	EditorToy.force16Bit = 0;
	EditorToy.cellRowOrder = true;
	EditorToy.showFrames = true;
	
	//Set AnimationAsset Defaults
	EditorToy.animationName = "default";
	EditorToy.animationImage = "default";
	EditorToy.animationFrame = "";
	EditorToy.animationTime = 1.0;
	EditorToy.animationLoop = false;
	EditorToy.animationRand = false;
	
	
	//SetSpriteDefaults
	EditorToy.selSprite = null;
	EditorToy.spriteName = "";
	EditorToy.spriteClass = "";
	EditorToy.spritePosX = 0.0;
	EditorToy.spritePosY = 0.0;
	EditorToy.spriteWidth = 0.0;
	EditorToy.spriteHeight = 0.0;
	EditorToy.spriteFlipX = false;
	EditorToy.spriteFlipY = false;
	EditorToy.spriteAngle = 0;
	EditorToy.spriteFixedAngle = false;
	EditorToy.spriteAngVel = 0;
	EditorToy.spriteAngDamp = 0;
	EditorToy.spriteLinVelX = 0;
	EditorToy.spriteLinVelY = 0;
	EditorToy.spriteLinVelPolAngle = 0;
	EditorToy.spriteLinVelPolSpeed = 0;
	EditorToy.spriteLinDamp = 0;
	EditorToy.spriteDefDensity = 0;
	EditorToy.spriteDefFriction = 0;
	EditorToy.spriteDefRestitution = 0;
	EditorToy.spriteCollOne = false;
	EditorToy.spriteCollSupp = false;
	EditorToy.spriteBody = 0;
	EditorToy.spriteCollShapeCount = 0;
	EditorToy.spriteFrame = 0;
	EditorToy.spriteGravity = 1.0;
	EditorToy.spriteSceneLayer = 0;
	EditorToy.spriteSceneGroup = 0;
	EditorToy.spriteCollLayers = "0";
	EditorToy.spriteCollGroups = "0";
	EditorToy.spriteAlphaTest = 0.0;
	EditorToy.spriteBlendMode = false;
	EditorToy.spriteSrcBlend = 0.0;
	EditorToy.spriteDestBlend = 0.0;
	EditorToy.spriteBlendR = 0.0;
	EditorToy.spriteBlendG = 0.0;
	EditorToy.spriteBlendB = 0.0;
	EditorToy.spriteBlendA = 0.0;
	
	//Sprite Collision Layers
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
	
	//Sprite collision groups
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
	
	//CompositeSprite Defaults
	EditorToy.compRectXNum = 16;
	EditorToy.compRectYNum = 9;
	EditorToy.compBatchLayout = "rect";
	EditorToy.compBatchSortMode = "z";
	EditorToy.compSpriteIsolated = false;
	EditorToy.compSpriteCull = false;
	EditorToy.compDefSpriteStrideX = 1;
	EditorToy.compDefSpriteStrideY = 1;
	EditorToy.compDefSpriteWidth = 1;
	EditorToy.compDefSpriteHeight= 1;
	EditorToy.compBatchIsolate = false;
	EditorToy.compBatchCull = false;
	EditorToy.selCompSprite = null;
	EditorToy.compSpriteName = "";
	EditorToy.compSpriteClass = "";
	EditorToy.compSpritePosX = 0.0;
	EditorToy.compSpritePosY = 0.0;
	EditorToy.compSpriteFlipX = false;
	EditorToy.compSpriteFlipY = false;
	EditorToy.compSpriteAngle = 0;
	EditorToy.compSpriteFixedAngle = false;
	EditorToy.compSpriteAngVel = 0;
	EditorToy.compSpriteAngDamp = 0;
	EditorToy.compSpriteLinVelX = 0;
	EditorToy.compSpriteLinVelY = 0;
	EditorToy.compSpriteLinVelPolAngle = 0;
	EditorToy.compSpriteLinVelPolSpeed = 0;
	EditorToy.compSpriteLinDamp = 0;
	EditorToy.compSpriteDefDensity = 0;
	EditorToy.compSpriteDefFriction = 0;
	EditorToy.compSpriteDefRestitution = 0;
	EditorToy.compSpriteCollOne = false;
	EditorToy.compSpriteCollSupp = false;
	EditorToy.compSpriteBody = 0;
	EditorToy.compSpriteCollShapeCount = 0;
	EditorToy.compSpriteFrame = 0;
	EditorToy.compSpriteGravity = 1.0;
	EditorToy.compSpriteSceneLayer = 0;
	EditorToy.compSpriteSceneGroup = 0;
	EditorToy.compSpriteCollLayers = "0";
	EditorToy.compSpriteCollGroups = "0";
	EditorToy.compSpriteAlphaTest = 0.0;
	EditorToy.compSpriteBlendMode = false;
	EditorToy.compSpriteSrcBlend = 0.0;
	EditorToy.compSpriteDestBlend = 0.0;
	EditorToy.compSpriteBlendR = 0.0;
	EditorToy.compSpriteBlendG = 0.0;
	EditorToy.compSpriteBlendB = 0.0;
	EditorToy.compSpriteBlendA = 0.0;
	
	//CompSprite Edit values
	EditorToy.cssName = "";
	EditorToy.cssFrame = 0;
	EditorToy.cssSizeX = 0;
	EditorToy.cssSizeY = 0;
	EditorToy.cssLocPosX = 0;
	EditorToy.cssLocPosY = 0;
	EditorToy.cssLogPosX = 0;
	EditorToy.cssLogPosY = 0;
	EditorToy.cssAng = 0;
	EditorToy.cssFlipX = false;
	EditorToy.cssFlipY = false;
	EditorToy.cssAlpha = 0;
	EditorToy.cssBlendAlpha = 0;
	EditorToy.cssBlendR = 0;
	EditorToy.cssBlendG	= 0;
	EditorToy.cssBlendB = 0;
	EditorToy.cssBlendMode = false;
	EditorToy.cssSrcBlendFac = 0;
	EditorToy.cssDstBlendFac = 0;
	EditorToy.editImage = "EditorToy:rectDefault";
	EditorToy.editImageFrame = 0;
	EditorToy.selectedCss = "";
	
	//Sprite Collision Layers
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
	
	//Sprite collision groups
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
	
	//CompositeSprite Defaults
	EditorToy.selScroller = null;
	EditorToy.scrollerName = "";
	EditorToy.scrollerClass = "";
	EditorToy.scrollerPosX = 0.0;
	EditorToy.scrollerPosY = 0.0;
	EditorToy.scrollerWidth = 0.0;
	EditorToy.scrollerHeight = 0.0;
	EditorToy.scrollerSpeedX = 0.0;
	EditorToy.scrollerSpeedY = 0.0;
	EditorToy.scrollerScrollRepX = 0.0;
	EditorToy.scrollerScrollRepY = 0.0;
	EditorToy.scrollerAngle = 0;
	EditorToy.scrollerFixedAngle = false;
	EditorToy.scrollerAngVel = 0;
	EditorToy.scrollerAngDamp = 0;
	EditorToy.scrollerLinVelX = 0;
	EditorToy.scrollerLinVelY = 0;
	EditorToy.scrollerLinVelPolAngle = 0;
	EditorToy.scrollerLinVelPolSpeed = 0;
	EditorToy.scrollerLinDamp = 0;
	EditorToy.scrollerDefDensity = 0;
	EditorToy.scrollerDefFriction = 0;
	EditorToy.scrollerDefRestitution = 0;
	EditorToy.scrollerCollOne = false;
	EditorToy.scrollerCollSupp = false;
	EditorToy.scrollerBody = 0;
	EditorToy.scrollerCollShapeCount = 0;
	EditorToy.scrollerFrame = 0;
	EditorToy.scrollerGravity = 1.0;
	EditorToy.scrollerSceneLayer = 0;
	EditorToy.scrollerSceneGroup = 0;
	EditorToy.scrollerCollLayers = "0";
	EditorToy.scrollerCollGroups = "0";
	EditorToy.scrollerAlphaTest = 0.0;
	EditorToy.scrollerBlendMode = false;
	EditorToy.scrollerSrcBlend = 0.0;
	EditorToy.scrollerDestBlend = 0.0;
	EditorToy.scrollerBlendR = 0.0;
	EditorToy.scrollerBlendG = 0.0;
	EditorToy.scrollerBlendB = 0.0;
	EditorToy.scrollerBlendA = 0.0;
	
	//Sprite Collision Layers
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
	
	//Sprite collision groups
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
	
	//create directory
	/*
	if(!isDirectory("./Output/1/"))
	createPath("./Output/1/");
	*/
	//copy files
	/*
	%fromName = "./main.cs";
	%toName = "./Output/main.cs";
	pathCopy(%fromName,%toName);*/
	//get file extension
	/*%extension = fileExt(%fromName);
	echo("file extension = ", %extension);
	//File name without extension
	%extension2 = fileBase(%fromName);
	echo("file without extension = ", %extension2);
	//Asset type data
	%assetId = "ToyAssets:CrossHair1";
	echo("Asset type = ", AssetDatabase.getAssetType(%assetId) );
	*/
	
    //Create our menus here
	%this.createModuleMenu();
	%this.createSceneMenu();
	%this.createModuleLoadMenu();
	%this.createImageAssetMenu();
	%this.createPolyEditorMenu();
	%this.createRectLayoutMenu();
	//%this.createSimSets();
	// Reset the toy.
	//Remove default input listener
	SandboxWindow.removeInputListener( Sandbox.InputController ); 
	
    %this.reset();
}

function EditorToy::Init_controls(%this)
{
	new ActionMap(EditorControls);
	
	EditorControls.bindCmd(keyboard, "delete", "EditorToy.deleteObject();", "");
}

function scanForModules()
{
	// Find the toy modules.
	ModuleDatabase.scanModules("^EditorToy/projects/");
	
    %editorModules = ModuleDatabase.findModuleTypes( "EditorModule", false );
    // Do we have an existing set of sandbox toys?
    if ( !isObject(EditorModules) )
    {
        // No, so create one.
        new SimSet(EditorModules);
    }
    // Clear the EditorModules.
    EditorModules.clear();
    
    // Fetch module count.
    %moduleCount = getWordCount( %editorModules );
    
    // Add modules.
    for ( %i = 0; %i < %moduleCount; %i++ )
    {
        // Fetch module definition.
        %moduleDefinition = getWord( %editorModules, %i );
        // Add to EditorModules sandbox toys.
		%moduleTitle = %moduleDefinition.moduleId;
		if(EditorToy.moduleName $= %moduleTitle)
		{
			EditorToy.activeModule = %moduleDefinition;
		}
        EditorModules.add( %moduleDefinition );       
    }
	
	%count = EditorModules.getCount();
	if(%count > 0)
	{
		EditorToy.activateModuleLoadBttn();
	}
	ModuleList.update();
}

function EditorToy::destroy(%this)
{
	//Just in case window is closed while creating
	//a collision shape
	%this.resetPolyListPosLocal();
	//SaveScene if there is one
	%scene = EditorToy.activeScene;
	if(%scene != "")
	{
		EditorToy.saveScene();
	}
	%mName = EditorToy.moduleName;
	if(%mName !$= "")
	{
		EditorToy.saveModule();
	}
	EditorControls.pop();
	EditorControls.delete();
	//Remove our gui menus
	SandboxWindow.remove(ImageAssetMenu);
    // Deactivate the package.
    deactivatePackage( EditorToyPackage );
}

function EditorToy::reset(%this)
{   
    // Clear the scene.
    SandboxScene.clear();
	
    %extent = Canvas.extent;
    // Set a typical Earth gravity.
    SandboxScene.setGravity( 0, 0);
	SandboxWindow.remove(MainOverlay);
    // Camera Configuration
    SandboxWindow.setCameraPosition( 0 , 0 );
    SandboxWindow.setCameraAngle( 0 );
    SandboxWindow.setCameraSize( %extent.x , %extent.y );
	
	%this.Init_controls();
	
	EditorControls.push();
	
	/*Setting custom cursor
	Should probably be done in guiprofiles
	if(!isObject(CustomCursor)) new GuiCursor(CustomCursor)
	{
		hotspost = "1 1";
		bitmapName = "^EditorToy/assets/gui/images/ModuleBttn";
	};
	Canvas.setCursor(CustomCursor);*/
	
	%this.aspectRatio = %extent.x / %extent.y;
	
	// Create background.
    //%this.createBackground();
	
	//Create composite
	//%this.createComposite();
	
	// Create pick cursor.
    //%this.createCursor();
	
	//%this.testObject();
}
/*CREATE SIM SETS
function EditorToy::createSimSets(%this)
{
	%scene = EditorToy.activeScene;
	if(!isObject (SpriteSim))
	{
		%spriteSim = new SimSet(SpriteSim);
	}
	
	if(!isObject (CompositeSim))
	{
		%compositeSim = new SimSet(CompositeSim);
	}
	
	if(!isObject (ScrollerSim))
	{
		%scrollerSim = new SimSet(ScrollerSim);
	}
	
}

function EditorToy::deleteSimSets(%this)
{
	if(isObject (SpriteSim))
	{
		SpriteSim.delete();
	}
	
	if(isObject (CompositeSim))
	{
		CompositeSim.delete();
	}
	
	if(isObject (ScrollerSim))
	{
		ScrollerSim.delete();
	}
}

function EditorToy::populateSim(%this)
{
	%scene = %this.activeScene;
	%count = %scene.getCount();
	%list = %scene.getSceneObjectList();
	for(%i = 0; %i < %count; %i++)
	{
		%obj = getWord(%list, %i);
		%class = %obj.getClassName();
		if(%class $= "Sprite")
		{
			SpriteSim.add(%obj);
		}
		else if(%class $= "CompositeSprite")
		{
			CompositeSim.add(%obj);
		}
		else if(%class $= "Scroller")
		{
			ScrollerSim.add(%obj);
		}
	}
}
*/

function EditorToy::createAssetSims(%this)
{
	if(!isObject (AnimationSim))
	{
		%animSim = new SimSet(AnimationSim);
	}
	
	if(!isObject (ImageSim))
	{
		%imageSim = new SimSet(ImageSim);
	}
	
	if(!isObject (ParticleSim))
	{
		%particleSim = new SimSet(ParticleSim);
	}
}

function EditorToy::deleteAssetSims(%this)
{
	if(isObject (AnimationSim))
	{
		AnimationSim.delete();
	}
	
	if(isObject (ImageSim))
	{
		ImageSim.delete();
	}
	
	if(isObject (ParticleSim))
	{
		ParticleSim.delete();
	}
}
//-----------------------------------------------------------------------------
function EditorToy::createCursor(%this)
{
	// Create the sprite.
    %object = SandboxScene.create( Sprite );
    %object.Size = "10 10";
	%object.SetBodyType( static );
    %object.BlendColor = White;
	%object.Position = "0 0";
    %object.PickingAllowed = false;
    %object.Image = "ToyAssets:CrossHair1";
	//check what type of object we have
	if (%object.isMemberOfClass(Sprite))
	{
		echo("yes member of sprite");
	}
	//check what class the object is
	%className = %object.getClassName();
	echo(%className);
	//dump all fields
	// %object.dump();
	/*
	Dump all static fields
	%count = %object.getFieldCount();
	for (%i = 0; %i < %count; %i++)
	{
		%fieldName = %object.getField(%i);
		%fieldValue = %object.getFieldValue(%fieldName);
		echo(%fieldName @ " = " @ %fieldValue);
	}*/

	// Set the cursor.
    EditorToy.CursorObject = %object;
}
//-----------------------------------------------------------------------------
function EditorToy::createBackground(%this)
{    
    // Create the scroller.
    %object = new Scroller();
    
    // Set the sprite as "static" so it is not affected by gravity.
    %object.setBodyType( static );
       
    // Always try to configure a scene-object prior to adding it to a scene for best performance.

    // Set the position.
    %object.Position = "0 0";

    // Set the size.        
    %object.Size = "100 100";
    %object.PickingAllowed = false;
    // Set to the furthest background layer.
    %object.SceneLayer = 31;
    // Set an image.
    %object.Image = "ToyAssets:checkered";
    // Set the blend color.
    %object.BlendColor = LightSteelBlue;
    // Add the sprite to the scene.
    SandboxScene.add( %object );
}

//-----------------------------------------------------------------------------
//Handle Mouse Events
function PolyEditorWindow::onTouchDown(%this, %touchID, %worldPosition)
{
	
	%drawMode = EditorToy.drawMode;
	%scene = EditorToy.activeScene;
	
	// Pick an object.
    %picked = %scene.pickPoint( %worldPosition );
	%pickCount = %picked.Count;
	//loop through each object and check for a "PolyHandle"
	//so we don't draw 100 polyhandles in the same place
	//when all we are trying to do is move it!
	for(%n = 0; %n < %pickCount; %n++)
	{
		%pickedObject = getWord(%picked, %n);
		if(%pickedObject.getClassNamespace() $= "PolyHandle")
		{
			%scene.SelectedObject[%touchID] = %pickedObject;
			echo("found a polyhandle");
			break;
		}
		else
		{
			%pickedObject = getWord(%picked, 0);
			%scene.SelectedObject[%touchID] = %pickedObject;
		}
	}
	%name = %scene.SelectedObject[%touchID].getClassNamespace();
	if(%name !$= "PolyHandle")
	{
		if(%drawMode == true)
		{
			EditorToy.createPolylistItem(%worldPosition);
			EditorToy.createLine();
		}
	}
    
	
}

function PolyEditorWindow::onTouchDragged(%this, %touchID, %worldPosition)
{
	%scene = EditorToy.activeScene;
	if ( isObject(%scene.SelectedObject[%touchID]) )
	{
	%name = %scene.SelectedObject[%touchID].getClassNamespace();
		if(%name $= "PolyHandle")
		{
			%scene.SelectedObject[%touchID].Position = %worldPosition;
			EditorToy.createLine();
		}
	}
	else
	{
		// Store the touch event.
		%this.NewTouchPosition[%touchId] = %worldPosition;
	}
}

function PolyEditorWindow::onTouchUp(%this, %touchID, %worldPosition)
{
    // Sanity!
    if ( %this.TouchEventActive[%touchId] == false )
        return;
	
    // Reset previous touch.
    %this.OldTouchPosition[%touchId] = "";
    
    // Reset current touch.
    %this.NewTouchPosition[%touchId] = "";
    
    // Flag event as inactive.
    %this.TouchEventActive[%touchId] = false;

    // Remove the touch Id.
    if ( %this.PreviousTouchId == %touchId )
    {
         %this.PreviousTouchId = "";
    }
    if ( %this.CurrentTouchId == %touchId )
    {
         %this.CurrentTouchId = %this.PreviousTouchId;
         %this.PreviousTouchId = "";
    }

    // Decrease event count.
    %this.TouchEventCount--;
}

function EditorToy::onTouchDown(%this, %touchID, %worldPosition)
{
	// Pick an object.
	%mode = EditorToy.mouseMode;
	%editMode = EditorToy.compEditMode;
	%scene = EditorToy.activeScene;
	%picked = %scene.pickPoint( %worldPosition );
	if(CameraPopout.isVisible())
	{
		CameraPopout.setVisible(0);
	}
	
	if(SceneSettingsPopout.isVisible())
	{
		SceneSettingsPopout.setVisible(0);
	}
	
	if(CompSpritePopout.isVisible())
	{
		CompSpritePopout.setVisible(0);
	}
	
	if(%mode $= "compositeEdit")
	{
		// Fetch the composite sprite.
		%compositeSprite = EditorToy.selObject;
		
		// Pick sprites.
		%sprites = %compositeSprite.pickPoint( %worldPosition );    

		// Fetch sprite count.    
		%spriteCount = %sprites.count;
		
		// Finish if no sprites picked.
		if ( %spriteCount == 0 )
			return;    
		if(%editMode $= "select")
		{
			%spriteId = getWord( %sprites, 0 );
			
			%compositeSprite.selectSpriteId( %spriteId );
			EditorToy.selectedCss = %spriteId;
			echo(%spriteId);
			%this.createCompSpriteSpriteMenu();
		} 
		else if(%editMode $= "delete")
		{
			// Fetch sprite Id.
			%spriteId = getWord( %sprites, 0 );
			
			// Select the sprite Id.
			%compositeSprite.selectSpriteId( %spriteId );
			
			// Remove the selected sprite
			%compositeSprite.removeSprite();
		}
		else if(%editMode $= "paint")
		{
			//Initializers
			%mName = EditorToy.moduleName;
			%img = EditorToy.editImage;
			%imgFrame = EditorToy.editImageFrame;
			
			//Fetch Sprite
			%spriteId = getWord( %sprites, 0);
			
			//Select Sprite
			%compositeSprite.selectSpriteId( %spriteId);
			
			%compositeSprite.setSpriteImage(%mName @ ":" @ %img , %imgFrame);
		}
	
	}
	else
	{
		// Finish if nothing picked.
		if ( %picked $= "" )
		{
			%scene.SelectedObject[%touchID] = "";
			%pickedObject = "";
			//make sure menus are removed when no object is selected
			%this.createObjectMenu(%pickedObject);
			%this.targetPos = %worldPosition;
			return;
		}
		// Fetch the pick count.
		%pickCount = %picked.Count;
		// Fetch the picked object.
		%pickedObject = getWord( %picked, 0);
		if(%pickCount > 1)
		{
			//Loop through each object at pickpoint
			for(%n = 0; %n < %pickCount; %n++)
			{
				%pickedObject = getWord(%picked, %n);
				//If picked object is already selected choose the next
				if(%pickedObject $= EditorToy.selObject)
				{
					%pickedObject = getWord(%picked, %n + 1);
					break;
				}
			}
			//If there is no higher value revert to first object
			if(%pickedObject $= "")
			{
				%pickedObject = getWord(%picked, 0);
			}
		}
		//%pos = %pickedObject.getPosition();
		%scene.SelectedObject[%touchID] = %pickedObject;
		%this.createObjectMenu(%pickedObject);
	}		
}

function EditorToy::onTouchDragged(%this, %touchID, %worldPosition)
{		
    // Update cursor position.
    //EditorToy.CursorObject.Position = %worldPosition;
	%scene = EditorToy.activeScene;
	%mode = EditorToy.mouseMode;
	if(%mode $= "compositeEdit")
		return;
	
	if ( isObject(%scene.SelectedObject[%touchID]) )
	{
		//we dont want to pan around while we have an object selected.
		%scene.SelectedObject[%touchID].Position = %worldPosition;
	}
}

function EditorToy::onTouchUp(%this, %touchID, %worldPosition)
{
	
	
	// Sanity!
    /*if ( %this.TouchEventActive[%touchId] == false )
        return;
	*/
	%scene = EditorToy.activeScene;
	//This has to be done to update selected objects position setting
	if ( isObject(%scene.SelectedObject[%touchID]) )
	{
		%pickedObject = %scene.SelectedObject[%touchID];
		%this.createObjectMenu(%pickedObject);
		continue;
	}
	
    // Reset previous touch.
    %this.OldTouchPosition[%touchId] = "";
    
    // Reset current touch.
    %this.NewTouchPosition[%touchId] = "";
    
    // Flag event as inactive.
    %this.TouchEventActive[%touchId] = false;

    // Remove the touch Id.
    if ( %this.PreviousTouchId == %touchId )
    {
         %this.PreviousTouchId = "";
    }
    if ( %this.CurrentTouchId == %touchId )
    {
         %this.CurrentTouchId = %this.PreviousTouchId;
         %this.PreviousTouchId = "";
    }

    // Decrease event count.
    %this.TouchEventCount--;
	
}

function EditorToy::onMiddleMouseDown(%this, %touchID, %worldPosition)
{
	%windowPosition = SandboxWindow.getWindowPoint(%worldPosition);
	EditorToy.oldPosition = %windowPosition;	
	EditorToy.newPosition = %windowPosition;
}

function EditorToy::onMiddleMouseDragged(%this, %touchID, %worldPosition)
{
	echo(%touchId @ " touch dragged " @ %worldPosition);
	
	%windowPosition = SandboxWindow.getWindowPoint(%worldPosition);
	
	EditorToy.oldPosition = EditorToy.newPosition;
	echo("old position" @ EditorToy.oldPosition);
	
	EditorToy.newPosition = %windowPosition;
	
	echo("new position" @ EditorToy.newPosition);
	
	%panOffset = Vector2Sub( EditorToy.newPosition , EditorToy.oldPosition);
	
	%panOffset = Vector2InverseY( %panOffset );
	
	%panOffset = Vector2Mult(%panOffset, SandboxWindow.getCameraWorldScale());
	
	SandboxWindow.setCameraPosition(Vector2Sub(SandboxWindow.getCameraPosition(), %panOffset));
	
}

function EditorToy::onMouseWheelUp(%this)
{
	SandboxWindow.setCameraZoom( SandboxWindow.getCameraZoom() + 0.1);
}

function EditorToy::onMouseWheelDown(%this)
{
	%zoom = SandboxWindow.getCameraZoom();
	
	if(%zoom > 0.1)
	{
		SandboxWindow.setCameraZoom( SandboxWindow.getCameraZoom() - 0.1);
	}
	else
	{
		SandboxWindow.setCameraZoom(0.1);
	}
}

//Create Editor window
function EditorToy::createEditorMenu(%this)
{	
	//Sandbox.add( TamlRead("./assets/gui/EditorMenu.gui.taml") );
	exec("./assets/gui/EditorMenu.gui");
	SandboxWindow.add(LevelEditorGui);
}

function EditorToy::hideObjMenus(%this)
{
	SpriteAssetMenu.setVisible(0);
	ScrollerAssetMenu.setVisible(0);
	CompSpriteAssetMenu.setVisible(0);
}

function EditorToy::createCompSpriteSpriteMenu(%this)
{
	%compositeSprite = EditorToy.selObject;
	
	CompSpriteSpriteMenu.setVisible(1);
	
	//Handle Initializers
	%cssName = %compositeSprite.getSpriteName();
	%cssFrame = %compositeSprite.getSpriteImageFrame();
	%cssLocPos = %compositeSprite.getSpriteLocalPosition();
	%cssLogPos = %compositeSprite.getSpriteLogicalPosition();
	%cssSize = %compositeSprite.getSpriteSize();
	%cssAng = %compositeSprite.getSpriteAngle();
	%cssFlipX = %compositeSprite.getSpriteFlipX();
	%cssFlipY = %compositeSprite.getSpriteFlipY();
	%cssAlphaTest = %compositeSprite.getSpriteAlphaTest();
	%cssBlendAlpha = %compositeSprite.getSpriteBlendAlpha();
	%cssBlendCol = %compositeSprite.getSpriteBlendColor();
	%blendR = getWord(%cssBlendCol,0);
	%blendG = getWord(%cssBlendCol,1);
	%blendB = getWord(%cssBlendCol,2);
	%cssBlendMode = %compositeSprite.getSpriteBlendMode();
	%cssSrcBlendFac = %compositeSprite.getSpriteSrcBlendFactor();
	%cssDstBlendFac = %compositeSprite.getSpriteDstBlendFactor();
	%cssWidth = getWord(%cssSize, 0);
	%cssHeight = getWord(%cssSize,1);
	
	//Update
	%this.updateCssName(%cssName);
	%this.updateCssFrame(%cssFrame);
	%this.updateCssLocalPositionX(%cssLocPos.x);
	%this.updateCssLocalPositionY(%cssLocPos.y);
	%this.updateCssLogicalPositionX(%cssLogPos.x);
	%this.updateCssLogicalPositionY(%cssLogPos.Y);
	%this.updateCssSizeWidth(%cssWidth);
	%this.updateCssSizeHeight(%cssHeight);
	%this.updateCssAngle(%cssAng);
	%this.updateCssFlipX(%cssFlipX);
	%this.updateCssFlipY(%cssFlipY);
	%this.updateCssAlphaTest(%cssAlphaTest);
	%this.updateCssBlendAlpha(%cssBlendAlpha);
	%this.updateCssBlendR(%blendR);
	%this.updateCssBlendG(%blendG);
	%this.updateCssBlendB(%blendB);
	%this.updateCssBlendMode(%cssBlendMode);
	%this.updateCssSrcBlendFactor(%cssSrcBlendFac);
	%this.updateCssDstBlendFactor(%cssDstBlendFac);
	
	//Update GUI
	CSSName.update();
	CSSFrame.update();
	CSSPosX.update();
	CSSPosY.update();
	CSSHeight.update();
	CSSWidth.update();
	CSSAngle.update();
	CSSFlipX.update();
	CSSFlipY.update();
	CSSSrcBlendList.update();
	CSSDstBlendList.update();
	CSSBlendMode.update();
	CSSAlphaTest.update();
	CSSBlendA.update();
}

function EditorToy::createObjectMenu(%this, %obj)
{
	//get the class name eg. sprite, compositiesprite, scroller.
	if(%obj != "")
	{
		%className = %obj.getClassName();
	}
	
	if(%obj != %this.selObject)
	{
		%obj.setDebugOn("OOBB");
		%this.selObject.setDebugOff("OOBB");
	}
	
	if(%className !$= "Sprite")
	{
		SpriteAssetMenu.setVisible(0);
	}
	if(%className !$= "Scroller")
	{
		ScrollerAssetMenu.setVisible(0);
	}
	if(%className !$= "CompositeSprite")
	{
		CompSpriteAssetMenu.setVisible(0);
	}
	//Do this after we hide all menus
	if(%obj $= "")
	{
		%this.selObject = "null";
		return;
	}
	if(%className $= "Sprite")
	{
		%this.selObject = %obj;
		SpriteAssetMenu.setVisible(1);
		//Initializers
		%name = %obj.getName();
		%class = %obj.getClassNamespace();
		%pos = %obj.getPosition();
		%width = %obj.getWidth();
		%height = %obj.getHeight();
		%flipX = %obj.getFlipX();
		%flipY = %obj.getFlipY();
		%body = %obj.getBodyType();
		%ang = %obj.getAngle();
		%fixAng = %obj.getFixedAngle();
		%angDam = %obj.getAngularDamping();
		%angVel = %obj.getAngularVelocity();
		%linVelX = %obj.getLinearVelocityX();
		%linVelY = %obj.getLinearVelocityY();
		%linVelPol = %obj.getLinearVelocityPolar();
		%linVelPolAng = getWord(%linVelPol, 0);
		%linVelPolSpeed = getWord(%linVelPol, 1);
		%linDam = %obj.getLinearDamping();
		%defDen = %obj.getDefaultDensity();
		%defFri = %obj.getDefaultFriction();
		%defRes = %obj.getDefaultRestitution();
		%collSupp = %obj.getCollisionSuppress();
		%collOne = %obj.getCollisionOneWay();
		%colNum = %obj.getCollisionShapeCount();
		%frame = %obj.getImageFrame();
		%grav = %obj.getGravityScale();
		%sceneLay = %obj.getSceneLayer();
		%sceneGroup = %obj.getSceneGroup();
		%collLayers = %obj.getCollisionLayers();
		%collGroups = %obj.getCollisionGroups();
		%alphaTest = %obj.getAlphaTest();
		%blendMode = %obj.getBlendMode();
		%srcBlend = %obj.getSrcBlendFactor();
		%dstBlend = %obj.getDstBlendFactor();
		%blendCol = %obj.getBlendColor();
		%blendR = getWord(%blendCol,0);
		%blendG = getWord(%blendCol,1);
		%blendB = getWord(%blendCol,2);
		%blendA = %obj.getBlendAlpha();
		
		//Update our defaults
		%this.updateSelSprite(%obj);
		%this.updateSpritePosX(%pos.x);
		%this.updateSpritePosY(%pos.y);
		%this.updateSpriteHeight(%height);
		%this.updateSpriteWidth(%width);
		%this.updateSpriteFlipX(%flipX);
		%this.updateSpriteFlipY(%flipY);
		%this.updateSpriteBody(%body);
		%this.updateSpriteName(%name);
		%this.updateSpriteClass(%class);
		%this.updateSpriteAngle(%ang);
		%this.updateSpriteFixedAngle(%fixAng);
		%this.updateSpriteAngDamp(%angDam);
		%this.updateSpriteAngVel(%angVel);
		%this.updateSpriteLinVelX(%linVelX);
		%this.updateSpriteLinVelY(%linVelY);
		%this.updateSpriteLinVelPolAngle(%linVelPolAng);
		%this.updateSpriteLinVelPolSpeed(%linVelPolSpeed);
		%this.updateSpriteLinDamp(%linDam);
		%this.updateSpriteDefDensity(%defDen);
		%this.updateSpriteDefFriction(%defFri);
		%this.updateSpriteDefRestitution(%defRes);
		%this.updateSpriteCollSupp(%collSupp);
		%this.updateSpriteCollOne(%collOne);
		%this.updateSpriteCollShapeCount();
		%this.updateSpriteFrame(%frame);
		%this.updateSpriteGravity(%grav);
		%this.updateSpriteSceneLayer(%sceneLay);
		%this.updateSpriteSceneGroup(%sceneGroup);
		%this.updateSpriteCollLayers(%collLayers);
		%this.updateSpriteCollGroups(%collGroups);
		%this.loadSpriteCollLayerArray();
		%this.loadSpriteCollGroupArray();
		%this.updateSpriteAlphaTest(%alphaTest);
		%this.updateSpriteBlendMode(%blendMode);
		%this.updateSpriteSrcBlend(%srcBlend);
		%this.updateSpriteDstBlend(%dstBlend);
		%this.updateSpriteBlendR(%blendR);
		%this.updateSpriteBlendG(%blendG);
		%this.updateSpriteBlendB(%blendB);
		%this.updateSpriteBlendA(%blendA);
		
		//Update our gui
		SpriteName.update();
		SpriteClass.update();
		SpriteAngle.update();
		SpriteFixedAngle.update();
		SpriteAngularDamp.update();
		SpriteAngularVel.update();
		SpriteLinearVelX.update();
		SpriteLinearVelY.update();
		SpriteLinearVelPolAngle.update();
		SpriteLinearVelPolSpeed.update();
		SpriteLinearDamp.update();
		SpriteDefDensity.update();
		SpriteDefFriction.update();
		SpriteDefRestitution.update();
		SpriteCollSuppress.update();
		SpriteCollOneWay.update();
		SpritePosX.update();
		SpritePosY.update();
		SpriteWidth.update();
		SpriteHeight.update();
		SpriteFlipX.update();
		SpriteFlipY.update();
		SpriteBodyList.update();
		%this.updateSpriteCollGui();
		SpriteFrame.update();
		SpriteGravity.update();
		SpriteSceneLayer.update();
		SpriteSceneGroup.update();
		SpriteAlphaTest.update();
		SpriteBlendMode.update();
		SpriteBlendA.update();
		SpriteSrcBlendList.update();
		SpriteDstBlendList.update();
	}
	
	else if(%className $= "CompositeSprite")
	{
		//Initializers
		CompSpriteAssetMenu.setVisible(1);
		%this.selObject = %obj;
		%pos = %obj.getPosition();
		%width = %obj.getWidth();
		%height = %obj.getHeight();
		%sceneLay = %obj.getSceneLayer();
		%sceneGroup = %obj.getSceneGroup();
		%batchCull = %obj.getBatchCulling();
		%batchIsolate = %obj.getBatchIsolated();
		%batchLayout = %obj.getBatchLayout();
		%batchSort = %obj.getBatchSortMode();
		//Handle sprite size and separate
		%spriteSize = %obj.getDefaultSpriteSize();
		%spriteSizeX = getWord(%spriteSize,0);
		%spriteSizeY = getWord(%spriteSize,1);
		//Handle stride and separate
		%spriteStride = %obj.getDefaultSpriteStride();
		%spriteStrideX = getWord(%spriteStride,0);
		%spriteStrideY = getWord(%spriteStride,1);
		
		//Initializers
		%name = %obj.getName();
		%class = %obj.getClassNamespace();
		%pos = %obj.getPosition();
		%body = %obj.getBodyType();
		%ang = %obj.getAngle();
		%fixAng = %obj.getFixedAngle();
		%angDam = %obj.getAngularDamping();
		%angVel = %obj.getAngularVelocity();
		%linVelX = %obj.getLinearVelocityX();
		%linVelY = %obj.getLinearVelocityY();
		%linVelPol = %obj.getLinearVelocityPolar();
		%linVelPolAng = getWord(%linVelPol, 0);
		%linVelPolSpeed = getWord(%linVelPol, 1);
		%linDam = %obj.getLinearDamping();
		%defDen = %obj.getDefaultDensity();
		%defFri = %obj.getDefaultFriction();
		%defRes = %obj.getDefaultRestitution();
		%collSupp = %obj.getCollisionSuppress();
		%collOne = %obj.getCollisionOneWay();
		%colNum = %obj.getCollisionShapeCount();
		%grav = %obj.getGravityScale();
		%sceneLay = %obj.getSceneLayer();
		%sceneGroup = %obj.getSceneGroup();
		%collLayers = %obj.getCollisionLayers();
		%collGroups = %obj.getCollisionGroups();
		%alphaTest = %obj.getAlphaTest();
		%blendMode = %obj.getBlendMode();
		%srcBlend = %obj.getSrcBlendFactor();
		%dstBlend = %obj.getDstBlendFactor();
		%blendCol = %obj.getBlendColor();
		%blendR = getWord(%blendCol,0);
		%blendG = getWord(%blendCol,1);
		%blendB = getWord(%blendCol,2);
		%blendA = %obj.getBlendAlpha();
		
		//Update our defaults
		%this.updateSelCompSprite(%obj);
		%this.updateCompSpritePosX(%pos.x);
		%this.updateCompSpritePosY(%pos.y);
		%this.updateCompSpriteBody(%body);
		%this.updateCompSpriteName(%name);
		%this.updateCompSpriteClass(%class);
		%this.updateCompSpriteAngle(%ang);
		%this.updateCompSpriteFixedAngle(%fixAng);
		%this.updateCompSpriteLayout(%batchLayout);
		%this.updateCompBatchSortMode(%batchSort);
		%this.updateCompSpriteIsolated(%batchSort);
		%this.updatecompSpriteCull(%batchCull);
		%this.updateCompSpriteAngDamp(%angDam);
		%this.updateCompSpriteAngVel(%angVel);
		%this.updateCompSpriteLinVelX(%linVelX);
		%this.updateCompSpriteLinVelY(%linVelY);
		%this.updateCompSpriteLinVelPolAngle(%linVelPolAng);
		%this.updateCompSpriteLinVelPolSpeed(%linVelPolSpeed);
		%this.updateCompSpriteLinDamp(%linDam);
		%this.updateCompSpriteDefDensity(%defDen);
		%this.updateCompSpriteDefFriction(%defFri);
		%this.updateCompSpriteDefRestitution(%defRes);
		%this.updateCompSpriteCollSupp(%collSupp);
		%this.updateCompSpriteCollOne(%collOne);
		%this.updateCompSpriteCollShapeCount();
		%this.updateCompSpriteFrame(%frame);
		%this.updateCompSpriteGravity(%grav);
		%this.updateCompSpriteSceneLayer(%sceneLay);
		%this.updateCompSpriteSceneGroup(%sceneGroup);
		%this.updateCompSpriteCollLayers(%collLayers);
		%this.updateCompSpriteCollGroups(%collGroups);
		%this.loadCompSpriteCollLayerArray();
		%this.loadCompSpriteCollGroupArray();
		%this.updateCompSpriteAlphaTest(%alphaTest);
		%this.updateCompSpriteBlendMode(%blendMode);
		%this.updateCompSpriteSrcBlend(%srcBlend);
		%this.updateCompSpriteDstBlend(%dstBlend);
		%this.updateCompSpriteBlendR(%blendR);
		%this.updateCompSpriteBlendG(%blendG);
		%this.updateCompSpriteBlendB(%blendB);
		%this.updateCompSpriteBlendA(%blendA);
		
		//Update our gui
		CompSpriteName.update();
		CompSpriteClass.update();
		CompSpriteAngle.update();
		CompSpriteFixedAngle.update();
		CompSpriteLayoutMode.update();
		CompSpriteBatchList.update();
		CompSpriteIsolated.update();
		CompSpriteCull.update();
		CompSpriteAngularDamp.update();
		CompSpriteAngularVel.update();
		CompSpriteLinearVelX.update();
		CompSpriteLinearVelY.update();
		CompSpriteLinearVelPolAngle.update();
		CompSpriteLinearVelPolSpeed.update();
		CompSpriteLinearDamp.update();
		CompSpriteDefDensity.update();
		CompSpriteDefFriction.update();
		CompSpriteDefRestitution.update();
		CompSpriteCollSuppress.update();
		CompSpriteCollOneWay.update();
		CompSpritePosX.update();
		CompSpritePosY.update();
		CompSpriteBodyList.update();
		%this.updateCompSpriteCollGui();
		CompSpriteGravity.update();
		CompSpriteSceneLayer.update();
		CompSpriteSceneGroup.update();
		CompSpriteAlphaTest.update();
		CompSpriteBlendMode.update();
		CompSpriteBlendA.update();
		CompSpriteSrcBlendList.update();
		CompSpriteDstBlendList.update();
	}
	
	else if(%className $= "Scroller")
	{
		//Initializers
		%this.selObject = %obj;
		ScrollerAssetMenu.setVisible(1);
		%name = %obj.getName();
		%class = %obj.getClassNamespace();
		%pos = %obj.getPosition();
		%speedX = %obj.getScrollX();
		%speedY = %obj.getScrollY();
		%repX = %obj.getRepeatX();
		%repY = %obj.getRepeatY();
		%width = %obj.getWidth();
		%height = %obj.getHeight();
		%body = %obj.getBodyType();
		%angDam = %obj.getAngularDamping();
		%angVel = %obj.getAngularVelocity();
		%linVelX = %obj.getLinearVelocityX();
		%linVelY = %obj.getLinearVelocityY();
		%linVelPol = %obj.getLinearVelocityPolar();
		%linVelPolAng = getWord(%linVelPol, 0);
		%linVelPolSpeed = getWord(%linVelPol, 1);
		%linDam = %obj.getLinearDamping();
		%defDen = %obj.getDefaultDensity();
		%defFri = %obj.getDefaultFriction();
		%defRes = %obj.getDefaultRestitution();
		%collSupp = %obj.getCollisionSuppress();
		%collOne = %obj.getCollisionOneWay();
		%colNum = %obj.getCollisionShapeCount();
		%frame = %obj.getImageFrame();
		%grav = %obj.getGravityScale();
		%sceneLay = %obj.getSceneLayer();
		%sceneGroup = %obj.getSceneGroup();
		%collLayers = %obj.getCollisionLayers();
		%collGroups = %obj.getCollisionGroups();
		%alphaTest = %obj.getAlphaTest();
		%blendMode = %obj.getBlendMode();
		%srcBlend = %obj.getSrcBlendFactor();
		%dstBlend = %obj.getDstBlendFactor();
		%blendCol = %obj.getBlendColor();
		%blendR = getWord(%blendCol,0);
		%blendG = getWord(%blendCol,1);
		%blendB = getWord(%blendCol,2);
		%blendA = %obj.getBlendAlpha();
		
		//Update our defaults
		%this.updateSelScroller(%obj);
		%this.updateScrollerPosX(%pos.x);
		%this.updateScrollerPosY(%pos.y);
		%this.updateScrollerSpeedX(%speedX);
		%this.updateScrollerSpeedY(%speedY);
		%this.updateScrollerScrollRepX(%repX);
		%this.updateScrollerScrollRepY(%repY);
		%this.updateScrollerHeight(%height);
		%this.updateScrollerWidth(%width);
		%this.updateScrollerBody(%body);
		%this.updateScrollerName(%name);
		%this.updateScrollerClass(%class);
		%this.updateScrollerAngDamp(%angDam);
		%this.updateScrollerAngVel(%angVel);
		%this.updateScrollerLinVelX(%linVelX);
		%this.updateScrollerLinVelY(%linVelY);
		%this.updateScrollerLinVelPolAngle(%linVelPolAng);
		%this.updateScrollerLinVelPolSpeed(%linVelPolSpeed);
		%this.updateScrollerLinDamp(%linDam);
		%this.updateScrollerDefDensity(%defDen);
		%this.updateScrollerDefFriction(%defFri);
		%this.updateScrollerDefRestitution(%defRes);
		%this.updateScrollerCollSupp(%collSupp);
		%this.updateScrollerCollOne(%collOne);
		%this.updateScrollerCollShapeCount();
		%this.updateScrollerFrame(%frame);
		%this.updateScrollerGravity(%grav);
		%this.updateScrollerSceneLayer(%sceneLay);
		%this.updateScrollerSceneGroup(%sceneGroup);
		%this.updateScrollerCollLayers(%collLayers);
		%this.updateScrollerCollGroups(%collGroups);
		%this.loadScrollerCollLayerArray();
		%this.loadScrollerCollGroupArray();
		%this.updateScrollerAlphaTest(%alphaTest);
		%this.updateScrollerBlendMode(%blendMode);
		%this.updateScrollerSrcBlend(%srcBlend);
		%this.updateScrollerDstBlend(%dstBlend);
		%this.updateScrollerBlendR(%blendR);
		%this.updateScrollerBlendG(%blendG);
		%this.updateScrollerBlendB(%blendB);
		%this.updateScrollerBlendA(%blendA);
		
		//Update our gui
		ScrollerName.update();
		ScrollerClass.update();
		ScrollerAngularDamp.update();
		ScrollerAngularVel.update();
		ScrollerLinearVelX.update();
		ScrollerLinearVelY.update();
		ScrollerLinearVelPolAngle.update();
		ScrollerLinearVelPolSpeed.update();
		ScrollerLinearDamp.update();
		ScrollerDefDensity.update();
		ScrollerDefFriction.update();
		ScrollerDefRestitution.update();
		ScrollerCollSuppress.update();
		ScrollerCollOneWay.update();
		ScrollerPosX.update();
		ScrollerPosY.update();
		ScrollerSpeedX.update();
		ScrollerSpeedY.update();
		ScrollerScrollRepX.update();
		ScrollerScrollRepY.update();
		ScrollerWidth.update();
		ScrollerHeight.update();
		ScrollerBodyList.update();
		%this.updateScrollerCollGui();
		ScrollerFrame.update();
		ScrollerGravity.update();
		ScrollerSceneLayer.update();
		ScrollerSceneGroup.update();
		ScrollerAlphaTest.update();
		ScrollerBlendMode.update();
		ScrollerBlendA.update();
		ScrollerSrcBlendList.update();
		ScrollerDstBlendList.update();
	}

}
function EditorToy::activateModuleLoadBttn(%this)
{
	ModuleLoadBttn.setActive(1);
	
}
function EditorToy::activateSceneBttn(%this)
{
	NewSceneBttn.setActive(1);
	LoadSceneBttn.setActive(1);
}

function EditorToy::activateToolbarBttn(%this)
{
	SaveSceneBttn.setActive(1);
	SceneSettingsBttn.setActive(1);
	CameraSettingsBttn.setActive(1);
	ImportImageBttn.setActive(1);
	CreateSpriteBttn.setActive(1);
	CreateAnimationBttn.setActive(1);
	CreateCompBttn.setActive(1);
	CreateScrollerBttn.setActive(1);
	CreateShapeBttn.setActive(1);
	CreateParticleBttn.setActive(1);
	CreateForceBttn.setActive(1);
	CreateJointBttn.setActive(1);
	CreateTextSpriteBttn.setActive(1);
	CreateTriggerBttn.setActive(1);
}

function EditorToy::deactivateToolbar(%this)
{
	NewSceneBttn.setActive(0);
	LoadSceneBttn.setActive(0);
	SaveSceneBttn.setActive(0);
	SceneSettingsBttn.setActive(0);
	CameraSettingsBttn.setActive(0);
	ImportImageBttn.setActive(0);
	CreateSpriteBttn.setActive(0);
	CreateAnimationBttn.setActive(0);
	CreateCompBttn.setActive(0);
	CreateScrollerBttn.setActive(0);
	CreateShapeBttn.setActive(0);
	CreateParticleBttn.setActive(0);
	CreateForceBttn.setActive(0);
	CreateJointBttn.setActive(0);
	CreateTextSpriteBttn.setActive(0);
	CreateTriggerBttn.setActive(0);
}

function EditorToy::hideAssetMenus(%this)
{
	SpriteAssetMenu.setVisible(0);
	CompSpriteAssetMenu.setVisible(0);
	CompSpriteEditMenu.setVisible(0);
	RectToolsDisplay.setVisible(0);
	CompSpriteSpriteMenu.setVisible(0);
	ScrollerAssetMenu.setVisible(0);
}
//Steering Behaviors test
/*function EditorToy::testObject(%this)
{
	%object = SandboxScene.create( Sprite );
	%object.class = "SteerTest";
    %object.Size = "10 10";
	%object.SetBodyType( dynamic );
    %object.BlendColor = White;
	%object.Position = "0 0";
    %object.PickingAllowed = false;
    %object.Image = "ToyAssets:hollowArrow";
	%object.startTimer( objectMovement, 100 );
	%this.steerTest = %object;
	//%this.objectMovement(%object);
}

function SteerTest::objectMovement(%this)
{
	//Based on :https://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732
	//Calculations for standard steering behaviours
	//desired_velocity = normalize(target - position) * max_velocity
	//steering = desired_velocity - velocity
	//steering = truncate (steering, max_force)
	//steering = steering / mass
 
	//velocity = truncate (velocity + steering , max_speed)
	//position = position + velocity
	
	%targetPos = EditorToy.targetPos;
	echo(%targetPos);
	%objPos = %this.getPosition();
	%currentVel = %this.getLinearVelocity();
	%maxVelocity = 20.1;
	%maxForce = 3.1;
	%slowRad = 100.0; 
	//%targetRad = 10.0;
	%mass = %this.getMass();
	//Seek with arrival
	%steer = seek(%targetPos,%objPos,%maxVelocity, %currentVel, %slowRad,%targetRad);
	//Flee
	//%steer = flee(%targetPos,%objPos,%maxVelocity, %currentVel);
	%steer = truncate(%steer, %maxForce);
	%steer = Vector2Scale(%steer , 1 / 2);
	echo(%steer);
	%currentVel = Vector2Add(%currentVel,%steer);
	echo("currentVel before truncate", %currentVel);
	%currentVel = truncate(%currentVel, %maxVelocity);
	echo(%currentVel);
	%position = Vector2Add(%objPos, %currentVel);
	//CurrentlyWorking
	%this.applyForce(%position,%this.getWorldCenter());
	//%this.Position = %position;
	//Current simple rotation
	%angle = Vector2AngleToPoint( %objPos, %targetPos );
	%this.rotateTo( %angle - 90, 180 );
}

function truncate(%vec2,%maxNum)
{
	%i = %maxNum;
	echo("vec2 before vectorscale", %vec2);
	%i = %i < 1.0 ? 1.0 : %i;
	%vec2 = Vector2Scale(%vec2,%i);
	echo("vec2 after",%vec2);
	
	return %vec2;
}

function seek(%target, %pos, %maxVelocity, %currentVel, %slowingRadius,%targetRadius)
{
	%desired = Vector2Sub(%target, %pos);
	%distance = Vector2Length(%desired);
	%desired = Vector2Normalize(%desired);
	//Arrive code
	if(%distance < %slowingRadius)
	{
		%desired = Vector2Scale(%desired, %maxVelocity * %distance / %slowingRadius );
	}else{
		%desired = Vector2Scale(%desired, %maxVelocity);
	}
	//basic seek, best for paths
	//%desired = Vector2Scale(%desired, %maxVelocity);
	%force = Vector2Sub(%desired,%currentVel);
	
	return %force;
}

function flee(%target, %pos, %maxVelocity, %currentVel)
{
	%desired = VectorSub(%pos, %target);
	%distance = VectorLen(%desired);
	%desired = VectorNormalize(%desired);
	%desired = VectorScale(%desired, %maxVelocity);
	%force = VectorSub(%desired,%currentVel);
	
	return %force;
}*/

//-----------------------------------------------------------------------------
//Create Poly Editor
function EditorToy::createPolyEditorMenu(%this)
{
	SandboxWindow.add(PolyEditorMenu);
}

function EditorToy::updatePolylistMenuItems(%this)
{
	//PolyList Layout
	//Container((label with id)(Xpos)(Ypos)(deleteBttn))
	//should fit inside container with size 200 x 40
	//each polylist item is added to a simset. This 
	//makes it easier to remove them and keep the ids updating.
	
	//initializer
	
	
	//Sanity
	if(!isObject (PolylistSim))
	{
		echo("No polylistpoints");
		return;
	}
	//we have to completely delete the list then recreate it
	//to update the menu list.
	if(isObject(PolyStack))
	{
		ListEntry.remove(PolyStack);
		%this.polyListPosLocal = "";
	}

	%stackContainer = new GuiStackControl( PolyStack )
	{
		Position="0 0";
		Extent="160 320";
		Profile="GuiDefaultProfile";
		Padding = "10";
	};
	
	
		for(%i = 0; %i < PolylistSim.getCount(); %i++)
		{	
			%object = PolylistSim.getObject(%i);
			%pPos = %object.getPosition();
			
			%sObj = EditorToy.selObject;
			//Convert Points to LocalSpace for the selected scroller
			%lPos = %sObj.getLocalPoint(%pPos);
			%this.polyListPosLocal = %this.polyListPosLocal SPC %lPos;
			
			%pPosX = %lPos.x;
			%pPosY = %lPos.y;
			%posX = 1;
			%posY = %i * 55;
			
			%itemcontainer = new GuiControl()
			{
				    
				isContainer = 1;
				HorizSizing = "relative";
				VertSizing = "relative";
				position = "0 0";
				extent = "150 57";
				Profile = "GuiDefaultBorderProfile";
				
			};
			
			%text = new GuiTextCtrl() {
                text = %i;
                maxLength = "1024";
				margin = "0 0 0 0";
				padding = "0 0 0 0";
				anchorTop = "1";
				anchorBottom = "0";
                anchorLeft = "1";
                anchorRight = "0";
                position = "13 13";
                extent = "12 14";
                minExtent = "8 2";
                horizSizing = "right";
				vertSizing = "bottom";
				profile = "GuiEditorTextProfile";
				visible = "1";
				active = "1";
				tooltipProfile = "GuiToolTipProfile";
				hovertime = "1000";
			};
			%itemcontainer.add(%text);
			
			%textX = new GuiTextCtrl()
			{
				Position = "25 10";
				text = "X:";
				Extent = "15 10";
			};
			
			%itemcontainer.add(%textX);
			
			%pPosTextX = new GuiTextEditCtrl()
			{
				ObjId = %i;
				class = "PolyPosX";
				Position = "40 5";
				text = %pPosX;
				Extent = "40 20";
				Profile = "GuiEditorTextEditProfile";
			};
			
			
			%itemcontainer.add(%pPosTextX);
			
			%textY = new GuiTextCtrl()
			{
				Position = "25 35";
				text = "Y:";
				Extent = "15 10";

			};
			
			%itemcontainer.add(%textY);
			
			%pPosTextY = new GuiTextEditCtrl()
			{
				ObjId = %i;
				class = "PolyPosY";
				Position = "40 30";
				text = %pPosY;
				Extent = "40 20";
				Profile = "GuiEditorTextEditProfile";
			};
			
			
			%itemcontainer.add(%pPosTextY);

			%button = new GuiButtonCtrl()
			{
				Name = "PolyButton";
				canSaveDynamicFields = "0";
				HorizSizing = "relative";
				VertSizing = "relative";
				isContainer = "0";
				Profile = "RedButtonProfile";
				Position = "90 10";
				Extent = "30 30";
				Visible = "1";
				command = "EditorToy.deletePolyItem("@ %i @ ");";
				isContainer = "0";
				Active = "1";
				hovertime = "1000";
				groupNum = "-1";
				buttonType = "PushButton";
				useMouseEvents = "0";
			};
			
			%itemcontainer.add(%button);
			
			%stackContainer.add(%itemcontainer);
			
		}
	//Create after the for loop building the string of poly points
	%this.createLine();
    ListEntry.add(%stackContainer);
}

//Handle Dynamic gui returns
function EditorToy::deletePolyItem(%this, %objId)
{
	%object = PolylistSim.getObject(%objId);
	%object.delete();
	%this.updatePolylistMenuItems();
}

function PolyPosX::onReturn(%this)
{
	%objId = %this.ObjId;
	%text = %this.getText();
	EditorToy.changeXtoWorld(%text,%objId);
}

function PolyPosX::onLoseFirstResponder(%this)
{
	%objId = %this.ObjId;
	%text = %this.getText();
	EditorToy.changeXtoWorld(%text,%objId);
}

function PolyPosY::onReturn(%this)
{
	%objId = %this.ObjId;
	%text = %this.getText();
	EditorToy.changeYtoWorld(%text,%objId);
}

function PolyPosY::onLoseFirstResponder(%this)
{
	%objId = %this.ObjId;
	%text = %this.getText();
	EditorToy.changeYtoWorld(%text,%objId);
}

//We have to convert typed in values to world positions
//as the gui displays local positions but the gui elements
//are built in worldspace.
function EditorToy::changeXtoWorld(%this ,%text, %objId)
{
	%sObj = EditorToy.selObject;
	%text = %text SPC "0";
	%wPoint = %sObj.getWorldPoint(%text);
	%object = PolylistSim.getObject(%objId);
	%object.setPositionX(%wPoint.x);
	EditorToy.updateComplete();
}

function EditorToy::changeYtoWorld(%this ,%text, %objId)
{
	%sObj = EditorToy.selObject;
	%text = "0" SPC %text;
	%wPoint = %sObj.getWorldPoint(%text);
	%object = PolylistSim.getObject(%objId);
	%object.setPositionY(%wPoint.y);
	EditorToy.updateComplete();
}

function EditorToy::updateComplete(%this)
{
	%this.updatePolyListMenuItems();
}

function EditorToy::createPolylistItem(%this, %pos)
{
	%scene = %this.activeScene;
	//Sanity
	if(!isObject (PolylistSim))
	{
		%mySim3 = new SimSet(PolylistSim);
	}
	%type = EditorToy.drawType;
	%count = PolylistSim.getCount();
	if( %type $= "polyCol")
	{
		if(%count == 8)
		{
			return;
		}
	}
	else if( %type $= "edgeCol")
	{
		if(%count == 2)
		{
			return;
		}
	}
	else if( %type $= "chainCol")
	{
		if(%count == 8)
		{
			return;
		}
	}
	%size = EditorToy.selObject.getSize() / 10;
	
	// Create the handle.
	%obj = new ShapeVector();
	%obj.class = "PolyHandle";
	%obj.setPosition( %pos );
	%obj.setPolyPrimitive(4);
	%obj.setSize( %size );
	%obj.LineColor = "1.0 1.0 1.0 1.0";
	%obj.FillColor = "0.0 1.0 0.0 0.5";
	%obj.FillMode = "1";
	PolylistSim.add(%obj);
	// Add to the scene.
	%scene.add( %obj ); 
	%this.updatePolylistMenuItems();
}

function EditorToy::createLine(%this)
{
	%pL = EditorToy.polyListPosLocal;
	%scene = %this.activeScene;
	%pCount = PolylistSim.getCount();
	//Sanity
	if(isObject(LineOverlay))
	{
		LineOverlay.delete(); 
		%pL = "";
	}
	%sObj = EditorToy.selObject;
	for(%i = 0; %i < %pCount; %i++)
	{
		%object = PolylistSim.getObject(%i);
		%pPos = %object.getPosition();
		
		//Convert Points to LocalSpace
		%lPos = %sObj.getLocalPoint(%pPos);
		%pL = %pL SPC %lPos;
	}
	%cPos = %sObj.getPosition();
	%ang = %sObj.getAngle();
	//only draw when we have more than 1 point
	if(%pCount > 1)
	{
		%obj = new ShapeVector( LineOverlay );
		%obj.PolyList = %pL;
		%obj.Position = %cPos;
		%obj.Angle = %ang;
		%obj.LineColor = "0.0 0.0 1.0 1.0";
		// Add to the scene.
		%scene.add( %obj );
	}
}

function EditorToy::createPolyCollision(%this)
{
	%this.drawMode = true;
	%this.drawType = "polyCol";
	%obj = EditorToy.selObject;
	%objHeight = %obj.getHeight() * 1.5;
	%objWidth = %obj.getWidth() * 1.5;
	%objPos = %obj.getPosition();
	%aspect = EditorToy.aspectRatio;
	if(%objWidth < %objHeight)
	{
		%objWidth = %objHeight * %aspect;
	}
	if(%objWidth > %objHeight)
	{
		%objHeight = %objWidth * %aspect;
	}
	%scene = %this.activeScene;
	PolyEditorWindow.setScene(%scene);
    PolyEditorWindow.setCameraSize( %objWidth , %objHeight );
	PolyEditorWindow.setCameraPosition( %objPos.x , %objPos.y );
	%obj.setPickingAllowed(false);
	
	PolyEditorMenu.setVisible(1);
	PolyEditorMain.add( PolyEditorWindow );
}

function EditorToy::createEdgeCollision(%this)
{
	%this.drawMode = true;
	%this.drawType = "edgeCol";
	%obj = EditorToy.selObject;
	%objHeight = %obj.getHeight() * 1.5;
	%objWidth = %obj.getWidth() * 1.5;
	%objPos = %obj.getPosition();
	%aspect = EditorToy.aspectRatio;
	if(%objWidth < %objHeight)
	{
		%objWidth = %objHeight * %aspect;
	}
	if(%objWidth > %objHeight)
	{
		%objHeight = %objWidth * %aspect;
	}
	%scene = %this.activeScene;
	PolyEditorWindow.setScene(%scene);
    PolyEditorWindow.setCameraSize( %objWidth , %objHeight );
	PolyEditorWindow.setCameraPosition( %objPos.x , %objPos.y );
	%obj.setPickingAllowed(false);
	
	PolyEditorMenu.setVisible(1);
	PolyEditorMain.add( PolyEditorWindow );
}

function EditorToy::createChainCollision(%this)
{
	%this.drawMode = true;
	%this.drawType = "chainCol";
	%obj = EditorToy.selObject;
	%objHeight = %obj.getHeight() * 1.5;
	%objWidth = %obj.getWidth() * 1.5;
	%objPos = %obj.getPosition();
	%aspect = EditorToy.aspectRatio;
	if(%objWidth < %objHeight)
	{
		%objWidth = %objHeight * %aspect;
	}
	if(%objWidth > %objHeight)
	{
		%objHeight = %objWidth * %aspect;
	}
	%scene = %this.activeScene;
	PolyEditorWindow.setScene(%scene);
    PolyEditorWindow.setCameraSize( %objWidth , %objHeight );
	PolyEditorWindow.setCameraPosition( %objPos.x , %objPos.y );
	%obj.setPickingAllowed(false);
	
	PolyEditorMenu.setVisible(1);
	PolyEditorMain.add( PolyEditorWindow );
}

function EditorToy::finishPolyShape(%this)
{
	%this.drawMode = false;
	%obj = EditorToy.selObject;
	%className = %obj.getClassName();
	%obj.setPickingAllowed(true);
	if(%className $= "Scroller")
	{
		%this.updateScroller();
		%this.updateScrollerCollShapeCount();
		%this.updateScrollerCollGui();
	}
	else if(%className $= "Sprite")
	{	
		%this.updateSprite();
		%this.updateSpriteCollShapeCount();
		%this.updateSpriteCollGui();
	}
	else if(%className $= "CompositeSprite")
	{	
		%this.updateCompSprite();
		%this.updateCompSpriteCollShapeCount();
		%this.updateCompSpriteCollGui();
	}
}

function EditorToy::resetPolyListPosLocal(%this)
{
	%this.polyListPosLocal = "";
	
	
	//Sanity
	if(isObject(LineOverlay))
	{
		LineOverlay.delete(); 
	}
	
	if(isObject (PolylistSim))
	{
		for (%i = PolylistSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = PolylistSim.getObject(%i);
			%object.delete();
		}
	}
	//ListEntry.remove(PolyContainer);
	//PolyEditorMain.setVisible(0);
	%this.hidePolyMenu();
}

function EditorToy::hidePolyMenu(%this)
{
	%this.updatePolylistMenuItems();
	PolyEditorMenu.setVisible(0);
}

function CameraPopout::toggleVisible(%this)
{
	if(SceneSettingsPopout.isVisible())
	{
		SceneSettingsPopout.setVisible(0);
	}
	
	if(CompSpritePopout.isVisible())
	{
		CompSpritePopout.setVisible(0);
	}
	
	if(%this.isVisible())
	{
		%this.setVisible(0);
	}
	else
	{
		%this.setVisible(1);
	}
}

function SceneSettingsPopout::toggleVisible(%this)
{
	if(CameraPopout.isVisible())
	{
		CameraPopout.setVisible(0);
	}
	
	if(CompSpritePopout.isVisible())
	{
		CompSpritePopout.setVisible(0);
	}
	
	if(%this.isVisible())
	{
		%this.setVisible(0);
	}
	else
	{
		%this.setVisible(1);
	}
}

function CompSpritePopout::toggleVisible(%this)
{
	if(CameraPopout.isVisible())
	{
		CameraPopout.setVisible(0);
	}
	
	if(SceneSettingsPopout.isVisible())
	{
		SceneSettingsPopout.setVisible(0);
	}
	
	if(%this.isVisible())
	{
		%this.setVisible(0);
	}
	else
	{
		%this.setVisible(1);
	}
}

function EditorToy::setSceneWindowCamera(%this)
{
	%editCamX = EditorToy.sceneCameraX * 2;
	%editCamY = EditorToy.sceneCameraY * 2;
	SandboxWindow.setCameraSize(%editCamX,%editCamY);
	%this.createCamera();
}

function EditorToy::createCamera(%this)
{
	%scene = %this.activeScene;
	if(isObject (CameraObject))
	{
		CameraObject.delete();
	}
	
	%obj = new ShapeVector( CameraObject );
	%obj.setPolyPrimitive(4);
	%obj.setSize( EditorToy.sceneCameraX SPC EditorToy.sceneCameraY );
	%obj.LineColor = "0.6 0.6 1.0 1.0";
	//Should never interact with gravity
	%obj.setGravityScale("0");
	//Never directly interact with camera
	%obj.setPickingAllowed(false);
	
	%scene.add(%obj);
}

function EditorToy::moveCamera(%this,%x,%y)
{
	%origPos = SandboxWindow.getCameraPosition();
	SandboxWindow.setCameraPosition(%origPos.x + %x, %origPos.y + %y);
	%this.moveSchedule = %this.schedule(25, moveCamera, %x , %y);
}

function EditorToy::stopCamera(%this)
{
	cancel(%this.moveSchedule);
}

function EditorToy::moveObject(%this,%x,%y)
{
	%obj = EditorToy.selObject;
	%origPos = %obj.getPosition();
	%obj.setPosition(%origPos.x + %x, %origPos.y + %y);
}

function EditorToy::deleteObject(%this)
{
	EditorToy.mouseMode = "default";
	if(EditorToy.compEditMode !$= "select")
	{
		EditorToy.compEditMode = "select";
		Canvas.resetCursor();
	}
	%this.hideAssetMenus();
	EditorToy.selObject.delete();
}

function LockState::onClick(%this)
{
	%obj = EditorToy.selObject;
	%className = %obj.getClassName();
	echo("button Clicked");
	if(%this.getStateOn())
	{
		if(%className $= "Sprite")
		{
			SpriteScroll.setVisible(0);
		}
	}
	else
	{
		if(%className $= "Sprite")
		{
			SpriteScroll.setVisible(1);
		}
	}
}