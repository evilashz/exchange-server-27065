using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000946 RID: 2374
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PublicFolderMailboxSynchronizer : DisposeTrackableBase
	{
		// Token: 0x06005863 RID: 22627 RVA: 0x0016B880 File Offset: 0x00169A80
		static PublicFolderMailboxSynchronizer()
		{
			PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterSuccessInterval = TimeSpan.FromMilliseconds((double)StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "MailboxSynchronizerSuccessInterval", 900000, (int x) => x >= 60000));
			PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterFailureInterval = TimeSpan.FromMilliseconds((double)StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "MailboxSynchronizerFailureInterval", 60000, (int x) => x >= 60000));
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x0016B944 File Offset: 0x00169B44
		public PublicFolderMailboxSynchronizer(OrganizationId organizationId, Guid mailboxGuid, string serverFqdn, bool onlyRefCounting)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			ArgumentValidator.ThrowIfNull("server", serverFqdn);
			this.tenantPartitionHint = TenantPartitionHint.FromOrganizationId(organizationId);
			this.mailboxGuid = mailboxGuid;
			this.serverFqdn = serverFqdn;
			if (!onlyRefCounting)
			{
				this.ScheduleSynchronizeHierarchy(TimeSpan.FromMilliseconds(0.0));
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06005865 RID: 22629 RVA: 0x0016B9B5 File Offset: 0x00169BB5
		public Guid MailboxGuid
		{
			get
			{
				base.CheckDisposed();
				return this.mailboxGuid;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06005866 RID: 22630 RVA: 0x0016B9C3 File Offset: 0x00169BC3
		public PublicFolderSyncJobState SyncJobState
		{
			get
			{
				base.CheckDisposed();
				return this.syncJobState;
			}
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0016B9D4 File Offset: 0x00169BD4
		private void OnSynchronizeHierarchy(object state)
		{
			lock (this.objectDisposedLock)
			{
				if (this.isInternalDisposed)
				{
					return;
				}
				if (this.synchronizeHierarchyTimer != null)
				{
					this.synchronizeHierarchyTimer.Dispose();
					this.synchronizeHierarchyTimer = null;
				}
			}
			try
			{
				OrganizationId organizationId = this.tenantPartitionHint.GetOrganizationId();
				PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.QueryStatusSyncHierarchy(organizationId, this.mailboxGuid, this.serverFqdn);
				this.syncJobState = publicFolderSyncJobState;
				if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Queued)
				{
					this.ScheduleSynchonizeHierarchyCheck(PublicFolderMailboxSynchronizer.QueryStatusSynchronizeHierarchyInterval);
				}
				else
				{
					publicFolderSyncJobState = PublicFolderSyncJobRpc.StartSyncHierarchy(organizationId, this.mailboxGuid, this.serverFqdn, false);
					this.syncJobState = publicFolderSyncJobState;
					if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Queued)
					{
						this.ScheduleSynchonizeHierarchyCheck(PublicFolderMailboxSynchronizer.QueryStatusSynchronizeHierarchyInterval);
					}
					else
					{
						this.ScheduleSynchronizeHierarchy(PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterFailureInterval);
					}
				}
			}
			catch (PublicFolderSyncTransientException)
			{
				this.ScheduleSynchronizeHierarchy(PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterFailureInterval);
			}
			catch (PublicFolderSyncPermanentException)
			{
				this.ScheduleSynchronizeHierarchy(PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterFailureInterval);
			}
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0016BAE4 File Offset: 0x00169CE4
		private void OnSynchronizeHierarchyCheck(object sender)
		{
			lock (this.objectDisposedLock)
			{
				if (this.isInternalDisposed)
				{
					return;
				}
				if (this.synchronizeHierarchyCheckTimer != null)
				{
					this.synchronizeHierarchyCheckTimer.Dispose();
					this.synchronizeHierarchyCheckTimer = null;
				}
			}
			try
			{
				OrganizationId organizationId = this.tenantPartitionHint.GetOrganizationId();
				PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.QueryStatusSyncHierarchy(organizationId, this.mailboxGuid, this.serverFqdn);
				this.syncJobState = publicFolderSyncJobState;
				if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Queued)
				{
					this.ScheduleSynchonizeHierarchyCheck(PublicFolderMailboxSynchronizer.QueryStatusSynchronizeHierarchyInterval);
				}
				else if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.None)
				{
					this.ScheduleSynchronizeHierarchy(TimeSpan.FromMilliseconds(0.0));
				}
				else if (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Completed)
				{
					if (publicFolderSyncJobState.LastError != null)
					{
						this.ScheduleSynchronizeHierarchy(PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterFailureInterval);
					}
					else
					{
						this.ScheduleSynchronizeHierarchy(PublicFolderMailboxSynchronizer.SynchronizeHierarchyAfterSuccessInterval);
					}
				}
			}
			catch (PublicFolderSyncTransientException)
			{
				this.ScheduleSynchonizeHierarchyCheck(PublicFolderMailboxSynchronizer.QueryStatusSynchronizeHierarchyInterval);
			}
			catch (PublicFolderSyncPermanentException)
			{
				this.ScheduleSynchonizeHierarchyCheck(PublicFolderMailboxSynchronizer.QueryStatusSynchronizeHierarchyInterval);
			}
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0016BC00 File Offset: 0x00169E00
		private void ScheduleSynchronizeHierarchy(TimeSpan dueTime)
		{
			lock (this.objectDisposedLock)
			{
				if (!this.isInternalDisposed)
				{
					if (this.synchronizeHierarchyTimer == null)
					{
						this.synchronizeHierarchyTimer = new Timer(new TimerCallback(this.OnSynchronizeHierarchy), null, dueTime, TimeSpan.FromMilliseconds(-1.0));
					}
				}
			}
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0016BC74 File Offset: 0x00169E74
		private void ScheduleSynchonizeHierarchyCheck(TimeSpan dueTime)
		{
			lock (this.objectDisposedLock)
			{
				if (!this.isInternalDisposed)
				{
					if (this.synchronizeHierarchyCheckTimer == null)
					{
						this.synchronizeHierarchyCheckTimer = new Timer(new TimerCallback(this.OnSynchronizeHierarchyCheck), null, dueTime, TimeSpan.FromMilliseconds(-1.0));
					}
				}
			}
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0016BCE8 File Offset: 0x00169EE8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMailboxSynchronizer>(this);
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0016BCF0 File Offset: 0x00169EF0
		protected override void InternalDispose(bool disposing)
		{
			lock (this.objectDisposedLock)
			{
				if (this.synchronizeHierarchyTimer != null)
				{
					this.synchronizeHierarchyTimer.Dispose();
					this.synchronizeHierarchyTimer = null;
				}
				if (this.synchronizeHierarchyCheckTimer != null)
				{
					this.synchronizeHierarchyCheckTimer.Dispose();
					this.synchronizeHierarchyCheckTimer = null;
				}
				this.isInternalDisposed = true;
			}
		}

		// Token: 0x04003026 RID: 12326
		private const string RegKeyPublicFolder = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder";

		// Token: 0x04003027 RID: 12327
		private const string RegValueMailboxSynchronizerSuccessInterval = "MailboxSynchronizerSuccessInterval";

		// Token: 0x04003028 RID: 12328
		private const string RegValueMailboxSynchronizerFailureInterval = "MailboxSynchronizerFailureInterval";

		// Token: 0x04003029 RID: 12329
		private const string RegValueMailboxSynchronizerQueryInterval = "MailboxSynchronizerQueryInterval";

		// Token: 0x0400302A RID: 12330
		internal static TimeSpan SynchronizeHierarchyAfterSuccessInterval;

		// Token: 0x0400302B RID: 12331
		internal static TimeSpan SynchronizeHierarchyAfterFailureInterval;

		// Token: 0x0400302C RID: 12332
		internal static TimeSpan QueryStatusSynchronizeHierarchyInterval = TimeSpan.FromMilliseconds((double)StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "MailboxSynchronizerQueryInterval", 60000, (int x) => x >= 60000));

		// Token: 0x0400302D RID: 12333
		private readonly TenantPartitionHint tenantPartitionHint;

		// Token: 0x0400302E RID: 12334
		private readonly Guid mailboxGuid;

		// Token: 0x0400302F RID: 12335
		private readonly string serverFqdn;

		// Token: 0x04003030 RID: 12336
		private Timer synchronizeHierarchyTimer;

		// Token: 0x04003031 RID: 12337
		private Timer synchronizeHierarchyCheckTimer;

		// Token: 0x04003032 RID: 12338
		protected PublicFolderSyncJobState syncJobState;

		// Token: 0x04003033 RID: 12339
		private object objectDisposedLock = new object();

		// Token: 0x04003034 RID: 12340
		private bool isInternalDisposed;
	}
}
