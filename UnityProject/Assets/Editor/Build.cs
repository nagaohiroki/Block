using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
public static class Build
{
	[MenuItem("File/Build WebGL")]
	public static void Run()
	{
		var sceneNames = new List<string>();
		for(int i = 0; i < SceneManager.sceneCount; i++)
		{
			sceneNames.Add(SceneManager.GetSceneAt(i).path);
		}
		var option = new BuildPlayerOptions();
		option.scenes = sceneNames.ToArray();
		option.options = BuildOptions.None;
		option.target = BuildTarget.WebGL;
		option.locationPathName = "../docs";
		BuildPipeline.BuildPlayer(option);
	}
}
