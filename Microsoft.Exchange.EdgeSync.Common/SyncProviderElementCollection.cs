using System;
using System.Configuration;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000018 RID: 24
	[ConfigurationCollection(typeof(SyncProviderElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	internal class SyncProviderElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003363 File Offset: 0x00001563
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003366 File Offset: 0x00001566
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SyncProviderElementCollection.properties;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000336D File Offset: 0x0000156D
		public void Add(ConfigurationElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003376 File Offset: 0x00001576
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000337E File Offset: 0x0000157E
		protected override ConfigurationElement CreateNewElement()
		{
			return new SyncProviderElement();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003385 File Offset: 0x00001585
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as SyncProviderElement).Name;
		}

		// Token: 0x0400004C RID: 76
		private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
