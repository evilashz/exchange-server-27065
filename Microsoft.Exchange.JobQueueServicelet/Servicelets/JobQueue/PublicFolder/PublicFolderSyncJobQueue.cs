using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderSyncJobQueue : JobQueue, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000055E0 File Offset: 0x000037E0
		public PublicFolderSyncJobQueue() : base(QueueType.PublicFolder, new Configuration(10, 10, TimeSpan.FromMilliseconds(10.0)))
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005634 File Offset: 0x00003834
		public override void OnJobCompletion(Job job)
		{
			PublicFolderSyncJob publicFolderSyncJob = (PublicFolderSyncJob)job;
			lock (this.lockObject)
			{
				this.queuedPublicFolderSyncJobs.Remove(publicFolderSyncJob.ContentMailboxGuid);
				this.completedPublicFolderSyncJobs.AddAbsolute(publicFolderSyncJob.ContentMailboxGuid, publicFolderSyncJob, PublicFolderSyncJobQueue.completedSyncJobExpirationTime, null);
			}
			base.OnJobCompletion(job);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000056A8 File Offset: 0x000038A8
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000056CA File Offset: 0x000038CA
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderSyncJobQueue>(this);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000056D2 File Offset: 0x000038D2
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000056E8 File Offset: 0x000038E8
		protected override bool TryCreateJob(byte[] data, out Job job, out EnqueueResult result)
		{
			job = null;
			if (data == null)
			{
				result = new EnqueueResult(EnqueueResultType.InvalidData, "Null arguments");
				return false;
			}
			PublicFolderSyncJobRpcInParameters publicFolderSyncJobRpcInParameters = null;
			try
			{
				publicFolderSyncJobRpcInParameters = new PublicFolderSyncJobRpcInParameters(data);
				if (publicFolderSyncJobRpcInParameters.ContentMailboxGuid == Guid.Empty)
				{
					result = new EnqueueResult(EnqueueResultType.InvalidData, "Empty ContentMailboxGuid");
				}
			}
			catch (SerializationException ex)
			{
				result = new EnqueueResult(EnqueueResultType.InvalidData, ex.Message);
				return false;
			}
			if (publicFolderSyncJobRpcInParameters.SyncAction == PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.SyncFolder)
			{
				LocalizedException lastError = null;
				try
				{
					using (PublicFolderSynchronizerContext publicFolderSynchronizerContext = new PublicFolderSynchronizerContext(publicFolderSyncJobRpcInParameters.OrganizationId, publicFolderSyncJobRpcInParameters.ContentMailboxGuid, true, false, Guid.NewGuid()))
					{
						PublicFolderHierarchySyncExecutor publicFolderHierarchySyncExecutor = PublicFolderHierarchySyncExecutor.CreateForSingleFolderSync(publicFolderSynchronizerContext);
						publicFolderHierarchySyncExecutor.SyncSingleFolder(publicFolderSyncJobRpcInParameters.FolderId);
					}
				}
				catch (PublicFolderSyncPermanentException ex2)
				{
					lastError = ex2;
				}
				catch (PublicFolderSyncTransientException ex3)
				{
					lastError = ex3;
				}
				result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.None, lastError));
				return false;
			}
			if (publicFolderSyncJobRpcInParameters.SyncAction == PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.StartSyncHierarchy || publicFolderSyncJobRpcInParameters.SyncAction == PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.StartSyncHierarchyWithFolderReconciliation)
			{
				lock (this.lockObject)
				{
					if (this.completedPublicFolderSyncJobs.Contains(publicFolderSyncJobRpcInParameters.ContentMailboxGuid))
					{
						this.completedPublicFolderSyncJobs.Remove(publicFolderSyncJobRpcInParameters.ContentMailboxGuid);
					}
					if (this.queuedPublicFolderSyncJobs.ContainsKey(publicFolderSyncJobRpcInParameters.ContentMailboxGuid))
					{
						result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.Queued, null));
						return false;
					}
					result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.Queued, null));
					job = new PublicFolderSyncJob(this, publicFolderSyncJobRpcInParameters.OrganizationId, publicFolderSyncJobRpcInParameters.ContentMailboxGuid, publicFolderSyncJobRpcInParameters.SyncAction == PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.StartSyncHierarchyWithFolderReconciliation);
					this.queuedPublicFolderSyncJobs[publicFolderSyncJobRpcInParameters.ContentMailboxGuid] = (PublicFolderSyncJob)job;
					return true;
				}
			}
			if (publicFolderSyncJobRpcInParameters.SyncAction == PublicFolderSyncJobRpcInParameters.PublicFolderSyncAction.QueryStatusSyncHierarchy)
			{
				lock (this.lockObject)
				{
					PublicFolderSyncJob publicFolderSyncJob = null;
					if (this.queuedPublicFolderSyncJobs.TryGetValue(publicFolderSyncJobRpcInParameters.ContentMailboxGuid, out publicFolderSyncJob))
					{
						result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.Queued, null));
					}
					else if (this.completedPublicFolderSyncJobs.TryGetValue(publicFolderSyncJobRpcInParameters.ContentMailboxGuid, out publicFolderSyncJob))
					{
						result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.Completed, (LocalizedException)publicFolderSyncJob.LastError));
					}
					else
					{
						result = new PublicFolderSyncJobEnqueueResult(EnqueueResultType.Successful, new PublicFolderSyncJobState(PublicFolderSyncJobState.Status.None, null));
					}
					return false;
				}
			}
			throw new InvalidOperationException(string.Format("Should not have reached here. SyncAction: {0}", publicFolderSyncJobRpcInParameters.SyncAction));
		}

		// Token: 0x04000064 RID: 100
		private static TimeSpan completedSyncJobExpirationTime = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000065 RID: 101
		private TimeoutCache<Guid, PublicFolderSyncJob> completedPublicFolderSyncJobs = new TimeoutCache<Guid, PublicFolderSyncJob>(1, 1000, false);

		// Token: 0x04000066 RID: 102
		private Dictionary<Guid, PublicFolderSyncJob> queuedPublicFolderSyncJobs = new Dictionary<Guid, PublicFolderSyncJob>();

		// Token: 0x04000067 RID: 103
		private object lockObject = new object();

		// Token: 0x04000068 RID: 104
		private DisposeTracker disposeTracker;
	}
}
