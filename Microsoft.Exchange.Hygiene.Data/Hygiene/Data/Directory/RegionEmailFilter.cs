using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F9 RID: 249
	public class RegionEmailFilter : ADObject
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001E0E7 File Offset: 0x0001C2E7
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0001E0F9 File Offset: 0x0001C2F9
		public bool Enabled
		{
			get
			{
				return (bool)this[RegionEmailFilterSchema.FilterStatus];
			}
			set
			{
				this[RegionEmailFilterSchema.FilterStatus] = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001E10C File Offset: 0x0001C30C
		internal override ADObjectSchema Schema
		{
			get
			{
				return RegionEmailFilter.schema;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001E113 File Offset: 0x0001C313
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RegionEmailFilter.mostDerivedClass;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001E11A File Offset: 0x0001C31A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000525 RID: 1317
		private static readonly string mostDerivedClass = "RegionEmailFilter";

		// Token: 0x04000526 RID: 1318
		private static readonly RegionEmailFilterSchema schema = ObjectSchema.GetInstance<RegionEmailFilterSchema>();
	}
}
