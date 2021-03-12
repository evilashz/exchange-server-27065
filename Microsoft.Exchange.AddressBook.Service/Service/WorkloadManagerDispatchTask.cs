using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000008 RID: 8
	internal abstract class WorkloadManagerDispatchTask : DispatchTask, ITask
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003E9A File Offset: 0x0000209A
		public WorkloadManagerDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.workloadSettings = new WorkloadSettings(WorkloadType.Domt, false);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003EB2 File Offset: 0x000020B2
		public WorkloadSettings WorkloadSettings
		{
			get
			{
				base.CheckDisposed();
				return this.workloadSettings;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003EC0 File Offset: 0x000020C0
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003ECE File Offset: 0x000020CE
		public object State
		{
			get
			{
				base.CheckDisposed();
				return this.state;
			}
			set
			{
				base.CheckDisposed();
				this.state = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003EDD File Offset: 0x000020DD
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003EEB File Offset: 0x000020EB
		public string Description
		{
			get
			{
				base.CheckDisposed();
				return this.description;
			}
			set
			{
				base.CheckDisposed();
				this.description = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003EFA File Offset: 0x000020FA
		public TimeSpan MaxExecutionTime
		{
			get
			{
				base.CheckDisposed();
				return Configuration.MaxExecutionTime;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121
		public abstract IBudget Budget { get; }

		// Token: 0x0600007A RID: 122 RVA: 0x00003F07 File Offset: 0x00002107
		public virtual IActivityScope GetActivityScope()
		{
			base.CheckDisposed();
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003F10 File Offset: 0x00002110
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			IActivityScope activityScope = this.GetActivityScope();
			TaskExecuteResult result;
			using (new ActivityScopeThreadGuard(activityScope))
			{
				if (activityScope != null)
				{
					activityScope.Action = this.TaskName;
				}
				result = this.ExecuteTask(queueAndDelayTime, totalTime, false);
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003F64 File Offset: 0x00002164
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			IActivityScope activityScope = this.GetActivityScope();
			using (new ActivityScopeThreadGuard(activityScope))
			{
				if (activityScope != null)
				{
					activityScope.Action = this.TaskName;
				}
				base.CheckDisposed();
				base.Completion();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void Cancel()
		{
			base.CheckDisposed();
			base.Completion();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003FC6 File Offset: 0x000021C6
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.ExecuteTask(queueAndDelayTime, totalTime, true);
			this.Complete(queueAndDelayTime, totalTime);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003FDA File Offset: 0x000021DA
		public ResourceKey[] GetResources()
		{
			base.CheckDisposed();
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003FE3 File Offset: 0x000021E3
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			base.CheckDisposed();
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000081 RID: 129
		protected abstract void InternalPreExecute();

		// Token: 0x06000082 RID: 130
		protected abstract void InternalExecute();

		// Token: 0x06000083 RID: 131
		protected abstract void InternalPostExecute(TimeSpan queueAndDelayTime, TimeSpan totalTime, bool calledFromTimeout);

		// Token: 0x06000084 RID: 132
		protected abstract bool TryHandleException(Exception exception);

		// Token: 0x06000085 RID: 133 RVA: 0x00003FEC File Offset: 0x000021EC
		public TaskExecuteResult ExecuteTask(TimeSpan queueAndDelayTime, TimeSpan totalTime, bool calledFromTimeout)
		{
			base.CheckDisposed();
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.InternalPreExecute();
				try
				{
					if (!calledFromTimeout)
					{
						this.InternalExecute();
					}
				}
				catch (Exception exception)
				{
					if (!this.TryHandleException(exception))
					{
						if (Debugger.IsAttached)
						{
							Debugger.Break();
						}
						else
						{
							if (Configuration.CrashOnUnhandledException)
							{
								try
								{
									ExWatson.SendReportAndCrashOnAnotherThread(exception);
									goto IL_63;
								}
								finally
								{
									try
									{
										Process.GetCurrentProcess().Kill();
									}
									catch (Win32Exception)
									{
									}
									Environment.Exit(1);
								}
							}
							ExWatson.SendReport(exception, ReportOptions.DoNotFreezeThreads, null);
						}
					}
					IL_63:;
				}
				finally
				{
					stopwatch.Stop();
				}
				this.InternalPostExecute(queueAndDelayTime, stopwatch.Elapsed, calledFromTimeout);
			}
			finally
			{
				if (BaseTrace.CurrentThreadSettings.IsEnabled)
				{
					BaseTrace.CurrentThreadSettings.DisableTracing();
				}
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x04000032 RID: 50
		private WorkloadSettings workloadSettings;

		// Token: 0x04000033 RID: 51
		private object state;

		// Token: 0x04000034 RID: 52
		private string description;
	}
}
