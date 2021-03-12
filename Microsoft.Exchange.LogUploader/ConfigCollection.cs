using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200002A RID: 42
	[ConfigurationCollection(typeof(ConfigInstance), AddItemName = "Config")]
	internal class ConfigCollection : ConfigurationElementCollection
	{
		// Token: 0x06000217 RID: 535 RVA: 0x0000AEEA File Offset: 0x000090EA
		public ConfigCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000AEF7 File Offset: 0x000090F7
		public void Add(ConfigurationElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000AF00 File Offset: 0x00009100
		public ConfigInstance Get(object key)
		{
			return (ConfigInstance)base.BaseGet(key);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000AF0E File Offset: 0x0000910E
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConfigInstance();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AF15 File Offset: 0x00009115
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigInstance)element).Name;
		}
	}
}
