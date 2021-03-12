using System;

namespace Microsoft.Exchange.Diagnostics.Components.Groups
{
	// Token: 0x020003FA RID: 1018
	public static class ExTraceGlobals
	{
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005C0A2 File Offset: 0x0005A2A2
		public static Trace GroupNotificationStorageTracer
		{
			get
			{
				if (ExTraceGlobals.groupNotificationStorageTracer == null)
				{
					ExTraceGlobals.groupNotificationStorageTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.groupNotificationStorageTracer;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005C0C0 File Offset: 0x0005A2C0
		public static Trace UnseenItemsReaderTracer
		{
			get
			{
				if (ExTraceGlobals.unseenItemsReaderTracer == null)
				{
					ExTraceGlobals.unseenItemsReaderTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.unseenItemsReaderTracer;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0005C0DE File Offset: 0x0005A2DE
		public static Trace COWGroupMessageEscalationTracer
		{
			get
			{
				if (ExTraceGlobals.cOWGroupMessageEscalationTracer == null)
				{
					ExTraceGlobals.cOWGroupMessageEscalationTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.cOWGroupMessageEscalationTracer;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0005C0FC File Offset: 0x0005A2FC
		public static Trace COWGroupMessageWSPublishingTracer
		{
			get
			{
				if (ExTraceGlobals.cOWGroupMessageWSPublishingTracer == null)
				{
					ExTraceGlobals.cOWGroupMessageWSPublishingTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.cOWGroupMessageWSPublishingTracer;
			}
		}

		// Token: 0x04001D1A RID: 7450
		private static Guid componentGuid = new Guid("1E4EC963-CD8B-4D26-A28B-832E3EA645CA");

		// Token: 0x04001D1B RID: 7451
		private static Trace groupNotificationStorageTracer = null;

		// Token: 0x04001D1C RID: 7452
		private static Trace unseenItemsReaderTracer = null;

		// Token: 0x04001D1D RID: 7453
		private static Trace cOWGroupMessageEscalationTracer = null;

		// Token: 0x04001D1E RID: 7454
		private static Trace cOWGroupMessageWSPublishingTracer = null;
	}
}
