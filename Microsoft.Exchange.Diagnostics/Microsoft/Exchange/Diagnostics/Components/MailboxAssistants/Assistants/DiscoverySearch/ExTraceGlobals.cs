using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DiscoverySearch
{
	// Token: 0x0200034B RID: 843
	public static class ExTraceGlobals
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00051EC3 File Offset: 0x000500C3
		public static Trace DiscoverySearchEventBasedAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.discoverySearchEventBasedAssistantTracer == null)
				{
					ExTraceGlobals.discoverySearchEventBasedAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.discoverySearchEventBasedAssistantTracer;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00051EE1 File Offset: 0x000500E1
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x04001857 RID: 6231
		private static Guid componentGuid = new Guid("27e7e37b-4dd9-40a7-9a27-99c623e088f3");

		// Token: 0x04001858 RID: 6232
		private static Trace discoverySearchEventBasedAssistantTracer = null;

		// Token: 0x04001859 RID: 6233
		private static Trace pFDTracer = null;
	}
}
