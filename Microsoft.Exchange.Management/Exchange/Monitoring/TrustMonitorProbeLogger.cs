using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000033 RID: 51
	internal class TrustMonitorProbeLogger : StxLoggerBase
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000066C0 File Offset: 0x000048C0
		internal override string LogTypeName
		{
			get
			{
				return "TrustMonitorProbeLogger Logs";
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000066C7 File Offset: 0x000048C7
		internal override string LogComponent
		{
			get
			{
				return "TrustMonitorProbeLogger";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000066CE File Offset: 0x000048CE
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-TrustMonitorProbeLogger_";
			}
		}
	}
}
