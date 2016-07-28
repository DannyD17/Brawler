//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com
#pragma strict
var carrier:Transform;
var yOffset:float;
var xOffset:float;
static var followers:Array;
var following:boolean;
static var player:Transform;
private var resetPosTimer:float;
var randomForce:boolean;
private var resetPosCounter:float;

function Start () {
	if(followers ==null){
		followers = new Array();
	}
	player = GameObject.FindGameObjectWithTag("Player").transform;
	if(player == null){
	this.enabled = false;
	Debug.Log("no gameobject tagged Player, disabling waterball script");
	}	
	if(randomForce){
		resetPosTimer = Random.Range(2, 10);
	}
}

function OnCollisionEnter (collision:Collision) {
	if(collision.gameObject.tag == "Player" && !following){
		if(followers.length == 0){
		carrier = collision.gameObject.transform;
		}else{
		carrier = followers[followers.length - 1] as Transform;
		}
		following = true;
		followers.push(transform);
		yield(WaitForSeconds(1));
		Physics.IgnoreCollision(GetComponent.<Collider>(), collision.collider, false);	
	}
}

function LateUpdate () {

		if(player && !carrier && Vector3.Distance(player.transform.position, transform.position) < GetComponent.<Collider>().bounds.size.magnitude && !following){
			if(followers.length == 0){
			carrier = player;
		}else{
			carrier = followers[followers.length - 1] as Transform;
		}
			following = true;
			followers.push(transform);		
		}
		
		if(resetPosCounter > resetPosTimer){
			resetPosCounter = 0;
			resetPosTimer = Random.Range(2, 10);
			transform.GetComponent.<Rigidbody>().AddForce(Random.Range(-50,50) , Random.value * 75 + 25 ,Random.Range(-50,50));
			transform.GetComponent.<Rigidbody>().AddTorque(Random.Range(-50,50) , Random.Range(-50,50) ,Random.Range(-50,50));
		}else if(resetPosTimer > 0){
			resetPosCounter+=Time.deltaTime;
		}
		
}

function FixedUpdate () {
	if(carrier){	
		var yDif = carrier.position.z -transform.position.z+yOffset;
		transform.GetComponent.<Rigidbody>().velocity.z = yDif*50*Time.deltaTime;
		var xDif = carrier.position.x -transform.position.x+xOffset;
		transform.GetComponent.<Rigidbody>().velocity.x = xDif*50*Time.deltaTime;
	}
}