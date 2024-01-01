using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldUniverseState : MonoBehaviour
{
    private OldUniverseTime _universeTime;

    public GameObject Moon { get; private set; }

    public Dictionary<string, CelestialBody> CelestialBodies { get; } = new();


    // Start is called before the first frame update
    void Start()
    {
        _universeTime = GetComponentInParent<OldUniverseTime>();
        
        if (_universeTime == null) {
            Debug.LogError("UniverseTime not found");
            Application.Quit();
        }

        // Universe center
        var universeCenter = OrbitNode.UniverseRootNode;

        // Sun orbit
        var sunOrbitNode = new OrbitNode(universeCenter, "Sun orbit");
        // sunOrbit.Modifiers.Add();
        var sunMesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sunMesh.name = "Sun";
        var sunDiameter = 5.0f;
        sunMesh.transform.localScale = new Vector3(sunDiameter, sunDiameter, sunDiameter);
        CelestialBodies.Add("Sun", new CelestialBody(sunOrbitNode, sunMesh));
        
        // Earth
        var earthOrbitNode = new OrbitNode(sunOrbitNode, "Earth orbit");
        var ellipsis = new Ellipse_XZ( 50.0f, 10.0f, 20.0f );
        earthOrbitNode.Modifiers.Add(new EllipsisOrbitModifier_XZ(ellipsis));
        var earthMesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        earthMesh.name = "Earth";
        var earthDiameter = 2.0f;
        earthMesh.transform.localScale = new Vector3(earthDiameter, earthDiameter, earthDiameter);
        CelestialBodies.Add("Earth", new CelestialBody(earthOrbitNode, earthMesh));
        
        // Moon      
        var moonOrbit = new OrbitNode(earthOrbitNode, "Moon orbit");
        ellipsis = new Ellipse_XZ( 3.0f, 1.5f, 5.0f );
        moonOrbit.Modifiers.Add(new EllipsisOrbitModifier_XZ(ellipsis));
        var moonMesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        moonMesh.name = "Moon";
        var moonDiameter = 0.2f;
        moonMesh.transform.localScale = new Vector3(moonDiameter, moonDiameter, moonDiameter);
        CelestialBodies.Add("Moon", new CelestialBody(moonOrbit, moonMesh));


    }

    // // Update is called once per frame
    // void Update()
    // {
    // }

    // Unity's fixed update
    void FixedUpdate()
    {
        var timeSeconds = _universeTime.CurrentTime;
        long timeMs = (long)(timeSeconds * 1000);
        // var universeRoot = OrbitNode.UniverseRootNode;

        // var lastDelta = _universeTime.LastDelta;

        foreach (var celestialBody in CelestialBodies.Values) {
            var orbitNode = celestialBody.OrbitNode;


            var x = orbitNode.X(timeMs);
            var y = orbitNode.Y(timeMs);
            var z = orbitNode.Z(timeMs);

            var position = new Vector3(x, y, z);

            celestialBody.Mesh.transform.position = position;

        }
    }
}
