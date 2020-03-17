//When creating a scene we need to create a camera
//aswell so the user can modify the camera settings.
//Also scenes need to save automatically after a set
//amount of time.

function EditorToy::resetSceneDefault(%this)
{
	SceneName.setText("");
}

function EditorToy::createSceneMenu(%this)
{
	SandboxWindow.add(SceneCreate);
}

function EditorToy::createScene(%this)
{
	SceneCreate.setVisible(1);
}

//-------------------------------------------------------------------------
//Update Scene settings
function EditorToy::updateSceneName(%this, %value)
{
	%this.sceneName = %value;
}

//-------------------------------------------------------------------------
//Scene creation gui
function SceneName::onAdd(%this)
{
	%text = EditorToy.sceneName;
	%this.setText(%text);
}

function SceneName::onReturn(%this)
{
	%value = %this.getText();
	EditorToy.updateSceneName(%value);
}

function SceneName::onLoseFirstResponder(%this)
{
	%value = %this.getText();
	EditorToy.updateSceneName(%value);
}

function EditorToy::finishScene(%this)
{
	
	%sceneName = SceneName.getText();
	%activeScene = EditorToy.activeScene;
	%this.updateSceneName(%sceneName);
	SceneCreate.setVisible(0);
	EditorToy.hideObjMenus();
	%scene = new Scene(%sceneName);
	if(%activeScene != "")
	{
		%activeScene.clear();
		%activeScene.delete();
	}
	else
	{
		SandboxScene.clear();
	}
	SandboxWindow.setScene(%scene);
	
	EditorToy.activeScene = %scene;
	%gravX = EditorToy.sceneGravityX;
	%gravY = EditorToy.sceneGravityY;
	//Scene Defaults for new Scene
	%scene.setGravity(%gravX , %gravY);
	//%scene.setScenePause(1);
	%scene.setDebugOn("collision");
	EditorToy.activateToolbarBttn();
	%this.resetSceneDefault();
	%this.saveScene();
}

function EditorToy::saveScene(%this)
{
	//Do not save the camera object
	//it should be just a placeholder
	//for the actual camera.
	if(isObject (CameraObject))
	{
		CameraObject.delete();
	}
	%scene = EditorToy.activeScene;
	%sceneName = %scene.getName();
	%mName = EditorToy.moduleName;
	TamlWrite(%scene, "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/scenes/" @ %sceneName @ ".scene.taml" );
	$pref::Scene::CameraX = EditorToy.sceneCameraX;
	$pref::Scene::CameraY = EditorToy.sceneCameraY;
	$pref::Scene::GravityX = EditorToy.sceneGravityX;
	$pref::Scene::GravityY = EditorToy.sceneGravityY;
	$pref::Scene::PositionIterations = EditorToy.scenePosIter;
	$pref::Scene::VelocityIterations = EditorToy.sceneVelIter;
	$pref::Scene::ObjectSizeX = EditorToy.sceneObjectX;
	$pref::Scene::ObjectSizeY = EDitorToy.sceneObjectY;
	export("$pref::Scene::*","^EditorToy/projects/"@ %mName @ "/1/assets/scenes/" @ %sceneName @ ".prefs.cs");
	%this.loadScenePref();
}

function EditorToy::loadScene(%this)
{
	%mName = EditorToy.moduleName;
	%scene = EditorToy.activeScene;
	
	EditorToy.hideObjMenus();
	//save active scene first
	if(%scene != "")
	{
		%this.saveScene();
	}
	%SceneAssetLoad = new OpenFileDialog();
	%SceneAssetLoad.DefaultPath = "modules/EditorToy/1/projects/"@ %mName @ "/1/assets/scenes/";
	%SceneAssetLoad.Title = "Choose Scene...";
	%SceneAssetLoad.MustExist = true;
	%SceneAssetLoad.Filters = "(*scene.taml)|*.scene.taml";
	//we only want to use assets imported into editor
	%SceneAssetLoad.ChangePath = false;
	if(%SceneAssetLoad.Execute())
	{
		Tools.FileDialogs.LastFilePath = "";
		%defaultFile = %SceneAssetLoad.fileName;
		%defaultBase = fileBase(%defaultFile);
		//need to do it twice because scene assets are .scene.taml
		//first fileBase takes it to .scene 
		%sceneBase = fileBase(%defaultBase);
		
		if(%scene != "")
		{
			%scene.clear();
			%scene.delete();
		}
		%this.loadScene = TamlRead("^EditorToy/projects/"@ %mName @ "/1/assets/scenes/"@ %sceneBase @ ".scene.taml");
		EditorToy.sceneName = %sceneBase;
		SandboxWindow.setScene(%this.loadScene);
		EditorToy.activeScene = %this.loadScene;
		%this.loadScene.setDebugOn("collision");
		%SceneAssetLoad.delete();
		EditorToy.activateToolbarBttn();
		//%this.loadScene.setScenePause(1);
	}
	if(isObject(%SceneAssetLoad))
	{
		%SceneAssetLoad.delete();
	}
	%this.loadScenePref();
}

function EditorToy::autoLoadScene(%this, %sceneBase)
{
	%mName = EditorToy.moduleName;
	%scene = EditorToy.activeScene;
	if(%scene != "")
	{
		%scene.clear();
		%scene.delete();
	}
	%this.loadScene = TamlRead("^EditorToy/projects/"@ %mName @ "/1/assets/scenes/"@ %sceneBase @ ".scene.taml");
	SandboxWindow.setScene(%this.loadScene);
	EditorToy.sceneName = %sceneBase;
	EditorToy.activeScene = %this.loadScene;
	%this.loadScene.setDebugOn("collision");
	%this.loadScenePref();
	EditorToy.activateToolbarBttn();
}

function EditorToy::loadScenePref(%this)
{
	%mName = EditorToy.moduleName;
	%sceneName = EditorToy.activeScene.getName();
	exec("^EditorToy/projects/"@ %mName @ "/1/assets/scenes/" @ %sceneName @ ".prefs.cs");
	EditorToy.sceneCameraX = $pref::Scene::CameraX;
	EditorToy.sceneCameraY = $pref::Scene::CameraY;
	EditorToy.sceneGravityX = $pref::Scene::GravityX;
	EditorToy.sceneGravityY = $pref::Scene::GravityY;
	EditorToy.scenePosIter = $pref::Scene::PositionIterations;
	EditorToy.sceneVelIter = $pref::Scene::VelocityIterations;
	EditorToy.sceneObjectX = $pref::Scene::ObjectSizeX;
	EditorToy.sceneObjectY = $pref::Scene::ObjectSizeY;
	
	EditorToy.setSceneWindowCamera();
	
	%this.updateSceneEdit();
}

function EditorToy::playScene(%this)
{
	%scene = %this.activeScene;
	%count = %scene.getCount();
	%list = %scene.getSceneObjectList();
	for(%i = 0; %i < %count; %i++)
	{
		%obj = getWord(%list, %i);
		%class = %obj.getClassName();
		%obj.setActive(1);
	}
}

function EditorToy::pauseScene(%this)
{
	%scene = %this.activeScene;
	%count = %scene.getCount();
	%list = %scene.getSceneObjectList();
	for(%i = 0; %i < %count; %i++)
	{
		%obj = getWord(%list, %i);
		%class = %obj.getClassName();
		%obj.setActive(0);
	}
}

function SceneGravX::update(%this)
{
	%value = EditorToy.sceneGravityX;
	%this.setText(%value);
}

function SceneGravY::update(%this)
{
	%value = EditorToy.sceneGravityY;
	%this.setText(%value);
}

function VelIter::update(%this)
{
	%value = EditorToy.sceneVelIter;
	%this.setText(%value);
}

function PosIter::update(%this)
{
	%value = EditorToy.scenePosIter;
	%this.setText(%value);
}

function ObjectSizeX::update(%this)
{
	%value = EditorToy.sceneObjectX;
	%this.setText(%value);
}

function ObjectSizeY::update(%this)
{
	%value = EditorToy.sceneObjectY;
	%this.setText(%value);
}

function SceneGravX::onReturn(%this)
{
	%value = %this.getText();
	%this.updateSceneGravityX(%value);
}

function SceneGravY::onReturn(%this)
{
	%value = %this.getText();
	%this.updateSceneGravityY(%value);
}

function VelIter::onReturn(%this)
{
	%value = %this.getText();
	%this.updateSceneVelIter(%value);
}

function PosIter::onReturn(%this)
{
	%value = %this.getText();
	%this.updateScenePosIter(%value);
}

function ObjectSizeX::onReturn(%this)
{
	%value = %this.getText();
	%this.updateSceneObjectSizeX(%value);
}

function ObjectSizeY::onReturn(%this)
{
	%value = %this.getText();
	%this.updateSceneObjectSizeY(%value);
}

function EditorToy::updateSceneGravityX(%this,%value)
{
	%this.sceneGravityX = %value;
}

function EditorToy::updateSceneGravityY(%this,%value)
{
	%this.sceneGravityY = %value;
}

function EditorToy::updateSceneVelIter(%this, %value)
{
	%this.sceneVelIter = %value;
}

function EditorToy::updateScenePosIter(%this, %value)
{
	%this.scenePosIter = %value;
}

function EditorToy::updateSceneObjectSizeX(%this, %value)
{
	%this.sceneObjectX = %value;
}

function EditorToy::updateSceneObjectSizeY(%this, %value)
{
	%this.sceneObjectY = %value;
}

function EditorToy::updateSceneValues(%this)
{
	%scene = EditorToy.activeScene;
	%gravX = SceneGravX.getText();
	%gravY = SceneGravY.getText();
	%velIter = VelIter.getText();
	%posIter = PosIter.getText();
	%objX = ObjectSizeX.getText();
	%objY = ObjectSizeY.getText();
	
	EditorToy.sceneGravityX = %gravX;
	EditorToy.sceneGravityY = %gravY;
	EditorToy.sceneVelIter = %velIter;
	EditorToy.scenePosIter = %posIter;
	EditorToy.sceneObjectX = %objX;
	EditorToy.sceneObjectY = %objY;
	
	%scene.setGravity(%gravX, %gravY);
	%scene.setVelocityIterations(%velIter);
	%scene.setPositionIterations(%posIter);
}

function EditorToy::updateSceneEdit(%this)
{
	SceneGravX.update();
	SceneGravY.update();
	VelIter.update();
	PosIter.update();
	ObjectSizeX.update();
	ObjectSizeY.update();
	
	%this.updateSceneValues();
}