using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003AB RID: 939
	internal sealed class StandardSession : LocalSession
	{
		// Token: 0x06003182 RID: 12674 RVA: 0x00098B7F File Offset: 0x00096D7F
		private StandardSession(RbacContext context) : base(context, StandardSession.sessionPerfCounters, StandardSession.esoSessionPerfCounters)
		{
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00098B94 File Offset: 0x00096D94
		protected override void WriteInitializationLog()
		{
			EcpEventLogConstants.Tuple_StandardSessionInitialize.LogEvent(new object[]
			{
				base.NameForEventLog,
				EcpEventLogExtensions.GetFlightInfoForLog()
			});
		}

		// Token: 0x04002406 RID: 9222
		private static PerfCounterGroup sessionsCounters = new PerfCounterGroup(EcpPerfCounters.StandardSessions, EcpPerfCounters.StandardSessionsPeak, EcpPerfCounters.StandardSessionsTotal);

		// Token: 0x04002407 RID: 9223
		private static PerfCounterGroup requestsCounters = new PerfCounterGroup(EcpPerfCounters.StandardRequests, EcpPerfCounters.StandardRequestsPeak, EcpPerfCounters.StandardRequestsTotal);

		// Token: 0x04002408 RID: 9224
		private static PerfCounterGroup esoSessionsCounters = new PerfCounterGroup(EcpPerfCounters.EsoStandardSessions, EcpPerfCounters.EsoStandardSessionsPeak, EcpPerfCounters.EsoStandardSessionsTotal);

		// Token: 0x04002409 RID: 9225
		private static PerfCounterGroup esoRequestsCounters = new PerfCounterGroup(EcpPerfCounters.EsoStandardRequests, EcpPerfCounters.EsoStandardRequestsPeak, EcpPerfCounters.EsoStandardRequestsTotal);

		// Token: 0x0400240A RID: 9226
		private static SessionPerformanceCounters sessionPerfCounters = new SessionPerformanceCounters(StandardSession.sessionsCounters, StandardSession.requestsCounters);

		// Token: 0x0400240B RID: 9227
		private static EsoSessionPerformanceCounters esoSessionPerfCounters = new EsoSessionPerformanceCounters(StandardSession.sessionsCounters, StandardSession.requestsCounters, StandardSession.esoSessionsCounters, StandardSession.esoRequestsCounters);

		// Token: 0x020003AC RID: 940
		public new sealed class Factory : RbacSession.Factory
		{
			// Token: 0x06003185 RID: 12677 RVA: 0x00098C67 File Offset: 0x00096E67
			public Factory(RbacContext context) : base(context)
			{
			}

			// Token: 0x06003186 RID: 12678 RVA: 0x00098C70 File Offset: 0x00096E70
			protected override bool CanCreateSession()
			{
				return true;
			}

			// Token: 0x06003187 RID: 12679 RVA: 0x00098C73 File Offset: 0x00096E73
			protected override RbacSession CreateNewSession()
			{
				return new StandardSession(base.Context);
			}
		}
	}
}
