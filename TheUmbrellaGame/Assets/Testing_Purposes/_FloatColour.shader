
// For some weird reason it doesn't like _Color.

Shader "unityShaders/testing/_FloatColour" { //this can renamed to whatever I want it to be and it'll appear in unity under that heading
	
	
	Properties { //holds the various options that appear in unity
		_Color ("Color", Color) = (1,1,1,1)
	}
	
	SubShader { //shader that is actually used, can create multiple ones for different renders
		Pass{
			
			CGPROGRAM // uses the CG programming language rather then the shaderlab stuff
				
				//pragmas				
				#pragma vertex vert 
				//where to look for the vertex function
				#pragme fragment frag
				//where to look for the fragment function
				
				//user defined variables
				fixed4 _Color; // defines the color variable as containing 4 floats (x,y,z,w)
				
				//base input structs
				struct vertexInput{
					float4 vertex : POSITION; //assigns the semantics that are used
				};
				struct vertexOutput{
					float4 pos : SV_POSITION; //so unity knows where the vertices are so it can do the calculations accordingly...SV is used for directX so better to include it just to be safe
				};
				
				//vertex function
				vertexOutput vert(vertexInput v){ //writing to the vertexOutput. vertexInput is assigned to v and the result is sent to vertexOutput
					vertexOutput o; //essentially o = vertexOutput
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex); //taking the vertex posiition and multiplying it into the Un ity matrix mvp so it can understand it
			
//					what's going on in the line above

//					UNITY_MATRIX_MVP xyzw
//					v.vertex xyzw
// 					UNITY_MATRIX_MVP1.xyzw * v.vertex.x
// 					UNITY_MATRIX_MVP2.xyzw * v.vertex.y
// 					UNITY_MATRIX_MVP3.xyzw * v.vertex.z
// 					UNITY_MATRIX_MVP4.xyzw * v.vertex.w
//					results in a single float4 matrix vector
 					
					
					return o; //You need to return the output ("o")
				};
				
				//fragment function
				
				float4 frag(vertexOutput i) : COLOR{
					return _Color;
				};
				
			ENDCG //Switches back to shaderlab
		}
	}
	
	//fallback commented out during development
//	Fallback "Diffuse"
}