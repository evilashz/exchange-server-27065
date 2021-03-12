using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000F RID: 15
	internal interface IManagementClassHelper
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006E RID: 110
		DateTime LocalBootTime { get; }

		// Token: 0x0600006F RID: 111
		DateTime GetBootTime(AmServerName machineName);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112
		string LocalComputerFqdn { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000071 RID: 113
		string LocalDomainName { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114
		string LocalMachineName { get; }
	}
}
