using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService
{
	// Token: 0x020003CC RID: 972
	public static class ExTraceGlobals
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00059AB2 File Offset: 0x00057CB2
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00059AD0 File Offset: 0x00057CD0
		public static Trace ClientTracer
		{
			get
			{
				if (ExTraceGlobals.clientTracer == null)
				{
					ExTraceGlobals.clientTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.clientTracer;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00059AEE File Offset: 0x00057CEE
		public static Trace WCFServiceEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.wCFServiceEndpointTracer == null)
				{
					ExTraceGlobals.wCFServiceEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.wCFServiceEndpointTracer;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00059B0C File Offset: 0x00057D0C
		public static Trace WCFClientEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.wCFClientEndpointTracer == null)
				{
					ExTraceGlobals.wCFClientEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.wCFClientEndpointTracer;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00059B2A File Offset: 0x00057D2A
		public static Trace TopologyTracer
		{
			get
			{
				if (ExTraceGlobals.topologyTracer == null)
				{
					ExTraceGlobals.topologyTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.topologyTracer;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00059B48 File Offset: 0x00057D48
		public static Trace SuitabilityVerifierTracer
		{
			get
			{
				if (ExTraceGlobals.suitabilityVerifierTracer == null)
				{
					ExTraceGlobals.suitabilityVerifierTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.suitabilityVerifierTracer;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00059B66 File Offset: 0x00057D66
		public static Trace DiscoveryTracer
		{
			get
			{
				if (ExTraceGlobals.discoveryTracer == null)
				{
					ExTraceGlobals.discoveryTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.discoveryTracer;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00059B84 File Offset: 0x00057D84
		public static Trace DnsTroubleshooterTracer
		{
			get
			{
				if (ExTraceGlobals.dnsTroubleshooterTracer == null)
				{
					ExTraceGlobals.dnsTroubleshooterTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.dnsTroubleshooterTracer;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00059BA2 File Offset: 0x00057DA2
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001BF9 RID: 7161
		private static Guid componentGuid = new Guid("23c20436-ba78-481d-99c3-5c523ff23024");

		// Token: 0x04001BFA RID: 7162
		private static Trace serviceTracer = null;

		// Token: 0x04001BFB RID: 7163
		private static Trace clientTracer = null;

		// Token: 0x04001BFC RID: 7164
		private static Trace wCFServiceEndpointTracer = null;

		// Token: 0x04001BFD RID: 7165
		private static Trace wCFClientEndpointTracer = null;

		// Token: 0x04001BFE RID: 7166
		private static Trace topologyTracer = null;

		// Token: 0x04001BFF RID: 7167
		private static Trace suitabilityVerifierTracer = null;

		// Token: 0x04001C00 RID: 7168
		private static Trace discoveryTracer = null;

		// Token: 0x04001C01 RID: 7169
		private static Trace dnsTroubleshooterTracer = null;

		// Token: 0x04001C02 RID: 7170
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
