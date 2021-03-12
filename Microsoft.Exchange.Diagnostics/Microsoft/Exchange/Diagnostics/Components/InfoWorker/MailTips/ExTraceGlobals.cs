using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips
{
	// Token: 0x02000358 RID: 856
	public static class ExTraceGlobals
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00052324 File Offset: 0x00050524
		public static Trace GetMailTipsConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.getMailTipsConfigurationTracer == null)
				{
					ExTraceGlobals.getMailTipsConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.getMailTipsConfigurationTracer;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00052342 File Offset: 0x00050542
		public static Trace GetMailTipsTracer
		{
			get
			{
				if (ExTraceGlobals.getMailTipsTracer == null)
				{
					ExTraceGlobals.getMailTipsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.getMailTipsTracer;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00052360 File Offset: 0x00050560
		public static Trace GroupMetricsTracer
		{
			get
			{
				if (ExTraceGlobals.groupMetricsTracer == null)
				{
					ExTraceGlobals.groupMetricsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.groupMetricsTracer;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0005237E File Offset: 0x0005057E
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

		// Token: 0x0400187D RID: 6269
		private static Guid componentGuid = new Guid("EF265C98-7258-4d64-B449-75B576D9A678");

		// Token: 0x0400187E RID: 6270
		private static Trace getMailTipsConfigurationTracer = null;

		// Token: 0x0400187F RID: 6271
		private static Trace getMailTipsTracer = null;

		// Token: 0x04001880 RID: 6272
		private static Trace groupMetricsTracer = null;

		// Token: 0x04001881 RID: 6273
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
