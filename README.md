# Hybrid-EZS    (WIP Readme)

Welcome!

This repository has been created for help to streamline the transition from MonoBehaviour to DOTS in a simple and modular way  
making the ECS Hybridation much easer for the developer who works with Monobehaviours  
and giving in this way a better performance and architecture to your MonoBehaviour based projects  

<details>
<summary>Table Of Contents</summary>

  - [Introduction](#introduction)
  - [Features](#features)

</details>


## Introduction
- Usually the terms ECS and DOTS are confusing for new developers or for experienced ones with a strong OOP mindset  
- Other developers know about ECS and DOTS but are not sure how to port their projects  
- There are also large projects that would be a nightmare to port to ECS, and let alone DOTS.

This project has been created to solve most of these problems  
by focusing on offering a much smoother and faster workflow to work with Monobehaviours and entities at the same time in a hybrid way.

## Features
A few features have been made to avoid the sometimes excesive code quantity needed to work with ECS and conversions, among others, the most important are:

- Custom Entity Injector
  - Enables easier and more modular way to inject custom converted entities from a GameObject

- Entity extensions
  - Making it much easier to do operations on entities, such as add components and so

- Easier Component Conversion
  - Forget the entity conversion from unity and use way much less code making it faster to create ComponentDatas for attaching them to a GameObject

- Easy entity referencing
  - Make an entity be referenced by other entities by creating automatic entity dynamic buffers from inspector

- Easy System Injection
  - Offers a super-duper easy way to create a system with custom depencences, for example a scriptable object that you can configute on the fly while system runs


## Installation
Easy!  



Window > Package Manager > + > Add package from git URL... > ``com.squirrelbytes.ezs``  

if that doesnt works then...  

Window > Package Manager > + > Add package from git URL... > ``https://github.com/Extrys/Hybrid-EZS.git``  


![HowToInstall](https://user-images.githubusercontent.com/38926085/117202445-1d47d280-adee-11eb-9d8d-33ae2d93b749.png)  

There is one more thing to do!

Now just need to configure the script Execution order and put "HybridEZS.EntityInjector" just over default   
Its easy too just follow the next image  
![ChangeExecutionOrder](https://user-images.githubusercontent.com/38926085/117226383-d6b99e80-ae14-11eb-9ef6-de2c26478cb1.png)

Apply the changes and wait a few seconds and...  

***Its ready to use!***




## Basic Usage
Once everything is ready to start using the EZS Hybrid workflow, you might want to start using it, otherwise I don't know what you're doing here!  

Lets start with a basic example!  

The EZ Cube  

![GIF 05-05-2021 23-24-36](https://user-images.githubusercontent.com/38926085/117211305-1d999b00-adf9-11eb-9915-8c8b73a9310a.gif)  

I know is the best thing you have seen ever but lets get to the point!

What we have here is a Cube with a "CoolMovement" Script attached

Here we have some variables here that makes our cube move like this

> **Current Oscilation Time:**  
>   Its a number that changes a lot  
>   
> **Oscilation Speed:**  
>   How fast it moves  
>   
> **Oscilation Amplitude Multiplier:**  
>   How far it moves  
>   
> **Rotation Speed:**  
>   How quickly he starts to get dizzy  

And this script is a MonoBehaviour using a fantastic and wonderful super fast Update [*I'm being sarcastic, of course*].

```csharp
public class CoolMovement : MonoBehaviour
{
	public float currentOscilationTime;
	public float oscilationSpeed;
	public float oscilationAmplitude;
	public float rotationSpeed;

	void Update()
	{
		currentOscilationTime += oscilationSpeed * Time.deltaTime;
		transform.position = new Vector3(0, Mathf.Sin(currentOscilationTime) * oscilationAmplitude, 0);
		transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
	}
}
```

Ok so lets make this MonoBehaviour use a System instead, shall we?

```csharp
public class CoolMovement : MonoBehaviour
{
	public float currentOscilationTime;
	public float oscilationSpeed;
	public float oscilationAmplitude;
	public float rotationSpeed;

	public void DoUpdate() //Changed the name
	{
		currentOscilationTime += oscilationSpeed * Time.deltaTime;
		transform.position = new Vector3(0, Mathf.Sin(currentOscilationTime) * oscilationAmplitude, 0);
		transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
	}
}

public class CoolMovementSystem : SystemBase
{
	protected override void OnUpdate()
	{
		Entities
			.ForEach((CoolMovement coolMovement) => coolMovement.DoUpdate())
			.WithoutBurst()  //Because MonoBehaviours can not be Bursted
			.Run(); //To run in main thread 
	}
}
```
Ok done! :D

We just changed the "Update" for "DoUpdate" so we dont use the Unity one

If we hit play right now it will ***not*** work  
Because the Entities.ForEach just work on entities with "CoolMovement"
But... we have not entities yet...

Here comes the **Entity Injector** to the rescue!

### Injecting an Entity

This is the main component that defines the workflow!  
![image](https://user-images.githubusercontent.com/38926085/117226660-6b240100-ae15-11eb-87d6-4931e6ed0838.png)

As you see it has 3 lists, each one is for different purpouses that will be explained easier a little bit later in other section  
but for now just Drag N Drop the CoolMovement Component to the EntityInjector in this way:  
![image](https://user-images.githubusercontent.com/38926085/117226852-d66dd300-ae15-11eb-8845-71843faa4e9c.png)

Congratulations you have added a monobehaviour to your future entity

You can press play, enjoy your spinning cube, sell the game and get rich!

But wait there is more...

What about moving finally to DOTS?

We have been using this component just to make an entity having this monobehaviour attached as component, so we can query it in systems, is for that it works now  
just hybridizing the game will give you a little bit of performance, depending on the "Update" usage you have in your game  
but most of the times the performance gain is not so heavy, unless you start using it correctly

So this is not the end, now the next step is to segregate the data and disintegrate the OnUpdate bit a bit until all the logic is inside the system and the references are components of the gameobject to conver them easily to data.

I will be doing that transition later so you can see how easy it is

Lets take apart this section to start with something more technical and a little more advanced usages on this component


## Entity Injector
![image](https://user-images.githubusercontent.com/38926085/117227835-0322ea00-ae18-11eb-9197-ee9870b8c1b7.png)

(WIP)

