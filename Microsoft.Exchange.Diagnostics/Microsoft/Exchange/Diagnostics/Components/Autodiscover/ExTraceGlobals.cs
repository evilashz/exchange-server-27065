using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Autodiscover
{
	// Token: 0x02000317 RID: 791
	public static class ExTraceGlobals
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0004AE33 File Offset: 0x00049033
		public static Trace FrameworkTracer
		{
			get
			{
				if (ExTraceGlobals.frameworkTracer == null)
				{
					ExTraceGlobals.frameworkTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.frameworkTracer;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0004AE51 File Offset: 0x00049051
		public static Trace OutlookProviderTracer
		{
			get
			{
				if (ExTraceGlobals.outlookProviderTracer == null)
				{
					ExTraceGlobals.outlookProviderTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.outlookProviderTracer;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004AE6F File Offset: 0x0004906F
		public static Trace MobileSyncProviderTracer
		{
			get
			{
				if (ExTraceGlobals.mobileSyncProviderTracer == null)
				{
					ExTraceGlobals.mobileSyncProviderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.mobileSyncProviderTracer;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004AE8D File Offset: 0x0004908D
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0004AEAB File Offset: 0x000490AB
		public static Trace AuthMetadataTracer
		{
			get
			{
				if (ExTraceGlobals.authMetadataTracer == null)
				{
					ExTraceGlobals.authMetadataTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.authMetadataTracer;
			}
		}

		// Token: 0x04001531 RID: 5425
		private static Guid componentGuid = new Guid("B3E33516-3A9E-4fba-8469-A88ECCCCDCD1");

		// Token: 0x04001532 RID: 5426
		private static Trace frameworkTracer = null;

		// Token: 0x04001533 RID: 5427
		private static Trace outlookProviderTracer = null;

		// Token: 0x04001534 RID: 5428
		private static Trace mobileSyncProviderTracer = null;

		// Token: 0x04001535 RID: 5429
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001536 RID: 5430
		private static Trace authMetadataTracer = null;
	}
}
