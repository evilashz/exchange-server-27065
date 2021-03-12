using System;

namespace Microsoft.Exchange.Diagnostics.Components.HygieneForwardSync
{
	// Token: 0x020003C3 RID: 963
	public static class ExTraceGlobals
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0005951F File Offset: 0x0005771F
		public static Trace ServiceInstanceSyncTracer
		{
			get
			{
				if (ExTraceGlobals.serviceInstanceSyncTracer == null)
				{
					ExTraceGlobals.serviceInstanceSyncTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceInstanceSyncTracer;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005953D File Offset: 0x0005773D
		public static Trace FullTenantSyncTracer
		{
			get
			{
				if (ExTraceGlobals.fullTenantSyncTracer == null)
				{
					ExTraceGlobals.fullTenantSyncTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.fullTenantSyncTracer;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0005955B File Offset: 0x0005775B
		public static Trace PersistenceTracer
		{
			get
			{
				if (ExTraceGlobals.persistenceTracer == null)
				{
					ExTraceGlobals.persistenceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.persistenceTracer;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00059579 File Offset: 0x00057779
		public static Trace ProvisioningTracer
		{
			get
			{
				if (ExTraceGlobals.provisioningTracer == null)
				{
					ExTraceGlobals.provisioningTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.provisioningTracer;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00059597 File Offset: 0x00057797
		public static Trace MsoServicesTracer
		{
			get
			{
				if (ExTraceGlobals.msoServicesTracer == null)
				{
					ExTraceGlobals.msoServicesTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.msoServicesTracer;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x000595B5 File Offset: 0x000577B5
		public static Trace GlsServicesTracer
		{
			get
			{
				if (ExTraceGlobals.glsServicesTracer == null)
				{
					ExTraceGlobals.glsServicesTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.glsServicesTracer;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x000595D3 File Offset: 0x000577D3
		public static Trace DNSServicesTracer
		{
			get
			{
				if (ExTraceGlobals.dNSServicesTracer == null)
				{
					ExTraceGlobals.dNSServicesTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.dNSServicesTracer;
			}
		}

		// Token: 0x04001BCD RID: 7117
		private static Guid componentGuid = new Guid("952887AB-4E9A-4CF8-867F-3C5BD5BB67A3");

		// Token: 0x04001BCE RID: 7118
		private static Trace serviceInstanceSyncTracer = null;

		// Token: 0x04001BCF RID: 7119
		private static Trace fullTenantSyncTracer = null;

		// Token: 0x04001BD0 RID: 7120
		private static Trace persistenceTracer = null;

		// Token: 0x04001BD1 RID: 7121
		private static Trace provisioningTracer = null;

		// Token: 0x04001BD2 RID: 7122
		private static Trace msoServicesTracer = null;

		// Token: 0x04001BD3 RID: 7123
		private static Trace glsServicesTracer = null;

		// Token: 0x04001BD4 RID: 7124
		private static Trace dNSServicesTracer = null;
	}
}
