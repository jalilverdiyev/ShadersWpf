using System.Windows;
using ShadersWpf.Helpers;
using ShadersWpf.Models.Core;

namespace ShadersWpf.CustomEffects;

public class GrayscaleShaderEffect : BaseShaderEffect
{
	public GrayscaleShaderEffect() : base(ResourcesHelper.GetShaderUri("{0}grayscale.ps"))
	{
		UpdateShaderValue(GrayscaleAmountProperty);
	}

	public static readonly DependencyProperty GrayscaleAmountProperty =
			DependencyProperty.Register(nameof(GrayscaleAmount), typeof(double), typeof(GrayscaleShaderEffect), new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));

	public double GrayscaleAmount
	{
		get => (double)GetValue(GrayscaleAmountProperty);
		set => SetValue(GrayscaleAmountProperty, value);
	}
}
