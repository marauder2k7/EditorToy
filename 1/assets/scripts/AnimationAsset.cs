function EditorToy::createAnimationAsset(%this, %imgName)
{
	%mName = EditorToy.moduleName;
	AnimationBuilder.setVisible(1);
	%anim  = new AnimationAsset();
	%asset = %mName @ ":" @ %imgName;
	//We have to do this to get the frame count
	%image = AssetDatabase.acquireAsset(%asset);
	%frameCount = %image.getFrameCount();
	ImageFrames.clear();
	for(%i = 0; %i < %frameCount; %i++)
	{
		%imageCont = new GuiBitmapButtonCtrl()
		{
			bitmap = "^EditorToy/assets/gui/images/OverlayBttn";
			bitmapMode = "Stretched";
			autoFitExtents = "0";
			useModifiers = "0";
			useStates = "1";
			masked = "0";
			groupNum = "-1";
			command = "EditorToy.addFrame(" @ %i @ ");";
			buttonType = "PushButton";
			useMouseEvents = "0";
			extent = "64 64";
		};
		
		%imageFrame = new GuiSpriteCtrl()
		{
			Image = %asset;
			Active = "1";
			Position = "0 0";
			Extent = "64 64";
			Frame = %i;
		};
		//hahahahahhahaha i win!!!!
		%imageFrame.add(%imageCont);
		ImageFrames.add(%imageFrame);
	}
	%rows = %frameCount / 5;
	
	ImageFrames.resize( 0, 0, 370, (%rows * 68) + 68);
}

function EditorToy::updateAnimationName(%this, %value)
{
	%this.animationName = %value;
}

function EditorToy::updateAnimationImage(%this, %value)
{
	%this.animationImage = %value;
}

function EditorToy::updateAnimationFrame(%this, %value)
{
	%this.animationFrame = %value;
}

function EditorToy::updateAnimationTime(%this, %value)
{
	%this.animationTime = %value;
}

function EditorToy::updateAnimationLoop(%this, %value)
{
	%this.animationLoop = %value;
}

function EditorToy::updateAnimationRand(%this, %value)
{
	%this.animationRand = %value;
}

function EditorToy::loadAnimationAsset(%this)
{
	%mName = EditorToy.moduleName;
	%AnimAssetLoad = new OpenFileDialog();
	%AnimAssetLoad.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/images/";
	%AnimAssetLoad.Title = "Choose ImageAsset for Animation";
	%AnimAssetLoad.MustExist = true;
	%AnimAssetLoad.Filters = "(*asset.taml)|*.asset.taml";
	//we only want to use assets imported into editor
	%AnimAssetLoad.ChangePath = false;
	
	
	if(%AnimAssetLoad.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %AnimAssetLoad.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because assets are .asset.taml
		//first fileBase takes it to .asset 
		%num = AnimationSim.getCount();
		%imgName = fileBase(%defaultBase);
		%animName = "Animation_" @ %num;
		%this.updateAnimationName(%animName);
		%this.updateAnimationImage(%imgName);
		%AnimAssetLoad.delete();
		%this.createAnimationAsset(%imgName);
		%this.createPreviewAnim();
	}
	if(isObject(%AnimAssetLoad))
	{
		%AnimAssetLoad.delete();
	}
}

function EditorToy::addFrame(%this, %frame)
{
	if(!isObject(FrameSim))
	{
		%frameSim = new SimSet(FrameSim);
	}
	
	%frameNum = new ScriptObject(%frame);
	FrameSim.add(%frameNum);
	%this.updateFrameStack();
}

function EditorToy::removeFrame(%this, %frame)
{
	if(!isObject(FrameSim))
	{
		return;
	}
	
	%object = FrameSim.getObject(%frame);
	%object.delete();
	%this.updateFrameStack();
}

function EditorToy::updateFrameStack(%this)
{
	%mName = EditorToy.moduleName;
	%imgName = EditorToy.animationImage;
	%asset = %mName @ ":" @ %imgName;
	if(!isObject(FrameStackSim))
	{
		%frameStackSim = new SimSet(FrameStackSim);
	}
	//Refresh on change to update gui
	if(isObject(FrameStackSim))
	{
		for (%i = FrameStackSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = FrameStackSim.getObject(%i);
			%object.delete();
		}
	}
	for(%i = 0; %i < FrameSim.getCount(); %i++)
		{
			%fOb = FrameSim.getObject(%i);
			%frameNum = %fOb.getName();
			%imageCont = new GuiBitmapButtonCtrl()
			{
				bitmap = "^EditorToy/assets/gui/images/OverlayBttn";
				bitmapMode = "Stretched";
				autoFitExtents = "0";
				useModifiers = "0";
				useStates = "1";
				masked = "0";
				groupNum = "-1";
				command = "EditorToy.removeFrame(" @ %i @ ");";
				buttonType = "PushButton";
				useMouseEvents = "0";
				extent = "64 64";
			};
			
			%imageFrame = new GuiSpriteCtrl()
			{
				Image = %asset;
				Active = "1";
				Position = "0 0";
				Extent = "64 64";
				Frame = %frameNum;
			};
			%imageFrame.add(%imageCont);
			FrameStack.add(%imageFrame);
			FrameStackSim.add(%imageFrame);
		}

	%this.setAnimationFrames();
}

function EditorToy::setAnimationFrames(%this)
{
	%frames = "";
	%n = 0;
	for(%i = 0; %i < FrameSim.getCount(); %i ++)
	{
		%fOb = FrameSim.getObject(%i);
		%frameNum = %fOb.getName();
		
		if(%n > 0)
		{
			%frames = %frames SPC %frameNum;
		}
		else
		{
			%frames = %frameNum;
			%n =1;
		}
	}
	
	%this.updateAnimationFrame(%frames);
	if(%frames $= "")
		return;
	%this.updatePreviewAnim();
}

function EditorToy::createPreviewAnim(%this)
{
	%mName = EditorToy.moduleName;
	%imgName = EditorToy.animationImage;
	%asset = %mName @ ":" @ %imgName;
	
	%animAsset = new AnimationAsset();
	%animAsset.setImage(%asset);
	%animAsset.setName("PreviewAnim");
	%animAsset.setAnimationFrames("0");
	%mName = EditorToy.moduleName;
	%assetId = AssetDatabase.addPrivateAsset( %animAsset );
	
	%time = %animAsset.getAnimationTime();
	EditorToy.updateAnimationTime(%time);
	%loop = %animAsset.getAnimationCycle();
	EditorToy.updateAnimationLoop(%loop);
	
	%imageFrame = new GuiSpriteCtrl(PreviewAnimation)
	{
		Image = %asset;
		Active = "1";
		Position = "50 50";
		Extent = "300 300";
		
	};
	%imageFrame.setAnimation(%assetId);
	PreviewWindow.add(%imageFrame);
	
	AnimName.update();
	AnimTime.update();
	AnimLoop.update();
}

function EditorToy::pausePreviewAnim(%this, %value)
{
	PreviewAnimation.pauseAnimation(%value);
}

function EditorToy::updatePreviewAnim(%this)
{
	%frames = EditorToy.animationFrame;
	%time = EditorToy.animationTime;
	%loop = EditorToy.animationLoop;
	%rand = EditorToy.animationRand;
	PreviewAnim.setAnimationFrames(%frames);
	PreviewAnim.setAnimationTime(%time);
	//PreviewAnim.setAnimationCycle(%loop);
	PreviewAnim.RandomStart = %rand;
}

function AnimName::update(%this)
{
	%text = EditorToy.animationName;
	%this.setText(%text);
}

function AnimName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateAnimationName(%value);
}

function AnimTime::update(%this)
{
	%text = EditorToy.animationTime;
	%this.setText(%text);
}

function AnimTime::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateAnimationTime(%value);
	EditorToy.updatePreviewAnim();
}

function AnimLoop::update(%this)
{
	%value = EditorToy.animationLoop;
	%this.setStateOn(%value);
}

function AnimLoop::onReturn(%this)
{
	%value = %this.getStateOn();
	if(!isObject(FrameSim))
		return;
	EditorToy.updateAnimationLoop(%value);
}

function AnimRand::onReturn(%this)
{
	%value = %this.getStateOn();
	EditorToy.updateAnimationRand(%value);
	EditorToy.updatePreviewAnim();
}

function EditorToy::exportAnimationTaml(%this)
{
	%mName = EditorToy.moduleName;
	%name = EditorToy.animationName;
	%frames = EditorToy.animationFrame;
	%img = EditorToy.animationImage;
	%loop = EditorToy.animationLoop;
	%time = EditorToy.animationTime;
	%rand = EditorToy.animationRand;
	PreviewAnimation.delete();
	PreviewWindow.clear();
	
	%defaultLocation = "^EditorToy/projects/"@ %mName @ "/1/assets/animations/";
	%defaultTaml = "^EditorToy/assets/defaults/empty.taml";
	%toTaml = %defaultLocation @ %img @ "_" @ %name @ ".asset.taml";
	
	//Write Taml File
	%file = new FileObject();
	%file.openForWrite( "^EditorToy/projects/"@ %mName @ "/1/assets/animations/" @ %img @ "_"  @ %name @ ".asset.taml" );
	%file.writeLine("<AnimationAsset");
	%file.writeLine("	AssetName=\"" @ %img @ "_"  @ %name @ "\"");
	%file.writeLine("	Image=\"@asset=" @ %mName @ ":" @ %img @ "\"");
	%file.writeLine("	AnimationFrames=\"" @ %frames @ "\"");
	%file.writeLine("	AnimationTime=\"" @ %time @ "\"");
	%file.writeLine("	AnimationCycle=\"" @ %loop @ "\"");
	%file.writeLine("	RandomStart=\"" @ %rand @ "\"");
	%file.writeLine("/>");
	%file.close();
	
	%assetFile = "^EditorToy/projects/"@ %mName @ "/1/assets/animations/" @ %name @ ".asset.taml";
	%this.addAnimationAsset(%assetFile);
}

function EditorToy::addAnimationAsset(%this, %assetFile)
{
	%aFile = %assetFile;
	%mName = EditorToy.moduleName;
	%moduleDef = ModuleDatabase.findModule( %mName , 1);
	AssetDatabase.addDeclaredAsset(%moduleDef,%aFile);
	%this.resetAnimationAssetDefaults();
}

function EditorToy::resetAnimationAssetDefaults(%this)
{
	//SandboxWindow.remove(AnimationBuilder);
	//AnimationBuilder.setVisible(0);
	EditorToy.populateAssetSims();
	//Clear FrameStackSim
	if(isObject(FrameStackSim))
	{
		for (%i = FrameStackSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = FrameStackSim.getObject(%i);
			%object.delete();
		}
	}
	
	//Clear FrameSim
	if(isObject(FrameSim))
	{
		for (%i = FrameSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = FrameSim.getObject(%i);
			%object.delete();
		}
	}
	%num = AnimationSim.getCount();
	%animName = "Animation_" @ %num;
	%this.updateAnimationName(%animName);
	
	%imgName = %this.animationImage;
	%this.createAnimationAsset(%imgName);
	%this.createPreviewAnim();
}

function EditorToy::closeAnimationBuilder(%this)
{
	AnimationBuilder.setVisible(0);
	PreviewAnimation.delete();
	PreviewWindow.clear();
	if(isObject(FrameStackSim))
	{
		for (%i = FrameStackSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = FrameStackSim.getObject(%i);
			%object.delete();
		}
	}
	
	//Clear FrameSim
	if(isObject(FrameSim))
	{
		for (%i = FrameSim.getCount() -1; %i >= 0; %i-- )
		{
			%object = FrameSim.getObject(%i);
			%object.delete();
		}
	}
	
	//Reset Defaults
	EditorToy.animationName = "default";
	EditorToy.animationImage = "default";
	EditorToy.animationFrame = "";
	EditorToy.animationTime = 1.0;
	EditorToy.animationLoop = false;
	EditorToy.animationRand = false;
	
}