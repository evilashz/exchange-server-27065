using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000033 RID: 51
	[ConfigurationCollection(typeof(LogManagerPlugin), AddItemName = "Plugin")]
	internal class LogManagerPluginCollection : ConfigurationElementCollection
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000B588 File Offset: 0x00009788
		public LogManagerPluginCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B595 File Offset: 0x00009795
		public LogManagerPlugin Get(object key)
		{
			return (LogManagerPlugin)base.BaseGet(key);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B5A3 File Offset: 0x000097A3
		protected override ConfigurationElement CreateNewElement()
		{
			return new LogManagerPlugin();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B5AA File Offset: 0x000097AA
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LogManagerPlugin)element).Name;
		}
	}
}
