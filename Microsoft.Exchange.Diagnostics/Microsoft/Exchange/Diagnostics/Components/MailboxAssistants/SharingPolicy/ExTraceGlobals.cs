using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy
{
	// Token: 0x02000351 RID: 849
	public static class ExTraceGlobals
	{
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000520B5 File Offset: 0x000502B5
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

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x000520D3 File Offset: 0x000502D3
		public static Trace StoredSharingPolicyTracer
		{
			get
			{
				if (ExTraceGlobals.storedSharingPolicyTracer == null)
				{
					ExTraceGlobals.storedSharingPolicyTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.storedSharingPolicyTracer;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000520F1 File Offset: 0x000502F1
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

		// Token: 0x04001868 RID: 6248
		private static Guid componentGuid = new Guid("69F0F794-5732-43c6-A9AC-4393BEF4C477");

		// Token: 0x04001869 RID: 6249
		private static Trace assistantTracer = null;

		// Token: 0x0400186A RID: 6250
		private static Trace storedSharingPolicyTracer = null;

		// Token: 0x0400186B RID: 6251
		private static Trace pFDTracer = null;
	}
}
