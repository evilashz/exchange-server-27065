using System;
using Microsoft.Exchange.Diagnostics.Components.MExRuntime;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000079 RID: 121
	internal sealed class Dispatcher : IDispatcher
	{
		// Token: 0x060003BB RID: 955 RVA: 0x000126F0 File Offset: 0x000108F0
		public Dispatcher(IRuntimeSettings settings, AgentRecord[] agentsInDefaultOrder, string parentName)
		{
			string text = string.IsNullOrEmpty(parentName) ? null : (parentName + ".");
			text += this.GetHashCode().ToString();
			this.InstanceNameFormatted = string.Format("[{0}][{1}] ", "MExDispatcher", text);
			this.settings = settings;
			this.clonedExecutionEntry = -1;
			this.executionChain = Dispatcher.BuildAgentList(agentsInDefaultOrder);
			this.watchdog = new ExecutionMonitor(this.settings.MonitoringOptions.AgentExecutionLimitInMilliseconds, this);
			ExTraceGlobals.InitializeTracer.Information((long)this.GetHashCode(), this.InstanceNameFormatted + "created");
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060003BC RID: 956 RVA: 0x0001279C File Offset: 0x0001099C
		// (remove) Token: 0x060003BD RID: 957 RVA: 0x000127D4 File Offset: 0x000109D4
		public event AgentInvokeStartHandler OnAgentInvokeStart;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060003BE RID: 958 RVA: 0x0001280C File Offset: 0x00010A0C
		// (remove) Token: 0x060003BF RID: 959 RVA: 0x00012844 File Offset: 0x00010A44
		public event AgentInvokeReturnsHandler OnAgentInvokeReturns;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060003C0 RID: 960 RVA: 0x0001287C File Offset: 0x00010A7C
		// (remove) Token: 0x060003C1 RID: 961 RVA: 0x000128B4 File Offset: 0x00010AB4
		public event AgentInvokeEndHandler OnAgentInvokeEnd;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060003C2 RID: 962 RVA: 0x000128EC File Offset: 0x00010AEC
		// (remove) Token: 0x060003C3 RID: 963 RVA: 0x00012924 File Offset: 0x00010B24
		public event AgentInvokeScheduledHandler OnAgentInvokeScheduled;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060003C4 RID: 964 RVA: 0x0001295C File Offset: 0x00010B5C
		// (remove) Token: 0x060003C5 RID: 965 RVA: 0x00012994 File Offset: 0x00010B94
		public event AgentInvokeResumedHandler OnAgentInvokeResumed;

		// Token: 0x060003C6 RID: 966 RVA: 0x000129CC File Offset: 0x00010BCC
		public void Invoke(MExSession session)
		{
			string eventTopic = session.EventTopic;
			AgentRecord agentRecord;
			if (session.CurrentAgent == null)
			{
				agentRecord = this.executionChain;
				if (this.clonedTopic != null && this.clonedExecutionEntry >= 0 && this.clonedTopic == eventTopic)
				{
					ExTraceGlobals.DispatchTracer.TraceDebug<string, int>((long)this.GetHashCode(), this.InstanceNameFormatted + "restore clone state for '{0}' at index {1}", eventTopic, this.clonedExecutionEntry);
					for (int i = 0; i < this.clonedExecutionEntry; i++)
					{
						if (agentRecord == null)
						{
							ExTraceGlobals.DispatchTracer.TraceError<int>((long)this.GetHashCode(), this.InstanceNameFormatted + "restore clone state failed at index {0}", i);
							break;
						}
						agentRecord = agentRecord.Next;
					}
				}
			}
			else
			{
				agentRecord = session.NextAgent;
			}
			session.IsAsyncAgent = false;
			while (agentRecord != null)
			{
				session.CurrentAgent = agentRecord;
				if (agentRecord.Enabled)
				{
					Agent instance = agentRecord.Instance;
					instance.Session = session;
					instance.Synchronous = true;
					instance.EventTopic = eventTopic;
					Delegate @delegate = (Delegate)instance.Handlers[eventTopic];
					if (@delegate != null)
					{
						ExTraceGlobals.DispatchTracer.Information<string, string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "dispatching event '{0}' to {1} ({2})", eventTopic, instance.GetType().FullName, instance.GetHashCode().ToString());
						session.IsAsyncAgent = false;
						if (this.OnAgentInvokeStart != null)
						{
							this.OnAgentInvokeStart(this, session);
						}
						bool flag = true;
						try
						{
							session.StartCpuTracking(instance.Name);
							instance.Invoke(eventTopic, session.CurrentEventSource, session.CurrentEventArgs);
							flag = false;
						}
						finally
						{
							session.StopCpuTracking();
							if (flag)
							{
								session.NextAgent = null;
								session.ExecutionCompleted();
								session.CleanupCpuTracker();
								ExTraceGlobals.DispatchTracer.TraceDebug<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "agent {0} crashed during event '{1}'", instance.GetType().FullName, eventTopic);
							}
						}
						ExTraceGlobals.DispatchTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "event '{0}' handled by {1} {2}synchronously", eventTopic, instance.GetType().FullName, session.IsAsyncAgent ? "a" : string.Empty);
						if (this.OnAgentInvokeReturns != null)
						{
							this.OnAgentInvokeReturns(this, session);
						}
						session.NextAgent = agentRecord.Next;
						if (session.IsStatusHalt)
						{
							this.AgentInvokeCompleted(session);
							session.NextAgent = null;
							break;
						}
						if (session.IsAsyncAgent)
						{
							if (!session.IsSyncInvoke)
							{
								ExTraceGlobals.DispatchTracer.Information((long)this.GetHashCode(), this.InstanceNameFormatted + "async agent pending");
								break;
							}
							session.Wait();
							this.AgentInvokeCompleted(session);
						}
						else
						{
							this.AgentInvokeCompleted(session);
						}
					}
					else if (this.settings.MonitoringOptions.MessageSnapshotEnabled)
					{
						instance.Invoke(eventTopic, session.CurrentEventSource, session.CurrentEventArgs);
					}
				}
				agentRecord = agentRecord.Next;
			}
			if (agentRecord == null || (session.NextAgent == null && !session.IsAsyncAgent))
			{
				ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "dispatching of event '{0}' completed", eventTopic);
				session.ExecutionCompleted();
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012CEC File Offset: 0x00010EEC
		public void AgentInvokeCompleted(MExSession session)
		{
			if (this.OnAgentInvokeEnd != null)
			{
				this.OnAgentInvokeEnd(this, session);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012D03 File Offset: 0x00010F03
		public void AgentInvokeScheduled(MExSession session)
		{
			if (this.OnAgentInvokeScheduled != null)
			{
				this.OnAgentInvokeScheduled(this, session);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012D1A File Offset: 0x00010F1A
		public void AgentInvokeResumed(MExSession session)
		{
			if (this.OnAgentInvokeResumed != null)
			{
				this.OnAgentInvokeResumed(this, session);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012D31 File Offset: 0x00010F31
		public void Shutdown()
		{
			ExTraceGlobals.ShutdownTracer.TraceDebug((long)this.GetHashCode(), this.InstanceNameFormatted + "shutdonw");
			this.watchdog.Shutdown();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012D5F File Offset: 0x00010F5F
		public bool HasHandler(string eventTopic)
		{
			return this.executionChain != null;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012D6D File Offset: 0x00010F6D
		public void SetCloneState(string eventTopic, int firstAgentIndex)
		{
			this.clonedTopic = eventTopic;
			this.clonedExecutionEntry = firstAgentIndex;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012D80 File Offset: 0x00010F80
		public int GetAgentIndex(AgentRecord agentEntry)
		{
			int num = 0;
			AgentRecord next = this.executionChain;
			while (next != null && next != agentEntry)
			{
				num++;
				next = next.Next;
			}
			ExTraceGlobals.DispatchTracer.Information<string, int, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "agent '{0}' has index {1} ({2})", (agentEntry == null) ? "null" : agentEntry.Name, num, (agentEntry == null || next == null) ? "not found" : "found");
			return num;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private static AgentRecord BuildAgentList(AgentRecord[] agentsInDefaultOrder)
		{
			AgentRecord agentRecord = null;
			AgentRecord agentRecord2 = null;
			for (int i = 0; i < agentsInDefaultOrder.Length; i++)
			{
				AgentRecord agentRecord3 = (AgentRecord)agentsInDefaultOrder[i].Clone();
				if (agentRecord == null)
				{
					agentRecord = agentRecord3;
					agentRecord2 = agentRecord3;
				}
				else
				{
					agentRecord2.Next = agentRecord3;
					agentRecord2 = agentRecord3;
				}
			}
			return agentRecord;
		}

		// Token: 0x0400047A RID: 1146
		private readonly string InstanceNameFormatted;

		// Token: 0x0400047B RID: 1147
		private readonly AgentRecord executionChain;

		// Token: 0x0400047C RID: 1148
		private IRuntimeSettings settings;

		// Token: 0x0400047D RID: 1149
		private string clonedTopic;

		// Token: 0x0400047E RID: 1150
		private int clonedExecutionEntry;

		// Token: 0x0400047F RID: 1151
		private ExecutionMonitor watchdog;
	}
}
