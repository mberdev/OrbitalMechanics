using TMPro;
using UnityEngine;
using System;

public class TimeText : MonoBehaviour
{
    private UniverseTime _universeTime;

    // Start is called before the first frame update
    void Start()
    {
        Root.Instance.onGameInstanceCreated += OnGameInstanceCreated;
    }

    private void OnGameInstanceCreated(GameInstance instance, int count)
    {
        _universeTime = instance.gameObject.GetComponent<UniverseTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_universeTime == null)
        {
            return;
        }

        var formattedTime = new DateTime(1950,1,1,0,0,0).AddMilliseconds(_universeTime.CurrentTimeMs).ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        var textControl = gameObject.GetComponent<TextMeshProUGUI>();
        textControl.text = $"Current time: {formattedTime}";
    }
}
