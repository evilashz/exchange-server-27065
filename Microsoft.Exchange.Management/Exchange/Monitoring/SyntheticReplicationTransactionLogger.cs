using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200002E RID: 46
	internal class SyntheticReplicationTransactionLogger : StxLoggerBase
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000662F File Offset: 0x0000482F
		internal override string LogTypeName
		{
			get
			{
				return "SyntheticReplicationTransaction Logs";
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006636 File Offset: 0x00004836
		internal override string LogComponent
		{
			get
			{
				return "SyntheticReplicationTransaction";
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000663D File Offset: 0x0000483D
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-SyntheticReplicationTransaction_";
			}
		}
	}
}
