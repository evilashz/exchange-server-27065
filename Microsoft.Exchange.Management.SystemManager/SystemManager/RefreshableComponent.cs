using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000009 RID: 9
	[DefaultEvent("RefreshingChanged")]
	public class RefreshableComponent : Component, IRefreshableNotification, IRefreshable, ISupportInitializeNotification, ISupportInitialize
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002733 File Offset: 0x00000933
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000273B File Offset: 0x0000093B
		public ICloneable RefreshArgument
		{
			get
			{
				return this.refreshArgument;
			}
			set
			{
				this.refreshArgument = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002744 File Offset: 0x00000944
		protected virtual ICloneable DefaultRefreshArgument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002747 File Offset: 0x00000947
		private bool ShouldSerializeRefreshArgument()
		{
			return this.RefreshArgument != this.DefaultRefreshArgument;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000275A File Offset: 0x0000095A
		private void ResetRefreshArgument()
		{
			this.RefreshArgument = this.DefaultRefreshArgument;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002768 File Offset: 0x00000968
		protected object CloneRefreshArgument()
		{
			if (this.RefreshArgument == null)
			{
				return null;
			}
			return this.RefreshArgument.Clone();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000277F File Offset: 0x0000097F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002787 File Offset: 0x00000987
		[Browsable(false)]
		public bool Refreshing
		{
			get
			{
				return this.refreshing;
			}
			private set
			{
				if (this.Refreshing != value)
				{
					if (value)
					{
						this.Refreshed = true;
					}
					ExTraceGlobals.ProgramFlowTracer.TraceFunction<RefreshableComponent, bool>((long)this.GetHashCode(), "*--RefreshableComponent.Refreshing: {0}: {1}", this, value);
					this.refreshing = value;
					this.OnRefreshingChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027C8 File Offset: 0x000009C8
		protected virtual void OnRefreshingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RefreshableComponent.EventRefreshingChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003A RID: 58 RVA: 0x000027F6 File Offset: 0x000009F6
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002809 File Offset: 0x00000A09
		public event EventHandler RefreshingChanged
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventRefreshingChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventRefreshingChanged, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000281C File Offset: 0x00000A1C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002824 File Offset: 0x00000A24
		public bool Refreshed
		{
			get
			{
				return this.refreshed;
			}
			private set
			{
				this.refreshed = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002830 File Offset: 0x00000A30
		protected virtual void OnRefreshStarting(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[RefreshableComponent.EventRefreshStarting];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003F RID: 63 RVA: 0x0000285E File Offset: 0x00000A5E
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002871 File Offset: 0x00000A71
		public event CancelEventHandler RefreshStarting
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventRefreshStarting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventRefreshStarting, value);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002884 File Offset: 0x00000A84
		public void Refresh(IProgress progress)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnRefreshStarting(cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				RefreshRequestEventArgs refreshRequest = this.CreateFullRefreshRequest(progress);
				this.RefreshCore(progress, refreshRequest);
				return;
			}
			progress.ReportProgress(100, 100, string.Empty);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028C6 File Offset: 0x00000AC6
		protected virtual RefreshRequestEventArgs CreateFullRefreshRequest(IProgress progress)
		{
			return new RefreshRequestEventArgs(true, progress, this.CloneRefreshArgument());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028D8 File Offset: 0x00000AD8
		protected void RefreshCore(IProgress progress, RefreshRequestEventArgs refreshRequest)
		{
			ExTraceGlobals.ProgramFlowTracer.TracePerformance<RefreshableComponent, string>(0L, "Time: {1}. Start Refresh in UI thread. -->RefreshableComponent.RefreshCore: {0}. ", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"));
			if (!this.IsInitialized || this.IsInCriticalState)
			{
				progress.ReportProgress(100, 100, string.Empty);
				return;
			}
			this.EnqueueRequest(progress, refreshRequest);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002934 File Offset: 0x00000B34
		private void EnqueueRequest(IProgress progress, RefreshRequestEventArgs refreshRequest)
		{
			int num = 0;
			bool flag = false;
			foreach (RefreshRequestEventArgs refreshRequestEventArgs in this.refreshRequestQueue)
			{
				if (refreshRequest.Priority < refreshRequestEventArgs.Priority)
				{
					if (flag)
					{
						break;
					}
				}
				else if (refreshRequest.Priority == refreshRequestEventArgs.Priority && !refreshRequestEventArgs.Cancel)
				{
					PartialOrder partialOrder = this.ComparePartialOrder(refreshRequest, refreshRequestEventArgs);
					if (partialOrder == null || partialOrder == 1)
					{
						this.CancelRefresh(refreshRequestEventArgs);
					}
					else if (partialOrder == -1 && flag)
					{
						this.CancelRefresh(refreshRequest);
						break;
					}
				}
				num++;
				flag = true;
			}
			this.refreshRequestQueue.Insert(num, refreshRequest);
			if (this.refreshRequestQueue.Count == 1)
			{
				this.ProcessRequest(refreshRequest);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A00 File Offset: 0x00000C00
		protected virtual PartialOrder ComparePartialOrder(RefreshRequestEventArgs leftValue, RefreshRequestEventArgs rightValue)
		{
			PartialOrder result = int.MinValue;
			if (leftValue.IsFullRefresh)
			{
				result = (rightValue.IsFullRefresh ? 0 : 1);
			}
			else if (rightValue.IsFullRefresh)
			{
				result = -1;
			}
			else
			{
				PartialRefreshRequestEventArgs partialRefreshRequestEventArgs = leftValue as PartialRefreshRequestEventArgs;
				PartialRefreshRequestEventArgs partialRefreshRequestEventArgs2 = rightValue as PartialRefreshRequestEventArgs;
				if (!partialRefreshRequestEventArgs.Identities.IsEmptyCollection() && !partialRefreshRequestEventArgs2.Identities.IsEmptyCollection())
				{
					if (partialRefreshRequestEventArgs.Identities.Length > partialRefreshRequestEventArgs2.Identities.Length)
					{
						if (RefreshableComponent.IsSubsetOf(partialRefreshRequestEventArgs.Identities, partialRefreshRequestEventArgs2.Identities))
						{
							result = 1;
						}
					}
					else if (RefreshableComponent.IsSubsetOf(partialRefreshRequestEventArgs2.Identities, partialRefreshRequestEventArgs.Identities))
					{
						result = -1;
					}
				}
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private static bool IsSubsetOf(object[] objectsSet, object[] objectsSubset)
		{
			foreach (object value in objectsSubset)
			{
				if (Array.IndexOf<object>(objectsSet, value) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void ProcessRequest(RefreshRequestEventArgs refreshRequest)
		{
			BackgroundWorker backgroundWorker = refreshRequest.BackgroundWorker;
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.WorkerSupportsCancellation = true;
			backgroundWorker.DoWork += this.worker_DoWork;
			backgroundWorker.RunWorkerCompleted += this.worker_RunWorkerCompleted;
			backgroundWorker.ProgressChanged += this.worker_ProgressChanged;
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			backgroundWorker.RunWorkerAsync(refreshRequest);
			this.Refreshing = true;
			this.OnRefreshStarted(EventArgs.Empty);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B50 File Offset: 0x00000D50
		protected virtual void OnRefreshStarted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RefreshableComponent.EventRefreshStarted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000049 RID: 73 RVA: 0x00002B7E File Offset: 0x00000D7E
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00002B91 File Offset: 0x00000D91
		public event EventHandler RefreshStarted
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventRefreshStarted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventRefreshStarted, value);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public void CancelRefresh()
		{
			foreach (RefreshRequestEventArgs refreshRequest in this.refreshRequestQueue)
			{
				this.CancelRefresh(refreshRequest);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private void CancelRefresh(RefreshRequestEventArgs refreshRequest)
		{
			if (refreshRequest.CancellationPending || refreshRequest.Cancel)
			{
				return;
			}
			if (refreshRequest.BackgroundWorker.IsBusy)
			{
				refreshRequest.BackgroundWorker.CancelAsync();
				return;
			}
			refreshRequest.Cancel = true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C2C File Offset: 0x00000E2C
		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TracePerformance<RefreshableComponent, string, string>(0L, "Time:{1}. Start Refresh {2} in worker thread. -->RefreshableComponent.worker_DoWork: {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), (this is DataTableLoader) ? (this as DataTableLoader).Table.TableName : string.Empty);
			RefreshRequestEventArgs refreshRequestEventArgs = (RefreshRequestEventArgs)e.Argument;
			try
			{
				this.OnDoRefreshWork(refreshRequestEventArgs);
			}
			finally
			{
				refreshRequestEventArgs.ShellProgress.ReportProgress(100, 100, "");
			}
			e.Result = refreshRequestEventArgs.Result;
			e.Cancel = refreshRequestEventArgs.CancellationPending;
			ExTraceGlobals.ProgramFlowTracer.TracePerformance<RefreshableComponent, string, string>(0L, "Time:{1}. End Refresh {2} in worker thread. <--RefreshableComponent.worker_DoWork: {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), (this is DataTableLoader) ? (this as DataTableLoader).Table.TableName : string.Empty);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D14 File Offset: 0x00000F14
		protected virtual void OnDoRefreshWork(RefreshRequestEventArgs e)
		{
			RefreshRequestEventHandler refreshRequestEventHandler = (RefreshRequestEventHandler)base.Events[RefreshableComponent.EventDoRefreshWork];
			if (refreshRequestEventHandler != null)
			{
				refreshRequestEventHandler(this, e);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004F RID: 79 RVA: 0x00002D42 File Offset: 0x00000F42
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00002D55 File Offset: 0x00000F55
		public event RefreshRequestEventHandler DoRefreshWork
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventDoRefreshWork, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventDoRefreshWork, value);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D68 File Offset: 0x00000F68
		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			RefreshProgressChangedEventArgs refreshProgressChangedEventArgs = (RefreshProgressChangedEventArgs)e.UserState;
			this.OnProgressChanged(refreshProgressChangedEventArgs);
			if (refreshProgressChangedEventArgs.IsFirstProgressReport)
			{
				ExTraceGlobals.ProgramFlowTracer.TracePerformance<RefreshableComponent, string, string>(0L, "Time:{1}. {2} First batch data arrived in UI thread. {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), (this is DataTableLoader) ? (this as DataTableLoader).Table.TableName : string.Empty);
				this.OnFirstBatchDataArrived(EventArgs.Empty);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002E18 File Offset: 0x00001018
		public event EventHandler FirstBatchDataArrived;

		// Token: 0x06000054 RID: 84 RVA: 0x00002E4D File Offset: 0x0000104D
		private void OnFirstBatchDataArrived(EventArgs e)
		{
			if (this.FirstBatchDataArrived != null)
			{
				this.FirstBatchDataArrived(this, e);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E64 File Offset: 0x00001064
		protected virtual void OnProgressChanged(RefreshProgressChangedEventArgs e)
		{
			RefreshProgressChangedEventHandler refreshProgressChangedEventHandler = (RefreshProgressChangedEventHandler)base.Events[RefreshableComponent.EventProgressChanged];
			if (refreshProgressChangedEventHandler != null)
			{
				refreshProgressChangedEventHandler(this, e);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000056 RID: 86 RVA: 0x00002E92 File Offset: 0x00001092
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x00002EA5 File Offset: 0x000010A5
		public event RefreshProgressChangedEventHandler ProgressChanged
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventProgressChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventProgressChanged, value);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EB8 File Offset: 0x000010B8
		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			RefreshRequestEventArgs refreshRequestEventArgs = this.refreshRequestQueue[0];
			if (!refreshRequestEventArgs.Cancel)
			{
				this.DoPostRefreshAction(refreshRequestEventArgs);
			}
			this.refreshRequestQueue.RemoveAt(0);
			List<RefreshRequestEventArgs> list = new List<RefreshRequestEventArgs>();
			while (this.refreshRequestQueue.Count > 0 && this.refreshRequestQueue[0].Cancel)
			{
				list.Add(this.refreshRequestQueue[0]);
				this.refreshRequestQueue.RemoveAt(0);
			}
			if (this.refreshRequestQueue.Count > 0)
			{
				this.ProcessRequest(this.refreshRequestQueue[0]);
			}
			else
			{
				this.Refreshing = false;
			}
			this.isInCriticalState = true;
			try
			{
				if (e.Error != null && this.UIService != null)
				{
					this.UIService.ShowError(e.Error);
				}
			}
			finally
			{
				this.isInCriticalState = false;
			}
			this.OnRefreshCompleted(e);
			foreach (RefreshRequestEventArgs refreshRequestEventArgs2 in list)
			{
				refreshRequestEventArgs2.ShellProgress.ReportProgress(100, 100, "");
				this.OnRefreshCompleted(new RunWorkerCompletedEventArgs(null, null, true));
			}
			ExTraceGlobals.ProgramFlowTracer.TracePerformance<RefreshableComponent, string>(0L, "Time:{1}. End Refresh in UI thread. <--RefreshableComponent.worker_RunWorkerCompleted: {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003024 File Offset: 0x00001224
		protected virtual void DoPostRefreshAction(RefreshRequestEventArgs refreshRequest)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003026 File Offset: 0x00001226
		protected IUIService UIService
		{
			get
			{
				return (IUIService)this.GetService(typeof(IUIService));
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000303D File Offset: 0x0000123D
		private bool IsInCriticalState
		{
			get
			{
				return this.isInCriticalState;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003048 File Offset: 0x00001248
		protected virtual void OnRefreshCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = (RunWorkerCompletedEventHandler)base.Events[RefreshableComponent.EventRefreshCompleted];
			if (runWorkerCompletedEventHandler != null)
			{
				runWorkerCompletedEventHandler(this, e);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00003076 File Offset: 0x00001276
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00003089 File Offset: 0x00001289
		public event RunWorkerCompletedEventHandler RefreshCompleted
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventRefreshCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventRefreshCompleted, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000309C File Offset: 0x0000129C
		public bool IsInitialized
		{
			get
			{
				return !this.initializing;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000030A7 File Offset: 0x000012A7
		public void BeginInit()
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<RefreshableComponent>((long)this.GetHashCode(), "*--RefreshableComponent.BeginInit: {0}", this);
			this.initializing = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030C7 File Offset: 0x000012C7
		public void EndInit()
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<RefreshableComponent>((long)this.GetHashCode(), "*--RefreshableComponent.EndInit: {0}", this);
			this.initializing = false;
			this.OnInitialized(EventArgs.Empty);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030F4 File Offset: 0x000012F4
		protected virtual void OnInitialized(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RefreshableComponent.EventInitialized];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000063 RID: 99 RVA: 0x00003122 File Offset: 0x00001322
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x00003135 File Offset: 0x00001335
		public event EventHandler Initialized
		{
			add
			{
				base.Events.AddHandler(RefreshableComponent.EventInitialized, value);
			}
			remove
			{
				base.Events.RemoveHandler(RefreshableComponent.EventInitialized, value);
			}
		}

		// Token: 0x0400000F RID: 15
		private ICloneable refreshArgument;

		// Token: 0x04000010 RID: 16
		private bool refreshing;

		// Token: 0x04000011 RID: 17
		private bool initializing;

		// Token: 0x04000012 RID: 18
		private bool isInCriticalState;

		// Token: 0x04000013 RID: 19
		private static readonly object EventRefreshingChanged = new object();

		// Token: 0x04000014 RID: 20
		private bool refreshed;

		// Token: 0x04000015 RID: 21
		private static readonly object EventRefreshStarting = new object();

		// Token: 0x04000016 RID: 22
		private List<RefreshRequestEventArgs> refreshRequestQueue = new List<RefreshRequestEventArgs>();

		// Token: 0x04000017 RID: 23
		private static readonly object EventRefreshStarted = new object();

		// Token: 0x04000018 RID: 24
		private static readonly object EventDoRefreshWork = new object();

		// Token: 0x0400001A RID: 26
		private static readonly object EventProgressChanged = new object();

		// Token: 0x0400001B RID: 27
		private static readonly object EventRefreshCompleted = new object();

		// Token: 0x0400001C RID: 28
		private static readonly object EventInitialized = new object();
	}
}
