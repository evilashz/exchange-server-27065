using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.HygieneData
{
	// Token: 0x020003C1 RID: 961
	public static class ExTraceGlobals
	{
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00059425 File Offset: 0x00057625
		public static Trace WebstoreProviderTracer
		{
			get
			{
				if (ExTraceGlobals.webstoreProviderTracer == null)
				{
					ExTraceGlobals.webstoreProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.webstoreProviderTracer;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00059443 File Offset: 0x00057643
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00059461 File Offset: 0x00057661
		public static Trace DomainSessionTracer
		{
			get
			{
				if (ExTraceGlobals.domainSessionTracer == null)
				{
					ExTraceGlobals.domainSessionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.domainSessionTracer;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0005947F File Offset: 0x0005767F
		public static Trace GLSQueryTracer
		{
			get
			{
				if (ExTraceGlobals.gLSQueryTracer == null)
				{
					ExTraceGlobals.gLSQueryTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.gLSQueryTracer;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0005949D File Offset: 0x0005769D
		public static Trace WebServiceProviderTracer
		{
			get
			{
				if (ExTraceGlobals.webServiceProviderTracer == null)
				{
					ExTraceGlobals.webServiceProviderTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.webServiceProviderTracer;
			}
		}

		// Token: 0x04001BC5 RID: 7109
		private static Guid componentGuid = new Guid("4B65DA35-2EAC-4452-B7B7-375D986BCA91");

		// Token: 0x04001BC6 RID: 7110
		private static Trace webstoreProviderTracer = null;

		// Token: 0x04001BC7 RID: 7111
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001BC8 RID: 7112
		private static Trace domainSessionTracer = null;

		// Token: 0x04001BC9 RID: 7113
		private static Trace gLSQueryTracer = null;

		// Token: 0x04001BCA RID: 7114
		private static Trace webServiceProviderTracer = null;
	}
}
