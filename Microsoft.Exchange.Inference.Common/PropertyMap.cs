using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000F RID: 15
	internal class PropertyMap<TPropertyMapping> : IDictionary<PropertyDefinition, TPropertyMapping>, ICollection<KeyValuePair<PropertyDefinition, TPropertyMapping>>, IEnumerable<KeyValuePair<PropertyDefinition, TPropertyMapping>>, IEnumerable where TPropertyMapping : PropertyMapping
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002D98 File Offset: 0x00000F98
		protected internal PropertyMap()
		{
			this.map = new Dictionary<PropertyDefinition, TPropertyMapping>();
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			foreach (FieldInfo fieldInfo in fields)
			{
				PropertyMappingAttribute[] array2 = fieldInfo.GetCustomAttributes(typeof(PropertyMappingAttribute), true) as PropertyMappingAttribute[];
				if (array2 != null && array2.Length > 0)
				{
					object value = fieldInfo.GetValue(null);
					TPropertyMapping tpropertyMapping = value as TPropertyMapping;
					if (tpropertyMapping != null)
					{
						this.map.Add(tpropertyMapping.GenericPropertyDefinition, tpropertyMapping);
					}
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E38 File Offset: 0x00001038
		public int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002E45 File Offset: 0x00001045
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E48 File Offset: 0x00001048
		public ICollection<PropertyDefinition> Keys
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002E55 File Offset: 0x00001055
		public ICollection<TPropertyMapping> Values
		{
			get
			{
				return this.map.Values;
			}
		}

		// Token: 0x1700002D RID: 45
		public TPropertyMapping this[PropertyDefinition key]
		{
			get
			{
				return this.map[key];
			}
			set
			{
				this.map[key] = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E7F File Offset: 0x0000107F
		public void Add(PropertyDefinition key, TPropertyMapping value)
		{
			this.map.Add(key, value);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E8E File Offset: 0x0000108E
		public bool ContainsKey(PropertyDefinition key)
		{
			return this.map.ContainsKey(key);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E9C File Offset: 0x0000109C
		public bool Remove(PropertyDefinition key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EA3 File Offset: 0x000010A3
		public bool TryGetValue(PropertyDefinition key, out TPropertyMapping value)
		{
			return this.map.TryGetValue(key, out value);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EB2 File Offset: 0x000010B2
		public void Add(KeyValuePair<PropertyDefinition, TPropertyMapping> item)
		{
			this.Add(item);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EBB File Offset: 0x000010BB
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EC2 File Offset: 0x000010C2
		public bool Contains(KeyValuePair<PropertyDefinition, TPropertyMapping> item)
		{
			return this.map.Contains(item);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void CopyTo(KeyValuePair<PropertyDefinition, TPropertyMapping>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002ED7 File Offset: 0x000010D7
		public bool Remove(KeyValuePair<PropertyDefinition, TPropertyMapping> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EDE File Offset: 0x000010DE
		public IEnumerator<KeyValuePair<PropertyDefinition, TPropertyMapping>> GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EEB File Offset: 0x000010EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x04000030 RID: 48
		private const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

		// Token: 0x04000031 RID: 49
		private IDictionary<PropertyDefinition, TPropertyMapping> map;
	}
}
