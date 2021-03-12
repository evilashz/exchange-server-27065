using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	internal class ADForeignPrincipal : ADObject
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00014239 File Offset: 0x00012439
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001424B File Offset: 0x0001244B
		internal ADObjectId ForeignPrincipalId
		{
			get
			{
				return this[ADForeignPrincipalSchema.ForeignPrincipalIdProperty] as ADObjectId;
			}
			set
			{
				this[ADForeignPrincipalSchema.ForeignPrincipalIdProperty] = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00014259 File Offset: 0x00012459
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001426B File Offset: 0x0001246B
		internal ADObjectId ForeignContextId
		{
			get
			{
				return this[ADForeignPrincipalSchema.ForeignContextIdProperty] as ADObjectId;
			}
			set
			{
				this[ADForeignPrincipalSchema.ForeignContextIdProperty] = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00014279 File Offset: 0x00012479
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001428B File Offset: 0x0001248B
		internal string DisplayName
		{
			get
			{
				return this[ADForeignPrincipalSchema.DisplayNameProperty] as string;
			}
			set
			{
				this[ADForeignPrincipalSchema.DisplayNameProperty] = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00014299 File Offset: 0x00012499
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x000142AB File Offset: 0x000124AB
		internal string Description
		{
			get
			{
				return this[ADForeignPrincipalSchema.DescriptionProperty] as string;
			}
			set
			{
				this[ADForeignPrincipalSchema.DescriptionProperty] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000142B9 File Offset: 0x000124B9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADForeignPrincipal.schema;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000142C0 File Offset: 0x000124C0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADForeignPrincipal.mostDerivedClass;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000142C7 File Offset: 0x000124C7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040003C1 RID: 961
		private static readonly ADForeignPrincipalSchema schema = ObjectSchema.GetInstance<ADForeignPrincipalSchema>();

		// Token: 0x040003C2 RID: 962
		private static string mostDerivedClass = "ADForeignPrincipal";
	}
}
