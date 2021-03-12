using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder
{
	// Token: 0x02000352 RID: 850
	public static class ExTraceGlobals
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00052132 File Offset: 0x00050332
		public static Trace AssistantTracer
		{
			get
			{
				if (ExTraceGlobals.assistantTracer == null)
				{
					ExTraceGlobals.assistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantTracer;
			}
		}

		// Token: 0x0400186C RID: 6252
		private static Guid componentGuid = new Guid("376A5667-2C9B-4A3C-AC51-6C504750183E");

		// Token: 0x0400186D RID: 6253
		private static Trace assistantTracer = null;
	}
}
