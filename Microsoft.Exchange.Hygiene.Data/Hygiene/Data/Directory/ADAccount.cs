using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	internal class ADAccount : ADObject
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00014068 File Offset: 0x00012268
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001407A File Offset: 0x0001227A
		internal string DisplayName
		{
			get
			{
				return this[ADAccountSchema.DisplayNameProperty] as string;
			}
			set
			{
				this[ADAccountSchema.DisplayNameProperty] = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00014088 File Offset: 0x00012288
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADAccount.schema;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001408F File Offset: 0x0001228F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADAccount.mostDerivedClass;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00014096 File Offset: 0x00012296
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040003B3 RID: 947
		private static readonly ADAccountSchema schema = ObjectSchema.GetInstance<ADAccountSchema>();

		// Token: 0x040003B4 RID: 948
		private static string mostDerivedClass = "ADAccount";
	}
}
