#version 140

// fog_of_war_resolve.fs

#ifdef GL_ES
precision mediump float;
#endif

uniform sampler2D GBufferDepth;
uniform vec4 GBufferDepth_size;
uniform vec4 GBufferDepth_range;

uniform sampler2D ldf_result;

uniform vec4 light_diffuse;
uniform mat4 light_transform;

in vec3 v_Forward;

out vec4 out_FragColor;

void main() 
{
    vec2 screenCoord = gl_FragCoord.xy * GBufferDepth_size.zw;

    // Figure out the world position
    float depth = texture(GBufferDepth, screenCoord).x * GBufferDepth_range.y + GBufferDepth_range.x;
    vec3 fragPos = depth * normalize(v_Forward);

    // Put in light space
    vec4 rawLightPos = light_transform * vec4(fragPos, 1.0);

    // cull
    if (any(greaterThan(abs(rawLightPos.xyz), rawLightPos.www)))
        discard;

    // Read from the distance field results
    float distance = texture(ldf_result, screenCoord).x;


    float a = (1.0 - min(distance, 1.0)) * light_diffuse.a;

    if (distance >= 0.6) discard;
    vec3 color = vec3(0.0078, 0.0627, 0.8588);

    if (distance <= 0.5) color = vec3(0, 0, 0);
    else  a = 1.5;
  
    out_FragColor = vec4(color.xyz * (1.5 - rawLightPos.z), a / 1.5);
}
