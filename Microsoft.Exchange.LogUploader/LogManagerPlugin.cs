using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000032 RID: 50
	internal class LogManagerPlugin : ConfigurationElement
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B54A File Offset: 0x0000974A
		[ConfigurationProperty("Name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000B55C File Offset: 0x0000975C
		[ConfigurationProperty("MonitorCreator", IsRequired = true)]
		public string MonitorCreatorClassName
		{
			get
			{
				return (string)base["MonitorCreator"];
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B56E File Offset: 0x0000976E
		[ConfigurationProperty("Assembly", IsRequired = true)]
		public string AssemblyName
		{
			get
			{
				return (string)base["Assembly"];
			}
		}
	}
}
