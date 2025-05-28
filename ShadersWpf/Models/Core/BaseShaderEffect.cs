using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ShadersWpf.Models.Core;

public abstract class BaseShaderEffect : ShaderEffect
{
	protected BaseShaderEffect(Uri shaderUri)
	{
		PixelShader shader = new() { UriSource = shaderUri };
		PixelShader = shader;

		UpdateShaderValue(InputProperty);
	}

	public static readonly DependencyProperty InputProperty =
			RegisterPixelShaderSamplerProperty("Input", typeof(BaseShaderEffect), 0);

	public Brush Input
	{
		get => (Brush)GetValue(InputProperty);
		set => SetValue(InputProperty, value);
	}
}
