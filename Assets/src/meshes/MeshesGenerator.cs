using Assets.src.definitions.converters;
using Assets.src.definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.meshes
{
    internal static class MeshesGenerator
    {

        public static GameObject CreateSunMesh(string id, JsonMesh meshDefinition)
        {
            var diameter = MeshConverter.ToSun(meshDefinition, id);

            var o = UnityEngine.Object.Instantiate(Root.Instance.sunPrefab);

            //The size is actually decided by the particle system's start size, not the transform.
            //mesh.transform.localScale = new Vector3(diameter, diameter, diameter);
            var particleSystem = o.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startSize = diameter;

            return o;
        }

        public static GameObject CreateSphereMesh(string id, JsonMesh meshDefinition)
        {
            var (diameter, color) = MeshConverter.ToSphere(meshDefinition, id);
            // (TODO : nicer meshes)
            var mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            if (!ColorUtility.TryParseHtmlString(color, out var colorParsed))
            {
                Debug.LogError($"Invalid color {color} for {id}");
                Application.Quit(); // TODO: better error handling.
            }

            mesh.GetComponent<Renderer>().material.color = colorParsed;
            mesh.transform.localScale = new Vector3(diameter, diameter, diameter);

            return mesh;

        }
    }
}
