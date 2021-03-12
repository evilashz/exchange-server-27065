using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003A RID: 58
	internal class TopologyServiceStxLogger : StxLoggerBase
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000678B File Offset: 0x0000498B
		internal override string LogTypeName
		{
			get
			{
				return "TopologyService Logs";
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006792 File Offset: 0x00004992
		internal override string LogComponent
		{
			get
			{
				return "TopologyService";
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006799 File Offset: 0x00004999
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-TopologyService_";
			}
		}
	}
}
