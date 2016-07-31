using UnityEngine;

public class MultiplayerCamera : MonoBehaviour
{

	public float _distance_to_target = 15f; //cam distance from ave player position
	public float _follow_speed = 3f; //Cam lerp speed
	private Vector3 _average; //ave player position
	private float furthest_player_distance; //from ave player position
	public GameObject[] _players; //array of players
	public float clamp_min_y = -1f; //clamps min average position
	public float clamp_max_y = 1f; //
	public float max_camera_distance = 8f; 
	public float min_cam_distance = 2f;
	public float max_player_seperation = 20f;

	public float distance_to_target {
		get { return _distance_to_target; }
		set { _distance_to_target = Mathf.Max (0.0f, value); } //Always have a value of 0 or greater
	}

	public float follow_speed{
		get{ return _follow_speed; }
		set{ _follow_speed = Mathf.Max (0.0f, value); } //Always have a value of 0 or greater
	} 


	public void InitiateCamera() {
		_players = GameObject.FindGameObjectsWithTag ("CamTar"); 
		_average = Vector3.zero;
		furthest_player_distance = Vector3.zero.magnitude;
	}

	void FixedUpdate(){
 
        InitiateCamera();

        distance_to_target = MaxDistanceOfPlayers() * max_camera_distance; //Sets cam distance from scene
		distance_to_target = Mathf.Clamp (distance_to_target, min_cam_distance, max_camera_distance); //Clamps cam distance

        Vector3 camRelPos = CameraRelativePosition();
        if(_players != null)
            transform.position = Vector3.Lerp(transform.position, camRelPos, follow_speed * Time.deltaTime); //
	}

	private Vector3 CameraRelativePosition(){

        Vector3 avgPlayerPos = AveragePlayerPosition();
        
        return (avgPlayerPos - transform.forward * distance_to_target);  // the vector we go to in the lerp. 
	}

	private Vector3 AveragePlayerPosition(){
        _average = Vector3.zero;
		for (int i = 0; i < _players.Length; i++) {
			_average += _players [i].transform.position;
		}
		_average /= _players.Length;

		_average.y = Mathf.Clamp (_average.y, clamp_min_y, clamp_max_y);

		return _average;
	}

	private float MaxDistanceOfPlayers(){
		furthest_player_distance = 0f;
		for (int i = 0; i < _players.Length; i++) {

			if (Vector3.Distance (_players [i].transform.position, AveragePlayerPosition ()) > furthest_player_distance) {
				furthest_player_distance = Vector3.Distance (_players [i].transform.position, AveragePlayerPosition ());
			}
		}

		furthest_player_distance /= max_player_seperation;

		return furthest_player_distance;
	}
}
