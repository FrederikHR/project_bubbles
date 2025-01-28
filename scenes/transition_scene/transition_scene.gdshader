shader_type canvas_item;

uniform float radius : hint_range(0.0,1.5) = 1.5; // The size of the iris
uniform vec2 center = vec2(0.5,0.5); // Center of the screen
uniform vec2 screen_size = vec2(1.0, 1.0); // for aspect ratio calculation

void fragment() {
	vec2 uv = UV - center;
    
    //Calculate aspect ratio
    float aspect_ratio = screen_size.x / screen_size.y;
    
    //adjust UV coords based on the aspect ratio
    uv.x *= aspect_ratio;
    
    float dist = length(uv);
    
    if (dist < radius) {
        discard; // Reveal the game inside the iris
    } else {
        COLOR = vec4(0.0,0.0,0.0,1.0); // Black mask covering everthing else
    }
}
