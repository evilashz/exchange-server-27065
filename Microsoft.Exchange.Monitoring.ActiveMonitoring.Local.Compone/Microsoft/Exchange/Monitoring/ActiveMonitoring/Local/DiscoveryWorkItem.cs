using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200000C RID: 12
	public abstract class DiscoveryWorkItem : MaintenanceWorkItem
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000033 RID: 51
		protected abstract Trace Trace { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x00004A5C File Offset: 0x00002C5C
		protected static void AddTestsForEachResource<TResource>(Func<IEnumerable<TResource>> resourceAccessor, params Action<IEnumerable<TResource>>[] testCreators)
		{
			Task<TResource[]> resourceTask = Task.Factory.StartNew<TResource[]>(() => resourceAccessor().ToArray<TResource>(), TaskCreationOptions.AttachedToParent);
			Array.ForEach<Action<IEnumerable<TResource>>>(testCreators, delegate(Action<IEnumerable<TResource>> testCreator)
			{
				resourceTask.ContinueWith(delegate(Task<TResource[]> task)
				{
					testCreator(task.Result);
				}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			});
		}

		// Token: 0x06000035 RID: 53
		protected abstract void CreateWorkTasks(CancellationToken cancellationToken);

		// Token: 0x06000036 RID: 54 RVA: 0x00004C04 File Offset: 0x00002E04
		protected sealed override void DoWork(CancellationToken cancellationToken)
		{
			Task.Factory.StartNew(delegate()
			{
				this.CreateWorkTasks(cancellationToken);
			}, TaskCreationOptions.AttachedToParent).ContinueWith(delegate(Task param0)
			{
				base.Result.StateAttribute1 = Strings.RcaWorkItemCreationSummaryEntry(this.workItemCreationLog.Count((DiscoveryWorkItem.WorkItemCreationLogEntry entry) => entry.Exception == null), this.workItemCreationLog.Count<DiscoveryWorkItem.WorkItemCreationLogEntry>());
				base.Result.StateAttribute2 = string.Join<LocalizedString>(Environment.NewLine, from entry in this.workItemCreationLog
				where entry.Exception != null
				select Strings.RcaWorkItemDescriptionEntry(entry.BaseIdentity.GetAlertMask(), string.Join("; ", from ex in new AggregateException(new Exception[]
				{
					entry.Exception
				}).Flatten().InnerExceptions
				select ex.Message)));
			}, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004C68 File Offset: 0x00002E68
		protected Task CreateRelatedWorkItems<TIdentity>(TIdentity basedOnIdentity, Action<TIdentity> workDefinitionCreator) where TIdentity : WorkItemIdentity
		{
			return this.CreateRelatedWorkItems<TIdentity, object>(basedOnIdentity, null, delegate(TIdentity identity, object unused2)
			{
				workDefinitionCreator(identity);
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004D3C File Offset: 0x00002F3C
		protected Task CreateRelatedWorkItems<TIdentity, TResource>(TIdentity basedOnIdentity, TResource resource, Action<TIdentity, TResource> workDefinitionCreator) where TIdentity : WorkItemIdentity
		{
			ArgumentValidator.ThrowIfNull("basedOnIdentity", basedOnIdentity);
			return Task.Factory.StartNew(delegate()
			{
				workDefinitionCreator(basedOnIdentity, resource);
			}, TaskCreationOptions.AttachedToParent).ContinueWith(delegate(Task workDefinitionTask)
			{
				this.workItemCreationLog.Add(new DiscoveryWorkItem.WorkItemCreationLogEntry
				{
					BaseIdentity = basedOnIdentity,
					Exception = workDefinitionTask.Exception
				});
				if (workDefinitionTask.IsFaulted)
				{
					WTFDiagnostics.TraceError<AggregateException>(this.Trace, this.TraceContext, "MapiMTDiscovery.TryAddProbeWorkDefinition() failed.  Skipping this probe's creation.  Error: {0}", workDefinitionTask.Exception, null, "CreateRelatedWorkItems", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DiscoveryWorkItem.cs", 171);
				}
			}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004DAD File Offset: 0x00002FAD
		protected TDefinition AddWorkDefinition<TDefinition>(TDefinition definition) where TDefinition : WorkDefinition
		{
			base.Broker.AddWorkDefinition<TDefinition>(definition, base.TraceContext);
			return definition;
		}

		// Token: 0x04000031 RID: 49
		private readonly ConcurrentBag<DiscoveryWorkItem.WorkItemCreationLogEntry> workItemCreationLog = new ConcurrentBag<DiscoveryWorkItem.WorkItemCreationLogEntry>();

		// Token: 0x0200000D RID: 13
		private struct WorkItemCreationLogEntry
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000040 RID: 64 RVA: 0x00004DD6 File Offset: 0x00002FD6
			// (set) Token: 0x06000041 RID: 65 RVA: 0x00004DDE File Offset: 0x00002FDE
			public WorkItemIdentity BaseIdentity { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000042 RID: 66 RVA: 0x00004DE7 File Offset: 0x00002FE7
			// (set) Token: 0x06000043 RID: 67 RVA: 0x00004DEF File Offset: 0x00002FEF
			public Exception Exception { get; set; }
		}
	}
}
