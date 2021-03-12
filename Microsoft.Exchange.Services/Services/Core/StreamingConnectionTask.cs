using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200023C RID: 572
	internal class StreamingConnectionTask : ITask, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0004A058 File Offset: 0x00048258
		internal StreamingConnectionTask(StreamingConnection connection, CallContext callContext, StreamingConnectionTask.StreamingConnectionExecuteDelegate executeDelegate, string taskType)
		{
			this.connection = connection;
			this.callContext = callContext;
			this.executeDelegate = executeDelegate;
			this.Description = string.Format("StreamingConnectionTask: Connection: [{0}], Type:[{1}]", this.connection.GetHashCode(), taskType);
			this.WorkloadSettings = new WorkloadSettings(this.callContext.WorkloadType, this.callContext.BackgroundLoad);
			this.budget = EwsBudget.Acquire(this.callContext.Budget.Owner);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0004A0EA File Offset: 0x000482EA
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamingConnectionTask>(this);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0004A0F2 File Offset: 0x000482F2
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0004A107 File Offset: 0x00048307
		public IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0004A10F File Offset: 0x0004830F
		public void Cancel()
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[StreamingConnectionTask.Cancel] Task.Cancel called for task {0}", this.Description);
			this.Dispose();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0004A133 File Offset: 0x00048333
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Dispose();
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0004A13B File Offset: 0x0004833B
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0004A143 File Offset: 0x00048343
		public string Description { get; set; }

		// Token: 0x06000EFA RID: 3834 RVA: 0x0004A14C File Offset: 0x0004834C
		public IActivityScope GetActivityScope()
		{
			IActivityScope result = null;
			if (this.callContext != null && this.callContext.ProtocolLog != null)
			{
				result = this.callContext.ProtocolLog.ActivityScope;
			}
			return result;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0004A184 File Offset: 0x00048384
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			TaskExecuteResult result;
			try
			{
				CallContext.SetCurrent(this.callContext);
				result = this.executeDelegate();
			}
			finally
			{
				CallContext.SetCurrent(null);
			}
			return result;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0004A1C4 File Offset: 0x000483C4
		public TimeSpan MaxExecutionTime
		{
			get
			{
				return StreamingConnectionTask.DefaultMaxExecutionTime;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0004A1CB File Offset: 0x000483CB
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0004A1D3 File Offset: 0x000483D3
		public object State { get; set; }

		// Token: 0x06000EFF RID: 3839 RVA: 0x0004A1DC File Offset: 0x000483DC
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.Dispose();
			this.connection.TryEndConnection(false);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0004A1F0 File Offset: 0x000483F0
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			this.connection.TryEndConnection(exception);
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0004A1FF File Offset: 0x000483FF
		public ResourceKey[] GetResources()
		{
			return null;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0004A202 File Offset: 0x00048402
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0004A20A File Offset: 0x0004840A
		public WorkloadSettings WorkloadSettings { get; private set; }

		// Token: 0x06000F04 RID: 3844 RVA: 0x0004A213 File Offset: 0x00048413
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0004A22F File Offset: 0x0004842F
		private void Dispose(bool suppressFinalize)
		{
			if (!this.isDisposed)
			{
				if (suppressFinalize)
				{
					GC.SuppressFinalize(this);
				}
				if (this.budget != null)
				{
					this.budget.LogEndStateToIIS();
					this.budget.Dispose();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000B84 RID: 2948
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000B85 RID: 2949
		private static readonly TimeSpan DefaultMaxExecutionTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000B86 RID: 2950
		private StreamingConnection connection;

		// Token: 0x04000B87 RID: 2951
		private CallContext callContext;

		// Token: 0x04000B88 RID: 2952
		private StreamingConnectionTask.StreamingConnectionExecuteDelegate executeDelegate;

		// Token: 0x04000B89 RID: 2953
		private IEwsBudget budget;

		// Token: 0x04000B8A RID: 2954
		private bool isDisposed;

		// Token: 0x0200023D RID: 573
		// (Invoke) Token: 0x06000F08 RID: 3848
		internal delegate TaskExecuteResult StreamingConnectionExecuteDelegate();
	}
}
