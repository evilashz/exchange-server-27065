using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Security
{
	// Token: 0x020003A3 RID: 931
	public static class ExTraceGlobals
	{
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00058082 File Offset: 0x00056282
		public static Trace AuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.authenticationTracer == null)
				{
					ExTraceGlobals.authenticationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.authenticationTracer;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x000580A0 File Offset: 0x000562A0
		public static Trace PartnerTokenTracer
		{
			get
			{
				if (ExTraceGlobals.partnerTokenTracer == null)
				{
					ExTraceGlobals.partnerTokenTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.partnerTokenTracer;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x000580BE File Offset: 0x000562BE
		public static Trace X509CertAuthTracer
		{
			get
			{
				if (ExTraceGlobals.x509CertAuthTracer == null)
				{
					ExTraceGlobals.x509CertAuthTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.x509CertAuthTracer;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x000580DC File Offset: 0x000562DC
		public static Trace OAuthTracer
		{
			get
			{
				if (ExTraceGlobals.oAuthTracer == null)
				{
					ExTraceGlobals.oAuthTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.oAuthTracer;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000580FA File Offset: 0x000562FA
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00058118 File Offset: 0x00056318
		public static Trace BackendRehydrationTracer
		{
			get
			{
				if (ExTraceGlobals.backendRehydrationTracer == null)
				{
					ExTraceGlobals.backendRehydrationTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.backendRehydrationTracer;
			}
		}

		// Token: 0x04001B2B RID: 6955
		private static Guid componentGuid = new Guid("5ce0dc7e-6229-4bd9-9464-c92d7813bc3b");

		// Token: 0x04001B2C RID: 6956
		private static Trace authenticationTracer = null;

		// Token: 0x04001B2D RID: 6957
		private static Trace partnerTokenTracer = null;

		// Token: 0x04001B2E RID: 6958
		private static Trace x509CertAuthTracer = null;

		// Token: 0x04001B2F RID: 6959
		private static Trace oAuthTracer = null;

		// Token: 0x04001B30 RID: 6960
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B31 RID: 6961
		private static Trace backendRehydrationTracer = null;
	}
}
