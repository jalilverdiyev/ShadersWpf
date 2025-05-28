using System;
using System.Windows;

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

public class EffectAnimationController : DependencyObject
{
	public static readonly DependencyProperty TimeProperty =
			DependencyProperty.Register(
					nameof(Time), typeof(double), typeof(EffectAnimationController),
					new PropertyMetadata(0.0, OnTimeChanged));

	public double Time
	{
		get => (double)GetValue(TimeProperty);
		set => SetValue(TimeProperty, value);
	}

	public AnimatableShaderEffect? TargetEffect { get; set; }

	private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is EffectAnimationController { TargetEffect: not null } ctrl)
		{
			ctrl.TargetEffect.Time = (double)e.NewValue;
		}
	}
}
