using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000030 RID: 48
	public abstract class CoordinatedRecoveryAction
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00004D6C File Offset: 0x00002F6C
		public CoordinatedRecoveryAction(RecoveryActionId actionId, string requester, int minimumRequiredTobeInReadyState, int maximumConcurrentActionsAllowed, string[] resources)
		{
			this.CancellationTokenSource = new CancellationTokenSource();
			this.TraceContext = TracingContext.Default;
			this.Requester = requester;
			this.Resources = resources;
			this.statusMap = new Dictionary<string, ResourceAvailabilityStatus>(resources.Length);
			foreach (string key in resources)
			{
				this.statusMap.Add(key, ResourceAvailabilityStatus.Unknown);
			}
			this.ActionId = actionId;
			this.MinimumRequiredTobeInReadyState = minimumRequiredTobeInReadyState;
			this.MaximumConcurrentActionsAllowed = maximumConcurrentActionsAllowed;
			WTFDiagnostics.TraceDebug<string, string, int, int, int>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "Created an instance of {0}. (requester: {1}, resourceCount: {2}, readyMinimum: {3}, concurrentMax: {4})", base.GetType().Name, this.Requester, this.statusMap.Count, this.MinimumRequiredTobeInReadyState, this.MaximumConcurrentActionsAllowed, null, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 126);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004E40 File Offset: 0x00003040
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00004E48 File Offset: 0x00003048
		public RecoveryActionId ActionId { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004E51 File Offset: 0x00003051
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004E59 File Offset: 0x00003059
		internal int MinimumRequiredTobeInReadyState { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004E62 File Offset: 0x00003062
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004E6A File Offset: 0x0000306A
		internal int MaximumConcurrentActionsAllowed { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004E73 File Offset: 0x00003073
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004E7B File Offset: 0x0000307B
		internal string Requester { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004E84 File Offset: 0x00003084
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00004E8C File Offset: 0x0000308C
		internal string[] Resources { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004E95 File Offset: 0x00003095
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00004E9D File Offset: 0x0000309D
		private protected TracingContext TraceContext { protected get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004EA6 File Offset: 0x000030A6
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00004EAE File Offset: 0x000030AE
		private protected CancellationTokenSource CancellationTokenSource { protected get; private set; }

		// Token: 0x06000164 RID: 356 RVA: 0x00005030 File Offset: 0x00003230
		public void Execute(TimeSpan arbitrationTimeout, Action<CoordinatedRecoveryAction.ResourceAvailabilityStatistics> action)
		{
			CoordinatedRecoveryAction.ResourceAvailabilityStatistics stats = null;
			RecoveryActionThrottlingMode throttlingMode = RecoveryActionHelper.GetRecoveryActionDistributedThrottlingMode(this.ActionId, RecoveryActionThrottlingMode.None);
			if (throttlingMode == RecoveryActionThrottlingMode.None)
			{
				this.totalRequests = this.statusMap.Count;
				Task[] tasks = new Task[this.totalRequests];
				int num = 0;
				bool isParallelMode = true;
				object serialExecutionLock = new object();
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "{0}: Initiating arbitration", base.GetType().Name, null, "Execute", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 192);
				string[] array = this.statusMap.Keys.ToArray<string>();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string resourceName2 = array2[i];
					string resourceName = resourceName2;
					Task task = Task.Factory.StartNew(delegate()
					{
						if (isParallelMode)
						{
							this.ExecutePerResource(resourceName);
							return;
						}
						object serialExecutionLock;
						lock (serialExecutionLock)
						{
							this.ExecutePerResource(resourceName);
						}
					});
					tasks[num++] = task;
				}
				bool isTimedout = false;
				RecoveryActionHelper.RunAndMeasure(string.Format("WaitAll (resourceCount: {0} timeout: {1})", tasks.Length, arbitrationTimeout), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
				{
					isTimedout = Task.WaitAll(tasks, (int)arbitrationTimeout.TotalMilliseconds, this.CancellationTokenSource.Token);
					if (isTimedout)
					{
						WTFDiagnostics.TraceDebug(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "WaitAll() timed out", null, "Execute", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 240);
					}
					return string.Format("isTimeout = {0}", isTimedout);
				});
				lock (this.locker)
				{
					this.isMajorityCheckCompleted = true;
				}
				stats = this.GetConsolidatedStatistics();
				this.EnsureArbitrationSucceeeded(stats);
			}
			RecoveryActionHelper.RunAndMeasure(string.Format("Running the coordinated action of {0} - Throttling mode: {1}", base.GetType().Name, throttlingMode), true, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
			{
				if (throttlingMode != RecoveryActionThrottlingMode.ForceFail)
				{
					action(stats);
					return string.Empty;
				}
				throw new DistributedThrottlingRejectedOperationException(this.ActionId.ToString(), this.Requester);
			});
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005244 File Offset: 0x00003444
		public virtual void EnsureArbitrationSucceeeded(CoordinatedRecoveryAction.ResourceAvailabilityStatistics stats)
		{
			Exception ex = null;
			if (stats.TotalReady < this.MinimumRequiredTobeInReadyState)
			{
				WTFDiagnostics.TraceError<int, int, int, int>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "Arbitration failed since number of ready resoures not meeting the minimum requirement. (ready={0}, minimumReady={1}, concurrent={2}, maximumConcurrent={3})", stats.TotalReady, this.MinimumRequiredTobeInReadyState, stats.TotalArbitrating, this.MaximumConcurrentActionsAllowed, null, "EnsureArbitrationSucceeeded", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 291);
				ex = new ArbitrationMinimumRequiredReadyNotSatisfiedException(stats.TotalReady, this.MinimumRequiredTobeInReadyState);
			}
			else if (stats.TotalArbitrating > this.MaximumConcurrentActionsAllowed)
			{
				WTFDiagnostics.TraceError<int, int, int, int>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "Arbitration failed since number of concurrent operations exceeded the maximum requirement. (ready={0}, minimumReady={1}, concurrent={2}, maximumConcurrent={3})", stats.TotalReady, this.MinimumRequiredTobeInReadyState, stats.TotalArbitrating, this.MaximumConcurrentActionsAllowed, null, "EnsureArbitrationSucceeeded", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 306);
				ex = new ArbitrationMaximumAllowedConcurrentNotSatisfiedException(stats.TotalArbitrating, this.MaximumConcurrentActionsAllowed);
			}
			else
			{
				WTFDiagnostics.TraceDebug<int, int, int, int>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "Arbitration succeeded. (ready={0}, minimumReady={1}, concurrent={2}, maximumConcurrent={3})", stats.TotalReady, this.MinimumRequiredTobeInReadyState, stats.TotalArbitrating, this.MaximumConcurrentActionsAllowed, null, "EnsureArbitrationSucceeeded", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 321);
			}
			string text = string.Join(",", stats.ReadyResources);
			string text2 = string.Join(",", stats.ArbitratingResources);
			string text3 = string.Join(",", stats.InMaintenanceResources);
			string text4 = string.Join(",", stats.UnknownResources);
			string text5 = string.Join(",", stats.OfflineResources);
			ManagedAvailabilityCrimsonEvent managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ArbitrationSucceeded;
			if (ex != null)
			{
				managedAvailabilityCrimsonEvent = ManagedAvailabilityCrimsonEvents.ArbitrationFailed;
			}
			managedAvailabilityCrimsonEvent.LogGeneric(new object[]
			{
				base.GetType().Name,
				this.Requester,
				stats.TotalRequested,
				stats.TotalReady,
				stats.TotalArbitrating,
				stats.TotalMaintenance,
				stats.TotalUnknown,
				this.MinimumRequiredTobeInReadyState,
				this.MaximumConcurrentActionsAllowed,
				text,
				text2,
				text3,
				text4,
				(ex != null) ? ex.Message : string.Empty,
				stats.TotalOffline,
				text5
			});
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005498 File Offset: 0x00003698
		internal string GetShortName(string fqdn)
		{
			string[] array = fqdn.Split(new char[]
			{
				'.'
			});
			return array[0];
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000054BC File Offset: 0x000036BC
		internal CoordinatedRecoveryAction.ResourceAvailabilityStatistics GetConsolidatedStatistics()
		{
			CoordinatedRecoveryAction.ResourceAvailabilityStatistics resourceAvailabilityStatistics = new CoordinatedRecoveryAction.ResourceAvailabilityStatistics();
			resourceAvailabilityStatistics.TotalRequested = this.statusMap.Count;
			resourceAvailabilityStatistics.ReadyResources = new List<string>();
			resourceAvailabilityStatistics.ArbitratingResources = new List<string>();
			resourceAvailabilityStatistics.UnknownResources = new List<string>();
			resourceAvailabilityStatistics.InMaintenanceResources = new List<string>();
			resourceAvailabilityStatistics.OfflineResources = new List<string>();
			foreach (KeyValuePair<string, ResourceAvailabilityStatus> keyValuePair in this.statusMap)
			{
				switch (keyValuePair.Value)
				{
				case ResourceAvailabilityStatus.Ready:
					resourceAvailabilityStatistics.TotalReady++;
					resourceAvailabilityStatistics.ReadyResources.Add(this.GetShortName(keyValuePair.Key));
					continue;
				case ResourceAvailabilityStatus.Arbitrating:
					resourceAvailabilityStatistics.TotalArbitrating++;
					resourceAvailabilityStatistics.ArbitratingResources.Add(this.GetShortName(keyValuePair.Key));
					continue;
				case ResourceAvailabilityStatus.Maintenance:
					resourceAvailabilityStatistics.TotalMaintenance++;
					resourceAvailabilityStatistics.InMaintenanceResources.Add(this.GetShortName(keyValuePair.Key));
					continue;
				case ResourceAvailabilityStatus.Offline:
					resourceAvailabilityStatistics.TotalOffline++;
					resourceAvailabilityStatistics.OfflineResources.Add(this.GetShortName(keyValuePair.Key));
					continue;
				}
				resourceAvailabilityStatistics.TotalUnknown++;
				resourceAvailabilityStatistics.UnknownResources.Add(this.GetShortName(keyValuePair.Key));
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, string.Format("GetConsolidatedStatistics(). (Requested: {0} Ready: {1}, Busy: {2}, Maintenance: {3}, Unknown: {4}, Offline:{5})", new object[]
			{
				resourceAvailabilityStatistics.TotalRequested,
				resourceAvailabilityStatistics.TotalReady,
				resourceAvailabilityStatistics.TotalArbitrating,
				resourceAvailabilityStatistics.TotalMaintenance,
				resourceAvailabilityStatistics.TotalUnknown,
				resourceAvailabilityStatistics.TotalOffline
			}), null, "GetConsolidatedStatistics", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 425);
			return resourceAvailabilityStatistics;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005740 File Offset: 0x00003940
		internal void ExecutePerResource(string resourceName)
		{
			WTFDiagnostics.TraceDebug<string, int>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "RunCheck: Task started for {0} (ManagedThreadId: {1})", resourceName, Thread.CurrentThread.ManagedThreadId, null, "ExecutePerResource", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 446);
			lock (this.locker)
			{
				if (this.IsArbitrationCompleted())
				{
					WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.RecoveryActionTracer, this.TraceContext, "Skipping the work since completion is marked. (resourceName: {0})", resourceName, null, "ExecutePerResource", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\CoordinatedRecoveryAction.cs", 457);
					return;
				}
			}
			ResourceAvailabilityStatus status = ResourceAvailabilityStatus.Unknown;
			RecoveryActionHelper.RunAndMeasure(string.Format("RunCheck({0})", resourceName), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
			{
				string text = string.Empty;
				status = this.RunCheck(resourceName, out text);
				text = string.Format("{0} => {1} :", resourceName, status) + text;
				return text;
			});
			this.UpdateStatus(resourceName, status);
			if (this.IsArbitrationCompleted() && !this.CancellationTokenSource.IsCancellationRequested)
			{
				this.CancellationTokenSource.Cancel();
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000585C File Offset: 0x00003A5C
		protected virtual bool IsArbitrationCompleted()
		{
			return this.isMajorityCheckCompleted;
		}

		// Token: 0x0600016A RID: 362
		protected abstract ResourceAvailabilityStatus RunCheck(string resourceName, out string debugInfo);

		// Token: 0x0600016B RID: 363 RVA: 0x00005864 File Offset: 0x00003A64
		private void UpdateStatus(string resourceName, ResourceAvailabilityStatus status)
		{
			lock (this.locker)
			{
				if (!this.IsArbitrationCompleted())
				{
					this.statusMap[resourceName] = status;
					if (status == ResourceAvailabilityStatus.Arbitrating)
					{
						this.inArbitrationCount++;
						if (this.inArbitrationCount > this.MaximumConcurrentActionsAllowed)
						{
							this.isMajorityCheckCompleted = true;
						}
					}
					else if (status == ResourceAvailabilityStatus.Ready)
					{
						this.readyCount++;
						if (this.readyCount >= this.MinimumRequiredTobeInReadyState)
						{
							this.isMajorityCheckCompleted = true;
						}
					}
				}
			}
		}

		// Token: 0x040000BE RID: 190
		private object locker = new object();

		// Token: 0x040000BF RID: 191
		private Dictionary<string, ResourceAvailabilityStatus> statusMap;

		// Token: 0x040000C0 RID: 192
		private int totalRequests;

		// Token: 0x040000C1 RID: 193
		private int readyCount;

		// Token: 0x040000C2 RID: 194
		private int inArbitrationCount;

		// Token: 0x040000C3 RID: 195
		private bool isMajorityCheckCompleted;

		// Token: 0x02000031 RID: 49
		public class ResourceAvailabilityStatistics
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600016C RID: 364 RVA: 0x00005904 File Offset: 0x00003B04
			// (set) Token: 0x0600016D RID: 365 RVA: 0x0000590C File Offset: 0x00003B0C
			internal int TotalRequested { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x0600016E RID: 366 RVA: 0x00005915 File Offset: 0x00003B15
			// (set) Token: 0x0600016F RID: 367 RVA: 0x0000591D File Offset: 0x00003B1D
			internal int TotalReady { get; set; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000170 RID: 368 RVA: 0x00005926 File Offset: 0x00003B26
			// (set) Token: 0x06000171 RID: 369 RVA: 0x0000592E File Offset: 0x00003B2E
			internal int TotalArbitrating { get; set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000172 RID: 370 RVA: 0x00005937 File Offset: 0x00003B37
			// (set) Token: 0x06000173 RID: 371 RVA: 0x0000593F File Offset: 0x00003B3F
			internal int TotalUnknown { get; set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000174 RID: 372 RVA: 0x00005948 File Offset: 0x00003B48
			// (set) Token: 0x06000175 RID: 373 RVA: 0x00005950 File Offset: 0x00003B50
			internal int TotalMaintenance { get; set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000176 RID: 374 RVA: 0x00005959 File Offset: 0x00003B59
			// (set) Token: 0x06000177 RID: 375 RVA: 0x00005961 File Offset: 0x00003B61
			internal int TotalOffline { get; set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0000596A File Offset: 0x00003B6A
			// (set) Token: 0x06000179 RID: 377 RVA: 0x00005972 File Offset: 0x00003B72
			internal List<string> ReadyResources { get; set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600017A RID: 378 RVA: 0x0000597B File Offset: 0x00003B7B
			// (set) Token: 0x0600017B RID: 379 RVA: 0x00005983 File Offset: 0x00003B83
			internal List<string> ArbitratingResources { get; set; }

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x0600017C RID: 380 RVA: 0x0000598C File Offset: 0x00003B8C
			// (set) Token: 0x0600017D RID: 381 RVA: 0x00005994 File Offset: 0x00003B94
			internal List<string> UnknownResources { get; set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x0600017E RID: 382 RVA: 0x0000599D File Offset: 0x00003B9D
			// (set) Token: 0x0600017F RID: 383 RVA: 0x000059A5 File Offset: 0x00003BA5
			internal List<string> InMaintenanceResources { get; set; }

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000180 RID: 384 RVA: 0x000059AE File Offset: 0x00003BAE
			// (set) Token: 0x06000181 RID: 385 RVA: 0x000059B6 File Offset: 0x00003BB6
			internal List<string> OfflineResources { get; set; }

			// Token: 0x06000182 RID: 386 RVA: 0x000059C0 File Offset: 0x00003BC0
			public string GetStatisticsAsString()
			{
				return string.Format("ResourceAvailabilityStatistics: (Requested: {0} Ready: {1}, Busy: {2}, Maintenance: {3}, Unknown: {4}, Offline:{5})", new object[]
				{
					this.TotalRequested,
					this.TotalReady,
					this.TotalArbitrating,
					this.TotalMaintenance,
					this.TotalUnknown,
					this.TotalOffline
				});
			}
		}
	}
}
