using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.ServiceHost.Common
{
	// Token: 0x02000013 RID: 19
	internal abstract class ServiceTaskBase : IServiceTask
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003A4C File Offset: 0x00001C4C
		protected ServiceTaskBase(CancellationToken cancellationToken, bool recurring)
		{
			this.CancellationToken = cancellationToken;
			this.IsRecurring = recurring;
			this.ConsequtiveFailureCount = 0;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111
		public abstract string Name { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003A69 File Offset: 0x00001C69
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003A71 File Offset: 0x00001C71
		public bool IsRecurring { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003A7A File Offset: 0x00001C7A
		public virtual bool DelayedStart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003A7D File Offset: 0x00001C7D
		public virtual int MaxRandomStartDelay
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003A80 File Offset: 0x00001C80
		public virtual int DelayBetweenRuns
		{
			get
			{
				return 240;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003A87 File Offset: 0x00001C87
		public virtual int MaxNumberOfRetries
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003A8A File Offset: 0x00001C8A
		public virtual int DelayBetweenRetries
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003A8E File Offset: 0x00001C8E
		public virtual int MaxNumberOfConsequtiveFailures
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003A92 File Offset: 0x00001C92
		public virtual IResourceLoadMonitor ResourceLoadMonitor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003A95 File Offset: 0x00001C95
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003A9D File Offset: 0x00001C9D
		internal int ConsequtiveFailureCount { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003AA6 File Offset: 0x00001CA6
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003AAE File Offset: 0x00001CAE
		private protected CancellationToken CancellationToken { protected get; private set; }

		// Token: 0x0600007D RID: 125 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public void Run()
		{
			bool flag = true;
			for (;;)
			{
				for (int i = 0; i < this.MaxNumberOfRetries; i++)
				{
					try
					{
						if (flag && this.DelayedStart)
						{
							this.RandomSleep();
						}
						flag = false;
						this.CheckForCancellationRequest();
						this.InternalReset();
						if (this.InternalPreRunCheck())
						{
							this.WaitForHealthyResourceLoadState();
							this.RaiseEvent(this.StartingTaskExecution, null);
							this.InternalRun();
							this.RaiseEvent(this.TaskCompleted, null);
							this.ConsequtiveFailureCount = 0;
						}
						else
						{
							this.RaiseEvent(this.SkippingTaskExecution, null);
						}
						if (this.IsRecurring)
						{
							break;
						}
						return;
					}
					catch (OperationCanceledException)
					{
						this.RaiseEvent(this.TaskCancelled, null);
						return;
					}
					catch (DataSourceTransientException error)
					{
						this.ConsequtiveFailureCount++;
						this.RaiseEvent(this.TransientTaskError, error);
					}
					catch (Exception error2)
					{
						this.ConsequtiveFailureCount++;
						this.RaiseEvent(this.PermanentTaskError, error2);
					}
					if (this.ConsequtiveFailureCount >= this.MaxNumberOfConsequtiveFailures)
					{
						goto Block_2;
					}
					if (this.MaxNumberOfRetries - i > 1)
					{
						this.SleepSecondsOrUntilCancelled(this.DelayBetweenRetries, false);
					}
				}
				if (this.IsRecurring)
				{
					this.SleepSecondsOrUntilCancelled(this.DelayBetweenRuns * 60, false);
				}
				if (!this.IsRecurring)
				{
					return;
				}
			}
			Block_2:
			this.RaiseEvent(this.TaskTerminated, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C1C File Offset: 0x00001E1C
		internal virtual bool IsHealthyResourceLoadState(ResourceLoadState currentState)
		{
			return currentState == ResourceLoadState.Underloaded;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C22 File Offset: 0x00001E22
		internal virtual void InternalReset()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C24 File Offset: 0x00001E24
		internal virtual bool InternalPreRunCheck()
		{
			return true;
		}

		// Token: 0x06000081 RID: 129
		internal abstract void InternalRun();

		// Token: 0x06000082 RID: 130 RVA: 0x00003C28 File Offset: 0x00001E28
		protected void CheckForCancellationRequest()
		{
			if (this.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected void SleepSecondsOrUntilCancelled(int seconds, bool throwIfCancelled = false)
		{
			if (seconds == 0)
			{
				return;
			}
			int i = 0;
			while (i < seconds)
			{
				if (this.CancellationToken.IsCancellationRequested)
				{
					if (throwIfCancelled)
					{
						throw new OperationCanceledException();
					}
					break;
				}
				else
				{
					Thread.Sleep(1000);
					i++;
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003C8C File Offset: 0x00001E8C
		private void RandomSleep()
		{
			int seconds = ServiceTaskBase.random.Next(this.MaxRandomStartDelay * 60);
			this.SleepSecondsOrUntilCancelled(seconds, true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003CB8 File Offset: 0x00001EB8
		private void WaitForHealthyResourceLoadState()
		{
			bool flag = false;
			while (this.ResourceLoadMonitor != null)
			{
				ResourceLoadState state = this.ResourceLoadMonitor.GetResourceLoad(WorkloadClassification.Discretionary, false, null).State;
				if (this.IsHealthyResourceLoadState(state))
				{
					this.RaiseEvent(this.HealthyResourceLoadState, null);
					return;
				}
				if (!flag)
				{
					this.RaiseEvent(this.WaitingForHealthyResourceLoadState, null);
					flag = true;
				}
				this.SleepSecondsOrUntilCancelled(5, true);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D1C File Offset: 0x00001F1C
		private void RaiseEvent(EventHandler<ServiceTaskEventArgs> handler, Exception error = null)
		{
			if (handler != null)
			{
				handler(this, new ServiceTaskEventArgs(this, error));
			}
		}

		// Token: 0x0400003D RID: 61
		private const int DefaultMaxRandomStartDelay = 1;

		// Token: 0x0400003E RID: 62
		private const int DefaultDelayBetweenRuns = 240;

		// Token: 0x0400003F RID: 63
		private const int DefaultMaxNumberOfRetries = 1;

		// Token: 0x04000040 RID: 64
		private const int DefaultDelayBetweenRetries = 15;

		// Token: 0x04000041 RID: 65
		private const int DefaultMaxNumberOfConsequtiveFailures = 10;

		// Token: 0x04000042 RID: 66
		private static readonly Random random = new Random();

		// Token: 0x04000043 RID: 67
		internal EventHandler<ServiceTaskEventArgs> TransientTaskError;

		// Token: 0x04000044 RID: 68
		internal EventHandler<ServiceTaskEventArgs> PermanentTaskError;

		// Token: 0x04000045 RID: 69
		internal EventHandler<ServiceTaskEventArgs> TaskCompleted;

		// Token: 0x04000046 RID: 70
		internal EventHandler<ServiceTaskEventArgs> TaskTerminated;

		// Token: 0x04000047 RID: 71
		internal EventHandler<ServiceTaskEventArgs> StartingTaskExecution;

		// Token: 0x04000048 RID: 72
		internal EventHandler<ServiceTaskEventArgs> SkippingTaskExecution;

		// Token: 0x04000049 RID: 73
		internal EventHandler<ServiceTaskEventArgs> TaskCancelled;

		// Token: 0x0400004A RID: 74
		internal EventHandler<ServiceTaskEventArgs> WaitingForHealthyResourceLoadState;

		// Token: 0x0400004B RID: 75
		internal EventHandler<ServiceTaskEventArgs> HealthyResourceLoadState;
	}
}
