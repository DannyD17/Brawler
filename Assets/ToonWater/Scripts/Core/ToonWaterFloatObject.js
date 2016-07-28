//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	
#pragma strict
var _yPosBuffer: float;
var floatHeight: float=.5;
var buoyancyCentreOffset: Vector3;
var bounceDamp: float =.8;
var ripplePS:ParticleSystem;
var splashPS:ParticleSystem;
private var ripple:boolean;
var water:ToonWater;				//Points to the ToonWater script
var waterPosAdjust:float;			//Adjust the Y position in water
var rippleSizeAdjust:float = 1;		//Adjust the size of particle system ripples
public var inWater:boolean;			//If the object is in water this will be toggled true
public var initialized:boolean;
private var rippleCounter:float = .5;

function Start(){
	InvokeRepeating("calcWaterLevel", 0,.5);
	
}

function calcWaterLevel(){
	_yPosBuffer = water.transform.position.y+(GetComponent.<Collider>().bounds.size.y*.25)+buoyancyCentreOffset.z+waterPosAdjust;
}

function Ripple(){
	
	if(inWater && rippleCounter < GetComponent.<Rigidbody>().velocity.magnitude){
		
		rippleCounter = .5;
		water.Ripple(transform);
	
	}

}

function Update(){
	//Countdown to emit a ripple
	rippleCounter -= Time.deltaTime;

}

function FixedUpdate () {
	if(inWater){	//
		var actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
		var forceFactor = 1f - ((actionPoint.y - _yPosBuffer) / floatHeight);
		if (forceFactor > 0f) {
			var uplift = -Physics.gravity * (forceFactor - GetComponent.<Rigidbody>().velocity.y * bounceDamp);
			GetComponent.<Rigidbody>().AddForceAtPosition(uplift*GetComponent.<Rigidbody>().mass, actionPoint);
		}

		
		if(water.currentMultiplier > 0){
			GetComponent.<Rigidbody>().AddForce(water.scrollSpeedX*water.currentMultiplier*GetComponent.<Rigidbody>().mass,0,water.scrollSpeedZ*water.currentMultiplier*GetComponent.<Rigidbody>().mass);
		}
		bob();
		if(transform.position.y > _yPosBuffer + (GetComponent.<Collider>().bounds.size.y*.5)){
			exitWater();
		}
	}else if(!inWater && transform.position.y < _yPosBuffer + (GetComponent.<Collider>().bounds.size.y*.5)){
		enterWater();
	}
}

function bob(){
	GetComponent.<Rigidbody>().AddForce(0,-water.waveForce/(GetComponent.<Rigidbody>().mass+1),0);
}
	
function enterWater() {
	if(!ripple){
		ripple = true;
		InvokeRepeating("Ripple", 0, Random.Range(0.35, 0.3));
	}
	inWater = true;
	water.Splash(transform);
}

function exitWater() {
	inWater = false;
	water.Splash(transform);
}