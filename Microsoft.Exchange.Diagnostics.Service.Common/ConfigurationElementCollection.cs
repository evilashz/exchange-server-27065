using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200001D RID: 29
	public abstract class ConfigurationElementCollection<T> : ConfigurationElementCollection where T : ConfigurationElement, new()
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00005B32 File Offset: 0x00003D32
		public ConfigurationElementCollection()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006B RID: 107
		public abstract override ConfigurationElementCollectionType CollectionType { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006C RID: 108
		protected abstract override string ElementName { get; }

		// Token: 0x1700000D RID: 13
		public T this[int index]
		{
			get
			{
				return (T)((object)base.BaseGet(index));
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005B48 File Offset: 0x00003D48
		protected override ConfigurationElement CreateNewElement()
		{
			return Activator.CreateInstance<T>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005B54 File Offset: 0x00003D54
		protected override object GetElementKey(ConfigurationElement element)
		{
			return this.GetElementKey((T)((object)element));
		}
	}
}
