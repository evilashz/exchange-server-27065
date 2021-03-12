using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000007 RID: 7
	internal abstract class ServiceCommand<Request, Response> : IServiceCommand, ITask
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002574 File Offset: 0x00000774
		public ServiceCommand(Request request, AsyncCallback asyncCallback, object asyncState)
		{
			this.Description = base.GetType().Name;
			this.asyncState = asyncState;
			this.asyncCallback = asyncCallback;
			this.RequestInstance = request;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025A2 File Offset: 0x000007A2
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000025AA File Offset: 0x000007AA
		public Request RequestInstance { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025B3 File Offset: 0x000007B3
		public IAsyncResult CommandAsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025BB File Offset: 0x000007BB
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025C3 File Offset: 0x000007C3
		public IBudget Budget { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000025D4 File Offset: 0x000007D4
		public string Description { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025DD File Offset: 0x000007DD
		public virtual TimeSpan MaxExecutionTime
		{
			get
			{
				return ServiceCommand<Request, Response>.DefaultMaxExecutionTime;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025E4 File Offset: 0x000007E4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025EC File Offset: 0x000007EC
		public object State { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025F5 File Offset: 0x000007F5
		public WorkloadSettings WorkloadSettings
		{
			get
			{
				return ServiceCommand<Request, Response>.WorkloadSettingsInstance;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025FC File Offset: 0x000007FC
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002604 File Offset: 0x00000804
		private Exception CommandError { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002610 File Offset: 0x00000810
		public void Cancel()
		{
			ExTraceGlobals.PushNotificationServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "ServiceCommand.Cancel: ServiceCommand cancelled for {0}.", this.Description);
			try
			{
				this.InternalCancel();
			}
			finally
			{
				this.Complete(new OperationCancelledException(this.Description));
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002664 File Offset: 0x00000864
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			return this.InternalCancelStep(exception);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002670 File Offset: 0x00000870
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.PushNotificationServiceTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "ServiceCommand.Complete: Complete with no exception called for ServiceCommand {0}. Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			try
			{
				this.InternalComplete(queueAndDelayTime, totalTime);
			}
			finally
			{
				this.Complete(null);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026C0 File Offset: 0x000008C0
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			TaskExecuteResult result = TaskExecuteResult.ProcessingComplete;
			try
			{
				this.asyncResult.Result = this.InternalExecute(queueAndDelayTime, totalTime);
			}
			catch (Exception ex)
			{
				this.CommandError = ex;
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "ServiceCommand.Execute: An Exception was reported from the InternalExecute call {0}.", ex.ToTraceString());
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000271C File Offset: 0x0000091C
		public IActivityScope GetActivityScope()
		{
			return ActivityContext.GetCurrentActivityScope();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002723 File Offset: 0x00000923
		public ResourceKey[] GetResources()
		{
			return this.InternalGetResources();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000272B File Offset: 0x0000092B
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.PushNotificationServiceTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "ServiceCommand.Timeout: Timeout called for ServiceCommoand {0}.  Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			this.InternalTimeout(queueAndDelayTime, totalTime);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002754 File Offset: 0x00000954
		public void Initialize(IBudget budget)
		{
			ArgumentValidator.ThrowIfNull("budget", budget);
			this.Budget = budget;
			this.asyncResult = new ServiceCommandAsyncResult<Response>(this.asyncCallback, this.asyncState);
			this.activityScope = ActivityContext.Start(ActivityType.Request);
			this.activityScope.Action = this.Description;
			this.activityScope.Component = WorkloadType.PushNotificationService.ToString();
			this.InternalInitialize(budget);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027CC File Offset: 0x000009CC
		public void Complete(Exception error = null)
		{
			if (!this.asyncResult.IsCompleted)
			{
				try
				{
					try
					{
						Exception ex = this.CommandError;
						if (error != null)
						{
							ex = ((ex != null) ? new AggregateException(new Exception[]
							{
								ex,
								error
							}) : error);
						}
						this.asyncResult.Complete(ex, false);
					}
					catch (InvalidOperationException exception)
					{
						ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "ServiceCommand.Complete: WCF request was already completed on another worker thread. {0}", exception.ToTraceString());
					}
					return;
				}
				finally
				{
					this.Budget.Dispose();
					this.activityScope.End();
				}
			}
			ExTraceGlobals.PushNotificationServiceTracer.TraceDebug((long)this.GetHashCode(), "ServiceCommand.Complete: WCF request was already completed on another worker thread.");
		}

		// Token: 0x0600003D RID: 61
		protected abstract ResourceKey[] InternalGetResources();

		// Token: 0x0600003E RID: 62
		protected abstract Response InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime);

		// Token: 0x0600003F RID: 63 RVA: 0x00002888 File Offset: 0x00000A88
		protected virtual void InternalInitialize(IBudget budget)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000288A File Offset: 0x00000A8A
		protected virtual void InternalCancel()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000288C File Offset: 0x00000A8C
		protected virtual void InternalComplete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000288E File Offset: 0x00000A8E
		protected virtual void InternalTimeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002890 File Offset: 0x00000A90
		protected virtual TaskExecuteResult InternalCancelStep(LocalizedException exception)
		{
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x04000018 RID: 24
		private static readonly TimeSpan DefaultMaxExecutionTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000019 RID: 25
		private static readonly WorkloadSettings WorkloadSettingsInstance = new WorkloadSettings(WorkloadType.PushNotificationService, false);

		// Token: 0x0400001A RID: 26
		private ServiceCommandAsyncResult<Response> asyncResult;

		// Token: 0x0400001B RID: 27
		private AsyncCallback asyncCallback;

		// Token: 0x0400001C RID: 28
		private object asyncState;

		// Token: 0x0400001D RID: 29
		private IActivityScope activityScope;
	}
}
