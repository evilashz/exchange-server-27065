using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200002F RID: 47
	internal class PassiveReplicationMonitorLogger : StxLoggerBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000664C File Offset: 0x0000484C
		internal override string LogTypeName
		{
			get
			{
				return "PassiveReplicationMonitor Logs";
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006653 File Offset: 0x00004853
		internal override string LogComponent
		{
			get
			{
				return "PassiveReplicationMonitor";
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000665A File Offset: 0x0000485A
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-PassiveReplicationMonitor_";
			}
		}
	}
}
