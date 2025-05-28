using System;
using ShadersWpf.Helpers;
using ShadersWpf.Models.Core;

namespace ShadersWpf.CustomEffects;

public class InvertEffect : BaseShaderEffect
{
	public InvertEffect() : base(ResourcesHelper.GetShaderUri("{0}invert.ps"))
	{
	}
}
