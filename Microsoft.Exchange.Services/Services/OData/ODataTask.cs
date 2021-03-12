using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E35 RID: 3637
	internal class ODataTask : ITask
	{
		// Token: 0x06005DBC RID: 23996 RVA: 0x00123CD0 File Offset: 0x00121ED0
		public ODataTask(ODataRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			this.Request = request;
			long num = 0L;
			if (request.ODataContext.RequestDetailsLogger.TryGetLatency(ServiceLatencyMetadata.HttpPipelineLatency, out num))
			{
				long num2 = (long)request.ODataContext.RequestDetailsLogger.ActivityScope.TotalMilliseconds - num;
				request.ODataContext.RequestDetailsLogger.UpdateLatency(ServiceLatencyMetadata.CheckAccessCoreLatency, (double)num2);
			}
			this.Description = request.GetOperationNameForLogging();
			this.WorkloadSettings = new WorkloadSettings(request.ODataContext.CallContext.WorkloadType, request.ODataContext.CallContext.BackgroundLoad);
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x00123DA0 File Offset: 0x00121FA0
		public static Task<ODataResponse> CreateTask(ODataRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ODataTask wlmTask = new ODataTask(request);
			if (!request.ODataContext.CallContext.WorkloadManager.TrySubmitNewTask(wlmTask))
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					wlmTask.Complete(new ServerBusyException());
				}, null);
			}
			return wlmTask.Task;
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x00123E0C File Offset: 0x0012200C
		// (set) Token: 0x06005DBF RID: 23999 RVA: 0x00123E14 File Offset: 0x00122014
		public ODataRequest Request { get; private set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x00123E1D File Offset: 0x0012201D
		// (set) Token: 0x06005DC1 RID: 24001 RVA: 0x00123E25 File Offset: 0x00122025
		public ODataResponse Response { get; private set; }

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x00123E2E File Offset: 0x0012202E
		public Task<ODataResponse> Task
		{
			get
			{
				return this.taskCompletionSource.Task;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x00123E3B File Offset: 0x0012203B
		public IBudget Budget
		{
			get
			{
				return this.Request.ODataContext.CallContext.Budget;
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x00123E52 File Offset: 0x00122052
		// (set) Token: 0x06005DC5 RID: 24005 RVA: 0x00123E5A File Offset: 0x0012205A
		public string Description { get; set; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00123E63 File Offset: 0x00122063
		public virtual TimeSpan MaxExecutionTime
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x00123E6A File Offset: 0x0012206A
		// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x00123E72 File Offset: 0x00122072
		public object State { get; set; }

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x00123E7B File Offset: 0x0012207B
		// (set) Token: 0x06005DCA RID: 24010 RVA: 0x00123E83 File Offset: 0x00122083
		public WorkloadSettings WorkloadSettings { get; set; }

		// Token: 0x06005DCB RID: 24011 RVA: 0x00123E8C File Offset: 0x0012208C
		public void Cancel()
		{
			this.Complete(new OperationCanceledException(this.Description));
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x00123E9F File Offset: 0x0012209F
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x00123EA2 File Offset: 0x001220A2
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Complete(null);
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x00123EAC File Offset: 0x001220AC
		public void Complete(Exception error = null)
		{
			Exception ex = error ?? this.executionException;
			if (ex != null)
			{
				this.taskCompletionSource.SetException(ex);
				return;
			}
			this.taskCompletionSource.SetResult(this.Response);
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x00123EE8 File Offset: 0x001220E8
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			try
			{
				HttpContext.Current = this.Request.ODataContext.HttpContext;
				this.Request.Validate();
				using (ODataCommand odataCommand = this.Request.GetODataCommand())
				{
					this.Response = (ODataResponse)odataCommand.Execute();
				}
			}
			catch (Exception ex)
			{
				this.executionException = ex;
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x00123F68 File Offset: 0x00122168
		public IActivityScope GetActivityScope()
		{
			return this.Request.ODataContext.RequestDetailsLogger.ActivityScope;
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x00123F7F File Offset: 0x0012217F
		public ResourceKey[] GetResources()
		{
			return null;
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x00123F82 File Offset: 0x00122182
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
		}

		// Token: 0x04003289 RID: 12937
		private TaskCompletionSource<ODataResponse> taskCompletionSource = new TaskCompletionSource<ODataResponse>();

		// Token: 0x0400328A RID: 12938
		private Exception executionException;
	}
}
