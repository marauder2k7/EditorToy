//When creating a scene we need to create a camera
//aswell so the user can modify the camera settings.
//Also scenes need to save automatically after a set
//amount of time.

function EditorToy::resetSceneDefault(%this)
{
	%this.sceneName = "";
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

function SceneName::update(%this)
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
	EditorToy.activeScene = %this.loadScene;
	%this.loadScene.setDebugOn("collision");
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
	EditorToy.setSceneWindowCamera();
}