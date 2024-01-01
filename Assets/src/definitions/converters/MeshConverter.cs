using Assets.src.definitions.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.definitions.converters
{
    internal class MeshConverter
    {
        public static (float diameter, string color) ToSphere(JsonMesh mesh, string id)
        {
            if (mesh.Params == null)
            {
                Debug.LogError($"{nameof(ToSphere)}: {nameof(JsonMesh.Params)} is missing in {id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!mesh.Params!.TryGetFloat("diameter", out var diameter)
                || diameter <= 0)
            {
                Debug.LogError($"{nameof(ToSphere)}: {nameof(diameter)} is missing in {id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!mesh.Params!.TryGetValue("color", out string color))
            {
                color = "#FFFFFF"; // fallback: white
            }

            return (diameter, color);
        }

        public static float ToSun(JsonMesh mesh, string id)
        {
            if (mesh.Params == null)
            {
                Debug.LogError($"{nameof(ToSun)}: {nameof(JsonMesh.Params)} is missing in {id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!mesh.Params!.TryGetFloat("diameter", out var diameter)
                || diameter <= 0)
            {
                Debug.LogError($"{nameof(ToSun)}: {nameof(diameter)} is missing in {id}");
                Application.Quit(); // TODO: better error handling.
            }
            return diameter;
        }

    }
}
