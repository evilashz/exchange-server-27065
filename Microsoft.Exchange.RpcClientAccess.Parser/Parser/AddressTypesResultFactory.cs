using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AddressTypesResultFactory : StandardResultFactory
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000E7B9 File Offset: 0x0000C9B9
		internal AddressTypesResultFactory() : base(RopId.AddressTypes)
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000E7C3 File Offset: 0x0000C9C3
		public RopResult CreateSuccessfulResult(string[] addressTypes)
		{
			return new SuccessfulAddressTypesResult(addressTypes);
		}
	}
}
