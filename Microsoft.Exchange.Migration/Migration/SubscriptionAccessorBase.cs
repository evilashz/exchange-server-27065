using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013D RID: 317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionAccessorBase : ISubscriptionAccessor
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x00043EFC File Offset: 0x000420FC
		public SubscriptionAccessorBase()
		{
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00043F04 File Offset: 0x00042104
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00043F0C File Offset: 0x0004210C
		public bool IncludeReport { get; set; }

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00043F18 File Offset: 0x00042118
		public static SubscriptionAccessorBase CreateAccessor(IMigrationDataProvider dataProvider, MigrationType migrationType, string jobName, bool isPAW, bool legacyManualSyncs = false)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				switch (migrationType)
				{
				case MigrationType.IMAP:
					if (isPAW)
					{
						return new MRSImapSyncRequestAccessor(dataProvider, jobName);
					}
					return new SyncSubscriptionRunspaceAccessor(dataProvider);
				case MigrationType.XO1:
					return new MRSXO1SyncRequestAccessor(dataProvider, jobName);
				case MigrationType.IMAP | MigrationType.XO1:
					goto IL_78;
				case MigrationType.ExchangeOutlookAnywhere:
					return new MRSMergeRequestAccessor(dataProvider, jobName, legacyManualSyncs);
				default:
					if (migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_78;
					}
					break;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType == MigrationType.PSTImport)
				{
					return new PSTImportAccessor(dataProvider, jobName);
				}
				if (migrationType != MigrationType.PublicFolder)
				{
					goto IL_78;
				}
				return new MrsPublicFolderAccessor(dataProvider, jobName);
			}
			return new MrsMoveRequestAccessor(dataProvider, jobName, legacyManualSyncs);
			IL_78:
			throw new ArgumentException("No accessor defined for protocol " + migrationType);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00043FB2 File Offset: 0x000421B2
		public static SubscriptionAccessorBase CreateAccessor(IMigrationDataProvider dataProvider, MigrationType type, bool isPAW)
		{
			return SubscriptionAccessorBase.CreateAccessor(dataProvider, type, null, isPAW, false);
		}

		// Token: 0x06000FF5 RID: 4085
		public abstract SubscriptionSnapshot CreateSubscription(MigrationJobItem jobItem);

		// Token: 0x06000FF6 RID: 4086
		public abstract SubscriptionSnapshot TestCreateSubscription(MigrationJobItem jobItem);

		// Token: 0x06000FF7 RID: 4087
		public abstract SnapshotStatus RetrieveSubscriptionStatus(ISubscriptionId subscriptionId);

		// Token: 0x06000FF8 RID: 4088
		public abstract SubscriptionSnapshot RetrieveSubscriptionSnapshot(ISubscriptionId subscriptionId);

		// Token: 0x06000FF9 RID: 4089
		public abstract bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool adoptingSubscription);

		// Token: 0x06000FFA RID: 4090
		public abstract bool ResumeSubscription(ISubscriptionId subscriptionId, bool finalize = false);

		// Token: 0x06000FFB RID: 4091
		public abstract bool SuspendSubscription(ISubscriptionId subscriptionId);

		// Token: 0x06000FFC RID: 4092
		public abstract bool RemoveSubscription(ISubscriptionId subscriptionId);

		// Token: 0x06000FFD RID: 4093 RVA: 0x00043FBE File Offset: 0x000421BE
		public virtual bool IsSnapshotCompatible(SubscriptionSnapshot subscriptionSnapshot, MigrationJobItem migrationJobItem)
		{
			return false;
		}
	}
}
