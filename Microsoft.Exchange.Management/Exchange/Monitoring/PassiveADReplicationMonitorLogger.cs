using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000030 RID: 48
	internal class PassiveADReplicationMonitorLogger : StxLoggerBase
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006669 File Offset: 0x00004869
		internal override string LogTypeName
		{
			get
			{
				return "PassiveADReplicationMonitor Logs";
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006670 File Offset: 0x00004870
		internal override string LogComponent
		{
			get
			{
				return "PassiveADReplicationMonitor";
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006677 File Offset: 0x00004877
		internal override string LogFilePrefix
		{
			get
			{
				return "PassiveADReplicationMonitor_";
			}
		}
	}
}
