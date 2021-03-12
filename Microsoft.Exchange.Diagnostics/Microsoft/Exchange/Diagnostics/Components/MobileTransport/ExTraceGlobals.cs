using System;

namespace Microsoft.Exchange.Diagnostics.Components.MobileTransport
{
	// Token: 0x020003AD RID: 941
	public static class ExTraceGlobals
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0005894D File Offset: 0x00056B4D
		public static Trace XsoTracer
		{
			get
			{
				if (ExTraceGlobals.xsoTracer == null)
				{
					ExTraceGlobals.xsoTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.xsoTracer;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0005896B File Offset: 0x00056B6B
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00058989 File Offset: 0x00056B89
		public static Trace TransportTracer
		{
			get
			{
				if (ExTraceGlobals.transportTracer == null)
				{
					ExTraceGlobals.transportTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.transportTracer;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000589A7 File Offset: 0x00056BA7
		public static Trace SessionTracer
		{
			get
			{
				if (ExTraceGlobals.sessionTracer == null)
				{
					ExTraceGlobals.sessionTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.sessionTracer;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000589C5 File Offset: 0x00056BC5
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000589E3 File Offset: 0x00056BE3
		public static Trace ApplicationlogicTracer
		{
			get
			{
				if (ExTraceGlobals.applicationlogicTracer == null)
				{
					ExTraceGlobals.applicationlogicTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.applicationlogicTracer;
			}
		}

		// Token: 0x04001B6E RID: 7022
		private static Guid componentGuid = new Guid("344A3E26-44B9-45b3-B5EC-623311EAA0AA");

		// Token: 0x04001B6F RID: 7023
		private static Trace xsoTracer = null;

		// Token: 0x04001B70 RID: 7024
		private static Trace coreTracer = null;

		// Token: 0x04001B71 RID: 7025
		private static Trace transportTracer = null;

		// Token: 0x04001B72 RID: 7026
		private static Trace sessionTracer = null;

		// Token: 0x04001B73 RID: 7027
		private static Trace serviceTracer = null;

		// Token: 0x04001B74 RID: 7028
		private static Trace applicationlogicTracer = null;
	}
}
