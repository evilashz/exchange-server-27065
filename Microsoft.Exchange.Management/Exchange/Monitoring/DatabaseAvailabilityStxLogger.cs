using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000044 RID: 68
	internal class DatabaseAvailabilityStxLogger : StxLoggerBase
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000069AD File Offset: 0x00004BAD
		internal override string LogTypeName
		{
			get
			{
				return "DatabaseAvailability Logs";
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000069B4 File Offset: 0x00004BB4
		internal override string LogComponent
		{
			get
			{
				return "Store";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000069BB File Offset: 0x00004BBB
		internal override string LogFilePrefix
		{
			get
			{
				return "DatabaseAvailability_";
			}
		}
	}
}
