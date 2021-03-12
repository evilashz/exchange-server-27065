using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants
{
	// Token: 0x02000344 RID: 836
	public static class ExTraceGlobals
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0005165E File Offset: 0x0004F85E
		public static Trace AssistantBaseTracer
		{
			get
			{
				if (ExTraceGlobals.assistantBaseTracer == null)
				{
					ExTraceGlobals.assistantBaseTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantBaseTracer;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0005167C File Offset: 0x0004F87C
		public static Trace ExchangeServerTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeServerTracer == null)
				{
					ExTraceGlobals.exchangeServerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.exchangeServerTracer;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0005169A File Offset: 0x0004F89A
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000516B8 File Offset: 0x0004F8B8
		public static Trace ProvisioningAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.provisioningAssistantTracer == null)
				{
					ExTraceGlobals.provisioningAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.provisioningAssistantTracer;
			}
		}

		// Token: 0x04001819 RID: 6169
		private static Guid componentGuid = new Guid("1A6DC046-AE90-4c5a-AEEA-2CB240A1A620");

		// Token: 0x0400181A RID: 6170
		private static Trace assistantBaseTracer = null;

		// Token: 0x0400181B RID: 6171
		private static Trace exchangeServerTracer = null;

		// Token: 0x0400181C RID: 6172
		private static Trace pFDTracer = null;

		// Token: 0x0400181D RID: 6173
		private static Trace provisioningAssistantTracer = null;
	}
}
