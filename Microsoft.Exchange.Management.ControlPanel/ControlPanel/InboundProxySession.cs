using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000393 RID: 915
	internal sealed class InboundProxySession : LocalSession
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x00094AA3 File Offset: 0x00092CA3
		private InboundProxySession(RbacContext context) : base(context, InboundProxySession.sessionPerfCounters, InboundProxySession.esoSessionPerfCounters)
		{
			this.originalCallerName = context.Settings.InboundProxyCallerName;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x00094AC8 File Offset: 0x00092CC8
		protected override void WriteInitializationLog()
		{
			ExTraceGlobals.RBACTracer.TraceInformation<InboundProxySession, string>(0, 0L, "{0} created to handle calls from {1}.", this, this.originalCallerName);
			EcpEventLogConstants.Tuple_InboundProxySessionInitialize.LogEvent(new object[]
			{
				base.NameForEventLog,
				this.originalCallerName
			});
		}

		// Token: 0x04002392 RID: 9106
		private static PerfCounterGroup sessionsCounters = new PerfCounterGroup(EcpPerfCounters.InboundProxySessions, EcpPerfCounters.InboundProxySessionsPeak, EcpPerfCounters.InboundProxySessionsTotal);

		// Token: 0x04002393 RID: 9107
		private static PerfCounterGroup requestsCounters = new PerfCounterGroup(EcpPerfCounters.InboundProxyRequests, EcpPerfCounters.InboundProxyRequestsPeak, EcpPerfCounters.InboundProxyRequestsTotal);

		// Token: 0x04002394 RID: 9108
		private static PerfCounterGroup esoSessionsCounters = new PerfCounterGroup(EcpPerfCounters.EsoInboundProxySessions, EcpPerfCounters.EsoInboundProxySessionsPeak, EcpPerfCounters.EsoInboundProxySessionsTotal);

		// Token: 0x04002395 RID: 9109
		private static PerfCounterGroup esoRequestsCounters = new PerfCounterGroup(EcpPerfCounters.EsoInboundProxyRequests, EcpPerfCounters.EsoInboundProxyRequestsPeak, EcpPerfCounters.EsoInboundProxyRequestsTotal);

		// Token: 0x04002396 RID: 9110
		private static SessionPerformanceCounters sessionPerfCounters = new SessionPerformanceCounters(InboundProxySession.sessionsCounters, InboundProxySession.requestsCounters);

		// Token: 0x04002397 RID: 9111
		private static EsoSessionPerformanceCounters esoSessionPerfCounters = new EsoSessionPerformanceCounters(InboundProxySession.sessionsCounters, InboundProxySession.requestsCounters, InboundProxySession.esoSessionsCounters, InboundProxySession.esoRequestsCounters);

		// Token: 0x04002398 RID: 9112
		private string originalCallerName;

		// Token: 0x02000394 RID: 916
		public sealed class ProxyLogonNeededFactory : RbacSession.Factory
		{
			// Token: 0x060030CA RID: 12490 RVA: 0x00094BB7 File Offset: 0x00092DB7
			public ProxyLogonNeededFactory(RbacContext context) : base(context)
			{
			}

			// Token: 0x060030CB RID: 12491 RVA: 0x00094BC0 File Offset: 0x00092DC0
			protected override bool CanCreateSession()
			{
				return base.Settings.IsInboundProxyRequest && !base.Settings.IsProxyLogon;
			}

			// Token: 0x060030CC RID: 12492 RVA: 0x00094BDF File Offset: 0x00092DDF
			protected override RbacSession CreateNewSession()
			{
				Util.EndResponse(HttpContext.Current.Response, (HttpStatusCode)441);
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000395 RID: 917
		public new sealed class Factory : RbacSession.Factory
		{
			// Token: 0x060030CD RID: 12493 RVA: 0x00094BFA File Offset: 0x00092DFA
			public Factory(RbacContext context) : base(context)
			{
			}

			// Token: 0x060030CE RID: 12494 RVA: 0x00094C03 File Offset: 0x00092E03
			protected override bool CanCreateSession()
			{
				return base.Settings.IsProxyLogon;
			}

			// Token: 0x060030CF RID: 12495 RVA: 0x00094C10 File Offset: 0x00092E10
			protected override RbacSession CreateNewSession()
			{
				return new InboundProxySession(base.Context);
			}
		}
	}
}
