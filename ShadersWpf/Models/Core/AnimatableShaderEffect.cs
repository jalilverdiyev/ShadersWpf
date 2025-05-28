using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ShadersWpf.Models.Core;

public abstract class AnimatableShaderEffect : BaseShaderEffect
{
	protected AnimatableShaderEffect(Uri shaderUri) : base(shaderUri)
	{
		UpdateShaderValue(TimeProperty);
	}

	public static readonly DependencyProperty TimeProperty =
			DependencyProperty.Register(nameof(Time), typeof(double), typeof(AnimatableShaderEffect),
					new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));

	public double Time
	{
		get => (double)GetValue(TimeProperty);
		set => SetValue(TimeProperty, value);
	}

	public abstract Duration AnimDuration { get; }
	public virtual double StartPoint => 0;
	public abstract double EndPoint { get; }
}
