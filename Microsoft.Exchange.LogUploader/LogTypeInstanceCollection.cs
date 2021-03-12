using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200002F RID: 47
	[ConfigurationCollection(typeof(LogTypeInstance), AddItemName = "Log")]
	internal class LogTypeInstanceCollection : ConfigurationElementCollection
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000B425 File Offset: 0x00009625
		public void Add(ConfigurationElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B42E File Offset: 0x0000962E
		protected override ConfigurationElement CreateNewElement()
		{
			return new LogTypeInstance();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B435 File Offset: 0x00009635
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LogTypeInstance)element).Instance;
		}
	}
}
