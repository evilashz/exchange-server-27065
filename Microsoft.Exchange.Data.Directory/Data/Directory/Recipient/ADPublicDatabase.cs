using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	internal sealed class ADPublicDatabase : ADRecipient
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0006DA81 File Offset: 0x0006BC81
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADPublicDatabase.schema;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0006DA88 File Offset: 0x0006BC88
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADPublicDatabase.MostDerivedClass;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0006DA8F File Offset: 0x0006BC8F
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0006DAA2 File Offset: 0x0006BCA2
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0006DAA9 File Offset: 0x0006BCA9
		internal ADPublicDatabase(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006DAB3 File Offset: 0x0006BCB3
		public ADPublicDatabase()
		{
		}

		// Token: 0x04000B6B RID: 2923
		private static readonly ADPublicDatabaseSchema schema = ObjectSchema.GetInstance<ADPublicDatabaseSchema>();

		// Token: 0x04000B6C RID: 2924
		internal static string MostDerivedClass = "msExchPublicMdb";
	}
}
