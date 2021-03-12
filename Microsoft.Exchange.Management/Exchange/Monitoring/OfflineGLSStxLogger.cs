using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000047 RID: 71
	internal class OfflineGLSStxLogger : StxLoggerBase
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006A04 File Offset: 0x00004C04
		internal override string LogTypeName
		{
			get
			{
				return "OfflineGLS Logs";
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006A0B File Offset: 0x00004C0B
		internal override string LogComponent
		{
			get
			{
				return "OfflineGLS";
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006A12 File Offset: 0x00004C12
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-OfflineGLS_";
			}
		}
	}
}
