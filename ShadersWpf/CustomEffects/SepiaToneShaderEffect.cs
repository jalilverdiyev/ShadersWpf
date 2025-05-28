using System;
using ShadersWpf.Helpers;
using ShadersWpf.Models.Core;

namespace ShadersWpf.CustomEffects;

public class SepiaToneEffect : BaseShaderEffect
{
	public SepiaToneEffect() : base(ResourcesHelper.GetShaderUri("{0}sepiaTone.ps"))
	{
	}
}
