using Assets.src;
using Assets.src.definitions;
using Assets.src.state2;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this behaviour to any empty node
/// for it to act as a cheap UI for the program.
/// </summary>
public class UI : MonoBehaviour
{

    private bool gotValidated = false;

    //+TEST

    //[SerializeField] bool aBool;

    //private bool _oldABool;

    //public bool ABool
    //{
    //    get => aBool;
    //    set
    //    {
    //        aBool = value;

    //        if (aBool != _oldABool)
    //        {
    //            if (aBool)
    //            {
    //                // do stuff
    //            }
    //            else
    //            {
    //                // do stuff
    //            }
    //        }

    //        _oldABool = aBool;
    //    }
    //}

    //private void OnValidate()
    //{
    //    // Only call properties during PlayMode since they might depend on runtime stuff
    //    if (!Application.isPlaying) return;

    //    ABool = aBool;
    //}

    //-TEST



    // Global universe time as observed by the UI.
    // (for now it also acts as the actual time)
    [SerializeField]
    public long TimeMs;
    private long _previousTimeMs;




    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gotValidated)
        {
            gotValidated = false;

            Engine.Instance.Time.ChangeTime_Instantaneous(TimeMs);
            _previousTimeMs = TimeMs;

            // TODO: only when needed
            GenerateGlobalSnapshot();


            return;
        }
    }

    private void OnValidate()
    {
        // Only call properties during PlayMode since they might depend on runtime stuff
        if (!Application.isPlaying) return;

        if (TimeMs != _previousTimeMs)
        {
            if (TimeMs < 0)
            {
                Debug.LogWarning($"TimeMs must be >= 0.");
                TimeMs = _previousTimeMs;
                gotValidated = false;
                return;
            }
            gotValidated = true;
        }

    }

    public void GenerateGlobalSnapshot()
    {
        // Temporary : Generate global snapshot (here?)
        var snapshot = GlobalSnapshot.Create(Engine.Instance.Definition, TimeMs);

        var snapshotNode = SnapshotsManager.Instance.GetOrCreateSnapshotNode(snapshot.TimeMs.ToString());
        snapshotNode.Generate(snapshot);
    }
}
