using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Inference.Performance;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200005B RID: 91
	internal abstract class StatefulComponent : Disposable, IDiagnosable
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00004004 File Offset: 0x00002204
		internal StatefulComponent(uint initialState)
		{
			this.perfCounterInstance = StatefulComponentPerformanceCounters.GetInstance(base.GetType().ToString());
			this.diagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("StatefulComponent", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreComponentTracer, (long)this.GetHashCode());
			this.CurrentState = initialState;
			this.dispatchQueue = new Heap(StatefulComponent.DispatchPriorityComparer.Instance);
			this.transitionLog = new Queue<TransitionLogEntry>(10);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004077 File Offset: 0x00002277
		internal StatefulComponent() : this(1U)
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004080 File Offset: 0x00002280
		public IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004088 File Offset: 0x00002288
		internal TransitionLogEntry[] TransitionLog
		{
			get
			{
				TransitionLogEntry[] result;
				lock (this.transitionLog)
				{
					result = this.transitionLog.ToArray();
				}
				return result;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000040D0 File Offset: 0x000022D0
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000040D8 File Offset: 0x000022D8
		internal uint CurrentState
		{
			get
			{
				return this.currentState;
			}
			private set
			{
				if (value == this.currentState)
				{
					if (this.DiagnosticsSession.IsTraceEnabled(TraceType.DebugTrace))
					{
						this.DiagnosticsSession.TraceDebug<StateInfo>("State already set to {0}.", this.CurrentStateInfo);
					}
					return;
				}
				if (this.DiagnosticsSession.IsTraceEnabled(TraceType.DebugTrace))
				{
					StateInfo arg = null;
					ComponentRegistry.TryGetStateInfo(base.GetType(), value, out arg);
					this.DiagnosticsSession.TraceDebug<StateInfo, StateInfo>("Changing state from {0} to {1}.", this.CurrentStateInfo, arg);
				}
				this.currentState = value;
				this.lastStateChangedTime = DateTime.UtcNow;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000415C File Offset: 0x0000235C
		protected StateInfo CurrentStateInfo
		{
			get
			{
				StateInfo result = null;
				ComponentRegistry.TryGetStateInfo(base.GetType(), this.CurrentState, out result);
				return result;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004180 File Offset: 0x00002380
		public override string ToString()
		{
			return string.Format("{0} ({1})", base.GetType(), this.GetHashCode());
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000419D File Offset: 0x0000239D
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return base.GetType().Name;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000041AA File Offset: 0x000023AA
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.InternalGetDiagnosticInfo(parameters);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000041B4 File Offset: 0x000023B4
		internal string GetSignalName(uint signal)
		{
			SignalInfo signalInfo = null;
			Type type = base.GetType();
			while (type != null && typeof(StatefulComponent).IsAssignableFrom(type))
			{
				if (ComponentRegistry.TryGetSignalInfo(type, signal, out signalInfo))
				{
					return signalInfo.Name;
				}
				type = type.BaseType;
			}
			return string.Format("Unknown Signal: {0}", signal);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004210 File Offset: 0x00002410
		internal string GetStateName(uint state)
		{
			StateInfo stateInfo = null;
			Type type = base.GetType();
			while (type != null && typeof(StatefulComponent).IsAssignableFrom(type))
			{
				if (ComponentRegistry.TryGetStateInfo(type, state, out stateInfo))
				{
					return stateInfo.Name;
				}
				type = type.BaseType;
			}
			return string.Format("Unknown State: {0}", state);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000426C File Offset: 0x0000246C
		internal IAsyncResult InternalBeginDispatchSignal(WaitHandle waitHandle, uint signal, AsyncCallback callback, object context, TimeSpan delayInTimespan, params object[] arguments)
		{
			base.CheckDisposed();
			if (delayInTimespan != TimeSpan.Zero && waitHandle == null)
			{
				throw new ArgumentException("A value for delayInTimespan different than zero doesn't make sense if waitHandle is null");
			}
			IAsyncResult result;
			lock (this.dispatchQueue)
			{
				SignalInfo signalInfo = null;
				if (!ComponentRegistry.TryGetSignalInfo(base.GetType(), signal, out signalInfo))
				{
					throw new ArgumentException(string.Format("Unknown signal '{0}'", signal));
				}
				AsyncResult<bool> asyncResult = new AsyncResult<bool>(callback, context);
				StatefulComponent.QueueItem queueItem = new StatefulComponent.QueueItem(signalInfo, asyncResult, waitHandle, arguments);
				if (waitHandle == null)
				{
					this.DiagnosticsSession.TraceDebug<StatefulComponent.QueueItem>("Enqueuing {0}", queueItem);
					this.dispatchQueue.Push(queueItem);
					queueItem.MarkAsEnqueued();
					this.DispatchNextItemNoLock();
				}
				else
				{
					this.DiagnosticsSession.TraceDebug<StatefulComponent.QueueItem, double>("Delaying enqueuing {0} for {1} ms.", queueItem, delayInTimespan.TotalMilliseconds);
					RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(waitHandle, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.DelayedDispatchSignalCallback)), queueItem, delayInTimespan, true);
				}
				result = asyncResult;
			}
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000436C File Offset: 0x0000256C
		internal bool EndDispatchSignal(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			return ((AsyncResult<bool>)asyncResult).End();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000437F File Offset: 0x0000257F
		protected sealed override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StatefulComponent>(this);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004388 File Offset: 0x00002588
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.dispatchQueue)
				{
				}
				SpinWait spinWait = default(SpinWait);
				while (this.dispatchingItem != null)
				{
					spinWait.SpinOnce();
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000043E0 File Offset: 0x000025E0
		protected virtual XElement InternalGetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			xelement.Add(new XElement("State", this.GetStateName(this.CurrentState)));
			xelement.Add(new XElement("StateStartTime", this.lastStateChangedTime));
			return xelement;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00004440 File Offset: 0x00002640
		protected virtual bool AtNoTransitionDefined(uint signal)
		{
			throw new NoTransitionException(this, this.CurrentState, signal);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000444F File Offset: 0x0000264F
		private static void RegisterComponent(ComponentInfo componentInfo)
		{
			componentInfo.RegisterState(StatefulComponent.State.New);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004460 File Offset: 0x00002660
		private void DispatchNextItemNoLock()
		{
			this.DiagnosticsSession.TraceDebug<Heap>("DispatchNextItemNoLock, queue snapshot: {0}", this.dispatchQueue);
			if (this.dispatchingItem != null)
			{
				this.DiagnosticsSession.TraceDebug("Another thread already dispatching queue, exiting.", new object[0]);
				return;
			}
			IHeapItem heapItem = null;
			if (this.dispatchQueue.TryPop(out heapItem))
			{
				StatefulComponent.QueueItem queueItem = (StatefulComponent.QueueItem)heapItem;
				this.DiagnosticsSession.TraceDebug<StatefulComponent.QueueItem>("Dispatching: {0}", queueItem);
				this.dispatchingItem = queueItem;
				using (ActivityContext.SuppressThreadScope())
				{
					ThreadPool.QueueUserWorkItem(CallbackWrapper.WaitCallback(new WaitCallback(this.DispatchQueueItem)), queueItem);
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004510 File Offset: 0x00002710
		private void DispatchQueueItem(object context)
		{
			base.CheckDisposed();
			try
			{
				ComponentException ex = null;
				StatefulComponent.QueueItem queueItem = context as StatefulComponent.QueueItem;
				bool asCompleted = true;
				long incrementValue = queueItem.MarkAsDispatched();
				this.DiagnosticsSession.IncrementCounterBy(this.perfCounterInstance.AverageSignalDispatchingLatency, incrementValue);
				this.DiagnosticsSession.IncrementCounter(this.perfCounterInstance.AverageSignalDispatchingLatencyBase);
				queueItem.CheckForCancellation();
				List<TransitionInfo> list = null;
				if (!ComponentRegistry.TryGetTransitionInfo(base.GetType(), this.CurrentState, queueItem.SignalInfo.Value, out list))
				{
					try
					{
						asCompleted = this.AtNoTransitionDefined(queueItem.SignalInfo.Value);
						goto IL_190;
					}
					catch (ComponentException ex2)
					{
						ex = ex2;
						goto IL_190;
					}
				}
				TransitionInfo transitionInfo = null;
				foreach (TransitionInfo transitionInfo2 in list)
				{
					if (transitionInfo2.Condition == null)
					{
						transitionInfo = transitionInfo2;
						break;
					}
					if (transitionInfo2.Condition(this))
					{
						transitionInfo = transitionInfo2;
						break;
					}
				}
				if (transitionInfo == null)
				{
					try
					{
						asCompleted = this.AtNoTransitionDefined(queueItem.SignalInfo.Value);
						goto IL_190;
					}
					catch (ComponentException ex3)
					{
						ex = ex3;
						goto IL_190;
					}
				}
				if (transitionInfo.Action != null)
				{
					this.DiagnosticsSession.TraceDebug<MethodInfo>("Executing transition action: {0}", transitionInfo.Action.Method);
					try
					{
						transitionInfo.Action(this, queueItem.Arguments);
						goto IL_175;
					}
					catch (ComponentException ex4)
					{
						ex = ex4;
						goto IL_175;
					}
				}
				if (this.DiagnosticsSession.IsTraceEnabled(TraceType.DebugTrace))
				{
					this.DiagnosticsSession.TraceDebug<StateInfo, SignalInfo>("Action is not defined (noop) for this transition for state {0} and signal {1}", this.CurrentStateInfo, queueItem.SignalInfo);
				}
				asCompleted = false;
				IL_175:
				this.CurrentState = transitionInfo.TargetState;
				this.LogTransitionHistory(transitionInfo.TargetState, queueItem);
				IL_190:
				if (ex != null)
				{
					queueItem.AsyncResult.SetAsCompleted(ex);
				}
				else
				{
					queueItem.AsyncResult.SetAsCompleted(asCompleted);
				}
			}
			finally
			{
				this.dispatchingItem = null;
			}
			lock (this.dispatchQueue)
			{
				this.DispatchNextItemNoLock();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004794 File Offset: 0x00002994
		private void DelayedDispatchSignalCallback(object context, bool isTimeout)
		{
			base.CheckDisposed();
			StatefulComponent.QueueItem queueItem = context as StatefulComponent.QueueItem;
			if (!isTimeout)
			{
				this.DiagnosticsSession.TraceDebug<StatefulComponent.QueueItem>("Event set, cancelling dispatch item: {0}", queueItem);
				queueItem.Cancel();
			}
			lock (this.dispatchQueue)
			{
				this.DiagnosticsSession.TraceDebug<StatefulComponent.QueueItem>("Enqueuing delayed dispatch item: {0}", queueItem);
				this.dispatchQueue.Push(queueItem);
				queueItem.MarkAsEnqueued();
				this.DispatchNextItemNoLock();
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004820 File Offset: 0x00002A20
		private void LogTransitionHistory(uint currentState, StatefulComponent.QueueItem queueItem)
		{
			TransitionLogEntry item = new TransitionLogEntry(queueItem.Sequence, currentState, queueItem.SignalInfo.Value);
			lock (this.transitionLog)
			{
				if (this.transitionLog.Count == 10)
				{
					this.transitionLog.Dequeue();
				}
				this.transitionLog.Enqueue(item);
			}
		}

		// Token: 0x040000AF RID: 175
		private const int TransitionLogSize = 10;

		// Token: 0x040000B0 RID: 176
		private readonly StatefulComponentPerformanceCountersInstance perfCounterInstance;

		// Token: 0x040000B1 RID: 177
		private readonly Heap dispatchQueue;

		// Token: 0x040000B2 RID: 178
		private readonly Queue<TransitionLogEntry> transitionLog;

		// Token: 0x040000B3 RID: 179
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000B4 RID: 180
		private uint currentState;

		// Token: 0x040000B5 RID: 181
		private DateTime lastStateChangedTime;

		// Token: 0x040000B6 RID: 182
		private volatile StatefulComponent.QueueItem dispatchingItem;

		// Token: 0x0200005C RID: 92
		internal enum State : uint
		{
			// Token: 0x040000B8 RID: 184
			New = 1U,
			// Token: 0x040000B9 RID: 185
			Max,
			// Token: 0x040000BA RID: 186
			Any = 4294967295U
		}

		// Token: 0x0200005D RID: 93
		internal enum Signal : uint
		{
			// Token: 0x040000BC RID: 188
			Max = 1U,
			// Token: 0x040000BD RID: 189
			Cancelled = 4026531840U
		}

		// Token: 0x0200005E RID: 94
		private sealed class DispatchPriorityComparer : IComparer<IHeapItem>
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001E5 RID: 485 RVA: 0x000048A8 File Offset: 0x00002AA8
			internal static StatefulComponent.DispatchPriorityComparer Instance
			{
				get
				{
					return StatefulComponent.DispatchPriorityComparer.instance;
				}
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x000048B0 File Offset: 0x00002AB0
			public int Compare(IHeapItem left, IHeapItem right)
			{
				StatefulComponent.QueueItem queueItem = (StatefulComponent.QueueItem)left;
				StatefulComponent.QueueItem queueItem2 = (StatefulComponent.QueueItem)right;
				uint priority = (uint)queueItem.SignalInfo.Priority;
				uint priority2 = (uint)queueItem2.SignalInfo.Priority;
				if (priority > priority2)
				{
					return 1;
				}
				if (priority < priority2)
				{
					return -1;
				}
				if (queueItem.Sequence > queueItem2.Sequence)
				{
					return -1;
				}
				if (queueItem.Sequence < queueItem2.Sequence)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040000BE RID: 190
			private static StatefulComponent.DispatchPriorityComparer instance = new StatefulComponent.DispatchPriorityComparer();
		}

		// Token: 0x02000060 RID: 96
		private sealed class QueueItem : IHeapItem
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00004918 File Offset: 0x00002B18
			internal QueueItem(SignalInfo signalInfo, AsyncResult<bool> asyncResult, WaitHandle cancelItem, params object[] arguments)
			{
				this.signalInfo = signalInfo;
				this.asyncResult = asyncResult;
				this.cancelItem = cancelItem;
				this.arguments = arguments;
				this.sequence = Interlocked.Increment(ref StatefulComponent.QueueItem.globalSequence);
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001EB RID: 491 RVA: 0x0000494D File Offset: 0x00002B4D
			// (set) Token: 0x060001EC RID: 492 RVA: 0x00004955 File Offset: 0x00002B55
			public int Handle
			{
				get
				{
					return this.heapHandle;
				}
				set
				{
					this.heapHandle = value;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001ED RID: 493 RVA: 0x0000495E File Offset: 0x00002B5E
			internal SignalInfo SignalInfo
			{
				get
				{
					return this.signalInfo;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001EE RID: 494 RVA: 0x00004966 File Offset: 0x00002B66
			internal object[] Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001EF RID: 495 RVA: 0x0000496E File Offset: 0x00002B6E
			internal AsyncResult<bool> AsyncResult
			{
				get
				{
					return this.asyncResult;
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x00004976 File Offset: 0x00002B76
			internal long Sequence
			{
				get
				{
					return this.sequence;
				}
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x0000497E File Offset: 0x00002B7E
			public override string ToString()
			{
				return string.Format("{0}, seq: {1}", this.SignalInfo.ToString(), this.Sequence);
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x000049A0 File Offset: 0x00002BA0
			internal void MarkAsEnqueued()
			{
				this.stopWatch = Stopwatch.StartNew();
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x000049AD File Offset: 0x00002BAD
			internal long MarkAsDispatched()
			{
				this.stopWatch.Stop();
				return this.stopWatch.ElapsedMilliseconds;
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x000049C5 File Offset: 0x00002BC5
			internal void Cancel()
			{
				this.signalInfo = this.signalInfo.Clone();
				this.signalInfo.Value |= 4026531840U;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x000049EF File Offset: 0x00002BEF
			internal void CheckForCancellation()
			{
				if (this.cancelItem != null && this.cancelItem.WaitOne(0))
				{
					this.Cancel();
				}
			}

			// Token: 0x040000BF RID: 191
			private static long globalSequence;

			// Token: 0x040000C0 RID: 192
			private readonly object[] arguments;

			// Token: 0x040000C1 RID: 193
			private readonly AsyncResult<bool> asyncResult;

			// Token: 0x040000C2 RID: 194
			private readonly long sequence;

			// Token: 0x040000C3 RID: 195
			private readonly WaitHandle cancelItem;

			// Token: 0x040000C4 RID: 196
			private int heapHandle;

			// Token: 0x040000C5 RID: 197
			private SignalInfo signalInfo;

			// Token: 0x040000C6 RID: 198
			private Stopwatch stopWatch;
		}
	}
}
