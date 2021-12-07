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
            
            for (var i = 1; i < playerInstances.Length; i++)
                Destroy(playerInstances[i]);
                
            var instance = playerInstances[0];
            
            if (instance == null)
                instance = new GameObject(nameof(Player), typeof(Player));
                
            _player = instance;
        }
    }
```

New implementation:

```sh
    public class Demo : MonoBehaviour
    {
        [LazyFind] private Player _player;
    }
```
