using System;
using System.Globalization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetSyncSubscriptionStateResult : MigrationServiceRpcResult, ISubscriptionStatus
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003BA4 File Offset: 0x00001DA4
		internal GetSyncSubscriptionStateResult(MdbefPropertyCollection args, MigrationServiceRpcMethodCode expectedMethodCode) : base(args)
		{
			object obj;
			if (args.TryGetValue(2936143875U, out obj))
			{
				this.status = (AggregationStatus)obj;
			}
			if (args.TryGetValue(2936209411U, out obj))
			{
				this.detailedStatus = (DetailedAggregationStatus)obj;
			}
			if (args.TryGetValue(2936406027U, out obj))
			{
				this.isInitialSyncComplete = (bool)obj;
			}
			if (args.TryGetValue(2936537108U, out obj))
			{
				this.lastSyncTime = new DateTime?(DateTime.FromBinary((long)obj));
			}
			if (args.TryGetValue(2936471572U, out obj))
			{
				this.lastSuccessfulSyncTime = new DateTime?(DateTime.FromBinary((long)obj));
			}
			if (args.TryGetValue(2936602644U, out obj))
			{
				this.itemsSynced = new long?((long)obj);
			}
			if (args.TryGetValue(2936668180U, out obj))
			{
				this.itemsSkipped = new long?((long)obj);
			}
			if (args.TryGetValue(2936733716U, out obj))
			{
				this.lastSyncNowRequestTime = new DateTime?(DateTime.FromBinary((long)obj));
			}
			base.ThrowIfVerifyFails(expectedMethodCode);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003CC8 File Offset: 0x00001EC8
		internal GetSyncSubscriptionStateResult(MigrationServiceRpcMethodCode methodCode, AggregationStatus status, DetailedAggregationStatus substatus, MigrationSubscriptionStatus migrationSubscriptionStatus, bool isInitialSyncComplete, DateTime? lastSyncTime, DateTime? lastSuccessfulSyncTime, long? itemsSynced, long? itemsSkipped, DateTime? lastSyncNowRequestTime) : base(methodCode)
		{
			this.status = status;
			this.detailedStatus = substatus;
			this.isInitialSyncComplete = isInitialSyncComplete;
			this.lastSyncTime = lastSyncTime;
			this.lastSuccessfulSyncTime = lastSuccessfulSyncTime;
			this.itemsSynced = itemsSynced;
			this.itemsSkipped = itemsSkipped;
			this.migrationStatus = migrationSubscriptionStatus;
			this.lastSyncNowRequestTime = lastSyncNowRequestTime;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003D29 File Offset: 0x00001F29
		internal GetSyncSubscriptionStateResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails) : base(methodCode, resultCode, errorDetails)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003D3B File Offset: 0x00001F3B
		public AggregationStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003D43 File Offset: 0x00001F43
		public DetailedAggregationStatus SubStatus
		{
			get
			{
				return this.detailedStatus;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003D4B File Offset: 0x00001F4B
		public bool IsInitialSyncComplete
		{
			get
			{
				return this.isInitialSyncComplete;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003D53 File Offset: 0x00001F53
		public DateTime? LastSyncTime
		{
			get
			{
				return this.lastSyncTime;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003D5B File Offset: 0x00001F5B
		public DateTime? LastSuccessfulSyncTime
		{
			get
			{
				return this.lastSuccessfulSyncTime;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D63 File Offset: 0x00001F63
		public DateTime? LastSyncNowRequestTime
		{
			get
			{
				return this.lastSyncNowRequestTime;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003D6B File Offset: 0x00001F6B
		public long? ItemsSynced
		{
			get
			{
				return this.itemsSynced;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D73 File Offset: 0x00001F73
		public long? ItemsSkipped
		{
			get
			{
				return this.itemsSkipped;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003D7B File Offset: 0x00001F7B
		public MigrationSubscriptionStatus MigrationSubscriptionStatus
		{
			get
			{
				return this.migrationStatus;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D84 File Offset: 0x00001F84
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "GetSyncSubscriptionStateResult: Status: [{0}], Substatus: [{1}], LastSync: [{2}], LastGoodSync: [{3}], InitialSyncDone: [{4}]", new object[]
			{
				this.Status,
				this.SubStatus,
				this.LastSyncTime,
				this.LastSuccessfulSyncTime,
				this.IsInitialSyncComplete
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003DF0 File Offset: 0x00001FF0
		protected override void WriteTo(MdbefPropertyCollection collection)
		{
			collection[2936143875U] = (int)this.status;
			collection[2936209411U] = (int)this.detailedStatus;
			collection[2936406027U] = this.isInitialSyncComplete;
			if (this.lastSyncTime != null)
			{
				collection[2936537108U] = this.LastSyncTime.Value.ToBinary();
			}
			if (this.lastSuccessfulSyncTime != null)
			{
				collection[2936471572U] = this.LastSuccessfulSyncTime.Value.ToBinary();
			}
			if (this.itemsSynced != null)
			{
				collection[2936602644U] = this.itemsSynced.Value;
			}
			if (this.itemsSkipped != null)
			{
				collection[2936668180U] = this.itemsSkipped.Value;
			}
			if (this.lastSyncNowRequestTime != null)
			{
				collection[2936733716U] = this.lastSyncNowRequestTime.Value.ToBinary();
			}
		}

		// Token: 0x040000A2 RID: 162
		private readonly AggregationStatus status;

		// Token: 0x040000A3 RID: 163
		private readonly DetailedAggregationStatus detailedStatus;

		// Token: 0x040000A4 RID: 164
		private readonly MigrationSubscriptionStatus migrationStatus = MigrationSubscriptionStatus.None;

		// Token: 0x040000A5 RID: 165
		private readonly DateTime? lastSyncTime;

		// Token: 0x040000A6 RID: 166
		private readonly DateTime? lastSuccessfulSyncTime;

		// Token: 0x040000A7 RID: 167
		private readonly bool isInitialSyncComplete;

		// Token: 0x040000A8 RID: 168
		private readonly long? itemsSynced;

		// Token: 0x040000A9 RID: 169
		private readonly long? itemsSkipped;

		// Token: 0x040000AA RID: 170
		private readonly DateTime? lastSyncNowRequestTime;
	}
}
