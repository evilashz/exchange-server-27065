using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Transport;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x0200010E RID: 270
	internal class TransportRuleCollectionFacade : ADObject
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001F2B3 File Offset: 0x0001D4B3
		public override ObjectId Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0001F2B6 File Offset: 0x0001D4B6
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public byte[] FileData
		{
			get
			{
				return (byte[])this[TransportRuleCollectionFacadeSchema.FileData];
			}
			set
			{
				this[TransportRuleCollectionFacadeSchema.FileData] = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0001F2D6 File Offset: 0x0001D4D6
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		public MigrationSourceType MigrationSource
		{
			get
			{
				return (MigrationSourceType)this[TransportRuleCollectionFacadeSchema.MigrationSource];
			}
			set
			{
				this[TransportRuleCollectionFacadeSchema.MigrationSource] = value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001F2FB File Offset: 0x0001D4FB
		internal override ADObjectSchema Schema
		{
			get
			{
				return TransportRuleCollectionFacade.schema;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0001F302 File Offset: 0x0001D502
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TransportRuleCollectionFacade.mostDerivedClass;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001F309 File Offset: 0x0001D509
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000560 RID: 1376
		private static readonly TransportRuleCollectionFacadeSchema schema = ObjectSchema.GetInstance<TransportRuleCollectionFacadeSchema>();

		// Token: 0x04000561 RID: 1377
		private static string mostDerivedClass = "TransportRuleCollectionFacade";
	}
}
