function EditorToy::createParticleAsset(%this)
{
	%this.createDataKeySim();
	%scene = EditorToy.activeScene;
	%effect = new ParticleAsset(DefaultParticleAsset);
	%effect.AssetName = "Default";
	%emitter = %effect.createEmitter();
	%emitter.EmitterName = "DefaultEmitter 0";
	%emitter.Image = "EditorToy:Blocks";
	ParticleAssetSettings.setVisible(1);
	ParticleAssetMenu.setVisible(1);
	EditorToy.addEmitterPage(0);
	
	%assetId = AssetDatabase.addPrivateAsset(%effect);
	
	%player = new ParticlePlayer(ParticlePlayground);
	%player.Particle = %assetId;
	%player.setActive(0);
	%player.setPickingAllowed(0);
	%scene.add(%player);
	PAssetName.update();
	PAssetLifemode.update();
	PAssetLifetime.update();
	//Update zero datakey they need to be
	//first in the list for datakey updates
	PLifetimeScaleTime0.update();
	PLifetimeScaleValue0.update();
	PQuantityScaleTime0.update();
	PQuantityScaleValue0.update();
	PSizeXScaleTime0.update();
	PSizeXScaleValue0.update();
	PSizeYScaleTime0.update();
	PSizeYScaleValue0.update();
	PSpeedScaleTime0.update();
	PSpeedScaleValue0.update();
	PSpinScaleTime0.update();
	PSpinScaleValue0.update();
	PFixedForceScaleTime0.update();
	PFixedForceScaleValue0.update();
	PRandomMotionScaleTime0.update();
	PRandomMotionScaleValue0.update();
	PAlphaChannelScaleTime0.update();
	PAlphaChannelScaleValue0.update();
}

function EditorToy::saveParticleAsset(%this)
{
	%mName = EditorToy.moduleName;
	%pName = EditorToy.particleAssetName;
	%lMode = EditorToy.particleLifeMode;
	ParticlePlayground.setPaused(1);
	DefaultParticleAsset.setLifeMode(%lMode);
	TamlWrite(DefaultParticleAsset,"modules/EditorToy/1/projects/"@ %mName @ "/1/assets/particles/" @ %pName @ ".asset.taml");
	PEmitterBook.clear();
	ParticleAssetSettings.setVisible(0);
	ParticleAssetMenu.setVisible(0);
	%this.updateParticleAssetFile();
}

function EditorToy::cancelParticleAsset(%this)
{
	DefaultParticleAsset.delete();
	ParticlePlayground.delete();
	PEmitterBook.clear();
	ParticleAssetSettings.setVisible(0);
	ParticleAssetMenu.setVisible(0);
}

function EditorToy::updateParticleAssetFile(%this)
{
	%mName = EditorToy.moduleName;
	%pName = EditorToy.particleAssetName;
	%lMode = EditorToy.particleLifeMode;
	//Because the particle asset is created as a private asset it is given 
	//a random name from the AssetDatabase. We need to open the file and 
	//rewrite the third line to the name set by the user
	%file = new FileStreamObject();
	%file.open( "^EditorToy/projects/"@ %mName @ "/1/assets/particles/" @ %pName @ ".asset.taml", readwrite );
	%file.readLine();
	%file.readLine();
	%file.writeLine("	AssetName=\"" @ %pName @ "\" ");
	%file.close();
	
	DefaultParticleAsset.delete();
	ParticlePlayground.delete();
	
	%assetFile = "^EditorToy/projects/"@ %mName @ "/1/assets/particles/" @ %pName @ ".asset.taml";
	
	%this.addParticleAsset(%assetFile);
}

function EditorToy::addParticleAsset(%this, %assetFile)
{
	%aFile = %assetFile;
	%mName = EditorToy.moduleName;
	%moduleDef = ModuleDatabase.findModule( %mName , 1);
	AssetDatabase.addDeclaredAsset(%moduleDef,%aFile);
	EditorToy.populateAssetSims();
}

function EditorToy::createDataKeySim(%this)
{
	if(!isObject (LifetimeSim))
	{
		new SimSet(LifetimeSim);
	}
	
	if(!isObject (LifetimeVarSim))
	{
		new SimSet(LifetimeVarSim);
	}
	
	if(!isObject (QuantitySim))
	{
		new SimSet(QuantitySim);
	}
	
	if(!isObject (QuantityVarSim))
	{
		new SimSet(QuantityVarSim);
	}
	
	if(!isObject (SizeXSim))
	{
		new SimSet(SizeXSim);
	}
	
	if(!isObject (SizeXVarSim))
	{
		new SimSet(SizeXVarSim);
	}
	
	if(!isObject (SizeXLifeSim))
	{
		new SimSet(SizeXLifeSim);
	}

	if(!isObject (SizeYSim))
	{
		new SimSet(SizeYSim);
	}
	
	if(!isObject (SizeYVarSim))
	{
		new SimSet(SizeYVarSim);
	}
	
	if(!isObject (SizeYLifeSim))
	{
		new SimSet(SizeYLifeSim);
	}

	if(!isObject (SpeedSim))
	{
		new SimSet(SpeedSim);
	}
	
	if(!isObject (SpeedVarSim))
	{
		new SimSet(SpeedVarSim);
	}
	
	if(!isObject (SpeedLifeSim))
	{
		new SimSet(SpeedLifeSim);
	}
	
	if(!isObject (SpinSim))
	{
		new SimSet(SpinSim);
	}
	
	if(!isObject (SpinVarSim))
	{
		new SimSet(SpinVarSim);
	}
	
	if(!isObject (SpinLifeSim))
	{
		new SimSet(SpinLifeSim);
	}
	
	if(!isObject (FixedForceSim))
	{
		new SimSet(FixedForceSim);
	}
	
	if(!isObject (FixedForceVarSim))
	{
		new SimSet(FixedForceVarSim);
	}
	
	if(!isObject (FixedForceLifeSim))
	{
		new SimSet(FixedForceLifeSim);
	}

	if(!isObject (RandomMotionSim))
	{
		new SimSet(RandomMotionSim);
	}
	
	if(!isObject (RandomMotionVarSim))
	{
		new SimSet(RandomMotionVarSim);
	}
	
	if(!isObject (RandomMotionLifeSim))
	{
		new SimSet(RandomMotionLifeSim);
	}
		
	if(!isObject (EmissionForceSim))
	{
		new SimSet(EmissionForceSim);
	}
	
	if(!isObject (EmissionForceVarSim))
	{
		new SimSet(EmissionForceVarSim);
	}

	if(!isObject (EmissionAngleSim))
	{
		new SimSet(EmissionAngleSim);
	}
	
	if(!isObject (EmissionAngleVarSim))
	{
		new SimSet(EmissionAngleVarSim);
	}

	if(!isObject (EmissionArcSim))
	{
		new SimSet(EmissionArcSim);
	}
	
	if(!isObject (EmissionArcVarSim))
	{
		new SimSet(EmissionArcVarSim);
	}

	if(!isObject (RedChannelSim))
	{
		new SimSet(RedChannelSim);
	}
	
	if(!isObject (GreenChannelSim))
	{
		new SimSet(GreenChannelSim);
	}
	
	if(!isObject (BlueChannelSim))
	{
		new SimSet(BlueChannelSim);
	}
	
		if(!isObject (AlphaChannelSim))
	{
		new SimSet(AlphaChannelSim);
	}
	
	//Asset only sims
	if(!isObject (LifetimeScaleSim))
	{
		new SimSet(LifetimeScaleSim);
	}
	
	if(!isObject (QuantityScaleSim))
	{
		new SimSet(QuantityScaleSim);
	}
	
	if(!isObject (SizeXScaleSim))
	{
		new SimSet(SizeXScaleSim);
	}
	
	if(!isObject (SizeYScaleSim))
	{
		new SimSet(SizeYScaleSim);
	}
	
	if(!isObject (SpeedScaleSim))
	{
		new SimSet(SpeedScaleSim);
	}
	
	if(!isObject (SpinScaleSim))
	{
		new SimSet(SpinScaleSim);
	}
	
	if(!isObject (FixedForceScaleSim))
	{
		new SimSet(FixedForceScaleSim);
	}
	
	if(!isObject (RandomMotionScaleSim))
	{
		new SimSet(RandomMotionScaleSim);
	}
	
	if(!isObject (AlphaChannelScaleSim))
	{
		new SimSet(AlphaChannelScaleSim);
	}
	
}

function EditorToy::updateAssetDataKey(%this)
{
	%lifetimeScaleCount = LifetimeScaleSim.getCount();
	if(%lifetimeScaleCount > 0)
	{
		for(%i = 0; %i < %lifetimeScaleCount; %i++)
		{
			%obj = LifetimeScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("LifetimeScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%quantityScaleCount = QuantityScaleSim.getCount();
	if(%quantityScaleCount > 0)
	{
		for(%i = 0; %i < %quantityScaleCount; %i++)
		{
			%obj = QuantityScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("QuantityScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%sizexScaleCount = SizeXScaleSim.getCount();
	if(%sizexScaleCount > 0)
	{
		for(%i = 0; %i < %sizexScaleCount; %i++)
		{
			%obj = SizeXScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("SizeXScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%sizeyScaleCount = SizeYScaleSim.getCount();
	if(%sizeyScaleCount > 0)
	{
		for(%i = 0; %i < %sizeyScaleCount; %i++)
		{
			%obj = SizeYScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("SizeYScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%speedScaleCount = SpeedScaleSim.getCount();
	if(%speedScaleCount > 0)
	{
		for(%i = 0; %i < %speedScaleCount; %i++)
		{
			%obj = SpeedScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("SpeedScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%spinScaleCount = SpinScaleSim.getCount();
	if(%spinScaleCount > 0)
	{
		for(%i = 0; %i < %spinScaleCount; %i++)
		{
			%obj = SpinScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("SpinScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%fixedForceScaleCount = FixedForceScaleSim.getCount();
	if(%fixedForceScaleCount > 0)
	{
		for(%i = 0; %i < %fixedForceScaleCount; %i++)
		{
			%obj = FixedForceScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("FixedForceScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%randomMotionScaleCount = RandomMotionScaleSim.getCount();
	if(%randomMotionScaleCount > 0)
	{
		for(%i = 0; %i < %randomMotionScaleCount; %i++)
		{
			%obj = RandomMotionScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("RandomMotionScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%alphaChannelScaleCount = AlphaChannelScaleSim.getCount();
	if(%alphaChannelScaleCount > 0)
	{
		for(%i = 0; %i < %alphaChannelScaleCount; %i++)
		{
			%obj = AlphaChannelScaleSim.getObject(%i);
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.selectField("AlphaChannelScale");
				DefaultParticleAsset.clearDataKeys();
				DefaultParticleAsset.setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

}

function EditorToy::addEmitterAsset(%this)
{
	//This will always be 1 number higher than
	//Emitter Ids.
	%count = DefaultParticleAsset.getEmitterCount();
	%emitter = DefaultParticleAsset.createEmitter();
	%emitter.EmitterName = "DefaultEmitter " @ %count;
	%emitter.Image = "EditorToy:Blocks";
	EditorToy.addEmitterPage(%count);
}

function EditorToy::updateDataKey(%this)
{
	%lifetimeCount = LifetimeSim.getCount();
	if(%lifetimeCount > 0)
	{
		for(%i = 0; %i < %lifetimeCount; %i++)
		{
			%obj = LifetimeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("Lifetime");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%lifetimeCount = LifetimeVarSim.getCount();
	if(%lifetimeCount > 0)
	{
		for(%i = 0; %i < %lifetimeCount; %i++)
		{
			%obj = LifetimeVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("LifetimeVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%quantityCount = QuantitySim.getCount();
	if(%quantityCount > 0)
	{
		for(%i = 0; %i < %quantityCount; %i++)
		{
			%obj = QuantitySim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("Quantity");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%quantityCount = QuantityVarSim.getCount();
	if(%quantityCount > 0)
	{
		for(%i = 0; %i < %quantityCount; %i++)
		{
			%obj = QuantityVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("QuantityVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%sizexCount = SizeXSim.getCount();
	if(%sizexCount > 0)
	{
		for(%i = 0; %i < %sizexCount; %i++)
		{
			%obj = SizeXSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeX");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%sizexCount = SizeXVarSim.getCount();
	if(%sizexCount > 0)
	{
		for(%i = 0; %i < %sizexCount; %i++)
		{
			%obj = SizeXVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeXVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%sizexCount = SizeXLifeSim.getCount();
	if(%sizexCount > 0)
	{
		for(%i = 0; %i < %sizexCount; %i++)
		{
			%obj = SizeXLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeXLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%sizeyCount = SizeYSim.getCount();
	if(%sizeyCount > 0)
	{
		for(%i = 0; %i < %sizeyCount; %i++)
		{
			%obj = SizeYSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeY");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%sizeyCount = SizeYVarSim.getCount();
	if(%sizeyCount > 0)
	{
		for(%i = 0; %i < %sizeyCount; %i++)
		{
			%obj = SizeYVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeYVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%sizeyCount = SizeYLifeSim.getCount();
	if(%sizeyCount > 0)
	{
		for(%i = 0; %i < %sizeyCount; %i++)
		{
			%obj = SizeYLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SizeYLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%speedCount = SpeedSim.getCount();
	if(%speedCount > 0)
	{
		for(%i = 0; %i < %speedCount; %i++)
		{
			%obj = SpeedSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("Speed");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%speedCount = SpeedVarSim.getCount();
	if(%speedCount > 0)
	{
		for(%i = 0; %i < %speedCount; %i++)
		{
			%obj = SpeedVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SpeedVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%speedCount = SpeedLifeSim.getCount();
	if(%speedCount > 0)
	{
		for(%i = 0; %i < %speedCount; %i++)
		{
			%obj = SpeedLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SpeedLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%spinCount = SpinSim.getCount();
	if(%spinCount > 0)
	{
		for(%i = 0; %i < %spinCount; %i++)
		{
			%obj = SpinSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("Spin");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%spinCount = SpinVarSim.getCount();
	if(%spinCount > 0)
	{
		for(%i = 0; %i < %spinCount; %i++)
		{
			%obj = SpinVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SpinVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%spinCount = SpinLifeSim.getCount();
	if(%spinCount > 0)
	{
		for(%i = 0; %i < %spinCount; %i++)
		{
			%obj = SpinLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("SpinLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%fixedForceCount = FixedForceSim.getCount();
	if(%fixedForceCount > 0)
	{
		for(%i = 0; %i < %fixedForceCount; %i++)
		{
			%obj = FixedForceSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("FixedForce");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%fixedForceCount = FixedForceVarSim.getCount();
	if(%fixedForceCount > 0)
	{
		for(%i = 0; %i < %fixedForceCount; %i++)
		{
			%obj = FixedForceVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("FixedForceVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%fixedForceCount = FixedForceLifeSim.getCount();
	if(%fixedForceCount > 0)
	{
		for(%i = 0; %i < %fixedForceCount; %i++)
		{
			%obj = FixedForceLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("FixedForceLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}


	%randomMotionCount = RandomMotionSim.getCount();
	if(%randomMotionCount > 0)
	{
		for(%i = 0; %i < %randomMotionCount; %i++)
		{
			%obj = RandomMotionSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("RandomMotion");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%randomMotionCount = RandomMotionVarSim.getCount();
	if(%randomMotionCount > 0)
	{
		for(%i = 0; %i < %randomMotionCount; %i++)
		{
			%obj = RandomMotionVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("RandomMotionVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%randomMotionCount = RandomMotionLifeSim.getCount();
	if(%randomMotionCount > 0)
	{
		for(%i = 0; %i < %randomMotionCount; %i++)
		{
			%obj = RandomMotionLifeSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("RandomMotionLife");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}


	%emissionForceCount = EmissionForceSim.getCount();
	if(%emissionForceCount > 0)
	{
		for(%i = 0; %i < %emissionForceCount; %i++)
		{
			%obj = EmissionForceSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionForce");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%emissionForceCount = EmissionForceVarSim.getCount();
	if(%emissionForceCount > 0)
	{
		for(%i = 0; %i < %emissionForceCount; %i++)
		{
			%obj = EmissionForceVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionForceVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%emissionAngleCount = EmissionAngleSim.getCount();
	if(%emissionAngleCount > 0)
	{
		for(%i = 0; %i < %emissionAngleCount; %i++)
		{
			%obj = EmissionAngleSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionAngle");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%emissionAngleCount = EmissionAngleVarSim.getCount();
	if(%emissionAngleCount > 0)
	{
		for(%i = 0; %i < %emissionAngleCount; %i++)
		{
			%obj = EmissionAngleVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionAngleVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%emissionArcCount = EmissionArcSim.getCount();
	if(%emissionArcCount > 0)
	{
		for(%i = 0; %i < %emissionArcCount; %i++)
		{
			%obj = EmissionArcSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionArc");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%emissionArcCount = EmissionArcVarSim.getCount();
	if(%emissionArcCount > 0)
	{
		for(%i = 0; %i < %emissionArcCount; %i++)
		{
			%obj = EmissionArcVarSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("EmissionArcVariation");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%redChannelCount = RedChannelSim.getCount();
	if(%redChannelCount > 0)
	{
		for(%i = 0; %i < %redChannelCount; %i++)
		{
			%obj = RedChannelSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("RedChannel");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

	%greenChannelCount = GreenChannelSim.getCount();
	if(%greenChannelCount > 0)
	{
		for(%i = 0; %i < %greenChannelCount; %i++)
		{
			%obj = GreenChannelSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("GreenChannel");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%blueChannelCount = BlueChannelSim.getCount();
	if(%blueChannelCount > 0)
	{
		for(%i = 0; %i < %blueChannelCount; %i++)
		{
			%obj = BlueChannelSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("BlueChannel");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}
	
	%alphaChannelCount = AlphaChannelSim.getCount();
	if(%alphaChannelCount > 0)
	{
		for(%i = 0; %i < %alphaChannelCount; %i++)
		{
			%obj = AlphaChannelSim.getObject(%i);
			%eId = %obj.EmitterId;
			%key = %obj.Key;
			%time = %obj.Time;
			%value = %obj.Value;
			if(%key $= "0")
			{
				echo("correct key");
				DefaultParticleAsset.getEmitter(%eId).selectField("AlphaChannel");
				DefaultParticleAsset.getEmitter(%eId).clearDataKeys();
				DefaultParticleAsset.getEmitter(%eId).setDataKeyValue(0, %value);
				echo(%value);
			}
			else
			{
				echo("other key");
				DefaultParticleAsset.getEmitter(%eId).addDataKey(%obj.Time, %obj.Value);
			}
		}
	}

}

function PAssetName::update(%this)
{
	%id = ParticleSim.getCount();
	%value = "ParticleAsset " @ %id;
	EditorToy.particleAssetName = %value;
	%this.setText(%value);
}

function PAssetName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.particleAssetName = %value;
}

function PAssetName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.particleAssetName = %value;
}

function PAssetLifemode::onAdd(%this)
{
	%this.clear();
	%this.add( "INFINITE"	, 1);
	%this.add( "CYCLE", 2);
	%this.add( "KILL" , 3);
	%this.add( "STOP" , 4);
}

function PAssetLifemode::update(%this)
{
	%value = DefaultParticleAsset.getLifeMode();
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function PAssetLifemode::onSelect(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value $= "KILL")
	{
		EditorToy.particleLifeMode = %value;
		//If we let kill get passed through emitters will delete
		//on completion.
		DefaultParticleAsset.setLifeMode("STOP");
	}
	else
	{
		EditorToy.particleLifeMode = %value;
		DefaultParticleAsset.setLifeMode(%value);
	}
}

function PAssetLifetime::update(%this)
{
	%value = DefaultParticleAsset.getLifetime();
	%this.setText(%value);
}

function PAssetLifetime::onReturn(%this)
{
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	
	DefaultParticleAsset.setLifetime(%value);
}

function PAssetLifetime::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	
	DefaultParticleAsset.setLifetime(%value);
}

//DataKey for LifetimeScale
function PLifetimeScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("LifetimeScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(LifetimeScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(LifetimeScaleKey0 @ %id);
	LifetimeScaleSim.add(%key);
	%this.setText(%time);
}

function PLifetimeScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeScaleKey0 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeScaleKey0 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("LifetimeScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PLifetimeScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeScaleKey0 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeScaleKey0 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleLifetimeScaleKey1::onClick(%this)
{
	  
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeScaleKey1 @ %id))
	{
		%key = new SimObject(LifetimeScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeScaleKey1 @ %id);
		LifetimeScaleSim.add(%key);
		LifetimeScaleSim.pushToBack(%key);
		PLifetimeScaleTime1.update();
		return;
	}
		
}

function PLifetimeScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PLifetimeScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PLifetimeScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleLifetimeScaleKey2::onClick(%this)
{
	  
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeScaleKey2 @ %id))
	{
		%key = new SimObject(LifetimeScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeScaleKey2 @ %id);
		LifetimeScaleSim.add(%key);
		LifetimeScaleSim.pushToBack(%key);
		PLifetimeScaleTime2.update();
		return;
	}
		
}

function PLifetimeScaleTime2::update(%this)
{
	  
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PLifetimeScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (LifetimeScaleKey1 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PLifetimeScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleLifetimeScaleKey3::onClick(%this)
{
	  
	if(isObject (LifetimeScaleKey3 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeScaleKey3 @ %id))
	{
		%key = new SimObject(LifetimeScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeScaleKey3 @ %id);
		LifetimeScaleSim.add(%key);
		LifetimeScaleSim.pushToBack(%key);
		PLifetimeScaleTime3.update();
		return;
	}
		
}

function PLifetimeScaleTime3::update(%this)
{
	  
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey3 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PLifetimeScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (LifetimeScaleKey2 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeScaleKey3 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PLifetimeScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeScaleKey3 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PLifetimeScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeScaleKey3 @ %id))
	{
		%objId = LifetimeScaleSim.findObjectByInternalName(LifetimeScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for QuantityScale
function PQuantityScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("QuantityScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(QuantityScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(QuantityScaleKey0 @ %id);
	QuantityScaleSim.add(%key);
	%this.setText(%time);
}

function PQuantityScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityScaleKey0 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityScaleKey0 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("QuantityScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PQuantityScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityScaleKey0 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityScaleKey0 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleQuantityScaleKey1::onClick(%this)
{
	  
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityScaleKey1 @ %id))
	{
		%key = new SimObject(QuantityScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityScaleKey1 @ %id);
		QuantityScaleSim.add(%key);
		QuantityScaleSim.pushToBack(%key);
		PQuantityScaleTime1.update();
		return;
	}
		
}

function PQuantityScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PQuantityScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PQuantityScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleQuantityScaleKey2::onClick(%this)
{
	  
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityScaleKey2 @ %id))
	{
		%key = new SimObject(QuantityScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityScaleKey2 @ %id);
		QuantityScaleSim.add(%key);
		QuantityScaleSim.pushToBack(%key);
		PQuantityScaleTime2.update();
		return;
	}
		
}

function PQuantityScaleTime2::update(%this)
{
	  
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PQuantityScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (QuantityScaleKey1 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PQuantityScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleQuantityScaleKey3::onClick(%this)
{
	  
	if(isObject (QuantityScaleKey3 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityScaleKey3 @ %id))
	{
		%key = new SimObject(QuantityScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityScaleKey3 @ %id);
		QuantityScaleSim.add(%key);
		QuantityScaleSim.pushToBack(%key);
		PQuantityScaleTime3.update();
		return;
	}
		
}

function PQuantityScaleTime3::update(%this)
{
	  
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey3 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PQuantityScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (QuantityScaleKey2 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityScaleKey3 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PQuantityScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityScaleKey3 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PQuantityScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityScaleKey3 @ %id))
	{
		%objId = QuantityScaleSim.findObjectByInternalName(QuantityScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for SizeXScale
function PSizeXScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("SizeXScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(SizeXScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeXScaleKey0 @ %id);
	SizeXScaleSim.add(%key);
	%this.setText(%time);
}

function PSizeXScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXScaleKey0 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXScaleKey0 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("SizeXScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PSizeXScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXScaleKey0 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXScaleKey0 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeXScaleKey1::onClick(%this)
{
	  
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXScaleKey1 @ %id))
	{
		%key = new SimObject(SizeXScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXScaleKey1 @ %id);
		SizeXScaleSim.add(%key);
		SizeXScaleSim.pushToBack(%key);
		PSizeXScaleTime1.update();
		return;
	}
		
}

function PSizeXScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PSizeXScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PSizeXScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeXScaleKey2::onClick(%this)
{
	  
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXScaleKey2 @ %id))
	{
		%key = new SimObject(SizeXScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXScaleKey2 @ %id);
		SizeXScaleSim.add(%key);
		SizeXScaleSim.pushToBack(%key);
		PSizeXScaleTime2.update();
		return;
	}
		
}

function PSizeXScaleTime2::update(%this)
{
	  
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSizeXScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeXScaleKey1 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSizeXScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeXScaleKey3::onClick(%this)
{
	  
	if(isObject (SizeXScaleKey3 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXScaleKey3 @ %id))
	{
		%key = new SimObject(SizeXScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXScaleKey3 @ %id);
		SizeXScaleSim.add(%key);
		SizeXScaleSim.pushToBack(%key);
		PSizeXScaleTime3.update();
		return;
	}
		
}

function PSizeXScaleTime3::update(%this)
{
	  
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey3 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSizeXScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeXScaleKey2 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXScaleKey3 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSizeXScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXScaleKey3 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeXScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXScaleKey3 @ %id))
	{
		%objId = SizeXScaleSim.findObjectByInternalName(SizeXScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for SizeYScale
function PSizeYScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("SizeYScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(SizeYScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeYScaleKey0 @ %id);
	SizeYScaleSim.add(%key);
	%this.setText(%time);
}

function PSizeYScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYScaleKey0 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYScaleKey0 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("SizeYScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PSizeYScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYScaleKey0 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYScaleKey0 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeYScaleKey1::onClick(%this)
{
	  
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYScaleKey1 @ %id))
	{
		%key = new SimObject(SizeYScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYScaleKey1 @ %id);
		SizeYScaleSim.add(%key);
		SizeYScaleSim.pushToBack(%key);
		PSizeYScaleTime1.update();
		return;
	}
		
}

function PSizeYScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PSizeYScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PSizeYScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeYScaleKey2::onClick(%this)
{
	  
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYScaleKey2 @ %id))
	{
		%key = new SimObject(SizeYScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYScaleKey2 @ %id);
		SizeYScaleSim.add(%key);
		SizeYScaleSim.pushToBack(%key);
		PSizeYScaleTime2.update();
		return;
	}
		
}

function PSizeYScaleTime2::update(%this)
{
	  
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSizeYScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeYScaleKey1 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSizeYScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSizeYScaleKey3::onClick(%this)
{
	  
	if(isObject (SizeYScaleKey3 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYScaleKey3 @ %id))
	{
		%key = new SimObject(SizeYScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYScaleKey3 @ %id);
		SizeYScaleSim.add(%key);
		SizeYScaleSim.pushToBack(%key);
		PSizeYScaleTime3.update();
		return;
	}
		
}

function PSizeYScaleTime3::update(%this)
{
	  
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey3 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSizeYScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SizeYScaleKey2 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYScaleKey3 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSizeYScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYScaleKey3 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSizeYScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYScaleKey3 @ %id))
	{
		%objId = SizeYScaleSim.findObjectByInternalName(SizeYScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for SpeedScale
function PSpeedScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("SpeedScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(SpeedScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpeedScaleKey0 @ %id);
	SpeedScaleSim.add(%key);
	%this.setText(%time);
}

function PSpeedScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedScaleKey0 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedScaleKey0 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("SpeedScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PSpeedScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedScaleKey0 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedScaleKey0 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpeedScaleKey1::onClick(%this)
{
	  
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedScaleKey1 @ %id))
	{
		%key = new SimObject(SpeedScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedScaleKey1 @ %id);
		SpeedScaleSim.add(%key);
		SpeedScaleSim.pushToBack(%key);
		PSpeedScaleTime1.update();
		return;
	}
		
}

function PSpeedScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PSpeedScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PSpeedScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpeedScaleKey2::onClick(%this)
{
	  
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedScaleKey2 @ %id))
	{
		%key = new SimObject(SpeedScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedScaleKey2 @ %id);
		SpeedScaleSim.add(%key);
		SpeedScaleSim.pushToBack(%key);
		PSpeedScaleTime2.update();
		return;
	}
		
}

function PSpeedScaleTime2::update(%this)
{
	  
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSpeedScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpeedScaleKey1 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSpeedScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpeedScaleKey3::onClick(%this)
{
	  
	if(isObject (SpeedScaleKey3 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedScaleKey3 @ %id))
	{
		%key = new SimObject(SpeedScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedScaleKey3 @ %id);
		SpeedScaleSim.add(%key);
		SpeedScaleSim.pushToBack(%key);
		PSpeedScaleTime3.update();
		return;
	}
		
}

function PSpeedScaleTime3::update(%this)
{
	  
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey3 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSpeedScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpeedScaleKey2 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedScaleKey3 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSpeedScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedScaleKey3 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpeedScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedScaleKey3 @ %id))
	{
		%objId = SpeedScaleSim.findObjectByInternalName(SpeedScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for SpinScale
function PSpinScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("SpinScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(SpinScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpinScaleKey0 @ %id);
	SpinScaleSim.add(%key);
	%this.setText(%time);
}

function PSpinScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinScaleKey0 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinScaleKey0 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("SpinScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PSpinScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinScaleKey0 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinScaleKey0 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpinScaleKey1::onClick(%this)
{
	  
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinScaleKey1 @ %id))
	{
		%key = new SimObject(SpinScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinScaleKey1 @ %id);
		SpinScaleSim.add(%key);
		SpinScaleSim.pushToBack(%key);
		PSpinScaleTime1.update();
		return;
	}
		
}

function PSpinScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PSpinScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PSpinScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpinScaleKey2::onClick(%this)
{
	  
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinScaleKey2 @ %id))
	{
		%key = new SimObject(SpinScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinScaleKey2 @ %id);
		SpinScaleSim.add(%key);
		SpinScaleSim.pushToBack(%key);
		PSpinScaleTime2.update();
		return;
	}
		
}

function PSpinScaleTime2::update(%this)
{
	  
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSpinScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpinScaleKey1 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSpinScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleSpinScaleKey3::onClick(%this)
{
	  
	if(isObject (SpinScaleKey3 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinScaleKey3 @ %id))
	{
		%key = new SimObject(SpinScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinScaleKey3 @ %id);
		SpinScaleSim.add(%key);
		SpinScaleSim.pushToBack(%key);
		PSpinScaleTime3.update();
		return;
	}
		
}

function PSpinScaleTime3::update(%this)
{
	  
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey3 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PSpinScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (SpinScaleKey2 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinScaleKey3 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PSpinScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinScaleKey3 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PSpinScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinScaleKey3 @ %id))
	{
		%objId = SpinScaleSim.findObjectByInternalName(SpinScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for FixedForceScale
function PFixedForceScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("FixedForceScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(FixedForceScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(FixedForceScaleKey0 @ %id);
	FixedForceScaleSim.add(%key);
	%this.setText(%time);
}

function PFixedForceScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceScaleKey0 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceScaleKey0 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("FixedForceScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PFixedForceScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceScaleKey0 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceScaleKey0 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleFixedForceScaleKey1::onClick(%this)
{
	  
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceScaleKey1 @ %id))
	{
		%key = new SimObject(FixedForceScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceScaleKey1 @ %id);
		FixedForceScaleSim.add(%key);
		FixedForceScaleSim.pushToBack(%key);
		PFixedForceScaleTime1.update();
		return;
	}
		
}

function PFixedForceScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PFixedForceScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PFixedForceScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleFixedForceScaleKey2::onClick(%this)
{
	  
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceScaleKey2 @ %id))
	{
		%key = new SimObject(FixedForceScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceScaleKey2 @ %id);
		FixedForceScaleSim.add(%key);
		FixedForceScaleSim.pushToBack(%key);
		PFixedForceScaleTime2.update();
		return;
	}
		
}

function PFixedForceScaleTime2::update(%this)
{
	  
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PFixedForceScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (FixedForceScaleKey1 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PFixedForceScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleFixedForceScaleKey3::onClick(%this)
{
	  
	if(isObject (FixedForceScaleKey3 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceScaleKey3 @ %id))
	{
		%key = new SimObject(FixedForceScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceScaleKey3 @ %id);
		FixedForceScaleSim.add(%key);
		FixedForceScaleSim.pushToBack(%key);
		PFixedForceScaleTime3.update();
		return;
	}
		
}

function PFixedForceScaleTime3::update(%this)
{
	  
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey3 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PFixedForceScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (FixedForceScaleKey2 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceScaleKey3 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PFixedForceScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceScaleKey3 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PFixedForceScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceScaleKey3 @ %id))
	{
		%objId = FixedForceScaleSim.findObjectByInternalName(FixedForceScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for RandomMotionScale
function PRandomMotionScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("RandomMotionScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(RandomMotionScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(RandomMotionScaleKey0 @ %id);
	RandomMotionScaleSim.add(%key);
	%this.setText(%time);
}

function PRandomMotionScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey0 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey0 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("RandomMotionScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PRandomMotionScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionScaleKey0 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey0 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleRandomMotionScaleKey1::onClick(%this)
{
	  
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionScaleKey1 @ %id))
	{
		%key = new SimObject(RandomMotionScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionScaleKey1 @ %id);
		RandomMotionScaleSim.add(%key);
		RandomMotionScaleSim.pushToBack(%key);
		PRandomMotionScaleTime1.update();
		return;
	}
		
}

function PRandomMotionScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PRandomMotionScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PRandomMotionScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleRandomMotionScaleKey2::onClick(%this)
{
	  
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionScaleKey2 @ %id))
	{
		%key = new SimObject(RandomMotionScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionScaleKey2 @ %id);
		RandomMotionScaleSim.add(%key);
		RandomMotionScaleSim.pushToBack(%key);
		PRandomMotionScaleTime2.update();
		return;
	}
		
}

function PRandomMotionScaleTime2::update(%this)
{
	  
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PRandomMotionScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (RandomMotionScaleKey1 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PRandomMotionScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleRandomMotionScaleKey3::onClick(%this)
{
	  
	if(isObject (RandomMotionScaleKey3 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionScaleKey3 @ %id))
	{
		%key = new SimObject(RandomMotionScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionScaleKey3 @ %id);
		RandomMotionScaleSim.add(%key);
		RandomMotionScaleSim.pushToBack(%key);
		PRandomMotionScaleTime3.update();
		return;
	}
		
}

function PRandomMotionScaleTime3::update(%this)
{
	  
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey3 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PRandomMotionScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (RandomMotionScaleKey2 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionScaleKey3 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PRandomMotionScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionScaleKey3 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PRandomMotionScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionScaleKey3 @ %id))
	{
		%objId = RandomMotionScaleSim.findObjectByInternalName(RandomMotionScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

//DataKey for AlphaChannelScale
function PAlphaChannelScaleTime0::update(%this)
{
	%keyId = %this.KeyId;
	DefaultParticleAsset.selectField("AlphaChannelScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%key = new SimObject(AlphaChannelScaleKey0);
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(AlphaChannelScaleKey0 @ %id);
	AlphaChannelScaleSim.add(%key);
	%this.setText(%time);
}

function PAlphaChannelScaleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey0 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	  
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey0 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleValue0::update(%this)
{
	  
	DefaultParticleAsset.selectField("AlphaChannelScale");
	%keyCount = DefaultParticleAsset.getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.deselectField();
	%this.setText(%value);
}

function PAlphaChannelScaleValue0::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelScaleKey0 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleValue0::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey0 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleAlphaChannelScaleKey1::onClick(%this)
{
	  
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelScaleKey1 @ %id))
	{
		%key = new SimObject(AlphaChannelScaleKey1 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelScaleKey1 @ %id);
		AlphaChannelScaleSim.add(%key);
		AlphaChannelScaleSim.pushToBack(%key);
		PAlphaChannelScaleTime1.update();
		return;
	}
		
}

function PAlphaChannelScaleTime1::update(%this)
{
	%time = 0.01;
	%this.setText(%time);
}

function PAlphaChannelScaleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
}	

function PAlphaChannelScaleValue1::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleValue1::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleAlphaChannelScaleKey2::onClick(%this)
{
	  
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelScaleKey2 @ %id))
	{
		%key = new SimObject(AlphaChannelScaleKey2 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelScaleKey2 @ %id);
		AlphaChannelScaleSim.add(%key);
		AlphaChannelScaleSim.pushToBack(%key);
		PAlphaChannelScaleTime2.update();
		return;
	}
		
}

function PAlphaChannelScaleTime2::update(%this)
{
	  
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PAlphaChannelScaleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PAlphaChannelScaleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (AlphaChannelScaleKey1 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PAlphaChannelScaleValue2::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleValue2::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function ParticleAlphaChannelScaleKey3::onClick(%this)
{
	  
	if(isObject (AlphaChannelScaleKey3 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelScaleKey3 @ %id))
	{
		%key = new SimObject(AlphaChannelScaleKey3 @ %id);
		  
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelScaleKey3 @ %id);
		AlphaChannelScaleSim.add(%key);
		AlphaChannelScaleSim.pushToBack(%key);
		PAlphaChannelScaleTime3.update();
		return;
	}
		
}

function PAlphaChannelScaleTime3::update(%this)
{
	  
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PAlphaChannelScaleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey3 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}

function PAlphaChannelScaleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	  
	if(isObject (AlphaChannelScaleKey2 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelScaleKey3 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateAssetDataKey();
	%this.setText(%value);
}	

function PAlphaChannelScaleValue3::onReturn(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelScaleKey3 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}

function PAlphaChannelScaleValue3::onLoseFirstResponder(%this)
{
	  
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelScaleKey3 @ %id))
	{
		%objId = AlphaChannelScaleSim.findObjectByInternalName(AlphaChannelScaleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateAssetDataKey();
}


//--------------------------------------------------------------------
//Emitter Properties
function ParticleEmitterImage::onAdd(%this)
{
	%count = ImageSim.getCount();
	//Empty selection
	%this.add( " ", 1);
	for(%i = 0; %i < %count; %i++)
	{
		%obj = ImageSim.getObject(%i);
		%name = %obj.getName();
		%num = %i + 2;
		%this.add( %name, %num);
	}
	%id = %this.EmitterId;
	//Set emiter to first image we find and
	//set the popup menu to reflect
	DefaultParticleAsset.getEmitter(%id).setImage(ImageSim.getObject(0).getName());
	%this.setSelected(2);
}

function ParticleEmitterImage::onSelect(%this)
{
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setImage(%value);
}

function ParticleEmitterImageFrame::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getImageFrame();
	%this.setText(%value);
}

function ParticleEmitterImageFrame::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	DefaultParticleAsset.getEmitter(%id).setImageFrame(%value);
	%this.setText(%value);
}

function ParticleEmitterImageFrame::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	DefaultParticleAsset.getEmitter(%id).setImageFrame(%value);
	%this.setText(%value);
}

function ParticleEmitterAlphaTest::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getAlphaTest();
	%this.setText(%value);
}

function ParticleEmitterAlphaTest::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = -1.0;
	if(%value > 1)
		%value = 1;
	DefaultParticleAsset.getEmitter(%id).setAlphaTest(%value);
	%this.setText(%value);
}

function ParticleEmitterAlphaTest::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = -1.0;
	if(%value > 1)
		%value = 1;
	DefaultParticleAsset.getEmitter(%id).setAlphaTest(%value);
	%this.setText(%value);
}

function ParticleEmitterIntenseParticles::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getIntenseParticles();
	%this.setStateOn(%value);
}

function ParticleEmitterIntenseParticles::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setIntenseParticles( %value );
}

function ParticleEmitterBlendMode::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getBlendMode();
	%this.setStateOn(%value);
}

function ParticleEmitterBlendMode::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setBlendMode( %value );
}

function ParticleEmitterSrcBlendFactor::onAdd(%this)
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
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getSrcBlendFactor();
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticleEmitterSrcBlendFactor::onSelect(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setSrcBlendFactor(%value);
}

function ParticleEmitterDstBlendFactor::onAdd(%this)
{
	%this.add( "ZERO"	, 1);
	%this.add( "ONE", 2);
	%this.add( "SRC_COLOR" , 3);
	%this.add( "ONE_MINUS_SRC_COLOR" , 4);
	%this.add( "SRC_ALPHA" , 5);
	%this.add( "ONE_MINUS_SRC_ALPHA" , 6);
	%this.add( "DST_ALPHA" , 7);
	%this.add( "ONE_MINUS_DST_ALPHA" , 8);
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getDstBlendFactor();
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticleEmitterDstBlendFactor::onSelect(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setDstBlendFactor(%value);
}

function ParticleEmitterAnimation::onAdd(%this)
{
	%count = AnimationSim.getCount();
	//Empty selection
	%this.add( " ", 1);
	for(%i = 0; %i < %count; %i++)
	{
		%obj = AnimationSim.getObject(%i);
		%name = %obj.getName();
		%num = %i + 2;
		%this.add( %name, %num);
	}
	%id = %this.EmitterId;
	//Do not set a default animation
}

function ParticleEmitterAnimation::onSelect(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setAnimation(%value);
}

function ParticleEmitterRandomFrame::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setRandomImageFrame( %value );
}

function ParticleEmitterType::onAdd(%this)
{
	%this.add( "POINT"	, 1);
	%this.add( "LINE", 2);
	%this.add( "BOX" , 3);
	%this.add( "DISK" , 4);
	%this.add( "ELLIPSE" , 5);
	%this.add( "TORUS" , 6);
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getEmitterType();
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticleEmitterType::onSelect(%this)
{
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setEmitterType(%value);
}

function ParticleEmitterOrientationType::onAdd(%this)
{
	%this.add( "FIXED"	, 1);
	%this.add( "ALIGNED", 2);
	%this.add( "RANDOM" , 3);
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getOrientationType();
	%value2 = %this.findText(%value);
	%this.setSelected(%value2);
}

function ParticleEmitterOrientationType::onSelect(%this)
{
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setOrientationType(%value);
}

function ParticleEmitterName::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getEmitterName();
	%this.setText(%value);
}

function ParticleEmitterName::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setEmitterName(%value);
}

function ParticleEmitterName::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setEmitterName(%value);
}

function ParticleEmitterSizeX::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%value = getWord(%size,0);
	%this.setText(%value);
}

function ParticleEmitterSizeX::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterSize(%value SPC %y);
}

function ParticleEmitterSizeX::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterSize(%value SPC %y);
}

function ParticleEmitterSizeY::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%value = getWord(%size,1);
	%this.setText(%value);
}

function ParticleEmitterSizeY::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterSize(%x SPC %value);
}

function ParticleEmitterSizeY::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterSize();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterSize(%x SPC %value);
}

function ParticleEmitterAngle::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getEmitterAngle();
	%this.setText(%value);
}

function ParticleEmitterAngle::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setEmitterAngle(%value);
}

function ParticleEmitterAngle::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setEmitterAngle(%value);
}

function ParticleEmitterFixedForceAngle::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getFixedForceAngle();
	%this.setText(%value);
}

function ParticleEmitterFixedForceAngle::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setFixedForceAngle(%value);
}

function ParticleEmitterFixedForceAngle::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setFixedForceAngle(%value);
}

function ParticleEmitterOffsetX::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%value = getWord(%size,0);
	%this.setText(%value);
}

function ParticleEmitterOffsetX::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterOffset(%value SPC %y);
}

function ParticleEmitterOffsetX::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterOffset(%value SPC %y);
}

function ParticleEmitterOffsetY::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%value = getWord(%size,1);
	%this.setText(%value);
}

function ParticleEmitterOffsetY::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterOffset(%x SPC %value);
}

function ParticleEmitterOffsetY::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getEmitterOffset();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setEmitterOffset(%x SPC %value);
}

function ParticleEmitterPivPointX::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%value = getWord(%size,0);
	%this.setText(%value);
}

function ParticleEmitterPivPointX::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setPivotPoint(%value SPC %y);
}

function ParticleEmitterPivPointX::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setPivotPoint(%value SPC %y);
}

function ParticleEmitterPivPointY::onAdd(%this)
{
	%id = %this.EmitterId;
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%value = getWord(%size,1);
	%this.setText(%value);
}

function ParticleEmitterPivPointY::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setPivotPoint(%x SPC %value);
}

function ParticleEmitterPivPointY::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	%size = DefaultParticleAsset.getEmitter(%id).getPivotPoint();
	%x = getWord(%size,0);
	%y = getWord(%size,1);
	DefaultParticleAsset.getEmitter(%id).setPivotPoint(%x SPC %value);
}

function ParticleEmitterKeepAligned::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getKeepAligned();
	%this.setStateOn(%value);
}

function ParticleEmitterKeepAligned::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setKeepAligned( %value );
}

function ParticleEmitterRandomOffset::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getRandomAngleOffset();
	%this.setText(%value);
}

function ParticleEmitterRandomOffset::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setRandomAngleOffset(%value);
}

function ParticleEmitterRandomOffset::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setRandomAngleOffset(%value);
}

function ParticleEmitterRandomArc::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getRandomArc();
	%this.setText(%value);
}

function ParticleEmitterRandomArc::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setRandomArc(%value);
}

function ParticleEmitterRandomArc::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setRandomArc(%value);
}

function ParticleEmitterAlignedOffset::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getAlignedAngleOffset();
	%this.setText(%value);
}

function ParticleEmitterAlignedOffset::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setAlignedAngleOffset(%value);
}

function ParticleEmitterAlignedOffset::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setAlignedAngleOffset(%value);
}

function ParticleEmitterFixedOffset::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getFixedAngleOffset();
	%this.setText(%value);
}

function ParticleEmitterFixedOffset::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setFixedAngleOffset(%value);
}

function ParticleEmitterFixedOffset::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	DefaultParticleAsset.getEmitter(%id).setFixedAngleOffset(%value);
}

function ParticleEmitterFixedAspect::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getFixedAspect();
	%this.setStateOn(%value);
}

function ParticleEmitterFixedAspect::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setFixedAspect( %value );
}

function ParticleEmitterLinkRotation::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getLinkEmissionRotation();
	%this.setStateOn(%value);
}

function ParticleEmitterLinkRotation::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setLinkEmissionRotation( %value );
}

function ParticleEmitterAttachPosition::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getAttachPositionToEmitter();
	%this.setStateOn(%value);
}

function ParticleEmitterAttachPosition::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setAttachPositionToEmitter( %value );
}

function ParticleEmitterAttachRotation::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getAttachRotationToEmitter();
	%this.setStateOn(%value);
}

function ParticleEmitterAttachRotation::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setAttachRotationToEmitter( %value );
}

function ParticleEmitterOldInFront::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getOldestInFront();
	%this.setStateOn(%value);
}

function ParticleEmitterOldInFront::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setOldestInFront( %value );
}

function ParticleEmitterSingleParticle::onAdd(%this)
{
	%id = %this.EmitterId;
	%value = DefaultParticleAsset.getEmitter(%id).getSingleParticle();
	%this.setStateOn(%value);
}

function ParticleEmitterSingleParticle::onClick(%this)
{
	%id = %this.EmitterId;
	%value = %this.getStateOn();
	DefaultParticleAsset.getEmitter(%id).setSingleParticle( %value );
}
//-----------------------------------------------------------------
//DataKey for Lifetime
function PLifetimeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("Lifetime");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(LifetimeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(LifetimeKey0 @ %id);
	LifetimeSim.add(%key);
	%this.setText(%time);
}

function PLifetimeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeKey0 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeKey0 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("Lifetime");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PLifetimeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeKey0 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeKey0 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeKey1 @ %id))
	{
		%key = new SimObject(LifetimeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeKey1 @ %id);
		LifetimeSim.add(%key);
		LifetimeSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PLifetimeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PLifetimeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeKey2 @ %id))
	{
		%key = new SimObject(LifetimeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeKey2 @ %id);
		LifetimeSim.add(%key);
		LifetimeSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PLifetimeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeKey1 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PLifetimeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeKey3 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeKey3 @ %id))
	{
		%key = new SimObject(LifetimeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeKey3 @ %id);
		LifetimeSim.add(%key);
		LifetimeSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey3 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PLifetimeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeKey2 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeKey3 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PLifetimeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeKey3 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeKey3 @ %id))
	{
		%objId = LifetimeSim.findObjectByInternalName(LifetimeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("Lifetime");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(LifetimeVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(LifetimeVarKey0 @ %id);
	LifetimeVarSim.add(%key);
	%this.setText(%time);
}

function PLifetimeVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeVarKey0 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (LifetimeVarKey0 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("LifetimeVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PLifetimeVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeVarKey0 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeVarKey0 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeVarKey1 @ %id))
	{
		%key = new SimObject(LifetimeVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeVarKey1 @ %id);
		LifetimeVarSim.add(%key);
		LifetimeVarSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PLifetimeVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PLifetimeVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeVarKey2 @ %id))
	{
		%key = new SimObject(LifetimeVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeVarKey2 @ %id);
		LifetimeVarSim.add(%key);
		LifetimeVarSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PLifetimeVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey1 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PLifetimeVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleLifetimeVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey3 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (LifetimeVarKey3 @ %id))
	{
		%key = new SimObject(LifetimeVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(LifetimeVarKey3 @ %id);
		LifetimeVarSim.add(%key);
		LifetimeVarSim.pushToBack(%key);
		return;
	}
		
}

function PLifetimeVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PLifetimeVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey3 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PLifetimeVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (LifetimeVarKey2 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (LifetimeVarKey3 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PLifetimeVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (LifetimeVarKey3 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PLifetimeVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (LifetimeVarKey3 @ %id))
	{
		%objId = LifetimeVarSim.findObjectByInternalName(LifetimeVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for Quantity
function PQuantityTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("Quantity");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(QuantityKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(QuantityKey0 @ %id);
	QuantitySim.add(%key);
	%this.setText(%time);
}

function PQuantityTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityKey0 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityKey0 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("Quantity");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PQuantityValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityKey0 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityKey0 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityKey1 @ %id))
	{
		%key = new SimObject(QuantityKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityKey1 @ %id);
		QuantitySim.add(%key);
		QuantitySim.pushToBack(%key);
		return;
	}
		
}

function PQuantityTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PQuantityTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PQuantityValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityKey2 @ %id))
	{
		%key = new SimObject(QuantityKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityKey2 @ %id);
		QuantitySim.add(%key);
		QuantitySim.pushToBack(%key);
		return;
	}
		
}

function PQuantityTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PQuantityTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityKey1 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PQuantityValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityKey3 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityKey3 @ %id))
	{
		%key = new SimObject(QuantityKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityKey3 @ %id);
		QuantitySim.add(%key);
		QuantitySim.pushToBack(%key);
		return;
	}
		
}

function PQuantityTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey3 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PQuantityTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityKey2 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityKey3 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PQuantityValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityKey3 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityKey3 @ %id))
	{
		%objId = QuantitySim.findObjectByInternalName(QuantityKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("QuantityVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(QuantityVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(QuantityVarKey0 @ %id);
	QuantityVarSim.add(%key);
	%this.setText(%time);
}

function PQuantityVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityVarKey0 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (QuantityVarKey0 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("QuantityVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PQuantityVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityVarKey0 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityVarKey0 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityVarKey1 @ %id))
	{
		%key = new SimObject(QuantityVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityVarKey1 @ %id);
		QuantityVarSim.add(%key);
		QuantityVarSim.pushToBack(%key);
		return;
	}
		
}

function PQuantityVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PQuantityVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PQuantityVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityVarKey2 @ %id))
	{
		%key = new SimObject(QuantityVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityVarKey2 @ %id);
		QuantityVarSim.add(%key);
		QuantityVarSim.pushToBack(%key);
		return;
	}
		
}

function PQuantityVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PQuantityVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey1 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PQuantityVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleQuantityVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey3 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (QuantityVarKey3 @ %id))
	{
		%key = new SimObject(QuantityVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(QuantityVarKey3 @ %id);
		QuantityVarSim.add(%key);
		QuantityVarSim.pushToBack(%key);
		return;
	}
		
}

function PQuantityVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PQuantityVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey3 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PQuantityVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (QuantityVarKey2 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (QuantityVarKey3 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PQuantityVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (QuantityVarKey3 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PQuantityVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (QuantityVarKey3 @ %id))
	{
		%objId = QuantityVarSim.findObjectByInternalName(QuantityVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for SizeX
function PSizeXTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeX");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeXKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeXKey0 @ %id);
	SizeXSim.add(%key);
	%this.setText(%time);
}

function PSizeXTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXKey0 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXKey0 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeX");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeXValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXKey0 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXKey0 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXKey1 @ %id))
	{
		%key = new SimObject(SizeXKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXKey1 @ %id);
		SizeXSim.add(%key);
		SizeXSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeXTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeXValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXKey2 @ %id))
	{
		%key = new SimObject(SizeXKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXKey2 @ %id);
		SizeXSim.add(%key);
		SizeXSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXKey1 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXKey3 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXKey3 @ %id))
	{
		%key = new SimObject(SizeXKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXKey3 @ %id);
		SizeXSim.add(%key);
		SizeXSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey3 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXKey2 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXKey3 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXKey3 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXKey3 @ %id))
	{
		%objId = SizeXSim.findObjectByInternalName(SizeXKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeXVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeXVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeXVarKey0 @ %id);
	SizeXVarSim.add(%key);
	%this.setText(%time);
}

function PSizeXVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXVarKey0 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXVarKey0 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeXVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeXVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXVarKey0 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXVarKey0 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXVarKey1 @ %id))
	{
		%key = new SimObject(SizeXVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXVarKey1 @ %id);
		SizeXVarSim.add(%key);
		SizeXVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeXVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeXVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXVarKey2 @ %id))
	{
		%key = new SimObject(SizeXVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXVarKey2 @ %id);
		SizeXVarSim.add(%key);
		SizeXVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey1 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey3 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXVarKey3 @ %id))
	{
		%key = new SimObject(SizeXVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXVarKey3 @ %id);
		SizeXVarSim.add(%key);
		SizeXVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey3 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXVarKey2 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXVarKey3 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXVarKey3 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXVarKey3 @ %id))
	{
		%objId = SizeXVarSim.findObjectByInternalName(SizeXVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeXLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeXLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeXLifeKey0 @ %id);
	SizeXLifeSim.add(%key);
	%this.setText(%time);
}

function PSizeXLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXLifeKey0 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeXLifeKey0 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeXLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeXLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXLifeKey0 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXLifeKey0 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXLifeKey1 @ %id))
	{
		%key = new SimObject(SizeXLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXLifeKey1 @ %id);
		SizeXLifeSim.add(%key);
		SizeXLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeXLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeXLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXLifeKey2 @ %id))
	{
		%key = new SimObject(SizeXLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXLifeKey2 @ %id);
		SizeXLifeSim.add(%key);
		SizeXLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey1 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeXLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey3 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeXLifeKey3 @ %id))
	{
		%key = new SimObject(SizeXLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeXLifeKey3 @ %id);
		SizeXLifeSim.add(%key);
		SizeXLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeXLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeXLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey3 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeXLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeXLifeKey2 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeXLifeKey3 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeXLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeXLifeKey3 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeXLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeXLifeKey3 @ %id))
	{
		%objId = SizeXLifeSim.findObjectByInternalName(SizeXLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for SizeY
function PSizeYTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeY");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeYKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeYKey0 @ %id);
	SizeYSim.add(%key);
	%this.setText(%time);
}

function PSizeYTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYKey0 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYKey0 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeY");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeYValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYKey0 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYKey0 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYKey1 @ %id))
	{
		%key = new SimObject(SizeYKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYKey1 @ %id);
		SizeYSim.add(%key);
		SizeYSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeYTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeYValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYKey2 @ %id))
	{
		%key = new SimObject(SizeYKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYKey2 @ %id);
		SizeYSim.add(%key);
		SizeYSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYKey1 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYKey3 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYKey3 @ %id))
	{
		%key = new SimObject(SizeYKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYKey3 @ %id);
		SizeYSim.add(%key);
		SizeYSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey3 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYKey2 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYKey3 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYKey3 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYKey3 @ %id))
	{
		%objId = SizeYSim.findObjectByInternalName(SizeYKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeYVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeYVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeYVarKey0 @ %id);
	SizeYVarSim.add(%key);
	%this.setText(%time);
}

function PSizeYVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYVarKey0 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYVarKey0 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeYVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeYVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYVarKey0 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYVarKey0 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYVarKey1 @ %id))
	{
		%key = new SimObject(SizeYVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYVarKey1 @ %id);
		SizeYVarSim.add(%key);
		SizeYVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeYVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeYVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYVarKey2 @ %id))
	{
		%key = new SimObject(SizeYVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYVarKey2 @ %id);
		SizeYVarSim.add(%key);
		SizeYVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey1 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey3 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYVarKey3 @ %id))
	{
		%key = new SimObject(SizeYVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYVarKey3 @ %id);
		SizeYVarSim.add(%key);
		SizeYVarSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey3 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYVarKey2 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYVarKey3 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYVarKey3 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYVarKey3 @ %id))
	{
		%objId = SizeYVarSim.findObjectByInternalName(SizeYVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeYLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SizeYLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SizeYLifeKey0 @ %id);
	SizeYLifeSim.add(%key);
	%this.setText(%time);
}

function PSizeYLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYLifeKey0 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SizeYLifeKey0 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SizeYLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSizeYLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYLifeKey0 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYLifeKey0 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYLifeKey1 @ %id))
	{
		%key = new SimObject(SizeYLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYLifeKey1 @ %id);
		SizeYLifeSim.add(%key);
		SizeYLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSizeYLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSizeYLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYLifeKey2 @ %id))
	{
		%key = new SimObject(SizeYLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYLifeKey2 @ %id);
		SizeYLifeSim.add(%key);
		SizeYLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey1 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSizeYLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey3 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SizeYLifeKey3 @ %id))
	{
		%key = new SimObject(SizeYLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SizeYLifeKey3 @ %id);
		SizeYLifeSim.add(%key);
		SizeYLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSizeYLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSizeYLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey3 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSizeYLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SizeYLifeKey2 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SizeYLifeKey3 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSizeYLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SizeYLifeKey3 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSizeYLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SizeYLifeKey3 @ %id))
	{
		%objId = SizeYLifeSim.findObjectByInternalName(SizeYLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for Speed
function PSpeedTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("Speed");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpeedKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpeedKey0 @ %id);
	SpeedSim.add(%key);
	%this.setText(%time);
}

function PSpeedTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedKey0 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedKey0 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("Speed");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpeedValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedKey0 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedKey0 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedKey1 @ %id))
	{
		%key = new SimObject(SpeedKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedKey1 @ %id);
		SpeedSim.add(%key);
		SpeedSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpeedTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpeedValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedKey2 @ %id))
	{
		%key = new SimObject(SpeedKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedKey2 @ %id);
		SpeedSim.add(%key);
		SpeedSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedKey1 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedKey3 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedKey3 @ %id))
	{
		%key = new SimObject(SpeedKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedKey3 @ %id);
		SpeedSim.add(%key);
		SpeedSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey3 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedKey2 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedKey3 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedKey3 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedKey3 @ %id))
	{
		%objId = SpeedSim.findObjectByInternalName(SpeedKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpeedVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpeedVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpeedVarKey0 @ %id);
	SpeedVarSim.add(%key);
	%this.setText(%time);
}

function PSpeedVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedVarKey0 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedVarKey0 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpeedVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpeedVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedVarKey0 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedVarKey0 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedVarKey1 @ %id))
	{
		%key = new SimObject(SpeedVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedVarKey1 @ %id);
		SpeedVarSim.add(%key);
		SpeedVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpeedVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpeedVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedVarKey2 @ %id))
	{
		%key = new SimObject(SpeedVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedVarKey2 @ %id);
		SpeedVarSim.add(%key);
		SpeedVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey1 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey3 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedVarKey3 @ %id))
	{
		%key = new SimObject(SpeedVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedVarKey3 @ %id);
		SpeedVarSim.add(%key);
		SpeedVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey3 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedVarKey2 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedVarKey3 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedVarKey3 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedVarKey3 @ %id))
	{
		%objId = SpeedVarSim.findObjectByInternalName(SpeedVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpeedLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpeedLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpeedLifeKey0 @ %id);
	SpeedLifeSim.add(%key);
	%this.setText(%time);
}

function PSpeedLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedLifeKey0 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpeedLifeKey0 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpeedLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpeedLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedLifeKey0 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedLifeKey0 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedLifeKey1 @ %id))
	{
		%key = new SimObject(SpeedLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedLifeKey1 @ %id);
		SpeedLifeSim.add(%key);
		SpeedLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpeedLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpeedLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedLifeKey2 @ %id))
	{
		%key = new SimObject(SpeedLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedLifeKey2 @ %id);
		SpeedLifeSim.add(%key);
		SpeedLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey1 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpeedLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey3 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpeedLifeKey3 @ %id))
	{
		%key = new SimObject(SpeedLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpeedLifeKey3 @ %id);
		SpeedLifeSim.add(%key);
		SpeedLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpeedLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpeedLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey3 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpeedLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpeedLifeKey2 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpeedLifeKey3 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpeedLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpeedLifeKey3 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpeedLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpeedLifeKey3 @ %id))
	{
		%objId = SpeedLifeSim.findObjectByInternalName(SpeedLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for Spin
function PSpinTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("Spin");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpinKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpinKey0 @ %id);
	SpinSim.add(%key);
	%this.setText(%time);
}

function PSpinTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinKey0 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinKey0 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("Spin");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpinValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinKey0 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinKey0 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinKey1 @ %id))
	{
		%key = new SimObject(SpinKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinKey1 @ %id);
		SpinSim.add(%key);
		SpinSim.pushToBack(%key);
		return;
	}
		
}

function PSpinTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpinTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpinValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinKey2 @ %id))
	{
		%key = new SimObject(SpinKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinKey2 @ %id);
		SpinSim.add(%key);
		SpinSim.pushToBack(%key);
		return;
	}
		
}

function PSpinTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinKey1 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinKey3 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinKey3 @ %id))
	{
		%key = new SimObject(SpinKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinKey3 @ %id);
		SpinSim.add(%key);
		SpinSim.pushToBack(%key);
		return;
	}
		
}

function PSpinTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey3 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinKey2 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinKey3 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinKey3 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinKey3 @ %id))
	{
		%objId = SpinSim.findObjectByInternalName(SpinKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpinVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpinVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpinVarKey0 @ %id);
	SpinVarSim.add(%key);
	%this.setText(%time);
}

function PSpinVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinVarKey0 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinVarKey0 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpinVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpinVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinVarKey0 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinVarKey0 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinVarKey1 @ %id))
	{
		%key = new SimObject(SpinVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinVarKey1 @ %id);
		SpinVarSim.add(%key);
		SpinVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpinVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpinVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpinVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinVarKey2 @ %id))
	{
		%key = new SimObject(SpinVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinVarKey2 @ %id);
		SpinVarSim.add(%key);
		SpinVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpinVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinVarKey1 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinVarKey3 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinVarKey3 @ %id))
	{
		%key = new SimObject(SpinVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinVarKey3 @ %id);
		SpinVarSim.add(%key);
		SpinVarSim.pushToBack(%key);
		return;
	}
		
}

function PSpinVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey3 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinVarKey2 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinVarKey3 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinVarKey3 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinVarKey3 @ %id))
	{
		%objId = SpinVarSim.findObjectByInternalName(SpinVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpinLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(SpinLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(SpinLifeKey0 @ %id);
	SpinLifeSim.add(%key);
	%this.setText(%time);
}

function PSpinLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinLifeKey0 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (SpinLifeKey0 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("SpinLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PSpinLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinLifeKey0 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinLifeKey0 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinLifeKey1 @ %id))
	{
		%key = new SimObject(SpinLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinLifeKey1 @ %id);
		SpinLifeSim.add(%key);
		SpinLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpinLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PSpinLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PSpinLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinLifeKey2 @ %id))
	{
		%key = new SimObject(SpinLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinLifeKey2 @ %id);
		SpinLifeSim.add(%key);
		SpinLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpinLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey1 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleSpinLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey3 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (SpinLifeKey3 @ %id))
	{
		%key = new SimObject(SpinLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(SpinLifeKey3 @ %id);
		SpinLifeSim.add(%key);
		SpinLifeSim.pushToBack(%key);
		return;
	}
		
}

function PSpinLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PSpinLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey3 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PSpinLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (SpinLifeKey2 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (SpinLifeKey3 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PSpinLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (SpinLifeKey3 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PSpinLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (SpinLifeKey3 @ %id))
	{
		%objId = SpinLifeSim.findObjectByInternalName(SpinLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for FixedForce
function PFixedForceTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForce");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(FixedForceKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(FixedForceKey0 @ %id);
	FixedForceSim.add(%key);
	%this.setText(%time);
}

function PFixedForceTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceKey0 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceKey0 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForce");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PFixedForceValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceKey0 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceKey0 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceKey1 @ %id))
	{
		%key = new SimObject(FixedForceKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceKey1 @ %id);
		FixedForceSim.add(%key);
		FixedForceSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PFixedForceTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PFixedForceValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceKey2 @ %id))
	{
		%key = new SimObject(FixedForceKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceKey2 @ %id);
		FixedForceSim.add(%key);
		FixedForceSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceKey1 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceKey3 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceKey3 @ %id))
	{
		%key = new SimObject(FixedForceKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceKey3 @ %id);
		FixedForceSim.add(%key);
		FixedForceSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey3 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceKey2 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceKey3 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceKey3 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceKey3 @ %id))
	{
		%objId = FixedForceSim.findObjectByInternalName(FixedForceKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForceVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(FixedForceVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(FixedForceVarKey0 @ %id);
	FixedForceVarSim.add(%key);
	%this.setText(%time);
}

function PFixedForceVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceVarKey0 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceVarKey0 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForceVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PFixedForceVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceVarKey0 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceVarKey0 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceVarKey1 @ %id))
	{
		%key = new SimObject(FixedForceVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceVarKey1 @ %id);
		FixedForceVarSim.add(%key);
		FixedForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PFixedForceVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PFixedForceVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceVarKey2 @ %id))
	{
		%key = new SimObject(FixedForceVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceVarKey2 @ %id);
		FixedForceVarSim.add(%key);
		FixedForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey1 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey3 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceVarKey3 @ %id))
	{
		%key = new SimObject(FixedForceVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceVarKey3 @ %id);
		FixedForceVarSim.add(%key);
		FixedForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey3 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceVarKey2 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceVarKey3 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceVarKey3 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceVarKey3 @ %id))
	{
		%objId = FixedForceVarSim.findObjectByInternalName(FixedForceVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForceLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(FixedForceLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(FixedForceLifeKey0 @ %id);
	FixedForceLifeSim.add(%key);
	%this.setText(%time);
}

function PFixedForceLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceLifeKey0 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (FixedForceLifeKey0 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("FixedForceLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PFixedForceLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceLifeKey0 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceLifeKey0 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceLifeKey1 @ %id))
	{
		%key = new SimObject(FixedForceLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceLifeKey1 @ %id);
		FixedForceLifeSim.add(%key);
		FixedForceLifeSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PFixedForceLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PFixedForceLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceLifeKey2 @ %id))
	{
		%key = new SimObject(FixedForceLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceLifeKey2 @ %id);
		FixedForceLifeSim.add(%key);
		FixedForceLifeSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey1 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleFixedForceLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey3 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (FixedForceLifeKey3 @ %id))
	{
		%key = new SimObject(FixedForceLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(FixedForceLifeKey3 @ %id);
		FixedForceLifeSim.add(%key);
		FixedForceLifeSim.pushToBack(%key);
		return;
	}
		
}

function PFixedForceLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PFixedForceLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey3 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PFixedForceLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (FixedForceLifeKey2 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (FixedForceLifeKey3 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PFixedForceLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (FixedForceLifeKey3 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PFixedForceLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (FixedForceLifeKey3 @ %id))
	{
		%objId = FixedForceLifeSim.findObjectByInternalName(FixedForceLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for RandomMotion
function PRandomMotionTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotion");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(RandomMotionKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(RandomMotionKey0 @ %id);
	RandomMotionSim.add(%key);
	%this.setText(%time);
}

function PRandomMotionTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionKey0 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionKey0 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotion");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PRandomMotionValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionKey0 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionKey0 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionKey1 @ %id))
	{
		%key = new SimObject(RandomMotionKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionKey1 @ %id);
		RandomMotionSim.add(%key);
		RandomMotionSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PRandomMotionTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PRandomMotionValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionKey2 @ %id))
	{
		%key = new SimObject(RandomMotionKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionKey2 @ %id);
		RandomMotionSim.add(%key);
		RandomMotionSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey1 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey3 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionKey3 @ %id))
	{
		%key = new SimObject(RandomMotionKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionKey3 @ %id);
		RandomMotionSim.add(%key);
		RandomMotionSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey3 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionKey2 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionKey3 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionKey3 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionKey3 @ %id))
	{
		%objId = RandomMotionSim.findObjectByInternalName(RandomMotionKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotionVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(RandomMotionVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(RandomMotionVarKey0 @ %id);
	RandomMotionVarSim.add(%key);
	%this.setText(%time);
}

function PRandomMotionVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionVarKey0 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionVarKey0 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotionVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PRandomMotionVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionVarKey0 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionVarKey0 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionVarKey1 @ %id))
	{
		%key = new SimObject(RandomMotionVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionVarKey1 @ %id);
		RandomMotionVarSim.add(%key);
		RandomMotionVarSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PRandomMotionVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PRandomMotionVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionVarKey2 @ %id))
	{
		%key = new SimObject(RandomMotionVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionVarKey2 @ %id);
		RandomMotionVarSim.add(%key);
		RandomMotionVarSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey1 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey3 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionVarKey3 @ %id))
	{
		%key = new SimObject(RandomMotionVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionVarKey3 @ %id);
		RandomMotionVarSim.add(%key);
		RandomMotionVarSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey3 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionVarKey2 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionVarKey3 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionVarKey3 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionVarKey3 @ %id))
	{
		%objId = RandomMotionVarSim.findObjectByInternalName(RandomMotionVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotionLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(RandomMotionLifeKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(RandomMotionLifeKey0 @ %id);
	RandomMotionLifeSim.add(%key);
	%this.setText(%time);
}

function PRandomMotionLifeTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey0 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey0 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("RandomMotionLife");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PRandomMotionLifeValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionLifeKey0 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey0 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionLifeKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionLifeKey1 @ %id))
	{
		%key = new SimObject(RandomMotionLifeKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionLifeKey1 @ %id);
		RandomMotionLifeSim.add(%key);
		RandomMotionLifeSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionLifeTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PRandomMotionLifeTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PRandomMotionLifeValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionLifeKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionLifeKey2 @ %id))
	{
		%key = new SimObject(RandomMotionLifeKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionLifeKey2 @ %id);
		RandomMotionLifeSim.add(%key);
		RandomMotionLifeSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionLifeTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionLifeTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionLifeTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey1 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionLifeValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRandomMotionLifeKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey3 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RandomMotionLifeKey3 @ %id))
	{
		%key = new SimObject(RandomMotionLifeKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RandomMotionLifeKey3 @ %id);
		RandomMotionLifeSim.add(%key);
		RandomMotionLifeSim.pushToBack(%key);
		return;
	}
		
}

function PRandomMotionLifeTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRandomMotionLifeTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey3 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRandomMotionLifeTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RandomMotionLifeKey2 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RandomMotionLifeKey3 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRandomMotionLifeValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RandomMotionLifeKey3 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRandomMotionLifeValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RandomMotionLifeKey3 @ %id))
	{
		%objId = RandomMotionLifeSim.findObjectByInternalName(RandomMotionLifeKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for EmissionForce
function PEmissionForceTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionForce");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionForceKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionForceKey0 @ %id);
	EmissionForceSim.add(%key);
	%this.setText(%time);
}

function PEmissionForceTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionForceKey0 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionForceKey0 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionForce");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionForceValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceKey0 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceKey0 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceKey1 @ %id))
	{
		%key = new SimObject(EmissionForceKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceKey1 @ %id);
		EmissionForceSim.add(%key);
		EmissionForceSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionForceTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionForceValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceKey2 @ %id))
	{
		%key = new SimObject(EmissionForceKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceKey2 @ %id);
		EmissionForceSim.add(%key);
		EmissionForceSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionForceTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionForceTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey1 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionForceValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey3 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceKey3 @ %id))
	{
		%key = new SimObject(EmissionForceKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceKey3 @ %id);
		EmissionForceSim.add(%key);
		EmissionForceSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionForceTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey3 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionForceTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceKey2 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceKey3 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionForceValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceKey3 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceKey3 @ %id))
	{
		%objId = EmissionForceSim.findObjectByInternalName(EmissionForceKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionForceVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionForceVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionForceVarKey0 @ %id);
	EmissionForceVarSim.add(%key);
	%this.setText(%time);
}

function PEmissionForceVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionForceVarKey0 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionForceVarKey0 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionForceVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionForceVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceVarKey0 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceVarKey0 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceVarKey1 @ %id))
	{
		%key = new SimObject(EmissionForceVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceVarKey1 @ %id);
		EmissionForceVarSim.add(%key);
		EmissionForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionForceVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionForceVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceVarKey2 @ %id))
	{
		%key = new SimObject(EmissionForceVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceVarKey2 @ %id);
		EmissionForceVarSim.add(%key);
		EmissionForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionForceVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionForceVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey1 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionForceVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionForceVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey3 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionForceVarKey3 @ %id))
	{
		%key = new SimObject(EmissionForceVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionForceVarKey3 @ %id);
		EmissionForceVarSim.add(%key);
		EmissionForceVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionForceVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionForceVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey3 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionForceVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionForceVarKey2 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionForceVarKey3 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionForceVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionForceVarKey3 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionForceVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionForceVarKey3 @ %id))
	{
		%objId = EmissionForceVarSim.findObjectByInternalName(EmissionForceVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}


//----------------------------------------------------------------
//DataKey for EmissionAngle
function PEmissionAngleTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionAngle");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionAngleKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionAngleKey0 @ %id);
	EmissionAngleSim.add(%key);
	%this.setText(%time);
}

function PEmissionAngleTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionAngleKey0 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionAngleKey0 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionAngle");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionAngleValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	//Set first datakey to the value
	if(isObject (EmissionAngleKey0 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(isObject (EmissionAngleKey0 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleKey1 @ %id))
	{
		%key = new SimObject(EmissionAngleKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleKey1 @ %id);
		EmissionAngleSim.add(%key);
		EmissionAngleSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionAngleTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionAngleValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	//Set first datakey to the value
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleKey2 @ %id))
	{
		%key = new SimObject(EmissionAngleKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleKey2 @ %id);
		EmissionAngleSim.add(%key);
		EmissionAngleSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionAngleTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionAngleTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey1 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionAngleValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	//Set first datakey to the value
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey3 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleKey3 @ %id))
	{
		%key = new SimObject(EmissionAngleKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleKey3 @ %id);
		EmissionAngleSim.add(%key);
		EmissionAngleSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionAngleTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey3 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionAngleTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleKey2 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleKey3 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionAngleValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(isObject (EmissionAngleKey3 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	//Set first datakey to the value
	if(isObject (EmissionAngleKey3 @ %id))
	{
		%objId = EmissionAngleSim.findObjectByInternalName(EmissionAngleKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionAngleVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionAngleVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionAngleVarKey0 @ %id);
	EmissionAngleVarSim.add(%key);
	%this.setText(%time);
}

function PEmissionAngleVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey0 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey0 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionAngleVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionAngleVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionAngleVarKey0 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey0 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleVarKey1 @ %id))
	{
		%key = new SimObject(EmissionAngleVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleVarKey1 @ %id);
		EmissionAngleVarSim.add(%key);
		EmissionAngleVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionAngleVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionAngleVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleVarKey2 @ %id))
	{
		%key = new SimObject(EmissionAngleVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleVarKey2 @ %id);
		EmissionAngleVarSim.add(%key);
		EmissionAngleVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionAngleVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionAngleVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey1 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionAngleVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionAngleVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey3 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionAngleVarKey3 @ %id))
	{
		%key = new SimObject(EmissionAngleVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionAngleVarKey3 @ %id);
		EmissionAngleVarSim.add(%key);
		EmissionAngleVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionAngleVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionAngleVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey3 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionAngleVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionAngleVarKey2 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionAngleVarKey3 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionAngleVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionAngleVarKey3 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionAngleVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionAngleVarKey3 @ %id))
	{
		%objId = EmissionAngleVarSim.findObjectByInternalName(EmissionAngleVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for EmissionArc
function PEmissionArcTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionArc");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionArcKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionArcKey0 @ %id);
	EmissionArcSim.add(%key);
	%this.setText(%time);
}

function PEmissionArcTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionArcKey0 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionArcKey0 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionArc");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionArcValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcKey0 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcKey0 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcKey1 @ %id))
	{
		%key = new SimObject(EmissionArcKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcKey1 @ %id);
		EmissionArcSim.add(%key);
		EmissionArcSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionArcTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionArcValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcKey2 @ %id))
	{
		%key = new SimObject(EmissionArcKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcKey2 @ %id);
		EmissionArcSim.add(%key);
		EmissionArcSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionArcTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionArcTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey1 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionArcValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey3 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcKey3 @ %id))
	{
		%key = new SimObject(EmissionArcKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcKey3 @ %id);
		EmissionArcSim.add(%key);
		EmissionArcSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionArcTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey3 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionArcTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcKey2 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcKey3 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionArcValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcKey3 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcKey3 @ %id))
	{
		%objId = EmissionArcSim.findObjectByInternalName(EmissionArcKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionArcVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(EmissionArcVarKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(EmissionArcVarKey0 @ %id);
	EmissionArcVarSim.add(%key);
	%this.setText(%time);
}

function PEmissionArcVariationTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionArcVarKey0 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (EmissionArcVarKey0 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("EmissionArcVariation");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PEmissionArcVariationValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcVarKey0 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcVarKey0 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcVarKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcVarKey1 @ %id))
	{
		%key = new SimObject(EmissionArcVarKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcVarKey1 @ %id);
		EmissionArcVarSim.add(%key);
		EmissionArcVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcVariationTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PEmissionArcVariationTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PEmissionArcVariationValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcVarKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcVarKey2 @ %id))
	{
		%key = new SimObject(EmissionArcVarKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcVarKey2 @ %id);
		EmissionArcVarSim.add(%key);
		EmissionArcVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcVariationTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionArcVariationTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionArcVariationTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey1 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionArcVariationValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleEmissionArcVarKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey3 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (EmissionArcVarKey3 @ %id))
	{
		%key = new SimObject(EmissionArcVarKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(EmissionArcVarKey3 @ %id);
		EmissionArcVarSim.add(%key);
		EmissionArcVarSim.pushToBack(%key);
		return;
	}
		
}

function PEmissionArcVariationTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PEmissionArcVariationTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey3 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PEmissionArcVariationTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (EmissionArcVarKey2 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (EmissionArcVarKey3 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PEmissionArcVariationValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (EmissionArcVarKey3 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PEmissionArcVariationValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (EmissionArcVarKey3 @ %id))
	{
		%objId = EmissionArcVarSim.findObjectByInternalName(EmissionArcVarKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for RedChannel
function PRedChannelTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("RedChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(RedChannelKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(RedChannelKey0 @ %id);
	RedChannelSim.add(%key);
	%this.setText(%time);
}

function PRedChannelTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RedChannelKey0 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (RedChannelKey0 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("RedChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PRedChannelValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RedChannelKey0 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RedChannelKey0 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRedChannelKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RedChannelKey1 @ %id))
	{
		%key = new SimObject(RedChannelKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RedChannelKey1 @ %id);
		RedChannelSim.add(%key);
		RedChannelSim.pushToBack(%key);
		return;
	}
		
}

function PRedChannelTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PRedChannelTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PRedChannelValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRedChannelKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RedChannelKey2 @ %id))
	{
		%key = new SimObject(RedChannelKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RedChannelKey2 @ %id);
		RedChannelSim.add(%key);
		RedChannelSim.pushToBack(%key);
		return;
	}
		
}

function PRedChannelTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRedChannelTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRedChannelTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RedChannelKey1 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRedChannelValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleRedChannelKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (RedChannelKey3 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (RedChannelKey3 @ %id))
	{
		%key = new SimObject(RedChannelKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(RedChannelKey3 @ %id);
		RedChannelSim.add(%key);
		RedChannelSim.pushToBack(%key);
		return;
	}
		
}

function PRedChannelTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PRedChannelTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey3 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PRedChannelTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (RedChannelKey2 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (RedChannelKey3 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PRedChannelValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (RedChannelKey3 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PRedChannelValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (RedChannelKey3 @ %id))
	{
		%objId = RedChannelSim.findObjectByInternalName(RedChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for GreenChannel
function PGreenChannelTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("GreenChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(GreenChannelKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(GreenChannelKey0 @ %id);
	GreenChannelSim.add(%key);
	%this.setText(%time);
}

function PGreenChannelTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (GreenChannelKey0 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (GreenChannelKey0 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("GreenChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PGreenChannelValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (GreenChannelKey0 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (GreenChannelKey0 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleGreenChannelKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (GreenChannelKey1 @ %id))
	{
		%key = new SimObject(GreenChannelKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(GreenChannelKey1 @ %id);
		GreenChannelSim.add(%key);
		GreenChannelSim.pushToBack(%key);
		return;
	}
		
}

function PGreenChannelTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PGreenChannelTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PGreenChannelValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleGreenChannelKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (GreenChannelKey2 @ %id))
	{
		%key = new SimObject(GreenChannelKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(GreenChannelKey2 @ %id);
		GreenChannelSim.add(%key);
		GreenChannelSim.pushToBack(%key);
		return;
	}
		
}

function PGreenChannelTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PGreenChannelTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PGreenChannelTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey1 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PGreenChannelValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleGreenChannelKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey3 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (GreenChannelKey3 @ %id))
	{
		%key = new SimObject(GreenChannelKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(GreenChannelKey3 @ %id);
		GreenChannelSim.add(%key);
		GreenChannelSim.pushToBack(%key);
		return;
	}
		
}

function PGreenChannelTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PGreenChannelTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey3 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PGreenChannelTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (GreenChannelKey2 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (GreenChannelKey3 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PGreenChannelValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (GreenChannelKey3 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PGreenChannelValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (GreenChannelKey3 @ %id))
	{
		%objId = GreenChannelSim.findObjectByInternalName(GreenChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for BlueChannel
function PBlueChannelTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("BlueChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(BlueChannelKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(BlueChannelKey0 @ %id);
	BlueChannelSim.add(%key);
	%this.setText(%time);
}

function PBlueChannelTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (BlueChannelKey0 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (BlueChannelKey0 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("BlueChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PBlueChannelValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (BlueChannelKey0 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (BlueChannelKey0 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleBlueChannelKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (BlueChannelKey1 @ %id))
	{
		%key = new SimObject(BlueChannelKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(BlueChannelKey1 @ %id);
		BlueChannelSim.add(%key);
		BlueChannelSim.pushToBack(%key);
		return;
	}
		
}

function PBlueChannelTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PBlueChannelTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PBlueChannelValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleBlueChannelKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (BlueChannelKey2 @ %id))
	{
		%key = new SimObject(BlueChannelKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(BlueChannelKey2 @ %id);
		BlueChannelSim.add(%key);
		BlueChannelSim.pushToBack(%key);
		return;
	}
		
}

function PBlueChannelTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PBlueChannelTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PBlueChannelTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey1 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PBlueChannelValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleBlueChannelKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey3 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (BlueChannelKey3 @ %id))
	{
		%key = new SimObject(BlueChannelKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(BlueChannelKey3 @ %id);
		BlueChannelSim.add(%key);
		BlueChannelSim.pushToBack(%key);
		return;
	}
		
}

function PBlueChannelTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PBlueChannelTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey3 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PBlueChannelTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (BlueChannelKey2 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (BlueChannelKey3 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PBlueChannelValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (BlueChannelKey3 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PBlueChannelValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (BlueChannelKey3 @ %id))
	{
		%objId = BlueChannelSim.findObjectByInternalName(BlueChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

//----------------------------------------------------------------
//DataKey for AlphaChannel
function PAlphaChannelTime0::onAdd(%this)
{
	%id = %this.EmitterId;
	%keyId = %this.KeyId;
	DefaultParticleAsset.getEmitter(%id).selectField("AlphaChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%key = new SimObject(AlphaChannelKey0 @ %id);
	%key.EmitterId = %id;
	%key.Time = %time;
	%key.Value = %value;
	%key.Key = 0;
	%key.setInternalName(AlphaChannelKey0 @ %id);
	AlphaChannelSim.add(%key);
	%this.setText(%time);
}

function PAlphaChannelTime0::onReturn(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (AlphaChannelKey0 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelTime0::onLoseFirstResponder(%this)
{
	//This is our first key value and should always
	//have a time of 0
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0 || %value > 0)
		%value = 0;
	if(isObject (AlphaChannelKey0 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey0 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelValue0::onAdd(%this)
{
	%id = %this.EmitterId;
	DefaultParticleAsset.getEmitter(%id).selectField("AlphaChannel");
	%keyCount = DefaultParticleAsset.getEmitter(%id).getDataKeyCount();
	if(%keyCount > 0)
	{
		%keyValue = DefaultParticleAsset.getEmitter(%id).getDataKey(0);
		%time = getWord(%keyValue,0);
		%value = getWord(%keyValue,1);
	}
	DefaultParticleAsset.getEmitter(%id).deselectField();
	%this.setText(%value);
}

function PAlphaChannelValue0::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelKey0 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelValue0::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelKey0 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey0 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleAlphaChannelKey1::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelKey1 @ %id))
	{
		%key = new SimObject(AlphaChannelKey1 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelKey1 @ %id);
		AlphaChannelSim.add(%key);
		AlphaChannelSim.pushToBack(%key);
		return;
	}
		
}

function PAlphaChannelTime1::onAdd(%this)
{
	%id = %this.EmitterId;
	%time = 0.01;
	%this.setText(%time);
}

function PAlphaChannelTime1::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelTime1::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0.01)
		%value = 0.01;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
}	

function PAlphaChannelValue1::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelValue1::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleAlphaChannelKey2::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelKey2 @ %id))
	{
		%key = new SimObject(AlphaChannelKey2 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelKey2 @ %id);
		AlphaChannelSim.add(%key);
		AlphaChannelSim.pushToBack(%key);
		return;
	}
		
}

function PAlphaChannelTime2::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PAlphaChannelTime2::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PAlphaChannelTime2::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey1 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey1 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PAlphaChannelValue2::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelValue2::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function ParticleAlphaChannelKey3::onClick(%this)
{
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey3 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey3 @ %id);
		%objId.delete();
		return;
	}
	else if(!isObject (AlphaChannelKey3 @ %id))
	{
		%key = new SimObject(AlphaChannelKey3 @ %id);
		%key.EmitterId = %id;
		%key.Time = 0.01;
		%key.Value = 0.0;
		%key.Key = 1;
		%key.setInternalName(AlphaChannelKey3 @ %id);
		AlphaChannelSim.add(%key);
		AlphaChannelSim.pushToBack(%key);
		return;
	}
		
}

function PAlphaChannelTime3::onAdd(%this)
{
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%this.setText(%time);
}

function PAlphaChannelTime3::onReturn(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey3 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}

function PAlphaChannelTime3::onLoseFirstResponder(%this)
{
	//This is our second key value and should always
	//have a time more than the first key value
	//otherwise it will overwrite it. 
	%id = %this.EmitterId;
	if(isObject (AlphaChannelKey2 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey2 @ %id);
		%time = %objId.Time;
	}
	%value = %this.getText();
	if(%value < %time)
		%value = %time;
	if(%value > 1)
		%value = 1;
	if(isObject (AlphaChannelKey3 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey3 @ %id);
		%objId.Time = %value;
	}
	EditorToy.updateDataKey();
	%this.setText(%value);
}	

function PAlphaChannelValue3::onReturn(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	if(isObject (AlphaChannelKey3 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}

function PAlphaChannelValue3::onLoseFirstResponder(%this)
{
	%id = %this.EmitterId;
	%value = %this.getText();
	if(%value < 0)
		%value = 0;
	//Set first datakey to the value
	if(isObject (AlphaChannelKey3 @ %id))
	{
		%objId = AlphaChannelSim.findObjectByInternalName(AlphaChannelKey3 @ %id);
		%objId.Value = %value;
	}
	EditorToy.updateDataKey();
}
