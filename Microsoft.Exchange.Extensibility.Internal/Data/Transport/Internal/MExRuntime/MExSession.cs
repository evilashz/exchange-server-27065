using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MExRuntime;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Extensibility.EventLog;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200008C RID: 140
	internal sealed class MExSession : IMExSession, IExecutionControl, ICloneable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00014D7E File Offset: 0x00012F7E
		internal MExSession(IRuntimeSettings settings, ICloneableInternal state, string name, string parentName) : this(settings, state, name, parentName, null, null, null)
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014D90 File Offset: 0x00012F90
		internal MExSession(IRuntimeSettings settings, ICloneableInternal state, string name, string parentName, Action startAsyncAgentCallback, Action completeAsyncAgentCallback, Func<bool> resumeAgentCallback) : this(settings, state, name, parentName, null, null, startAsyncAgentCallback, completeAsyncAgentCallback, resumeAgentCallback)
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014DB0 File Offset: 0x00012FB0
		private MExSession(IRuntimeSettings settings, ICloneableInternal state, string name, string parentName, AgentRecord[] agentsInDefaultOrder, AgentRecord[] publicAgentsInDefaultOrder, Action startAsyncAgentCallback, Action completeAsyncAgentCallback, Func<bool> resumeAgentCallback)
		{
			settings.AddSessionRef();
			this.settings = settings;
			this.hostState = state;
			this.name = name;
			this.parentName = parentName;
			this.id = this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			string str = string.IsNullOrEmpty(parentName) ? null : (parentName + ".");
			string arg = str + this.id;
			this.InstanceNameFormatted = string.Format("[{0}][{1}] ", "MExSession", arg);
			this.isCompleted = true;
			if (agentsInDefaultOrder == null)
			{
				agentsInDefaultOrder = this.settings.CreateDefaultAgentOrder();
				publicAgentsInDefaultOrder = new AgentRecord[this.settings.PublicAgentsInDefaultOrder.Length];
				int num = 0;
				for (int i = 0; i < agentsInDefaultOrder.Length; i++)
				{
					agentsInDefaultOrder[i].Instance = this.CreateAgentInstance(agentsInDefaultOrder[i]);
					agentsInDefaultOrder[i].Enabled = this.IsAgentEnabled(agentsInDefaultOrder[i]);
					if (!agentsInDefaultOrder[i].IsInternal)
					{
						publicAgentsInDefaultOrder[num] = agentsInDefaultOrder[i];
						num++;
					}
				}
			}
			if (ExTraceGlobals.DispatchTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				foreach (AgentRecord agentRecord in agentsInDefaultOrder)
				{
					if (agentRecord.Enabled)
					{
						ExTraceGlobals.DispatchTracer.TraceDebug<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "{0} ({1})", agentRecord.Instance.GetType().FullName, agentRecord.Instance.Id);
					}
				}
			}
			this.agentsInDefaultOrder = agentsInDefaultOrder;
			this.publicAgentsInDefaultOrder = publicAgentsInDefaultOrder;
			this.startAsyncAgentCallback = startAsyncAgentCallback;
			this.completeAsyncAgentCallback = completeAsyncAgentCallback;
			this.resumeAgentCallback = resumeAgentCallback;
			this.scheduleWorkDelegate = new MExSession.ScheduleWork(ThreadPool.QueueUserWorkItem);
			this.dispatcher = new Dispatcher(this.settings, this.agentsInDefaultOrder, arg);
			this.disposeTracker = DisposeTracker.Get<MExSession>(this);
			ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "created '{0}'", this.Name);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00014FC1 File Offset: 0x000131C1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00014FC9 File Offset: 0x000131C9
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00014FD1 File Offset: 0x000131D1
		public object CurrentEventSource
		{
			get
			{
				return this.currentEventSource;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00014FD9 File Offset: 0x000131D9
		public object CurrentEventArgs
		{
			get
			{
				return this.currentEventArgs;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00014FE1 File Offset: 0x000131E1
		public string ExecutingAgentName
		{
			get
			{
				if (this.currentAgent != null)
				{
					return this.currentAgent.Name;
				}
				return null;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00014FF8 File Offset: 0x000131F8
		public string OutstandingEventTopic
		{
			get
			{
				if (this.currentAgent != null)
				{
					return this.currentAgent.Instance.EventTopic;
				}
				return null;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00015014 File Offset: 0x00013214
		public long TotalProcessorTime
		{
			get
			{
				return (long)this.cpuTracker.TotalProcessorTime.TotalMilliseconds;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00015035 File Offset: 0x00013235
		public IDispatcher Dispatcher
		{
			get
			{
				return this.dispatcher;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001503D File Offset: 0x0001323D
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00015048 File Offset: 0x00013248
		public AgentRecord CurrentAgent
		{
			get
			{
				return this.currentAgent;
			}
			set
			{
				this.currentAgent = value;
				if (value != null)
				{
					if (string.IsNullOrEmpty(value.Name))
					{
						ExTraceGlobals.DispatchTracer.TraceDebug((long)this.GetHashCode(), "Agent name was not specified, using type instead.");
						this.lastAgentName = value.Type;
						return;
					}
					this.lastAgentName = value.Name;
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001509B File Offset: 0x0001329B
		public string LastAgentName
		{
			get
			{
				return this.lastAgentName;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000150A3 File Offset: 0x000132A3
		public string EventTopic
		{
			get
			{
				return this.eventTopic;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000150AB File Offset: 0x000132AB
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000150B3 File Offset: 0x000132B3
		internal bool IsAsyncAgent
		{
			get
			{
				return this.isAsyncAgent;
			}
			set
			{
				this.isAsyncAgent = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000150BC File Offset: 0x000132BC
		internal bool IsExecutionCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000150C6 File Offset: 0x000132C6
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000150CE File Offset: 0x000132CE
		internal long BeginInvokeTicks
		{
			get
			{
				return this.beginInvokeTicks;
			}
			set
			{
				this.beginInvokeTicks = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000150D7 File Offset: 0x000132D7
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000150DF File Offset: 0x000132DF
		internal long BeginSchedulingTicks
		{
			get
			{
				return this.beginSchedulingTicks;
			}
			set
			{
				this.beginSchedulingTicks = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000150E8 File Offset: 0x000132E8
		internal MExAsyncResult AsyncResult
		{
			get
			{
				return this.pendingResult;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000150F0 File Offset: 0x000132F0
		internal bool IsSyncInvoke
		{
			get
			{
				return this.isSyncInvoke;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000150F8 File Offset: 0x000132F8
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00015100 File Offset: 0x00013300
		internal AgentRecord NextAgent
		{
			get
			{
				return this.nextAgent;
			}
			set
			{
				this.nextAgent = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00015109 File Offset: 0x00013309
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00015111 File Offset: 0x00013311
		internal bool IsStatusHalt
		{
			get
			{
				return this.isStatusHalt;
			}
			set
			{
				this.isStatusHalt = value;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001511A File Offset: 0x0001331A
		public void StartCpuTracking(string agentName)
		{
			this.cpuTracker = CpuTracker.StartCpuTracking(agentName);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015128 File Offset: 0x00013328
		public void StopCpuTracking()
		{
			this.cpuTracker.End();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015135 File Offset: 0x00013335
		public void CleanupCpuTracker()
		{
			this.cpuTracker = null;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015140 File Offset: 0x00013340
		public void Invoke(string topic, object source, object e)
		{
			if (topic == null)
			{
				throw new ArgumentNullException("topic");
			}
			ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "sync dispatch event {0}", topic);
			if (this.closed)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			if (!this.dispatcher.HasHandler(topic))
			{
				ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "no handler for {0}", topic);
				return;
			}
			lock (this.syncRoot)
			{
				try
				{
					if (this.closed)
					{
						throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
					}
					this.BindContexts(source, e);
					this.eventTopic = topic;
					this.Invoke();
				}
				finally
				{
					this.UnbindContexts();
				}
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001522C File Offset: 0x0001342C
		public IAsyncResult BeginInvoke(string topic, object source, object e, AsyncCallback callback, object callbackState)
		{
			if (topic == null)
			{
				throw new ArgumentNullException("topic");
			}
			ExTraceGlobals.DispatchTracer.TraceDebug<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "async dispatch event {0} {1} callback", topic, (callback == null) ? "without" : "with");
			if (this.closed)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			MExAsyncResult result = new MExAsyncResult(callback, callbackState);
			if (!this.dispatcher.HasHandler(topic))
			{
				ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "no handler for {0}", topic);
				this.pendingResult = result;
				this.pendingResult.InvokeCompleted();
			}
			else
			{
				lock (this.syncRoot)
				{
					if (this.closed)
					{
						throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
					}
					this.BindContexts(source, e);
					this.eventTopic = topic;
					this.pendingResult = result;
					this.AsyncInvoke(null);
				}
			}
			return result;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00015338 File Offset: 0x00013538
		public void EndInvoke(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult != this.pendingResult)
			{
				throw new ArgumentException(MExRuntimeStrings.InvalidEndInvoke, "asyncResult");
			}
			ExTraceGlobals.DispatchTracer.TraceDebug<int>((long)this.GetHashCode(), this.InstanceNameFormatted + "async dispatch ended, async result {0}", asyncResult.GetHashCode());
			try
			{
				this.pendingResult.EndInvoke();
			}
			finally
			{
				this.pendingResult = null;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000153B8 File Offset: 0x000135B8
		public void HaltExecution()
		{
			if (this.closed)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			this.IsStatusHalt = true;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000153D4 File Offset: 0x000135D4
		public void OnStartAsyncAgent()
		{
			if (this.startAsyncAgentCallback != null)
			{
				this.startAsyncAgentCallback();
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000153E9 File Offset: 0x000135E9
		public void ResumeAgent()
		{
			if (this.resumeAgentCallback != null)
			{
				this.resumeAgentCallback();
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000153FF File Offset: 0x000135FF
		public AgentAsyncCallback GetAgentAsyncCallback()
		{
			if (this.closed)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			return this.GetAsyncCallbackHandler();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001541C File Offset: 0x0001361C
		public object Clone()
		{
			int agentIndex;
			MExSession mexSession;
			lock (this.syncRoot)
			{
				if (this.closed)
				{
					throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
				}
				agentIndex = this.dispatcher.GetAgentIndex(this.currentAgent);
				mexSession = new MExSession(this.settings, this.hostState, this.name, this.parentName, this.agentsInDefaultOrder, this.publicAgentsInDefaultOrder, this.startAsyncAgentCallback, this.completeAsyncAgentCallback, this.resumeAgentCallback);
				mexSession.SetCloneState(this.eventTopic, agentIndex);
			}
			ExTraceGlobals.DispatchTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), this.InstanceNameFormatted + "{0} cloned on event '{1}' with index {2}", mexSession.GetHashCode().ToString(CultureInfo.InvariantCulture), this.eventTopic, agentIndex);
			return mexSession;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015504 File Offset: 0x00013704
		public void Close()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.closed && !this.IsExecutionCompleted)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			lock (this.syncRoot)
			{
				if (this.closed)
				{
					return;
				}
				this.closed = true;
			}
			if (this.syncWaitHandle != null)
			{
				this.syncWaitHandle.Close();
			}
			if (this.dispatcher != null)
			{
				this.dispatcher.Shutdown();
			}
			this.settings.SaveAgentSubscription(this.agentsInDefaultOrder);
			foreach (AgentRecord agentRecord in this.agentsInDefaultOrder)
			{
				Agent instance = agentRecord.Instance;
				if (instance != null)
				{
					instance.Id = null;
				}
			}
			this.DisposeAgents(this.settings.DisposeAgents);
			this.agentsInDefaultOrder = null;
			this.publicAgentsInDefaultOrder = null;
			this.currentEventSource = null;
			this.currentEventArgs = null;
			this.dispatcher = null;
			this.hostState = null;
			this.nextAgent = null;
			this.currentAgent = null;
			this.pendingResult = null;
			this.syncWaitHandle = null;
			this.settings.ReleaseSessionRef();
			this.settings = null;
			ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "closed '{0}'", this.Name);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001567C File Offset: 0x0001387C
		private void DisposeAgents(bool disposeAllAgents)
		{
			foreach (AgentRecord agentRecord in this.agentsInDefaultOrder)
			{
				Agent instance = agentRecord.Instance;
				if (instance != null && (agentRecord.IsInternal || disposeAllAgents))
				{
					IDisposable disposable = instance as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000156CC File Offset: 0x000138CC
		internal void SetCloneState(string topic, int firstAgentIndex)
		{
			this.dispatcher.SetCloneState(topic, firstAgentIndex);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000156DB File Offset: 0x000138DB
		internal void Wait()
		{
			if (this.isSyncInvoke)
			{
				this.syncWaitHandle.WaitOne();
				this.syncWaitHandle.Reset();
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000156FD File Offset: 0x000138FD
		internal void ExecutionCompleted()
		{
			this.isCompleted = true;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00015708 File Offset: 0x00013908
		private void Invoke()
		{
			ExTraceGlobals.DispatchTracer.TraceDebug<string>((long)this.GetHashCode(), this.InstanceNameFormatted + "dispatching event {0}", this.eventTopic);
			this.dispatcher.Invoke(this);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015740 File Offset: 0x00013940
		private void ResumeExecution()
		{
			ExTraceGlobals.DispatchTracer.TraceDebug<bool>((long)this.GetHashCode(), this.InstanceNameFormatted + "execution resumed ({0})", this.IsStatusHalt);
			if (this.IsStatusHalt)
			{
				this.NextAgent = null;
			}
			this.dispatcher.AgentInvokeScheduled(this);
			if (!this.scheduleWorkDelegate(new WaitCallback(this.ResumeExecutionWork)))
			{
				ExTraceGlobals.DispatchTracer.TraceError((long)this.GetHashCode(), this.InstanceNameFormatted + "scheduling workItem failed");
				throw new InsufficientMemoryException(MExRuntimeStrings.TooManyInvokes);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000157D4 File Offset: 0x000139D4
		private void ResumeExecutionWork(object state)
		{
			this.ResumeAgent();
			this.dispatcher.AgentInvokeResumed(this);
			this.AsyncInvoke(state);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000157F0 File Offset: 0x000139F0
		private void AsyncInvoke(object state)
		{
			lock (this.syncRoot)
			{
				if (this.pendingResult == null)
				{
					MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentCompletedTwice, null, new object[]
					{
						this.lastAgentName,
						this.eventTopic
					});
					throw new InvalidOperationException(string.Format("{0} agent is invoking async completion twice", this.lastAgentName));
				}
				ExTraceGlobals.DispatchTracer.TraceDebug<int>((long)this.GetHashCode(), this.InstanceNameFormatted + "async result {0}", this.pendingResult.GetHashCode());
				this.isSyncInvoke = false;
				try
				{
					this.Invoke();
				}
				catch (IOException e)
				{
					this.HandleLeakedException(e);
				}
				catch (LocalizedException e2)
				{
					this.HandleLeakedException(e2);
				}
				if (this.IsExecutionCompleted && this.pendingResult != null)
				{
					ExTraceGlobals.DispatchTracer.TraceDebug<bool>((long)this.GetHashCode(), this.InstanceNameFormatted + "async dispatch done: {0}", this.IsStatusHalt);
					this.UnbindContexts();
					this.pendingResult.InvokeCompleted();
				}
				else
				{
					ExTraceGlobals.DispatchTracer.TraceDebug<bool>((long)this.GetHashCode(), this.InstanceNameFormatted + "async dispatch pending: {0}", this.IsStatusHalt);
				}
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015974 File Offset: 0x00013B74
		private void BindContexts(object source, object e)
		{
			if (!this.isCompleted)
			{
				ExTraceGlobals.DispatchTracer.TraceError((long)this.GetHashCode(), this.InstanceNameFormatted + "concurrent invoke within a session");
				throw new InvalidOperationException(MExRuntimeStrings.InvalidConcurrentInvoke);
			}
			this.isStatusHalt = false;
			this.isCompleted = false;
			this.currentAgent = null;
			this.nextAgent = null;
			this.currentEventSource = source;
			this.currentEventArgs = e;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000159E3 File Offset: 0x00013BE3
		private void UnbindContexts()
		{
			this.currentEventSource = null;
			this.currentEventArgs = null;
			this.currentAgent = null;
			this.nextAgent = null;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015A04 File Offset: 0x00013C04
		private AgentAsyncCallback GetAsyncCallbackHandler()
		{
			this.isAsyncAgent = true;
			if (this.isSyncInvoke)
			{
				if (this.syncWaitHandle == null)
				{
					this.syncWaitHandle = new ManualResetEvent(false);
				}
			}
			else
			{
				this.pendingResult.SetAsync();
			}
			ExTraceGlobals.DispatchTracer.TraceDebug((long)this.GetHashCode(), this.InstanceNameFormatted + "going async");
			return new AgentAsyncCallback(this.AgentAsyncCompletionCallback);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015A70 File Offset: 0x00013C70
		private void AgentAsyncCompletionCallback(AgentAsyncContext context)
		{
			if (this.resumeAgentCallback != null && this.resumeAgentCallback())
			{
				MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentDidNotCallResume, this.lastAgentName, new object[]
				{
					this.lastAgentName,
					this.eventTopic
				});
			}
			ExTraceGlobals.DispatchTracer.TraceDebug<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "async completed, async result {0}, exception {1}", this.isSyncInvoke ? "n/a" : this.pendingResult.GetHashCode().ToString(CultureInfo.InvariantCulture), (context.AsyncException == null) ? "n/a" : context.AsyncException.GetType().FullName);
			if (this.isSyncInvoke)
			{
				if (context.AsyncException != null)
				{
					MExAsyncResult.WrapAndRethrowException(context.AsyncException, new LocalizedString(MExRuntimeStrings.AgentFault(this.currentAgent.Name, this.eventTopic)));
				}
				this.syncWaitHandle.Set();
				return;
			}
			if (context.AsyncException != null)
			{
				this.pendingResult.AsyncException = context.AsyncException;
				this.HaltExecution();
				MExSession.LogMexAgentFaultEvent(MExDiagnostics.EventLog, context.AsyncException, this.currentAgent.Name, this.eventTopic);
			}
			if (this.completeAsyncAgentCallback != null)
			{
				this.completeAsyncAgentCallback();
			}
			this.Dispatcher.AgentInvokeCompleted(this);
			this.ResumeExecution();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015BD4 File Offset: 0x00013DD4
		internal static void LogMexAgentFaultEvent(ExEventLog eventLog, Exception exception, string agentName, string eventTopic)
		{
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			ArgumentValidator.ThrowIfNull("exception", exception);
			string text = exception.GetType().Name;
			if (text == "SmtpResponseException")
			{
				return;
			}
			eventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentFault, agentName + "," + text, new object[]
			{
				agentName,
				text,
				exception.Message,
				eventTopic
			});
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015C48 File Offset: 0x00013E48
		private Agent CreateAgentInstance(AgentRecord agentRecord)
		{
			AgentFactory agentFactory = this.settings.AgentFactories[agentRecord.Id];
			object state = (this.hostState == null) ? null : this.hostState.Clone();
			Agent agent;
			try
			{
				agent = agentFactory.CreateAgent(agentRecord.Type, state);
			}
			catch (LocalizedException ex)
			{
				MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentInstanceCreationFailure, null, new object[]
				{
					agentRecord.Name,
					ex.Message
				});
				throw;
			}
			if (agent == null || agent.Id != null)
			{
				string error = (agent == null) ? "agent instance cannot be null" : "agent instance already in use";
				ApplicationException ex2 = new ApplicationException(MExRuntimeStrings.AgentCreationFailure(agentRecord.Name, error));
				MExDiagnostics.EventLog.LogEvent(EdgeExtensibilityEventLogConstants.Tuple_MExAgentInstanceCreationFailure, null, new object[]
				{
					agentRecord.Name,
					ex2.Message
				});
				throw ex2;
			}
			agent.Id = agent.GetHashCode().ToString(CultureInfo.InvariantCulture);
			agent.Name = agentRecord.Name;
			agent.SnapshotEnabled = this.settings.MonitoringOptions.MessageSnapshotEnabled;
			agent.HostState = state;
			ExTraceGlobals.DispatchTracer.Information<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "agent '{0}' created from factory '{1}'", agent.GetType().FullName, agentFactory.GetType().FullName);
			return agent;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private bool IsAgentEnabled(AgentRecord agentRecord)
		{
			if (string.Equals(agentRecord.Type, MExSession.DeliveryAgentType, StringComparison.OrdinalIgnoreCase))
			{
				DeliveryAgentManager deliveryAgentManager = (DeliveryAgentManager)this.settings.AgentFactories.GetAgentManager(agentRecord.Id);
				if (!string.Equals(deliveryAgentManager.SupportedDeliveryProtocol, this.name, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00015E08 File Offset: 0x00014008
		private void HandleLeakedException(Exception e)
		{
			this.pendingResult.EventTopic = this.eventTopic;
			this.pendingResult.FaultyAgentName = this.currentAgent.Name;
			this.pendingResult.AsyncException = e;
			ExTraceGlobals.DispatchTracer.TraceError((long)this.GetHashCode(), this.InstanceNameFormatted + e);
			MExSession.LogMexAgentFaultEvent(MExDiagnostics.EventLog, e, this.currentAgent.Name, this.eventTopic);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015E81 File Offset: 0x00014081
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00015E89 File Offset: 0x00014089
		public DisposeTracker GetDisposeTracker()
		{
			return this.disposeTracker;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015E91 File Offset: 0x00014091
		public void SuppressDisposeTracker()
		{
			this.disposeTracker.Suppress();
		}

		// Token: 0x040004BE RID: 1214
		private static readonly string DeliveryAgentType = typeof(DeliveryAgent).ToString();

		// Token: 0x040004BF RID: 1215
		private readonly object syncRoot = new object();

		// Token: 0x040004C0 RID: 1216
		private readonly string InstanceNameFormatted;

		// Token: 0x040004C1 RID: 1217
		private readonly string name;

		// Token: 0x040004C2 RID: 1218
		private readonly string parentName;

		// Token: 0x040004C3 RID: 1219
		private readonly string id;

		// Token: 0x040004C4 RID: 1220
		private bool closed;

		// Token: 0x040004C5 RID: 1221
		private ICloneableInternal hostState;

		// Token: 0x040004C6 RID: 1222
		private IRuntimeSettings settings;

		// Token: 0x040004C7 RID: 1223
		private Dispatcher dispatcher;

		// Token: 0x040004C8 RID: 1224
		private AgentRecord[] agentsInDefaultOrder;

		// Token: 0x040004C9 RID: 1225
		private AgentRecord[] publicAgentsInDefaultOrder;

		// Token: 0x040004CA RID: 1226
		private object currentEventSource;

		// Token: 0x040004CB RID: 1227
		private object currentEventArgs;

		// Token: 0x040004CC RID: 1228
		private readonly Func<bool> resumeAgentCallback;

		// Token: 0x040004CD RID: 1229
		private readonly Action startAsyncAgentCallback;

		// Token: 0x040004CE RID: 1230
		private readonly Action completeAsyncAgentCallback;

		// Token: 0x040004CF RID: 1231
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040004D0 RID: 1232
		private CpuTracker cpuTracker;

		// Token: 0x040004D1 RID: 1233
		private readonly MExSession.ScheduleWork scheduleWorkDelegate;

		// Token: 0x040004D2 RID: 1234
		private volatile bool isCompleted;

		// Token: 0x040004D3 RID: 1235
		private bool isStatusHalt;

		// Token: 0x040004D4 RID: 1236
		private AgentRecord nextAgent;

		// Token: 0x040004D5 RID: 1237
		private AgentRecord currentAgent;

		// Token: 0x040004D6 RID: 1238
		private string lastAgentName;

		// Token: 0x040004D7 RID: 1239
		private string eventTopic;

		// Token: 0x040004D8 RID: 1240
		private ManualResetEvent syncWaitHandle;

		// Token: 0x040004D9 RID: 1241
		private bool isSyncInvoke = true;

		// Token: 0x040004DA RID: 1242
		private MExAsyncResult pendingResult;

		// Token: 0x040004DB RID: 1243
		private long beginInvokeTicks;

		// Token: 0x040004DC RID: 1244
		private long beginSchedulingTicks;

		// Token: 0x040004DD RID: 1245
		private bool isAsyncAgent;

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x06000492 RID: 1170
		public delegate bool ScheduleWork(WaitCallback callback);
	}
}
