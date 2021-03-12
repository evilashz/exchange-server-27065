using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200001E RID: 30
	public class AnalyzerConfigurationCollection : ConfigurationElementCollection<AnalyzerConfigurationElement>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00005B67 File Offset: 0x00003D67
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00005B6A File Offset: 0x00003D6A
		protected override string ElementName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005B71 File Offset: 0x00003D71
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element != null)
			{
				return ((AnalyzerConfigurationElement)element).Name;
			}
			return null;
		}
	}
}
