using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E1 RID: 225
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetNamesFromIDsResultFactory : StandardResultFactory
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000F201 File Offset: 0x0000D401
		internal GetNamesFromIDsResultFactory() : base(RopId.GetNamesFromIDs)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000F20B File Offset: 0x0000D40B
		public RopResult CreateSuccessfulResult(NamedProperty[] namedProperties)
		{
			return new SuccessfulGetNamesFromIDsResult(namedProperties);
		}
	}
}
