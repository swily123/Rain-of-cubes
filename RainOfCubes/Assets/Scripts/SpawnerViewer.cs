using TMPro;
using UnityEngine;

public class SpawnerViewer : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;

    [Header("text fields")]
    [SerializeField] private TextMeshProUGUI _textCountForAllTimeObjects;
    [SerializeField] private TextMeshProUGUI _textCountAllObjects;
    [SerializeField] private TextMeshProUGUI _textActiveObjects;

    private const string InfoForText1 = "количество заспавненых объектов за всё время: ";
    private const string InfoForText2 = "количество созданных объектов: ";
    private const string InfoForText3 = "количество активных объектов на сцене: ";

    private void Update()
    { 
        _textCountForAllTimeObjects.text = InfoForText1 + _spawner.SpawnCountForAllTime;
        _textCountAllObjects.text = InfoForText2 + _spawner.SpawnCount;
        _textActiveObjects.text = InfoForText3 + _spawner.ActiveObjectCount;
    }
}
