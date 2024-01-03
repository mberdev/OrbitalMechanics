#nullable enable


using Assets.src.computeShaders;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
public class FixedOrbitsTimeTracker : MonoBehaviour, ITimeTracker
{
    public long lastTimeUpdateMs { get; private set; } = -1;

    #region GPU
    public ComputeShader shader;
    private ComputeBuffer computeBuffer;
    #endregion

    public FunctionsGroupsManager FgManager = new();

    #region groupsManager
    public class FunctionsGroupsManager
    {

        public static int maxDepth = 7;

        public CSOrbitFunction[] csFunctions = new CSOrbitFunction[0];

        public DepthData[] DepthDatas = new DepthData[maxDepth];
        public class DepthData
        {
            public List<CSOrbitFunction> AllFunctions { get; } = new();
            public List<FunctionsGroup> Groups { get;  } = new();
            public int StartIndex_AllFunctions { get; set; }
            public int EndIndex_AllFunctions { get; set; }
        }


        // Reset structure (call once)
        public void StartAddingFunctions()
        {
            csFunctions = new CSOrbitFunction[0];
            DepthDatas = new DepthData[maxDepth];
        }

        public class FunctionsGroup
        {
            public int Depth { get; }
            public FixedOrbitTimeTracker Node { get; }
            public FixedOrbitTimeTracker? Parent { get; }
            public int StartIndex_DepthData { get; set; }
            public int EndIndex_DepthData { get; set; }

            public int StartIndex_AllFunctions { get; set; }
            public int EndIndex_AllFunctions { get; set; }

            public FunctionsGroup(int depth, FixedOrbitTimeTracker node, FixedOrbitTimeTracker? parent, int startIndex, int endIndex)
            {
                Depth = depth;  
                Node = node;
                Parent = parent;
                StartIndex_DepthData = startIndex;
                EndIndex_DepthData = endIndex;
                StartIndex_AllFunctions = -1; //won't be known until EndAddingFunctions
                EndIndex_AllFunctions = -1; //won't be known until EndAddingFunctions
            }
        }

        public void AddGgroup(FixedOrbitTimeTracker tt, FixedOrbitTimeTracker? parent, int depth)
        {
            if (depth >= maxDepth)
            {
                Debug.LogError($"{nameof(FixedOrbitsTimeTracker)}: depth {depth} is too big");
                Application.Quit();
                return;
            }

            var depthData = DepthDatas[depth];
            if (depthData == null) {
                DepthDatas[depth] = new DepthData();
                depthData = DepthDatas[depth];
            }

            int startIndex = depthData.AllFunctions.Count;
            depthData.AllFunctions.AddRange(tt.FixedOrbitFunctions);
            int endIndex = depthData.AllFunctions.Count - 1; // Can be -1 if the group is empty

            var group = new FunctionsGroup(depth, tt, null, startIndex, endIndex);
            depthData.Groups.Add(group);
        }

        public void EndAddingFunctions()
        {
            var allFunctions = new List<CSOrbitFunction>();

            foreach (var depthData in DepthDatas)
            {
                if (depthData == null)
                {
                    continue;
                }

                depthData.StartIndex_AllFunctions = allFunctions.Count;
                allFunctions.AddRange(depthData.AllFunctions);
                depthData.EndIndex_AllFunctions = allFunctions.Count - 1;

                foreach (var group in depthData.Groups)
                {
                    group.StartIndex_AllFunctions = depthData.StartIndex_AllFunctions + group.StartIndex_DepthData;
                    group.EndIndex_AllFunctions = depthData.StartIndex_AllFunctions + group.EndIndex_DepthData;
                
                    group.StartIndex_DepthData = -1; //won't be needed anymore
                    group.EndIndex_DepthData = -1; //won't be needed anymore
                }

                depthData.AllFunctions.Clear();
            }

            csFunctions = allFunctions.ToArray();
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        shader = Root.Instance.orbitFunctionsComputeShader;
    }


    public void UpdateTime(long timeMs)
    {
        ComputeAllFunctions_GPU(timeMs);
    }


    private void InitGPU(int max)
    {
        int count = FgManager.csFunctions.Length;
        // Because that's the value we set in the shader's dimensions
        if (count > max)
        {
            Debug.LogError($"Too many functions ({count}) for the shader. Max is {max}.");
            Application.Quit(); // TODO: better error handling.
            return;
        }

        int sizeInBytes = CSOrbitFunction.SizeInBytes;
        computeBuffer = new ComputeBuffer(count, sizeInBytes);
        computeBuffer.SetData(FgManager.csFunctions);

        // Set array of functions once and for all
        shader.SetBuffer(0, "functions", computeBuffer);
    }

    private void ComputeAllFunctions_GPU(long timeMs)
    {

        if (timeMs > int.MaxValue)
        {
            Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: timeMs {timeMs} is too big for the shader");
            Application.Quit();
            return;
        }

        if (FgManager.csFunctions.Length == 0)
        {
            Debug.LogWarning($"{nameof(FixedOrbitTimeTracker)}: no functions to compute");
            return;
        }


        if (computeBuffer == null)
        {
            const int MAX = 1024; // Must match the value in the shader
            InitGPU(MAX);
        }


        var timeMs_ID = Shader.PropertyToID("timeMs");
        shader.SetInt(timeMs_ID, (int)timeMs);

        // Run the shader
        int count = FgManager.csFunctions.Length;
        shader.Dispatch(0, count, 1, 1);

        // TODO: put here some medium-length processing here
        //       while the shader works



        computeBuffer.GetData(FgManager.csFunctions);


        for(int depth = 0; depth < FunctionsGroupsManager.maxDepth; depth++)
        {
            var depthData = FgManager.DepthDatas[depth];
            if (depthData == null)
            {
                continue;
            }

            var groups = depthData.Groups;
            foreach (var group in groups)
            {
                var node = group.Node;
                var startIndex = group.StartIndex_AllFunctions;
                var endIndex = group.EndIndex_AllFunctions;

                Vector3 groupSum = Vector3.zero;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var f = FgManager.csFunctions[i];
                    groupSum.x += f.outX;
                    groupSum.y += f.outY;
                    groupSum.z += f.outZ;
                }

                var parentPosition = group.Parent?.transform.localPosition ?? Vector3.zero;
                group.Node.transform.localPosition = parentPosition + groupSum;
            }
        }

    }

    void OnDestroy()
    {
        if (computeBuffer != null)
        {
            computeBuffer.Dispose();
        }
    }

}
