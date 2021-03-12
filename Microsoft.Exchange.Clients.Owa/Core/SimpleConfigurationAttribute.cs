using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200024F RID: 591
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class SimpleConfigurationAttribute : Attribute
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x00079F01 File Offset: 0x00078101
		internal SimpleConfigurationAttribute(string configurationName) : this(configurationName, configurationName)
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00079F0C File Offset: 0x0007810C
		internal SimpleConfigurationAttribute(string configurationName, string configurationRootNodeName)
		{
			if (configurationName == null)
			{
				throw new ArgumentNullException("configurationName");
			}
			if (configurationName.Length == 0)
			{
				throw new ArgumentException("configurationName is empty", "configurationName");
			}
			if (configurationRootNodeName == null)
			{
				throw new ArgumentNullException("ConfigurationRootNodeName");
			}
			if (configurationRootNodeName.Length == 0)
			{
				throw new ArgumentException("ConfigurationRootNodeName is empty", "ConfigurationRootNodeName");
			}
			this.configurationName = configurationName;
			this.configurationRootNodeName = configurationRootNodeName;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00079F84 File Offset: 0x00078184
		internal void AddProperty(SimpleConfigurationPropertyAttribute property)
		{
			if (this.propertyCount >= 64)
			{
				throw new OwaNotSupportedException(string.Format("SimpleConfiguration doesn't support types with more than {0} properties", 64));
			}
			ulong num = 1UL << this.propertyCount;
			property.PropertyMask = num;
			if (property.IsRequired)
			{
				this.requiredMask |= num;
			}
			this.propertyTable.Add(property.Name, property);
			this.propertyCount++;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00079FFC File Offset: 0x000781FC
		internal SimpleConfigurationPropertyAttribute TryGetProperty(string propertyName)
		{
			SimpleConfigurationPropertyAttribute result = null;
			if (this.propertyTable.TryGetValue(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0007A01E File Offset: 0x0007821E
		internal IEnumerable<SimpleConfigurationPropertyAttribute> GetPropertyCollection()
		{
			return this.propertyTable.Values;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0007A02B File Offset: 0x0007822B
		internal string ConfigurationName
		{
			get
			{
				return this.configurationName;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0007A033 File Offset: 0x00078233
		internal string ConfigurationRootNodeName
		{
			get
			{
				return this.configurationRootNodeName;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0007A03B File Offset: 0x0007823B
		internal ulong RequiredMask
		{
			get
			{
				return this.requiredMask;
			}
		}

		// Token: 0x04000DA2 RID: 3490
		private string configurationName;

		// Token: 0x04000DA3 RID: 3491
		private string configurationRootNodeName;

		// Token: 0x04000DA4 RID: 3492
		private ulong requiredMask;

		// Token: 0x04000DA5 RID: 3493
		private int propertyCount;

		// Token: 0x04000DA6 RID: 3494
		private Dictionary<string, SimpleConfigurationPropertyAttribute> propertyTable = new Dictionary<string, SimpleConfigurationPropertyAttribute>();
	}
}
