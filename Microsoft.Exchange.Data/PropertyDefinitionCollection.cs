using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PropertyDefinitionCollection
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005100 File Offset: 0x00003300
		internal static T[] Merge<T>(params IEnumerable<T>[] propertyDefinitionCollections) where T : PropertyDefinition
		{
			ArgumentValidator.ThrowIfNull("propertyDefinitionCollections", propertyDefinitionCollections);
			ArgumentValidator.ThrowIfInvalidValue<int>("propertyDefinitionCollections.Length", propertyDefinitionCollections.Length, (int length) => length > 1);
			HashSet<T> hashSet = new HashSet<T>();
			foreach (IList<T> list in propertyDefinitionCollections)
			{
				if (list != null)
				{
					hashSet.UnionWith(list);
				}
			}
			return PropertyDefinitionCollection.GetArray<T>(hashSet);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005168 File Offset: 0x00003368
		internal static T[] Merge<T>(IEnumerable<T> propertyDefinitionCollection, params T[] properties) where T : PropertyDefinition
		{
			ArgumentValidator.ThrowIfNull("propertyDefinitionCollection", propertyDefinitionCollection);
			ArgumentValidator.ThrowIfNull("properties", properties);
			ArgumentValidator.ThrowIfInvalidValue<int>("properties.Length", properties.Length, (int length) => length > 0);
			HashSet<T> hashSet = new HashSet<T>(propertyDefinitionCollection);
			hashSet.UnionWith(properties);
			return PropertyDefinitionCollection.GetArray<T>(hashSet);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000051B8 File Offset: 0x000033B8
		private static T[] GetArray<T>(HashSet<T> properties)
		{
			T[] array = new T[properties.Count];
			properties.CopyTo(array);
			return array;
		}
	}
}
