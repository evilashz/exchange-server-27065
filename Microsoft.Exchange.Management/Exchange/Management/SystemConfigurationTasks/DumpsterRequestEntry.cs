using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000896 RID: 2198
	[Serializable]
	public sealed class DumpsterRequestEntry
	{
		// Token: 0x06004D28 RID: 19752 RVA: 0x0013FD8E File Offset: 0x0013DF8E
		internal DumpsterRequestEntry(string server, DateTime startTime, DateTime endTime)
		{
			this.m_server = server;
			this.m_startTime = startTime;
			this.m_endTime = endTime;
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x0013FDAB File Offset: 0x0013DFAB
		public string Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x0013FDB3 File Offset: 0x0013DFB3
		public DateTime StartTime
		{
			get
			{
				return this.m_startTime;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x0013FDBB File Offset: 0x0013DFBB
		public DateTime EndTime
		{
			get
			{
				return this.m_endTime;
			}
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013FDC3 File Offset: 0x0013DFC3
		public override string ToString()
		{
			return string.Format("{0}({1};{2})", this.m_server, this.m_startTime, this.m_endTime);
		}

		// Token: 0x04002E18 RID: 11800
		private const string ToStringFormatStr = "{0}({1};{2})";

		// Token: 0x04002E19 RID: 11801
		private readonly string m_server;

		// Token: 0x04002E1A RID: 11802
		private readonly DateTime m_startTime;

		// Token: 0x04002E1B RID: 11803
		private readonly DateTime m_endTime;
	}
}
