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

    private void OnEnable()
    {
        _spawner.AllTimeObjectsChanged += UpdateTextAllTimeObjects;
        _spawner.SpawnObjectChanged += UpdateTextSpawnedObject;
        _spawner.ActiveObjectsChanged += UpdateTextActivedObjects;
    }

    private void OnDisable()
    {
        _spawner.AllTimeObjectsChanged -= UpdateTextAllTimeObjects;
        _spawner.SpawnObjectChanged -= UpdateTextSpawnedObject;
        _spawner.ActiveObjectsChanged -= UpdateTextActivedObjects;
    }

    private void UpdateTextAllTimeObjects(int count)
    {
        _textCountForAllTimeObjects.text = InfoForText1 + count;
    }

    private void UpdateTextSpawnedObject(int count)
    {
        _textCountAllObjects.text = InfoForText2 + count;
    }

    private void UpdateTextActivedObjects(int count)
    {
        _textActiveObjects.text = InfoForText3 + count;
    }
}
