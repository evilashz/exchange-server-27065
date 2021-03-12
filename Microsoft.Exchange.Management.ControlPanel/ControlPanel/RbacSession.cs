using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200038F RID: 911
	internal abstract class RbacSession : RbacPrincipal, IRbacSession, IPrincipal, IIdentity
	{
		// Token: 0x06003092 RID: 12434 RVA: 0x00093D40 File Offset: 0x00091F40
		protected RbacSession(RbacContext context, SessionPerformanceCounters sessionPerfCounters, EsoSessionPerformanceCounters esoSessionPerfCounters) : base(context.Roles, context.Settings.CacheKey)
		{
			this.Context = context;
			this.Settings = context.Settings;
			this.sessionPerfCounters = (context.Settings.IsExplicitSignOn ? esoSessionPerfCounters : sessionPerfCounters);
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x00093D8E File Offset: 0x00091F8E
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x00093D96 File Offset: 0x00091F96
		internal RbacSettings Settings { get; private set; }

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x00093D9F File Offset: 0x00091F9F
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x00093DA7 File Offset: 0x00091FA7
		internal RbacContext Context { get; private set; }

		// Token: 0x06003097 RID: 12439 RVA: 0x00093DB0 File Offset: 0x00091FB0
		public override void SetCurrentThreadPrincipal()
		{
			base.SetCurrentThreadPrincipal();
			if (base.UserCulture != null)
			{
				Thread.CurrentThread.CurrentCulture = base.UserCulture;
				Thread.CurrentThread.CurrentUICulture = base.UserCulture;
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x00093DE0 File Offset: 0x00091FE0
		public virtual void Initialize()
		{
			this.SetCurrentThreadPrincipal();
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x00093DE8 File Offset: 0x00091FE8
		public virtual void SessionStart()
		{
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Starting RBAC session for {0}", base.NameForEventLog);
			this.WriteInitializationLog();
			RbacSession.totalSessionsCounter.Increment();
			this.sessionPerfCounters.IncreaseSessionCounter();
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x00093E1D File Offset: 0x0009201D
		public virtual void SessionEnd()
		{
			this.sessionPerfCounters.DecreaseSessionCounter();
			RbacSession.totalSessionsCounter.Decrement();
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Ending RBAC session for {0}", base.NameForEventLog);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x00093E4C File Offset: 0x0009204C
		public virtual void RequestReceived()
		{
			this.sessionPerfCounters.IncreaseRequestCounter();
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Request received from {0}", base.NameForEventLog);
			this.SetCurrentThreadPrincipal();
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x00093E77 File Offset: 0x00092077
		public virtual void RequestCompleted()
		{
			this.sessionPerfCounters.DecreaseRequestCounter();
			ExTraceGlobals.RBACTracer.TraceInformation<string>(0, 0L, "Request completed for {0}", base.NameForEventLog);
			base.RbacConfiguration.TroubleshootingContext.TraceOperationCompletedAndUpdateContext();
		}

		// Token: 0x0600309D RID: 12445
		protected abstract void WriteInitializationLog();

		// Token: 0x0400237D RID: 9085
		private static PerfCounterGroup totalSessionsCounter = new PerfCounterGroup(EcpPerfCounters.RbacSessions, EcpPerfCounters.RbacSessionsPeak, EcpPerfCounters.RbacSessionsTotal);

		// Token: 0x0400237E RID: 9086
		private SessionPerformanceCounters sessionPerfCounters;

		// Token: 0x02000390 RID: 912
		public abstract class Factory
		{
			// Token: 0x0600309F RID: 12447 RVA: 0x00093EC7 File Offset: 0x000920C7
			protected Factory(RbacContext context)
			{
				this.Context = context;
			}

			// Token: 0x17001F3C RID: 7996
			// (get) Token: 0x060030A0 RID: 12448 RVA: 0x00093ED6 File Offset: 0x000920D6
			// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00093EDE File Offset: 0x000920DE
			private protected RbacContext Context { protected get; private set; }

			// Token: 0x17001F3D RID: 7997
			// (get) Token: 0x060030A2 RID: 12450 RVA: 0x00093EE7 File Offset: 0x000920E7
			protected RbacSettings Settings
			{
				get
				{
					return this.Context.Settings;
				}
			}

			// Token: 0x060030A3 RID: 12451
			protected abstract bool CanCreateSession();

			// Token: 0x060030A4 RID: 12452
			protected abstract RbacSession CreateNewSession();

			// Token: 0x060030A5 RID: 12453 RVA: 0x00093EF4 File Offset: 0x000920F4
			public RbacSession CreateSession()
			{
				ExTraceGlobals.RBACTracer.TraceInformation<RbacSession.Factory, string>(0, 0L, "Testing if {0} can create a session for {1}.", this, this.Settings.UserName);
				if (this.CanCreateSession())
				{
					ExTraceGlobals.RBACTracer.TraceInformation<RbacSession.Factory, string>(0, 0L, "{0} accepted the request to create a session for {1}.", this, this.Settings.UserName);
					RbacSession rbacSession = this.CreateNewSession();
					ExTraceGlobals.RBACTracer.TraceInformation<RbacSession, string>(0, 0L, "Initializing {0} session for {1}.", rbacSession, this.Settings.UserName);
					rbacSession.Initialize();
					return rbacSession;
				}
				ExTraceGlobals.RBACTracer.TraceInformation<RbacSession.Factory, string>(0, 0L, "{0} declined the request to create a session for {1}.", this, this.Settings.UserName);
				return null;
			}
		}
	}
}
