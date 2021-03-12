using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000011 RID: 17
	public class MonitorStateTransition
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00006EF4 File Offset: 0x000050F4
		public MonitorStateTransition(ServiceHealthStatus toState, TimeSpan transitionTimeout)
		{
			this.ToState = toState;
			this.TransitionTimeout = transitionTimeout;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006F0A File Offset: 0x0000510A
		public MonitorStateTransition(ServiceHealthStatus toState, int transitionTimeoutSeconds) : this(toState, TimeSpan.FromSeconds((double)transitionTimeoutSeconds))
		{
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006F1A File Offset: 0x0000511A
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00006F22 File Offset: 0x00005122
		public ServiceHealthStatus ToState { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006F2B File Offset: 0x0000512B
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00006F33 File Offset: 0x00005133
		public TimeSpan TransitionTimeout { get; private set; }
	}
}
