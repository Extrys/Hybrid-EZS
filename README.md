# Hybrid-EZS    (WIP Readme)

Welcome!</br>
This repository has been created for help to streamline the transition from MonoBehaviour to DOTS in a simple and modular way , 
making the ECS Hybridation much easer for the developer who works with Monobehaviours, 
giving in this way a better performance and architecture to your MonoBehaviour based projects

<details>
<summary>Table Of Contents</summary>

  - [Introduction](#introduction)
  - [Features](#features)

</details>


## Introduction
Usually the terms ECS and DOTS are confusing for new developers or for experienced ones with a strong OOP mindset, 
other know about ECS and DOTS but are not sure how to port their project, there are also large projects that would be a nightmare to port to ECS, and let alone DOTS.

This project has been created to solve most of these problems, focusing on offering a much smoother and faster workflow to allow working with Monobehaviours and entities at the same time in a hybrid way.


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
Easy!</br>



Window > Package Manager > + > Add package from git URL... > ``com.squirrelbytes.ezs``</br>

if that doesnt works then...</br>

Window > Package Manager > + > Add package from git URL... > ``https://github.com/Extrys/Hybrid-EZS.git``</br>


![HowToInstall](https://user-images.githubusercontent.com/38926085/117202445-1d47d280-adee-11eb-9d8d-33ae2d93b749.png)


Now just wait a few seconds and its ready to use!</br>




## Basic Usage
Once everything is ready to start using the EZS Hybrid workflow, you might want to start using it, otherwise I don't know what you're doing here!</br>



