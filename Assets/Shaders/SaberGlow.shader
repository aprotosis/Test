Shader "Custom/SaberGlow" {
	Properties {
		_Color ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_GlowColor("Glow Color", Color) = (1,1,1,1)
		_GlowPower("Glow Power", Range(1.0, 6.0)) = 1.0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float4 color : COLOR;
			float2 uv_MainTex;
			float3 viewDir;
		};

		sampler2D _MainTex;
		fixed4 _Color;
		float4 _GlowColor;
		float4 _GlowPower;
		float3 upVector = { 0, 1, 0 };

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			IN.color = _Color;
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * IN.color;
			half glow = 1.0 - saturate(dot(normalize(IN.viewDir), upVector));
			o.Emission = _GlowColor.rgb * pow(glow, _GlowPower);
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
