using ShadersWpf.CustomEffects;
using ShadersWpf.Models.Core;
using ShadersWpf.ViewModels.Base;
using System;
using System.Windows.Input;
using System.Windows.Media.Effects;
using ShadersWpf.Controllers;

namespace ShadersWpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private readonly ShaderAnimationController _animationController = new();

	public Effect? Effect { get; set; }

	public ICommand ChangeEffect
	{
		get => new RelayCommand(res =>
		{
			if (res is not ShaderEffectType effectType)
				return;

			Effect = effectType switch
			{
				ShaderEffectType.None => null,
				ShaderEffectType.Grayscale => new GrayscaleShaderEffect(),
				ShaderEffectType.SepiaTone => new SepiaToneEffect(),
				ShaderEffectType.Ripple => new RippleShaderEffect(),
				ShaderEffectType.Invert => new InvertEffect(),
				ShaderEffectType.ScanlineDistortion => new ScanlineDistortionShaderEffect(),
				_ => throw new ArgumentOutOfRangeException()
			};

			if (Effect is not AnimatableShaderEffect animatableEffect)
				return;

			_animationController.SetTarget(animatableEffect);
			_animationController.AnimateTarget();
		});
	}
}

public enum ShaderEffectType
{
	None,
	Grayscale,
	SepiaTone,
	Invert,
	Ripple,
	ScanlineDistortion
}
