using TMPro;
using UnityEngine;
using System;

public class TimeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var universeTime = Root.CurrentGameInstance?.gameObject.GetComponent<UniverseTime>();

        // No instance running or game not ready
        if (universeTime == null)
        {
            return;
        }

        var formattedTime = new DateTime(1950,1,1,0,0,0).AddMilliseconds(universeTime.CurrentTimeMs).ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        var gui = gameObject.GetComponent<TextMeshProUGUI>();
        gui.text = $"Current time: {formattedTime}";
    }
}
