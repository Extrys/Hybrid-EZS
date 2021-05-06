using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace HybridEZS
{
	[Serializable]
	public static class ComponentDataAuthorCollectionExtensions
	{
		public static void InsertComponentDatasIntoEntity(this List<ComponentDataAuthor> authors, Entity entity)
		{
			int iterations = authors.Count;
			for (int i = 0; i < iterations; i++)
				authors[i].InsertAuthoredComponentToEntity(entity);
		}
		public static void AddUniqueAuthor(this List<ComponentDataAuthor> authors, ComponentDataAuthor dataAuthor)
		{
			authors.RemoveAll(x => x == null);
			if (authors.CheckForAuthorInList(dataAuthor))
			{
				Object.DestroyImmediate(dataAuthor);
				Debug.LogError("You can not add more than ONE component of the same type to an entity, Use Buffers for that");
				return;
			}
			authors.Add(dataAuthor);
		}

		static bool CheckForAuthorInList(this List<ComponentDataAuthor> authors, ComponentDataAuthor author)
		{
			foreach (var auth in authors)
				if (auth.GetType() == author.GetType())
					return true;

			return false;
		}
	}
}