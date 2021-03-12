using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000174 RID: 372
	internal static class UmGlobals
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002B5EB File Offset: 0x000297EB
		public static ExEventLog ExEvent
		{
			get
			{
				return UmGlobals.eventLog;
			}
		}

		// Token: 0x0400064E RID: 1614
		internal const string EventSourceName = "MSExchange Unified Messaging";

		// Token: 0x0400064F RID: 1615
		private static ExEventLog eventLog = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchange Unified Messaging");
	}
}
