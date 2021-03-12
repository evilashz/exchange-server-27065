using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000040 RID: 64
	internal class ForwardFullSyncStxLogger : StxLoggerBase
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006939 File Offset: 0x00004B39
		internal override string LogTypeName
		{
			get
			{
				return "ForwardFullSyncProbe Logs";
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006940 File Offset: 0x00004B40
		internal override string LogComponent
		{
			get
			{
				return "ForwardSyncFullProbe";
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006947 File Offset: 0x00004B47
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ForwardFullSync_";
			}
		}
	}
}
