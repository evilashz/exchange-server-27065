using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServicesServerTasks
{
	// Token: 0x02000336 RID: 822
	public static class ExTraceGlobals
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004F1BF File Offset: 0x0004D3BF
		public static Trace TaskTracer
		{
			get
			{
				if (ExTraceGlobals.taskTracer == null)
				{
					ExTraceGlobals.taskTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.taskTracer;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0004F1DD File Offset: 0x0004D3DD
		public static Trace NonTaskTracer
		{
			get
			{
				if (ExTraceGlobals.nonTaskTracer == null)
				{
					ExTraceGlobals.nonTaskTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.nonTaskTracer;
			}
		}

		// Token: 0x04001714 RID: 5908
		private static Guid componentGuid = new Guid("DD7D3371-4EDD-4645-9BA9-F0532EA8C214");

		// Token: 0x04001715 RID: 5909
		private static Trace taskTracer = null;

		// Token: 0x04001716 RID: 5910
		private static Trace nonTaskTracer = null;
	}
}
