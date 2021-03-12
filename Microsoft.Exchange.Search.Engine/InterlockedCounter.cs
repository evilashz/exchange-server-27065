using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000008 RID: 8
	internal abstract class InterlockedCounter
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002EF8 File Offset: 0x000010F8
		protected InterlockedCounter(int initialValue, Func<int, bool> condition)
		{
			this.counter = initialValue;
			this.condition = condition;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F0E File Offset: 0x0000110E
		public static InterlockedCounter Create(int initialValue, Func<int, bool> condition, Action action)
		{
			return new InterlockedCounter.CallbackCounter(initialValue, condition, action);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F18 File Offset: 0x00001118
		public static InterlockedCounter.EventCounter Create(int initialValue, Func<int, bool> condition)
		{
			return new InterlockedCounter.EventCounter(initialValue, condition);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F24 File Offset: 0x00001124
		public int Increment()
		{
			int num = Interlocked.Increment(ref this.counter);
			if (this.condition(num))
			{
				this.CounterAction();
			}
			return num;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F54 File Offset: 0x00001154
		public int Decrement()
		{
			int num = Interlocked.Decrement(ref this.counter);
			if (this.condition(num))
			{
				this.CounterAction();
			}
			return num;
		}

		// Token: 0x06000053 RID: 83
		protected abstract void CounterAction();

		// Token: 0x04000020 RID: 32
		private readonly Func<int, bool> condition;

		// Token: 0x04000021 RID: 33
		private int counter;

		// Token: 0x02000009 RID: 9
		internal class CallbackCounter : InterlockedCounter
		{
			// Token: 0x06000054 RID: 84 RVA: 0x00002F82 File Offset: 0x00001182
			internal CallbackCounter(int initialValue, Func<int, bool> condition, Action callbackAction) : base(initialValue, condition)
			{
				Util.ThrowOnNullArgument(callbackAction, "callbackAction");
				Util.ThrowOnNullArgument(condition, "condition");
				this.callbackAction = callbackAction;
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00002FA9 File Offset: 0x000011A9
			protected override void CounterAction()
			{
				this.callbackAction();
			}

			// Token: 0x04000022 RID: 34
			private readonly Action callbackAction;
		}

		// Token: 0x0200000A RID: 10
		internal class EventCounter : InterlockedCounter, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00002FB6 File Offset: 0x000011B6
			internal EventCounter(int initialValue, Func<int, bool> condition) : base(initialValue, condition)
			{
				this.conditionSatisfied = new ManualResetEventSlim(condition(initialValue));
				this.disposeTracker = this.GetDisposeTracker();
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002FDE File Offset: 0x000011DE
			public void Wait(TimeSpan timeout)
			{
				if (!this.conditionSatisfied.Wait(timeout))
				{
					throw new InterlockedCounter.InterlockedCounterException(Strings.InterlockedCounterTimeout);
				}
				if (this.disposedBeforeTimeout)
				{
					throw new InterlockedCounter.InterlockedCounterException(Strings.InterlockedCounterDisposed);
				}
			}

			// Token: 0x06000058 RID: 88 RVA: 0x0000300C File Offset: 0x0000120C
			public void Dispose()
			{
				if (!this.conditionSatisfied.IsSet)
				{
					this.disposedBeforeTimeout = true;
					this.conditionSatisfied.Set();
				}
				this.conditionSatisfied.Dispose();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000059 RID: 89 RVA: 0x0000305C File Offset: 0x0000125C
			public DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<InterlockedCounter.EventCounter>(this);
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00003064 File Offset: 0x00001264
			public void SuppressDisposeTracker()
			{
				this.disposeTracker.Suppress();
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00003071 File Offset: 0x00001271
			protected override void CounterAction()
			{
				this.conditionSatisfied.Set();
			}

			// Token: 0x04000023 RID: 35
			private readonly ManualResetEventSlim conditionSatisfied;

			// Token: 0x04000024 RID: 36
			private bool disposedBeforeTimeout;

			// Token: 0x04000025 RID: 37
			private DisposeTracker disposeTracker;
		}

		// Token: 0x0200000B RID: 11
		internal class InterlockedCounterException : ComponentFailedTransientException
		{
			// Token: 0x0600005C RID: 92 RVA: 0x0000307E File Offset: 0x0000127E
			public InterlockedCounterException(LocalizedString message) : base(message)
			{
			}
		}
	}
}
