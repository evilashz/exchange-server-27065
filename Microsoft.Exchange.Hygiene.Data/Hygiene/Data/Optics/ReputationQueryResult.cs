using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B8 RID: 440
	internal class ReputationQueryResult : ConfigurablePropertyBag
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x00038278 File Offset: 0x00036478
		public ReputationQueryResult()
		{
			this.identity = new ConfigObjectId(Guid.NewGuid().ToString());
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x000382A9 File Offset: 0x000364A9
		public override ObjectId Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x000382B1 File Offset: 0x000364B1
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x000382C3 File Offset: 0x000364C3
		public long Value
		{
			get
			{
				return (long)this[ReputationQueryResult.ValueProperty];
			}
			set
			{
				this[ReputationQueryResult.ValueProperty] = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x000382D6 File Offset: 0x000364D6
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x000382E8 File Offset: 0x000364E8
		public byte EntityType
		{
			get
			{
				return (byte)this[ReputationQueryResult.EntityTypeProperty];
			}
			set
			{
				this[ReputationQueryResult.EntityTypeProperty] = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x000382FB File Offset: 0x000364FB
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x0003830D File Offset: 0x0003650D
		public string EntityKey
		{
			get
			{
				return (string)this[ReputationQueryResult.EntityKeyProperty];
			}
			set
			{
				this[ReputationQueryResult.EntityKeyProperty] = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0003831B File Offset: 0x0003651B
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0003832D File Offset: 0x0003652D
		public int DataPointType
		{
			get
			{
				return (int)this[ReputationQueryResult.DataPointTypeProperty];
			}
			set
			{
				this[ReputationQueryResult.DataPointTypeProperty] = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00038340 File Offset: 0x00036540
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00038352 File Offset: 0x00036552
		public int UdpTimeout
		{
			get
			{
				return (int)this[ReputationQueryResult.UdpTimeoutProperty];
			}
			set
			{
				this[ReputationQueryResult.UdpTimeoutProperty] = value;
			}
		}

		// Token: 0x040008CD RID: 2253
		public static readonly HygienePropertyDefinition QueryInputCountProperty = new HygienePropertyDefinition("inputcount", typeof(int?));

		// Token: 0x040008CE RID: 2254
		public static readonly HygienePropertyDefinition EntityTypeProperty = new HygienePropertyDefinition("entitytype", typeof(byte));

		// Token: 0x040008CF RID: 2255
		public static readonly HygienePropertyDefinition EntityKeyProperty = new HygienePropertyDefinition("entitykey", typeof(string));

		// Token: 0x040008D0 RID: 2256
		public static readonly HygienePropertyDefinition DataPointTypeProperty = new HygienePropertyDefinition("datapointtype", typeof(int?));

		// Token: 0x040008D1 RID: 2257
		public static readonly HygienePropertyDefinition UdpTimeoutProperty = new HygienePropertyDefinition("timeout", typeof(long?));

		// Token: 0x040008D2 RID: 2258
		public static readonly HygienePropertyDefinition FlagsProperty = new HygienePropertyDefinition("flags", typeof(int?));

		// Token: 0x040008D3 RID: 2259
		public static readonly HygienePropertyDefinition ValueProperty = new HygienePropertyDefinition("value", typeof(long?));

		// Token: 0x040008D4 RID: 2260
		private ObjectId identity;
	}
}
