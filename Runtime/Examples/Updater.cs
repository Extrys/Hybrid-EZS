using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HybridEZS.Examples
{
	public class Updater : MonoBehaviour
	{
		public void DoUpdate()
		{
			Debug.Log("Updated");
		}
	}
	public class UpdatSys : SystemBase
	{
		protected override void OnUpdate()
		{
			Entities.ForEach((Updater up) =>
			{
				up.DoUpdate();
			}).WithoutBurst().Run();
		}
	}
}