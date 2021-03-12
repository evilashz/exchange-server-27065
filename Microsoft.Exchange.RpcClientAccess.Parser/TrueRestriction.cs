using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000217 RID: 535
	internal sealed class TrueRestriction : Restriction
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00025031 File Offset: 0x00023231
		internal TrueRestriction()
		{
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00025039 File Offset: 0x00023239
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.True;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00025040 File Offset: 0x00023240
		internal new static TrueRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			return new TrueRestriction();
		}
	}
}
