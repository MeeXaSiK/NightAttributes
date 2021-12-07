# NightAttributes
Attributes that make life easier for Unity

## How to use

1. Add files into your Unity project
2. Add "NightAttributesEntry" on any GameObject in scene

## [LazyFind] Attribute

Old implementation:

```sh
    public class Demo : MonoBehaviour
    {
        private Player _player;
    
        private void Start()
        {
            var playerInstances = FindObjectsOfType<Player>();
            var player = playerInstances.Length > 0 
                ? playerInstances[0] 
                : new GameObject($"[LazyFind] {type.Name}", type).GetComponent(type);
            
            for (var i = 1; i < playerInstances.Length; i++)
                Destroy(instances[i]);
                
            _player = player;
        }
    }
```

New implementation:

```sh
using Development.Global.Code.NightAttributes;

public class Demo : MonoBehaviour
{
    [LazyFind] private Player _player;
}
```

TODO if attribute using in class, that spawned after awake

```sh
using Development.Global.Code.NightAttributes;

public class SpawnedAfterAwake : MonoBehaviour
{
    [LazyFind] public PlayerUnit playerUnit;
    [LazyFind] public EnemyUnit enemyUnit;

    private void Start()
    {
        this.CheckForNightAttributes();
    }
}
```
