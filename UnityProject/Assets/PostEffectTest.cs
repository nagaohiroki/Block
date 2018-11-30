using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectTest : MonoBehaviour
{

	[SerializeField]
	Material mMat;
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mMat);
	}
}
