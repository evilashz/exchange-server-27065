using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	[DataContract]
	internal sealed class AccountSettings : ItemPropertiesBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		[DataMember]
		public string PrimaryLogin { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021BD File Offset: 0x000003BD
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021C5 File Offset: 0x000003C5
		[DataMember]
		public DateTime LastLoginTime { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021CE File Offset: 0x000003CE
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021D6 File Offset: 0x000003D6
		[DataMember]
		public int AccountStatusInt { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021DF File Offset: 0x000003DF
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021E7 File Offset: 0x000003E7
		public OlcAccountStatus AccountStatus
		{
			get
			{
				return (OlcAccountStatus)this.AccountStatusInt;
			}
			set
			{
				this.AccountStatusInt = (int)value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021F0 File Offset: 0x000003F0
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021F8 File Offset: 0x000003F8
		[DataMember]
		public ulong OimTrxSize { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002201 File Offset: 0x00000401
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002209 File Offset: 0x00000409
		[DataMember]
		public ulong OimNonTrxSize { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002212 File Offset: 0x00000412
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000221A File Offset: 0x0000041A
		[DataMember]
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002223 File Offset: 0x00000423
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000222B File Offset: 0x0000042B
		[DataMember]
		public ushort UserClassCode { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000223C File Offset: 0x0000043C
		[DataMember]
		public string DemoCode { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002245 File Offset: 0x00000445
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000224D File Offset: 0x0000044D
		[DataMember]
		public int DatFlagsInt { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000225E File Offset: 0x0000045E
		[DataMember]
		public int DatFlags2Int { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000226F File Offset: 0x0000046F
		[DataMember]
		public string[] FeatureSetList { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002278 File Offset: 0x00000478
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002280 File Offset: 0x00000480
		[DataMember]
		public string[] HMFeatureSetList { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002289 File Offset: 0x00000489
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002291 File Offset: 0x00000491
		[DataMember]
		public ulong StorageSpaceUsageLimit { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000229A File Offset: 0x0000049A
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022A2 File Offset: 0x000004A2
		[DataMember]
		public DateTime? StorageSpaceUsageLimitIncremented { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022AB File Offset: 0x000004AB
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022B3 File Offset: 0x000004B3
		[DataMember]
		public Alias[] Aliases { get; set; }
	}
}
