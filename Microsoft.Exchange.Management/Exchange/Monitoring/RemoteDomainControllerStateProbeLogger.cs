using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000032 RID: 50
	internal class RemoteDomainControllerStateProbeLogger : StxLoggerBase
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000066A3 File Offset: 0x000048A3
		internal override string LogTypeName
		{
			get
			{
				return "RemoteDomainControllerStateProbeLogger Logs";
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000066AA File Offset: 0x000048AA
		internal override string LogComponent
		{
			get
			{
				return "RemoteDomainControllerStateProbeLogger";
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000066B1 File Offset: 0x000048B1
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-RemoteDomainControllerStateProbeLogger_";
			}
		}
	}
}
