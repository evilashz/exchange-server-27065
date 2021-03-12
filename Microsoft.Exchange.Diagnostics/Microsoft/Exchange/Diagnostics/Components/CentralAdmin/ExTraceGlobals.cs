using System;

namespace Microsoft.Exchange.Diagnostics.Components.CentralAdmin
{
	// Token: 0x0200039F RID: 927
	public static class ExTraceGlobals
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00057D1A File Offset: 0x00055F1A
		public static Trace CentralAdminServiceTracer
		{
			get
			{
				if (ExTraceGlobals.centralAdminServiceTracer == null)
				{
					ExTraceGlobals.centralAdminServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.centralAdminServiceTracer;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00057D38 File Offset: 0x00055F38
		public static Trace MOMConnectorTracer
		{
			get
			{
				if (ExTraceGlobals.mOMConnectorTracer == null)
				{
					ExTraceGlobals.mOMConnectorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mOMConnectorTracer;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00057D56 File Offset: 0x00055F56
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x00057D74 File Offset: 0x00055F74
		public static Trace UtilitiesTracer
		{
			get
			{
				if (ExTraceGlobals.utilitiesTracer == null)
				{
					ExTraceGlobals.utilitiesTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.utilitiesTracer;
			}
		}

		// Token: 0x04001B11 RID: 6929
		private static Guid componentGuid = new Guid("e84c009b-68df-4514-a1f4-498aab784af1");

		// Token: 0x04001B12 RID: 6930
		private static Trace centralAdminServiceTracer = null;

		// Token: 0x04001B13 RID: 6931
		private static Trace mOMConnectorTracer = null;

		// Token: 0x04001B14 RID: 6932
		private static Trace commonTracer = null;

		// Token: 0x04001B15 RID: 6933
		private static Trace utilitiesTracer = null;
	}
}
