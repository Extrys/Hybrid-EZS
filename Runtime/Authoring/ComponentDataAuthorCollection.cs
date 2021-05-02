using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace HybridEZS
{
	[Serializable]
	public class ComponentDataAuthorCollection
	{
		public List<ComponentDataAuthor> authors = new List<ComponentDataAuthor>();
		public void InsertComponentDatasIntoEntity(Entity entity)
		{
			int iterations = authors.Count;
			for (int i = 0; i < iterations; i++)
				authors[i].InsertAuthoredComponentToEntity(entity);
		}
		public void AddUniqueAuthor(ComponentDataAuthor dataAuthor)
		{
			authors.RemoveAll(x => x == null);
			if (CheckForAuthorInList(dataAuthor))
			{
				Object.DestroyImmediate(dataAuthor);
				Debug.LogError("You can not add more than ONE component of the same type to an entity, Use Buffers for that");
				return;
			}
			authors.Add(dataAuthor);
		}

		bool CheckForAuthorInList(ComponentDataAuthor author)
		{
			foreach (var auth in authors)
				if (auth.GetType() == author.GetType())
					return true;

			return false;
		}
	}
}