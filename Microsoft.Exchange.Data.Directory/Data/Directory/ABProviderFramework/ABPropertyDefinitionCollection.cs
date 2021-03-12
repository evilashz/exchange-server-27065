using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ABPropertyDefinitionCollection : IEnumerable<ABPropertyDefinition>, IEnumerable
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000030D8 File Offset: 0x000012D8
		public ABPropertyDefinitionCollection(ICollection<ABPropertyDefinition> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				throw new ArgumentNullException("propertyDefinitionCollection");
			}
			using (IEnumerator<ABPropertyDefinition> enumerator = properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw new ArgumentException("propertyDefinitionCollection contains null property.", "propertyDefinitionCollection");
					}
				}
			}
			this.propertyDefinitionCollection = properties;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003168 File Offset: 0x00001368
		public static ABPropertyDefinitionCollection FromPropertyDefinitionCollection(ICollection<PropertyDefinition> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				throw new ArgumentNullException("properties");
			}
			ICollection<ABPropertyDefinition> collection = new List<ABPropertyDefinition>(properties.Count);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ABPropertyDefinition abpropertyDefinition = (ABPropertyDefinition)propertyDefinition;
				if (abpropertyDefinition == null)
				{
					throw new ArgumentException("propertyDefinitionCollection contains null property.", "propertyDefinitionCollection");
				}
				collection.Add(abpropertyDefinition);
			}
			return new ABPropertyDefinitionCollection(collection);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000031F0 File Offset: 0x000013F0
		public int Count
		{
			get
			{
				return this.propertyDefinitionCollection.Count;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031FD File Offset: 0x000013FD
		public bool Contains(ABPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (this.propertyNames == null)
			{
				this.BuildPropertyNamesHashSet();
			}
			return this.propertyNames.Contains(propertyDefinition.Name);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000322C File Offset: 0x0000142C
		public object GetNativePropertyCollection(string providerToken)
		{
			if (string.IsNullOrEmpty(providerToken))
			{
				throw new ArgumentNullException("providerToken");
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Thread.VolatileRead(ref this.cache);
			object result;
			if (!dictionary.TryGetValue(providerToken, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000326C File Offset: 0x0000146C
		public void SetNativePropertyCollection(string providerToken, object nativeCollection)
		{
			if (string.IsNullOrEmpty(providerToken))
			{
				throw new ArgumentNullException("providerToken");
			}
			lock (this.updatingCacheLock)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>((Dictionary<string, object>)this.cache, StringComparer.OrdinalIgnoreCase);
				dictionary[providerToken] = nativeCollection;
				this.cache = dictionary;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000032E0 File Offset: 0x000014E0
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder(20 * this.propertyDefinitionCollection.Count);
			stringBuilder.Append("[");
			foreach (ABPropertyDefinition abpropertyDefinition in this.propertyDefinitionCollection)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(abpropertyDefinition.Name ?? "<null>");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034B4 File Offset: 0x000016B4
		public IEnumerator<ABPropertyDefinition> GetEnumerator()
		{
			foreach (ABPropertyDefinition propertyDefinition in this.propertyDefinitionCollection)
			{
				yield return propertyDefinition;
			}
			yield break;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003600 File Offset: 0x00001800
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (ABPropertyDefinition propertyDefinition in this.propertyDefinitionCollection)
			{
				yield return propertyDefinition;
			}
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000361C File Offset: 0x0000181C
		private void BuildPropertyNamesHashSet()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (ABPropertyDefinition abpropertyDefinition in this.propertyDefinitionCollection)
			{
				hashSet.Add(abpropertyDefinition.Name);
			}
			this.propertyNames = hashSet;
		}

		// Token: 0x0400002A RID: 42
		private static Dictionary<string, object> emptyCache = new Dictionary<string, object>(0, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400002B RID: 43
		private ICollection<ABPropertyDefinition> propertyDefinitionCollection;

		// Token: 0x0400002C RID: 44
		private HashSet<string> propertyNames;

		// Token: 0x0400002D RID: 45
		private object cache = ABPropertyDefinitionCollection.emptyCache;

		// Token: 0x0400002E RID: 46
		private object updatingCacheLock = new object();
	}
}
