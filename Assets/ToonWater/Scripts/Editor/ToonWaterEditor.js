//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com																									

@CustomEditor (ToonWater)

class ToonWaterEditor extends Editor {
	var defaultInspector:boolean;
    function OnInspectorGUI () {
    	var tex:Texture = Resources.Load("ToonWaterTitle");
		GUILayout.Label(tex);
    	if(GUILayout.Button("Toggle Editor")) {
		 	defaultInspector =!defaultInspector;
		}
    	if(defaultInspector){
   			DrawDefaultInspector ();
   		}else{

   		target.ripplePS = EditorGUILayout.ObjectField("Ripple Particle System", target.ripplePS, GameObject,false);
    	target.splashPS = EditorGUILayout.ObjectField("Splash Particle System", target.splashPS, GameObject,false);
    	target.tileMaterial1 = EditorGUILayout.Slider("Material 1 Tile", target.tileMaterial1, 1,100);
    	
    	var renderer = target.gameObject.GetComponent(Renderer);
    	
    	if(renderer.sharedMaterials.Length == 2){												
			target.tileMaterial2 = EditorGUILayout.Slider("Material 2 Tile", target.tileMaterial2, 1,100);		
		}
		
    	
    	EditorGUILayout.Space();
   		EditorGUILayout.LabelField("Automaticly Add Floating Script To Objects that fall in the water");  
		
		target.autoAddFloat = EditorGUILayout.Toggle("Auto Float" , target.autoAddFloat);	
    	
    	EditorGUILayout.Space();
   		EditorGUILayout.LabelField("How much water flow force applied to floating objects");   
    	target.currentMultiplier = EditorGUILayout.Slider("Flow Multiplier", target.currentMultiplier, 0,1);

    	EditorGUILayout.Space();
   		EditorGUILayout.LabelField("Max flow speed (randomized)");   
    	target.maxCurrent = EditorGUILayout.Slider("Flow Speed", target.maxCurrent, 0,25);
    	
    	EditorGUILayout.Space();
   		EditorGUILayout.LabelField("Seconds to wait before changing flow direction");   
    	target._randomizeCurrent = EditorGUILayout.Slider("Flow Timer", target._randomizeCurrent, 0,60);
    	
    	EditorGUILayout.Space();
    	target.wave = EditorGUILayout.Toggle("Enable Waves" , target.wave);
    	
    	if(target.wave){
    		EditorGUILayout.Space();
   			EditorGUILayout.LabelField("Height movement of waves bobing up and down");   
    		target.waveForceHeight = EditorGUILayout.Slider("Wave Height", target.waveForceHeight, 0,5);
    	
    		EditorGUILayout.Space();
   			EditorGUILayout.LabelField("Height scaling of waves bobing up and down");   
    		target.waveScale = EditorGUILayout.Slider("Wave Scale Multiplier", target.waveScale, 0,1);
    	
    		EditorGUILayout.Space();
   			EditorGUILayout.LabelField("Speed of waves bobing up and down");   
    		target.waveForceSpeed = EditorGUILayout.Slider("Wave Speed", target.waveForceSpeed, 0.1 ,10);
    	}
   		
   		}
        if (GUI.changed)
            EditorUtility.SetDirty (target);
    }
}