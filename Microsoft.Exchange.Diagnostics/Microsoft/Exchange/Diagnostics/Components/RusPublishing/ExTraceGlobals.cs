using System;

namespace Microsoft.Exchange.Diagnostics.Components.RusPublishing
{
	// Token: 0x020003F1 RID: 1009
	public static class ExTraceGlobals
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0005BC0B File Offset: 0x00059E0B
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0005BC29 File Offset: 0x00059E29
		public static Trace PublisherWebTracer
		{
			get
			{
				if (ExTraceGlobals.publisherWebTracer == null)
				{
					ExTraceGlobals.publisherWebTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.publisherWebTracer;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005BC47 File Offset: 0x00059E47
		public static Trace EngineUpdateDownloaderTracer
		{
			get
			{
				if (ExTraceGlobals.engineUpdateDownloaderTracer == null)
				{
					ExTraceGlobals.engineUpdateDownloaderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.engineUpdateDownloaderTracer;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0005BC65 File Offset: 0x00059E65
		public static Trace EngineUpdatePublisherTracer
		{
			get
			{
				if (ExTraceGlobals.engineUpdatePublisherTracer == null)
				{
					ExTraceGlobals.engineUpdatePublisherTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.engineUpdatePublisherTracer;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005BC83 File Offset: 0x00059E83
		public static Trace SignaturePollingJobTracer
		{
			get
			{
				if (ExTraceGlobals.signaturePollingJobTracer == null)
				{
					ExTraceGlobals.signaturePollingJobTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.signaturePollingJobTracer;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0005BCA1 File Offset: 0x00059EA1
		public static Trace EnginePackagingStepTracer
		{
			get
			{
				if (ExTraceGlobals.enginePackagingStepTracer == null)
				{
					ExTraceGlobals.enginePackagingStepTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.enginePackagingStepTracer;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0005BCBF File Offset: 0x00059EBF
		public static Trace EngineTestingStepTracer
		{
			get
			{
				if (ExTraceGlobals.engineTestingStepTracer == null)
				{
					ExTraceGlobals.engineTestingStepTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.engineTestingStepTracer;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0005BCDD File Offset: 0x00059EDD
		public static Trace EngineCodeSignStepTracer
		{
			get
			{
				if (ExTraceGlobals.engineCodeSignStepTracer == null)
				{
					ExTraceGlobals.engineCodeSignStepTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.engineCodeSignStepTracer;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0005BCFB File Offset: 0x00059EFB
		public static Trace EngineCleanUpStepTracer
		{
			get
			{
				if (ExTraceGlobals.engineCleanUpStepTracer == null)
				{
					ExTraceGlobals.engineCleanUpStepTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.engineCleanUpStepTracer;
			}
		}

		// Token: 0x04001CF5 RID: 7413
		private static Guid componentGuid = new Guid("534d6f5a-8ca8-4c44-abd7-481335889364");

		// Token: 0x04001CF6 RID: 7414
		private static Trace commonTracer = null;

		// Token: 0x04001CF7 RID: 7415
		private static Trace publisherWebTracer = null;

		// Token: 0x04001CF8 RID: 7416
		private static Trace engineUpdateDownloaderTracer = null;

		// Token: 0x04001CF9 RID: 7417
		private static Trace engineUpdatePublisherTracer = null;

		// Token: 0x04001CFA RID: 7418
		private static Trace signaturePollingJobTracer = null;

		// Token: 0x04001CFB RID: 7419
		private static Trace enginePackagingStepTracer = null;

		// Token: 0x04001CFC RID: 7420
		private static Trace engineTestingStepTracer = null;

		// Token: 0x04001CFD RID: 7421
		private static Trace engineCodeSignStepTracer = null;

		// Token: 0x04001CFE RID: 7422
		private static Trace engineCleanUpStepTracer = null;
	}
}
