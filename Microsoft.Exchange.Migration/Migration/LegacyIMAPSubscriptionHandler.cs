using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyIMAPSubscriptionHandler : LegacySubscriptionHandlerBase, ILegacySubscriptionHandler, IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00045068 File Offset: 0x00043268
		internal LegacyIMAPSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob migrationJob) : base(dataProvider, migrationJob)
		{
			this.resourceAccessor = new SyncResourceAccessor(dataProvider);
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0004507E File Offset: 0x0004327E
		public MigrationType SupportedMigrationType
		{
			get
			{
				return MigrationType.IMAP;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00045081 File Offset: 0x00043281
		public bool SupportsDupeDetection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00045084 File Offset: 0x00043284
		public override bool SupportsActiveIncrementalSync
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00045087 File Offset: 0x00043287
		public override bool SupportsAdvancedValidation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004508C File Offset: 0x0004328C
		public bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			MigrationUtil.AssertOrThrow(jobItem.LocalMailbox != null, "job item should have a local mailbox before creating subscriptions", new object[0]);
			MigrationUtil.AssertOrThrow(jobItem.SubscriptionSettings != null, "job item should have subscription settings before creating subscriptions", new object[0]);
			MigrationUtil.AssertOrThrow(base.Job.SubscriptionSettings != null, "job should have subscription settings before creating subscriptions", new object[0]);
			ImapEndpoint imapEndpoint = (ImapEndpoint)base.Job.SourceEndpoint;
			IMAPJobItemSubscriptionSettings imapjobItemSubscriptionSettings = (IMAPJobItemSubscriptionSettings)jobItem.SubscriptionSettings;
			IMAPJobSubscriptionSettings imapjobSubscriptionSettings = (IMAPJobSubscriptionSettings)base.Job.SubscriptionSettings;
			CreateIMAPSyncSubscriptionArgs subscriptionCreationArgs = new CreateIMAPSyncSubscriptionArgs(base.DataProvider.OrganizationId.OrganizationalUnit, ((MailboxData)jobItem.LocalMailbox).MailboxLegacyDN, jobItem.Identifier, jobItem.Identifier, SmtpAddress.Parse(jobItem.Identifier), imapjobItemSubscriptionSettings.Username, imapjobItemSubscriptionSettings.EncryptedPassword, imapEndpoint.RemoteServer, imapEndpoint.Port, imapjobSubscriptionSettings.ExcludedFolders, imapEndpoint.Security, imapEndpoint.Authentication, imapjobItemSubscriptionSettings.UserRootFolder, false);
			this.resourceAccessor.CreateSubscription(jobItem, subscriptionCreationArgs);
			return true;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000451A7 File Offset: 0x000433A7
		public bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			throw new NotImplementedException("TestCreateUnderlyingSubscriptions is not available for IMAP");
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000451B3 File Offset: 0x000433B3
		public MigrationProcessorResult SyncToUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			return ItemStateTransitionHelper.ProcessDelayedSubscription(base.DataProvider, base.Job, jobItem);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000451D4 File Offset: 0x000433D4
		public void DeleteUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "disabling IMAP subscription for item {0} because handler DOES NOT support dupe detection", new object[]
			{
				jobItem
			});
			base.StopUnderlyingSubscriptions(jobItem);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000451FF File Offset: 0x000433FF
		public void ResumeUnderlyingSubscriptions(MigrationUserStatus startedStatus, MigrationJobItem jobItem)
		{
			this.resourceAccessor.FinalizeSubscription(jobItem, MigrationUserStatus.Completing, MigrationUserStatus.Completed, MigrationUserStatus.IncrementalFailed);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00045210 File Offset: 0x00043410
		public override void DisableSubscriptions(MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			try
			{
				this.resourceAccessor.UpdateSubscription(jobItem, UpdateSyncSubscriptionAction.Disable);
			}
			catch (MigrationPermanentException innerException)
			{
				throw new UnableToDisableSubscriptionTransientException(innerException)
				{
					InternalError = "Unable to disable subscription for " + jobItem
				};
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00045264 File Offset: 0x00043464
		public override void SyncSubscriptionSettings(MigrationJobItem jobItem)
		{
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00045266 File Offset: 0x00043466
		public override IEnumerable<MigrationJobItem> GetJobItemsForSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck)
		{
			return MigrationJobItem.GetBySubscriptionLastChecked(base.DataProvider, base.Job, cutoffTime, status, maxItemsToCheck);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004527C File Offset: 0x0004347C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LegacyIMAPSubscriptionHandler>(this);
		}

		// Token: 0x040005BB RID: 1467
		private readonly SyncResourceAccessor resourceAccessor;
	}
}
