using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractCreateSyncSubscriptionArgs
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002BBA File Offset: 0x00000DBA
		internal AbstractCreateSyncSubscriptionArgs(AggregationSubscriptionType subscriptionType, ADObjectId organizationalUnit, string subscriptionName, string userLegacyDN, string userDisplayName, SmtpAddress remoteEmailAddress, bool forceNew)
		{
			this.SubscriptionType = subscriptionType;
			this.OrganizationalUnit = organizationalUnit;
			this.userLegacyDn = userLegacyDN;
			this.subscriptionName = subscriptionName;
			this.userDisplayName = userDisplayName;
			this.remoteEmailAddress = remoteEmailAddress;
			this.forceNew = forceNew;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BF7 File Offset: 0x00000DF7
		internal string SubscriptionName
		{
			get
			{
				return this.subscriptionName;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002BFF File Offset: 0x00000DFF
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002C07 File Offset: 0x00000E07
		internal AggregationSubscriptionType SubscriptionType { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C10 File Offset: 0x00000E10
		internal string UserLegacyDn
		{
			get
			{
				return this.userLegacyDn;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C18 File Offset: 0x00000E18
		internal string UserDisplayName
		{
			get
			{
				return this.userDisplayName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C20 File Offset: 0x00000E20
		internal SmtpAddress SmtpAddress
		{
			get
			{
				return this.remoteEmailAddress;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C28 File Offset: 0x00000E28
		internal bool ForceNew
		{
			get
			{
				return this.forceNew;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C30 File Offset: 0x00000E30
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002C38 File Offset: 0x00000E38
		internal ADObjectId OrganizationalUnit { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002C44 File Offset: 0x00000E44
		internal static AbstractCreateSyncSubscriptionArgs Create(MdbefPropertyCollection inputArgs, int version)
		{
			if (version == 1)
			{
				return CreateIMAPSyncSubscriptionArgs.Unmarshal(inputArgs);
			}
			if (version > 2)
			{
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.VersionMismatchError, string.Format("This method was invoked with a version too high : {0}", version));
			}
			AggregationSubscriptionType aggregationSubscriptionType = MigrationRpcHelper.ReadEnum<AggregationSubscriptionType>(inputArgs, 2684616707U);
			AggregationSubscriptionType aggregationSubscriptionType2 = aggregationSubscriptionType;
			if (aggregationSubscriptionType2 == AggregationSubscriptionType.IMAP)
			{
				return CreateIMAPSyncSubscriptionArgs.Unmarshal(inputArgs);
			}
			throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.VersionMismatchError, string.Format("This method was invoked with an unsupported aggregation type {0}", aggregationSubscriptionType));
		}

		// Token: 0x06000045 RID: 69
		internal abstract AggregationSubscription CreateInMemorySubscription();

		// Token: 0x06000046 RID: 70 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal virtual MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684616707U] = (int)this.SubscriptionType;
			mdbefPropertyCollection[2688811266U] = this.OrganizationalUnit.GetBytes();
			mdbefPropertyCollection[2684485663U] = this.UserLegacyDn;
			mdbefPropertyCollection[2685403167U] = this.SubscriptionName;
			mdbefPropertyCollection[2685927455U] = this.userDisplayName;
			mdbefPropertyCollection[2685599775U] = this.SmtpAddress.ToString();
			mdbefPropertyCollection[2686255115U] = this.forceNew;
			return mdbefPropertyCollection;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D58 File Offset: 0x00000F58
		internal virtual void FillSubscription(AggregationSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			subscription.AggregationType = AggregationType.Migration;
			subscription.SubscriptionEvents = (SubscriptionEvents.WorkItemCompleted | SubscriptionEvents.WorkItemFailedLoadSubscription);
			subscription.Name = this.SubscriptionName;
		}

		// Token: 0x0400006F RID: 111
		private readonly string userLegacyDn;

		// Token: 0x04000070 RID: 112
		private readonly string subscriptionName;

		// Token: 0x04000071 RID: 113
		private readonly string userDisplayName;

		// Token: 0x04000072 RID: 114
		private readonly SmtpAddress remoteEmailAddress;

		// Token: 0x04000073 RID: 115
		private readonly bool forceNew;
	}
}
