using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000016 RID: 22
	public abstract class MonitorWorkItem : WorkItem
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008218 File Offset: 0x00006418
		public new MonitorDefinition Definition
		{
			get
			{
				return (MonitorDefinition)base.Definition;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008225 File Offset: 0x00006425
		public new MonitorResult Result
		{
			get
			{
				return (MonitorResult)base.Result;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008232 File Offset: 0x00006432
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000823A File Offset: 0x0000643A
		internal MonitorStateMachine StateMachine { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008243 File Offset: 0x00006443
		protected internal DateTime MonitoringWindowStartTime
		{
			get
			{
				return this.Result.ExecutionStartTime - TimeSpan.FromSeconds((double)this.Definition.MonitoringIntervalSeconds);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008266 File Offset: 0x00006466
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000826E File Offset: 0x0000646E
		protected internal MonitorResult LastSuccessfulResult { get; internal set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008277 File Offset: 0x00006477
		protected new IMonitorWorkBroker Broker
		{
			get
			{
				return (IMonitorWorkBroker)base.Broker;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008330 File Offset: 0x00006530
		public Task<int> GetLastFailedProbeResultId(string sampleMask, CancellationToken cancellationToken)
		{
			IEnumerable<MonitorWorkItem.ProbeResultPair> query = (from r in this.Broker.GetProbeResults(sampleMask, this.MonitoringWindowStartTime, this.Result.ExecutionStartTime)
			where r.ResultType == ResultType.Failed || r.ResultType == ResultType.Rejected || r.ResultType == ResultType.TimedOut
			select new MonitorWorkItem.ProbeResultPair
			{
				ResultId = r.ResultId,
				WorkItemId = r.WorkItemId
			}).Take(1);
			IDataAccessQuery<MonitorWorkItem.ProbeResultPair> dataAccessQuery = this.Broker.AsDataAccessQuery<MonitorWorkItem.ProbeResultPair>(query);
			Task<int> task = dataAccessQuery.ExecuteAsync(delegate(MonitorWorkItem.ProbeResultPair result)
			{
				WTFDiagnostics.TraceInformation(WTFLog.WorkItem, base.TraceContext, "MonitorWorkItem.GetLastFailedProbeResult: Successfully found the last failed probe result.", null, "GetLastFailedProbeResultId", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 98);
				this.Result.LastFailedProbeId = result.WorkItemId;
				this.Result.LastFailedProbeResultId = result.ResultId;
			}, cancellationToken, base.TraceContext);
			return task.Continue((Task<int> lastFailedProbeResult) => this.Result.LastFailedProbeResultId, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008408 File Offset: 0x00006608
		public Task GetConsecutiveProbeFailureInformation(string sampleMask, int consecutiveFailureThreshold, Action<int> setNewResultCount, Action<int> setTotalResultCount, CancellationToken cancellationToken)
		{
			Func<DateTime, DateTime, IDataAccessQuery<ProbeResult>> query = (DateTime start, DateTime end) => this.Broker.GetProbeResults(sampleMask, start, end);
			return this.GetConsecutiveFailureInformation(query, consecutiveFailureThreshold, setNewResultCount, setTotalResultCount, cancellationToken);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000084E8 File Offset: 0x000066E8
		public Task GetConsecutiveFailureInformation(Func<DateTime, DateTime, IDataAccessQuery<ProbeResult>> query, int consecutiveFailureThreshold, Action<int> setNewResultCount, Action<int> setTotalResultCount, CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<int>(WTFLog.WorkItem, base.TraceContext, "MonitorWorkItem.GetConsecutiveFailureInformation: Getting consecutive failure information using a consecutive failure threshold of {0}.", consecutiveFailureThreshold, null, "GetConsecutiveFailureInformation", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 149);
			Func<DateTime, DateTime, Func<DateTime, DateTime, IDataAccessQuery<ProbeResult>>, IDataAccessQuery<ResultType>> func = delegate(DateTime start, DateTime end, Func<DateTime, DateTime, IDataAccessQuery<ProbeResult>> existingQuery)
			{
				IEnumerable<ResultType> query2 = (from r in existingQuery(start, end)
				select r.ResultType).Take(consecutiveFailureThreshold);
				return this.Broker.AsDataAccessQuery<ResultType>(query2);
			};
			DateTime arg = this.MonitoringWindowStartTime;
			if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.ExecutionStartTime > this.MonitoringWindowStartTime)
			{
				arg = this.LastSuccessfulResult.ExecutionStartTime;
			}
			Task<int> largestConsecutiveCount = this.GetLargestConsecutiveCount<ResultType>(func(arg, this.Result.ExecutionStartTime, query), (ResultType result) => MonitorWorkItem.ShouldConsiderFailed(result), consecutiveFailureThreshold, cancellationToken);
			largestConsecutiveCount.Continue(delegate(int newResultCount)
			{
				setNewResultCount(newResultCount);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			Task<int> largestConsecutiveCount2 = this.GetLargestConsecutiveCount<ResultType>(func(this.MonitoringWindowStartTime, this.Result.ExecutionStartTime, query), delegate(ResultType result)
			{
				this.Result.TotalSampleCount++;
				return MonitorWorkItem.ShouldConsiderFailed(result);
			}, consecutiveFailureThreshold, cancellationToken);
			return largestConsecutiveCount2.Continue(delegate(int totalResultCount)
			{
				setTotalResultCount(totalResultCount);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000864C File Offset: 0x0000684C
		public Task GetConsecutiveSampleValueAboveThresholdCounts(string sampleMask, double thresholdValue, int consecutiveThreshold, Action<int> setNewResultCount, Action<int> setTotalResultCount, CancellationToken cancellationToken)
		{
			Func<double, bool> thresholdComparer = (double value) => value > thresholdValue;
			return this.GetConsecutiveSampleValueThresholdCounts(sampleMask, consecutiveThreshold, thresholdComparer, setNewResultCount, setTotalResultCount, cancellationToken);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008698 File Offset: 0x00006898
		public Task GetConsecutiveSampleValueBelowThresholdCounts(string sampleMask, double thresholdValue, int consecutiveThreshold, Action<int> setNewResultCount, Action<int> setTotalResultCount, CancellationToken cancellationToken)
		{
			Func<double, bool> thresholdComparer = (double value) => value < thresholdValue;
			return this.GetConsecutiveSampleValueThresholdCounts(sampleMask, consecutiveThreshold, thresholdComparer, setNewResultCount, setTotalResultCount, cancellationToken);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008728 File Offset: 0x00006928
		public Task GetConsecutiveSampleValueThresholdCounts(string sampleMask, int consecutiveThreshold, Func<double, bool> thresholdComparer, Action<int> setNewResultCount, Action<int> setTotalResultCount, CancellationToken cancellationToken)
		{
			DateTime startTime = this.MonitoringWindowStartTime;
			if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.ExecutionStartTime > this.MonitoringWindowStartTime)
			{
				startTime = this.LastSuccessfulResult.ExecutionStartTime;
			}
			IDataAccessQuery<double> consecutiveSampleValue = this.GetConsecutiveSampleValue(sampleMask, consecutiveThreshold, startTime, this.Result.ExecutionStartTime);
			Task<int> largestConsecutiveCount = this.GetLargestConsecutiveCount<double>(consecutiveSampleValue, (double sampleValue) => thresholdComparer(sampleValue), consecutiveThreshold, cancellationToken);
			largestConsecutiveCount.Continue(delegate(int newResultCount)
			{
				setNewResultCount(newResultCount);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			IDataAccessQuery<double> consecutiveSampleValue2 = this.GetConsecutiveSampleValue(sampleMask, consecutiveThreshold, this.MonitoringWindowStartTime, this.Result.ExecutionStartTime);
			Task<int> largestConsecutiveCount2 = this.GetLargestConsecutiveCount<double>(consecutiveSampleValue2, delegate(double sampleValue)
			{
				this.Result.TotalSampleCount++;
				return thresholdComparer(sampleValue);
			}, consecutiveThreshold, cancellationToken);
			return largestConsecutiveCount2.Continue(delegate(int totalResultCount)
			{
				setTotalResultCount(totalResultCount);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000882B File Offset: 0x00006A2B
		public Task<Dictionary<ResultType, int>> GetResultTypeCountsForNewProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			return this.GetResultTypeCountsForNewProbeResults(sampleMask, true, cancellationToken);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008838 File Offset: 0x00006A38
		public Task<Dictionary<ResultType, int>> GetResultTypeCountsForNewProbeResults(string sampleMask, bool consolidateFailureResults, CancellationToken cancellationToken)
		{
			DateTime startTime = this.MonitoringWindowStartTime;
			if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.ExecutionStartTime > this.MonitoringWindowStartTime)
			{
				startTime = this.LastSuccessfulResult.ExecutionStartTime;
			}
			return this.GetResultTypeCountsForProbeResults(sampleMask, startTime, this.Result.ExecutionStartTime, consolidateFailureResults, cancellationToken);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000888D File Offset: 0x00006A8D
		public Task<Dictionary<ResultType, int>> GetResultTypeCountsForAllProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			return this.GetResultTypeCountsForAllProbeResults(sampleMask, true, cancellationToken);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008898 File Offset: 0x00006A98
		public Task<Dictionary<ResultType, int>> GetResultTypeCountsForAllProbeResults(string sampleMask, bool consolidateFailureResults, CancellationToken cancellationToken)
		{
			return this.GetResultTypeCountsForProbeResults(sampleMask, this.MonitoringWindowStartTime, this.Result.ExecutionStartTime, consolidateFailureResults, cancellationToken);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000088B4 File Offset: 0x00006AB4
		public Task<Dictionary<string, int>> GetStateAttribute1CountsForNewFailedProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			DateTime startTime = this.MonitoringWindowStartTime;
			if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.ExecutionStartTime > this.MonitoringWindowStartTime)
			{
				startTime = this.LastSuccessfulResult.ExecutionStartTime;
			}
			return this.GetStateAttribute1CountsForFailedProbeResults(sampleMask, startTime, this.Result.ExecutionStartTime, cancellationToken);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008908 File Offset: 0x00006B08
		public Task<Dictionary<string, int>> GetStateAttribute1CountsForAllFailedProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			return this.GetStateAttribute1CountsForFailedProbeResults(sampleMask, this.MonitoringWindowStartTime, this.Result.ExecutionStartTime, cancellationToken);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008924 File Offset: 0x00006B24
		public Task<Dictionary<int, int>> GetFailureCategoryCountsForNewFailedProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			DateTime startTime = this.MonitoringWindowStartTime;
			if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.ExecutionStartTime > this.MonitoringWindowStartTime)
			{
				startTime = this.LastSuccessfulResult.ExecutionStartTime;
			}
			return this.GetFailureCategoryCountsForFailedProbeResults(sampleMask, startTime, this.Result.ExecutionStartTime, cancellationToken);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008978 File Offset: 0x00006B78
		public Task<Dictionary<int, int>> GetFailureCategoryCountsForAllFailedProbeResults(string sampleMask, CancellationToken cancellationToken)
		{
			return this.GetFailureCategoryCountsForFailedProbeResults(sampleMask, this.MonitoringWindowStartTime, this.Result.ExecutionStartTime, cancellationToken);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008993 File Offset: 0x00006B93
		internal static bool ShouldConsiderFailed(ResultType r)
		{
			return r == ResultType.Failed || r == ResultType.Rejected || r == ResultType.TimedOut;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008A60 File Offset: 0x00006C60
		internal virtual Task<Dictionary<ResultType, int>> GetResultTypeCountsForProbeResults(string sampleMask, DateTime startTime, DateTime endTime, bool consolidateFailureResults, CancellationToken cancellationToken)
		{
			IEnumerable<MonitorWorkItem.ProbeResultsGroup> query = from r in this.Broker.GetProbeResults(sampleMask, startTime, endTime)
			group r by r.ResultType into g
			select new MonitorWorkItem.ProbeResultsGroup
			{
				ResultType = g.Key,
				Count = g.Count<ProbeResult>()
			};
			Dictionary<ResultType, int> resultTypeCounts = new Dictionary<ResultType, int>();
			Task<int> task = this.Broker.AsDataAccessQuery<MonitorWorkItem.ProbeResultsGroup>(query).ExecuteAsync(delegate(MonitorWorkItem.ProbeResultsGroup group)
			{
				Dictionary<ResultType, int> resultTypeCounts;
				if (!consolidateFailureResults || !MonitorWorkItem.ShouldConsiderFailed(group.ResultType))
				{
					resultTypeCounts.Add(group.ResultType, group.Count);
					return;
				}
				if (resultTypeCounts.ContainsKey(ResultType.Failed))
				{
					(resultTypeCounts = resultTypeCounts)[ResultType.Failed] = resultTypeCounts[ResultType.Failed] + group.Count;
					return;
				}
				resultTypeCounts.Add(ResultType.Failed, group.Count);
			}, cancellationToken, base.TraceContext);
			return task.Continue((Task<int> totalResultCount) => resultTypeCounts, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008B5C File Offset: 0x00006D5C
		internal Task<Dictionary<string, int>> GetStateAttribute1CountsForFailedProbeResults(string sampleMask, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<DateTime, DateTime>(WTFLog.WorkItem, base.TraceContext, "[MonitorWorkItem.GetStateAttribute1Counts] Getting the count of the probe StateAttribute1s from {0} to {1}.", startTime, endTime, null, "GetStateAttribute1CountsForFailedProbeResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 556);
			IEnumerable<MonitorWorkItem.ProbeResultsGroup> enumerable = from r in this.Broker.GetProbeResults(sampleMask, startTime, endTime)
			where r.ResultType == ResultType.Failed
			group r by r.StateAttribute1 into g
			select new MonitorWorkItem.ProbeResultsGroup
			{
				Name = g.Key,
				Count = g.Count<ProbeResult>()
			};
			return this.GetAttributeCountsForProbeResults((IDataAccessQuery<MonitorWorkItem.ProbeResultsGroup>)enumerable, cancellationToken);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008C48 File Offset: 0x00006E48
		internal Task<Dictionary<string, int>> GetAttributeCountsForProbeResults(IDataAccessQuery<MonitorWorkItem.ProbeResultsGroup> probeQueryResults, CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(WTFLog.WorkItem, base.TraceContext, "[MonitorWorkItem.GetAttributeCountsForProbeResults] Getting the count of the probe attributes.", null, "GetAttributeCountsForProbeResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 581);
			Dictionary<string, int> attributeCounts = new Dictionary<string, int>();
			Task<int> task = probeQueryResults.ExecuteAsync(delegate(MonitorWorkItem.ProbeResultsGroup group)
			{
				attributeCounts.Add(group.Name ?? string.Empty, group.Count);
			}, cancellationToken, base.TraceContext);
			return task.Continue((Task<int> totalAttributeCount) => attributeCounts, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008D00 File Offset: 0x00006F00
		internal virtual Task<Dictionary<int, int>> GetFailureCategoryCountsForFailedProbeResults(string sampleMask, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<DateTime, DateTime>(WTFLog.WorkItem, base.TraceContext, "[MonitorWorkItem.GetFailureCategoryCounts] Getting the count of the probe FailureCategories from {0} to {1}.", startTime, endTime, null, "GetFailureCategoryCountsForFailedProbeResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 616);
			IEnumerable<MonitorWorkItem.ProbeResultsGroup> enumerable = from r in this.Broker.GetProbeResults(sampleMask, startTime, endTime)
			where r.ResultType == ResultType.Failed
			group r by r.FailureCategory into g
			select new MonitorWorkItem.ProbeResultsGroup
			{
				Value = g.Key,
				Count = g.Count<ProbeResult>()
			};
			return this.GetAttributeValueCountsForProbeResults((IDataAccessQuery<MonitorWorkItem.ProbeResultsGroup>)enumerable, cancellationToken);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008DE0 File Offset: 0x00006FE0
		internal Task<Dictionary<int, int>> GetAttributeValueCountsForProbeResults(IDataAccessQuery<MonitorWorkItem.ProbeResultsGroup> probeQueryResults, CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(WTFLog.WorkItem, base.TraceContext, "[MonitorWorkItem.GetAttributeValueCountsForProbeResults] Getting the count of the probe attributes.", null, "GetAttributeValueCountsForProbeResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 641);
			Dictionary<int, int> attributeCounts = new Dictionary<int, int>();
			Task<int> task = probeQueryResults.ExecuteAsync(delegate(MonitorWorkItem.ProbeResultsGroup group)
			{
				attributeCounts.Add(group.Value, group.Count);
			}, cancellationToken, base.TraceContext);
			return task.Continue((Task<int> totalAttributeCount) => attributeCounts, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008F70 File Offset: 0x00007170
		protected void HandleInsufficientSamples(Func<bool> insufficientSamples, CancellationToken cancellationToken)
		{
			Task<MonitorResult> task = this.Broker.GetLastMonitorResult(this.Definition, this.Broker.DefaultResultWindow).ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(MonitorResult result)
			{
				if (!insufficientSamples())
				{
					this.Result.FirstInsufficientSamplesObservedTime = null;
					return;
				}
				if (result != null)
				{
					this.Result.FirstInsufficientSamplesObservedTime = result.FirstInsufficientSamplesObservedTime;
				}
				if (this.Result.FirstInsufficientSamplesObservedTime != null && (this.Result.ExecutionStartTime - this.Result.FirstInsufficientSamplesObservedTime.Value).TotalSeconds >= (double)this.Definition.InsufficientSamplesIntervalSeconds)
				{
					this.Result.IsAlert = true;
					return;
				}
				if (this.Result.FirstInsufficientSamplesObservedTime == null)
				{
					this.Result.FirstInsufficientSamplesObservedTime = new DateTime?(this.Result.ExecutionStartTime);
				}
				throw new Exception("Not enough samples to make a decision");
			}, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion);
		}

		// Token: 0x06000171 RID: 369
		protected abstract void DoMonitorWork(CancellationToken cancellationToken);

		// Token: 0x06000172 RID: 370 RVA: 0x00009010 File Offset: 0x00007210
		protected sealed override void DoWork(CancellationToken cancellationToken)
		{
			IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition);
			Task<MonitorResult> task = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, base.TraceContext);
			Task task2 = task.Continue(delegate(MonitorResult lastSuccessfulResult)
			{
				this.LastSuccessfulResult = lastSuccessfulResult;
				this.DoMonitorWork(cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			task2.ContinueWith(delegate(Task t)
			{
				this.DoManagedAvailabilityWork(cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Current);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009098 File Offset: 0x00007298
		private void DoManagedAvailabilityWork(CancellationToken cancellationToken)
		{
			string name = this.Definition.Name;
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = utcNow;
			if (this.Result.IsAlert)
			{
				if (this.LastSuccessfulResult != null && this.LastSuccessfulResult.IsAlert)
				{
					dateTime = (this.LastSuccessfulResult.FirstAlertObservedTime ?? dateTime);
				}
				this.Result.FirstAlertObservedTime = new DateTime?(dateTime);
			}
			if (this.Definition.MonitorStateTransitions != null && this.Definition.MonitorStateTransitions.Length > 0)
			{
				this.StateMachine = new MonitorStateMachine(this.Definition.MonitorStateTransitions);
			}
			else
			{
				this.StateMachine = new MonitorStateMachine(MonitorStateMachine.DefaultUnhealthyTransition);
			}
			if (this.Result.IsAlert)
			{
				ServiceHealthStatus serviceHealthStatus = this.StateMachine.GreenState;
				int num = -1;
				DateTime? healthStateChangedTime = new DateTime?(utcNow);
				if (this.LastSuccessfulResult != null)
				{
					this.Result.HealthState = this.LastSuccessfulResult.HealthState;
					this.Result.HealthStateTransitionId = this.LastSuccessfulResult.HealthStateTransitionId;
					this.Result.HealthStateChangedTime = this.LastSuccessfulResult.HealthStateChangedTime;
					serviceHealthStatus = this.LastSuccessfulResult.HealthState;
					num = this.LastSuccessfulResult.HealthStateTransitionId;
					healthStateChangedTime = this.LastSuccessfulResult.HealthStateChangedTime;
				}
				WTFDiagnostics.TraceDebug(WTFLog.ManagedAvailability, base.TraceContext, string.Format("[{0}] LastHealthState={1} LastTransitionId={2} LastChangedTime={3} FirstAlertObservedTime={4} Now={5}", new object[]
				{
					name,
					serviceHealthStatus,
					num,
					healthStateChangedTime,
					dateTime,
					utcNow
				}), null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 850);
				bool flag = false;
				int nextTransitionId = this.StateMachine.GetNextTransitionId(num);
				if (nextTransitionId != -1)
				{
					MonitorStateTransition transitionInfo = this.StateMachine.GetTransitionInfo(nextTransitionId);
					WTFDiagnostics.TraceDebug(WTFLog.ManagedAvailability, base.TraceContext, string.Format("[{0}] Attempting to transition Current:{1} Next:{2} NextTransitionId={3}", new object[]
					{
						name,
						serviceHealthStatus,
						transitionInfo.ToState,
						nextTransitionId
					}), null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 873);
					if (utcNow >= dateTime + transitionInfo.TransitionTimeout)
					{
						WTFDiagnostics.TraceDebug<string>(WTFLog.ManagedAvailability, base.TraceContext, "[{0}] Allowing transition since timeout exceeded from the time monitor changed its state", name, null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 887);
						this.ChangeHealthState(transitionInfo.ToState, nextTransitionId, utcNow, true);
						flag = true;
					}
				}
				else
				{
					WTFDiagnostics.TraceError<string, int>(WTFLog.ManagedAvailability, base.TraceContext, "[{0}] {1} is the last state transition id", name, num, null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 904);
				}
				if (!flag)
				{
					WTFDiagnostics.TraceDebug<string>(WTFLog.ManagedAvailability, base.TraceContext, "[{0}] No transition happened", name, null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 915);
				}
				WTFDiagnostics.TraceFunction(WTFLog.ManagedAvailability, base.TraceContext, string.Format("[{0}] Exiting: HealthState={1} TransitionId={2} HealthStateChangedTime={3}", new object[]
				{
					name,
					this.Result.HealthState,
					this.Result.HealthStateTransitionId,
					this.Result.HealthStateChangedTime
				}), null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 922);
				return;
			}
			this.Result.FirstAlertObservedTime = null;
			if (this.LastSuccessfulResult == null || this.LastSuccessfulResult.HealthState != this.StateMachine.GreenState)
			{
				WTFDiagnostics.TraceDebug<string, ServiceHealthStatus, DateTime>(WTFLog.ManagedAvailability, base.TraceContext, "[{0}] Setting health state to {1} @ {2}", name, this.StateMachine.GreenState, utcNow, null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 798);
				this.ChangeHealthState(this.StateMachine.GreenState, -1, utcNow, true);
				return;
			}
			WTFDiagnostics.TraceDebug<string, ServiceHealthStatus, DateTime?>(WTFLog.ManagedAvailability, base.TraceContext, "[{0}] Only copying the previous state {1} @ {2}", name, this.StateMachine.GreenState, this.LastSuccessfulResult.HealthStateChangedTime, null, "DoManagedAvailabilityWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkItem.cs", 814);
			this.ChangeHealthState(this.StateMachine.GreenState, -1, this.LastSuccessfulResult.HealthStateChangedTime.Value, false);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000094D5 File Offset: 0x000076D5
		private void ChangeHealthState(ServiceHealthStatus newState, int newTransitionId, DateTime stateChangedTime, bool isSetManagedAvailabilityHealthStatus)
		{
			this.Result.HealthState = newState;
			this.Result.HealthStateTransitionId = newTransitionId;
			this.Result.HealthStateChangedTime = new DateTime?(stateChangedTime);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009508 File Offset: 0x00007708
		private IDataAccessQuery<double> GetConsecutiveSampleValue(string sampleMask, int consecutiveSampleCount, DateTime startTime, DateTime endTime)
		{
			IEnumerable<double> query = (from p in this.Broker.GetProbeResults(sampleMask, startTime, endTime)
			select p.SampleValue).Take(consecutiveSampleCount);
			return this.Broker.AsDataAccessQuery<double>(query);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000095BC File Offset: 0x000077BC
		private Task<int> GetLargestConsecutiveCount<T>(IDataAccessQuery<T> query, Func<T, bool> stateCheck, int consecutiveThreshold, CancellationToken cancellationToken)
		{
			int localConsecutiveCount = 0;
			int largestConsecutiveCount = 0;
			Task<int> task = query.ExecuteAsync(delegate(T result)
			{
				bool flag = stateCheck(result);
				if (flag)
				{
					localConsecutiveCount++;
				}
				if (localConsecutiveCount > largestConsecutiveCount)
				{
					largestConsecutiveCount = localConsecutiveCount;
				}
				if (!flag)
				{
					localConsecutiveCount = 0;
				}
			}, cancellationToken, base.TraceContext);
			return task.ContinueWith<int>((Task<int> t) => largestConsecutiveCount, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
		}

		// Token: 0x02000017 RID: 23
		internal class ProbeResultsGroup
		{
			// Token: 0x06000187 RID: 391 RVA: 0x00009627 File Offset: 0x00007827
			internal ProbeResultsGroup()
			{
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000188 RID: 392 RVA: 0x0000962F File Offset: 0x0000782F
			// (set) Token: 0x06000189 RID: 393 RVA: 0x00009637 File Offset: 0x00007837
			internal ResultType ResultType { get; set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600018A RID: 394 RVA: 0x00009640 File Offset: 0x00007840
			// (set) Token: 0x0600018B RID: 395 RVA: 0x00009648 File Offset: 0x00007848
			internal string Name { get; set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600018C RID: 396 RVA: 0x00009651 File Offset: 0x00007851
			// (set) Token: 0x0600018D RID: 397 RVA: 0x00009659 File Offset: 0x00007859
			internal int Value { get; set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600018E RID: 398 RVA: 0x00009662 File Offset: 0x00007862
			// (set) Token: 0x0600018F RID: 399 RVA: 0x0000966A File Offset: 0x0000786A
			internal int Count { get; set; }
		}

		// Token: 0x02000018 RID: 24
		private class ProbeResultPair
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000190 RID: 400 RVA: 0x00009673 File Offset: 0x00007873
			// (set) Token: 0x06000191 RID: 401 RVA: 0x0000967B File Offset: 0x0000787B
			public int WorkItemId { get; set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000192 RID: 402 RVA: 0x00009684 File Offset: 0x00007884
			// (set) Token: 0x06000193 RID: 403 RVA: 0x0000968C File Offset: 0x0000788C
			public int ResultId { get; set; }
		}
	}
}
