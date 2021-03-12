using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000042 RID: 66
	internal class SharedConfigurationTenantMonitorLogger : StxLoggerBase
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006973 File Offset: 0x00004B73
		internal override string LogTypeName
		{
			get
			{
				return "SharedConfigurationTenantMonitor Logs";
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000697A File Offset: 0x00004B7A
		internal override string LogComponent
		{
			get
			{
				return "SharedConfigurationTenantMonitor";
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006981 File Offset: 0x00004B81
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-SharedConfigurationTenantMonitor_";
			}
		}
	}
}
