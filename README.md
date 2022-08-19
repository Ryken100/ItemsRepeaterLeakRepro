# ItemsRepeater leak repro project

## Info
When an `ItemsRepeater` is removed from the visual tree while there is an object or objects in its `ItemsSource` collection, the `ItemsRepeater` is not released from memory. If the `ItemsReapeater` does not have any children, its released correctly.

## Repro project
* Build and deploy the project
* Click the "Create a new one" button twice to create a new `MyUserControl` and add it to the visual tree
  * `MyUserControl` contains an `ItemsRepeater` which is added to a static collection of `WeakReference<ItemsRepeater>` instances
* Observe the "Alive count" text in the top right, which counts the number of active `WeakReference<ItemsRepeater>` instances

The text should say "Alive count: 1" since two instances of `MyUserControl` were created, and the first one should have been released along with its `ItemsRepeater`, instead it will say "Alive count: 2".
