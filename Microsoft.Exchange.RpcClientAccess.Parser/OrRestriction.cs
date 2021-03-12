using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000208 RID: 520
	internal sealed class OrRestriction : CompositeRestriction
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0002433F File Offset: 0x0002253F
		internal OrRestriction(Restriction[] childRestrictions) : base(childRestrictions)
		{
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00024348 File Offset: 0x00022548
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Or;
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00024353 File Offset: 0x00022553
		internal new static OrRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			return CompositeRestriction.InternalParse<OrRestriction>(reader, (Restriction[] childRestrictions) => new OrRestriction(childRestrictions), wireFormatStyle, depth);
		}
	}
}
