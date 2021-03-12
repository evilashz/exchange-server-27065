using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000010 RID: 16
	internal class MonitorStateMachine
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00006DB4 File Offset: 0x00004FB4
		internal MonitorStateMachine(MonitorStateTransition[] transitions)
		{
			this.GreenState = ServiceHealthStatus.Healthy;
			if (transitions != null && transitions.Length > 0)
			{
				for (int i = 1; i < transitions.Length; i++)
				{
					if (transitions[i].TransitionTimeout <= transitions[i - 1].TransitionTimeout)
					{
						throw new ArgumentException(string.Format("Transition timeout should be in increasing order. TransitionId={0}", i));
					}
				}
				this.transitions = transitions;
				this.IsEnabled = true;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006E22 File Offset: 0x00005022
		internal static MonitorStateTransition[] DefaultUnhealthyTransition
		{
			get
			{
				return MonitorStateMachine.defaultStateTransition;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006E29 File Offset: 0x00005029
		internal MonitorStateTransition[] Transitions
		{
			get
			{
				return this.transitions;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00006E31 File Offset: 0x00005031
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00006E39 File Offset: 0x00005039
		internal ServiceHealthStatus GreenState { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006E42 File Offset: 0x00005042
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00006E4A File Offset: 0x0000504A
		internal bool IsEnabled { get; private set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00006E54 File Offset: 0x00005054
		internal static MonitorStateTransition[] ConstructSimpleTransitions(int unhealthyTimeout, int unrecoverableTimeout)
		{
			return new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, unhealthyTimeout),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, unrecoverableTimeout)
			};
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006E89 File Offset: 0x00005089
		internal MonitorStateTransition GetTransitionInfo(int transitionId)
		{
			if (transitionId != -1 && transitionId < this.transitions.Length)
			{
				return this.transitions[transitionId];
			}
			throw new IndexOutOfRangeException("Transition id is out of range");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006EB0 File Offset: 0x000050B0
		internal int GetNextTransitionId(int currentTransitionId)
		{
			int num = currentTransitionId + 1;
			if (num >= this.transitions.Length)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x04000197 RID: 407
		internal const int InvalidTransitionId = -1;

		// Token: 0x04000198 RID: 408
		private static MonitorStateTransition[] defaultStateTransition = new MonitorStateTransition[]
		{
			new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
		};

		// Token: 0x04000199 RID: 409
		private MonitorStateTransition[] transitions;
	}
}
