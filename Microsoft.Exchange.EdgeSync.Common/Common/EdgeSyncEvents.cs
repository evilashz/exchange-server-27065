using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x0200001A RID: 26
	internal static class EdgeSyncEvents
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000034F4 File Offset: 0x000016F4
		public static ExEventLog Log
		{
			get
			{
				return EdgeSyncEvents.log;
			}
		}

		// Token: 0x04000052 RID: 82
		private static readonly ExEventLog log = new ExEventLog(new Guid("{8169CAF8-E6F1-480b-9700-39478DEA1FD5}"), "MSExchange EdgeSync");
	}
}
