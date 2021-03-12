using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000041 RID: 65
	internal class TenantRelocationErrorLogger : StxLoggerBase
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006956 File Offset: 0x00004B56
		internal override string LogTypeName
		{
			get
			{
				return "TenantRelocationErrorMonitor Logs";
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000695D File Offset: 0x00004B5D
		internal override string LogComponent
		{
			get
			{
				return "TenantRelocationErrorMonitor";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006964 File Offset: 0x00004B64
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-TenantRelocationErrorMonitor_";
			}
		}
	}
}
