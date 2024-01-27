

Shader"Custom/CartoonPostProcessing"
{
    Properties
    {
           _MainTex("Base (RGB)", 2D) = "white" {}
        _Threshold("Threshold", Range(0, 1)) = 0.5
        _Smoothness("Smoothness", Range(0, 1)) = 0.1
        _Saturation("Saturation", Range(0, 2)) = 1.0
        _Blend("Blend", Range(0, 1)) = 0.5
        _PixelationAmount("Pixelation Amount", Range(1, 100)) = 1
        _ChromaticAberration("// Chromatic Aberration", Range(0, 0.1)) = 0.05
        _VignetteIntensity("Vignette Intensity", Range(0, 1)) = 0.5
        _BloomIntensity("Bloom Intensity", Range(0, 1)) = 0.5
        _BloomThreshold("Bloom Threshold", Range(0, 1)) = 0.5
        _NoiseIntensity("Noise Intensity", Range(0, 1)) = 0.5
        _FogIntensity("Fog Intensity", Range(0, 1)) = 0.5
        _DesaturationAmount("Desaturation Amount", Range(0, 1)) = 0.5
        _DarkeningAmount("Darkening Amount", Range(0, 1)) = 0.5
        _StarfieldIntensity("Starfield Intnsity", Range(0, 1)) = 0.5
        _WindLinesIntensity("// Wind Lines Intensity", Range(0, 1)) = 0.5
        _ScanLineFrequency("Scan Line Frequency", Range(1, 100)) = 50
        _FogColor("Fog Color", Color) = (0.5, 0.5, 0.7, 1)
        _EdgeDetectionIntensity("// Edge Detection Intensity", Range(0, 1)) = 1
        _ShakeIntensity("Shake Intensity", Range(0, 0.1)) = 0.05
        _VignetteSoftness("Vignette Softness", Range(0, 1)) = 0.5
        _ShakeSpeed("Shake Speed", Range(0, 50)) = 10.0
        _BloodyIntensity("Bloody Intensity", Range(0, 1)) = 0.5
        _GlitchIntensity("Glitch Intensity", Range(0, 2)) = 1
        _ApplyReducedGlitch("Apply Reduced Glitch", Range(0, 1)) = 0
        _MotionBlurIntensity("Motion Blur Intensity", Range(0, 1)) = 0.5
        _GhostingIntensity("Ghosting Intensity", Range(0, 1)) = 0.5
        _DynamicFOVFactor("// Dynamic FOV Factor", Range(0, 2)) = 1.0
        _NebulaOverlay("// Nebula Overlay", 2D) = "white" {}
        _NebulaOverlayIntensity("Nebula Overlay Intensity", Range(0, 1)) = 0.5
        _BloodyUVOffset("Bloody UV Offset", Vector) = (0, 0, 0, 0)
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _BloodyFadeAmount("Bloody Fade Amount", Range(0, 1)) = 0.5
        _BloodyColor("Bloody Color", Color) = (1, 0, 0, 1)
      
    }
  SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};
            
         
sampler2D _MainTex;
sampler2D _NoiseTex;
sampler2D _NebulaOverlay;
float _NebulaOverlayIntensity;
float _PixelationAmount;
float _FogIntensity;
half3 _FogColor;
float _DesaturationAmount;
float _DarkeningAmount;
float _StarfieldIntensity;
float _ScanLineFrequency;
float _WindLinesIntensity;
float _BloomIntensity;
float _ChromaticAberration;
float _VignetteIntensity;
float _NoiseIntensity;
float _BloodyIntensity;
half4 _BloodyColor;
float _BloodyFadeAmount;
half2 _BloodyUVOffset;
float _GlitchIntensity;
float _ApplyReducedGlitch;
float _ShakeIntensity;
float _ShakeSpeed;
float _MotionBlurIntensity;
float _GhostingIntensity;
float _DynamicFOVFactor;
float _BloomThreshold;
float _EdgeDetectionIntensity;
float _VignetteSoftness;
            
            // // Function // to // convert // RGB // to grayscale
float grayscale(float3 color)
{
    return dot(color, float3(0.3, 0.59, 0.11));
}


v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}



     
       
            // // Enhanced // Pixelation Function
half4 ApplyPixelation(half4 col, float2 uv)
{
    float2 resolution = _ScreenParams.xy;
    float2 pixelatedUV = floor(uv * resolution * _PixelationAmount) / (resolution * _PixelationAmount);
    return tex2D(_MainTex, pixelatedUV);
}

half4 ApplyChromaticAberration(half4 col, float2 uv)
{
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float caOffset = _ChromaticAberration * distToCenter * 2.5; // // Amplify // the offset
    
    half4 colR = tex2D(_MainTex, uv + float2(caOffset, 0));
    half4 colG = tex2D(_MainTex, uv);
    half4 colB = tex2D(_MainTex, uv - float2(caOffset, 0));
    
    return half4(colR.r, colG.g, colB.b, 1.0);
}


            // // Procedural // noise function
float random(float2 st)
{
    return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
}
            
half4 ApplyFog(half4 col, float2 uv)
{
    float fogDensity = (1.0 - uv.y) * random(uv * 50.0) * 0.7; // // Increased // the // fog intensity
    col.rgb = lerp(half3(col.rgb), _FogColor.rgb, half(_FogIntensity * fogDensity));
    return col;
}



float starfield(float2 uv)
{
    float starIntensity = random(floor(uv * 500.0)) > 0.995 ? random(uv * 100.0) : 0.0; // // Intensity // is // now random
    float starSize = lerp(0.015, 0.005, starIntensity); // // Larger // stars // are // closer // and // have // higher intensity

    float starMovement = _Time.y * 0.1; // // Adjust // speed // as needed
    
    uv.y += starMovement; // // Simulate // stars // moving // towards // the viewer
    uv.x += sin(uv.y * 20.0) * 0.01; // // Slight // horizontal // movement // for dynamism

    // // Check // if // the // star // is // out // of // bounds // after moving
    if (uv.y > 1.0 || uv.y < 0.0)
    {
        starIntensity = 0.0;
    }

    return starIntensity * (random(uv * 50.0) + 0.5);
}

          
half4 ApplyStarfield(half4 col, float2 uv)
{
    float starIntensity = starfield(uv);
    col.rgb += starIntensity * _StarfieldIntensity;
    return col;
}
half4 ApplyNoise(half4 col, float2 uv)
{
    float3 colorNoise = float3(random(uv + _Time.y), random(uv - _Time.y), random(uv));
    col.rgb = lerp(col.rgb, col.rgb + colorNoise * 0.1, _NoiseIntensity); // // Amplified // noise factor
    return col;
}


         

            // // Function // for // Desaturation Effect
half4 ApplyDesaturation(half4 col)
{
    half3 greyScale = dot(col.rgb, half3(0.299, 0.587, 0.114));
    col.rgb = lerp(col.rgb, greyScale, _DesaturationAmount);
    return col;
}

            // // Function // for // Darkening Effect
half4 ApplyDarkening(half4 col)
{
    col.rgb *= 1.0 - _DarkeningAmount;
    return col;
}

        

         
            // // Enhanced // Screen // Shake effect
half4 ApplyScreenShake(half4 col, float2 uv)
{
    float shakeAmount = _ShakeIntensity * (sin(_Time.y * _ShakeSpeed) + random(float2(-1, 1))) * 0.5;
    float distToCenter = distance(uv, float2(0.5, 0.5));
    shakeAmount *= lerp(1.0, 2.0, distToCenter); // // More // shake // towards // the edges
    uv += float2(shakeAmount, shakeAmount);
    return tex2D(_MainTex, uv);
}

half4 ApplyAdvancedRadialBlur(half4 col, float2 uv)
{
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float blurAmount = _ShakeIntensity * distToCenter * 0.5;
    float2 dir = normalize(uv - float2(0.5, 0.5));
    uv += dir * blurAmount;
    return tex2D(_MainTex, uv);
}


   // // Gaussian // Blur Function (3// x3 Kernel)
            // // Enhanced // // Blur function
        // // Enhanced Blur function
half4 ApplyBlur(half4 col, float2 uv)
{
    float2 resolution = _ScreenParams.xy;
    float2 texelSize = 1.0 / resolution;
    half3x3 kernel = half3x3(1, 2, 1, 2, 4, 2, 1, 2, 1) / 16;
    
    half3 blurredColor = col.rgb * kernel[1][1];
    for (int x = -1; x <= 1; x++)  // // Adjusted // // loop range
    {
        for (int y = -1; y <= 1; y++)  // // Adjusted loop range
        {
            float2 offset = float2(x, y) * texelSize;
            half3 sample = tex2D(_MainTex, uv + offset).rgb;
            blurredColor += sample * kernel[x + 1][y + 1];
        }
    }

    return half4(blurredColor, 1.0);
}





            // // Enhanced // // // // Bloody effect
   // // Adjusted Bloody effect

// // Enhanced Bloody effect
half4 ApplyBloodyEffect(half4 col, float2 uv)
{
    half2 bloodyUV = uv + _BloodyUVOffset;
    half bloody = tex2D(_NoiseTex, bloodyUV).r;
    if (bloody > 0.9)
    {
        col = lerp(col, half4(_BloodyColor.rgb * _BloodyFadeAmount, 1.0), _BloodyIntensity);
    }
    return col;
}






    // // Enhanced // // Glitch effect
half4 ApplyGlitchEffect(half4 col, float2 uv)
{
    float glitchBaseIntensity = _GlitchIntensity * random(uv + _Time.y * 5.0); // // Randomized // base intensity

    // // Adjust // glitch intensity // based // on // _ApplyReducedGlitch flag
    if (_ApplyReducedGlitch > 0.5)
    {
        glitchBaseIntensity /= 2.0;
    }

    float glitchAmountX = glitchBaseIntensity * (random(floor(uv * 250.0 + _Time.y * 20.0)) > 0.8 ? 1.0 : 0.0);
    float glitchAmountY = glitchBaseIntensity * (random(floor(uv * 350.0 - _Time.y * 20.0)) > 0.8 ? 1.0 : 0.0);
    
    // // Amplify // the // glitchAmount // and // vary // its // effect // using // random // values // for // different // parts // of // the screen
    uv.x += glitchAmountX * (random(uv + _Time.y) - 0.5) * random(uv) * 2.0; // // Amplified // and // randomized // direction // // and size
    uv.y += glitchAmountY * (random(uv - _Time.y) - 0.5) * random(uv) * 2.0; // // Amplified // and // randomized // direction and size

    return tex2D(_MainTex, uv);
}



half4 ApplyScanLines(half4 col, float2 uv)
{
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float frequency = lerp(100.0, 50.0, distToCenter);
    float speed = lerp(5.0, 2.0, distToCenter);
    float scanLine = step(0.5, sin((1.0 - distToCenter + _Time.y * speed) * frequency));

    col.rgb *= 1.0 - (1.0 - scanLine) * 5.0; // // Amplify // // the effect
    return col;
}


half4 ApplySpeedLines(half4 col, float2 uv)
{
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float frequency = lerp(50.0, 10.0, distToCenter);
    float speed = lerp(2.0, 10.0, distToCenter);
    float thickness = lerp(0.005, 0.02, distToCenter);
    float movingValue = uv.y + _Time.y * _WindLinesIntensity * speed;
    float sineWave = sin(movingValue * frequency);
    float speedLine = step(0.9 + thickness, sineWave);
    
    col.rgb *= 1.0 - speedLine * _WindLinesIntensity * 5.0; // // Amplify the effect
    return col;
}



half4 ApplyMotionBlur(half4 col, float2 uv)
{
                // // Use // the // previous frame's UVs and lerp based on the Motion Blur Intensity
    float2 previousUV = uv - float2(0.01, 0.01) * _MotionBlurIntensity;
    half4 previousCol = tex2D(_MainTex, previousUV);
    return lerp(col, previousCol, _MotionBlurIntensity);
}

half4 ApplyGhosting(half4 col, float2 uv)
{
                // Use UVs from a couple of frames back to simulate ghosting
    float2 ghostUV = uv - float2(0.02, 0.02) * _GhostingIntensity;
    half4 ghostCol = tex2D(_MainTex, ghostUV);
    return lerp(col, ghostCol, _GhostingIntensity);
}

half4 ApplyDynamicFOV(half4 col, float2 uv)
{
                // Adjust UVs based on the FOV factor
    float2 centeredUV = uv - 0.5;
    centeredUV *= _DynamicFOVFactor;
    centeredUV += 0.5;
    return tex2D(_MainTex, centeredUV);
}

half4 ApplyNebulaOverlay(half4 col, float2 uv)
{
    half4 nebulaColor = tex2D(_NebulaOverlay, uv);
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float nebulaFactor = smoothstep(0.2, 0.5, distToCenter);
    col = lerp(col, nebulaColor, _NebulaOverlayIntensity * nebulaFactor);
    return col;
}



half4 ApplyAdvancedScreenShake(half4 col, float2 uv)
{
    // A factor that ranges from 1 to 0 every second to reduce the shake over time
    float timeFactor = 1.0 - _Time.y % 1.0;

    // Increased shake intensity with multiple sine waves for more erratic behavior
    float shakeAmountX = _ShakeIntensity * 2.0 * (sin(_Time.y * _ShakeSpeed) + sin(_Time.y * _ShakeSpeed * 1.5) + random(float2(-1, 1))) * timeFactor;
    float shakeAmountY = _ShakeIntensity * 2.0 * (cos(_Time.y * _ShakeSpeed) + cos(_Time.y * _ShakeSpeed * 1.3) + random(float2(-1, 1))) * timeFactor;

    // Distance // to the center to adjust the shake towards the edges
    float distToCenter = distance(uv, float2(0.5, 0.5));

    // More shake towards the edges
    shakeAmountX *= lerp(1.5, 3.0, distToCenter);
    shakeAmountY *= lerp(1.5, 3.0, distToCenter);

    // Adjust the UV coordinates
    uv += float2(shakeAmountX, shakeAmountY);

    // // Sample the texture with the shaken UVs
    return tex2D(_MainTex, uv);
}





  


            // Enhanced Vignette Function
half4 ApplyVignette(half4 col, float2 uv)
{
    float distToCenter = distance(uv, float2(0.5, 0.5));
    float vignetteFactor = 1.0 - smoothstep(0.5 - _VignetteSoftness, 0.5 + _VignetteSoftness, distToCenter);
    col.rgb *= 1.0 - vignetteFactor * _VignetteIntensity;
    return col;
}

half4 ApplyBloom(half4 col, float2 uv)  // Added uv as parameter
{
    half4 brightAreas = max(0, col - _BloomThreshold);
    half4 blurredBrightAreas = brightAreas;
    float2 offset = 0.008;
    
    for (int x = -3; x <= 3; x++)
    {
        for (int y = -3; y <= 3; y++)
        {
            blurredBrightAreas += tex2D(_MainTex, uv + float2(x * offset.x, y * offset.y)); // Use the passed uv
        }
    }
    blurredBrightAreas /= 49.0;
    col += blurredBrightAreas * _BloomIntensity;
    return col;
}

fixed4 CannyEdge(float2 uv)
{
    float3 result = float3(0.0, 0.0, 0.0);

    // Sobel filter kernels
    float3x3 Gx = float3x3(-1, 0, 1, -2, 0, 2, -1, 0, 1);
    float3x3 Gy = float3x3(-1, -2, -1, 0, 0, 0, 1, 2, 1);

    // Sample the surrounding pixels
    float texelSize = 1.0 / float2(512, 512); // Assuming a 512 x 512 texture;
    float gx = 0.0;
    float gy = 0.0;
    for (int y = -1; y <= 1; y++)
    {
        for (int x = -1; x <= 1; x++)
        {
            float sample = tex2D(_MainTex, uv + float2(x, y) * texelSize).r;
            gx += Gx[y + 1][x + 1] * sample;
            gy += Gy[y + 1][x + 1] * sample;
        }
    }

    // Compute gradient and angle
    float gradient = sqrt(gx * gx + gy * gy);
    float angle = atan2(gy, gx) * 180.0 / 3.14159;
    if (angle < 0)
        angle += 180;

    float3 originalColor = tex2D(_MainTex, uv).rgb;

    // Random condition to decide the effect
    bool randomCondition = (frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453) > 0.5); // A simple hash function to generate pseudo-random values

    if (randomCondition)
    {
        // If gradient is above threshold, set to black; otherwise keep original color
        result = (gradient > 0.2) ? float3(0.0, 0.0, 0.0) : originalColor;
    }
    else
    {
        // Blend the original color 50% or show only black and white
        result = lerp(originalColor, (gradient > 0.2) ? float3(0.0, 0.0, 0.0) : float3(1.0, 1.0, 1.0), 0.5);
    }

    return fixed4(result, 1.0);
}





            

     

half4 frag(v2f inp) : SV_Target
{
    half4 col = tex2D(_MainTex, inp.uv);
    
    if (_PixelationAmount > 0.01)
        col = ApplyPixelation(col, inp.uv);
        
    if (_FogIntensity > 0.01)
        col = ApplyFog(col, inp.uv);

    if (_DesaturationAmount > 0.01)
        col = ApplyDesaturation(col);

    if (_DarkeningAmount > 0.01)
        col = ApplyDarkening(col);

    if (_StarfieldIntensity > 0.01)
        col = ApplyStarfield(col, inp.uv);

    if (_ScanLineFrequency > 0.01 && _ScanLineFrequency < 1.1)
        col = ApplyScanLines(col, inp.uv);

    if (_WindLinesIntensity > 0.01)
        col = ApplySpeedLines(col, inp.uv);

    if (_BloomIntensity > 0.01)
        col = ApplyBloom(col, inp.uv);

    if (_ChromaticAberration > 0.01)
        col = ApplyChromaticAberration(col, inp.uv);

    if (_VignetteIntensity > 0.01)
        col = ApplyVignette(col, inp.uv);

    if (_NoiseIntensity > 0.01)
        col = ApplyNoise(col, inp.uv);

    half4 brightAreas = max(half4(0, 0, 0, 0), col - half4(_BloomThreshold, _BloomThreshold, _BloomThreshold, 0));

    if (_BloodyIntensity > 0.01)
        col = ApplyBloodyEffect(col, inp.uv);

    if (_GlitchIntensity > 0.01)
        col = ApplyGlitchEffect(col, inp.uv);

    col = ApplyBlur(col, inp.uv);

    if (_ShakeIntensity > 0.01)
    {
        col = ApplyAdvancedScreenShake(col, inp.uv);
        col = ApplyAdvancedRadialBlur(col, inp.uv);
    }

    if (_MotionBlurIntensity > 0.01)
        col = ApplyMotionBlur(col, inp.uv);

    if (_GhostingIntensity > 0.01)
        col = ApplyGhosting(col, inp.uv);

    if (_DynamicFOVFactor > 0.01)
        col = ApplyDynamicFOV(col, inp.uv);

    if (_NebulaOverlayIntensity > 0.01) 
        col = ApplyNebulaOverlay(col, inp.uv);

    if (_EdgeDetectionIntensity > 0.01)
        col = CannyEdge(inp.uv);

    return col;
}

ENDCG

            
            }
    }

}