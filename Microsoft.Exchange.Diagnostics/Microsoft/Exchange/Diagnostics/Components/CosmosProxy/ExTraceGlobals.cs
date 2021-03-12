using System;

namespace Microsoft.Exchange.Diagnostics.Components.CosmosProxy
{
	// Token: 0x020003FE RID: 1022
	public static class ExTraceGlobals
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		public static Trace SenderTracer
		{
			get
			{
				if (ExTraceGlobals.senderTracer == null)
				{
					ExTraceGlobals.senderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.senderTracer;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0005C3E6 File Offset: 0x0005A5E6
		public static Trace ReceiverTracer
		{
			get
			{
				if (ExTraceGlobals.receiverTracer == null)
				{
					ExTraceGlobals.receiverTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.receiverTracer;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0005C404 File Offset: 0x0005A604
		public static Trace DownloaderTracer
		{
			get
			{
				if (ExTraceGlobals.downloaderTracer == null)
				{
					ExTraceGlobals.downloaderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.downloaderTracer;
			}
		}

		// Token: 0x04001D32 RID: 7474
		private static Guid componentGuid = new Guid("18229B60-53AF-4337-9F63-BACE4AB588AD");

		// Token: 0x04001D33 RID: 7475
		private static Trace senderTracer = null;

		// Token: 0x04001D34 RID: 7476
		private static Trace receiverTracer = null;

		// Token: 0x04001D35 RID: 7477
		private static Trace downloaderTracer = null;
	}
}
