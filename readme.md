# ShadersWpf

This small project demonstrates various ways of playing with `ShaderEffect` in WPF. Below, listed 3 essential classes:

## `BaseShaderEffect`

This is a base class that should be inherited while adding a shader.

> It's mandatory to have a variable(input) called `input` in the shader code

## `AnimatableShaderEffect`

This class is derived from `BaseShaderEffect` which provides ability to a shader being animated using `Time` property. Besides that it contains two abstract properties which should be implemented in each child class and one virtual property which can be overriden if wanted:

- `AnimDuration`: Determines the duration of the animation before it winds back. **Should be overriden**
- `EndPoint`: Determines the final value for `time`. **Should be overriden**
  > As this is a property, you can do fancy calculations as well
- `StartPoint`: Determines the inital value for `time`. By default, it has the value of 0 but _can be overriden_.

> It's mandatory to have a variable(input) called `time` in the shader code

## `ShaderAnimationController`

This is the main class for animating shaders. It has 2 methods:

- `SetTarget(AnimatableShaderEffect targetEffect)`: Updates/Sets the shader which is going to be animated
- `AnimateTarget()`: Animates the shader using its properties such as `AnimDuration` and etc.

# Examples

The project is shipped with some examples. Below you can see both `.hlsl` codes & results.

## `Grayscale`

A basic shader that turns an image or a color to gray version.

### Codes

#### Shader code

```hlsl
sampler2D input : register(s0);
float grayscaleAmount : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    float gray = dot(color.rgb, float3(0.3, 0.59, 0.11));
    float3 grayscale = float3(gray, gray, gray);
    return float4(lerp(color.rgb, grayscale, grayscaleAmount), color.a);
}
```

#### C# class

```cs
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
```

### Result

![Result](https://gist.github.com/user-attachments/assets/092a495e-962f-47b1-9964-d87fdc02c209)
