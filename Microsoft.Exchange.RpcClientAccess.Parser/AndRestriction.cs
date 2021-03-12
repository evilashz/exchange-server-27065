using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000207 RID: 519
	internal sealed class AndRestriction : CompositeRestriction
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00024304 File Offset: 0x00022504
		internal AndRestriction(Restriction[] childRestrictions) : base(childRestrictions)
		{
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002430D File Offset: 0x0002250D
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.And;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00024318 File Offset: 0x00022518
		internal new static AndRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			return CompositeRestriction.InternalParse<AndRestriction>(reader, (Restriction[] childRestrictions) => new AndRestriction(childRestrictions), wireFormatStyle, depth);
		}
	}
}
