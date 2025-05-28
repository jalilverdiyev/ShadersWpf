using System;
using System.Windows;
using ShadersWpf.Helpers;
using ShadersWpf.Models.Core;

namespace ShadersWpf.CustomEffects;

public class RippleShaderEffect : AnimatableShaderEffect
{
	public RippleShaderEffect() : base( ResourcesHelper.GetShaderUri("{0}ripple.ps"))
	{ }

	public override Duration AnimDuration => TimeSpan.FromSeconds(5);
	public override double EndPoint => Math.PI *10;
}
