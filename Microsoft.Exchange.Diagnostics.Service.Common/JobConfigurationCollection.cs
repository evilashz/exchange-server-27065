using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000021 RID: 33
	public class JobConfigurationCollection : ConfigurationElementCollection<JobConfigurationElement>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005E37 File Offset: 0x00004037
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005E3A File Offset: 0x0000403A
		protected override string ElementName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005E41 File Offset: 0x00004041
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element != null)
			{
				return ((JobConfigurationElement)element).Name;
			}
			return null;
		}
	}
}
