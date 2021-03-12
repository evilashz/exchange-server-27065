using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000011 RID: 17
	public class TypedWorkBroker<TWorkItemDefinition, TWorkItem, TWorkItemResult, TDataAccess> : WorkBroker where TWorkItemDefinition : WorkDefinition, new() where TWorkItem : WorkItem where TWorkItemResult : WorkItemResult where TDataAccess : DataAccess, new()
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x000070F8 File Offset: 0x000052F8
		public TypedWorkBroker(IWorkItemFactory factory)
		{
			this.factory = factory;
			this.traceContext = new TracingContext(null)
			{
				LId = this.GetHashCode(),
				Id = base.GetType().GetHashCode()
			};
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.wtfPerfCountersInstance = WTFPerfCounters.GetInstance(currentProcess.ProcessName);
			}
			this.wtfPerfCountersInstance.PoisonedWorkItemCount.Reset();
			WTFDiagnostics.TraceInformation<Type>(WTFLog.Core, this.traceContext, "[TypedWorkBroker.TypedWorkBroker]: {0} created.", base.GetType(), null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 97);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000071D4 File Offset: 0x000053D4
		public TimeSpan DefaultResultWindow
		{
			get
			{
				return TimeSpan.FromMinutes((double)Settings.ResultHistoryWindowInMinutes);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000071E1 File Offset: 0x000053E1
		public bool IsLocal()
		{
			return this.isLocal;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000071EC File Offset: 0x000053EC
		public override IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsDataAccessQuery<TEntity>(query);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007210 File Offset: 0x00005410
		internal override void PublishResult(WorkItemResult result, TracingContext traceContext)
		{
			if (result.ResultType != ResultType.Poisoned)
			{
				result.PoisonedCount = 0;
			}
			this.WriteResult((TWorkItemResult)((object)result), traceContext);
			if (result.ResultType == ResultType.Poisoned)
			{
				WTFDiagnostics.TraceError<string>(WTFLog.Core, traceContext, "[TypedWorkBroker.PublishResult]: Poison result encountered: {0}", result.ResultName, null, "PublishResult", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 151);
				this.wtfPerfCountersInstance.PoisonedWorkItemCount.Increment();
				if (Settings.RestartOnPoisonedWorkItem)
				{
					base.RequestRestart(RestartRequest.CreatePoisonResultRestartRequest(result.ResultName, result.ResultId));
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007410 File Offset: 0x00005610
		internal override BlockingCollection<WorkItem> AsyncGetWork(int maxWorkitemCount, CancellationToken cancellationToken)
		{
			BlockingCollection<WorkItem> workItems = new BlockingCollection<WorkItem>();
			bool workItemsRetrieved = false;
			Task<int> task = this.AsyncGetWorkDefinition(maxWorkitemCount, delegate(TWorkItemDefinition definition)
			{
				WTFDiagnostics.TraceFunction(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWork.Task1]", null, "AsyncGetWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 185);
				TWorkItem tworkItem = this.CreateItemWithPoisonDetection(definition);
				if (tworkItem != null)
				{
					workItems.Add(tworkItem);
					workItemsRetrieved = true;
				}
			}, cancellationToken);
			task.ContinueWith(delegate(Task<int> t)
			{
				WTFDiagnostics.TraceFunction(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWork.Continuation1]", null, "AsyncGetWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 202);
				if (!workItemsRetrieved)
				{
					Thread.Sleep(this.workItemRetrievalDelay);
				}
				workItems.CompleteAdding();
				if (t.Exception != null)
				{
					this.consecutiveGetWorkFailures++;
					WTFDiagnostics.TraceError<AggregateException>(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWork]: Encountered an error while retrieving more work: \n\r{0}", t.Exception, null, "AsyncGetWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 213);
					if (this.consecutiveGetWorkFailures > 3)
					{
						WTFDiagnostics.TraceError<AggregateException>(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWork]: Too many consecutive errors. Requesting restart.\n\rError: {0}", t.Exception, null, "AsyncGetWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 217);
						this.RequestRestart(RestartRequest.CreateDataAccessErrorRestartRequest(t.Exception));
						return;
					}
				}
				else
				{
					this.consecutiveGetWorkFailures = 0;
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent, TaskScheduler.Current);
			return workItems;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007474 File Offset: 0x00005674
		internal override void Reject(WorkItem workItem)
		{
			WTFDiagnostics.TraceInformation<string>(WTFLog.WorkItem, workItem.Definition.TraceContext, "[TypedWorkBroker.Reject]: workitem '{0}' is already running and has been Rejected.", workItem.Definition.Name, null, "Reject", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 240);
			if (workItem.Result.PoisonedCount > 0)
			{
				WorkItemResult result = workItem.Result;
				result.PoisonedCount -= 1;
			}
			workItem.Result.SetCompleted(ResultType.Rejected, new Exception("Workitem was rejected because it is already executing."));
			this.WriteResult((TWorkItemResult)((object)workItem.Result), workItem.Definition.TraceContext);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000750C File Offset: 0x0000570C
		internal override void Abandon(WorkItem workItem)
		{
			WTFDiagnostics.TraceInformation<string>(WTFLog.WorkItem, workItem.Definition.TraceContext, "[TypedWorkBroker.Abandon]: workitem '{0}' is being abandoned because Controller is exiting.", workItem.Definition.Name, null, "Abandon", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 258);
			if (workItem.Result.PoisonedCount > 0)
			{
				WorkItemResult result = workItem.Result;
				result.PoisonedCount -= 1;
			}
			workItem.Result.SetCompleted(ResultType.Abandoned, new Exception("Workitem was rejected because Controller is exiting"));
			this.WriteResult((TWorkItemResult)((object)workItem.Result), workItem.Definition.TraceContext);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000076AC File Offset: 0x000058AC
		internal override BlockingCollection<string> AsyncGetWorkItemPackages(CancellationToken cancellationToken)
		{
			BlockingCollection<string> packages = new BlockingCollection<string>();
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			Task<int> task = tdataAccess.AsyncGetWorkItemPackages<TWorkItem>(Settings.DeploymentId, delegate(string fileName)
			{
				WTFDiagnostics.TraceFunction(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWorkItemPackages.Task1]", null, "AsyncGetWorkItemPackages", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 283);
				WTFDiagnostics.TraceDebug<string>(WTFLog.WorkItem, this.traceContext, "[TypedWorkBroker.AsyncGetWorkItemPackages]: Got package {0}.", fileName, null, "AsyncGetWorkItemPackages", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 284);
				packages.Add(fileName);
			}, cancellationToken, this.traceContext);
			task.ContinueWith(delegate(Task<int> t)
			{
				WTFDiagnostics.TraceFunction(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWorkItemPackages.Continuation1]", null, "AsyncGetWorkItemPackages", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 297);
				packages.CompleteAdding();
				if (t.Exception != null)
				{
					WTFDiagnostics.TraceError<AggregateException>(WTFLog.Core, this.traceContext, "[TypedWorkBroker.AsyncGetWorkItemPackages]: Encountered a fatal error while retrieving packages. Requesting restart.\n\rError: {0}", t.Exception, null, "AsyncGetWorkItemPackages", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 301);
					this.RequestRestart(RestartRequest.CreateDataAccessErrorRestartRequest(t.Exception));
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent, TaskScheduler.Current);
			return packages;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000077AC File Offset: 0x000059AC
		protected Task SaveStatusEntriesInternal(StatusEntryCollection entries, CancellationToken cancellationToken, TracingContext traceContext)
		{
			List<Task> list = new List<Task>();
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(0);
			list.Add(taskCompletionSource.Task);
			foreach (StatusEntry entry in entries.ItemsToRemove)
			{
				TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
				Task item = tdataAccess.SaveStatusEntry(entry, cancellationToken, traceContext);
				list.Add(item);
			}
			foreach (StatusEntry entry2 in entries)
			{
				TDataAccess tdataAccess2 = Activator.CreateInstance<TDataAccess>();
				Task item2 = tdataAccess2.SaveStatusEntry(entry2, cancellationToken, traceContext);
				list.Add(item2);
			}
			return Task.Factory.ContinueWhenAll(list.ToArray(), delegate(Task[] tasks)
			{
				foreach (Task task in tasks)
				{
					if (task.Status != TaskStatus.RanToCompletion || task.Exception != null)
					{
						string arg = (task.Exception == null) ? string.Empty : task.Exception.ToString();
						WTFDiagnostics.TraceError<string>(WTFLog.Core, this.traceContext, "[TypedWorkBroker.SaveStatusEntriesInternal]: Unable to save status entry. Exception: {0}", arg, null, "SaveStatusEntriesInternal", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 357);
					}
				}
				entries.PurgeInvalidEntries();
			}, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000078B8 File Offset: 0x00005AB8
		protected virtual Task<int> AsyncGetWorkDefinition(int maxWorkitemCount, Action<TWorkItemDefinition> processResult, CancellationToken cancellationToken)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncGetExclusive<TWorkItemDefinition>(maxWorkitemCount, Settings.DeploymentId, processResult, new Func<object, Exception, bool>(this.HandleCorruptWorkItemRow), cancellationToken, this.traceContext);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007968 File Offset: 0x00005B68
		protected IDataAccessQuery<TWorkItemResult> GetResultsQuery(TWorkItemDefinition definition, DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<TWorkItemResult> query = from r in tdataAccess.GetTable<TWorkItemResult, int>(WorkItemResultIndex<TWorkItemResult>.WorkItemIdAndExecutionEndTime(definition.Id, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.WorkItemId == definition.Id && r.ExecutionEndTime >= startTime
			orderby r.ExecutionStartTime descending
			select r;
			return tdataAccess.AsDataAccessQuery<TWorkItemResult>(query);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000079FA File Offset: 0x00005BFA
		protected IDataAccessQuery<TWorkItemResult> GetLastSuccessfulResultQuery(TWorkItemDefinition definition, TimeSpan searchWindow)
		{
			return this.GetLastSuccessfulResultQuery(definition, DateTime.UtcNow - searchWindow);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007A20 File Offset: 0x00005C20
		protected IDataAccessQuery<TWorkItemResult> GetLastSuccessfulResultQuery(TWorkItemDefinition definition, DateTime startTime)
		{
			IEnumerable<TWorkItemResult> source = from r in this.GetResultsQuery(definition, startTime)
			where r.ResultType == ResultType.Succeeded
			select r;
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsDataAccessQuery<TWorkItemResult>(source.Take(1));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007A98 File Offset: 0x00005C98
		protected IDataAccessQuery<TWorkItemResult> GetLastResultQuery(TWorkItemDefinition definition, TimeSpan searchWindow)
		{
			IEnumerable<TWorkItemResult> source = from r in this.GetResultsQuery(definition, DateTime.UtcNow - searchWindow)
			where r.ResultType == ResultType.Succeeded || r.ResultType == ResultType.Failed
			select r;
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsDataAccessQuery<TWorkItemResult>(source.Take(1));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007B1C File Offset: 0x00005D1C
		protected IDataAccessQuery<TOtherWorkItemResult> GetResultsQuery<TOtherWorkItemResult>(string mask, DateTime startTime) where TOtherWorkItemResult : WorkItemResult
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<TOtherWorkItemResult> query = from r in tdataAccess.GetTable<TOtherWorkItemResult, string>(WorkItemResultIndex<TOtherWorkItemResult>.ResultNameAndExecutionEndTime(mask, startTime))
			where r.DeploymentId == Settings.DeploymentId
			orderby r.ExecutionStartTime descending
			select r;
			return tdataAccess.AsDataAccessQuery<TOtherWorkItemResult>(query);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007B88 File Offset: 0x00005D88
		protected IDataAccessQuery<TOtherWorkItemResult> GetLastSuccessfulResultQuery<TOtherWorkItemResult>(string mask, DateTime startTime) where TOtherWorkItemResult : WorkItemResult
		{
			IEnumerable<TOtherWorkItemResult> source = from r in this.GetResultsQuery<TOtherWorkItemResult>(mask, startTime)
			where r.ResultType == ResultType.Succeeded
			select r;
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsDataAccessQuery<TOtherWorkItemResult>(source.Take(1));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007BC9 File Offset: 0x00005DC9
		protected IDataAccessQuery<TOtherWorkItemResult> GetLastSuccessfulResultQuery<TOtherWorkItemResult>(string mask, TimeSpan searchWindow) where TOtherWorkItemResult : WorkItemResult
		{
			return this.GetLastSuccessfulResultQuery<TOtherWorkItemResult>(mask, DateTime.UtcNow - searchWindow);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007C24 File Offset: 0x00005E24
		protected IDataAccessQuery<TOtherWorkItemResult> GetLastSuccessfulResultQuery<TOtherWorkItemResult>(int workItemId, DateTime startTime) where TOtherWorkItemResult : WorkItemResult
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<TOtherWorkItemResult> source = from r in tdataAccess.GetTable<TOtherWorkItemResult, int>(WorkItemResultIndex<TOtherWorkItemResult>.WorkItemIdAndExecutionEndTime(workItemId, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.WorkItemId == workItemId && r.ResultType == ResultType.Succeeded
			select r;
			return tdataAccess.AsDataAccessQuery<TOtherWorkItemResult>(source.Take(1));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007CDC File Offset: 0x00005EDC
		protected IDataAccessQuery<TOtherWorkItemResult> GetSingleResultQuery<TOtherWorkItemResult>(int workItemId, int resultId) where TOtherWorkItemResult : WorkItemResult
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<TOtherWorkItemResult> source = from r in tdataAccess.GetTable<TOtherWorkItemResult, int>(WorkItemResultIndex<TOtherWorkItemResult>.WorkItemIdAndExecutionEndTime(workItemId, DateTime.UtcNow.AddYears(-1)))
			where r.DeploymentId == Settings.DeploymentId && r.WorkItemId == workItemId && r.ResultId == resultId
			select r;
			return tdataAccess.AsDataAccessQuery<TOtherWorkItemResult>(source.Take(1));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007D50 File Offset: 0x00005F50
		private TWorkItem CreateItemWithPoisonDetection(TWorkItemDefinition definition)
		{
			TWorkItem result = default(TWorkItem);
			WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: '{0}' definition retrieved.", definition.Name, definition.Id, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 554);
			if (definition.LastExecutionStartTime != null && definition.LastExecutionStartTime < definition.UpdateTime)
			{
				WTFDiagnostics.TraceInformation<int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: '{0}' Last result is missing. Assuming posioned probe.", definition.Id, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 561);
				if ((int)definition.PoisonedResultCount < TypedWorkBroker<TWorkItemDefinition, TWorkItem, TWorkItemResult, TDataAccess>.MaxPoisonCount)
				{
					byte? poisonedResultCount = definition.PoisonedResultCount;
					definition.PoisonedResultCount = ((poisonedResultCount != null) ? new byte?(poisonedResultCount.GetValueOrDefault() + 1) : null);
				}
				WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: Detected a previous crash in '{0}' (Id={1}). Adding a POISONED entry.", definition.Name, definition.Id, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 570);
				TWorkItemResult result2 = (TWorkItemResult)((object)definition.CreateResult());
				result2.ExecutionStartTime = definition.UpdateTime + TimeSpan.FromSeconds((double)definition.RecurrenceIntervalSeconds);
				result2.SetCompleted(ResultType.Poisoned, new Exception("A previous poisoned execution was detected. This is indicative of a crashing workitem."));
				this.WriteResult(result2, definition.TraceContext);
			}
			bool flag = this.ShouldBeQuarantined(definition);
			TWorkItemResult tworkItemResult = (TWorkItemResult)((object)definition.CreateResult());
			if (flag)
			{
				WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: '{0}' (Id={1}) quarantined.", definition.Name, definition.Id, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 593);
				tworkItemResult.SetCompleted(ResultType.Quarantined, new Exception("Poisoned item was quarantined."));
				this.WriteResult(tworkItemResult, definition.TraceContext);
				return default(TWorkItem);
			}
			try
			{
				if ((int)tworkItemResult.PoisonedCount < TypedWorkBroker<TWorkItemDefinition, TWorkItem, TWorkItemResult, TDataAccess>.MaxPoisonCount)
				{
					tworkItemResult.PoisonedCount += 1;
				}
				definition.ParseExtensionAttributes(false);
				result = this.factory.CreateWorkItem<TWorkItem>(definition);
				result.Initialize(definition, tworkItemResult, this);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<int, Exception>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: Failed to create workitem '{0}'. Error was: {1}", definition.Id, ex, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 618);
				WTFDiagnostics.TraceError<string, int, Exception>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.CreateItemWithPoisonDetection]: '{0}' (Id={1}) could not be created. Error = {2}.", definition.Name, definition.Id, ex, null, "CreateItemWithPoisonDetection", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 621);
				tworkItemResult.SetCompleted(ResultType.Poisoned, ex);
				this.WriteResult(tworkItemResult, definition.TraceContext);
				return default(TWorkItem);
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008134 File Offset: 0x00006334
		private bool ShouldBeQuarantined(TWorkItemDefinition definition)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			TimeSpan? quarantineTimeSpan = tdataAccess.GetQuarantineTimeSpan<TWorkItemDefinition>(definition);
			if (quarantineTimeSpan != null && quarantineTimeSpan.Value > TimeSpan.Zero)
			{
				WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.ShouldBeQuarantined]: '{0}' (Id={1}) is currently in quarantin phase.", definition.Name, definition.Id, null, "ShouldBeQuarantined", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 642);
				if (definition.RecurrenceIntervalSeconds == 0)
				{
					definition.StartTime = DateTime.UtcNow + quarantineTimeSpan.Value;
				}
				return true;
			}
			if (definition.PoisonedResultCount > 0)
			{
				int num = ((int)((int)(definition.PoisonedResultCount * 100)) / TypedWorkBroker<TWorkItemDefinition, TWorkItem, TWorkItemResult, TDataAccess>.MaxPoisonCount).Value - 5;
				Random random = new Random();
				int num2 = random.Next(100);
				if (num2 < num)
				{
					WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, definition.TraceContext, "[TypedWorkBroker.ShouldBeQuarantined]: '{0}' (Id={1}) should be quarantined.", definition.Name, definition.Id, null, "ShouldBeQuarantined", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 673);
					if (definition.RecurrenceIntervalSeconds == 0)
					{
						definition.StartTime = DateTime.UtcNow.AddSeconds((double)Settings.NonRecurrentRetryIntervalSeconds);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000830C File Offset: 0x0000650C
		private bool HandleCorruptWorkItemRow(object id, Exception e)
		{
			TWorkItemDefinition tworkItemDefinition = Activator.CreateInstance<TWorkItemDefinition>();
			tworkItemDefinition.Id = (int)id;
			tworkItemDefinition.PoisonedResultCount = new byte?((byte)TypedWorkBroker<TWorkItemDefinition, TWorkItem, TWorkItemResult, TDataAccess>.MaxPoisonCount);
			tworkItemDefinition.Name = "Corrupt definition";
			tworkItemDefinition.DeploymentId = Settings.DeploymentId;
			TWorkItemResult result = (TWorkItemResult)((object)tworkItemDefinition.CreateResult());
			result.SetCompleted(ResultType.Poisoned, e);
			WTFDiagnostics.TraceDebug<int>(WTFLog.WorkItem, result.TraceContext, "[TypedWorkBroker.HandleCorrupWorkItemRow]: Poisoned definition {0}.", tworkItemDefinition.Id, null, "HandleCorruptWorkItemRow", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 703);
			this.WriteResult(result, this.traceContext);
			return true;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008430 File Offset: 0x00006630
		private void WriteResult(TWorkItemResult result, TracingContext traceContext)
		{
			WTFDiagnostics.TraceDebug<ResultType, byte>(WTFLog.WorkItem, traceContext, "[TypedWorkBroker.WriteResult]: Writing result {0}. PoisonedCount {1}", result.ResultType, result.PoisonedCount, null, "WriteResult", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 715);
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			Task task = tdataAccess.AsyncInsert<TWorkItemResult>(result, default(CancellationToken), result.TraceContext);
			task.ContinueWith(delegate(Task t)
			{
				WTFDiagnostics.TraceError<AggregateException>(WTFLog.WorkItem, traceContext, "[TypedWorkBroker.WriteResult]: Requesting Restart due to Exception - {0}", t.Exception, null, "WriteResult", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\TypedWorkBroker.cs", 723);
				this.RequestRestart(RestartRequest.CreateDataAccessErrorRestartRequest(t.Exception));
			}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x040000B4 RID: 180
		private const int MaxConsecutiveGetWorkRetries = 3;

		// Token: 0x040000B5 RID: 181
		private static readonly int MaxPoisonCount = Settings.MaxPoisonCount;

		// Token: 0x040000B6 RID: 182
		private readonly int workItemRetrievalDelay = Settings.WorkItemRetrievalDelay;

		// Token: 0x040000B7 RID: 183
		private readonly bool isLocal = typeof(TDataAccess).Name == "LocalDataAccess";

		// Token: 0x040000B8 RID: 184
		private readonly WTFPerfCountersInstance wtfPerfCountersInstance;

		// Token: 0x040000B9 RID: 185
		private IWorkItemFactory factory;

		// Token: 0x040000BA RID: 186
		private TracingContext traceContext;

		// Token: 0x040000BB RID: 187
		private int consecutiveGetWorkFailures;
	}
}
