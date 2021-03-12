using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000FB RID: 251
	internal class RoleGroupFacade : ADObject
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001E16A File Offset: 0x0001C36A
		public override ObjectId Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001E16D File Offset: 0x0001C36D
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0001E17F File Offset: 0x0001C37F
		public MultiValuedProperty<string> Members
		{
			get
			{
				return (MultiValuedProperty<string>)this[RoleGroupFacadeSchema.Members];
			}
			set
			{
				this[RoleGroupFacadeSchema.Members] = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001E18D File Offset: 0x0001C38D
		internal override ADObjectSchema Schema
		{
			get
			{
				return RoleGroupFacade.schema;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001E194 File Offset: 0x0001C394
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RoleGroupFacade.mostDerivedClass;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001E19B File Offset: 0x0001C39B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000528 RID: 1320
		private static readonly RoleGroupFacadeSchema schema = ObjectSchema.GetInstance<RoleGroupFacadeSchema>();

		// Token: 0x04000529 RID: 1321
		private static string mostDerivedClass = "RoleGroupFacade";
	}
}
