using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000218 RID: 536
	internal sealed class FalseRestriction : Restriction
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00025047 File Offset: 0x00023247
		internal FalseRestriction()
		{
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002504F File Offset: 0x0002324F
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.False;
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00025056 File Offset: 0x00023256
		internal new static FalseRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			return new FalseRestriction();
		}
	}
}
