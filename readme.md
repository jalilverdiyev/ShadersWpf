# ShadersWpf

This small project demonstrates various ways of playing with `ShaderEffect` in WPF. Below are listed 3 essential classes:

## `BaseShaderEffect`

This is a base class that should be inherited while adding a shader.

> It's mandatory to have a variable(input) called `input` in the shader code

## `AnimatableShaderEffect`

This class is derived from `BaseShaderEffect`, which provides the ability for a shader to be animated using the `Time` property. Besides that, it contains two abstract properties which should be implemented in each child class and one virtual property which can be overridden if wanted:

- `AnimDuration`: Determines the duration of the animation before it winds back. (**Should be overridden**)

- `EndPoint`: Determines the final value for `time`. (**Should be overridden**)
  > As this is a property, you can do fancy calculations also

- `StartPoint`: Determines the initial value for `time`. By default, it has the value of 0 (_can be overridden_).

> It's mandatory to have a variable(input) called `time` in the shader code

## `ShaderAnimationController`

This is the main class for animating shaders. It has 2 methods:

- `SetTarget(AnimatableShaderEffect targetEffect)`: Updates/Sets the shader that is going to be animated

- `AnimateTarget()`: Animates the shader using its properties such as `AnimDuration`, etc.

# Examples

The project is shipped with some examples. Below you can see both `.hlsl` codes, `C#` classes & results.

## `Grayscale`

A basic shader that turns an image or a color into a gray version.

### Shader code

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

### C# class

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
![Grayscale](https://github.com/user-attachments/assets/29f3f693-6116-4f3a-8fb8-58a3bb802d55)

## `Sepia Tone`

A simple shader that transforms the colors into reddish-brown ones.

### Shader code

```hlsl
sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    float3 sepia = float3(
        dot(color.rgb, float3(0.393, 0.769, 0.189)),
        dot(color.rgb, float3(0.349, 0.686, 0.168)),
        dot(color.rgb, float3(0.272, 0.534, 0.131))
    );
    return float4(sepia, color.a);
}
```

### C# class

```cs
public class SepiaToneEffect : BaseShaderEffect
{
	public SepiaToneEffect() : base(ResourcesHelper.GetShaderUri("{0}sepiaTone.ps"))
	{}
}
```

### Result
![SepiaTone](https://github.com/user-attachments/assets/bac6580c-29dd-41e6-8d7d-9642fa665525)

## `Invert`

A simple shader that inverts the colors of the control.

### Shader code

```hlsl
sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    return float4(1.0 - color.rgb, color.a);
}
```

### C# class

```c#
public class InvertEffect : BaseShaderEffect
{
	public InvertEffect() : base(ResourcesHelper.GetShaderUri("{0}invert.ps"))
	{}
}
```

### Result
![Invert](https://github.com/user-attachments/assets/21c5912b-1f23-4d14-af20-5ed85e7099b2)

## `Ripple`

An animatable wavy shader

### Shader code

```hlsl
sampler2D input : register(s0);
float time : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float amplitude = 0.02; // wave strength
    float frequency = 25.0;

    float2 offset = sin((uv.x * frequency + time)) * amplitude * float2(0.0, 1.0);
    float2 displacedUV = uv + offset;

    return tex2D(input, displacedUV);
}
```

### C# class

```c#
public class RippleShaderEffect : AnimatableShaderEffect
{
	public RippleShaderEffect() : base( ResourcesHelper.GetShaderUri("{0}ripple.ps"))
	{ }

	public override Duration AnimDuration => TimeSpan.FromSeconds(5);
	public override double EndPoint => Math.PI *10;
}
```

### Result
![Ripple](https://github.com/user-attachments/assets/e54e3709-59b4-457f-9ef6-3f7430b4cb31)

## `Scanline distortion`

A shader that makes the control look like old TVs.

### Shader code

```hlsl
sampler2D input : register(s0);
float time : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    // Horizontal scanline distortion
    float scanline = sin((uv.y + time * 2.0) * 100.0) * 0.005;

    // Distortion to horizontal coordinate
    uv.x += scanline;

    // Subtle vertical roll
    uv.y += sin(time * 0.5) * 0.01;

    // Flicker
    float flicker = 0.95 + 0.05 * sin(time * 40.0);

    float4 color = tex2D(input, uv) * flicker;
    return color;
}
```

### C# class

```cs
public class ScanlineDistortionShaderEffect : AnimatableShaderEffect
{
	public ScanlineDistortionShaderEffect() : base(ResourcesHelper.GetShaderUri("{0}scanlineDistortion.ps"))
	{ }

	public override Duration AnimDuration => TimeSpan.FromMinutes(1);
	public override double EndPoint => (Math.PI * 3) / 2;
}
```

### Result
![ScanlineDistortion](https://github.com/user-attachments/assets/a728de8e-9816-4cea-af53-ef7d170f11b9)
