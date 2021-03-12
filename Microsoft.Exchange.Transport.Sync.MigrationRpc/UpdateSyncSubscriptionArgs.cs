using System;
using System.Globalization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpdateSyncSubscriptionArgs
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000332B File Offset: 0x0000152B
		internal UpdateSyncSubscriptionArgs(ADObjectId organizationalUnit, string userLegacyDN, StoreObjectId subscriptionMessageId, AggregationSubscriptionType subscriptionType, UpdateSyncSubscriptionAction action)
		{
			this.OrganizationalUnit = organizationalUnit;
			this.userLegacyDN = userLegacyDN;
			this.subscriptionMessageId = subscriptionMessageId;
			this.subscriptionType = subscriptionType;
			this.action = action;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003358 File Offset: 0x00001558
		internal UpdateSyncSubscriptionArgs(ADObjectId organizationalUnit, string userLegacyDN, Guid subscriptionId, AggregationSubscriptionType subscriptionType, UpdateSyncSubscriptionAction action)
		{
			this.OrganizationalUnit = organizationalUnit;
			this.userLegacyDN = userLegacyDN;
			this.subscriptionId = new Guid?(subscriptionId);
			this.subscriptionType = subscriptionType;
			this.action = action;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000338A File Offset: 0x0000158A
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003392 File Offset: 0x00001592
		internal ADObjectId OrganizationalUnit { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000339B File Offset: 0x0000159B
		internal string UserLegacyDN
		{
			get
			{
				return this.userLegacyDN;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000033A3 File Offset: 0x000015A3
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000033AB File Offset: 0x000015AB
		internal Guid? SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000033B3 File Offset: 0x000015B3
		internal AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000033BB File Offset: 0x000015BB
		internal UpdateSyncSubscriptionAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033C4 File Offset: 0x000015C4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Action: {0}, Id: {1}, UserLegacyDN: {2}, Type: {3}", new object[]
			{
				this.Action,
				(this.subscriptionId != null) ? this.subscriptionId.Value.ToString() : this.subscriptionMessageId.ToString(),
				this.userLegacyDN,
				this.SubscriptionType
			});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000344C File Offset: 0x0000164C
		internal static UpdateSyncSubscriptionArgs UnMarshal(MdbefPropertyCollection inputArgs)
		{
			byte[] subscriptonMessageIdBytes;
			if (MigrationRpcHelper.TryReadValue<byte[]>(inputArgs, 2684551426U, out subscriptonMessageIdBytes))
			{
				return new UpdateSyncSubscriptionArgs(MigrationRpcHelper.ReadADObjectId(inputArgs, 2688811266U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2684485663U), MigrationRpcHelper.TryDeserializeStoreObjectId(subscriptonMessageIdBytes), MigrationRpcHelper.ReadEnum<AggregationSubscriptionType>(inputArgs, 2684616707U), MigrationRpcHelper.ReadEnum<UpdateSyncSubscriptionAction>(inputArgs, 2162691U));
			}
			return new UpdateSyncSubscriptionArgs(MigrationRpcHelper.ReadADObjectId(inputArgs, 2688811266U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2684485663U), MigrationRpcHelper.ReadValue<Guid>(inputArgs, 2228296U), MigrationRpcHelper.ReadEnum<AggregationSubscriptionType>(inputArgs, 2684616707U), MigrationRpcHelper.ReadEnum<UpdateSyncSubscriptionAction>(inputArgs, 2162691U));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034DC File Offset: 0x000016DC
		internal MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			if (this.subscriptionMessageId != null)
			{
				mdbefPropertyCollection[2684551426U] = MigrationRpcHelper.SerializeStoreObjectId(this.subscriptionMessageId);
			}
			else
			{
				if (this.SubscriptionId == null)
				{
					throw new MigrationCommunicationException(MigrationServiceRpcResultCode.ArgumentMismatchError, "No valid identity specified");
				}
				mdbefPropertyCollection[2228296U] = this.SubscriptionId;
			}
			mdbefPropertyCollection[2684485663U] = this.userLegacyDN;
			mdbefPropertyCollection[2684616707U] = (int)this.subscriptionType;
			mdbefPropertyCollection[2162691U] = (int)this.action;
			mdbefPropertyCollection[2688811266U] = this.OrganizationalUnit.GetBytes();
			return mdbefPropertyCollection;
		}

		// Token: 0x0400008C RID: 140
		private readonly string userLegacyDN;

		// Token: 0x0400008D RID: 141
		private readonly StoreObjectId subscriptionMessageId;

		// Token: 0x0400008E RID: 142
		private readonly Guid? subscriptionId;

		// Token: 0x0400008F RID: 143
		private readonly AggregationSubscriptionType subscriptionType;

		// Token: 0x04000090 RID: 144
		private readonly UpdateSyncSubscriptionAction action;
	}
}
