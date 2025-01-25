shader_type canvas_item;

uniform float radius : hint_range(0.0,1.5) = 1.5; // The size of the iris
uniform vec2 center = vec2(0.5,0.5); // Center of the screen

void fragment() {
	vec2 uv = UV - center;
    float dist = length(uv);
    
    if (dist < radius) {
        discard; // Reveal the game inside the iris
    } else {
        COLOR = vec4(0.0,0.0,0.0,1.0); // Black mask covering everthing else
    }
}
