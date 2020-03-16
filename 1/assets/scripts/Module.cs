//-----------------------------------------------
//MODULE CREATE
function EditorToy::createModuleMenu(%this)
{
	SandboxWindow.add(ModuleCreate);
}

function EditorToy::createModule(%this)
{
	ModuleCreate.setVisible(1);
}

//Module Name
function EditorToy::updateModuleName(%this, %value)
{
	%this.moduleName = %value;
}

function ModuleName::update(%this)
{
	%text = EditorToy.moduleName;
	%this.setText(%text);
}

function ModuleName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateModuleName(%value);
}

function ModuleName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateModuleName(%value);
}

//Module description
function EditorToy::updateModuleDesc(%this, %value)
{
	%this.moduleDesc = %value;
}

function ModuleDesc::update(%this)
{
	%text = EditorToy.moduleDesc;
	%this.setText(%text);
}

function ModuleDesc::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateModuleDesc(%value);
}

function ModuleDesc::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateModuleDesc(%value);
}

function EditorToy::finishModule(%this)
{
	//Do this when the button is hit incase the updates
	//were not updated before.
	%mName = ModuleName.getText();
	%mDesc = ModuleDesc.getText();
	
	EditorToy.updateModuleName(%mName);
	EditorToy.updateModuleDesc(%mDesc);
	
	if(%mName $= "")
		return;
	
	if(!isDirectory("^EditorToy/projects/"@ %mName @ "/1/"))
	{
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/images/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/particles/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/scripts/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/gui/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/scenes/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/sounds/");
		createPath("^EditorToy/projects/"@ %mName @ "/1/assets/animations/");
	}
	%this.copyModuleTaml();
}

function EditorToy::copyModuleTaml(%this)
{
	ModuleCreate.setVisible(0);
	%mName = EditorToy.moduleName;
	if(%mName $= "")
		return;
	%defaultLocation = "^EditorToy/projects/"@ %mName @ "/1/";
	echo(%defaultLocation);
	%defaultTaml = "^EditorToy/assets/defaults/empty.taml";
	%toTaml = %defaultLocation @ "module.taml";
	pathCopy(%defaultTaml,%toTaml);
	
	%defaultMain = "^EditorToy/assets/defaults/emptyMain.cs";
	%toMain = %defaultLocation @ "main.cs";
	pathCopy(%defaultMain,%toMain);
	
	%this.writeModuleTaml();
}

function EditorToy::writeModuleTaml(%this)
{
	%mName = EditorToy.moduleName;
	%mDesc = EditorToy.moduleDesc;
	
	//Write Taml File
	%file = new FileObject();
	%file.openForWrite("^EditorToy/projects/" @ %mName @ "/1/module.taml");
	%file.writeLine("<ModuleDefinition");
	%file.writeLine("	ModuleId=\"" @ %mName @ "\"");
	%file.writeLine("	VersionId=\"1\"");
	%file.writeLine("	Description=\"" @ %mDesc @ "\"");
	%file.writeLine("	Type=\"EditorModule\"");
	%file.writeLine("	ScriptFile=\"main.cs\"");
	%file.writeLine("	CreateFunction=\"create\"");
	%file.writeLine("	DestroyFunction=\"destroy\">");
	%file.writeLine("		<DeclaredAssets");
	%file.writeLine("			Path=\"assets\"");
	%file.writeLine("			Extension=\"asset.taml\"");
	%file.writeLine("			Recurse=\"true\"/>");
	%file.writeLine("</ModuleDefinition>");
	%file.close();
	
	%modulesFound = ModuleDatabase.scanModules("^EditorToy/projects/");
	//Re scan for modules to build our list again for loading new
	//modules that have just been created.
	scanForModules();
	EditorToy.hideObjMenus();
	EditorToy.activateSceneBttn();
}
//-----------------------------------------------
//MODULE LOAD

function EditorToy::createModuleLoadMenu(%this)
{
	SandboxWindow.add(ModuleLoad);
}

function EditorToy::loadModuleMenu(%this)
{
	ModuleLoad.setVisible(1);
}

function ModuleList::onAdd(%this)
{
	%moduleCount = EditorModules.getCount();
	for(%i = 0; %i < %moduleCount; %i++)
	{
		%moduleDefinition = EditorModules.getObject(%i);
		%moduleTitle = %moduleDefinition.moduleId;
		%this.add(%moduleTitle SPC %moduleDefinition, %i);
	}
	
}

function EditorToy::updateSelectedModule(%this, %value)
{
	%this.selectedModule = %value;
}

function ModuleList::onReturn(%this)
{
	%value = %this.getText();
	echo(%value);
	
	EditorToy.updateSelectedModule(%value);
}

function EditorToy::loadModule(%this)
{
	SandboxScene.clear();
	//%module = ModuleList.getText();
	%module = EditorToy.selectedModule;
	%moduleName = getWord(%module, 0);
	%moduleDef = getWord(%module, 1);
	EditorToy.moduleName = %moduleName;
	AssetDatabase.addModuleDeclaredAssets(%moduleDef);
	ModuleLoad.setVisible(0);
	%this.loadModulePref();
	EditorToy.activateSceneBttn();
}

function EditorToy::saveModule(%this)
{
	%mName = EditorToy.moduleName;
	$pref::Module::Scene = EditorToy.sceneName;
	export("$pref::Module::*","^EditorToy/projects/"@ %mName @ "/1/" @ %mName @ ".prefs.cs");
}

function EditorToy::loadModulePref(%this)
{
	%mName = EditorToy.moduleName;
	exec("^EditorToy/projects/"@ %mName @ "/1/" @ %mName @ ".prefs.cs");
	%autoScene = $pref::Module::Scene;
	if(%autoScene !$= "")
	{
		EditorToy.autoLoadScene(%autoScene);
	}
}