using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F0 RID: 240
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class SimpleConfigurationAttribute : Attribute
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x0001C95C File Offset: 0x0001AB5C
		internal SimpleConfigurationAttribute(string configurationName) : this(configurationName, configurationName)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001C968 File Offset: 0x0001AB68
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		internal string ConfigurationName
		{
			get
			{
				return this.configurationName;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		internal string ConfigurationRootNodeName
		{
			get
			{
				return this.configurationRootNodeName;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		internal ulong RequiredMask
		{
			get
			{
				return this.requiredMask;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		internal IEnumerable<SimpleConfigurationPropertyAttribute> GetPropertyCollection()
		{
			return this.propertyTable.Values;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001CA08 File Offset: 0x0001AC08
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

		// Token: 0x0600089C RID: 2204 RVA: 0x0001CA80 File Offset: 0x0001AC80
		internal SimpleConfigurationPropertyAttribute TryGetProperty(string propertyName)
		{
			SimpleConfigurationPropertyAttribute result = null;
			if (this.propertyTable.TryGetValue(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000558 RID: 1368
		private readonly string configurationName;

		// Token: 0x04000559 RID: 1369
		private readonly string configurationRootNodeName;

		// Token: 0x0400055A RID: 1370
		private ulong requiredMask;

		// Token: 0x0400055B RID: 1371
		private int propertyCount;

		// Token: 0x0400055C RID: 1372
		private Dictionary<string, SimpleConfigurationPropertyAttribute> propertyTable = new Dictionary<string, SimpleConfigurationPropertyAttribute>();
	}
}
