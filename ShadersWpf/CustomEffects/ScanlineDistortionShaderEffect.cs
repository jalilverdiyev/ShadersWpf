using System;
using System.Windows;
using ShadersWpf.Helpers;
using ShadersWpf.Models.Core;

namespace ShadersWpf.CustomEffects;

public class ScanlineDistortionShaderEffect : AnimatableShaderEffect
{
	public ScanlineDistortionShaderEffect() : base(ResourcesHelper.GetShaderUri("{0}scanlineDistortion.ps"))
	{ }

	public override Duration AnimDuration => TimeSpan.FromMinutes(1);
	public override double EndPoint => (Math.PI * 3) / 2;
}
