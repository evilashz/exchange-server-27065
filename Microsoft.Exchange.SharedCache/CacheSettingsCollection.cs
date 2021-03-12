using System;
using System.Configuration;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200000C RID: 12
	[ConfigurationCollection(typeof(CacheSettings))]
	internal class CacheSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003214 File Offset: 0x00001414
		protected override string ElementName
		{
			get
			{
				return "Caches";
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000321B File Offset: 0x0000141B
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000321E File Offset: 0x0000141E
		public override bool IsReadOnly()
		{
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003221 File Offset: 0x00001421
		protected override ConfigurationElement CreateNewElement()
		{
			return new CacheSettings();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003228 File Offset: 0x00001428
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CacheSettings)element).Guid;
		}
	}
}
