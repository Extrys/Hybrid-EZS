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


Now just wait a few seconds and its ready to use!  




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

And this script is a MonoBehaviour using a fantastic and wonderful super fast Update [I'm being sarcastic, of course].

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

We just changed the Update for "DoUpdate" so we dont use the Unity one

If we hit play right now it will ***not*** work  
Because the Entities.ForEach just work on entities with "CoolMovement"
But... we have not entities yet...

Here comes the **Entity Injector** to the rescue!

### EntityInjector





(WIP)

