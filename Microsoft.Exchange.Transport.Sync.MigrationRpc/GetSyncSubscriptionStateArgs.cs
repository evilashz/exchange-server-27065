using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GetSyncSubscriptionStateArgs
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003121 File Offset: 0x00001321
		internal GetSyncSubscriptionStateArgs(ADObjectId organizationalUnit, string userLegacyDN, StoreObjectId subscriptionMessageId, AggregationSubscriptionType subscriptionType)
		{
			this.userLegacyDN = userLegacyDN;
			this.subscriptionMessageId = subscriptionMessageId;
			this.subscriptionType = subscriptionType;
			this.OrganizationalUnit = organizationalUnit;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003146 File Offset: 0x00001346
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000314E File Offset: 0x0000134E
		internal ADObjectId OrganizationalUnit { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003157 File Offset: 0x00001357
		internal string UserLegacyDN
		{
			get
			{
				return this.userLegacyDN;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000315F File Offset: 0x0000135F
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003167 File Offset: 0x00001367
		internal AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003170 File Offset: 0x00001370
		internal static GetSyncSubscriptionStateArgs UnMarshal(MdbefPropertyCollection inputArgs)
		{
			byte[] subscriptonMessageIdBytes = MigrationRpcHelper.ReadValue<byte[]>(inputArgs, 2684551426U);
			return new GetSyncSubscriptionStateArgs(MigrationRpcHelper.ReadADObjectId(inputArgs, 2688811266U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2684485663U), MigrationRpcHelper.TryDeserializeStoreObjectId(subscriptonMessageIdBytes), MigrationRpcHelper.ReadEnum<AggregationSubscriptionType>(inputArgs, 2684616707U));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031B8 File Offset: 0x000013B8
		internal MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2688811266U] = this.OrganizationalUnit.GetBytes();
			mdbefPropertyCollection[2684485663U] = this.userLegacyDN;
			mdbefPropertyCollection[2684551426U] = MigrationRpcHelper.SerializeStoreObjectId(this.subscriptionMessageId);
			mdbefPropertyCollection[2684616707U] = (int)this.subscriptionType;
			return mdbefPropertyCollection;
		}

		// Token: 0x04000080 RID: 128
		private readonly string userLegacyDN;

		// Token: 0x04000081 RID: 129
		private readonly StoreObjectId subscriptionMessageId;

		// Token: 0x04000082 RID: 130
		private readonly AggregationSubscriptionType subscriptionType;
	}
}
