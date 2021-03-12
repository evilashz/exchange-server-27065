using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000035 RID: 53
	internal class RidMonitorLogger : StxLoggerBase
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000066FA File Offset: 0x000048FA
		internal override string LogTypeName
		{
			get
			{
				return "RidMonitor Logs";
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006701 File Offset: 0x00004901
		internal override string LogComponent
		{
			get
			{
				return "RidMonitor";
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006708 File Offset: 0x00004908
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-RidMonitor_";
			}
		}
	}
}
