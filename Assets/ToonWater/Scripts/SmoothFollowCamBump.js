var target : Transform;
var distance : float = 3.0;
var height : float = 1.0;
var damping : float = 5.0;
var smoothRotation : boolean = true;
var rotationDamping : float = 10.0;

var targetLookAtOffset : Vector3;     // allows offsetting of camera lookAt, very useful for low bumper heights

var bumperDistanceCheck : float = 2.5;  // length of bumper ray
var bumperCameraHeight : float = 1.0;   // adjust camera height while bumping
var bumperRayOffset : Vector3;    // allows offset of the bumper ray from target origin

// If the target moves, the camera should child the target to allow for smoother movement. DR
function Awake()
{
    //camera.transform.parent = target;
}

function FixedUpdate() {
    
    var wantedPosition = target.TransformPoint(0, height, -distance);
    
    // check to see if there is anything behind the target
    var hit : RaycastHit;
    var back = target.transform.TransformDirection(-1 * Vector3.forward);   
    
    // cast the bumper ray out from rear and check to see if there is anything behind
   // if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, hit, bumperDistanceCheck) 
     //         && hit.transform != target) { // ignore ray-casts that hit the user. DR
        // clamp wanted position to hit position
    //    wantedPosition.x = hit.point.x;
     //   wantedPosition.z = hit.point.z;
       // wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
  //  } 
    
    transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
	
	transform.position.y = height;
    var lookPosition : Vector3 = target.TransformPoint(targetLookAtOffset);
    
    if (smoothRotation) {
        var wantedRotation : Quaternion = Quaternion.LookRotation(lookPosition - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
    } else {
        transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
    }
}