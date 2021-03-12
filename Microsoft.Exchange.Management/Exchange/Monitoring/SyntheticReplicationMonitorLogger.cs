using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200002D RID: 45
	internal class SyntheticReplicationMonitorLogger : StxLoggerBase
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006612 File Offset: 0x00004812
		internal override string LogTypeName
		{
			get
			{
				return "SyntheticReplicationMonitor Logs";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006619 File Offset: 0x00004819
		internal override string LogComponent
		{
			get
			{
				return "SyntheticReplicationMonitor";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006620 File Offset: 0x00004820
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-SyntheticReplicationMonitor_";
			}
		}
	}
}
