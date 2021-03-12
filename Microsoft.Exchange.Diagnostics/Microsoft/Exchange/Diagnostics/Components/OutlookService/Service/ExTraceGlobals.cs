using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.OutlookService.Service
{
	// Token: 0x020003AB RID: 939
	public static class ExTraceGlobals
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00058853 File Offset: 0x00056A53
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00058871 File Offset: 0x00056A71
		public static Trace FrameworkTracer
		{
			get
			{
				if (ExTraceGlobals.frameworkTracer == null)
				{
					ExTraceGlobals.frameworkTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.frameworkTracer;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005888F File Offset: 0x00056A8F
		public static Trace FeaturesTracer
		{
			get
			{
				if (ExTraceGlobals.featuresTracer == null)
				{
					ExTraceGlobals.featuresTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.featuresTracer;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000588AD File Offset: 0x00056AAD
		public static Trace StorageNotificationSubscriptionTracer
		{
			get
			{
				if (ExTraceGlobals.storageNotificationSubscriptionTracer == null)
				{
					ExTraceGlobals.storageNotificationSubscriptionTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.storageNotificationSubscriptionTracer;
			}
		}

		// Token: 0x04001B66 RID: 7014
		private static Guid componentGuid = new Guid("33858cdd-8b16-4201-8490-dc180f17036e");

		// Token: 0x04001B67 RID: 7015
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B68 RID: 7016
		private static Trace frameworkTracer = null;

		// Token: 0x04001B69 RID: 7017
		private static Trace featuresTracer = null;

		// Token: 0x04001B6A RID: 7018
		private static Trace storageNotificationSubscriptionTracer = null;
	}
}
