//-----------------------------------------------------------------------------
//ImageAsset functions 
//Reset image asset to default values before next import
function EditorToy::resetImageAssetDefaults(%this)
{
	//Set ImageAsset Defaults	
	%this.imageAName = "default";
	%this.imageAFile = "default";
	%this.cellH = 0;
	%this.cellW = 0;
	%this.filterMode = "Nearest";
	%this.cellNX = 0;
	%this.cellNY = 0;
	%this.cellStrideX = 0;
	%this.cellStrideY = 0;
	%this.cellOffX = 0;
	%this.cellOffY = 0;
	%this.force16Bit = 0;
	%this.cellRowOrder = true;
	ImageAssetMenu.setVisible(0);
	//Also remove image editor menu
	CellAmountX.onAdd();
	CellAmountY.onAdd();
	CellOffX.onAdd();
	CellOffY.onAdd();
	CellWidth.onAdd();
	CellHeight.onAdd();
	RowOrder.onAdd();
	Force16Bit.onAdd();
	
	
	if ( isObject(ImagePreview))
		ImagePreview.delete();
	
	if(isObject (CellOverlay))
	{
		for (%i = CellSim.getCount() - 1; %i >= 0; %i-- )
		{
			%object = CellSim.getObject(%i);
			%object.delete();
		}
	}
	
	if(isObject (CellFrame))
	{
		for (%i = CellFrameSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = CellFrameSim.getObject(%i);
			%object.delete();
		}
	}
}

//Create ImageAsset Menu
function EditorToy::createImageAssetMenu(%this)
{	
	SandboxWindow.add(ImageAssetMenu);
	ImagePreviewWindowHolder.add(ImagePreviewWindow);
	FilterList.update();
}

//-----------------------------------------------------------------------------
//Handle Image Asset Outputs
function EditorToy::createImageAsset(%this)
{
	%mName = EditorToy.moduleName;
	%ImageAssetLoad = new OpenFileDialog();
	%ImageAssetLoad.DefaultPath = "modules/";
	%ImageAssetLoad.Title = "Choose Image to Import";
	%ImageAssetLoad.MustExist = true;      
	%ImageAssetLoad.ChangePath = false;
	%ImageAssetLoad.Filters = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

	if(%ImageAssetLoad.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %ImageAssetLoad.fileName;
		%defaultExpand = %ImageAssetLoad.expandFilename;
		echo(%defaultFile);
		echo(%defaultExpand);
		%extension = fileExt(%defaultFile);
		echo("file extension = ", %extension);
		if(%extension $= ".png" || %extension $= ".jpg" )
		{
			%extension2 = fileBase(%defaultFile);
			%extension3 = fileName(%defaultFile);
			%defaultLocation = "^EditorToy/projects/"@ %mName @ "/1/assets/images/";
			echo("file without extension = ", %extension2);
			//Copy to our directory for the editor
			%fromName = %defaultFile;
			%toName = %defaultLocation @ %extension3;
			pathCopy(%fromName,%toName);
			echo(%toName);
			//Copy Default taml file
			%defaultTaml = "^EditorToy/assets/defaults/empty.taml";
			%toTaml = %defaultLocation @ %extension2 @ ".asset.taml";
			pathCopy(%defaultTaml,%toTaml);
			%exPath = expandPath(%toName);
			%assetName = fileBase(%exPath);
			%assetFile = fileName(%exPath);
			/*%asset = new ImageAsset();
			%asset.AssetName = %assetName;
			%asset.ImageFile = %defaultLocation @ %assetFile;*/
			
			%newAsset = %defaultLocation @ %assetFile;
			%ImageAssetLoad.delete();
			//Update Values
			%this.updateImageAName(%assetName);
			%this.updateImageAFile(%assetFile);
			//Launch ImageAsset Viewer
			%this.createImageAssetViewer(%assetName,%newAsset);
			//TamlWrite(%asset, %defaultLocation @ %assetName @ ".assets.taml");
		}
		else
		{
			echo("file is not image format");
			return;
		}
	}
	if(isObject(%ImageAssetLoad))
	{
		%ImageAssetLoad.delete();
	}
}

function EditorToy::createImageAssetViewer(%this,%assetName,%assetFile)
{
	%aN = %assetName;
	%aF = %assetFile;
	echo("create taml output",%aF);
	%asset = new ImageAsset();
	%asset.AssetName = %aN;
	%asset.ImageFile = %aF;
	
	%assetId = AssetDatabase.addPrivateAsset( %asset );
	%image = AssetDatabase.acquireAsset(%assetId);
	%size = %image.getImageSize();
	echo(%size);
	%posX = %size.width / 2;
	%posY = (%size.height / 2) * -1;
	
	ImagePreviewWindow.setCameraPosition(%posX , %posY);
	ImagePreviewWindow.setCameraSize(%size.width + 10, %size.height + 10);
	
	//Sanitize 
	if ( isObject(ImagePreview))
		ImagePreview.delete();
	
	%sprite = new Sprite( ImagePreview );
	%sprite.Image = %assetId;
	%sprite.Size = %size;
	%sprite.setPosition(%posX,%posY);
	%sprite.PickingAllowed = false;
	%sprite.SceneLayer = 10;
	
	ImagePreviewScene.add(%sprite);
	
	//Setting image menu to invisible is better and does not create
	//errors in the log for object names.
	//%this.createImageAssetMenu();
	ImageAssetMenu.setVisible(1);
	
}

function EditorToy::createImageAssetOverlay(%this)
{
	//Sanitize 
	if(!isObject (CellSim))
	{
		%mySim = new SimSet(CellSim);
	}
	
	if(!isObject (CellFrameSim))
	{
		%mySim2 = new SimSet(CellFrameSim);
	}
	
	if(isObject (CellFrame))
	{
		for (%i = CellFrameSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = CellFrameSim.getObject(%i);
			%object.delete();
		}
	}
	
	if(isObject (CellOverlay))
	{
		for (%i = CellSim.getCount() - 1; %i >= 0; %i-- )
		{
			%object = CellSim.getObject(%i);
			%object.delete();
		}
	}
	
	echo("update image asset overlay");
	//initialize data
	%cH = EditorToy.cellH;
	%cW = EditorToy.cellW;
	%cCX = EditorToy.cellNX;
	%cCY = EditorToy.cellNY;
	%cSX = EditorToy.cellStrideX;
	%cSY = EditorToy.cellStrideY;
	%cOX = EditorToy.cellOffX;
	%cOY = EditorToy.cellOffY;
	%cRO = EditorToy.cellRowOrder;
	echo(%cRO);
    %showFrames = EditorToy.showFrames; 
    // Calculate a block building position.
    %posx = 0.0 + (%cW * 0.5 + %cOX);
    %posy = 0.0 - (%cH * 0.5 + %cOY);
	
	//Only show grid if we have cells to work with
	if(%cCX > 0 || %cCY > 0)
	{
		// Build the stack of blocks.
		//If cell row order is true build the stack 
		if(%cRO == 1)
		{
			for( %stack = 0; %stack < %cCY; %stack++ )
			{
				// Calculate the stack position.
				%stackX = %posX;
				//If we are using stride we don't use the height to calculate
				//position
				if(%cSY > 0)
				{
					%stackY = %posY + ( %stack * -%cSY );
				}
				else
				{
					%stackY = %posY + ( %stack * -%cH );
				}
				
				// Build the stack.
				for ( %stackIndex = 0; %stackIndex < %cCX; %stackIndex++ )
				{
					// Calculate the block position.
					//If we are using stride we don't use the height to calculate
					//position
					if(%cSX > 0)
					{
						%blockX = %stackX + (%stackIndex*%cSX);
					}
					else
					{
						%blockX = %stackX + (%stackIndex*%cW);
					}
					%blockY = %stackY;

					// Create the shape.
					%obj = new ShapeVector( CellOverlay );
					%obj.setPosition( %blockX, %blockY );
					%obj.setPolyPrimitive(4);
					%obj.setSize( %cW SPC %cH );
					%obj.PickingAllowed = false;
					CellSim.add(%obj);
					// Add to the scene.
					ImagePreviewScene.add( %obj );          
				}
			}
		}
		else
		{
			//Build the stack in Y order if row order false
			for( %stack = 0; %stack < %cCX; %stack++ )
			{
				// Calculate the stack position.
				%stackX = %posX + ( %stack * %cW );
				%stackY = %posY;
				
				// Build the stack.
				for ( %stackIndex = 0; %stackIndex < %cCY; %stackIndex++ )
				{
					// Calculate the block position.
					%blockX = %stackX;
					%blockY = %stackY + (%stackIndex* -%cH);

					// Create the sprite.
					%obj = new ShapeVector( CellOverlay );
					%obj.setPosition( %blockX, %blockY );
					%obj.setPolyPrimitive(4);
					%obj.setSize( %cW SPC %cH );
					%obj.PickingAllowed = false;
					CellSim.add(%obj);
					// Add to the scene.
					ImagePreviewScene.add( %obj );          
				}
			}
		}
		
		if(%showFrames == true)
		{
			for(%i = 0; %i < CellSim.getCount(); %i++)
			{
				
				%object = CellSim.getObject(%i);
				%pos = %object.getPosition();
				%fontSize = %cW / 5;
				//Our frames actually start at 0.
				%frame = %i;
				%obj = new TextSprite(CellFrame);
				%obj.setPosition(%pos);
				%obj.Text = %frame;
				%obj.FontSize = %fontSize;
				%obj.Font = "ToyAssets:ArialFont";
				%obj.OverflowModeY = "Visible";
				%obj.OverflowModeX = "Visible";
				%obj.TextAlignment = "center";
				%obj.TextVAlignment = "Middle";
				%obj.PickingAllowed = false;
				CellFrameSim.add( %obj );
				ImagePreviewScene.add( %obj );
			}
		}
		
	}
}

function EditorToy::exportImageAssetTaml(%this)
{
	//initialize data
	//Before this data was taken from the updates
	//The problem here is that you had to either
	//press enter on the input or press into another
	//input before pressing the finish button.
	%iA = EditorToy.imageAName;
	%iF = EditorToy.imageAFile;
	%fM = EditorToy.filterMode;
	%cH = CellHeight.getText();
	%cW = CellWidth.getText();
	%cCX = CellAmountX.getText();
	%cCY = CellAmountY.getText();
	//Cell Stride is not implemented yet.
	%cSX = EditorToy.cellStrideX;
	%cSY = EditorToy.cellStrideY;
	%cOX = CellOffX.getText();
	%cOY = CellOffY.getText();
	%cRO = RowOrder.getStateOn();
	%fSix = Force16Bit.getStateOn();
	%mName = EditorToy.moduleName;
	
	//Write Taml File
	%file = new FileObject();
	%file.openForWrite( "^EditorToy/projects/"@ %mName @ "/1/assets/images/" @ %iA @ ".asset.taml" );
	%file.writeLine("<ImageAsset");
	%file.writeLine("	AssetName=\"" @ %iA @ "\"");
	%file.writeLine("	ImageFile=\"" @ %iF @ "\"");
	%file.writeLine("	FilterMode=\"" @ %fM @ "\"");
	
	//If these values are zero, no point in writing them
	if(%cRO == 0)
	{
		%file.writeLine("	CellRowOrder=\"false\"");
	}
	if(%fSix > 0)
	{
		%file.writeLine("	Force16Bit=\"true\"");
	}
	if(%cCX > 0)
	{
		%file.writeLine("	CellCountX=\"" @ %cCX @ "\"");
	}
	if(%cCY > 0)
	{
		%file.writeLine("	CellCountY=\"" @ %cCY @ "\"");
	}
	if(%cW > 0)
	{
		%file.writeLine("	CellWidth=\"" @ %cW @ "\"");
	}
	if(%cH > 0)
	{
		%file.writeLine("	CellHeight=\"" @ %cH @ "\"");
	}
	if(%cOX > 0)
	{
		%file.writeLine("	CellOffsetX=\"" @ %cOX @ "\"");
	}
	if(%cOY > 0)
	{
		%file.writeLine("	CellOffsetY=\"" @ %cOY @ "\"");
	}
	if(%cSX > 0)
	{
		%file.writeLine("	CellStrideX=\"" @ %cSX @ "\"");
	}
	if(%cSY > 0)
	{
		%file.writeLine("	CellStrideY=\"" @ %cSY @ "\"");
	}
	
	%file.writeLine("/>");
	%file.close();
	
	%assetFile = "^EditorToy/projects/"@ %mName @ "/1/assets/images/" @ %iA @ ".asset.taml";
	
	%this.addImageAsset(%assetFile);
}

function EditorToy::addImageAsset(%this, %assetFile)
{
	
	%aFile = %assetFile;
	%mName = EditorToy.moduleName;
	%moduleDef = ModuleDatabase.findModule( %mName , 1);
	AssetDatabase.addDeclaredAsset(%moduleDef,%aFile);
	%this.resetImageAssetDefaults();
	EditorToy.populateAssetSims();
}
//-----------------------------------------------------------------------------
//Updates for ImageAsset values
function EditorToy::updateImageAName(%this, %value)
{
	%this.imageAName = %value;
}

function EditorToy::updateImageAFile(%this, %value)
{
	%this.imageAFile = %value;
}

function EditorToy::updateFilterMode(%this, %value)
{
	%this.filterMode = %value;
}

function EditorToy::updateCellH(%this, %value)
{
	%this.cellH = %value;
}

function EditorToy::updateCellW(%this, %value)
{
	%this.cellW = %value;
}

function EditorToy::updateCellNX(%this, %value)
{
	%this.cellNX = %value;
}

function EditorToy::updateCellNY(%this, %value)
{
	%this.cellNY = %value;
}

function EditorToy::updateCellStrideX(%this, %value)
{
	%this.cellStrideX = %value;
}

function EditorToy::updateCellStrideY(%this, %value)
{
	%this.cellStrideY = %value;
}

function EditorToy::updateCellOffX(%this, %value)
{
	%this.cellOffX = %value;
}

function EditorToy::updateCellOffY(%this, %value)
{
	%this.cellOffY = %value;
}

function EditorToy::updateForce16Bit(%this, %value)
{
	%this.force16Bit = %value;
}

function EditorToy::updateRowOrder(%this, %value)
{
	%this.cellRowOrder = %value;
}

function EditorToy::updateShowFrames(%this, %value)
{
	%this.showFrames = %value;
}

function EditorToy::updateFilterMode(%this, %value)
{
	%this.filterMode = %value;
}
//-----------------------------------------------------------------------------
//GUI RETURNS FOR IMAGE ASSET EDITOR
//-----------------------------------------------------------------------------
//Initializer
function Force16Bit::onAdd(%this)
{
	%this.setStateOn(false);
}

//handle Force16Bit Callbacks
function Force16Bit::update(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateForce16Bit(%value);
}

//Initializer
function RowOrder::onAdd(%this)
{
	%this.setStateOn(true);
}

//handle RowOrder Callbacks
function RowOrder::update(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateRowOrder(%value);
	EditorToy.createImageAssetOverlay();
}

//Cell Width handle
//Initializer
function CellWidth::onAdd(%this)
{
    %this.minCellWidth = 0;
	%text = EditorToy.cellW;
    %this.setText(%text);
}

//handle CellWidth Callbacks
function CellWidth::onReturn(%this)
{
	%value = %this.getText();
	if (%value <= %this.minCellWidth)
        %value = %this.minCellWidth;
	EditorToy.updateCellW(%value);
	EditorToy.createImageAssetOverlay();
}

function CellWidth::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	if (%value <= %this.minCellWidth)
        %value = %this.minCellWidth;
	EditorToy.updateCellW(%value);
	EditorToy.createImageAssetOverlay();
}

function CellWidth::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellWidth)
        %value = %this.minCellWidth;
	EditorToy.updateCellW(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellWidth::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellW(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Height handle
//Initializer
function CellHeight::onAdd(%this)
{
    %this.minCellHeight = 0;
	%text = EditorToy.cellH;
    %this.setText(%text);
}

//handle CellHeight Callbacks
function CellHeight::onReturn(%this)
{
	%value = %this.getText();
	if (%value <= %this.minCellHeight)
        %value = %this.minCellHeight;
	EditorToy.updateCellH(%value);
	EditorToy.createImageAssetOverlay();
}

function CellHeight::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	if (%value <= %this.minCellHeight)
        %value = %this.minCellHeight;
	EditorToy.updateCellH(%value);
	EditorToy.createImageAssetOverlay();
}

function CellHeight::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellHeight)
        %value = %this.minCellHeight;
	EditorToy.updateCellH(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellHeight::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellH(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Amount X
//Initializer
function CellAmountX::onAdd(%this)
{
    %this.minCellCX = 0;
	%text = EditorToy.cellNX;
    %this.setText(%text);
}

//handle CellAmountX Callbacks
function CellAmountX::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellCX)
        %value = %this.minCellCX;
	EditorToy.updateCellNX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellAmountX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellCX)
        %value = %this.minCellCX;
	EditorToy.updateCellNX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellAmountX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellCX)
        %value = %this.minCellCX;
	EditorToy.updateCellNX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellAmountX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellNX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Amount Y
//Initializer
function CellAmountY::onAdd(%this)
{
    %this.minCellCY = 0;
	%text = EditorToy.cellNY;
    %this.setText(%text);
}

//handle CellAmountY Callbacks
function CellAmountY::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellCY)
        %value = %this.minCellCY;
	EditorToy.updateCellNY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellAmountY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellCY)
        %value = %this.minCellCY;
	EditorToy.updateCellNY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellAmountY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellCY)
        %value = %this.minCellCY;
	EditorToy.updateCellNY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellAmountY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellNY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Offset X
//Initializer
function CellOffX::onAdd(%this)
{
    %this.minCellOX = 0;
	%text = EditorToy.cellOffX;
    %this.setText(%text);
}

//handle CellOffX Callbacks
function CellOffX::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellOX)
        %value = %this.minCellOX;
	EditorToy.updateCellOffX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellOffX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellOX)
        %value = %this.minCellOX;
	EditorToy.updateCellOffX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellOffX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellOX)
        %value = %this.minCellOX;
	EditorToy.updateCellOffX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellOffX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellOffX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Offset Y
//Initializer
function CellOffY::onAdd(%this)
{
    %this.minCellOY = 0;
	%text = EditorToy.cellOffY;
    %this.setText(%text);
}

//handle CellOffY Callbacks
function CellOffY::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellOY)
        %value = %this.minCellOY;
	EditorToy.updateCellOffY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellOffY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value <= %this.minCellOY)
        %value = %this.minCellOY;
	EditorToy.updateCellOffY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellOffY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value <= %this.minCellOY)
        %value = %this.minCellOY;
	EditorToy.updateCellOffY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellOffY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellOffY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
//Cell Stride X
//Initializer
function CellStrideX::onAdd(%this)
{
    %this.minCellOX = 0;
	%text = EditorToy.cellStrideX;
    %this.setText(%text);
}

//handle CellStrideX Callbacks
function CellStrideX::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellStrideX::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideX(%value);
	EditorToy.createImageAssetOverlay();
}

function CellStrideX::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellStrideX::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellStrideX(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Cell Stride Y
//Initializer
function CellStrideY::onAdd(%this)
{
    %this.minCellOY = 0;
	%text = EditorToy.cellStrideY;
    %this.setText(%text);
}

//handle CellStrideY Callbacks
function CellStrideY::onReturn(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellStrideY::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	%value = %value;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideY(%value);
	EditorToy.createImageAssetOverlay();
}

function CellStrideY::lowerAmount(%this)
{
    %value = %this.getText();
	%value = %value - 1;
	if (%value < 0)
        %value = 0;
	EditorToy.updateCellStrideY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

function CellStrideY::raiseAmount(%this)
{
    %value = %this.getText();
	%value = %value + 1;
	EditorToy.updateCellStrideY(%value);
    %text = %value;
    %this.setText(%text);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Show Frame Number
function ShowFrameNum::onAdd(%this)
{
	%this.setStateOn(true);
}

//handle Show Frame Number Callbacks
function ShowFrameNum::update(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateShowFrames(%value);
	EditorToy.createImageAssetOverlay();
}

//-----------------------------------------------------------------------------
//Filter Mode
function FilterList::onAdd(%this)
{
	%this.add( "Nearest", 1);
	%this.add( "Bilinear", 2);
}

function FilterList::update(%this)
{
	%value = EditorToy.filterMode;
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function FilterList::onReturn(%this)
{
	%value = %this.getText();
	
	EditorToy.updateFilterMode(%value);
	EditorToy.createImageAssetOverlay();
}