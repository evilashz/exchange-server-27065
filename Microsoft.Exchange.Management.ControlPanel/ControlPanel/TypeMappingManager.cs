using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006CF RID: 1743
	public class TypeMappingManager<T> where T : class, ITypeMapping
	{
		// Token: 0x06004A05 RID: 18949 RVA: 0x000E2978 File Offset: 0x000E0B78
		public void RegisterMapping(T typeMapping)
		{
			int i;
			for (i = this.sortedTypeMappings.Count; i > 0; i--)
			{
				Type sourceType = typeMapping.SourceType;
				T t = this.sortedTypeMappings[i - 1];
				if (sourceType.IsAssignableFrom(t.SourceType))
				{
					break;
				}
			}
			this.sortedTypeMappings.Insert(i, typeMapping);
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x000E2A20 File Offset: 0x000E0C20
		public T[] GetNearestMappings(Type type)
		{
			T firstNeareastMapping = this.sortedTypeMappings.FirstOrDefault((T mapping) => mapping.SourceType.IsAssignableFrom(type));
			if (firstNeareastMapping != null)
			{
				return (from mapping in this.sortedTypeMappings
				where mapping.SourceType == firstNeareastMapping.SourceType
				select mapping).ToArray<T>();
			}
			return new T[0];
		}

		// Token: 0x04003189 RID: 12681
		private List<T> sortedTypeMappings = new List<T>();
	}
}
