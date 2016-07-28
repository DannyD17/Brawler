//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com																									
static var scrollSpeedX = 0.015;
static var scrollSpeedZ = 0.015;
var tileMaterial1:float =1;
var tileMaterial2:float =1;
static var _splashPS:ParticleSystem;
static var _ripplePS:ParticleSystem;
var splashPS:GameObject;
var ripplePS:GameObject;
var currentMultiplier:float;
var _randomizeCurrent:int;
var maxCurrent:float = 10.1;
private var targetCurrentX:float;
private var targetCurrentY:float;
public var waveForce:float;
var waveForceHeight:float=10;
var waveForceSpeed:float=3;
var autoAddFloat:boolean;
var wave:boolean;
var waveScale:float;

function Start() {
		//Instantiate particles used for splash and ripple effects (Particles should only be instantiated twice)
		if(!_splashPS)
		_splashPS = Instantiate(splashPS).transform.GetComponent(ParticleSystem);
		if(!_ripplePS)
		_ripplePS = Instantiate(ripplePS).transform.GetComponent(ParticleSystem);
		
		
		GetComponent.<Renderer>().sharedMaterials[0].SetTextureScale("_MainTex", Vector2.one * tileMaterial1);
		if(GetComponent.<Renderer>().sharedMaterials.Length==2){
			GetComponent.<Renderer>().sharedMaterials[1].SetTextureScale("_MainTex", Vector2.one * tileMaterial2);		
		}
		if(_randomizeCurrent>0){
			InvokeRepeating("randomizeCurrent", 0, _randomizeCurrent);
		}else{
			scrollSpeedX = scrollSpeedZ = maxCurrent;
		}	
}

function Ripple(go:Transform){
	
	_ripplePS.transform.position = go.position;
	_ripplePS.transform.position.y = transform.position.y;
	_ripplePS.transform.localScale = go.GetComponent.<Collider>().bounds.size;
	
	_ripplePS.Emit(1);

}

function Splash(go:Transform) {
	_splashPS.transform.position = go.position;
	_splashPS.transform.localScale = go.GetComponent.<Collider>().bounds.size;
	
	
	_splashPS.Play();
	

}

function OnDrawGizmosSelected() {	//Updates materials in editor
		GetComponent.<Renderer>().sharedMaterials[0].SetTextureScale("_MainTex", Vector2.one * tileMaterial1);
		if(GetComponent.<Renderer>().sharedMaterials.Length==2){
			GetComponent.<Renderer>().sharedMaterials[1].SetTextureScale("_MainTex", Vector2.one * tileMaterial2);		
		}	
}

function FixedUpdate () {
	if(_randomizeCurrent>0){
		scrollSpeedX = Mathf.Lerp(scrollSpeedX, targetCurrentX, Time.deltaTime*.2);
  		scrollSpeedZ = Mathf.Lerp(scrollSpeedZ, targetCurrentY, Time.deltaTime*.2);
    }
	GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.x += scrollSpeedX*.1*Time.deltaTime;
	GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.x = GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.x%1;
    GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.y += scrollSpeedZ*.1*Time.deltaTime;
    GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.y = GetComponent.<Renderer>().sharedMaterials[0].mainTextureOffset.y%1;
    if(GetComponent.<Renderer>().sharedMaterials.Length==2){
   	 	GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.x += scrollSpeedX*.07*Time.deltaTime;
    	GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.x = GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.x%1;
    	GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.y += scrollSpeedZ*.07*Time.deltaTime;
    	GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.y = GetComponent.<Renderer>().sharedMaterials[1].mainTextureOffset.y%1;
    }
	if(wave){
		waveForce = Mathf.Sin(Time.time*waveForceSpeed)*waveForceHeight*.01;
		transform.position.y+=waveForce;
		transform.localScale.y += waveForce*transform.localScale.y;
	}
	
}

function randomizeCurrent(){
	targetCurrentX=Random.Range(maxCurrent*-1, maxCurrent);
	targetCurrentY=Random.Range(maxCurrent*-1, maxCurrent);
}

function OnTriggerEnter (collision : Collider) {						//executes when a object hits the water
	var fo:ToonWaterFloatObject;
	if(collision.transform.GetComponent("ToonWaterFloatObject") ==null){
		if(autoAddFloat){
			fo = collision.gameObject.AddComponent.<ToonWaterFloatObject>();
			initFloatObject(fo);
			fo.enterWater();
		}
	}else  {	
			fo = collision.gameObject.GetComponent("ToonWaterFloatObject");
			initFloatObject(fo);
			fo.enterWater();
	}
}

function initFloatObject(fo:ToonWaterFloatObject){
		fo.GetComponent.<Rigidbody>().drag =(Random.value*.5+.3);		//Adds drag to simulate movement restrictions in water
		if(!fo.initialized){
			fo.initialized = true;
			if(ripplePS)
			fo.ripplePS = ripplePS.transform.GetComponent.<ParticleSystem>();
			if(splashPS)
			fo.splashPS = splashPS.transform.GetComponent.<ParticleSystem>();
			fo.water = this;
			fo.enabled = true;
		}
}

function OnTriggerExit (collision : Collider) {
	var fo:ToonWaterFloatObject = collision.gameObject.GetComponent("ToonWaterFloatObject");
		if(collision.transform.GetComponent("ToonWaterFloatObject") !=null){
			fo.exitWater();
		}
}