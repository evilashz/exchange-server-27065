using System;

namespace Microsoft.Exchange.Diagnostics.Components.MapiHttpClient
{
	// Token: 0x020003A7 RID: 935
	public static class ExTraceGlobals
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000585CF File Offset: 0x000567CF
		public static Trace ClientAsyncOperationTracer
		{
			get
			{
				if (ExTraceGlobals.clientAsyncOperationTracer == null)
				{
					ExTraceGlobals.clientAsyncOperationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.clientAsyncOperationTracer;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000585ED File Offset: 0x000567ED
		public static Trace ClientSessionContextTracer
		{
			get
			{
				if (ExTraceGlobals.clientSessionContextTracer == null)
				{
					ExTraceGlobals.clientSessionContextTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.clientSessionContextTracer;
			}
		}

		// Token: 0x04001B52 RID: 6994
		private static Guid componentGuid = new Guid("377C2744-C481-4C11-9B16-82C9C5E65362");

		// Token: 0x04001B53 RID: 6995
		private static Trace clientAsyncOperationTracer = null;

		// Token: 0x04001B54 RID: 6996
		private static Trace clientSessionContextTracer = null;
	}
}
