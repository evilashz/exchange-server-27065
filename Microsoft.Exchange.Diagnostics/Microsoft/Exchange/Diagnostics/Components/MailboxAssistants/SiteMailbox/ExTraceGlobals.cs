using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SiteMailbox
{
	// Token: 0x02000353 RID: 851
	public static class ExTraceGlobals
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00052167 File Offset: 0x00050367
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

		// Token: 0x0400186E RID: 6254
		private static Guid componentGuid = new Guid("aefa8e76-9e79-425e-83a7-85e60ee2970a");

		// Token: 0x0400186F RID: 6255
		private static Trace assistantTracer = null;
	}
}
