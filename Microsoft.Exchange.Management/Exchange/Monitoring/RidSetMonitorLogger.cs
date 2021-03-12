using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000034 RID: 52
	internal class RidSetMonitorLogger : StxLoggerBase
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000066DD File Offset: 0x000048DD
		internal override string LogTypeName
		{
			get
			{
				return "RidSetMonitor Logs";
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000066E4 File Offset: 0x000048E4
		internal override string LogComponent
		{
			get
			{
				return "RidSetMonitor";
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000066EB File Offset: 0x000048EB
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-RidSetMonitor_";
			}
		}
	}
}
