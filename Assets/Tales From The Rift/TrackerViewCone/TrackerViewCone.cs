using UnityEngine;

namespace TalesFromTheRift {

	public class TrackerViewCone : MonoBehaviour {

		public bool showViewCone = false;
		public int trackerIndex = 0;
		public Material mat;

		public void Update() {

			// Create/Destroy the view cone
			if (showViewCone) {
				// Create the view cone renderer if needed
				if (GetComponent<LineRenderer>() == null && OVRManager.tracker.isPresent && OVRManager.tracker.isEnabled)  {
					OVRTracker.Frustum frustrum = OVRManager.tracker.GetFrustum(trackerIndex);
					AddViewConeLineRenderer(frustrum.fov, frustrum.nearZ, frustrum.farZ);
				}
			} else {
				// Destroy the view cone renderer if we have one
				LineRenderer lr = gameObject.GetComponent<LineRenderer> ();
				if (lr != null) {
					Destroy (lr);
				}
			}
		}

		void AddViewConeLineRenderer(Vector2 fov, float nearZ, float farZ)
		{
			LineRenderer lr = gameObject.GetComponent<LineRenderer>();
			if (lr == null)
			{
				lr = gameObject.AddComponent<LineRenderer>();
				lr.useWorldSpace = false;
				lr.SetWidth(0.001f, 0.001f);
				lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
				lr.receiveShadows = false;
				if (mat == null) 
				{
					mat = new Material(Shader.Find("Unlit/Color"));
					mat.color = Color.red;
				}
				lr.material = mat;
			}

			float z = farZ - nearZ;
			Vector3 x =  Vector3.left * (z * Mathf.Sin(Mathf.Deg2Rad * fov.x/2));
			Vector3 y =  Vector3.up * (z * Mathf.Sin(Mathf.Deg2Rad * fov.y/2));
			Vector3 farCenter =  Vector3.forward * z;

			// Normalized View Cone co-ordinates
			Vector3[] points = new Vector3[] { 
				new Vector3(0,0,0), 	// Apex
				new Vector3(-1,1,-1), 	// Top-Left to
				new Vector3(1,1,-1), 	// Top-Right
				new Vector3(0,0,0), 	// Apex
				new Vector3(-1,-1,-1), 	// Bottom-Left to
				new Vector3(1,-1,-1),	// Bottom-Right
				new Vector3(0,0,0), 	// Apex
				new Vector3(-1,1,-1), 	// Top-Left to
				new Vector3(-1,-1,-1), 	// Bottom-Left
				new Vector3(1,-1,-1), 	// Bottom-Right to
				new Vector3(1,1,-1)		// Top-Right
			};

			// Scale and position the Veiw Cone into world space
			lr.SetVertexCount(points.Length);
			int n = 0;
			foreach (Vector3 point in points)
			{
				lr.SetPosition(n++, farCenter * -point.z + x * point.x + y * point.y);
			}
		}
	}

}