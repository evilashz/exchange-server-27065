using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration
{
	// Token: 0x02000214 RID: 532
	internal struct MRSAvailabilityData
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000640FE File Offset: 0x000622FE
		public string Server
		{
			get
			{
				return LocalServer.GetServer().Name;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0006410A File Offset: 0x0006230A
		public int Version
		{
			get
			{
				return ServerVersion.InstalledVersion.ToInt();
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00064116 File Offset: 0x00062316
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0006411E File Offset: 0x0006231E
		public string EventContext { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00064127 File Offset: 0x00062327
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0006412F File Offset: 0x0006232F
		public string EventData { get; set; }
	}
}
