# Interview Portfolio : Cave Hero
您好，這份repository的檔案皆來自我創作的遊戲 Cave Hero  
我節選了幾個我認為寫得最好的腳本，希望能展現我的技術實力，感謝您撥冗閱讀。  
  
itch.io遊戲試玩連結 : https://dandee555.itch.io/cave-hero-demo-version

## ObjectPoolManager.cs
  ### Feature
  - **Efficient Object Management** : Reuses objects from the pool when available, reducing the need for frequent instantiation and destruction.
  - **Empty Holder System** : Implements a system to organize pooled objects under specific parent objects based on `PoolType`, keeping the hierarchy clean and organized.
  - **Easy To Add New PoolType** : Just add it to the `PoolType` enum fields without modifying any other code.
  - **Easy To Use** : Implemented with the Singleton pattern, it can be called from anywhere. Just one line of code :
    ```
    var spawnedObj = ObjectPoolManager.Instance.SpawnObject(objectToSpawn, spawnPosition, spawnRotation, PoolType.GameObjects);
    ```
    `objectToSpawn` : The gameobject you want to spawn.  
    `spawnPosition` : The position where the object should be spawned.  
    `spawnRotation` : The rotation that the spawned object should have.  
    `PoolType.GameObjects(Optional)` : The parameter for empty holder system. Think of it as a folder.
## SEManager.cs & SELibrary.cs
  ### Feature
  - **Easy Management** : Uses a library system to make resource management straightforward.
  - **Highly Secure** : Throws an exception if the given parameter does not correspond to an audioclip, informing the user immediately.
  - **Uses Object Pool System** : Reuses resources efficiently to avoid performance wastage.
  - **Easy To Use** : Implemented with the Singleton pattern, it can be called from anywhere. Just one line of code :
    ```
    SEManager.Instance.PlaySound(groupName, soundName, playPosition, volumn, isLoop);
    ```
    `groupName` : The sound effect group name.  
    `soundName` : The sound effect name.  
    `playPosition` : The position where the sound effect should be played.  
    `volumn(Optional)` : Volumn setting.  
    `isLoop(Optional)` : Loop setting.
## DestructibleBase.cs
  ### Overview
  `DestructibleBase` is an abstract base class designed to implement core functionality for destructible objects in Cave Hero.   
  This class leverages the features of abstract classes to provide a universal framework for destructible objects.
  ### Feature
  - **Code Reusability** : `DestructibleBase` defines common properties and methods for destructible objects. Subclasses can directly inherit these common features, reducing code duplication.
  - **Improved Code Maintainability** : Centralizing common functionality makes modifications and extensions easier to manage.
  - **Polymorphism Promotion** : Allows handling different types of destructible objects through base class references.
  - **Increase Flexibility** :
    - The `GetHitOneTime()` method provides a common logic that defines the destruction process for all destructible objects.
    - The abstract methods `OnHit()` and `OnDestruction()` allow for customized behavior for each specific type of destructible object.





  
