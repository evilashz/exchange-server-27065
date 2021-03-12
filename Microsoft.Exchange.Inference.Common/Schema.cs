using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000D RID: 13
	internal class Schema
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002C14 File Offset: 0x00000E14
		protected internal Schema()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			HashSet<PropertyDefinition> hashSet2 = new HashSet<PropertyDefinition>();
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			foreach (FieldInfo fieldInfo in fields)
			{
				object value = fieldInfo.GetValue(null);
				PropertyDefinition propertyDefinition = value as PropertyDefinition;
				if (propertyDefinition != null)
				{
					hashSet2.Add(propertyDefinition);
					if (fieldInfo.IsPublic)
					{
						hashSet.Add(propertyDefinition);
					}
				}
			}
			this.allProperties = hashSet;
			this.allPropertiesInternal = hashSet2;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002C9B File Offset: 0x00000E9B
		public static Schema Instance
		{
			get
			{
				if (Schema.instance == null)
				{
					Schema.instance = new Schema();
				}
				return Schema.instance;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public ICollection<PropertyDefinition> AllProperties
		{
			get
			{
				return this.allProperties;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002CBB File Offset: 0x00000EBB
		internal ICollection<PropertyDefinition> InternalAllProperties
		{
			get
			{
				return this.allPropertiesInternal;
			}
		}

		// Token: 0x04000025 RID: 37
		private const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000026 RID: 38
		private static Schema instance;

		// Token: 0x04000027 RID: 39
		private HashSet<PropertyDefinition> allProperties;

		// Token: 0x04000028 RID: 40
		private HashSet<PropertyDefinition> allPropertiesInternal;
	}
}
