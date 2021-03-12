using System;

namespace Microsoft.Exchange.Diagnostics.Components.ForefrontQuarantine
{
	// Token: 0x020003EA RID: 1002
	public static class ExTraceGlobals
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0005B663 File Offset: 0x00059863
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0005B681 File Offset: 0x00059881
		public static Trace StoreTracer
		{
			get
			{
				if (ExTraceGlobals.storeTracer == null)
				{
					ExTraceGlobals.storeTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.storeTracer;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0005B69F File Offset: 0x0005989F
		public static Trace ManagerTracer
		{
			get
			{
				if (ExTraceGlobals.managerTracer == null)
				{
					ExTraceGlobals.managerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.managerTracer;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0005B6BD File Offset: 0x000598BD
		public static Trace CleanupTracer
		{
			get
			{
				if (ExTraceGlobals.cleanupTracer == null)
				{
					ExTraceGlobals.cleanupTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.cleanupTracer;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0005B6DB File Offset: 0x000598DB
		public static Trace SpamDigestWSTracer
		{
			get
			{
				if (ExTraceGlobals.spamDigestWSTracer == null)
				{
					ExTraceGlobals.spamDigestWSTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.spamDigestWSTracer;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0005B6F9 File Offset: 0x000598F9
		public static Trace SpamDigestCommonTracer
		{
			get
			{
				if (ExTraceGlobals.spamDigestCommonTracer == null)
				{
					ExTraceGlobals.spamDigestCommonTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.spamDigestCommonTracer;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0005B717 File Offset: 0x00059917
		public static Trace SpamDigestGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.spamDigestGeneratorTracer == null)
				{
					ExTraceGlobals.spamDigestGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.spamDigestGeneratorTracer;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005B735 File Offset: 0x00059935
		public static Trace SpamDigestBackgroundJobTracer
		{
			get
			{
				if (ExTraceGlobals.spamDigestBackgroundJobTracer == null)
				{
					ExTraceGlobals.spamDigestBackgroundJobTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.spamDigestBackgroundJobTracer;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0005B753 File Offset: 0x00059953
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x04001CCA RID: 7370
		private static Guid componentGuid = new Guid("10B884FD-372F-490D-A233-7C2C4CB8F104");

		// Token: 0x04001CCB RID: 7371
		private static Trace agentTracer = null;

		// Token: 0x04001CCC RID: 7372
		private static Trace storeTracer = null;

		// Token: 0x04001CCD RID: 7373
		private static Trace managerTracer = null;

		// Token: 0x04001CCE RID: 7374
		private static Trace cleanupTracer = null;

		// Token: 0x04001CCF RID: 7375
		private static Trace spamDigestWSTracer = null;

		// Token: 0x04001CD0 RID: 7376
		private static Trace spamDigestCommonTracer = null;

		// Token: 0x04001CD1 RID: 7377
		private static Trace spamDigestGeneratorTracer = null;

		// Token: 0x04001CD2 RID: 7378
		private static Trace spamDigestBackgroundJobTracer = null;

		// Token: 0x04001CD3 RID: 7379
		private static Trace commonTracer = null;
	}
}
