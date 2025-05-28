using System.Windows;
using System.Windows.Media.Animation;
using ShadersWpf.Models.Core;

namespace ShadersWpf.Controllers;

public class ShaderAnimationController : DependencyObject
{
	private Storyboard? _sb;

	public static readonly DependencyProperty TimeProperty =
			DependencyProperty.Register(
					nameof(Time), typeof(double), typeof(ShaderAnimationController),
					new PropertyMetadata(0.0, OnTimeChanged));

	public double Time
	{
		get => (double)GetValue(TimeProperty);
		set => SetValue(TimeProperty, value);
	}

	public AnimatableShaderEffect? TargetEffect { get; private set; }

	public void SetTarget(AnimatableShaderEffect targetEffect)
		=> TargetEffect = targetEffect;

	public void AnimateTarget()
	{
		if (TargetEffect is null)
			return;

		_sb?.Stop();
		_sb = new();

		var anim = new DoubleAnimation
		{
				From = TargetEffect.StartPoint,
				To = TargetEffect.EndPoint,
				Duration = TargetEffect.AnimDuration,
				RepeatBehavior = RepeatBehavior.Forever
		};

		Storyboard.SetTarget(anim, this);
		Storyboard.SetTargetProperty(anim, new PropertyPath(TimeProperty));
		_sb.Children.Add(anim);
		_sb.Begin();
	}

	private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ShaderAnimationController { TargetEffect: not null } ctrl)
		{
			ctrl.TargetEffect.Time = (double)e.NewValue;
		}
	}
}
