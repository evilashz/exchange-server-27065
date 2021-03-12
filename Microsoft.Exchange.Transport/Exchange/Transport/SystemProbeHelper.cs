using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A2 RID: 418
	internal static class SystemProbeHelper
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000498BC File Offset: 0x00047ABC
		public static SystemProbeTrace MessageResubmissionTracer
		{
			get
			{
				return SystemProbeHelper.messageResubmissionTracer;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000498C3 File Offset: 0x00047AC3
		public static SystemProbeTrace ShadowRedundancyTracer
		{
			get
			{
				return SystemProbeHelper.shadowRedundancyTracer;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x000498CA File Offset: 0x00047ACA
		public static SystemProbeTrace SmtpReceiveTracer
		{
			get
			{
				return SystemProbeHelper.smtpReceiveTracer;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000498D1 File Offset: 0x00047AD1
		public static SystemProbeTrace SmtpSendTracer
		{
			get
			{
				return SystemProbeHelper.smtpSendTracer;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000498D8 File Offset: 0x00047AD8
		public static SystemProbeTrace EtrTracer
		{
			get
			{
				return SystemProbeHelper.etrTracer;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000498DF File Offset: 0x00047ADF
		public static SystemProbeTrace SchedulerTracer
		{
			get
			{
				return SystemProbeHelper.schedulerTracer;
			}
		}

		// Token: 0x04000988 RID: 2440
		private static SystemProbeTrace messageResubmissionTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.MessageResubmissionTracer, "MessageResubmission");

		// Token: 0x04000989 RID: 2441
		private static SystemProbeTrace shadowRedundancyTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.ShadowRedundancyTracer, "ShadowRedundancy");

		// Token: 0x0400098A RID: 2442
		private static SystemProbeTrace smtpReceiveTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.SmtpReceiveTracer, "SmtpReceive");

		// Token: 0x0400098B RID: 2443
		private static SystemProbeTrace smtpSendTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.SmtpSendTracer, "SmtpSend");

		// Token: 0x0400098C RID: 2444
		private static SystemProbeTrace etrTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.MessagingPolicies.ExTraceGlobals.TransportRulesEngineTracer, "exchangeTransportRules");

		// Token: 0x0400098D RID: 2445
		private static SystemProbeTrace schedulerTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.SchedulerTracer, "categorizer");
	}
}
