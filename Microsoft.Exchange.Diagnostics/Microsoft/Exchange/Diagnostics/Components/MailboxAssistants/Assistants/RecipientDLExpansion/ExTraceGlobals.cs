using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x0200034A RID: 842
	public static class ExTraceGlobals
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00051E46 File Offset: 0x00050046
		public static Trace RecipientDLExpansionEventBasedAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.recipientDLExpansionEventBasedAssistantTracer == null)
				{
					ExTraceGlobals.recipientDLExpansionEventBasedAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.recipientDLExpansionEventBasedAssistantTracer;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00051E64 File Offset: 0x00050064
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

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00051E82 File Offset: 0x00050082
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001853 RID: 6227
		private static Guid componentGuid = new Guid("06B6CA05-D4A0-4C19-B79E-24695A8E212D");

		// Token: 0x04001854 RID: 6228
		private static Trace recipientDLExpansionEventBasedAssistantTracer = null;

		// Token: 0x04001855 RID: 6229
		private static Trace pFDTracer = null;

		// Token: 0x04001856 RID: 6230
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
