using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001CD RID: 461
	internal sealed class TrafficUserDetail : ConfigurablePropertyBag
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0003A8C9 File Offset: 0x00038AC9
		public TrafficUserDetail()
		{
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003A8D1 File Offset: 0x00038AD1
		internal TrafficUserDetail(Guid tenantId, DataTable tvpTrafficUser)
		{
			this.TenantId = tenantId;
			this.TvpTrafficUser = tvpTrafficUser;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0003A8E7 File Offset: 0x00038AE7
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0003A8F9 File Offset: 0x00038AF9
		public Guid TenantId
		{
			get
			{
				return (Guid)this[TrafficUserDetail.TenantIdProp];
			}
			internal set
			{
				this[TrafficUserDetail.TenantIdProp] = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0003A90C File Offset: 0x00038B0C
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x0003A91E File Offset: 0x00038B1E
		public DataTable TvpTrafficUser
		{
			get
			{
				return (DataTable)this[TrafficUserDetail.TvpTrafficUserProp];
			}
			internal set
			{
				this[TrafficUserDetail.TvpTrafficUserProp] = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0003A92C File Offset: 0x00038B2C
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x0003A93E File Offset: 0x00038B3E
		public int TrafficType
		{
			get
			{
				return (int)this[TrafficUserDetail.TrafficTypeProp];
			}
			internal set
			{
				this[TrafficUserDetail.TrafficTypeProp] = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0003A951 File Offset: 0x00038B51
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x0003A963 File Offset: 0x00038B63
		public DateTime LogTime
		{
			get
			{
				return (DateTime)this[TrafficUserDetail.LogTimeProp];
			}
			internal set
			{
				this[TrafficUserDetail.LogTimeProp] = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0003A976 File Offset: 0x00038B76
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x0003A988 File Offset: 0x00038B88
		public Guid DomainId
		{
			get
			{
				return (Guid)this[TrafficUserDetail.DomainIdProp];
			}
			internal set
			{
				this[TrafficUserDetail.DomainIdProp] = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0003A99B File Offset: 0x00038B9B
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x0003A9AD File Offset: 0x00038BAD
		public string AddressPrefix
		{
			get
			{
				return (string)this[TrafficUserDetail.AddressPrefixProp];
			}
			internal set
			{
				this[TrafficUserDetail.AddressPrefixProp] = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0003A9BB File Offset: 0x00038BBB
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x0003A9CD File Offset: 0x00038BCD
		public string AddressDomain
		{
			get
			{
				return (string)this[TrafficUserDetail.AddressDomainProp];
			}
			internal set
			{
				this[TrafficUserDetail.AddressDomainProp] = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0003A9DB File Offset: 0x00038BDB
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x0003A9ED File Offset: 0x00038BED
		public int MessageCount
		{
			get
			{
				return (int)this[TrafficUserDetail.MessageCountProp];
			}
			internal set
			{
				this[TrafficUserDetail.MessageCountProp] = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0003AA00 File Offset: 0x00038C00
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x0003AA12 File Offset: 0x00038C12
		public long MessageSize
		{
			get
			{
				return (long)this[TrafficUserDetail.MessageSizeProp];
			}
			internal set
			{
				this[TrafficUserDetail.MessageSizeProp] = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0003AA28 File Offset: 0x00038C28
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Concat(new string[]
				{
					this.DomainId.ToString(),
					this.LogTime.ToString(),
					this.AddressPrefix,
					this.AddressDomain,
					this.TrafficType.ToString()
				}));
			}
		}

		// Token: 0x0400094D RID: 2381
		internal static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("tenantId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400094E RID: 2382
		internal static readonly HygienePropertyDefinition TvpTrafficUserProp = new HygienePropertyDefinition("Tvp_TrafficUser", typeof(DataTable));

		// Token: 0x0400094F RID: 2383
		internal static readonly HygienePropertyDefinition TvpDomainIdsProp = new HygienePropertyDefinition("tvp_DomainIds", typeof(DataTable));

		// Token: 0x04000950 RID: 2384
		internal static readonly HygienePropertyDefinition TrafficTypeProp = new HygienePropertyDefinition("trafficType", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000951 RID: 2385
		internal static readonly HygienePropertyDefinition LogTimeProp = new HygienePropertyDefinition("LogTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000952 RID: 2386
		internal static readonly HygienePropertyDefinition DomainIdProp = new HygienePropertyDefinition("DomainID", typeof(Guid));

		// Token: 0x04000953 RID: 2387
		internal static readonly HygienePropertyDefinition AddressPrefixProp = new HygienePropertyDefinition("AddressPrefix", typeof(string));

		// Token: 0x04000954 RID: 2388
		internal static readonly HygienePropertyDefinition AddressDomainProp = new HygienePropertyDefinition("AddressDomain", typeof(string));

		// Token: 0x04000955 RID: 2389
		internal static readonly HygienePropertyDefinition MessageCountProp = new HygienePropertyDefinition("MessageCount", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000956 RID: 2390
		internal static readonly HygienePropertyDefinition MessageSizeProp = new HygienePropertyDefinition("MessageSize", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
