#version 140

// prelight_pa_unit_dead.fs

#include "prelight_include.fs"

uniform vec4 GBufferDepth_range;

uniform vec3 TeamColor_Primary;
uniform vec3 TeamColor_Secondary;

uniform vec4 BuildInfo; // vec4(frac,age,radius,height)

uniform sampler2D DiffuseTexture;
uniform sampler2D MaskTexture;
uniform sampler2D GrimeTexture;

uniform float minAmbient; // Set to 1.0 for maximum ambient lighting

in vec2 v_TexCoord;
in vec3 v_Forward;
in vec3 v_Normal;

out vec4 out_FragData[4];

void main()
{
    vec2 tc = v_TexCoord;

    vec4 diffuse_raw = texture(DiffuseTexture, tc);
    vec4 mask = texture(MaskTexture, tc);

    float wire_width = BuildInfo.y;
    vec2 noise_tc = tc * wire_width / 24.0;
    noise_tc += vec2(BuildInfo.x * 0.0121, BuildInfo.x * 0.0749);

    vec3 noise = texture(GrimeTexture, noise_tc).rgb;
    vec3 viewNormal = normalize(v_Normal);

    float specularMask = 1.0;
    float specularExp = 1.0;
    float emissive = mask.b;

    vec3 weight = vec3(0.299, 0.587, 0.114);
    float lum = dot(diffuse_raw.rgb, weight);

    float base = lum * (1.0 - emissive);

    vec3 diffuse = clamp(2.0 * base, 1.0, 1.0) * (1.0 - 2.0 * (1.0 - clamp(base, 0.5, 1.0)) * (1.0 - noise));

    vec3 ambientColor = calcAmbient(viewNormal, v_Forward);
    vec3 ambient = mix(ambientColor * diffuse.rgb, diffuse.rgb, minAmbient);

    out_FragData[0] = vec4(ambient, 1.0);
    out_FragData[1] = vec4(diffuse.rgb, specularMask);
    out_FragData[2] = vec4(length(v_Forward) * GBufferDepth_range.z - GBufferDepth_range.w, 1.0, 1.0, 1.0);
    out_FragData[3] = vec4(encodeViewNormal(viewNormal), encodeSpecularExp(specularExp));
}
