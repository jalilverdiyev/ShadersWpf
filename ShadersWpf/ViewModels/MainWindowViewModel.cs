using ShadersWpf.CustomEffects;
using ShadersWpf.Models.Core;
using ShadersWpf.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace ShadersWpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private Storyboard? _sb;
	private readonly EffectAnimationController _animationController = new();

	public Effect? Effect { get; set; }

	public ICommand ChangeEffect
	{
		get => new RelayCommand(res =>
		{
			if (res is not EffectType effectType)
				return;

			_sb?.Stop();
			_sb = new();
			Effect = effectType switch
			{
				EffectType.None => null,
				EffectType.Grayscale => new GrayscaleShaderEffect(),
				EffectType.SepiaTone => new SepiaToneEffect(),
				EffectType.Ripple => new RippleShaderEffect(),
				EffectType.Invert => new InvertEffect(),
				EffectType.ScanlineDistortion => new ScanlineDistortionShaderEffect(),
				_ => throw new ArgumentOutOfRangeException()
			};

			if (Effect is not AnimatableShaderEffect animatableEffect)
				return;

			_animationController.TargetEffect = animatableEffect;
			var anim = new DoubleAnimation
			{
				From = animatableEffect.StartPoint,
				To = animatableEffect.EndPoint,
				Duration = animatableEffect.AnimDuration,
				RepeatBehavior = RepeatBehavior.Forever
			};

			Storyboard.SetTarget(anim, _animationController);
			Storyboard.SetTargetProperty(anim, new PropertyPath(EffectAnimationController.TimeProperty));
			_sb.Children.Add(anim);
			_sb.Begin();
		});
	}
}

public enum EffectType
{
	None,
	Grayscale,
	SepiaTone,
	Invert,
	Ripple,
	ScanlineDistortion
}
