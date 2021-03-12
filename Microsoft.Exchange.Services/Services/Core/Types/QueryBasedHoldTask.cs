using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A0 RID: 1696
	internal class QueryBasedHoldTask : ITask, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003438 RID: 13368 RVA: 0x000BC190 File Offset: 0x000BA390
		internal QueryBasedHoldTask(CallContext callContext, MailboxDiscoverySearch discoverySearch, DiscoverySearchDataProvider discoverySearchDataProvider, HoldAction actionType, IRecipientSession recipientSession)
		{
			this.CallContext = callContext;
			this.WorkloadSettings = new WorkloadSettings(WorkloadType.Ews, false);
			this.budget = EwsBudget.Acquire(callContext.Budget.Owner);
			this.DiscoverySearch = discoverySearch;
			this.DiscoverySearchDataProvider = discoverySearchDataProvider;
			this.ActionType = actionType;
			this.disposeTracker = this.GetDisposeTracker();
			if (!recipientSession.ReadOnly)
			{
				this.RecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, recipientSession.SessionSettings, 74, ".ctor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\WorkloadManager\\QueryBasedHoldTask.cs");
				return;
			}
			this.RecipientSession = recipientSession;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000BC227 File Offset: 0x000BA427
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			this.Dispose(true);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000BC24A File Offset: 0x000BA44A
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<QueryBasedHoldTask>(this);
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000BC252 File Offset: 0x000BA452
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000BC267 File Offset: 0x000BA467
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x000BC26F File Offset: 0x000BA46F
		internal CallContext CallContext { get; private set; }

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000BC278 File Offset: 0x000BA478
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x000BC280 File Offset: 0x000BA480
		internal MailboxDiscoverySearch DiscoverySearch { get; private set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000BC289 File Offset: 0x000BA489
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x000BC291 File Offset: 0x000BA491
		internal DiscoverySearchDataProvider DiscoverySearchDataProvider { get; private set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000BC29A File Offset: 0x000BA49A
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x000BC2A2 File Offset: 0x000BA4A2
		internal HoldAction ActionType { get; private set; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000BC2AB File Offset: 0x000BA4AB
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x000BC2B3 File Offset: 0x000BA4B3
		internal IRecipientSession RecipientSession { get; private set; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x000BC2BC File Offset: 0x000BA4BC
		// (set) Token: 0x06003447 RID: 13383 RVA: 0x000BC2C4 File Offset: 0x000BA4C4
		public object State { get; set; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000BC2CD File Offset: 0x000BA4CD
		// (set) Token: 0x06003449 RID: 13385 RVA: 0x000BC2D5 File Offset: 0x000BA4D5
		public string Description { get; set; }

		// Token: 0x0600344A RID: 13386 RVA: 0x000BC2DE File Offset: 0x000BA4DE
		public void Cancel()
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[QueryBasedHoldTask.Cancel] Cancel called for task {0}", this.Description);
			this.Dispose();
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000BC304 File Offset: 0x000BA504
		public IActivityScope GetActivityScope()
		{
			IActivityScope result = null;
			if (this.CallContext != null && this.CallContext.ProtocolLog != null)
			{
				result = this.CallContext.ProtocolLog.ActivityScope;
			}
			return result;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000BC3C4 File Offset: 0x000BA5C4
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			TaskExecuteResult result = TaskExecuteResult.ProcessingComplete;
			this.SendWatsonReportOnGrayException(delegate
			{
				this.DiscoverySearch.SynchronizeHoldSettings(this.DiscoverySearchDataProvider, this.RecipientSession, true);
				if (this.DiscoverySearch.Sources == null || this.DiscoverySearch.Sources.Count == 0)
				{
					this.DiscoverySearchDataProvider.Delete(this.DiscoverySearch);
				}
				result = TaskExecuteResult.ProcessingComplete;
			});
			return result;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000BC3FD File Offset: 0x000BA5FD
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000BC405 File Offset: 0x000BA605
		public WorkloadSettings WorkloadSettings { get; private set; }

		// Token: 0x0600344F RID: 13391 RVA: 0x000BC40E File Offset: 0x000BA60E
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[QueryBasedHoldTask.Complete] Complete with no exception called for task {0}.  Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			this.Dispose();
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000BC434 File Offset: 0x000BA634
		public IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000BC43C File Offset: 0x000BA63C
		public TimeSpan MaxExecutionTime
		{
			get
			{
				return QueryBasedHoldTask.DefaultMaxExecutionTime;
			}
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000BC443 File Offset: 0x000BA643
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[QueryBasedHoldTask.Timeout] Timeout called for task {0}.  Delay: {1}, Elapsed: {2}", this.Description, queueAndDelayTime, totalTime);
			this.Dispose();
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000BC469 File Offset: 0x000BA669
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[QueryBasedHoldTask.CancelStep] Current execution step for task {0} is cancelled with exception: {1}", this.Description, exception.ToString());
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000BC48E File Offset: 0x000BA68E
		public ResourceKey[] GetResources()
		{
			return null;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000BC494 File Offset: 0x000BA694
		private static bool GrayExceptionFilter(object exception)
		{
			bool flag = false;
			Exception ex = exception as Exception;
			if (ex != null && ExWatson.IsWatsonReportAlreadySent(ex))
			{
				flag = true;
			}
			bool flag2 = GrayException.ExceptionFilter(exception);
			if (flag2 && !flag && ex != null)
			{
				ExWatson.SetWatsonReportAlreadySent(ex);
			}
			return flag2;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000BC4E8 File Offset: 0x000BA6E8
		private void SendWatsonReportOnGrayException(QueryBasedHoldTask.GrayExceptionCallback callback)
		{
			Exception ex = null;
			string formatString = null;
			ServiceDiagnostics.RegisterAdditionalWatsonData();
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					callback();
				}, new GrayException.ExceptionFilterDelegate(QueryBasedHoldTask.GrayExceptionFilter));
			}
			catch (GrayException ex2)
			{
				ex = ex2;
				formatString = "Task {0} failed: {1}";
				if (this.Budget != null)
				{
					UserWorkloadManager.GetPerfCounterWrapper(this.Budget.Owner.BudgetType).UpdateTotalTaskExecutionFailuresCount();
				}
			}
			finally
			{
				ExWatson.ClearReportActions(WatsonActionScope.Thread);
			}
			if (ex != null)
			{
				ExTraceGlobals.ThrottlingTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), formatString, this.Description, ex);
			}
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000BC5A4 File Offset: 0x000BA7A4
		private void Dispose(bool suppressFinalize)
		{
			if (!this.disposed)
			{
				if (suppressFinalize)
				{
					GC.SuppressFinalize(this);
				}
				if (this.budget != null)
				{
					this.budget.LogEndStateToIIS();
					this.budget.Dispose();
					this.budget = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001D8A RID: 7562
		private static readonly TimeSpan DefaultMaxExecutionTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x04001D8B RID: 7563
		private bool disposed;

		// Token: 0x04001D8C RID: 7564
		private IEwsBudget budget;

		// Token: 0x04001D8D RID: 7565
		private DisposeTracker disposeTracker;

		// Token: 0x020006A1 RID: 1697
		// (Invoke) Token: 0x0600345A RID: 13402
		internal delegate void GrayExceptionCallback();
	}
}
