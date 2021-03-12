using System;

namespace Microsoft.Exchange.Diagnostics.Components.MessageSecurity
{
	// Token: 0x02000320 RID: 800
	public static class ExTraceGlobals
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004C114 File Offset: 0x0004A314
		public static Trace EdgeCredentialServiceTracer
		{
			get
			{
				if (ExTraceGlobals.edgeCredentialServiceTracer == null)
				{
					ExTraceGlobals.edgeCredentialServiceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.edgeCredentialServiceTracer;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004C132 File Offset: 0x0004A332
		public static Trace TasksTracer
		{
			get
			{
				if (ExTraceGlobals.tasksTracer == null)
				{
					ExTraceGlobals.tasksTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.tasksTracer;
			}
		}

		// Token: 0x040015B9 RID: 5561
		private static Guid componentGuid = new Guid("C03489AA-60B3-4417-ABD0-67A4EA779033");

		// Token: 0x040015BA RID: 5562
		private static Trace edgeCredentialServiceTracer = null;

		// Token: 0x040015BB RID: 5563
		private static Trace tasksTracer = null;
	}
}
