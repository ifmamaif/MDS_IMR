using UnityEngine;

public class CameraManager {

	private GameObject objectCamera;
	private Camera cam;

	public CameraManager(){
		objectCamera = new GameObject ("MainCamera");
		objectCamera.transform.position = new Vector3 (0, 0, -3);
		objectCamera.tag = "MainCamera";
		objectCamera.AddComponent<Camera> ();

		cam = objectCamera.GetComponent<Camera> ();
		cam.orthographic = true;

		cam.clearFlags = CameraClearFlags.SolidColor;
		cam.backgroundColor = Color.black;

		//cam.cullingMask = 0; // 0 = Nothing nu e bun Nothing

		cam.nearClipPlane = 0;
		cam.farClipPlane = 3;

		cam.orthographicSize = Screen.height /200; // Screen.height = 2 * orthographicSize * 100;

	}

	public void SetOrthographicSize(float size){
		cam.orthographicSize = size;
	}

	public Vector2 GetScreen(){
		Vector2 backScreen = new Vector2 ();
		backScreen.y = cam.orthographicSize * 200;
		backScreen.x = backScreen.y * cam.aspect;
		return backScreen;
	}

}
