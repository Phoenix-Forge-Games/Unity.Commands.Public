# Unity.Commands.Public
This is a simple Command System, a barebones version of a current system I'm working on (Which will include a more robust system handling colliders, UI, Audio, Video, etc)

The concept is simple. You have a Base Scriptable Object called ForgeCommand



ForgeCommand has 2 virtual methods (For those that don't know, virtual means you can override them in a derived(child) class)

```*.csharp
public class ForgeCommand : ScriptableObject {
    
    //A base Execute command
    public virtual void Execute() {
    }
    
    //A base Execute command that takes a MonoBehavior
    public virtual void Execute(MonoBehavior caller) {
        
    }
}
```

The second one, is the one I use the most, because it allows you to use commands to modify things on your monobehaviors.
Because of that, I've added a protected property to allow ease of access to the monobehavior.

```*.csharp
public class ForgeCommand : ScriptableObject {
    
    protected ForgeCommandComponent ForgeCommandComponent;
    
    //A base Execute command
    public virtual void Execute() {
    
    }
    
    //A base Execute command that takes a MonoBehavior
    public virtual void Execute(MonoBehavior caller) {        
        ForgeCommandComponent = caller.GetComponent<ForgeCommandComponent>();
        Execute();
        
    }
}
```

Now, if you don't want to use my included command component (I recommend you do, it adds lifecycle command lists for each of the gameobject's lifecycle events)
you can simply not access the protected field as it will be null.


Knowing that, You write a command that inherits ForgeCommand (Make sure it has the create asset menu attribute):

```*.csharp

[CreateAssetMenu(fileName = "MyCommand - MyCommandAction - ", menuName = "MyCommandMenu/MyCommand", order = 0)]
public class MyNewCommand : ForgeCommand {

    public override void Execute(MonoBehavior caller) {
        //Do Stuff
    }
    
}
```

You can even reference other scriptable objects, so for instance, if you run the observer pattern:


```*.csharp

[CreateAssetMenu(fileName = "MyEventChannel", menuName = "MyEvents/MyEventChannel", order = 0)]
public class MyEventChannel : ScriptableObject {

    public UnityAction<string> MyStringEvent;
    
    public UnityAction<MonoBehavior> MyMonoBehaviorEvent;
    
    public void RaiseMyStringEvent(string myString) {
    
        MyStringEvent?.Invoke(myString);
        
    }
    
    public void RaiseMonoBehaviorEvent(MonoBehavior caller) {
    
        MyMonoBehaviorEvent?.Invoke(caller);
        
    }
}
```

And then wanna raise that event (Or others):

```*.csharp

[CreateAssetMenu(fileName = "MyCommand - MyCommandAction - ", menuName = "MyCommandMenu/MyCommand", order = 0)]
public class MyNewCommand : ForgeCommand {
    
    //Assign in inspector after creating the scriptable object asset    
    public MyEventChannel myEventChannel;
    
    public string stringForMyEvent;

    public override void Execute(MonoBehavior caller) {
    
        myEventChannel?.RaiseMyStringEvent(stringForMyEvent);
        
        myEventChannel?.RaiseMonoBehaviorEvent(caller);
        
    }
}
```

There's a LOT you can do with this functionality. It essentially lets you turn scriptable objects into functions where you can manually define parameters.



I've included a few example events, including one that shows you how to execute coroutines.
Check the demo scene.

The ForgeCommandComponentBehavior is pretty self-explanatory. You add any commands that inherit ForgeCommand to the list, and it will execute them during the lifecycle event. Again, check demo scene

The Demo Scene uses Text Mesh Pro, and will need to be 