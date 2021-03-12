using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Compliance
{
	// Token: 0x02000409 RID: 1033
	public static class ExTraceGlobals
	{
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0005D2F2 File Offset: 0x0005B4F2
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005D310 File Offset: 0x0005B510
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0005D32E File Offset: 0x0005B52E
		public static Trace ViewProviderTracer
		{
			get
			{
				if (ExTraceGlobals.viewProviderTracer == null)
				{
					ExTraceGlobals.viewProviderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.viewProviderTracer;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0005D34C File Offset: 0x0005B54C
		public static Trace DataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.dataProviderTracer == null)
				{
					ExTraceGlobals.dataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.dataProviderTracer;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0005D36A File Offset: 0x0005B56A
		public static Trace ViewTracer
		{
			get
			{
				if (ExTraceGlobals.viewTracer == null)
				{
					ExTraceGlobals.viewTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.viewTracer;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005D388 File Offset: 0x0005B588
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005D3A6 File Offset: 0x0005B5A6
		public static Trace ComplianceServiceTracer
		{
			get
			{
				if (ExTraceGlobals.complianceServiceTracer == null)
				{
					ExTraceGlobals.complianceServiceTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.complianceServiceTracer;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
		public static Trace TaskDistributionSystemTracer
		{
			get
			{
				if (ExTraceGlobals.taskDistributionSystemTracer == null)
				{
					ExTraceGlobals.taskDistributionSystemTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.taskDistributionSystemTracer;
			}
		}

		// Token: 0x04001DA1 RID: 7585
		private static Guid componentGuid = new Guid("3719A9EF-E0BD-45DF-9B58-B36C0C2ECF0E");

		// Token: 0x04001DA2 RID: 7586
		private static Trace generalTracer = null;

		// Token: 0x04001DA3 RID: 7587
		private static Trace configurationTracer = null;

		// Token: 0x04001DA4 RID: 7588
		private static Trace viewProviderTracer = null;

		// Token: 0x04001DA5 RID: 7589
		private static Trace dataProviderTracer = null;

		// Token: 0x04001DA6 RID: 7590
		private static Trace viewTracer = null;

		// Token: 0x04001DA7 RID: 7591
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001DA8 RID: 7592
		private static Trace complianceServiceTracer = null;

		// Token: 0x04001DA9 RID: 7593
		private static Trace taskDistributionSystemTracer = null;
	}
}
