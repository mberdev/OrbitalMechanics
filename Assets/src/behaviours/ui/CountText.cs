using TMPro;
using UnityEngine;
using System;

public class CountText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Root.Instance.onGameCreated += OnInstanceCreated;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnInstanceCreated(GameInstance _, int count)
    {
        var textControl = gameObject.GetComponent<TextMeshProUGUI>();
        textControl.text = $"Objects count: {count}";
    }
}
