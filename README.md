# Interview Portfolio : Cave Hero
您好，這份repository的檔案皆來自我創作的遊戲 Cave Hero  
我節選了幾個我認為寫得最好的腳本，希望能展現我的技術實力，感謝您撥冗閱讀。

## ObjectPoolManager.cs
  ### Feature
  - **Efficient Object Management** : Reuses objects from the pool when available, reducing the need for frequent instantiation and destruction.
  - **Empty Holder System** : Implements a system to organize pooled objects under specific parent objects based on `PoolType`, keeping the hierarchy clean and organized.
  - **Easy To Add New PoolType** : Just add it to the `PoolType` enum fields without modifying any other code.
  - **Easy To Use** : Implemented with the Singleton pattern, it can be called from anywhere. Just one line of code :
    ```
    var spawnedObj = ObjectPoolManager.Instance.SpawnObject(objectToSpawn, spawnPosition, spawnRotation, PoolType.GameObject);
    ```
