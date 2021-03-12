using System;
using System.Globalization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateMigrationRequestArgs : ISubscriptionStatus
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000359C File Offset: 0x0000179C
		internal UpdateMigrationRequestArgs(string userExchangeMailboxSmtpAddress, string userExchangeMailboxLegacyDN, string migrationMailboxLegacyDN, ADObjectId organizationalUnit, StoreObjectId subscriptionMessageId, AggregationStatus subscriptionStatus, DetailedAggregationStatus subscriptionDetailedStatus, MigrationSubscriptionStatus migrationSubscriptionStatus, bool initialSyncComplete, DateTime? lastSyncTime, DateTime? lastSuccessfulSyncTime, long? itemsSynced, long? itemsSkipped, DateTime? lastSyncNowRequestTime)
		{
			this.userExchangeMailboxSmtpAddress = userExchangeMailboxSmtpAddress;
			this.userExchangeMailboxLegacyDN = userExchangeMailboxLegacyDN;
			this.migrationMailboxLegacyDN = migrationMailboxLegacyDN;
			this.organizationalUnit = organizationalUnit;
			this.subscriptionMessageId = subscriptionMessageId;
			this.subscriptionStatus = subscriptionStatus;
			this.subscriptionDetailedStatus = subscriptionDetailedStatus;
			this.migrationSubscriptionStatus = migrationSubscriptionStatus;
			this.initialSyncComplete = initialSyncComplete;
			this.lastSyncTime = lastSyncTime;
			this.lastSuccessfulSyncTime = lastSuccessfulSyncTime;
			this.itemsSynced = itemsSynced;
			this.itemsSkipped = itemsSkipped;
			this.lastSyncNowRequestTime = lastSyncNowRequestTime;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000361C File Offset: 0x0000181C
		public bool IsInitialSyncComplete
		{
			get
			{
				return this.initialSyncComplete;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003624 File Offset: 0x00001824
		public AggregationStatus Status
		{
			get
			{
				return this.subscriptionStatus;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000362C File Offset: 0x0000182C
		public DetailedAggregationStatus SubStatus
		{
			get
			{
				return this.subscriptionDetailedStatus;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003634 File Offset: 0x00001834
		public MigrationSubscriptionStatus MigrationSubscriptionStatus
		{
			get
			{
				return this.migrationSubscriptionStatus;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000363C File Offset: 0x0000183C
		public DateTime? LastSyncTime
		{
			get
			{
				return this.lastSyncTime;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003644 File Offset: 0x00001844
		public DateTime? LastSuccessfulSyncTime
		{
			get
			{
				return this.lastSuccessfulSyncTime;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000364C File Offset: 0x0000184C
		public DateTime? LastSyncNowRequestTime
		{
			get
			{
				return this.lastSyncNowRequestTime;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003654 File Offset: 0x00001854
		public long? ItemsSynced
		{
			get
			{
				return this.itemsSynced;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000365C File Offset: 0x0000185C
		public long? ItemsSkipped
		{
			get
			{
				return this.itemsSkipped;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003664 File Offset: 0x00001864
		internal string MigrationMailboxUserLegacyDN
		{
			get
			{
				return this.migrationMailboxLegacyDN;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000366C File Offset: 0x0000186C
		internal string UserExchangeMailboxSmtpAddress
		{
			get
			{
				return this.userExchangeMailboxSmtpAddress;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003674 File Offset: 0x00001874
		internal string UserExchangeMailboxLegacyDN
		{
			get
			{
				return this.userExchangeMailboxLegacyDN;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000367C File Offset: 0x0000187C
		internal ADObjectId OrganizationalUnit
		{
			get
			{
				return this.organizationalUnit;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003684 File Offset: 0x00001884
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000368C File Offset: 0x0000188C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "UpdateMigrationRequestArgs: Status: [{0}], Substatus: [{1}], LastSync: [{2}], LastGoodSync: [{3}], InitialSyncDone: [{4}]", new object[]
			{
				this.Status,
				this.SubStatus,
				this.LastSyncTime,
				this.LastSuccessfulSyncTime,
				this.IsInitialSyncComplete
			});
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000036F8 File Offset: 0x000018F8
		internal static UpdateMigrationRequestArgs UnMarshal(MdbefPropertyCollection inputArgs)
		{
			byte[] subscriptonMessageIdBytes = MigrationRpcHelper.ReadValue<byte[]>(inputArgs, 2684551426U);
			string text = MigrationRpcHelper.ReadValue<string>(inputArgs, 2684420127U);
			ADObjectId adobjectId = MigrationRpcHelper.ReadADObjectId(inputArgs, 2688811266U);
			StoreObjectId storeObjectId = MigrationRpcHelper.TryDeserializeStoreObjectId(subscriptonMessageIdBytes);
			AggregationStatus aggregationStatus = MigrationRpcHelper.ReadEnum<AggregationStatus>(inputArgs, 2684616707U);
			DetailedAggregationStatus detailedAggregationStatus = DetailedAggregationStatus.None;
			object obj;
			if (inputArgs.TryGetValue(2684682243U, out obj))
			{
				detailedAggregationStatus = (DetailedAggregationStatus)obj;
			}
			bool flag = MigrationRpcHelper.ReadValue<bool>(inputArgs, 2684485643U);
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			long dateData;
			if (MigrationRpcHelper.TryReadValue<long>(inputArgs, 2684813332U, out dateData))
			{
				dateTime = new DateTime?(DateTime.FromBinary(dateData));
			}
			if (MigrationRpcHelper.TryReadValue<long>(inputArgs, 2684747796U, out dateData))
			{
				dateTime2 = new DateTime?(DateTime.FromBinary(dateData));
			}
			int num;
			MigrationSubscriptionStatus migrationSubscriptionStatus;
			if (MigrationRpcHelper.TryReadValue<int>(inputArgs, 2684878851U, out num))
			{
				migrationSubscriptionStatus = (MigrationSubscriptionStatus)num;
			}
			else
			{
				migrationSubscriptionStatus = MigrationSubscriptionStatus.None;
			}
			string text2 = MigrationRpcHelper.ReadValue<string>(inputArgs, 2684944415U, null);
			string text3 = MigrationRpcHelper.ReadValue<string>(inputArgs, 2685206559U, null);
			long? num2 = null;
			long value;
			if (MigrationRpcHelper.TryReadValue<long>(inputArgs, 2685009940U, out value))
			{
				num2 = new long?(value);
			}
			long? num3 = null;
			long value2;
			if (MigrationRpcHelper.TryReadValue<long>(inputArgs, 2685075476U, out value2))
			{
				num3 = new long?(value2);
			}
			DateTime? dateTime3 = null;
			long dateData2;
			if (MigrationRpcHelper.TryReadValue<long>(inputArgs, 2685141012U, out dateData2))
			{
				dateTime3 = new DateTime?(DateTime.FromBinary(dateData2));
			}
			return new UpdateMigrationRequestArgs(text2, text3, text, adobjectId, storeObjectId, aggregationStatus, detailedAggregationStatus, migrationSubscriptionStatus, flag, dateTime, dateTime2, num2, num3, dateTime3);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003868 File Offset: 0x00001A68
		internal MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684420127U] = this.migrationMailboxLegacyDN;
			mdbefPropertyCollection[2685206559U] = this.UserExchangeMailboxLegacyDN;
			mdbefPropertyCollection[2688811266U] = this.organizationalUnit.GetBytes();
			mdbefPropertyCollection[2684551426U] = MigrationRpcHelper.SerializeStoreObjectId(this.subscriptionMessageId);
			mdbefPropertyCollection[2684616707U] = (int)this.subscriptionStatus;
			mdbefPropertyCollection[2684682243U] = (int)this.subscriptionDetailedStatus;
			mdbefPropertyCollection[2684485643U] = this.initialSyncComplete;
			mdbefPropertyCollection[2684878851U] = (int)this.migrationSubscriptionStatus;
			mdbefPropertyCollection[2684944415U] = this.userExchangeMailboxSmtpAddress;
			if (this.LastSuccessfulSyncTime != null)
			{
				mdbefPropertyCollection[2684747796U] = this.LastSuccessfulSyncTime.Value.ToBinary();
			}
			if (this.LastSyncTime != null)
			{
				mdbefPropertyCollection[2684813332U] = this.LastSyncTime.Value.ToBinary();
			}
			if (this.ItemsSynced != null)
			{
				mdbefPropertyCollection[2685009940U] = this.ItemsSynced.Value;
			}
			if (this.ItemsSkipped != null)
			{
				mdbefPropertyCollection[2685075476U] = this.ItemsSkipped.Value;
			}
			if (this.LastSyncNowRequestTime != null)
			{
				mdbefPropertyCollection[2685141012U] = this.LastSyncNowRequestTime.Value.ToBinary();
			}
			return mdbefPropertyCollection;
		}

		// Token: 0x04000092 RID: 146
		private readonly string migrationMailboxLegacyDN;

		// Token: 0x04000093 RID: 147
		private readonly string userExchangeMailboxSmtpAddress;

		// Token: 0x04000094 RID: 148
		private readonly string userExchangeMailboxLegacyDN;

		// Token: 0x04000095 RID: 149
		private readonly ADObjectId organizationalUnit;

		// Token: 0x04000096 RID: 150
		private readonly bool initialSyncComplete;

		// Token: 0x04000097 RID: 151
		private readonly StoreObjectId subscriptionMessageId;

		// Token: 0x04000098 RID: 152
		private readonly AggregationStatus subscriptionStatus;

		// Token: 0x04000099 RID: 153
		private readonly DetailedAggregationStatus subscriptionDetailedStatus;

		// Token: 0x0400009A RID: 154
		private readonly MigrationSubscriptionStatus migrationSubscriptionStatus;

		// Token: 0x0400009B RID: 155
		private readonly DateTime? lastSyncTime;

		// Token: 0x0400009C RID: 156
		private readonly DateTime? lastSuccessfulSyncTime;

		// Token: 0x0400009D RID: 157
		private readonly long? itemsSynced;

		// Token: 0x0400009E RID: 158
		private readonly long? itemsSkipped;

		// Token: 0x0400009F RID: 159
		private readonly DateTime? lastSyncNowRequestTime;
	}
}
