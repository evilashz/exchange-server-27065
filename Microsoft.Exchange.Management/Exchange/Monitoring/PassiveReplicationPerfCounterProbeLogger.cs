using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000031 RID: 49
	internal class PassiveReplicationPerfCounterProbeLogger : StxLoggerBase
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006686 File Offset: 0x00004886
		internal override string LogTypeName
		{
			get
			{
				return "PassiveReplicationPerfCounterProbeLogger Logs";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000668D File Offset: 0x0000488D
		internal override string LogComponent
		{
			get
			{
				return "PassiveReplicationPerfCounterProbeLogger";
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006694 File Offset: 0x00004894
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-PassiveReplicationPerfCounterProbe_";
			}
		}
	}
}
