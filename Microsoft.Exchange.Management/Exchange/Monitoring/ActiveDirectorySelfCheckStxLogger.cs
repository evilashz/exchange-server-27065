using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000038 RID: 56
	internal class ActiveDirectorySelfCheckStxLogger : StxLoggerBase
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006751 File Offset: 0x00004951
		internal override string LogTypeName
		{
			get
			{
				return "ActiveDirectorySelfCheck Logs";
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006758 File Offset: 0x00004958
		internal override string LogComponent
		{
			get
			{
				return "ActiveDirectorySelfCheck";
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000675F File Offset: 0x0000495F
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ActiveDirectorySelfCheck_";
			}
		}
	}
}
